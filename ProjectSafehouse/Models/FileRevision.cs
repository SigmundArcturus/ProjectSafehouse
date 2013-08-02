using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectArsenal.Models
{
    public enum FileType {doc, img, pdf, txt}

    public class FileRevision
    {
        public Guid ID { get; set; }
        public Double MBSize { get; set; }
        public FileType Type { get; set; }
        public string LocationOnHD { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User UploadedBy { get; set; }
        public DateTime UploadedOn { get; set; }
        public int RevisionNumber { get; set; }
        public List<FileRevision> PreviousRevisions { get; set; }
    }
}