$(function () {
	$('.current_date_selector').datepicker({
		dateFormat: 'm/d/yy',
		onSelect: function () {
			var $this = $(this);
			document.cookie = $this.data('datecookiename') + '=' + escape($this.val()) + ';path=/';
			window.location = window.location;
		}
	});

	$('.current_time_selector').change(function () {
		var $this = $(this);
		document.cookie = $this.data('timecookiename') + '=' + $this.val() + ';path=/';
		window.location = window.location;
	});

	var showReservationDetails = function (reservationId) {
		$.ajax({
			type: 'get',
			url: '/Home/RenderReservation',
			data: {
				reservationId: reservationId
			},
			success: function (result) {
				$('.reservation_details').html(result).show();

			}
		});
	}

	$('.search_by_reservation_number').submit(function (ev) {
		ev.preventDefault();

		var reservationId = $(this).find('input[type=text]').val();

		ReservationService.Exists(reservationId, function (exists) {
			if (!exists) {
				alert("Reservation not found");
				return;
			}
			showReservationDetails(reservationId);
		});
	});

	$('.search_by_guest_name').submit(function (ev) {
		ev.preventDefault();
		var reservationId = GuestService.GetReservationIdByGuestName($(this).find('input[type=text]').val());

		if (reservationId == null) {
			alert("Reservation not found");
			return;
		}

		showReservationDetails(reservationId);
	});
});