using InstratructureLayer.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandStack.Commands
{
    public class UpdateFoodStoreCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Link { get; protected set; }
        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Link);
        }
        public UpdateFoodStoreCommand(string name, string link, Guid id)
        {
            Name = name;
            Link = link;
            Id = id;
        }
    }
}
