using System;

namespace UnifiApiDemo.Business.Model
{
    public enum DetectorType
    {
        Unknown, MS, UV, FLR, IR, NMR
    }

    public enum ChannelType
    {
        Unknown, Chromatogram, Spectrum, Scanning, ScanningBlocked
    }

    public enum ChannelState
    {
        /// <summary>
        /// Repsresents a channel in an unspecified
        /// state as regards data. This is the default
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Represents a channel complete after
        /// acquisition and holding data.
        /// </summary>
        CompleteWithData = 1,

        /// <summary>
        /// Represents a channel that is complete
        /// but no data was written.
        /// </summary>
        CompleteNoData = 2,

        /// <summary>
        /// Represents channel in an error state
        /// from an acquisition perspective. It may
        /// or may not have data.
        /// </summary>
        Error = 3,

        /// <summary>
        /// Represents a channel whcih was aborted.
        /// May or may not hold data.
        /// </summary>
        Abort = 4
    }

    /// <summary>
    /// Container for raw data or processed data and associated meta data.
    /// </summary>
    public class Channel
    {
        public Channel()
        {
            BlockRecordCount = 1;
        }

        /// <summary>
        /// The channel identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the channel. May be constructed from variopus pieces of meta data.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the detector on which the contained data is based on.
        /// </summary>
        public DetectorType DetectorType { get; set; }

        /// <summary>
        /// Type of the channel and the contained data.
        /// </summary>
        public ChannelType ChannelType { get; set; }

        /// <summary>
        /// State of the channel and the contained data.
        /// </summary>
        public ChannelState ChannelState { get; set; }

        /// <summary>
        /// True if the channel contains diagnostic data as opposed to analytical data.
        /// </summary>
        public bool IsDiagnostic { get; set; }

        /// <summary>
        /// True if the channel data is calibrated.
        /// </summary>
        public bool IsCalibrated { get; set; }

        /// <summary>
        /// True if the channel was derived from another channel.
        /// </summary>
        public bool IsDerived { get; set; }

        /// <summary>
        /// The identifier of the channel that this channel was derived from (if any).
        /// </summary>
        public Guid? ParentChannelId { get; set; }

        /// <summary>
        /// Parameters relating to the analytical technique used for the creation of the channel.
        /// </summary>
        public AnalyticalTechnique AnalyticalTechnique { get; set; }

        /// <summary>
        /// String representation of processing settings.
        /// </summary>
        public string ProcessingSettings { get; set; }

        /// <summary>
        /// The number of records (scans) in a block if the channel type is ScannigBlocked.
        /// </summary>
        public int BlockRecordCount { get; set; }
    }

    /**** Example of a 3D channel stream:

	<stream xsi:type="ChromatogramMSSpectrumStream" location="AA24E673866946AF9DB0990CD3BB4A69" continuous="true" monotonic="true" name="StatsData" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
		<countVector label="ScanData" dataType="int" />
		<rtVector label="Retention Time" unit="min" coordinate="DefaultX" maxInclusive="7.51100015640259" minInclusive="0.0403333343565464" dataType="float" />
		<ticVector label="TIC" unit="Counts" coordinate="DefaultY" dataType="float" />
		<bpIntensityVector label="BPI" unit="Counts" coordinate="AlternateY" dataType="float" />
		<linearDetectorVoltageVector label="Linear Detector Voltage" dataType="short" />
		<LinearSensitivityVector label="Linear Sensitivity" dataType="short" />
		<vector label="Reflectron Voltage" dataType="short" />
		<vector label="Reflectron Detector Volta" dataType="short" />
		<vector label="Reflectron Sensitivity" dataType="short" />
		<laserRateVector label="Laser Repetition Rate" dataType="short" />
		<laserEnergyVector label="Laser Energy Coarse" dataType="short" />
		<laserEnergyFineVector label="Laser Energy Fine" dataType="short" />
		<xCoordinateVector label="Aim X Position" dataType="float" />
		<yCoordinateVector label="Aim Y Position" dataType="float" />
		<vector label="No. Shots Summed" dataType="short" />
		<vector label="No. Shots Performed" dataType="short" />
		<vector label="Segment Number" dataType="short" />
		<coneVoltageVector label="Cone" dataType="short" />
		<collisionEnergyVector label="Collision Energy" dataType="float" />
		<pusherFrequencyVector label="Pusher Frequency" dataType="short" />
		<setMassVector label="Set Mass" dataType="float" />
		<referenceScanVector label="Reference Scan" dataType="byte" />
		<useLockMassCorrectionVector label="Use lock mass correction" dataType="byte" />
		<lockMassCorrectionVector label="Lock mass correction" dataType="float" />
		<useTempCorrectionVector label="Use temp correction" dataType="byte" />
		<temperatureCorrectionVector label="Temperature correction" dataType="float" />
		<temperatureCoefficientVector label="Temperature coefficient" dataType="float" />
		<stream xsi:type="MSSpectrumStream" location="CB701825DB804E2AB077BE98312494E2" continuous="false" monotonic="true" name="ScanData">
			<massVector label="Mass" unit="Da" coordinate="DefaultX" maxInclusive="850.4893" minInclusive="99.62051" dataType="float" />
			<intensityVector label="Intensity" unit="Counts" coordinate="DefaultY" dataType="float" />
			<flagVector label="Flag" dataType="byte" />
		</stream>
	</stream>

	****/
}
