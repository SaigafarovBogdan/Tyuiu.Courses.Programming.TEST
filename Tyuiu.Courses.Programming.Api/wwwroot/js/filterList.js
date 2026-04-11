function filterList(listSelector, searchTerm, itemsSelector, itemValueSelector) {
	if (searchTerm.length > 0) {
		$(`${listSelector} ${itemsSelector}`).each(function () {
			var name = $(this).find(`${itemValueSelector}`).text();

			if (name.toLocaleLowerCase().includes(searchTerm.toLocaleLowerCase())) {
				$(this).removeAttr('hidden');
			} else {
				$(this).attr('hidden', true);
			}
		});
	} else {
		$(`${listSelector} ${itemsSelector}[hidden]`).removeAttr('hidden');
	}
}