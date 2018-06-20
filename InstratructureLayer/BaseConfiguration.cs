using InstratructureLayer.Entity;
using InstratructureLayer.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstratructureLayer
{
    public abstract class BaseConfiguration<T,U> : IEntityConfiguration where T: BaseEntity<U>
    {
        protected readonly ModelBuilder _modelBuilder;
        protected EntityTypeBuilder<T> _builder => _modelBuilder.Entity<T>();
        protected BaseConfiguration(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }
        public virtual void Configure()
        {
            _builder.Property(e => e.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
