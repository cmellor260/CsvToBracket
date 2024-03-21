using CSVData.Controllers;
using System.Text;

namespace CSVData
{
    /// <summary>
    /// contracts for CsvParser.
    /// </summary>
    public interface ICsvParser
    {
        string CsvToBracketSeperation(string input);
    }
}
