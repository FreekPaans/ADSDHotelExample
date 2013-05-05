$(function () {
	var $guestContainer = $('.reservation_obtain_guest_details');
	$(document).bind('submit_reservation_form', function () {
		$(document).trigger('handling_submit_reservation_form');

		var data={};
		$guestContainer.find('input').each(function() {
			var $this=$(this);
			data[$this.attr('name')] = $this.val();
		});
		
		var $emailInput = $guestContainer.find('input[name=GuestService\\.Email]');

		var email= $emailInput.val();
		$emailInput.parent().find('.validation_error').remove();
		if(email.indexOf('@')==-1) {
			var $msg = $('<div class="validation_error">Please enter a valid email</div>');
			$msg.css('left', $emailInput.position().left);
			$emailInput.after($msg);
			$(document).trigger('handled_submit_reservation_form',false);
			return;
		}

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
