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

	var showReservationDetails = function (reservationId, complete) {
		$.ajax({
			type: 'get',
			url: '/Reservation/RenderSearchSummary',
			data: {
				reservationId: reservationId
			},
			success: function (result) {
				
				$('.reservation_details').html(result).show();
			},
			complete: function () {
				if (typeof (complete) !== 'function') {
					return;
				}
				complete();
			}
		});
	}

	var showReservationsOverview = function (reservationIds, complete) {
		$.ajax({
			type: 'get',
			url: '/Reservation/RenderReservationsOverview',
			data: {
				reservationIds: reservationIds
			},
			traditional: true,
			success: function (html) {

				$('.search_guest_result').remove();
				$('.find_reservation').after(html);

				$('.reservations_overview tbody tr').bind('click', function () {
					var reservationId = $(this).data('reservationid');
					showReservationDetails(reservationId);
				});
			},
			complete: function () {
				complete();
			}
		});
	}

	$('.search_by_reservation_number').submit(function (ev) {
		ev.preventDefault();

		var reservationId = $(this).find('input[type=text]').val();

		showLoader();
		ReservationService.Exists(reservationId, function (exists) {
			if (!exists) {
				alert("Reservation not found");
				hideLoader();
				return;
			}
			$('.search_guest_result').remove();
			showReservationDetails(reservationId, function () {
				hideLoader();
			});
		});
	});

	$('.search_by_guest_name').submit(function (ev) {
		ev.preventDefault();
		showLoader();
		var reservationId = GuestService.FindReservationsByGuestName($(this).find('input[type=text]').val(), function (reservationIds) {
			
			if (reservationIds.length==0) {
				alert("No reservation found");
				hideLoader();
				return;
			}
			if (reservationIds.length == 1) {
				showReservationDetails(reservationIds[0], function () {
					hideLoader();
				});
				return;
			}
			showReservationsOverview(reservationIds, function () { hideLoader(); });
			
		});

		
	});
});