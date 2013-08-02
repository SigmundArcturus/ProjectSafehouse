using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProjectArsenal.CustomExceptions
{
    [Serializable]
    public class DuplicateUserInsertException: System.Exception
    {
        public string UserEmail { get; set; }

        public DuplicateUserInsertException()
        {

        }

        public DuplicateUserInsertException(string message)
            : base(message)
        {

        }

        public DuplicateUserInsertException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        protected DuplicateUserInsertException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
                this.UserEmail = info.GetString("UserEmail");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
                info.AddValue("UserEmail", this.UserEmail);
        }

    }

    [Serializable]
    public class InvalidUserDataInsertException : System.Exception
    {
        public string UserEmail { get; set; }

        public InvalidUserDataInsertException()
        {

        }

        public InvalidUserDataInsertException(string message)
            : base(message)
        {

        }

        public InvalidUserDataInsertException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        protected InvalidUserDataInsertException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
                this.UserEmail = info.GetString("UserEmail");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
                info.AddValue("UserEmail", this.UserEmail);
        }

    }
}