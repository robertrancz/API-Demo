using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnifiApiDemo.Models
{
    public class AnalysisViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LimitStatus { get; set; }
        public bool ManuallyModified { get; set; }
        public bool SampleAltered { get; set; }
    }
}
