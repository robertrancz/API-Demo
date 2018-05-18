using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnifiApiDemo.Business.Model.Chromatograms;
using UnifiApiDemo.Business.Model.Components;
using UnifiApiDemo.Business.Model.Spectra;

namespace UnifiApiDemo.Business.Model
{
    public class SampleResult
    {
        public SampleResult()
        {
            Components = new List<Component>();
            DynamicProperties = new Dictionary<string, object>();
        }

        #region Properties to be filled when the model is retrieved

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Sample Sample { get; set; }

        #endregion

        #region Properties NOT to be filled when the model is retrieved

        public IDictionary<string, object> DynamicProperties { get; set; }
        public List<Component> Components { get; set; }
        public SampleData SampleData {get; set;}
        //public List<Analysis> Analyses {get; set;}
        //public List<Spectrum> Spectra { get; set; }

        public List<SpectrumInfo> SpectrumInfos { get; set; }
        public List<ChromatogramInfo> ChromatogramInfos { get; set; }

        #endregion
    }
}
