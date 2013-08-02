var loginMenu = [
        {
            type: "DropDown",
            image: "cog_32x32.png",
            text: "Information",
            id: "informationDropDown",
            submenu:
                [
                    {
                        text: "Why Choose Us?",
                        link: "http://www.google.com"
                    },
                    {
                        text: "How to Sign Up!",
                        link: "http://www.google.com"
                    }
                ]
        }
];

$(document).ready(function () {

    var loginMenu = ProjectArsenal.Utilities.Menu;
    //loginMenu.SetCurrentMenu(loginMenu.ExampleMenu);
    //loginMenu.LoadTopLevel(loginMenu.ExampleMenu);

    $('div.CleanBox').on('focus', 'input', function () {
        var parent = $(this).parent();
        parent.addClass('SelectedWrapper');
    }).on('blur', 'input', function () {
        var parent = $(this).parent();
        parent.removeClass('SelectedWrapper');
    });

    $('#signUpSwitch').click(function () {
        $('#loginBox').animate(
            {
                height: 0,
                opacity: 0
            }, 400, function () {
                $('#loginBox').css("display", "none");
                $('#signupBox').css("display", "block").animate(
                    {
                        opacity: 1,
                        height: 360
                    }, 400
                 );
            }
        );
    });

    $('#logInSwitch').click(function () {
        $('#signupBox').animate(
           {
               height: 0,
               opacity: 0
           }, 400, function () {
               $('#signupBox').css("display", "none");
               $('#loginBox').css("display", "block").animate(
                   {
                       opacity: 1,
                       height: 260
                   }, 400
                );
           }
       );
    });

});