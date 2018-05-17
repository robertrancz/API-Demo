using System;
using System.Collections.Generic;


namespace UnifiApiDemo.Business.Model.Components
{
    public class Component
    {
        public Component()
        {
            ComponentStatus = ComponentStatus.Unknown;
        }
        /// <summary>
        /// The component ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The name of the component
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User generated comment
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Accessor for the label of the component.
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// Monoisotopic molar mass of the component
        /// </summary>
        public double? MonoisotopicMolarMass { get; set; }
        /// <summary>
        /// Charge of the component
        /// </summary>
        public int? Charge { get; set; }
        /// <summary>
        /// Chemical formula of the component
        /// </summary>
        public string Formula { get; set; }
        /// <summary>
        /// Id of the parent component e.g. for peptides or metabolites
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// It is useful to know if this component has been identified with a target component.
        /// </summary>
        public ComponentStatus ComponentStatus { get; set; }
        ///// <summary>
        ///// The current review status of this component(user set).
        ///// </summary>
        //public ReviewStatus? ReviewStatus { get; set; }
        ///// <summary>
        ///// Results of quantitative analysis
        ///// </summary>
        //public QuantitativResult Quantitativ { get; set; }
        ///// <summary>
        ///// Results of chromatography
        ///// </summary>
        //public ChromatographicResult Chromatographic { get; set; }
        ///// <summary>
        ///// Results of mass spectrometry
        ///// </summary>
        //public MSResult MS { get; set; }
        ///// <summary>
        ///// Results of ion mobility scan
        ///// </summary>
        //public IMSResult IMS { get; set; }
        /// <summary>
        /// Related Peaks
        /// </summary>
        public IEnumerable<ChromatogramPeak> Peaks { get; set; }
    }
}
