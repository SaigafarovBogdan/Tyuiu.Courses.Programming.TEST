function handleResponse(response) {
	if (isErrorResponse(response)) {
		handleErrorResponse(response);
	} else {
		handleSuccessResponse(response);
	}
}

function isErrorResponse(response) {
	return response.status >= 400 && response.status < 600;
}

function handleErrorResponse(response) {
	const isServerError = response.status >= 500;
	const errTitle = isServerError ? "Возникла ошибка на сервере" : "Возникла ошибка";
	const defaultErrorMessage = "Что-то пошло не так, попробуйте еще раз";
	const errText = getMessage(response, defaultErrorMessage);

	showAlert('error', errTitle, errText);
}

function handleSuccessResponse(response) {
	const defaultSuccessMessage = "Операция выполнена успешно";
	const successMessage = getMessage(response, defaultSuccessMessage);

	showAlert('success', 'Успешно', successMessage);
}

function getMessage(response, defaultMessage) {
	const responseMessage = response.responseJSON?.message || response.responseText;

	return responseMessage && !containsHTML(responseMessage) ?
		responseMessage : defaultMessage;
}

function containsHTML(text) {
	const htmlRegex = /<[a-z][\s\S]*>/i;
	return htmlRegex.test(text);
}