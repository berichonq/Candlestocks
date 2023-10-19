/*
 * Quinn Berichon
 * Candlestick Class
 * 4/18/2023
 */

using System;

namespace COP_2513_002
{
    public class Candlestick
    {
        public DateTime Date { get; set; }
        public Double Open { get; set; }
        public Double High { get; set; }
        public Double Low { get; set; }
        public Double Close { get; set; }
        public long Volume { get; set; }


        /// <summary>
        /// Default Constructor for the Candlestick class
        /// </summary>
        public Candlestick()
        {
            Date = DateTime.Now;
            Open = new Double();
            High = new Double();
            Low = new Double();
            Close = new Double();
            Volume = 0;

            computeProperties();
        }


        /// <summary>
        /// Constructor for the Candlestick class, accepts 6 arguments to initialize the attributes for each new Candlestick object
        /// </summary>
        /// <param name="date"></param>
        /// <param name="open"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        /// <param name="close"></param>
        /// <param name="volume"></param>
        public Candlestick(DateTime date, double open, double high, double low, double close, long volume)
        {
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;

            computeProperties();
        }


        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="c"></param>
        public Candlestick(Candlestick c)
        {
            Date = c.Date;
            Open = c.Open;
            High = c.High;
            Low = c.Low;
            Close = c.Close;
            Volume = c.Volume;

            computeProperties();
        }


        //Top-level candlestick properties
        public double range { get; private set; }
        public double body { get; private set; }
        public double upperBodyLimit { get; private set; }
        public double lowerBodyLimit { get; private set; }
        public double upperTail { get; private set; }
        public double lowerTail { get; private set; }

        public Boolean isBullish { get; private set; }
        public Boolean isNeutral { get; private set; }
        public Boolean isBearish { get; private set; }

        public Boolean isDoji { get; private set; }
        public Boolean isLongLeggedDoji { get; private set; }
        public Boolean isDragonflyDoji { get; private set; }
        public Boolean isGravestoneDoji { get; private set; }
        public Boolean isMarubozu { get; private set; }
        public Boolean isWhiteMarubozu { get; private set; }
        public Boolean isBlackMarubozu { get; private set; }
        public Boolean isHammer { get; private set; }
        public Boolean isBullishHammer { get; private set; }
        public Boolean isBearishHammer { get; private set; }
        public Boolean isInvertedHammer { get; private set; }
        public Boolean isBullishInvertedHammer { get; private set; }
        public Boolean isBearishInvertedHammer { get; private set; }


        /// <summary>
        /// Tests for the Doji pattern
        /// </summary>
        /// <param name="bodyTolerance"></param>
        /// <returns></returns>
        public Boolean dojiTest(double bodyTolerance = 0.06)
        {
            if (range < double.Epsilon)
            {
                return false; // prevent division by zero
            }
            return body/range <= bodyTolerance;
        }


        /// <summary>
        /// Tests for the Long-Legged Doji pattern
        /// </summary>
        /// <param name="bodyTolerance"></param>
        /// <param name="tailDifferenceTolerance"></param>
        /// <returns></returns>
        public Boolean longLeggedDojiTest(double bodyTolerance = 0.05, double tailDifferenceTolerance = 0.13)
        {
            return dojiTest(bodyTolerance) && (Math.Abs(upperTail - lowerTail) <= tailDifferenceTolerance * range);
        }


        /// <summary>
        /// Tests for the Dragonfly Doji pattern
        /// </summary>
        /// <param name="bodyTolerance"></param>
        /// <param name="upperTailTolerance"></param>
        /// <returns></returns>
        public Boolean dragonflyDojiTest(double bodyTolerance = 0.06, double upperTailTolerance = 0.2)
        {
            return dojiTest(bodyTolerance) && (upperTail <= range * upperTailTolerance);
        }


        /// <summary>
        /// Tests for the Gravestone Doji pattern
        /// </summary>
        /// <param name="bodyTolerance"></param>
        /// <param name="lowerTailTolerance"></param>
        /// <returns></returns>
        public Boolean gravestoneDojiTest(double bodyTolerance = 0.06, double lowerTailTolerance = 0.2)
        {
            return dojiTest(bodyTolerance) && (lowerTail <= range * lowerTailTolerance);
        }


        /// <summary>
        /// Tests for the Marubozu pattern
        /// </summary>
        /// <param name="tailTolerance"></param>
        /// <returns></returns>
        public Boolean marubozuTest(double tailTolerance = 0.025)
        {
            return (upperTail <= range * tailTolerance) && (lowerTail <= range * tailTolerance);
        }


        /// <summary>
        /// Tests for the White Marubozu pattern
        /// </summary>
        /// <param name="tailTolerance"></param>
        /// <returns></returns>
        public Boolean whiteMarubozuTest(double tailTolerance = 0.025)
        {
            return marubozuTest(tailTolerance) && isBullish;
        }


        /// <summary>
        /// Tests for the Black Marubozu pattern
        /// </summary>
        /// <param name="tailTolerance"></param>
        /// <returns></returns>
        public Boolean blackMarubozuTest(double tailTolerance = 0.025)
        {
            return marubozuTest(tailTolerance) && isBearish;
        }


        /// <summary>
        /// Tests for the Hammer pattern
        /// </summary>
        /// <param name="minBodyTolerance"></param>
        /// <param name="maxBodyTolerance"></param>
        /// <param name="UpperTailTolerance"></param>
        /// <returns></returns>
        public Boolean hammerTest(double minBodyTolerance = 0.2, double maxBodyTolerance = 0.35, double UpperTailTolerance = 0.12)
        {
            return (upperTail <= range * UpperTailTolerance) && (body >= range * minBodyTolerance) && (body <= range * maxBodyTolerance) && (lowerTail >= 2 * body);
        }


        /// <summary>
        /// Tests for the Bullish Hammer pattern
        /// </summary>
        /// <param name="minBodyTolerance"></param>
        /// <param name="maxBodyTolerance"></param>
        /// <param name="UpperTailTolerance"></param>
        /// <returns></returns>
        public Boolean bullishHammerTest(double minBodyTolerance = 0.2, double maxBodyTolerance = 0.35, double UpperTailTolerance = 0.12)
        {
            return hammerTest(minBodyTolerance, maxBodyTolerance, UpperTailTolerance) && isBullish;
        }


        /// <summary>
        /// Tests for the Bearish Hammer pattern
        /// </summary>
        /// <param name="minBodyTolerance"></param>
        /// <param name="maxBodyTolerance"></param>
        /// <param name="UpperTailTolerance"></param>
        /// <returns></returns>
        public Boolean bearishHammerTest(double minBodyTolerance = 0.2, double maxBodyTolerance = 0.35, double UpperTailTolerance = 0.12)
        {
            return hammerTest(minBodyTolerance, maxBodyTolerance, UpperTailTolerance) && isBearish;
        }


        /// <summary>
        /// Tests for the Inverted Hammer pattern
        /// </summary>
        /// <param name="minBodyTolerance"></param>
        /// <param name="maxBodyTolerance"></param>
        /// <param name="LowerTailTolerance"></param>
        /// <returns></returns>
        public Boolean invertedHammerTest(double minBodyTolerance = 0.2, double maxBodyTolerance = 0.35, double LowerTailTolerance = 0.12)
        {
            return (lowerTail <= range * LowerTailTolerance) && (body >= range * minBodyTolerance) && (body <= range * maxBodyTolerance) && (upperTail >= 2 * body);
        }


        /// <summary>
        /// Tests for the Bullish Inverted Hammer pattern
        /// </summary>
        /// <param name="minBodyTolerance"></param>
        /// <param name="maxBodyTolerance"></param>
        /// <param name="LowerTailTolerance"></param>
        /// <returns></returns>
        public Boolean bullishInvertedHammerTest(double minBodyTolerance = 0.2, double maxBodyTolerance = 0.35, double LowerTailTolerance = 0.12)
        {
            return invertedHammerTest(minBodyTolerance, maxBodyTolerance, LowerTailTolerance) && isBullish;
        }


        /// <summary>
        /// Tests for the Bearish Inverted Hammer pattern
        /// </summary>
        /// <param name="minBodyTolerance"></param>
        /// <param name="maxBodyTolerance"></param>
        /// <param name="LowerTailTolerance"></param>
        /// <returns></returns>
        public Boolean bearishInvertedHammerTest(double minBodyTolerance = 0.2, double maxBodyTolerance = 0.35, double LowerTailTolerance = 0.12)
        {
            return invertedHammerTest(minBodyTolerance, maxBodyTolerance, LowerTailTolerance) && isBearish;
        }


        /// <summary>
        /// Private helper method to compute the top level metrics of a candlestick object and contains the call to run the candlestick object through all pattern tests contained
        /// </summary>
        private void computeProperties()
        {
            range = High - Low;
            body = Math.Abs(Open - Close);
            upperBodyLimit = Math.Max(Open, Close);
            lowerBodyLimit = Math.Min(Open, Close);
            upperTail = High - upperBodyLimit;
            lowerTail = lowerBodyLimit - Low;

            computePatterns();
        }


        /// <summary>
        /// Initializes the values of the top-level properties for a candlestick object
        /// </summary>
        private void computePatterns()
        {
            isBullish = Close > Open;
            isNeutral = Close == Open;
            isBearish = Close < Open;

            //Set Pattern Properties
            isDoji = dojiTest();
            isLongLeggedDoji = longLeggedDojiTest();
            isDragonflyDoji = dragonflyDojiTest();
            isGravestoneDoji = gravestoneDojiTest();

            isMarubozu = marubozuTest();
            isWhiteMarubozu = whiteMarubozuTest();
            isBlackMarubozu = blackMarubozuTest();

            isHammer = hammerTest();
            isBullishHammer = bullishHammerTest();
            isBearishHammer = bearishHammerTest();

            isInvertedHammer = invertedHammerTest();
            isBullishInvertedHammer = bullishInvertedHammerTest();
            isBearishInvertedHammer = bearishInvertedHammerTest();
        }
       
    }
}