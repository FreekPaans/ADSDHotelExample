$(function() {
	var $billingContainer = $('.obtain_billing_information');
	

	var validateElement = function (locator, validator, errorMessage) {
		var $input = $billingContainer.find(locator);
		$input.parent().find('label.error').remove();

		if (!validator($input)) {
			$input.parent().append($('<label class="error"></label>').text(errorMessage));
			return false;
		}
		return true;
	}

	var validateCreditCardNumber = function () {
		return validateElement('input[name=Payment\\.CreditCardNumber]', function($input) {
			return $.trim($input.val()).length != 0;
		}, 'Please provide a valid credit card number');
				
	}
	var validateCreditCardExpiration = function() {
		return validateElement("input[name=Payment\\.CreditCardExperiationDate]", function ($input) {
			var trimmed = $.trim($input.val());
			
			return Helpers.ValidateExpirationDate(trimmed);

		}, 'Please enter a valid expiration date');
	}
	var validateBillingStreet = function () {
		return validateElement("input[name=Payment\\.Address\\.Street]", function ($input) {
			return $.trim($input.val()) != 0;
		}, 'Please enter a valid street');
	}
	var validateBillingPostcalCode = function () {
		return validateElement("input[name=Payment\\.Address\\.PostalCode]", function ($input) {
			return $.trim($input.val()) != 0;
		}, 'Please enter a valid postal code');
	}
	var validate = function () {
		var isValid = true;
		
		if (!validateCreditCardNumber()) {
			isValid = false;
		}
		if (!validateCreditCardExpiration()) {
			isValid = false;
		}
		if (!validateBillingStreet()) {
			isValid = false;
		}
		if (!validateBillingPostcalCode()) {
			isValid = false;
		}

		
		return isValid;
	}

	$(document).bind('validate_reservation_form', function() {
		$(document).trigger('handling_validate_reservation_form');
		var isValid = validate();
		$(document).trigger('handled_validate_reservation_form', isValid);
	});

	$(document).bind('submit_reservation_form', function() {
		$(document).trigger('handling_submit_reservation_form');
		var data={};
		
		if(!validate()) {
			$(document).trigger('handled_submit_reservation_form',false);
		}

		$billingContainer.find('input').each(function() {
			var $this=$(this);
			data[$this.attr('name')] = $this.val();
		});
		$.ajax({
			url: '/PaymentService/StorePaymentInformation',
			data: data,
			method: 'post',
			complete: function(xhr,status) {
				$(document).trigger('handled_submit_reservation_form',status=="success");
				return;
			}
		});
	});

	Helpers = {
		ValidateExpirationDate: function (value) {
			if (value.length == 0) {
				return false;
			}
			var monthRegex = /^([0-9]{1,2})-([0-9]{4})$/;
			if (!monthRegex.test(value)) {
				return false;
			}

			var matches = monthRegex.exec(value);

			var month = parseInt(matches[1], 10);
			var year = parseInt(matches[2], 10);
			if (month > 12) {
				return false;
			}

			if (new Date(year, month, 1) < new Date()) {
				return false;
			}
			return true;
		}
	}
});
