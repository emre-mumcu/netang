using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.App_Data.Entities
{
    public class State: LookupEntityBase
    {
        public string StateName { get; set; } = null!;
    }
}