using UnifiApiDemo.Business.Model.Spectra;

namespace UnifiApiDemo.Business.Model
{
    public class AnalyticalTechnique
    {
        /// <summary>
        /// The name of the detection hardware.
        /// </summary>
        public string HardwareName { get; set; }
    }

    public class MSTechnique : AnalyticalTechnique
    {
        /// <summary>
        /// The scanning method, e.g. MS or MSMS.
        /// </summary>
        public string ScanningMethod { get; set; }

        /// <summary>
        /// The mass analyser, e.g. QUADRUPOLE or TOF.
        /// </summary>
        public string MassAnalyser { get; set; }

        /// <summary>
        /// The ionisation mode, one of "+" or "-".
        /// </summary>
        public string IonisationMode { get; set; }

        /// <summary>
        /// The ionisation type, e.g. ESI or ACPI.
        /// </summary>
        public string IonisationType { get; set; }

        /// <summary>
        /// Low mass of the acquired scan range.
        /// </summary>
        public double LowMass { get; set; }

        /// <summary>
        /// High mass of the acquired scan range.
        /// </summary>
        public double HighMass { get; set; }

        /// <summary>
        /// Parameters related to the ADC.
        /// </summary>
        public ADCGroup AdcGroup { get; set; }

        /// <summary>
        /// Parameters related to the TOF analyzer.
        /// </summary>
        public TOFGroup TofGroup { get; set; }

        /// <summary>
        /// Parameters related to the quadrupole analyzer.
        /// </summary>
        public QuadGroup QuadGroup { get; set; }

    }

    public class QuadGroup
    {
        /// <summary>
        /// For scanning data, this is the mass range. For static calibration data, this is the acquisition range around each reference mass.
        /// </summary>
        public double MassSpan { get; set; }
    }

    public class ADCGroup
    {
        /// <summary>
        /// The ADC acquisition mode.
        /// </summary>
        public string AcquisitionMode { get; set; }

        /// <summary>
        /// The frequency of the ADC hardware.
        /// </summary>
        public double AcquisitionFrequency { get; set; }

        /// <summary>
        /// Ion responses.
        /// </summary>
        public IonResponse[] IonResponses { get; set; }
    }

    public class IonResponse
    {
        /// <summary>
        /// The ion type.
        /// </summary>
        public string IonType { get; set; }

        /// <summary>
        /// The ion's charge number.
        /// </summary>
        public int Charge { get; set; }

        /// <summary>
        /// The average ion area.
        /// </summary>
        public double AverageIonArea { get; set; }
    }

    public class TOFGroup
    {
        /// <summary>
        /// The nominal instrument resolution.
        /// </summary>
        public double NominalResolution { get; set; }

        /// <summary>
        /// The MSe energy level.
        /// </summary>
        public EnergyLevel MseLevel { get; set; }

        /// <summary>
        /// The pusher frequency. You can use this to calculate raw drift time values: driftTime[i] = 1000 * i / PusherFrequency.
        /// </summary>
        public double PusherFrequency { get; set; }
    }
}
