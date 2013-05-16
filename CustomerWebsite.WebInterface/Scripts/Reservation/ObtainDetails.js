$(function () {
	var $guestDetails = $('#Guest_ObtainReservationDetails');
	var $paymentDetails = $('#Payment_ObtainReservationDetails');
	
	$('#Reservations_DetailsForm form input[type=submit]').before($guestDetails.children().add($paymentDetails.children()));


	var $cpy = $('<input type="button" value="Copy from guest details" />');

	$cpy.bind('click', function () {
		$('.reservation_obtain_guest_details').Address().CopyTo($('.obtain_billing_information'));
	});

	$('.obtain_billing_information h4').first().append($cpy);

});