namespace UnifiApiDemo.Business.Model.Chromatograms
{
    public class DataAxis
    {
        /// <summary>
        /// A user friendly string representation of the axis.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The unit of measurement for the data represented by the axis.
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Lowest value that may be found in the data. The actual minimum value may be higher.
        /// </summary>
        public float LowerBound { get; set; }

        /// <summary>
        /// Highest value that may be found in the data. The actual maximum value may be lower.
        /// </summary>
        public float UpperBound { get; set; }
    }
}
