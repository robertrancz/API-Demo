namespace UnifiApiDemo.Business.Model
{
    public class AnalysisStatus
    {
        public AnalysisStatus()
        {
            ManuallyModified = false;
            SampleAltered = false;
            LimitStatus = LimitStatus.NoChecksPerformed;
        }

        public LimitStatus LimitStatus { get; set; }
        public bool ManuallyModified { get; set; }
        public bool SampleAltered { get; set; }
    }
}