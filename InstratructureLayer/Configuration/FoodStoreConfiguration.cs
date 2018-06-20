using InstratructureLayer.Entity;
using InstratructureLayer.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstratructureLayer.Configuration
{
    public class FoodStoreConfiguration : BaseConfiguration<FoodStore, Guid> , IEntityConfiguration
    {
        public FoodStoreConfiguration(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        public override void Configure()
        {
            base.Configure();
            _builder.ToTable("OnlineFoodStore");
        }
    }
}
