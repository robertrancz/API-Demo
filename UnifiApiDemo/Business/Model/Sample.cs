using System;
using System.Collections.Generic;

namespace UnifiApiDemo.Business.Model
{
    public class Sample
    {
        public Sample()
        {
            ReplicateNumber = 1;
            SampleType = SampleType.Unknown;
            DynamicProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Dictionary to be used for properties based on custom fields
        /// </summary>
        public IDictionary<string, object> DynamicProperties { get; set; }

        /// <summary>
        /// Description of the sample.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The original sample id.
        /// </summary>
        public string OriginalSampleId { get; set; }

        /// <summary>
        /// The sample id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The Gender of the sample. For example; Male, Female, Both or Unknown.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// The name of the sample.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The assay conditions.
        /// </summary>
        public string AssayConditions { get; set; }

        /// <summary>
        /// The order of the standard sample types within the list.
        /// </summary>
        public string BracketGroup { get; set; }

        /// <summary>
        /// Dose given to the subject
        /// </summary>
        public double Dose { get; set; }

        /// <summary>
        /// The dosing route.
        /// </summary>
        public string DosingRoute { get; set; }

        /// <summary>
        /// The day.
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// E Cord id.
        /// </summary>
        public string ECordId { get; set; }

        /// <summary>
        /// The experimental concentration.
        /// </summary>
        public string ExperimentalConcentration { get; set; }

        /// <summary>
        /// The group id.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// The injection id.
        /// </summary>
        public string InjectionId { get; set; }

        /// <summary>
        /// Specify microsomes, hepatocytes, etc.
        /// </summary>
        public string Matrix { get; set; }

        /// <summary>
        /// A delay time caused by the time taken for solvent to elute or reach the detector.
        /// </summary>
        public double SolventDelay { get; set; }

        /// <summary>
        /// The species of organism.
        /// </summary>
        public string Species { get; set; }

        /// <summary>
        /// The study id.
        /// </summary>
        public string StudyId { get; set; }

        /// <summary>
        /// The subject id.
        /// </summary>
        public string SubjectId { get; set; }

        /// <summary>
        /// The molecular formula assigned at the sample level.
        /// </summary>
        public string MolForm { get; set; }

        /// <summary>
        /// A description of the sample preparation.
        /// </summary>
        public string Preparation { get; set; }

        /// <summary>
        /// The type of the sample.
        /// </summary>
        public SampleType SampleType { get; set; }

        /// <summary>
        /// The batch id.
        /// </summary>
        public string BatchId { get; set; }

        /// <summary>
        /// The name of the study.
        /// </summary>
        public string StudyName { get; set; }

        /// <summary>
        /// If this is a standard, the calibration level.
        /// </summary>
        public SampleLevel SampleLevel { get; set; }

        /// <summary>
        /// The weight of the sample.
        /// </summary>
        public double SampleWeight { get; set; }

        /// <summary>
        /// Dilution factor for quantitation.
        /// </summary>
        public double Dilution { get; set; }

        /// <summary>
        /// Number of replicate injections from the sample.
        /// </summary>
        public int ReplicateNumber { get; set; }

        /// <summary>
        /// Sample position in the tray.
        /// </summary>
        public string WellPosition { get; set; }

        /// <summary>
        /// The volume of the sample injection.
        /// </summary>
        public double InjectionVolume { get; set; }

        /// <summary>
        /// Total time over which this sample is acquired.
        /// </summary>
        public double AcquisitionRunTime { get; set; }

        /// <summary>
        /// Time when begin sample injection.
        /// </summary>
        public DateTime AcquisitionStartTime { get; set; }

        /// <summary>
        /// Typically incubation or sample time.
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// The options used in processing the sample.
        /// </summary>
        public string ProcessingOptions { get; set; }

        /// <summary>
        /// The processing function.
        /// </summary>
        public string ProcessingFunction { get; set; }

        /// <summary>
        /// Processing sequence number.
        /// </summary>
        public int ProcessingSequenceNumber { get; set; }
    }

    public enum SampleType
    {
        Unspecified = 0,
        Standard = 1,
        Unknown = 2,
        Blank = 3,
        QC = 4,
        Reference = 5,
        ReagentBlank = 6,
        RTStandard = 7,
        CheckStandard = 8
    }

    public enum SampleLevel
    {
        Unspecified = 0,
        Level01 = 1,
        Level02 = 2,
        Level03 = 3,
        Level04 = 4,
        Level05 = 5,
        Level06 = 6,
        Level07 = 7,
        Level08 = 8,
        Level09 = 9,
        Level10 = 10
    }
}
