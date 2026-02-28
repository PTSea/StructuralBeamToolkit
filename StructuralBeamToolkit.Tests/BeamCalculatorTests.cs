using System;
using StructuralBeamToolkit.Models;
using StructuralBeamToolkit.Services;
using Xunit;

namespace StructuralBeamToolkit.Tests
{
    public class BeamCalculatorTests
    {
        #region MOMENT COMPUTATION TESTS
        [Fact]
        public void PointLoadCenter_Computes_MaxMoment()
        {
            // Mmax = P*L/4
            var beamCalculator = new BeamCalculator();

            var input = new BeamInput
            {
                Length = 10,
                E = 200e9,
                I = 1e-6,
                LoadType = LoadType.PointLoadCenter,
                Load = 100
            };

            var result = beamCalculator.Calculate(input);

            Assert.Equal(250, result.MaxMoment, 6); // 100*10/4 = 250

        }

        [Fact]
        public void UniformLoad_Computes_MaxMoment()
        {
            // Mmax = w*L^2/8
            var beamCalculator = new BeamCalculator();

            var input = new BeamInput
            {
                Length = 10,
                E = 200e9,
                I = 1e-6,  
                LoadType = LoadType.UniformLoad,
                Load = 20 
            };

            var result = beamCalculator.Calculate(input);

            Assert.Equal(250, result.MaxMoment, 6); // 20*10^2/8 = 250

        }
        #endregion

        #region DEFLECTION COMPUTATION TESTS
        [Fact]
        public void PointLoadCenter_Computes_MaxDeflection()
        {
            // δmax = P*L^3 / (48*E*I)
            var beamCalculator = new BeamCalculator();

            var input = new BeamInput
            {
                Length = 10,
                E = 200e9,
                I = 1e-6,
                LoadType = LoadType.PointLoadCenter,
                Load = 100
            };

            var result = beamCalculator.Calculate(input);

            Assert.Equal(0.0104167, result.MaxDeflection, 6); // 100*10^3/(48*200e9*1e-6) = 0.0104167
        }

        [Fact]
        public void UniformLoad_Computes_MaxDeflection()
        {
            // δmax = 5*w*L^4 / (384*E*I)
            var beamCalculator = new BeamCalculator();

            var input = new BeamInput
            {
                Length = 10,
                E = 200e9,
                I = 1e-6,
                LoadType = LoadType.UniformLoad,
                Load = 20
            };

            var result = beamCalculator.Calculate(input);

            Assert.Equal(0.0130208, result.MaxDeflection, 6); // 5*20*10^4/(384*200e9*1e-6) = 0.0130208
        }
        #endregion

        #region INVALID INPUT TESTS
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Calculate_InvalidLength_ThrowsArgumentException(double length)
        {
            var beamCalculator = new BeamCalculator();

            var input = new BeamInput
            {
                Length = length,
                E = 200e9,
                I = 1e-6,
                LoadType = LoadType.PointLoadCenter,
                Load = 100
            };

            Assert.Throws<ArgumentException>(() => beamCalculator.Calculate(input));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-200e9)]
        public void Calculate_InvalidYoungsModulus_ThrowsArgumentException(double youngsModulus)
        {
            var beamCalculator = new BeamCalculator();

            var input = new BeamInput
            {
                Length = 10,
                E = youngsModulus,
                I = 1e-6,
                LoadType = LoadType.PointLoadCenter,
                Load = 100
            };

            Assert.Throws<ArgumentException>(() => beamCalculator.Calculate(input));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1e-6)]
        public void Calculate_InvalidMomentOfInertia_ThrowsArgumentException(double momentOfInertia)
        {
            var beamCalculator = new BeamCalculator();
            var input = new BeamInput
            {
                Length = 10,
                E = 200e9,
                I = momentOfInertia,
                LoadType = LoadType.PointLoadCenter,
                Load = 100
            };
            Assert.Throws<ArgumentException>(() => beamCalculator.Calculate(input));
        }

        [Fact]
        public void Calculate_NegativeLoad_ThrowsArgumentException()
        {
            var beamCalculator = new BeamCalculator();
            var input = new BeamInput
            {
                Length = 10,
                E = 200e9,
                I = 1e-6,
                LoadType = LoadType.PointLoadCenter,
                Load = -50
            };
            Assert.Throws<ArgumentException>(() => beamCalculator.Calculate(input));
        }

        [Fact]
        public void Calculate_UnsupportedLoadType_ThrowsNotSupportedException()
        {
            var beamCalculator = new BeamCalculator();
            var input = new BeamInput
            {
                Length = 10,
                E = 200e9,
                I = 1e-6,
                LoadType = (LoadType)999, // Invalid load type
                Load = 100
            };
            Assert.Throws<NotSupportedException>(() => beamCalculator.Calculate(input));
        }

        #endregion
    }
}
