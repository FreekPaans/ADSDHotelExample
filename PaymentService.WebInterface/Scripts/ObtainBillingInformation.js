$(function() {
	var $billingContainer = $('.obtain_billing_information');
	$(document).bind('submit_reservation_form', function() {
		$(document).trigger('handling_submit_reservation_form');
		var data={};
			
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
