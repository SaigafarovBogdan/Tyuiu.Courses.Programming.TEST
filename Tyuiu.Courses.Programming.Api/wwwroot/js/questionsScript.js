// Массив с вопросами и вариантами ответов
const questions = [
    {
        question: 'Куда добавляется DLL?',
        options: ['В корневую папку проекта',
     'В папку с решением проекта', 'В отдельную папку', 'Никуда, он не нужен'],
        correctAnswer: 0
  },
    {
        question: 'Какой метод используется для сложения?',
        options: ['Math.Sum', 'Math.Sin', 'Math.Abs', 'Math.Log'],
        correctAnswer: 0
  },
    {
        question: 'Какой тип возвращает вещественное число?',
        options: ['Int', 'Double', 'Bool'],
        correctAnswer: 1
  },
    {
        question: 'Какой тип возвращает целое число?',
        options: ['Int', 'Double', 'Bool'],
        correctAnswer: 0
  }
];

let currentQuestionIndex = 0; // Индекс текущего вопроса
let answers = []; // Массив для сохранения выбранных ответов

// Функция для отображения текущего вопроса и вариантов ответов
function displayQuestion() {
    const questionContainer = document.getElementById('question-container');
    questionContainer.innerHTML = ''; // Очищаем контейнер

    const currentQuestion = questions[currentQuestionIndex];

    // Создаем элементы для вопроса и вариантов ответов
    const questionElement = document.createElement('h3');
    questionElement.textContent = currentQuestion.question;

    const optionsElement = document.createElement('ul');

    currentQuestion.options.forEach((option, index) => {
        const optionElement = document.createElement('li');
        const optionRadioButton = document.createElement('input');
        optionRadioButton.type = 'radio';
        optionRadioButton.name = 'answer';
        optionRadioButton.value = index;
        optionRadioButton.addEventListener('change', handleAnswerSelection);

        // Если для текущего вопроса уже есть выбранный ответ, проверяем, совпадает ли текущий вариант с выбранным ответом
        if (answers[currentQuestionIndex] && answers[currentQuestionIndex].answer === index.toString()) {
            optionRadioButton.checked = true;
        }

        optionElement.appendChild(optionRadioButton);
        optionElement.appendChild(document.createTextNode(option));
        optionsElement.appendChild(optionElement);
    });

    questionContainer.appendChild(questionElement);
    questionContainer.appendChild(optionsElement);

    // Отображаем кнопки перехода на каждый вопрос
    const navigationButtons = document.getElementById('navigation-buttons');
    const completedTest = document.getElementById('completed-test');
    navigationButtons.innerHTML = '';

    const prevBtn = document.createElement('button');
    prevBtn.setAttribute('id', 'previous-button');
    prevBtn.textContent = '<';
    navigationButtons.appendChild(prevBtn);



    for (let i = 0; i < questions.length; i++) {
        const button = document.createElement('button');
        button.textContent = i + 1;
        button.addEventListener('click', goToQuestion);
        navigationButtons.appendChild(button);
    }
    
   

    const nextBtn = document.createElement('button');
    nextBtn.setAttribute('id', 'next-button');
    nextBtn.textContent = '>';
    navigationButtons.appendChild(nextBtn);

    const finishBtn = document.createElement('button');
    finishBtn.setAttribute('id', 'finish-button');
    finishBtn.textContent = 'Завершить';
    finishBtn.hidden = true;
    completedTest.appendChild(finishBtn);
    
    // Меняем стиль кнопки текущего вопроса
    const currentQuestionButton = navigationButtons.children[currentQuestionIndex];
    currentQuestionButton.classList.add('current-question');

    // Проверяем, нужно ли отображать кнопки перехода или кнопку завершения
    const previousButton = document.getElementById('previous-button');
    const nextButton = document.getElementById('next-button');
    const finishButton = document.getElementById('finish-button');

    previousButton.disabled = currentQuestionIndex === 0;
    nextButton.disabled = currentQuestionIndex === questions.length - 1;
    finishButton.hidden = currentQuestionIndex !== questions.length - 1;

    // Назначаем обработчики клика для кнопок
    const previousButton1 = document.getElementById('previous-button');
    previousButton.addEventListener('click', goToPreviousQuestion);

    const nextButton2 = document.getElementById('next-button');
    nextButton.addEventListener('click', goToNextQuestion);

    const finishButton3 = document.getElementById('finish-button');
    finishButton.addEventListener('click', finishTest);
    
    
}


// Обработчик выбора ответа
function handleAnswerSelection(event) {
    const selectedAnswer = event.target.value;
    answers[currentQuestionIndex] = selectedAnswer;
}

// Обработчик клика по кнопке "Предыдущий вопрос"
function goToPreviousQuestion() {
    if (currentQuestionIndex > 0) {
        currentQuestionIndex--;
        displayQuestion();
    }
}

// Обработчик клика по кнопке "Следующий вопрос"
function goToNextQuestion() {
    if (currentQuestionIndex < questions.length - 1) {
        currentQuestionIndex++;
        displayQuestion();
    }
}

// Обработчик клика по кнопке "Завершить"
function finishTest() {
    let score = 0;
    answers.forEach((answer, index) => {
        if (answer != null && parseInt(answer) === questions[index].correctAnswer) {
            score++;
        }
    });

    alert(`Вы выбрали ${score} правильных ответов из ${questions.length}`);
    sessionStorage.setItem("score", score);
    sessionStorage.setItem("maxScore", questions.length);
}

// Обработчик клика по кнопке с номером вопроса
function goToQuestion(event) {
    const button = event.target; 
    const questionIndex = parseInt(button.textContent) - 1;
    currentQuestionIndex = questionIndex;

    displayQuestion();

}



// Инициализация теста
displayQuestion();
