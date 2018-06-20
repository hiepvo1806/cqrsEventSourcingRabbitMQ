using System;

namespace ApplicationLayer.ViewModel
{
    public class FoodStoreVM
    {
        public Guid Id { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}