let isDangerClassApplied = false;

function countdownTimer(time) {
    const timerElement = document.getElementById("timer");
    const timerContainer = document.querySelector('.float-end');

    const hours = Math.floor(time / 3600).toString().padStart(2, '0');
    const minutes = Math.floor((time % 3600) / 60).toString().padStart(2, '0');
    const seconds = Math.floor(time % 60).toString().padStart(2, '0');

    timerElement.innerHTML = `${hours}:${minutes}:${seconds}`;

    if (time > 0) {
        setTimeout(() => countdownTimer(time - 1), 1000);
    } else {
        sendAnswers();
    }

    if (time <= 150 && !isDangerClassApplied) {
        timerContainer.classList.add('border-danger-subtle', 'bg-danger-subtle', 'text-danger-emphasis');
        timerContainer.classList.remove('border-secondary-subtle', 'bg-secondary-subtle');
        isDangerClassApplied = true;
    } else if (time > 150 && isDangerClassApplied) {
        timerContainer.classList.remove('border-danger-subtle', 'bg-danger-subtle', 'text-danger-emphasis');
        timerContainer.classList.add('border-secondary-subtle', 'bg-secondary-subtle');
        isDangerClassApplied = false;
    }
}

document.addEventListener('DOMContentLoaded', () => {
    let currentQuestionIndex = 0;
    const questions = document.querySelectorAll('.question');
    const totalQuestions = window.totalQuestions;
    window.answers = [];

    questions.forEach(question => {
        const questionId = parseInt(question.querySelector('.answer-input').getAttribute('data-question-id'), 10);
        window.answers.push({ questionId, answer: [] });
    });

    const showQuestion = (index) => {
        questions.forEach((question, i) => {
            question.classList.toggle('d-none', i !== index);
        });
        updatePagination(index);
    };

    const updatePagination = (index) => {
        document.querySelectorAll('.pagination .page-item').forEach((item, i) => {
            item.classList.remove('active', 'answered');
            if (i === index) {
                item.classList.add('active');
            } else {
                const hasAnswer = window.answers[i] && window.answers[i].answer.length > 0;
                if (hasAnswer) {
                    item.classList.add('answered');
                }
            }
        });
    };

    const saveCurrentAnswer = () => {
        const currentQuestion = questions[currentQuestionIndex];
        const answerInputs = currentQuestion.querySelectorAll('.answer-input');
        const questionId = parseInt(answerInputs[0].getAttribute('data-question-id'), 10);
        const answerValue = [];

        answerInputs.forEach(answerInput => {
            if (answerInput.type === 'checkbox' || answerInput.type === 'radio') {
                if (answerInput.checked) {
                    answerValue.push(answerInput.value);
                }
            } else if (answerInput.value.trim() !== "") {
                answerValue.push(answerInput.value);
            }
        });

        const existingAnswerIndex = window.answers.findIndex(a => a.questionId === questionId);
        if (existingAnswerIndex !== -1) {
            window.answers[existingAnswerIndex].answer = answerValue;
        } else {
            window.answers.push({ questionId, answer: answerValue });
        }

        updatePagination(currentQuestionIndex);
    };

    document.querySelectorAll('.pagination .page-item').forEach(item => {
        item.addEventListener('click', event => {
            event.preventDefault();
            saveCurrentAnswer();
            currentQuestionIndex = parseInt(event.currentTarget.querySelector('a').getAttribute('data-index'), 10);
            showQuestion(currentQuestionIndex);
        });
    });

    document.querySelectorAll('.answer-btn').forEach(button => {
        button.addEventListener('click', event => {
            event.preventDefault();
            saveCurrentAnswer();

            if (currentQuestionIndex === totalQuestions - 1) {
                $('#completeTestModal').modal('show');
            } else {
                currentQuestionIndex = (currentQuestionIndex + 1) % totalQuestions;
                showQuestion(currentQuestionIndex);
            }
        });
    });

    document.getElementById('confirmCompleteTest').addEventListener('click', event => {
        event.preventDefault();
        sendAnswers();
        $('#completeTestModal').modal('hide');
    });

    document.querySelector('#completeBtn button').addEventListener('click', saveCurrentAnswer);

    showQuestion(currentQuestionIndex);
});

function sendAnswers() {
    $.ajax({
        url: '/Tests/Check',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            answers: window.answers,
            CourseThemeId: window.courseThemeId,
        }),
        headers: {
            'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
        },
        success: response => {
            if (response && response.testResultId) {
                window.location.href = `${window.resultsUrl}?testResultId=${response.testResultId}`;
            } else {
                console.error('Не удалось получить ID результата теста');
            }
        },
        error: handleResponse,
    });
}