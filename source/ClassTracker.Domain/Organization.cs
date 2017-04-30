using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KadGen.ClassTracker.Domain
{
    public class Organization
    {
        public Organization(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get;  }
        public string Name { get;  }
        // more stuff including location and contact, possibly department
    }
}
