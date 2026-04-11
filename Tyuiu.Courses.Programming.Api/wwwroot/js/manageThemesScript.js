// Функция, которая предотвращает открытие аккордеона при нажатии на кнопки
function notOpen() {
	var buttons = document.querySelectorAll('.buttonsFolder button');

	buttons.forEach(function (button) {
		button.addEventListener('mouseenter', function (e) {
			var accordionButton = this.closest('.accordion-button');
			if (!accordionButton) return;
			accordionButton.setAttribute('data-bs-toggle', '');
		});
		button.addEventListener('mouseleave', function (e) {
			var accordionButton = this.closest('.accordion-button');
			if (!accordionButton) return;
			accordionButton.setAttribute('data-bs-toggle', 'collapse');
			accordionButton.setAttribute('data-bs-target', '#' + accordionButton.getAttribute('aria-controls'));
		});
	});
}

document.addEventListener('DOMContentLoaded', notOpen);

//Функция регулирует стилизация аккордеона в зависимости от раскрытия
document.addEventListener('DOMContentLoaded', function () {
	const accordionCollapses = document.querySelectorAll('.accordion-collapse');
	const accordionItems = document.querySelectorAll('.accordion-item');

	// Обработка последнего accordion-collapse
	if (accordionCollapses.length > 0) {
		const lastCollapse = accordionCollapses[accordionCollapses.length - 1];
		const listItems = lastCollapse.querySelectorAll('li');

		if (listItems.length > 0) {
			const lastListItem = listItems[listItems.length - 1];
			lastListItem.classList.add('border-bottom-0', 'rounded-bottom');
		}
	}

	// Обработка последнего accordion-item
	if (accordionItems.length > 0) {
		const lastItem = accordionItems[accordionItems.length - 1];
		const lastButton = lastItem.querySelector('.accordion-button');

		lastItem.classList.add('rounded-bottom');
		lastItem.classList.add('border-bottom-0');
		lastButton.classList.add('rounded-bottom');

		const buttons = document.querySelectorAll('.accordion-button');
		buttons.forEach(button => {
			button.addEventListener('click', function () {
				if (this === lastButton) {
					if (!this.classList.contains('collapsed')) {
						this.classList.remove('rounded-bottom');
					} else {
						this.classList.add('rounded-bottom');
					}
				}
			});
		});
	}
});