$(function () {
	var $input = $('input[name=SelectedRoomNumber]');

	$('a.select_room').bind('click', function (ev) {
		ev.preventDefault();
		var $div = $(this).closest('div.selectable_room');
		$div.siblings('div.selectable_room.selected').removeClass('selected');
		$div.addClass('selected');

		$input.val($div.data('roomnumber'));
	});
});