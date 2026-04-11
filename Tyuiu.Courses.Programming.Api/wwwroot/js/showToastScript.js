const maps = {
    success: {
        icon: 'bi-check-circle text-success',
        color: 'bg-success',
        bg: 'bg-success-subtle'
    },
    error: {
        icon: 'bi-x-circle text-danger',
        color: 'bg-danger',
        bg: 'bg-danger-subtle'
    },
    info: {
        icon: 'bi-info-circle text-primary',
        color: 'bg-primary',
        bg: 'bg-primary-subtle'
    }
};

function createAlert(type, title, message) {
    const alertElement = document.createElement('div');
    alertElement.className = `alert alert-light alert-dismissible fade show d-flex flex-column p-0`;
    alertElement.setAttribute('role', 'alert');

    alertElement.innerHTML = `
    <div class="d-flex flex-row p-3 align-items-center" style="min-width: 100%; overflow-wrap: break-word;">
      <i class="pe-2 fs-2 bi ${maps[type].icon}"></i>
      <div class="pe-3 flex-grow-1" style="min-width: 0;">
            <strong class="alert-heading fw-2">${title}</strong>
            ${message ? `<div style="word-wrap: break-word;">${message}</div>` : ''}
      </div>
      <button type="button" class="btn-close shadow-none" data-bs-dismiss="alert" aria-label="Закрыть"></button>
    </div>
    <div class="progress ${maps[type].bg}" style="width: 100%; height: 4px">
      <div class="progress-bar ${maps[type].color} rounded-bottom" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%;"></div>
    </div>
            `;

    const closeButton = alertElement.querySelector('.btn-close');
    closeButton.addEventListener('click', () => {
        alertElement.classList.add('removing');
        setTimeout(() => {
            alertElement.remove();
        }, 500); // ждать окончания анимации
    });

    const progressBar = alertElement.querySelector('.progress-bar');
    let timeLeft = 5000; // время до скрытия уведомления
    let intervalId = setInterval(() => {
        timeLeft -= 100;
        const width = (timeLeft / 5000) * 100;
        progressBar.style.width = `${width}%`;
        if (timeLeft <= 0) {
            clearInterval(intervalId);
            alertElement.classList.add('removing');
            setTimeout(() => {
                alertElement.remove();
            }, 500); // ждать окончания анимации
        }
    }, 100);
    return alertElement;
}

function showAlert(type, title, message) {
    let alertContainer = document.getElementById('alertContainer');

    if (!alertContainer) {
        alertContainer = document.createElement('div');
        alertContainer.id = 'alertContainer';
        alertContainer.style.position = 'fixed';
        alertContainer.style.zIndex = '9999';
        alertContainer.style.width = '340px';
        alertContainer.style.display = 'flex';
        alertContainer.style.flexDirection = 'column-reverse';
        document.body.appendChild(alertContainer);
    }

    const alertElement = createAlert(type, title, message);
    alertContainer.appendChild(alertElement);
}