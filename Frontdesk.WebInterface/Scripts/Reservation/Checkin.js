$(function () {
	var $overlay = $('<div></div>').css({ position: 'absolute', width: '100%', height: '100%', opacity: 0.5, 'background-color': 'black', top: '0px', left: '0px' });
	$overlay.append($('<img src="/Content/Images/ajax-loader.gif" />').css({left: '50%', top: '50%', position:'absolute'}));

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
				$overlay.remove();
			},
			error: function (xhr, textStatus, errorThrown) {
				if (xhr.status == 404) {
					window.setTimeout(function () {
						pollCheckInResult(url);
						
					}, 1000);
					return;
				}

				alert("Unknown error: " + errorThrown)
				$overlay.remove();
			}
		});
	}
	$('.checkin_form').bind('submit', function (ev) {
		ev.preventDefault();

		var $form = $(this);
		
		$('body').append($overlay);

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