$(function () {
	var $guestContainer = $('.reservation_obtain_guest_details');
	$(document).bind('submit_reservation_form', function () {
		$(document).trigger('handling_submit_reservation_form');

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
