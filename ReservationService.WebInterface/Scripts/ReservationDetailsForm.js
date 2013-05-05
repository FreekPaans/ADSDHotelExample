$(function() {
	var requestsToFinish;
	var hadError;

	var resetSubmit = function() {
		requestsToFinish = 0;
		hadError = false;
	}
	
	$(document).bind('handling_submit_reservation_form', function() {
		requestsToFinish++;
	});

	
	var finishSubmit = function() {
		if(requestsToFinish==0) {
			if(!hadError) {
				window.location = $('form.reservation_details_form').attr('action');
				return;
			}
			alert('Failed handling requests');
		}
	}

	var triggerSubmit = function() {
		resetSubmit();
		$(document).trigger('submit_reservation_form');
		finishSubmit();
	}

	$(document).bind('handled_submit_reservation_form', function(ev,success) {
		requestsToFinish--;
		if(success===false) {
			hadError= true;
		}
		finishSubmit();
	});



	$(document).bind('submit_reservation_form', function() {
		$(document).trigger('handling_submit_reservation_form');
		$.ajax({
			url: '/ReservationService/Commit',
			type:'post',
			data: {
				"Reservation.ReservationId": $('input[name="Reservation.ReservationId"]').val()
			},
			complete: function(xhr,status) {
				$(document).trigger('handled_subit_reservation_form',status=="success");
			}
		});
	});

	var $reservationForm = $('.reservation_details_form');
	$reservationForm.validate({onsubmit: false});
	$reservationForm.bind('submit', function(ev) {
		ev.preventDefault();
		resetValidation();
		$(document).trigger('validate_reservation_form');
		isValidationRegistrationClosed = true;
		finishValidation();
	});
	
	var validationsToFinish;
	var hadValidationError;
	var validationFinished;
	var isValidationRegistrationClosed;
	
	$(document).bind('handling_validate_reservation_form', function() {
		validationsToFinish++;
	});

	var resetValidation = function() {
		validationsToFinish = 0;
		hadValidationError = false;
		validationFinished = false;
		isValidationRegistrationClosed = false;
	}

	var finishValidation = function() {
		if(!isValidationRegistrationClosed || validationFinished) {
			return;
		}
		validationFinished=true;
		if(validationsToFinish==0) {
			if(!hadValidationError) {
				triggerSubmit();
				return;
			}
			alert('Form validation failed, please correct');
		}
	}
	$(document).bind('handled_validate_reservation_form', function(ev, success) {
		validationsToFinish--;
		
		if(success===false) {
			hadValidationError = true;
		}
		finishValidation();
	});
});