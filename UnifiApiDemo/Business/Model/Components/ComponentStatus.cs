using System;

namespace UnifiApiDemo.Business.Model.Components
{
    /// <summary>
    /// Status of a component
    /// </summary>
    [Flags]
    public enum ComponentStatus
    {
        /// <summary>
        /// status is unknown e.g. processing is not done
        /// </summary>
        Unknown = 1,
        /// <summary>
        /// Unidentified candidate
        /// </summary>
        CandidateUnidentified = 2,
        /// <summary>
        /// Identified candidate
        /// </summary>
        CandidateIdentified = 4,
        /// <summary>
        /// Identified target component
        /// </summary>
        TargetIdentified = 8,
        /// <summary>
        /// Missing target component
        /// </summary>
        TargetMissing = 16,
        /// <summary>
        /// A named group calculated by components
        /// </summary>
        NamedGroup = 32
    }
}
