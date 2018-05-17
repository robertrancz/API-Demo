using System.Collections.Generic;
using UnifiApiDemo.Business.Model.Components;

namespace UnifiApiDemo.Business.Model
{
    public class SampleData : ItemBase
    {
        #region Properties to be filled when the model is retrieved

        public Sample Sample { get; set; }

        #endregion

        #region Properties NOT to be filled when the model is retrieved

        public SampleSet SampleSet { get; set;}
        public List<Component> Components { get; set; }
        public List<SampleResult> SampleResults { get; set; }

        #endregion
    }
}
