using InstratructureLayer.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandStack.Commands
{
    public class DeleteFoodStoreCommand : Command
    {
        public Guid Id { get; protected set; }
        public override bool IsValid()
        {
            return Id != new Guid();
        }
        public DeleteFoodStoreCommand(Guid id)
        {
            Id = id;
        }
    }
}
