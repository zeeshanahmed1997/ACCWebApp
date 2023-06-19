using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    // LadiesClothing Model
    public class LadiesType
    {
        public int SubtypeId { get; set; }
        public int TypeId { get; set; }
        public string SubtypeName { get; set; }

        public ClothingType ClothingType { get; set; }
    }


}

