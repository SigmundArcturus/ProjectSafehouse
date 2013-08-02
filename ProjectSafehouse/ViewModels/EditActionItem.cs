using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectArsenal.ViewModels
{
    public class EditActionItem : CreateActionItem
    {
        public List<HistoricEventGroup> ActionItemHistory { get; set; }

        public EditActionItem()
        {
            AvailableTypes = new List<Models.ActionItemType>();
            AvailableStatuses = new List<Models.ActionItemStatus>();
            CurrentActionItem = new Models.ActionItem();
            AvailableUsers = new List<SimpleUserInfo>();
            SelectedUser = new SimpleUserInfo();
            AvailablePriorities = new List<Models.Priority>();
            ActionItemHistory = new List<HistoricEventGroup>();
        }
        public EditActionItem(Models.ActionItem editing, List<Models.ActionItemType> availableTypes, List<Models.ActionItemStatus> availableStatuses, List<SimpleUserInfo> availableUsers, List<Models.Priority> availablePriorities, List<Models.Release> availableReleases, List<Models.ActionItemHistoryEvent> fullHistory)
        {
            AvailableTypes = availableTypes;
            AvailableStatuses = availableStatuses;
            AvailableUsers = availableUsers;
            AvailablePriorities = availablePriorities;
            AvailableReleases = availableReleases;
            CurrentActionItem = editing;
            ActionItemHistory = new List<HistoricEventGroup>();
            var historicGroupings = fullHistory.OrderByDescending(x => x.WhenItChanged).Select(x => x.Grouping).Distinct();
            foreach (var grouping in historicGroupings)
            {
                var firstEvent = fullHistory.FirstOrDefault(x => x.Grouping == grouping);

                ActionItemHistory.Add(new HistoricEventGroup()
                {
                    EventCausedBy = firstEvent.WhoChangedIt,
                    EventDateTime = firstEvent.WhenItChanged,
                    RelatedEvents = fullHistory.Where(x => x.Grouping == grouping).ToList()
                });
            }

            SelectedUser = new SimpleUserInfo();
        }
    }
}