var extendedTable = false;

$(document).ready(function () {
    var $filterBtn = $('.filterBtn');
    var $filterContainer = $('.filterContainer');
    var $mainDiv = $('.mainContainer'); 
    var $tableContainer = $('.col-lg-9');
    var $row = $('.row');
    const pathName = document.location.pathname;

    $filterBtn.on('click', function () {
     
        $filterBtn.toggleClass('btn-secondary btn-primary');
        $mainDiv.toggleClass(' container container-fluid');
        $tableContainer.toggleClass('col-lg-9 col-lg-12');
        $row.toggleClass('ps-3 pe-3');

        if ($filterContainer.attr('hidden')) {
            $filterContainer.removeAttr('hidden');
            extendedTable = false;
            showShortenedTable();
        } else {
            $filterContainer.attr('hidden', 'true');
            extendedTable = true;
            showExtendedTable();
        }
    });

    const isFromTable = sessionStorage.getItem('isFromTable') === 'true';
    sessionStorage.removeItem('isFromTable');

    if (isFromTable) {
        
        const isFilterHidden = sessionStorage.getItem(`filterState_${pathName}`) === 'true';
        if (isFilterHidden) {
            $filterBtn.trigger('click');
        }
    } else {
        $filterContainer.removeAttr('hidden');
        $filterBtn.removeClass('btn-secondary').addClass('btn-primary');
        $mainDiv.removeClass('container').addClass('container - fluid');
        $tableContainer.removeClass('col-lg-9').addClass('col - lg - 12');
        $row.removeClass('ps-3').addClass('pe - 3');

        extendedTable = false;
        showShortenedTable();
    }

    window.addEventListener('beforeunload', () => {
        if (!sessionStorage.getItem('isFromTable')) {
            sessionStorage.removeItem(`filterState_${pathName}`);
        }
    });
});

function showExtendedTable() {
    $('.gitRepoUrl').removeAttr('hidden');
    $('.option').removeAttr('hidden');
    $('.replays').removeAttr('hidden');
    $('.tries').removeAttr('hidden');
}

function showShortenedTable() {
    $('.gitRepoUrl').attr('hidden', 'true');
    $('.option').attr('hidden', 'true');
    $('.replays').attr('hidden', 'true');
    $('.tries').attr('hidden', 'true');
}