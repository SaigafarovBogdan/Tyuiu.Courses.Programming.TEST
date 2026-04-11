$(document).ready(function () {
    const questions = [
        {
            question: "Сколько будет 2+2?",
            options: ["2", "4", "6", "8"],
            correctAnswer: 1
        },
        {
            question: "Какой цвет у неба?",
            options: ["Синий", "Зеленый", "Красный", "Желтый"],
            correctAnswer: 0
        },
        {
            question: "Сколько дней в неделе?",
            options: ["5", "7", "10", "3"],
            correctAnswer: 1
        },
        {
            question: "Кто написал 'Войну и мир'?",
            options: ["Толстой", "Достоевский", "Пушкин", "Чехов"],
            correctAnswer: 0
        },
        {
            question: "Сколько планет в Солнечной системе?",
            options: ["7", "8", "9", "10"],
            correctAnswer: 1
        },
        {
            question: "Какой самый тяжелый металл?",
            options: ["Свинец", "Золото", "Уран", "Железо"],
            correctAnswer: 1
        },
        {
            question: "Какой океан самый большой?",
            options: ["Атлантический", "Тихий", "Индийский", "Северный"],
            correctAnswer: 1
        }
    ];

    let currentQuestionIndex = 0;
    const prevButton = document.getElementById('prev-button');
    const nextButton = document.getElementById('next-button');
    const prevPage = document.getElementById('prev-page');
    const nextPage = document.getElementById('next-page');
    const paginationInfo = document.getElementById('pagination-info');
    const questionText = document.getElementById('question-text');
    const paginationList = document.getElementById('pagination-list');
    const optionsContainer = document.getElementById('options-container');
    const completeBtn = document.getElementById('completeBtn');
    const resultButtons = document.getElementById('resultButtons');

    function updateQuestion() {
        const currentQuestion = questions[currentQuestionIndex];
        questionText.textContent = currentQuestion.question;
  
        optionsContainer.innerHTML = '';

        currentQuestion.options.forEach((option, index) => {
            const optionElement = document.createElement('div');
            optionElement.classList.add('form-check');

            const radioInput = document.createElement('input');
            radioInput.classList.add('form-check-input');
            radioInput.type = 'radio';
            radioInput.name = 'flexRadioDefault';
            radioInput.id = `flexRadioDefault${index + 1}`;

            const label = document.createElement('label');
            label.classList.add('form-check-label');
            label.htmlFor = `flexRadioDefault${index + 1}`;
            label.textContent = option;

            optionElement.appendChild(radioInput);
            optionElement.appendChild(label);
            optionsContainer.appendChild(optionElement);
        });

        paginationInfo.textContent = `${currentQuestionIndex + 1} / ${questions.length}`;

        if (currentQuestionIndex === 0) {
            prevButton.classList.add('disabled');
            prevPage.classList.add('disabled');
        } else {
            prevButton.classList.remove('disabled');
            prevPage.classList.remove('disabled');
        }

        if (currentQuestionIndex === questions.length - 1) {
            nextButton.classList.add('disabled');
            nextPage.classList.add('disabled');
            completeBtn.classList.remove('d-none');
        } else {
            nextButton.classList.remove('disabled');
            nextPage.classList.remove('disabled');
            completeBtn.classList.add('d-none');
        }
    }

    function updatePagination() {
      
        paginationList.innerHTML = '';

        for (let i = 0; i < questions.length; i++) {
            const pageItem = document.createElement('li');
            pageItem.classList.add('page-item');
            if (i === currentQuestionIndex) {
                pageItem.classList.add('active');
            }

            const pageLink = document.createElement('a');
            pageLink.classList.add('page-link');
            pageLink.href = '#';
            pageLink.textContent = i + 1;
            pageLink.addEventListener('click', () => {
                currentQuestionIndex = i;
                updateQuestion();
                updatePagination();
            });

            pageItem.appendChild(pageLink);
            paginationList.appendChild(pageItem);
        }

        if (currentQuestionIndex === 0) {
            prevButton.classList.add('disabled');
        } else {
            prevButton.classList.remove('disabled');
        }

        if (currentQuestionIndex === questions.length - 1) {
            nextButton.classList.add('disabled');
        } else {
            nextButton.classList.remove('disabled');
        }
    }

    let userAnswers = [];

    function createResultButtons() {
        resultButtons.innerHTML = '';

        questions.forEach((question, index) => {
            const button = document.createElement('button');
            button.type = 'button';
            button.classList.add('btn', 'rounded-4');

            const userAnswer = userAnswers[index];
            if (userAnswer !== undefined && userAnswer === question.correctAnswer) {
                button.classList.add('btn-success');
            } else if (userAnswer !== undefined) {
                button.classList.add('btn-danger');
            } else {
                button.classList.add('btn-primary');
            }

            button.textContent = index + 1;
            button.addEventListener('click', () => {
                currentQuestionIndex = index;
                updateQuestion();
                $('#resultTestModal').modal('hide');
            });

            resultButtons.appendChild(button);
        });
    }

    function checkAnswer() {
        const selectedOption = document.querySelector('input[name="flexRadioDefault"]:checked');
        if (selectedOption) {
            const selectedIndex = Array.from(selectedOption.parentNode.parentNode.children).indexOf(selectedOption.parentNode);
            userAnswers[currentQuestionIndex] = selectedIndex;
        } else {
            userAnswers[currentQuestionIndex] = undefined;
        }

        updateQuestion();
        createResultButtons();
    }

    createResultButtons();

    completeBtn.addEventListener('click', checkAnswer);


    //prevPage.addEventListener('click', () => {
    //    if (currentQuestionIndex > 0) {
    //        currentQuestionIndex--;
    //        updateQuestion();
    //        updatePagination();
    //    }
    //});

    //nextPage.addEventListener('click', () => {
    //    if (currentQuestionIndex < questions.length - 1) {
    //        currentQuestionIndex++;
    //        updateQuestion();
    //        updatePagination();
    //    }
    //});

    prevButton.addEventListener('click', () => {
        if (currentQuestionIndex > 0) {
            currentQuestionIndex--;
            updateQuestion();
            updatePagination();
        }
    });

    nextButton.addEventListener('click', () => {
        if (currentQuestionIndex < questions.length - 1) {
            currentQuestionIndex++;
            updateQuestion();
            updatePagination();
        }
    });

    updateQuestion();
    updatePagination();
});