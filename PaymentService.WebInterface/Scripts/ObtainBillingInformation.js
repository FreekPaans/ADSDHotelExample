$(function() {
	var $billingContainer = $('.obtain_billing_information');
	
	var validate = function() {
		var $creditCardInput = $billingContainer.find('input[name=Payment\\.CreditCardNumber]');
		$creditCardInput.parent().find('label.error').remove();
		var isValid = true;
		if($.trim($creditCardInput.val()).length==0) {
			$creditCardInput.parent().append('<label class="error">Please provide a valid credit card number</label>');
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
});
