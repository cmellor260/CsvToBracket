using CSVData.Controllers;
using System.Text;

namespace CSVData
{
    public class CsvParser : ICsvParser
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvParser"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        public CsvParser(ILogger<CsvParserController> logger)
        {
            this.logger = logger;
        }

        private StringBuilder FormFinalElement(StringBuilder sb)
        {
            StringBuilder finalEl = new StringBuilder();
            finalEl.Append("[");
            string element = sb.ToString().TrimEnd();
            finalEl.Append(element);
            finalEl.Append("]");
            return finalEl;
        }

        private List<string> GetNewColumns(string line)
        {
            List<string> columns = new List<string>();
            int sLen = line.Length;
            bool startWord = false;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < sLen; i++)
            {
                if (line[i] == '"')
                {
                    // Found Second Comma
                    if (startWord)
                    {
                        StringBuilder fe = FormFinalElement(sb);
                        columns.Add(fe.ToString());
                        // clear word
                        sb.Clear();
                        startWord = false;
                        // skip over comma after last end quote.
                        i++;
                    }
                    else
                    {
                        startWord = true;
                    }
                }
                else if ((line[i] == ',') && (!startWord))
                { 
                    // If found a comma and no start word, add element
                    // this is an element not seperated by "
                    StringBuilder fe = FormFinalElement(sb);
                    columns.Add(fe.ToString());
                    // clear word
                    sb.Clear();
                } 
                else
                {
                    sb.Append(line[i]);
                }
            }
            // Ensure last element gets added if not seperated "
            if (sb.Length > 0)
            {
                StringBuilder fe = FormFinalElement(sb);
                columns.Add(fe.ToString());
                // clear word
                sb.Clear();
            }
            return columns; 
        }


        private string ConvertCsvToBrackets(string input)
        {
            var lines = input.Split("\r\n");
            List<string> newLines = new List<string>();
            foreach (var line in lines)
            {
                var columns = GetNewColumns(line);
                StringBuilder sRow = new StringBuilder();
                foreach (var column in columns)
                {
                    sRow.Append(column.ToString());
                }
                newLines.Add(sRow.ToString());
            }
            StringBuilder finalStrB = new StringBuilder();
            foreach (var newLine in newLines)
            {
                finalStrB.Append(newLine);
                finalStrB.Append("\r\n");
            }
            if (finalStrB.Length >= 2)
            {
                // remove \r\n from the last one.
                finalStrB.Remove(finalStrB.Length - 2, 2);
            }
            return finalStrB.ToString();

        }


        /// <summary>
        /// takes data as csv input format and returns data with [] around each element.
        /// </summary>
        /// <returns>return list of MasterMethodSettingsResponseModel.</returns>
        public string CsvToBracketSeperation(string input)
        {
            this.logger.LogInformation("CsvToBracketSeperation(string input)");

            try
            {
                return ConvertCsvToBrackets(input);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message, ex);
                throw;
            }
        }

    }
}
