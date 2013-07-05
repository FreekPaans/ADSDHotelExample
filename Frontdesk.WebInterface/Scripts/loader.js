$(function () {
	var $overlay = $('<div></div>').css({ position: 'absolute', width: '100%', height: '100%', opacity: 0.5, 'background-color': 'black', top: '0px', left: '0px' });
	$overlay.append($('<img src="/Content/Images/ajax-loader.gif" />').css({ left: '50%', top: '50%', position: 'absolute' }));

	window.showLoader = function () {
		$('body').append($overlay);
	};
	window.hideLoader = function () {
		$overlay.remove();
	};
});