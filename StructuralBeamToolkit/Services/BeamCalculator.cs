using StructuralBeamToolkit.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace StructuralBeamToolkit.Services
{
    /// <summary>
    /// Calculates basic beam responses for a simply supported beam using standard closed-form formulas
    /// from elementary beam theory (Euler-Bernoulli assumptions).
    /// 
    /// Scope/Assumptions:
    /// 1. Simply supported at both ends.
    /// 2. Small deflections (linear elastic behavior).
    /// 3. Prismatic beam (constant E and I along the beam length).
    /// 4. Load cases: center point load OR full-span uniform load.
    /// 
    /// This tool is for quick checks and is not a full structural analysis program.
    /// </summary>
    public class BeamCalculator
    {
        /// <summary>
        /// Calculates the maximum moment and deflection for the given beam input.
        /// </summary>
        /// <param name="input">Beam geometry, material properties, and load case/magnitude.</param>
        /// <returns>Max moment and max deflection for the selected load case.</returns>
        /// <exception cref="ArgumentException">Thrown when inputs are non-physical (e.g., Length <=0 ).</exception>
        public BeamResult Calculate(BeamInput input)
        {
            Validate(input);

            return input.LoadType switch
            {
                LoadType.PointLoadCenter => CalculatePointLoadCenter(input),
                LoadType.UniformLoad => CalculateUniformLoad(input),
                _ => throw new NotSupportedException("Unsupported load type.")
            };


        }

        private static void Validate(BeamInput input)
        {
            if (input.Length <= 0)
                throw new ArgumentException("Length must be greater than zero.");
            if (input.E <= 0)
                throw new ArgumentException("Young's Modulus must be greater than zero.");
            if (input.I <= 0)
                throw new ArgumentException("Moment of Inertia must be greater than zero.");
            if (input.Load < 0)
                throw new ArgumentException("Load must be non-negative.");
        }


        /// <summary>
        /// Calculates max moment and deflection for a point load at the center of a simply supported beam.
        /// 
        /// Mmax = P*L/4
        /// δmax = P*L^3 / (48*E*I)
        /// </summary>
        private static BeamResult CalculatePointLoadCenter(BeamInput input)
        {
            double P = input.Load;
            double L = input.Length;

            return new BeamResult
            {
                MaxMoment = P * L / 4.0,
                MaxDeflection = (P * Math.Pow(L, 3)) / (48.0 * input.E * input.I)
            };
        }

        /// <summary>
        /// Calculates max moment and deflection for a uniformly distributed load on a simply supported beam.
        /// 
        /// Mmax = w*L^2/8
        /// δmax = 5*w*L^4 / (384*E*I)
        /// </summary>
        private static BeamResult CalculateUniformLoad(BeamInput input)
        {


            double w = input.Load;
            double L = input.Length;

            return new BeamResult
            {
                MaxMoment = w * Math.Pow(L, 2) / 8.0,
                MaxDeflection = (5.0 * w * Math.Pow(L, 4)) / (384.0 * input.E * input.I)
            };
        }
    }

}
