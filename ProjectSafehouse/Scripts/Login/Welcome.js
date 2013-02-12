var welcomeMainMenu = [
        {
            type: "DropDown",
            image: "cog_32x32.png",
            text: "User",
            id: "userDropDown",
            submenu:
                [
                    {
                        text: "My Tickets",
                        link: "http://www.google.com"
                    },
                    {
                        text: "My Time",
                        link: "http://www.google.com"
                    }
                ]
        },
        {
            type: "DropDown",
            image: "cog_32x32.png",
            text: "Project",
            id: "projectDropDown",
            submenu:
                [
                    {
                        text: "Releases",
                        link: "http://www.google.com"
                    },
                    {
                        text: "Project Timeline",
                        link: "http://www.google.com"
                    },
                    {
                        text: "Reporting",
                        link: "http://www.google.com"
                    }
                ]
        },
        {
            type: "DropDown",
            image: "cog_32x32.png",
            text: "Company",
            id: "companyDropDown",
            submenu:
                [
                    {
                        text: "All Projects",
                        link: "http://www.google.com"
                    }
                ]
        }
];

$(document).ready(function () {
    var loginMenu = ProjectSafehouse.Utilities.Menu;
    loginMenu.SetCurrentMenu(welcomeMainMenu);
    loginMenu.LoadTopLevel();

    $.ajax({
        url: "../Company/LoadUserCompanies",
        cache: false,
        data: {
            userId: '@Model.ID'
        },
        method: "GET",
        success: function (data) {
            $('#companiesList').html(data);
        }
    });
});