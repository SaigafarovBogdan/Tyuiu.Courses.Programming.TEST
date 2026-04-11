const tasks = [
    {
        option: 'Вариант 1',
        taskName: 'Task 0',
        task: 'Написать программу, которая вычисляет выражение 36/2/9+1-6/2*3 и печатает результат на экране.',
        imageSrc: '',
        sub: 'Название проектов (консольного приложения, библиотеки классов, тестового модуля) оформить по шаблону в соответствии с вариантом. Интерфейс консольного приложения оформить по шаблону.'
    },
    {
        option: 'Вариант 1',
        taskName: 'Task 1',
        task: 'Написать программу, которая запрашивает у пользователя исходные данные, вычисляет результат по формуле x/3/y+6*a и печатает его на экране.',
        imageSrc: '',
        sub: 'Название проектов (консольного приложения, библиотеки классов, тестового модуля) оформить по шаблону в соответствии с вариантом. Интерфейс консольного приложения оформить по шаблону.'
    },
    {
        option: 'Вариант 1',
        taskName: 'Task 2',
        task: 'Написать программу, которая запрашивает у пользователя исходные данные, выполняет указанные расчёты и печатает результат на экране.Формулировка задания: Известно расстояние в километрах. Вычислить расстояние в милях. При условии, что 1 миля = 1,609 км. Ответ округлите до 3 знаков после запятой. Что пользователь вводит? Расстояние в километрах (целое число) Что программа печатает на экране? Расстояние в милях(вещественное число)',
        imageSrc: '',
        sub: 'Название проектов (консольного приложения, библиотеки классов, тестового модуля) оформить по шаблону в соответствии с вариантом. Интерфейс консольного приложения оформить по шаблону.'
    },
    {
        option: 'Вариант 1',
        taskName: 'Task 3',
        task: 'Написать программу, которая запрашивает у пользователя исходные данные, выполняет указанные расчёты и печатает результат на экране. Расчеты: Объявите необходимые переменные и напишите программу вычисления объема цилиндра, предполагающий ввод исходных данных. Ответ округлите до 3 знаков после запятой.',
        imageSrc: '',
        sub: 'Название проектов (консольного приложения, библиотеки классов, тестового модуля) оформить по шаблону в соответствии с вариантом. Интерфейс консольного приложения оформить по шаблону.'
    },
    {
        option: 'Вариант 2',
        taskName: 'Task 4',
        task: 'Написать программу, которая запрашивает у пользователя исходные данные, вычисляет результат по формуле и печатает его на экране. Ответ округлите до 3 знаков после запятой. Формула: \( \frac{1}{ \sqrt{x+2y} } \)',
        imageSrc: '',
        sub: 'Название проектов (консольного приложения, библиотеки классов, тестового модуля) оформить по шаблону в соответствии с вариантом. Интерфейс консольного приложения оформить по шаблону.'
    },
    {
        option: 'Вариант 1',
        taskName: 'Task 5',
        task: 'Написать программу, которая решает следующую задачу: Найти расстояние между двумя точками с заданными координатами (x, y). Ответ привести к целому с помощью класса Convert.',
        imageSrc: '',
        sub: 'Название проектов (консольного приложения, библиотеки классов, тестового модуля) оформить по шаблону в соответствии с вариантом. Интерфейс консольного приложения оформить по шаблону.'
    },
    {
        option: 'Вариант 1',
        taskName: 'Task 6',
        task: 'Напишите программу, которая выводит код введенного пользователем символа. Программа должна завершать работу в результате ввода, например, точки. Рекомендуемый вид экрана во время выполнения программы приведен ниже. Введите символ и нажмите <Enter>. Для завершения введите точку.  -> 1 Символ: 1 Код: 49 -> .',
        imageSrc: '',
        sub: 'Название проектов (консольного приложения, библиотеки классов, тестового модуля) оформить по шаблону в соответствии с вариантом. Интерфейс консольного приложения оформить по шаблону.'
    },
    {
        option: 'Вариант 1',
        taskName: 'Task 7',
        task: 'Написать программу, которая вычисляет математическое выражение по исходным значениям данных, вводимых пользователем. Ответ округлите до 3 знаков после запятой.',
        imageSrc: '',
        sub: 'Название проектов (консольного приложения, библиотеки классов, тестового модуля) оформить по шаблону в соответствии с вариантом. Интерфейс консольного приложения оформить по шаблону.'
    }
];

let logic = false;

// Скрываем контейнер с классом "form" при загрузке страницы
document.getElementById("form").style.display = "none";

// Функция для отображения текущего раздела массива
function displayTask() {
    const task = tasks[currentTask];

    // Получаем родительский элемент, в который будем добавлять разделы массива
    const sprintContainer = document.querySelector(".sprint");
    sprintContainer.innerHTML = ""; // Очищаем содержимое контейнера

    const taskContainer = document.createElement("div");
    taskContainer.className = "task-container";

    const taskForm = document.createElement("div");
    taskForm.id = "form";

    const taskNameDiv = document.createElement("div");
    taskNameDiv.id = "taskName";
    taskNameDiv.innerText = task.taskName;

    const optionDiv = document.createElement("div");
    optionDiv.id = "option";
    optionDiv.innerText = task.option;

    const taskDiv = document.createElement("div");
    taskDiv.id = "task";
    taskDiv.innerText = task.task;

    const subDiv = document.createElement("div");
    subDiv.id = "sub";
    subDiv.innerText = task.sub;

    const imageDiv = document.createElement("div");
    imageDiv.className = "image";
    imageDiv.style.backgroundImage = `url('${task.imageSrc}')`;

    taskContainer.appendChild(taskNameDiv);
    taskContainer.appendChild(optionDiv);
    taskContainer.appendChild(taskDiv);
    taskContainer.appendChild(subDiv);
    taskContainer.appendChild(imageDiv);

    sprintContainer.appendChild(taskContainer);
}

let currentTask = 0; // Текущий раздел массива

// При клике на каждую кнопку раздела массива
for (let i = 0; i < tasks.length; i++) {
    const taskButton = document.getElementById(`task${i}`);
    taskButton.addEventListener("click", function () {
        currentTask = i; // Обновляем текущий раздел
        document.querySelector(".sprint").style.display = "flex";
        document.getElementById("form").style.display = "none";
        logic = false;
        displayTask(); // Отображаем выбранный раздел
        hideCompleteButton(); // Проверяем условие для отображения кнопки "Complete"
    });
}

// При клике на кнопку "githubLink"
document.getElementById("githubLink").addEventListener("click", function () {

    document.querySelector(".sprint").style.display = "none";
    document.getElementById("form").style.display = "flex";
    currentTask=7;
    logic = true;

    hideCompleteButton(); // Проверяем условие для отображения кнопки "Complete"
});

// При клике на кнопку "Вперед"
document.getElementById("nextButton").addEventListener("click", function () {
    if (currentTask === 7) {
        document.querySelector(".sprint").style.display = "none";
        document.getElementById("form").style.display = "flex";
        logic  = true;
        hideCompleteButton(); // Проверяем условие для отображения кнопки "Complete"
    }
    else if (currentTask < tasks.length - 1) {
        logic = false;
        currentTask++; // Увеличиваем текущий раздел на 1
        displayTask(); // Отображаем следующий раздел
    }

});

// При клике на кнопку "Назад"
document.getElementById("prevButton").addEventListener("click", function () {

    if (currentTask > 0) {
        if(logic === true) {} else{currentTask--;}
        // Уменьшаем текущий раздел на 1
        document.querySelector(".sprint").style.display = "flex";
        document.getElementById("form").style.display = "none";
        logic = false;
        displayTask(); // Отображаем предыдущий раздел
        hideCompleteButton();
        // Проверяем условие для отображения кнопки "Complete"
    }
});

function hideCompleteButton() {
    if (logic === true) {
        document.getElementById("Complete").style.display = "flex";
    } else {
        document.getElementById("Complete").style.display = "none";
    }
}
displayTask();