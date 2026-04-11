document.querySelectorAll('.download-btn').forEach(button => {
    button.addEventListener('click', function () {
        const fileName = this.dataset.fileName;
        const themeId = this.dataset.themeId;
        const fileGuid = this.dataset.fileGuid
        downloadThemeFile(fileGuid, themeId, fileName);
    });
});

function downloadThemeFile(fileGuid, themeId, fileName) {
    $.ajax({
        type: 'GET',
        url: '/ThemeFiles/DownloadFile',
        data: { fileGuid: fileGuid, themeId: themeId },
        headers: {
            'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
        },
        xhrFields: {
            responseType: 'blob'
        },
        success: function (data) {
            downloadFile(fileName, data);
        },
        error: handleResponse
    });
}

function downloadFile(fileName, data) {
    let blob;
    if (data instanceof Blob) {
        blob = data;
    } else {
        blob = new Blob([data], { type: 'application/octet-stream' });
    }
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    window.URL.revokeObjectURL(url);
}
