using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.ViewModels
{
    public class EditActionItem : CreateActionItem
    {
        public EditActionItem()
        {
            AvailableTypes = new List<Models.ActionItemType>();
            AvailableStatuses = new List<Models.ActionItemStatus>();
            CurrentActionItem = new Models.ActionItem();
            AvailableUsers = new List<SimpleUserInfo>();
            SelectedUser = new SimpleUserInfo();
            AvailablePriorities = new List<Models.Priority>();
        }
        public EditActionItem(Models.ActionItem editing, List<Models.ActionItemType> availableTypes, List<Models.ActionItemStatus> availableStatuses, List<SimpleUserInfo> availableUsers, List<Models.Priority> availablePriorities, List<Models.Release> availableReleases)
        {
            AvailableTypes = availableTypes;
            AvailableStatuses = availableStatuses;
            AvailableUsers = availableUsers;
            AvailablePriorities = availablePriorities;
            AvailableReleases = availableReleases;
            CurrentActionItem = editing;
            SelectedUser = new SimpleUserInfo();
        }
    }
}