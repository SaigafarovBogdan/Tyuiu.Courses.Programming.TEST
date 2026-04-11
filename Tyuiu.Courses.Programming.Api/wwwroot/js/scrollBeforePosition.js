$(document).ready(function () {

    $(function () {
        const pathName = document.location.pathname;

        window.onbeforeunload = function () {
            sessionStorage.setItem(
                "scrollPosition_" + pathName,
                $(document).scrollTop().toString()
            );
        }

        if (sessionStorage["scrollPosition_" + pathName]) {
            const scrollPosition = parseInt(
                sessionStorage.getItem("scrollPosition_" + pathName)
            );

            $(document).scrollTop(scrollPosition);
        }
    });
});