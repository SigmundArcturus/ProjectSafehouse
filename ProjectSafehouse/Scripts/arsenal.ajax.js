var ajaxMethod = extend(ProjectArsenal, "ProjectArsenal.Utilities.AJAX");
var loadingMethods = ProjectArsenal.Utilities.Loading;

ajaxMethod.LoadPartialView = function (params) {
    var params = params || {};

    if (params.resultTarget) {
        loadingMethods.ShowLoading(params.resultTarget);
    }

    $.ajax({
        url: params.url,
        cache: false,
        data: params.data,
        method: "GET",
        success: function (data) {
            $(params.resultTarget).html(data);
            loadingMethods.ClearLoading(params.resultTarget);
        }
    }); 
};