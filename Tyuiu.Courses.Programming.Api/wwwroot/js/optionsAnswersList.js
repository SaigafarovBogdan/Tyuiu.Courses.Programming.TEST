$(document).ready(function () {
    const list = document.querySelector('.listOptionAnswers');
    const addButton = document.querySelector('.AddNewOption');
    const selectElement = document.getElementById('Type');
    const saveButton = document.getElementById('saveBtn');

    const questionType = {
        single: 'Single',
        multiple: 'Multiple',
        handwritten: 'Handwritten',
    }

    addButton.setAttribute('disabled', true);
    saveButton.setAttribute('disabled', true);

    checkListLength();
    updateSaveButtonState();
    toggleIgnoreCaseButton(getSelectedType(selectElement));

    selectElement.addEventListener('change', () => {
        const selectedType = getSelectedType(selectElement);

        switch (selectedType) {
            case questionType.single:
            case questionType.multiple:
            case questionType.handwritten:
                addButton.removeAttribute('disabled');
                break;
            default:
                addButton.setAttribute('disabled', true);
                break;
        }

        toggleIgnoreCaseButton(selectedType);
    });

    let radioCounter = 0;
    let checkboxCounter = 0;

    function getSelectedType(selectElement) {
        if (!selectElement) {
            selectElement = document.getElementById('Type');
        }
        return selectElement.options[selectElement.selectedIndex].value;
    }

    function createNewListItem() {
        const newItem = document.createElement('li');
        newItem.classList.add('list-group-item', 'd-flex', 'align-items-center', 'gap-3');

        const input = document.createElement('input');
        input.classList.add('form-control');
        input.name = 'Text';
        input.maxLength = 1000;

        input.placeholder = 'Введите вариант ответа';

        const checkRadioBox = document.createElement('input');
        checkRadioBox.classList.add('form-check-input');
        checkRadioBox.name = 'IsCorrect';

        const selectedType = getSelectedType(selectElement);

        switch (selectedType) {
            case questionType.single:
                checkRadioBox.type = 'radio';
                checkRadioBox.id = `flexRadioDefault${radioCounter}`;

                input.for = `flexRadioDefault${radioCounter}`;
                newItem.appendChild(checkRadioBox);
                radioCounter++;
                break;
            case questionType.multiple:
                checkRadioBox.type = 'checkbox';
                checkRadioBox.id = `flexCheckDefault${checkboxCounter}`;

                input.for = `flexCheckDefault${checkboxCounter}`;
                newItem.appendChild(checkRadioBox);
                checkboxCounter++;
                break;
            case questionType.handwritten:
                break;
            default:
                console.log('Выбран другой вариант');
                break;
        };

        checkRadioBox.addEventListener('change', () => {
            updateSaveButtonState();
        });

        const deleteButton = document.createElement('button');
        deleteButton.classList.add('btn', 'btn-outline-danger', 'fs-5', 'btn-sm', 'ms-auto');
        deleteButton.innerHTML = '<i class="bi bi-trash"></i>';
        deleteButton.type = 'button';

        deleteButton.addEventListener('click', () => { removeAnswer(newItem) });

        newItem.appendChild(input);
        newItem.appendChild(deleteButton);

        return newItem;
    }

    $('[name="removeAnswer"]').on('click', function () {
        let item = $(this).closest('li');
        removeAnswer(item);
    });

    addButton.addEventListener('click', () => {
        const newItem = createNewListItem();
        list.appendChild(newItem);
        checkListLength();
        updateSaveButtonState();
    });

    function checkListLength() {

        if (list.children.length > 0) {
            selectElement.disabled = true;
        } else {
            selectElement.disabled = false;
        }

        if (list.children.length == 10) {
            addButton.setAttribute('disabled', true);
        }
        else if (list.children.length >= 1) {
            addButton.removeAttribute('disabled');
        }

		if (selectElement.value == questionType.handwritten && list.children.length == 1) {
			addButton.setAttribute('disabled', true);
		} else if (selectElement.value == questionType.handwritten && list.children.length < 1) {
			addButton.removeAttribute('disabled');
        }
    }

    function updateSaveButtonState() {
        const inputFields = document.querySelectorAll('input.form-control');
        inputFields.forEach(input => {
            input.addEventListener('input', updateSaveButtonState);
            input.addEventListener('change', updateSaveButtonState);
        });

        const filledInputCount = [...inputFields].filter(input => input.value.trim() !== '').length;
        if (selectElement.value == questionType.handwritten) {
            saveButton.disabled = filledInputCount < 1;
        } else {
            saveButton.disabled = filledInputCount < 2;
        }

        if (!hasCorrectAnswers()) {
            saveButton.disabled = true;
        }
    }

    function toggleIgnoreCaseButton(selectedType) {
        const ignoreRegBtn = document.querySelector('.ignoreRegBtn');
        const checkboxElement = document.getElementById('switchIgnoreReg');

        switch (selectedType) {
            case questionType.single:
            case questionType.multiple:
                ignoreRegBtn.classList.add('d-none');
                checkboxElement.checked = false;
                break;
            case questionType.handwritten:
                ignoreRegBtn.classList.remove('d-none');
                break;
            default:
                ignoreRegBtn.classList.add('d-none');
                checkboxElement.checked = false;
                break;
        }
    }

    const warningSaveQuestionModal = document.getElementById('warningSaveQuestionModal');
    const saveQuestionModal = document.getElementById('saveQuestionModal');
    const continueBtn = document.querySelector('.continueBtn');
    const modalWarning = new bootstrap.Modal(warningSaveQuestionModal);
    const modalSave = new bootstrap.Modal(saveQuestionModal);

    saveButton.addEventListener('click', () => {
        const listItems = document.querySelectorAll('.list-group-item');
        const hasEmptyInputs = false;

        for (const item of listItems) {
            const input = item.querySelector('input.form-control');
            if (!input.value.trim()) {
                hasEmptyInputs = true;
                break;
            }
        }

        if (hasEmptyInputs) {
            modalWarning.show();
        } else {
            modalSave.show();
        }
    });

    continueBtn.addEventListener('click', () => {
        const listItems = document.querySelectorAll('.list-group-item');

        listItems.forEach(item => {
            const input = item.querySelector('input.form-control');
            if (!input.value.trim()) {
                item.remove()
            }
        });
    });

    function hasCorrectAnswers() {
        const listItems = document.querySelectorAll('.list-group-item');

        if (getSelectedType(selectElement) == questionType.handwritten) {
            return document.querySelectorAll('.list-group-item').length > 0;
        }

        for (const item of listItems) {
            const input = item.querySelector('input.form-check-input');
            if (input.checked) {
                return true;
            }
        }

        return false;
    }

    function removeAnswer(item) {
        item.remove();
        checkListLength();
        updateSaveButtonState();
    }
});

