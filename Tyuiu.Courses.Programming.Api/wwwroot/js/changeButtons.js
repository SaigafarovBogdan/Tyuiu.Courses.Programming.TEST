document.addEventListener('DOMContentLoaded', function () {
    var buttons = document.getElementsByClassName('addStudent');
    for (var i = 0; i < buttons.length; i++) {
        buttons[i].addEventListener('click', function () {
            var listItem = this.closest('.list-group-item');
            var addButton = this;

            var nameSpan = listItem.querySelector('span.col-lg-10');
            var buttonsDiv = listItem.querySelector('div.d-grid');

            nameSpan.classList.remove('col-lg-10');
            nameSpan.classList.add('col-lg-6');

            buttonsDiv.classList.remove('col-lg-2');
            buttonsDiv.classList.add('col-lg-6');

            var span = document.createElement('span');
            span.setAttribute('class', 'd-inline-block');
            span.setAttribute('tabindex', '0');
            span.setAttribute('data-bs-toggle', 'popover');
            span.setAttribute('data-bs-placement', 'top');
            span.setAttribute('data-bs-trigger', 'hover focus');
            span.setAttribute('data-bs-content', 'Студент уже зачислен в группу ПКТб-21-1');

            var enrolledButton = document.createElement('button');
            enrolledButton.setAttribute('type', 'button');
            enrolledButton.setAttribute('class', 'btn btn-primary btn-sm me-md-2 disabled');
            enrolledButton.innerHTML = 'Зачислен';

            var addToGroupButton = document.createElement('button');
            addToGroupButton.setAttribute('type', 'button');
            addToGroupButton.setAttribute('class', 'btn btn-outline-primary btn-sm');
            addToGroupButton.innerHTML = 'Зачислить в эту группу';

            addButton.remove();
            span.appendChild(enrolledButton);
            buttonsDiv.innerHTML = '';
            buttonsDiv.appendChild(span);
            buttonsDiv.appendChild(addToGroupButton);

            // Инициализация Popover после добавления кнопки
            var popovers = listItem.querySelectorAll('[data-bs-toggle="popover"]');
            var popoverOptions = { trigger: 'hover' };
            popovers.forEach(function (popover) {
                var bsPopover = new bootstrap.Popover(popover, popoverOptions);
            });
        });
    }
});
