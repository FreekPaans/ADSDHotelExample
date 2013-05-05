$(function() {
	$('.search_room_results > li').each(function() {	
		var $this = $(this);
		$this.find('.reserve_room_form').appendTo($this.find('.price_block'));
	});
});