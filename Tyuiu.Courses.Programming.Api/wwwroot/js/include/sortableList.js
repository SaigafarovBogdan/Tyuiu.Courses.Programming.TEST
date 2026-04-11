function setSortableList(listSelectorId, actionUrl) {
	new Sortable(document.getElementById(listSelectorId), {
		animation: 150,
		ghostClass: 'blue-background-class',
		onEnd: function (evt) {
			var itemsId = [];
			var items = evt.target.children;
			for (var i = 0; i < items.length; i++) {
				var itemId = items[i].getAttribute('data-id');
				itemsId.push(parseInt(itemId));
			}
			fetch(actionUrl, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
					'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
				},
				body: JSON.stringify(itemsId)
			});
		}
	});
}