using System;
using System.ComponentModel.DataAnnotations;

namespace InstratructureLayer.Events
{
    public abstract class Command : BaseMessage
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
