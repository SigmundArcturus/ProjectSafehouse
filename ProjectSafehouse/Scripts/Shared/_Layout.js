$(document).ready(function () {
    $('.NavDropDown').click(function () {
        if ($('#subMenu').height() < 1) {
            $('#subMenu').animate(
                {
                    height: '72px'
                },
                400,
                function () { }
            );
        }
        else {
            $('#subMenu').animate(
                {
                    height: '0px'
                },
                400,
                function () { }
            );
        }
    });
});