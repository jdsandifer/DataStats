using Microsoft.VisualStudio.TestTools.UnitTesting;
using JDSandifer.Interview;

namespace DataStatsTests
{
    [TestClass]
    public class CalculateStatsTests
    {
        [TestMethod]
        public void CalcStats_WithEmptyArray_ReturnsNoData()
        {
            double[] input = new double[0];
            string expectedOutput = "   Error reading file: no data";
            
            Assert.AreEqual(expectedOutput, DataStats.CalculateAndFormatFileStats(input));
        }

        [TestMethod]
        public void CalcStats_WithOneElementArray_ReturnsStats()
        {
            double[] input = { 10.0 };
            string expectedOutput = "   Sum: 10"
                                + "\n   Min: 10"
                                + "\n   Max: 10"
                                + "\n   Average: 10"
                                + "\n   Standard Deviation: 0";
            Assert.AreEqual(expectedOutput, DataStats.CalculateAndFormatFileStats(input));
        }

        [TestMethod]
        public void CalcStats_WithOneThroughTenArray_ReturnsStats()
        {
            double[] input = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            string expectedOutput = "   Sum: 55"
                                + "\n   Min: 1"
                                + "\n   Max: 10"
                                + "\n   Average: 5.5"
                                + "\n   Standard Deviation: 2.87";
            Assert.AreEqual(expectedOutput, DataStats.CalculateAndFormatFileStats(input));
        }

        [TestMethod]
        public void CalcStats_WithLargeDeviationArray_ReturnsStats()
        {
            double[] input = { -997997997997.997, 997997997997.997, 997997997997.997,
                                997997997997.997, 997997997997.997, 997997997997.997,
                                997997997997.997, 997997997997.997, 997997997997.997,
                                997997997997.997 };
            string expectedOutput = "   Sum: 7983983983983.98"
                                + "\n   Min: -997997997998"
                                + "\n   Max: 997997997998"
                                + "\n   Average: 798398398398.4"
                                + "\n   Standard Deviation: 598798798798.8";
            Assert.AreEqual(expectedOutput, DataStats.CalculateAndFormatFileStats(input));
        }
    }
}
