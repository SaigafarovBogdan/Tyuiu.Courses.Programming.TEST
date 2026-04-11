document.addEventListener('DOMContentLoaded', function () {
    var cancelButtons = document.querySelectorAll('#changeCancel');
    cancelButtons.forEach(function (button) {
        button.addEventListener('click', function (event) {
            var myModalEl = document.getElementById('changeTaskModal');
            var modal = bootstrap.Modal.getInstance(myModalEl)
            modal.hide();
        });
    });
});

const courseThemeId = new URLSearchParams(window.location.search).get('courseThemeId');
const checkTaskButton = document.getElementById('checkTaskButton');
const modalContainer = document.getElementById('modalContainer');
const checkModal = new bootstrap.Modal(document.getElementById('checkTaskModal'), {
    backdrop: 'static',
    keyboard: false
});
const taskAnswerId = document.getElementById('taskAnswerId').value;

let remainingAttempts = null;
let currentAnalysisRequest = null;

checkTaskButton.addEventListener('click', () => {
    checkModal.show();

    var gitRepoUrl = $('#gitRepoUrl').val();
    $('#checkTaskModal').one('shown.bs.modal', function () {
        $.ajax({
            type: 'POST',
            url: '/TaskAnswers/CheckTask',
            data: { gitRepoUrl: gitRepoUrl, taskAnswerId: taskAnswerId },
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                checkModal.hide();

                updateStatusBar(response.status, response.score)
                modalContainer.innerHTML = response.modal;

                const successModalElement = modalContainer.querySelector('#successModal');
                const errorsModalElement = modalContainer.querySelector('#errorsModal');

                if (successModalElement) {
                    const successModal = new bootstrap.Modal(successModalElement);

                    successModalElement.addEventListener('hidden.bs.modal', function () {
                        if (successModalElement.parentNode) {
                            successModalElement.parentNode.removeChild(successModalElement);
                        }
                    });

                    successModal.show();

                    // Авто скрытие
                    setTimeout(() => {
                        successModal.hide();
                    }, 5000);

                } else if (errorsModalElement) {
                    const errorsModal = new bootstrap.Modal(errorsModalElement);

                    errorsModalElement.addEventListener('hidden.bs.modal', function () {
                        if (currentAnalysisRequest) {
                            currentAnalysisRequest.abort();
                            currentAnalysisRequest = null;
                        }

                        if (errorsModalElement.parentNode) {
                            errorsModalElement.parentNode.removeChild(errorsModalElement);
                        }
                    });

                    document.getElementById('aiHelpLink')?.addEventListener('click', function (event) {
                        handleAIButtonClick(event);
                        updateRequestsCount();
                    });
                    document.getElementById('tryAgainButton').addEventListener('click', function () {
                        errorsModal.hide();
                    });

                    errorsModal.show();
                }
                
            },
            error: function (xhr, status, error) {
                handleResponse(xhr);
                console.error(
                    'Error:', error || 'Произошла ошибка',
                    'Status:', xhr.status
                );
                setTimeout(() => {
                    checkModal.hide();
                }, 500);
            }
        });
    });
});

function updateRequestsCount() {
    if (!courseThemeId) return;

    const remainingCount = document.getElementById('remainingCount');
    if (remainingCount) remainingCount.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>';

    $.ajax({
        type: 'GET',
        url: '/AI/GetNumberOfRemainingRequests',
        data: { courseThemeId: courseThemeId },
        success: function (response)
        {
            remainingAttempts = parseInt(response, 10);
            remainingCount.textContent = remainingAttempts;
        },
        error: handleResponse
    });
}

function handleAIButtonClick() {
    const aiResponseContainer = document.getElementById('aiResponseContainer');
    const aiShowButton = document.getElementById('aiHelpLink');

    aiShowButton.style.display = 'none';
    aiResponseContainer.style.display = 'block';

    const collapseElement = document.getElementById('collapseAI');
    if (collapseElement) {
        new bootstrap.Collapse(collapseElement, {toggle: true});
    }

    const emptyAIHistory = document.getElementById('emptyAIHistory');
    const aiDialogContent = document.getElementById('aiDialogContent');

    const askAgainButton = document.getElementById('askAgainButton');
    askAgainButton.disabled = true;

    const aiLoading = document.getElementById('aiLoading');
    aiLoading.style.display = 'block';

    $.ajax({
        type: 'GET',
        url: '/AI/GetAIRequestHistory',
        data: { taskAnswerId: taskAnswerId },
        complete: function () {
            aiLoading.style.display = 'none';
        },
        success: function (aiRequests) {
            const isEmpty = !aiRequests || aiRequests.trim() === '';

            if (emptyAIHistory) {
                emptyAIHistory.style.display = isEmpty ? 'block' : 'none';
            }

            if(!isEmpty && aiDialogContent) {
                aiDialogContent.innerHTML = aiRequests;
            }

            askAgainButton.addEventListener('click', askAI);
            askAgainButton.disabled = false;
        },
        error: handleResponse
    });
}

function askAI() {
    if (currentAnalysisRequest) {
        currentAnalysisRequest.abort();
    }

    const askAgainButton = document.getElementById('askAgainButton');

    updateRequestsCount();
    if (remainingAttempts <= 0) {
        if (askAgainButton) askAgainButton.disabled = remainingAttempts <= 0;
        return;
    };

    
    askAgainButton.innerHTML = '<i class="bi bi-arrow-clockwise me-1"></i>Спросить еще раз';
    askAgainButton.disabled = true;

    document.getElementById('emptyAIHistory').style.display = 'none';

    const aiDialogContent = document.getElementById('aiDialogContent');
    if (aiDialogContent.innerHTML === '') {
        aiDialogContent.innerHTML += document.getElementById('firstUserMessage').outerHTML;
    } else {
        aiDialogContent.innerHTML += document.getElementById('retryUserMessage').outerHTML;
    }

    const aiLoading = document.getElementById('aiLoading');
    aiLoading.style.display = 'block';

    currentAnalysisRequest = $.ajax({
        type: 'POST',
        url: '/AI/AnalyzeTask',
        data: { taskAnswerId: taskAnswerId },
        headers: {
            'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
        },
        success: function (response) {
            aiDialogContent.innerHTML += response;
            updateRequestsCount()   
        },
        complete: function () {
            if (this === currentAnalysisRequest) {
                currentAnalysisRequest = null;
            }
            askAgainButton.disabled = false;
            aiLoading.style.display = 'none';
        }
    });
}
//