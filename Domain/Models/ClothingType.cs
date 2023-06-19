using System;
namespace Domain.Models
{
    public class ClothingType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }

        public ICollection<LadiesType> LadiesTypes { get; set; }
        public ICollection<GentsType> GentsTypes { get; set; }
    }


}

