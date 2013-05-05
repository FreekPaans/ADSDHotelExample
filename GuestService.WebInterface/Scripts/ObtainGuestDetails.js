$(function () {
	var $guestContainer = $('.reservation_obtain_guest_details');
	
	var validate = function(){
		var isValid = true;
		$guestContainer.find('input').each(function() {
			if(!$(this).valid()) {
				isValid = false;
			}
		});
		return isValid;
	}

	$(document).bind('validate_reservation_form', function() {
		$(document).trigger('handling_validate_reservation_form');

		var isValid = validate();

		$(document).trigger('handled_validate_reservation_form',isValid);
	});
	$(document).bind('submit_reservation_form', function () {
		$(document).trigger('handling_submit_reservation_form');

		if(!validate()) {
			$(document).trigger('handled_submit_reservation_form',false);
			return;
		}

		var data={};
		$guestContainer.find('input').each(function() {
			var $this=$(this);
			data[$this.attr('name')] = $this.val();
		});

		$.ajax({
			url: '/GuestService/StoreReservationInformation',
			data: data,
			type: "post",
			complete: function(xhr,status) {
				$(document).trigger('handled_submit_reservation_form',status=="success");
				return;
			}
		});
	});
});
