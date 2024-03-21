using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSVData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using CSVData.Controllers;

namespace CSVData.Tests
{
    [TestClass()]
    public class CsvParserTests
    {
        private readonly Mock<ILogger<CsvParserController>> loggerMock;

        public CsvParserTests()
        {
            this.loggerMock = new Mock<ILogger<CsvParserController>>();
        }

        [TestMethod()]
        public void CsvToBracketSeperationTest()
        {
            string csvInput = "\"Patient Name\",\"SSN\",\"Age\",\"Phone Number\",\"Status\"\r\n"
                + "\"Prescott, Zeke\",\"542-51-6641\",21,\"801-555-2134\",\"Opratory=2,PCP=1\"\r\n"
                + "\"Goldstein, Bucky\",\"635-45-1254\",42,\"435-555-1541\",\"Opratory=1,PCP=1\"\r\n"
                + "\"Vox, Bono\",\"414-45-1475\",51,\"801-555-2100\",\"Opratory=3,PCP=2\"";

            ICsvParser csvParser = new CsvParser(this.loggerMock.Object);
            var actualOutput = csvParser.CsvToBracketSeperation(csvInput);

            string csvOutput = "[Patient Name][SSN][Age][Phone Number][Status]\r\n" +
                               "[Prescott, Zeke][542-51-6641][21][801-555-2134][Opratory=2,PCP=1]\r\n" +
                                "[Goldstein, Bucky][635-45-1254][42][435-555-1541][Opratory=1,PCP=1]\r\n" +
                                "[Vox, Bono][414-45-1475][51][801-555-2100][Opratory=3,PCP=2]";


            Assert.AreEqual(csvOutput, actualOutput);
        }

        [TestMethod()]
        public void CsvToBracketSeperationTestNull()
        {
            string csvInput = null;
            try
            {
                ICsvParser csvParser = new CsvParser(this.loggerMock.Object);
                var actualOutput = csvParser.CsvToBracketSeperation(csvInput);
            } 
            catch (Exception ex)
            {
                return;
            }
            Assert.Fail();
        }

    }
}