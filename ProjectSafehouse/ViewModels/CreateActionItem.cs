using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectArsenal.ViewModels
{
    [Serializable]
    public class SimpleUserInfo
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    [Serializable]
    public class CreateActionItem
    {
        public List<Models.ActionItemType> AvailableTypes { get; set; }
        public List<Models.ActionItemStatus> AvailableStatuses { get; set; }
        public Models.ActionItem CurrentActionItem { get; set; }
        public List<SimpleUserInfo> AvailableUsers { get; set; }
        public SimpleUserInfo SelectedUser { get; set; }
        public string UnvalidatedEstimate { get; set; }
        public List<Models.Priority> AvailablePriorities { get; set; }
        public List<Models.Release> AvailableReleases { get; set; }
        public Models.Release SelectedRelease { get; set; }

        public CreateActionItem()
        {
            AvailableTypes = new List<Models.ActionItemType>();
            AvailableStatuses = new List<Models.ActionItemStatus>();
            CurrentActionItem = new Models.ActionItem();
            AvailableUsers = new List<SimpleUserInfo>();
            SelectedUser = new SimpleUserInfo();
            AvailablePriorities = new List<Models.Priority>();
        }

        public CreateActionItem(List<Models.ActionItemType> availableTypes, List<Models.ActionItemStatus> availableStatuses, List<SimpleUserInfo> availableUsers, List<Models.Priority> availablePriorities, List<Models.Release> availableReleases)
        {
            AvailableTypes = availableTypes;
            AvailableStatuses = availableStatuses;
            CurrentActionItem = new Models.ActionItem();
            AvailableUsers = availableUsers;
            AvailablePriorities = availablePriorities;
            AvailableReleases = availableReleases;
            SelectedUser = new SimpleUserInfo();
        }
    }
}