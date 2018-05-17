using System;

namespace UnifiApiDemo.Business.Model.Chromatograms
{
    public class ChromatogramInfo
    {
        #region Properties to be filled when the model is retrieved

        /// <summary>
        /// The chromatogram identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the chromatogram. May be constructed from variopus pieces of meta data.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the detector on which the contained data is based on.
        /// </summary>
        public DetectorType DetectorType { get; set; }

        /// <summary>
        /// Parameters relating to the analytical technique used for the creation of the channel.
        /// </summary>
        public AnalyticalTechnique AnalyticalTechnique { get; set; }

        /// <summary>
        /// Information about the x values of the chromatogram.
        /// </summary>
        public DataAxis AxisX { get; set; }

        /// <summary>
        /// Information about the y values of the chromatogram.
        /// </summary>
        public DataAxis AxisY { get; set; }

        #endregion

        #region Properties NOT to be filled when the model is retrieved

        /// <summary>
        /// Link to the chromatogram data.
        /// </summary>
        public ChromatogramData Data { get; set; }

#if WF_LATER
        /// <summary>
        /// Link to the parent spectra, if this is an extracted chromatogram.
        /// </summary>
        public SpectrumInfo ParentSpectrumInfo { get; set; }

        /// <summary>
        /// Link to a parent chromatogram, if this is a derived chromatogram.
        /// </summary>
        public ChromatogramInfo ParentChromatogramInfo { get; set; }

        /// <summary>
        /// Link to derived chromatograms.
        /// </summary>
        public IEnumerable<ChromatogramInfo> DerivedChromatogramInfos { get; set; }
#endif

#endregion
    }
}
