namespace StructuralBeamToolkit.Models
{
    /// <summary>
    /// These are the results for a simply supported beam under a basic load case.
    /// </summary>
    public class BeamResult
    {
        /// <summary>
        /// The maximum bending moment in the beam (at mid-span for these load cases).
        /// </summary>
        public double MaxDeflection { get; set; }

        /// <summary>
        /// The maximum vertical deflection (sag) of the beam (at mid-span for these load cases).
        /// </summary>
        public double MaxMoment { get; set; }
    }
}
