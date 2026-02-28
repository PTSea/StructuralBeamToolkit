namespace StructuralBeamToolkit.Models
{
    /// <summary>
    /// 
    /// Inputs for a simply supported beam calculation using closed-form formulas.
    /// 
    /// Units are user-defined, but must be consistent across all inputs.
    /// Example (SI): L in meters, P in Newtons, w in Newtons/meter, E in Pascals (N/m²), and I in m⁴.
    /// </summary>
    public class BeamInput
    {
        /// <summary>
        /// Beam length (distance between supports).
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// Load case (point load at center or uniform load).
        /// </summary>
        public LoadType LoadType { get; set; }

        /// <summary>
        /// Load magnitude.
        /// For PointLoadCenter, this is P (force at single location).
        /// For UniformLoad, this is w (force spread evenly along beam).
        /// </summary>
        public double Load { get; set; }

        /// <summary>
        /// Defines the material stiffness (Young's Modulus). Higher E, stiffer material, less deflection.
        /// </summary>
        public double E { get; set; }

        /// <summary>
        /// Second moment of area of the beam cross section. Higher I, more resistance to bending, less deflection.
        /// </summary>
        public double I { get; set; }
    }
}