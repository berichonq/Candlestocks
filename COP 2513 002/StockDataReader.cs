/*
 * Quinn Berichon
 * StockDataReader Class
 * 4/18/2023
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace COP_2513_002
{
    internal class StockDataReader
    {
        /// <summary>
        /// Default constructor for the StockDataReader class
        /// </summary>
        public StockDataReader() { }


        /// <summary>
        /// Accepts arguments for a file directory path, as well as starting and ending dates specifying the data range requested
        /// Reads and parses the contents of the stock data CSV located at the path argument passed
        /// Returns an empty list if no file is found or if the header line of the CSV is incorrectly formatted
        /// </summary>
        /// <param name="path"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns> a list of Candlestick objects, a new instance of the Candlestick class for each line of the stock data CSV</returns>
        public List<Candlestick> ReadStockData(string path, DateTime startDate, DateTime endDate)
        {
            List<Candlestick> selectedLines = new List<Candlestick>();
            if (File.Exists(path))
            {
                String[] allLines = System.IO.File.ReadAllLines(path);
                String header = allLines[0];
                if (header == "Date,Open,High,Low,Close,Adj Close,Volume")
                {
                    for (int i = 1; i < allLines.Length; i++)
                    {
                        String[] line = allLines[i].Split(',');
                        DateTime date = createDateTime(line);
                        if (DateTime.Compare(date, endDate.Date) <= 0 && DateTime.Compare(date, startDate.Date) >= 0)
                        {
                            double open = Math.Round(double.Parse(line[1]), 2);
                            double high = Math.Round(double.Parse(line[2]), 2);
                            double low = Math.Round(double.Parse(line[3]), 2);
                            double close = Math.Round(double.Parse(line[4]), 2);
                            long volume = long.Parse(line[6]);
                            selectedLines.Add(new Candlestick(date.Date, open, high, low, close, volume));
                        }
                    }
                    return selectedLines;
                }
                return selectedLines;
            }
            return selectedLines;
        }


        /// <summary>
        /// Private helper method called by the ReadStockData method to create a new DateTime object using the contents of a line from the CSV passed to it
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private DateTime createDateTime(String[] line)
        {
            String[] date = line[0].Split('-');
            DateTime candleDate = new DateTime(int.Parse(date[0].TrimStart(new Char[] {'0'})), int.Parse(date[1].TrimStart(new Char[] {'0'})), int.Parse(date[2].TrimStart(new Char[] {'0'})));
            return candleDate;
        }
    }
}