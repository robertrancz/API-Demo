using System;

namespace UnifiApiDemo.Business.Model.Chromatograms
{
    public class ChromatogramData
    {
        public Guid Id { get; set; }

        public float[] RetentionTimes { get; set; }

        public float[] Intensities { get; set; }

        public ChromatogramPeak[] Peaks { get; set; }
    }
}
