(function () {
	if (typeof (window.ReservationService) === "undefined") {
		window.ReservationService = {};
	}

	$.extend(window.ReservationService, {
		Exists: function (reservationId, resultHandler) {
			$.ajax({
				type: 'GET',
				url: '/ReservationService/ReservationExists',
				data: { reservationId: reservationId},
				success: function (result) {
					resultHandler(result);
				},
				error: function (result) {
					alert('error: ' + result.responseText);
					resultHandler(false);
				}
			});
		}
	});

})();