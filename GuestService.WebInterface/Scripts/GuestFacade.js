(function () {
	window.GuestService = {
		FindReservationsByGuestName: function (name, result) {
			$.ajax({
				url: '/GuestService/FindReservationsByGuestName',
				data: { name: name },
				type: 'get',
				success: function (data) {
					result(data);
				},
				error: function () {
					result([]);
				}
			});
		}
	};
})();