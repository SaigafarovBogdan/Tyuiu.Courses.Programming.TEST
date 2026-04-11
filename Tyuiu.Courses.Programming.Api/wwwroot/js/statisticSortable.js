let clickCounts;
let headerFilter = "";

$(document).ready(function () {
	clickCounts = {
		'Группа': 0,
		'ФИО': 0,
		'Спринт': 0,
		'Тема': 0,
		'Вариант': 0,
		'Дата сдачи': 0,
		'Результат': 0,
		'Повторы': 0,
		'Попытки': 0,
		'Баллы': 0
	};

	$('.table-header-btn').on('click', function () {
		var $btn = $(this);
		var $icon = $btn.find('.bi');
		var columnName = $btn.closest('th').text().trim();
		var currentOrder;

		$('.table-header-btn').not(this).each(function () {
			var $otherBtn = $(this);
			var $otherIcon = $otherBtn.find('.bi');
			var otherColumnName = $otherBtn.closest('th').text().trim();

			if (otherColumnName !== columnName) {
				$otherBtn.removeClass('btn-primary').addClass('btn-secondary');
				clickCounts[otherColumnName] = 0;
				if ($otherIcon.hasClass('bi-sort-alpha-down-alt')) {
					$otherIcon.removeClass('bi-sort-alpha-down-alt').addClass('bi-sort-alpha-down');
				}
				if ($otherIcon.hasClass('bi-sort-numeric-down-alt')) {
					$otherIcon.removeClass('bi-sort-numeric-down-alt').addClass('bi-sort-numeric-down');
				}
				if ($otherIcon.hasClass('bi-sort-down')) {
					$otherIcon.removeClass('bi-sort-down').addClass('bi-sort-down-alt');
				}
			}

		});

		switch (clickCounts[columnName]) {
			case 0:
				$btn.removeClass('btn-secondary').addClass('btn-primary');
				switch (columnName) {
					case 'Группа':
					case 'ФИО':
					case 'Спринт':
					case 'Тема':
					case 'Результат':
						$icon.addClass('bi-sort-alpha-down');
						currentOrder = "desc";
						break;
					case 'Вариант':
						$icon.addClass('bi-sort-numeric-down');
						currentOrder = "desc";
						break;
					case 'Дата сдачи':
					case 'Повторы':
					case 'Попытки':
					case 'Баллы':
						$icon.addClass('bi-sort-down-alt');
						currentOrder = "asc";
						break;
				}
				clickCounts[columnName]++;
				break;
			case 1:
				switch (columnName) {
					case 'Группа':
					case 'ФИО':
					case 'Спринт':
					case 'Тема':
					case 'Результат':
						$icon.removeClass('bi-sort-alpha-down').addClass('bi-sort-alpha-down-alt');
						currentOrder = "asc";
						break;
					case 'Вариант':
						$icon.removeClass('bi-sort-numeric-down').addClass('bi-sort-numeric-down-alt');
						currentOrder = "asc";
						break;
					case 'Дата сдачи':
					case 'Повторы':
					case 'Попытки':
					case 'Баллы':
						$icon.removeClass('bi-sort-down-alt').addClass('bi-sort-down');
						currentOrder = "desc";
						break;
				}
				clickCounts[columnName]++;
				break;
			case 2:
				switch (columnName) {
					case 'Группа':
					case 'ФИО':
					case 'Спринт':
					case 'Тема':
					case 'Результат':
						$icon.removeClass('bi-sort-alpha-down-alt').addClass('bi-sort-alpha-down');
						break;
					case 'Вариант':
						$icon.removeClass('bi-sort-numeric-down-alt').addClass('bi-sort-numeric-down');
						break;
					case 'Дата сдачи':
					case 'Повторы':
					case 'Попытки':
					case 'Баллы':
						$icon.removeClass('bi-sort-down').addClass('bi-sort-down-alt');
						break;
				}
				$btn.removeClass('btn-primary').addClass('btn-secondary');
				currentOrder = undefined;
				clickCounts[columnName] = 0;
				break;
		}

		const allHeaders = $(this).parent().parent().children();
		const currentHeader = $(this).parent();

		const columnIndex = allHeaders.index(currentHeader);

		headerFilter = `${currentOrder ? columnName : "Номер"}|${currentOrder || "asc"}`;
		filterTaskAnswers();
	});
});