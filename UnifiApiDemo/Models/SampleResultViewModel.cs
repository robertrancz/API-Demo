using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnifiApiDemo.Models
{
    public class SampleResultViewModel
    {
        public Guid Id { get; internal set; }

        public string Name { get; internal set; }

        public string Description { get; internal set; }

        public Business.Model.Sample Sample { get; internal set; }

        public bool MzmlFileExistsOnServer { get; internal set; }

        public List<ComponentViewModel> Components { get; set; }

        public int IdentifiedSpectraCount { get; set; }

        public List<AnalysisViewModel> Analyses { get; set; }
    }
}
