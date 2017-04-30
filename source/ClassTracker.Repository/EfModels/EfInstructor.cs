using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KadGen.ClassTracker.Repository
{
    public class EfInstructor
    {
        public int Id {get; set;}
        public EfOrganization Organization {get; set;}
        public string GivenName {get; set;}
        public string SurName {get; set;}

        public virtual List<EfSection> Sections { get; set; }
    }
}
