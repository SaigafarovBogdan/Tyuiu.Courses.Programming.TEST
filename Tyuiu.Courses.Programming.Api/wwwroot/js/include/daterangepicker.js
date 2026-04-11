var startDate;
var endDate;

function setDateRangePicker({ inputSelector, startDateSelector, endDateSelector, minDateLimit, maxDateLimit }) {
	var options = {
		opens: 'right',
		autoApply: true,
		locale: {
			format: getDateFormat(),
			applyLabel: 'Принять',
			cancelLabel: 'Отменить',
			fromLabel: 'От',
			toLabel: 'До',
			weekLabel: 'W',
			firstDay: 1,
			daysOfWeek: [
				'Вс',
				'Пн',
				'Вт',
				'Ср',
				'Чт',
				'Пт',
				'Сб',
			],
			monthNames: [
				'Январь',
				'Февраль',
				'Март',
				'Апрель',
				'Май',
				'Июнь',
				'Июль',
				'Август',
				'Сентябрь',
				'Октябрь',
				'Ноябрь',
				'Декабрь'
			]
		}
	};

	if (startDateSelector) {
		options.startDate = $(startDateSelector).val();
	}

	if (endDateSelector) {
		options.endDate = $(endDateSelector).val();
	}

	if (minDateLimit) {
		options.minDate = minDateLimit;
	}

	if (maxDateLimit) {
		options.maxDate = maxDateLimit;
	}

	$(inputSelector).daterangepicker(options, function (start, end) {
		$(startDateSelector).val(start.format(getDateFormat()));
		$(endDateSelector).val(end.format(getDateFormat()));

		startDate = start;
		endDate = end;
	});
}

function getDateFormat() {
	return 'DD.MM.YYYY';
}

function getStartDate() {
	return startDate;
}

function getEndDate() {
	return endDate;
}