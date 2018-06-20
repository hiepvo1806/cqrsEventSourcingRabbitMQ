using InstratructureLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstratructureLayer.DomainModel
{
    public class FoodStoreDomain : IBaseDomain<Guid>
    {
        public string Link { get; private set; }
        public string Name { get; private set; }

        public bool IsDeleted { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public DateTime UpdatedDate { get; private set; }

        public Guid Id {
            get;
            private set;
        }

        private FoodStoreDomain()
        {
        }

        public static FoodStoreDomain Create(string Name,string Link)
        {
            var result = new FoodStoreDomain()
            {
                Name = Name,
                Link = Link,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now

            };
            return result;
        }

        public static FoodStoreDomain Create(string Name, Guid Id)
        {
            var result = new FoodStoreDomain()
            {
                Name = Name,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Id = Id

            };
            return result;
        }

        public static FoodStoreDomain Update(FoodStoreDomain source, string name, string link)
        {
            source.UpdatedDate = DateTime.Now;
            source.Name = name;
            source.Link = link;
            return source;
        }
        public static FoodStoreDomain Delete(FoodStoreDomain source)
        {
            source.IsDeleted = true;
            return source;
        }

    }
}
