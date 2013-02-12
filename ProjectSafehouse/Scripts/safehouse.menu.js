var menuMethods = extend(ProjectSafehouse, "ProjectSafehouse.Utilities.Menu");

$(document).ready(function () {
    $('#mainMenu').on('click', '.NavDropDown', function () {
        menuMethods.ToggleSubMenu($(this).attr('id'));
    });

    $('body').on('mouseleave', '#subMenu', function () {
        $('#subMenu').animate(
            {
                height: '0px'
            },
            400,
            function () { $('#subMenu').html(''); }
        );
    });
});

menuMethods.ExampleMenu  =
    [
        {
            type: "DropDown",
            image: "cog_32x32.png",
            text: "Navigation",
            id: "genericDropDown",
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

menuMethods.SetCurrentMenu = function (Menu) {
    menuMethods.CurrentMenu = Menu || {};
}   

menuMethods.LoadTopLevel = function () {
    //$('#menuOptions').html('');
    var Menu = menuMethods.CurrentMenu;
    for (var i = 0; i < Menu.length; i++) {
        var currentOption = Menu[i];
        if(currentOption.type == "DropDown")
        {
            $('#mainMenu').append('<div class="NavOption NavDropDown" id="' + currentOption.id + '">' +
                '<img src="..\\Content\\themes\\base\\images\\' + currentOption.image + '"/>' +
                '<h2>' + currentOption.text + '</h2>' +
                '</div>');
        }
    }
}

menuMethods.ToggleSubMenu = function (targetId) {
    var Menu = menuMethods.CurrentMenu;
    $('#subMenu').html('');

    if ($('#subMenu').height() < 1) {
        
        $('#subMenu').animate(
            {
                height: '72px'
            },
            400,
            function () { }
        );
    }

    var selectedItem = null;
    for (var i = 0; i < Menu.length; i++) {
        if (Menu[i].id == targetId) {
            selectedItem = Menu[i];
        }
    }

    if (selectedItem && selectedItem.submenu) {
        for(var j = 0; j < selectedItem.submenu.length; j++)
        {
            var subItem = selectedItem.submenu[j];
            $('#subMenu').append('<div class="NavOption NavLink">' +
                '<a href="' + subItem.link + '">' +
                    subItem.text +
                '</a>' +
            '</div> ');
        }
    }
}

