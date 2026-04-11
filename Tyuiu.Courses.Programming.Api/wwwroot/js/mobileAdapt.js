document.addEventListener('DOMContentLoaded', function () {

    checkScreenSize();

    window.addEventListener('resize', checkScreenSize);

    function checkScreenSize() {
        if (window.innerWidth <= 430) {
            document.querySelector('.pageName').classList.add('d-none');
        }
        else {
            document.querySelector('.pageName').classList.remove('d-none');
        }
    }

});