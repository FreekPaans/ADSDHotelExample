$(function () {
	var pollCheckInResult = function (url) {
		$.ajax({
			url: url,
			type: 'get',
			success: function (data) {
				if (data.Success) {
					alert("Successfully checked in");
					window.location = "/";
				}
				else {
					alert("Failed checking in: " + data.FailReason);
				}
				hideLoader();
			},
			error: function (xhr, textStatus, errorThrown) {
				if (xhr.status == 404) {
					window.setTimeout(function () {
						pollCheckInResult(url);
						
					}, 1000);
					return;
				}

				alert("Unknown error: " + errorThrown)
				hideLoader();
			}
		});
	}
	$('.checkin_form').bind('submit', function (ev) {
		ev.preventDefault();

		var $form = $(this);
		
		showLoader();

		$.ajax({
			url: $form[0].action,
			type: 'post',
			data: $form.serialize(),
			success: function (resultUrl) {
				pollCheckInResult(resultUrl);
			}
		});
	});
});