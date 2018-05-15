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
    }
}
