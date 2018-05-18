using System;

namespace UnifiApiDemo.Models
{
    public class ComponentViewModel
    {
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
        public string ComponentStatus { get; set; }
    }
}
