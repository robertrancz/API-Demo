using System;

namespace UnifiApiDemo.Business.Model
{
    public class ChromatogramPeak
    {
        /// <summary>
        /// The start time of the peak.
        /// </summary>
        public double StartTime { get; set; }

        /// <summary>
        /// The retention time of the peak.
        /// </summary>
        public double RetentionTime { get; set; }

        /// <summary>
        /// The stop time of the peak.
        /// </summary>
        public double StopTime { get; set; }

        /// <summary>
        /// The area under the peak.
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// % area relative to the total area of all the peaks in the channel.
        /// </summary>
        public double AreaPercentage { get; set; }

        /// <summary>
        /// The peak height.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// % height relative to the total height of all the peaks in the channel.
        /// </summary>
        public double HeightPercentage { get; set; }

        /// <summary>
        /// The peak width.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Flag to indicate that the peak is created or modified manually.
        /// </summary>
        public bool Manual { get; set; }

        /// <summary>
        /// The id of the associated component of the peak. Will be null if peaks are retrieved from a component.
        /// </summary>
        public Guid? ComponentId { get; set; }
    }
}
