using System;
namespace Domain.Models
{
    // Season Model
    public class SeasonType
    {
        public int SeasonId { get; set; }
        public string SeasonName { get; set; }

        public ICollection<Clothing> Clothings { get; set; }
    }



}

