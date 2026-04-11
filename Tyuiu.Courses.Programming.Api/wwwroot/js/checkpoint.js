//Автоматически увеличивает размер textArea при вводе текста 

document.addEventListener('DOMContentLoaded', function () {
    const textareas = document.querySelectorAll('.auto-resize');

    function autoResize() {
        this.style.height = 'auto';
        this.style.height = this.scrollHeight + 'px';
    }

    textareas.forEach(textarea => {
        textarea.addEventListener('input', autoResize);
        autoResize.call(textarea);

        // Получаем ближайшее модальное окно для текстового поля
        const modal = textarea.closest('.modal');

        // При открытии модального окна вызываем функцию autoResize для текстового поля
        if (modal) {
            modal.addEventListener('shown.bs.modal', function () {
                autoResize.call(textarea);
            });
        }

        // Получаем ближайший аккордеон для текстового поля
        const accordion = textarea.closest('.accordion');

        // При открытии аккордеона вызываем функцию autoResize для текстового поля
        if (accordion) {
            accordion.addEventListener('shown.bs.collapse', function (event) {
                // Проверяем, что открытый элемент аккордеона содержит текстовое поле
                if (event.target.contains(textarea)) {
                    autoResize.call(textarea);
                }
            });
        }
    });
});

$(function () {
    // Общие настройки для daterangepicker
    function initializeDaterangepicker(selector, startDate = moment()) {
        $(selector).daterangepicker({
            drops: 'up',
            singleDatePicker: true,
            showDropdowns: true,
            startDate: startDate,
            minDate: moment(),
            maxDate: moment('31-12-' + (moment().year() + 1), 'DD.MM.YYYY HH:mm'),
            timePicker: true,
            timePicker24Hour: true,
            autoUpdateInput: true,
            locale: {
                firstDay: 1,
                format: 'DD.MM.YYYY HH:mm',
                applyLabel: 'Принять',
                cancelLabel: 'Отмена',
                invalidDateLabel: 'Выберите дату',
                daysOfWeek: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
                monthNames: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь']
            }
        });
    }

    // Инициализация daterangepicker для создания чекпоинта
    initializeDaterangepicker('input[name="createDaterange"]');

    // Обработчики событий для блокировки полей при открытии календаря
    function setupDaterangepickerEvents(selector) {
        $(selector).on('show.daterangepicker', function () {
            $(this).closest('.modal').find('.form-control').not(this).prop('disabled', true);
        });

        $(selector).on('hide.daterangepicker', function () {
            $(this).closest('.modal').find('.form-control').not(this).prop('disabled', false);
        });
    }

    // Настройка обработчиков событий для создания чекпоинта
    setupDaterangepickerEvents('input[name="createDaterange"]');

    // При открытии модального окна редактирования
    $(document).on('show.bs.modal', '#editCheckpointModal', function (event) {
        var button = $(event.relatedTarget);
        var dateValue = button.data('checkpoint-date');
        var editDaterangeSelector = 'input[name="editDaterange"]';

        // Получаем текущую дату из поля ввода, если она уже установлена
        var inputDate = $(editDaterangeSelector).val();
        var startDate = inputDate ? moment(inputDate, 'DD.MM.YYYY HH:mm') : moment();

        // Инициализируем daterangepicker с датой из поля ввода
        initializeDaterangepicker(editDaterangeSelector, startDate);

        // Устанавливаем начальную дату и обновляем input
        if (inputDate) {
            $(editDaterangeSelector).data('daterangepicker').setStartDate(startDate);
            $(editDaterangeSelector).val(startDate.format('DD.MM.YYYY HH:mm'));
        }

        // Настройка обработчиков событий для редактирования чекпоинта
        setupDaterangepickerEvents(editDaterangeSelector);
    });
});