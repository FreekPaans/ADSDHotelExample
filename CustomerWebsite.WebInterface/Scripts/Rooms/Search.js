$(function () {
	
	var $roomPrices = $('#Pricing_RoomTypeIDsAvailable_RoomPrices');
	var $roomDetails = $('#RoomTypeDetails_RoomTypeIDsAvailable_RoomTypeDetails');

	$('.room_item').each(function() {	
		var $this = $(this);
		var roomTypeId = this.id;
		$this.append($roomPrices.find('[roomtypeid=' + roomTypeId + ']'));
		$this.append($roomDetails.find('[roomtypeid=' + roomTypeId + ']'));
		$this.find('.reserve_room_form').appendTo($this.find('.price_block'));
	});

	$roomDetails.remove();
	$roomPrices.remove();
});