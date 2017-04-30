using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KadGen.ClassTracker.WebApi.ViewModels
{
    public class SectionViewModel
    {
        public int Id { get; set; }
        public int InstructorId { get; set; }
        public int TermId { get; set; }
    }
}
