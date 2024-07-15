using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.App_Data.Entities
{
    public class City: LookupEntityBase
    {
        public int StateId { get; set; }
        public string CityName { get; set; } = null!;
        public int Population { get; set; } 
        public decimal Area { get; set; } 
    }
}