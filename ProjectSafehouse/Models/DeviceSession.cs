using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectArsenal.Models
{
    [Serializable]
    public class DeviceSession
    {
        public Guid ID { get; set; }
        public User LoadedUser { get; set; }
        public Device Device { get; set; }
        public Boolean CurrentlyActive { get; set; }

        public DeviceSession()
        {
            ID = Guid.Empty;
            LoadedUser = null;
            Device = null;
            CurrentlyActive = false;
        }
    }
}