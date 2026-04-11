document.addEventListener('DOMContentLoaded', function () { 
    function togglePasswordVisibility(event) {
        const $button = $(event.currentTarget);
        const $passwordIcon = $button.find('.passwordIcon');
        const $passwordInput = $('#' + $button.data('passwordInput'));

        $passwordInput.attr('type', $passwordInput.attr('type') === 'password' ? 'text' : 'password');
        $passwordIcon.toggleClass('bi-eye-slash');
        $passwordIcon.toggleClass('bi-eye');
    }

    $('.showHidePassword').click(function (e) {
        e.preventDefault();
        togglePasswordVisibility(e);
    });

});

