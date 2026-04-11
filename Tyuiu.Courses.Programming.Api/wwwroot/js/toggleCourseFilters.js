const courseStatusToggle = $('input[name="btnradioDeadline"]');
const userRoleToggle = $('input[name="btnradioRole"]');

function updateCourseContainers() {
    toggleButtonFilters(false);

    var isModerator = $('input[name="btnradioRole"]:checked').attr('id') === 'btnradioModerator';
    var courseStatus = $('input[name="btnradioDeadline"]:checked').attr('id').replace(/btnradio/, '');

    $('#searchResult').empty();
    replaceClass($('#loading'), 'd-none', 'd-flex');

    $.ajax({
        url: '/Courses/SearchCourses',
        type: 'GET',
        data: { isModerator: isModerator, courseStatus: courseStatus },
        success: function (data) {
            replaceClass($('#loading'), 'd-flex', 'd-none');
            $('#searchResult').html(data);
            toggleContainers();
        },
        complete: function () {
            toggleButtonFilters(true);
        }
    });
}

function toggleButtonFilters(enabled) {
    courseStatusToggle.prop('disabled', !enabled);
    userRoleToggle.prop('disabled', !enabled);
}

$(document).ready(function () {
    updateCourseContainers();
    courseStatusToggle.change(function () {
        updateCourseContainers();
    });
    userRoleToggle.change(function () {
        updateCourseContainers();
    });
});

function replaceClass(element, removeClass, addClass) {
    element.removeClass(removeClass);
    element.addClass(addClass);
}
