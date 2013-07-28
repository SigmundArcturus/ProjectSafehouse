var companyCreator = extend(ProjectSafehouse, "ProjectSafehouse.AJAX.Company");

companyCreator.CreateCompany = function (newCompany) {
    console.log(newCompany);
    //$.ajax({
    //    url: '../Company/CreateNewCompany',
    //    cache: false,
    //    data: {
    //        toCreate: newCompany
    //    },
    //    method: 'POST',
    //    success: function (data) {
    //        $('#companiesList').html(data);
    //    }
    //});
}

$(document).ready(function () {

    var ajaxMethods = ProjectSafehouse.Utilities.AJAX;
    //ajaxMethods.LoadPartialView({
    //    url: "../Company/LoadUserCompanies",
    //    data: {
    //        userId: $('#userId').attr('value')
    //    },
    //    resultTarget: $('#companiesList')
    //});

    //$.ajax({
    //    url: "../Company/LoadUserCompanies",
    //    cache: false,
    //    data: {
    //        userId: $('#userId').attr('value')
    //    },
    //    method: "GET",
    //    success: function (data) {
    //        $('#companiesList').html(data);
    //    }
    //});

    $('body').on('click', '#createCompanyButton', function () {
        var targetExpand = $(this).attr('target');
        var companyId = $(this).attr('companyid');

        $('.CompanyDetailsContainer').stop(true, true);
        $('.CompanyDetailsContainer').animate(
            {
                height: '0px'
            }, 300, function(){
                $('.CompanyDetailsContainer').css('display', 'none');
                $('#' + targetExpand).css('display', 'block');
                $('#' + targetExpand).animate(
                    {
                        height: '220px'
                    }, 400, function () {
                        if (companyId != null) {

                            ajaxMethods.LoadPartialView({
                                url: "../Project/LoadCompanyProjects",
                                data: {
                                    companyId: companyId
                                },
                                resultTarget: $('#' + targetExpand + ' ul.ProjectList')
                            });

                        }
                    });
            }
        );        
    });

    $('body').on('click', 'img.CloseDetailsContainer', function(){
        var target = $(this).parent();
        target.stop(true, true);
        target.animate(
            {
                height: '0px'
            }, 300, function () {
                target.css('display', 'none');
            }
        );
    });
    
    
});