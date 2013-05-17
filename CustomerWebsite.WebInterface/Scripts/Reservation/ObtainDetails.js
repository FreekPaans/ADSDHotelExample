$(function () {
	var $guestDetails = $('#Guest_ObtainReservationDetails');
	var $paymentDetails = $('#Payment_ObtainReservationDetails');
	
	$('form.reservation_details_form input[type=submit]').before($.merge($guestDetails.children().get(), $paymentDetails.children().get()));


	var $cpy = $('<input type="button" name="CopyAddressBtn" value="Copy from guest details" />');

	$cpy.bind('click', function () {
		$('.reservation_obtain_guest_details').Address().CopyTo($('.obtain_billing_information'));
	});

	$('.obtain_billing_information h4').first().append($cpy);

	$guestDetails.remove();
	$paymentDetails.remove();

	//for testing purposes:
	$(document).bind('keypress', function (ev) {
		if (ev.charCode != 100) {
			return;
		}
		if (ev.ctrlKey == false) {
			return;
		}

		ev.preventDefault();
		ev.stopPropagation();
		
		$('input[name="GuestService\\.Firstname"]').val("John");
		$('input[name="GuestService\\.Lastname"]').val("Doe");
		$('input[name="GuestService\\.Email"]').val("john@doe.com");
		$('input[name="GuestService\\.Phonenumber"]').val("555-123456");

		$('input[name="Payment\\.Address\\.Firstname"]').val("Jane");
		$('input[name="Payment\\.Address\\.Lastname"]').val("Doe");
		$('input[name="Payment\\.Address\\.Street"]').val("101 Hollywood Blvd.");
		$('input[name="Payment\\.Address\\.PostalCode"]').val("110011");
		$('input[name="Payment\\.Address\\.City"]').val("Los Angelos");
		
		$('input[name="Payment\\.CreditCardNumber"]').val("1234567889765431");
		$('input[name="Payment\\.CreditCardExperiationDate"]').val("11-2013");

	});
});