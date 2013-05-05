$(function () {
	var $fromInput = $("input[name=search_from]");
	var $tillInput = $("input[name=search_till]");
	
	$fromInput.datepicker({
		defaultDate: "+1w",
		changeMonth: true,
		numberOfMonths: 2,
		minDate:"today",
		onClose: function( selectedDate ) {
			$tillInput.datepicker( "option", "minDate", selectedDate );
		}
	});
	$tillInput.datepicker({
		defaultDate: "+1w",
		changeMonth: true,
		numberOfMonths: 2,
		mindate:"today",
		onClose: function( selectedDate ) {
			$fromInput.datepicker( "option", "maxDate", selectedDate );
		}
	});
});
