using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.ViewModels
{
    public enum GroupingType { Company, Project };

    [Serializable]
    public class AddUserTo
    {
        public List<Models.User> CurrentUsers { get; set; }
        public AddingUser ToAdd { get; set; }
        public GroupingType DestinationType { get; set; }
        public Guid DestinationID { get; set; }
    }

    [Serializable]
    public class AddingUser
    {
        public string EmailAddress { get; set; }
        public bool Administrator { get; set; }
    }
}