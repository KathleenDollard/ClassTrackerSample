using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KadGen.ClassTracker.WebApi.ViewModels
{
    public class InstructorViewModel
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
