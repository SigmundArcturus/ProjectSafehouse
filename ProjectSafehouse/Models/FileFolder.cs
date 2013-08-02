using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectArsenal.Models
{
    public class FileFolder
    {
        public Guid ID { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<FileFolder> SubFolders { get; set; }
        public List<FileRevision> ContainedFiles { get; set; }
    }
}