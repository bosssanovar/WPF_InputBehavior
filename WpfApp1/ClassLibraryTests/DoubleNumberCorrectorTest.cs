using ClassLibrary;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryTests
{
    public class DoubleNumberCorrectorTest
    {
        [Theory]
        [InlineData(-100, -9.5)]
        [InlineData(-9.51, -9.5)]
        [InlineData(-9.5, -9.5)]
        [InlineData(-9.4, -9.0)]
        [InlineData(-9.0000001, -9.0)]
        [InlineData(-9.0, -9.0)]
        [InlineData(0, 0)]
        [InlineData(0.0001, 0)]
        [InlineData(0.499999, 0)]
        [InlineData(0.5, 0.5)]
        [InlineData(0.500001, 0.5)]
        [InlineData(10.4999, 10.0)]
        [InlineData(10.5, 10.5)]
        [InlineData(10.50001, 10.5)]
        [InlineData(100, 10.5)]
        public void 値補正(double input, double expected)
        {
            double max = 10.5;
            double min = -9.5;
            double step = 0.5;

            var corrector = new DoubleNumberCorrector(max, min, step);
            Assert.Equal(expected, corrector.Correct(input));
        }
    }
}
