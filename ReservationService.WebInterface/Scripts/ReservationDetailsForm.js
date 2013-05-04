$(function() {
	var requestsToFinish = 0;
	var hadError = false;
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


	$('.reservation_details_form').bind('submit', function(ev) {
		ev.preventDefault();
		$(document).trigger('submit_reservation_form');
		finishSubmit();
	});
	//$(document).ajaxStop(function() {
	//	//window.location = $('form.reservation_details_form').attr('action');
	//});
});