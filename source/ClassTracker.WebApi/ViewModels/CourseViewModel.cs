using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KadGen.ClassTracker.WebApi.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string CatalogNumber { get; set; }
        public string Name { get; set; }
    }
}
