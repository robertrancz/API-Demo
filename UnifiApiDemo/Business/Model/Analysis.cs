using System.Collections.Generic;

namespace UnifiApiDemo.Business.Model
{
    public class Analysis : ItemBase
    {
        public Analysis()
        {
            Status = new AnalysisStatus();
        }

        #region Properties to be filled when the model is retrieved

        public AnalysisStatus Status { get; set; }

        #endregion

        #region Properties NOT to be filled when the model is retrieved

        public IEnumerable<SampleSet> SampleSets { get; set; }
        public IEnumerable<SampleResult> SampleResults { get; set; }


        #endregion
    }
}
