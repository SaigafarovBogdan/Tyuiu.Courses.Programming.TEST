document.addEventListener('DOMContentLoaded', function () {
    var listFile = document.getElementById('listFile');
    var accordionButton = document.getElementById('btnFile');

    function updateAccordionButton() {
        if (listFile.children.length === 0) {
            accordionButton.classList.add('parent-class');
            accordionButton.innerHTML += '<span class="fs-5" >Файлы отсутствуют</span>';
        } else {
            accordionButton.classList.remove('parent-class');
            accordionButton.innerHTML = ' <i class="bi bi-file-earmark text-primary fs-2"></i>';
        }
    }

    var observer = new MutationObserver(function (mutations) {
        mutations.forEach(function (mutation) {
            if (mutation.type === 'childList') {
                updateAccordionButton();
            }
        });
    });

    observer.observe(listFile, { childList: true });
    updateAccordionButton();
});

function getFileIcon(fileName) {
    switch (fileName) {
        case 'xlsx':
            return "bi bi-filetype-xlsx h1 fw-bold rounded text-success";
        case 'txt':
            return "bi bi-filetype-txt h1 fw-bold rounded text-warning";
        case 'docx':
            return "bi bi-file-earmark-word h1 fw-bold rounded text-primary";
        case 'pdf':
            return 'bi bi-filetype-pdf h1 fw-bold rounded text-danger';
        default:
            return "bi bi-file-earmark-arrow-up h1 fw-bold rounded text-emphasis";
    }
}

function handleFileUpload(files) {
    fileList = document.getElementById('listFile');
    let themeId = document.getElementById('themeId').value;

    for (var i = 0; i < files.length; i++) {
        var listItems = fileList.querySelectorAll('li');
        var isExisting = Array.from(listItems).some(item => item.textContent.includes(files[i].name));
        if (isExisting) {
            continue;
        }

        addFileToList(files[i], themeId, fileList);
    }
}

function addFileToList(file, themeId, fileList) {
    var li = document.createElement('li');
    li.className = 'list-group-item list-group-item-action d-flex gap-5 justify-content-between align-items-center border-0 border-bottom border-top pt-1 pb-1';
    const divLeft = document.createElement('div');
    divLeft.className = "left d-flex align-items-center gap-2";
    const spanFileType = document.createElement('i');
    spanFileType.className = getFileIcon(file.name.split(".").pop());
    const h3FileName = document.createElement('h3');
    h3FileName.className = "fs-5 fw-normal m-0 text-wrap";
    h3FileName.textContent = file.name;

    divLeft.appendChild(spanFileType);
    divLeft.appendChild(h3FileName);

    var deleteButton = document.createElement('button');
    deleteButton.type = 'button';
    deleteButton.className = 'btn btn-outline-danger shadow-none';
    deleteButton.innerHTML = '<i class="bi bi-trash"></i>';

    li.appendChild(divLeft);
    li.appendChild(deleteButton);

    var formData = new FormData();
    formData.append('file', file);
    formData.append('themeId', parseInt(themeId));

    $.ajax({
        url: '/ThemeFiles/UploadFile',
        type: 'POST',
        data: formData,
        headers: {
            'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
        },
        processData: false,
        contentType: false,
        success: function (response) {
            var fileGuid = response.guid;
            deleteButton.addEventListener('click', function () {
                var fileItem = this.parentElement;
                var fileName = fileItem.querySelector('h3').textContent;
                deleteFile(fileGuid, themeId, fileName);
            });
            fileList.appendChild(li);
        },
        error: handleResponse
    });
}

document.addEventListener('DOMContentLoaded', function () {
    var dropArea = document.getElementById('createModal');
    var uploadLabel = document.querySelector('.uploadLabel');
    var conditionState = document.getElementById('condition');
    var conditionTextState = document.getElementById('conditionText');

    if (dropArea) {
        document.addEventListener('dragover', function (e) {
            e.preventDefault();
            conditionState.className = 'bi bi-plus-circle';
            conditionTextState.textContent = "Наведите и отпустите файлы";
        });
        document.addEventListener('dragleave', function (e) {
            if (e.clientX <= 0 || e.clientY <= 0 || e.clientX >= window.innerWidth || e.clientY >= window.innerHeight) {
                conditionState.className = 'bi bi-cloud-arrow-up';
                conditionTextState.textContent = "Нажмите или перетащите файлы";
            }
        });
    }

    if (uploadLabel) {
        uploadLabel.addEventListener('drop', function (e) {
            e.preventDefault();
            if (conditionState && conditionTextState) {
                conditionState.className = 'bi bi-cloud-arrow-up';
                conditionTextState.textContent = "Нажмите или перетащите файлы";
            }
            var files = e.dataTransfer.files;
            handleFileUpload(files);
            uploadInput.value = "";
        });
    }

    var uploadInput = document.getElementById('upload');
    if (uploadInput) {
        uploadInput.addEventListener('change', function (e) {
            var files = e.target.files;
            handleFileUpload(files);
            uploadInput.value = "";
        });
    }

    document.querySelectorAll('.delete-btn').forEach(button => {
        if (button) {
            button.addEventListener('click', function () {
                const fileName = this.dataset.fileName;
                const themeId = this.dataset.themeId;
                const fileGuid = this.dataset.fileGuid
                deleteFile(fileGuid, themeId, fileName);
            });
        }
    });
});

function deleteFile(fileGuid, themeId, fileName) {
    $.ajax({
        url: '/ThemeFiles/DeleteFile',
        type: 'POST',
        data: { fileGuid: fileGuid, themeId: themeId },
        headers: {
            'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
        },
        success: function (response) {
            $('li:contains("' + fileName + '")').remove();
        },
        error: handleResponse
    });
}