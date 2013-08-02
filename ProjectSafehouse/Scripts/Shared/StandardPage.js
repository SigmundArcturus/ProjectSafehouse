var loadingMethods = extend(ProjectArsenal, "ProjectArsenal.Utilities.Loading");

loadingMethods.ShowLoading = function (target) {
    $(target).prepend('<div class="GlobalLoading"></div>');
}

loadingMethods.ClearLoading = function (target) {
    $(target).find('.GlobalLoading').remove();
}