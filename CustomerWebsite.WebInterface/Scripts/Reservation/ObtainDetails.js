$(function () {
	var $guestDetails = $('#Guest_ObtainReservationDetails');
	var $paymentDetails = $('#Payment_ObtainReservationDetails');
	
	$('form.reservation_details_form input[type=submit]').before($.merge($guestDetails.children().get(), $paymentDetails.children().get()));


	var $cpy = $('<input type="button" value="Copy from guest details" />');

	$cpy.bind('click', function () {
		$('.reservation_obtain_guest_details').Address().CopyTo($('.obtain_billing_information'));
	});

	$('.obtain_billing_information h4').first().append($cpy);

	$guestDetails.remove();
	$paymentDetails.remove();
});