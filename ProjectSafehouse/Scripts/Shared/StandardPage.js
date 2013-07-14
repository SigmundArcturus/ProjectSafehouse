var loadingMethods = extend(ProjectSafehouse, "ProjectSafehouse.Utilities.Loading");

loadingMethods.ShowLoading = function (target) {
    $(target).prepend('<div class="GlobalLoading"></div>');
}

loadingMethods.ClearLoading = function (target) {
    $(target).find('.GlobalLoading').remove();
}