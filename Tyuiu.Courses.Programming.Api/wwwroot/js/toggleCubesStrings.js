function saveFilterState(storageKey, filterType, filterValue) {
    const currentState = JSON.parse(localStorage.getItem(storageKey)) || {};
    currentState[filterType] = filterValue;
    localStorage.setItem(storageKey, JSON.stringify(currentState));
}

function getSavedFilterState(storageKey) {
    const savedState = localStorage.getItem(storageKey);
    return savedState ? JSON.parse(savedState) : null;
}

function applySavedFilterState(storageKey) {
    const filters = getSavedFilterState(storageKey);
    if (!filters) {
        return false;
    }

    let applied = false;

    Object.entries(filters).forEach(([filterType, filterValue]) => {
        if (filterValue) {
            $(`input[data-filter-type="${filterType}"][data-filter-value="${filterValue}"]`).prop('checked', true);
            applied = true;
        }
    });

    return applied;
}

function toggleContainers() {
    var selectedRadio = $('input[name="btnradio"]:checked').attr('id');

    var cubesContainer = $('.cubes');
    var stringsContainer = $('.strings');

    var searchResult = $('#searchResult');

    if (selectedRadio === 'btnradioStrings') {
        cubesContainer.addClass('d-none');
        stringsContainer.removeClass('d-none');
        searchResult.addClass('p-0');
    } else {
        cubesContainer.removeClass('d-none');
        stringsContainer.addClass('d-none');
        searchResult.removeClass('p-0');
    }
}

$(document).ready(function () {
    const storageKey = 'courseFilters';
    const hasSavedState = applySavedFilterState(storageKey);

    if (hasSavedState) {
        toggleContainers();
        adaptSearchResult();
    }

    $('input[data-filter-type]').change(function () {
        const filterType = $(this).data('filter-type');
        const filterValue = $(this).data('filter-value');

        saveFilterState(storageKey, filterType, filterValue);

        if (filterType === 'view') {
            toggleContainers();
        }

        if (filterType === 'deadline' || filterType === 'role') {
            adaptSearchResult();
        }
    });

    function adaptSearchResult() {
        var $searchResult = $('#searchResult');

        adaptationElement();

        $searchResult.on('DOMNodeInserted DOMSubtreeModified', function () {
            adaptationElement();
        });
    }

    function adaptationElement() {

        const $toggleBtnGroup = $('.toggleStateBtnGroup'),
            $toggleBtn = $('.toggleStateBtn'),
            $toggleRoleBtnGroup = $('.toggleRoleBtnGroup'),
            $toggleRoleBtn = $('.toggleRoleBtn'),
            $toggleViewModeBtnGroup = $('.toggleViewModeBtnGroup'),
            $toggleViewModeBtn = $('.toggleViewModeBtn'),
            $themeName = $('.themeName'),
            $cubes = $('.cubesContainer');

        const windowWidth = $(window).width();

        switch (true) {
            case windowWidth >= 768:
                windowWidthMore768($toggleBtn, $toggleRoleBtn, $toggleViewModeBtn, $toggleBtnGroup, $toggleRoleBtnGroup, $themeName, $cubes);
                break;
            case windowWidth > 430 && windowWidth < 768:
                windowWidthBetween430And768($toggleBtn, $toggleRoleBtn, $toggleViewModeBtn, $toggleBtnGroup, $toggleViewModeBtnGroup, $themeName, $cubes);
                break;
            case windowWidth > 415 && windowWidth <= 430:
                windowWidthBetween415And430($toggleBtn, $toggleRoleBtn, $toggleViewModeBtn, $toggleBtnGroup, $toggleRoleBtnGroup, $toggleViewModeBtnGroup, $cubes);
                break;
            case windowWidth > 375 && windowWidth <= 415:
                windowWidthBetween375And415($toggleBtn, $toggleRoleBtn, $toggleViewModeBtn, $toggleRoleBtnGroup, $toggleViewModeBtnGroup, $toggleBtnGroup, $cubes);
                break;
            case windowWidth > 320 && windowWidth <= 375:
                windowWidthBetween320And375($toggleBtn, $toggleRoleBtn, $toggleViewModeBtn, /*$toggleRoleBtnGroup,*/ $toggleBtnGroup, $toggleViewModeBtnGroup, $cubes);
                break;
            default:
                windowWidthLess320($toggleBtn, $toggleRoleBtn, $toggleViewModeBtn, $toggleBtnGroup, $toggleViewModeBtnGroup, $toggleRoleBtnGroup, $cubes);
        }

        function windowWidthMore768($toggleBtn, $toggleRoleBtn, $toggleViewModeBtn, $toggleBtnGroup, $toggleRoleBtnGroup, $themeName, $cubes) {
            $toggleBtn.removeClass('rounded');
            $toggleRoleBtn.removeClass('rounded');
            $toggleViewModeBtn.removeClass('rounded');

            $toggleBtnGroup.removeClass('flex-fill');
            $toggleRoleBtnGroup.removeClass('flex-fill');

            $themeName.removeClass('col-10');

            $cubes.addClass('row row-cols-2 g-4').removeClass('d-flex flex-column flex-fill gap-4');
        }

        function windowWidthBetween430And768($toggleBtn, $toggleRoleBtn, $toggleViewModeBtn, $toggleBtnGroup, $toggleViewModeBtnGroup, $themeName, $cubes) {
            $toggleBtn.addClass('rounded');
            $toggleRoleBtn.addClass('rounded');
            $toggleViewModeBtn.addClass('rounded');

            $toggleBtnGroup.addClass('flex-fill');
            $toggleViewModeBtnGroup.removeClass('flex-row d-none').addClass('flex-column');

            $themeName.addClass('col-10');

            $cubes.addClass('row row-cols-2 g-4').removeClass('d-flex flex-column flex-fill gap-4');
        }

        function windowWidthBetween415And430($toggleBtn, $toggleRoleBtn, $toggleViewModeBtn, $toggleBtnGroup, $toggleRoleBtnGroup, $toggleViewModeBtnGroup, $cubes) {
            $toggleBtn.addClass('rounded');
            $toggleRoleBtn.addClass('rounded');
            $toggleViewModeBtn.addClass('rounded');

            $toggleBtnGroup.addClass('flex-fill');
            $toggleRoleBtnGroup.removeClass('flex-fill');

            $toggleViewModeBtnGroup.addClass('d-none');

            $cubes.removeClass('row row-cols-2 g-4').addClass('d-flex flex-column flex-fill gap-4');
        }

        function windowWidthBetween375And415($toggleBtn, $toggleRoleBtn, $toggleViewModeBtn, $toggleRoleBtnGroup, $toggleViewModeBtnGroup, $toggleBtnGroup, $cubes) {
            $toggleBtn.addClass('rounded');
            $toggleRoleBtn.addClass('rounded');
            $toggleViewModeBtn.addClass('rounded');

            $toggleRoleBtnGroup.addClass('flex-fill');
            /*$toggleViewModeBtnGroup.removeClass('flex-column').addClass('flex-row');*/
            $toggleBtnGroup.addClass('flex-fill');

            $toggleViewModeBtnGroup.addClass('d-none');

            $cubes.removeClass('row row-cols-2 g-4').addClass('d-flex flex-column flex-fill gap-4');
        }

        function windowWidthBetween320And375($toggleBtn, $toggleRoleBtn, $toggleViewModeBtn, /*$toggleRoleBtnGroup,*/ $toggleBtnGroup, $toggleViewModeBtnGroup, $cubes) {
            $toggleBtn.addClass('rounded');
            $toggleRoleBtn.addClass('rounded');
            $toggleViewModeBtn.addClass('rounded');

            /*$toggleRoleBtnGroup.addClass('flex-fill');*/
            $toggleBtnGroup.addClass('flex-fill');
            /*$toggleViewModeBtnGroup.removeClass('flex-column').addClass('flex-row');*/

            $toggleViewModeBtnGroup.addClass('d-none');

            $cubes.removeClass('row row-cols-2 g-4').addClass('d-flex flex-column flex-fill gap-4');
        }

        function windowWidthLess320($toggleBtn, $toggleRoleBtn, $toggleViewModeBtn, $toggleBtnGroup, $toggleViewModeBtnGroup, $toggleRoleBtnGroup, $cubes) {
            $toggleBtn.addClass('rounded');
            $toggleRoleBtn.addClass('rounded');
            $toggleViewModeBtn.addClass('rounded');

            $toggleBtnGroup.addClass('flex-fill');
            /*$toggleViewModeBtnGroup.removeClass('flex-row').addClass('flex-column');*/
            $toggleRoleBtnGroup.addClass('flex-fill');

            $toggleViewModeBtnGroup.addClass('d-none');

            $cubes.removeClass('row row-cols-2 g-4').addClass('d-flex flex-column flex-fill gap-4');
        }

    }

    $(window).resize(function () {
        adaptationElement();
    });

    adaptationElement();
});
function openModal(event) {
    event.stopPropagation(); // Останавливает всплытие события
    $('#ratingModal').modal('show'); // Открывает модальное окно
}