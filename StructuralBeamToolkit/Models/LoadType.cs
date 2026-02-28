namespace StructuralBeamToolkit.Models
{
    /// <summary>
    /// Supported types of loads on a simply supported beam
    /// </summary>
    public enum LoadType
    {
        /// <summary>
        /// A single concentrated load applied at the center of the beam
        /// </summary>
        PointLoadCenter,

        /// <summary>
        /// A uniformly distributed load applied along the entire length of the beam
        /// </summary>
        UniformLoad
    }
}
