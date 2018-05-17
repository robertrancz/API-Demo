using System;
using UnifiApiDemo.Business.Model.Chromatograms;

namespace UnifiApiDemo.Business.Model.Spectra
{
    public class SpectrumInfo
    {
        #region Properties to be filled when the model is retrieved

        /// <summary>
        /// The spectrum identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the spectrum. May be constructed from variopus pieces of meta data.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// True if spectra contain peak information only (as opposed to profile data).
        /// </summary>
        public bool IsCentroidData { get; set; }

        /// <summary>
        /// True if multiple spectra are acquired over time.
        /// </summary>
        public bool IsRetentionData { get; set; }

        /// <summary>
        /// True if spectra are separated by drift time.
        /// </summary>
        public bool IsIonMobilityData { get; set; }

        /// <summary>
        /// True if CCS calibration is available.
        /// </summary>
        public bool HasCCSCalibration { get; set; }

        /// <summary>
        /// Type of the detector on which the contained data is based on.
        /// </summary>
        public DetectorType DetectorType { get; set; }

        /// <summary>
        /// Parameters relating to the analytical technique used for the creation of the channel.
        /// </summary>
        public AnalyticalTechnique AnalyticalTechnique { get; set; }

        /// <summary>
        /// Information about the x values of the spectrum.
        /// </summary>
        public DataAxis AxisX { get; set; }

        /// <summary>
        /// Information about the y values of the spectrum.
        /// </summary>
        public DataAxis AxisY { get; set; }

        #endregion

        #region Properties NOT to be filled when the model is retrieved

        /// <summary>
        /// Link to the spectrum data.
        /// </summary>
        //public SpectrumData2D Data { get; set; }

#if WF_LATER
        /// <summary>
        /// Link to derived spectra.
        /// </summary>
        public IEnumerable<SpectrumInfo> DerivedSpectrumInfos { get; set; }

        /// <summary>
        /// Link to the parent spectra, if this is a derived spectrum.
        /// </summary>
        public SpectrumInfo ParentSpectrumInfo { get; set; }

        /// <summary>
        /// Link to derived (extracted) chromatograms.
        /// </summary>
        public IEnumerable<ChromatogramInfo> DerivedChromatogramInfos { get; set; }
#endif

#endregion
    }
}
