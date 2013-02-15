var ajaxMethod = extend(ProjectSafehouse, "ProjectSafehouse.Utilities.AJAX");

ajaxMethod.LoadPartialView = function (params) {

    var params = params || {};

    $.ajax({
        url: params.url,
        cache: false,
        data: params.data,
        method: "GET",
        success: function (data) {
            $(params.resultTarget).html(data);
        }
    });
};