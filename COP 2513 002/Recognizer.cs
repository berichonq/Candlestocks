/*
 * Quinn Berichon
 * Recognizer Class
 * 4/18/2023
*/

using System.Collections.Generic;

namespace COP_2513_002
{
    internal abstract class Recognizer
    {
        public string patternName { get; set; }
        public int patternSize { get; set; }
    
        
        /// <summary>
        /// Base Class Constructor
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="s"></param>
        public Recognizer(string pn = "?", int s = 1)
        {
            patternName = pn;
            patternSize = s;
        }


        /// <summary>
        /// Implementing classes check if the candlestick subset passed is the pattern
        /// </summary>
        /// <param name="subset"></param>
        /// <returns></returns>
        protected abstract bool subsetMatchesPattern(List<Candlestick> subset);


        /// <summary>
        /// Loops through the candlesticks, making calls to subsetMatchesPattern for each subset and returning candlestick indices accordingly
        /// </summary>
        /// <param name="candlesticks"></param>
        /// <returns></returns>
        public List<int> Recognize(List<Candlestick> candlesticks)
        {
            List<int> indices = new List<int>(candlesticks.Count / 8);

            int offset = patternSize - 1;
            for(int i = patternSize - 1; i < candlesticks.Count; i++)
            {
                List<Candlestick> subset = candlesticks.GetRange(i - offset, patternSize);
                if (subsetMatchesPattern(subset))
                {
                    indices.Add(i);
                }
            }
            return indices;
        }
    }


    internal class Recognizer_Doji : Recognizer
    {
        /// <summary>
        /// Calls the base class constructor
        /// </summary>
        public Recognizer_Doji() : base("Doji", 1) { }


        /// <summary>
        /// Implements the abstract subsetMatchesPattern method defined in the Recognizer base class. Returns true if the candlestick subset is a Doji
        /// </summary>
        /// <param name="subset"></param>
        /// <returns></returns>
        protected override bool subsetMatchesPattern(List<Candlestick> subset)
        {
            if (subset[0].isDoji)
            {
                return true;
            }
            return false;
        }
    }


    internal class Recognizer_LongLeggedDoji : Recognizer
    {
        /// <summary>
        /// Calls the base class constructor
        /// </summary>
        public Recognizer_LongLeggedDoji() : base("Long-Legged Doji", 1) { }


        /// <summary>
        /// Implements the abstract subsetMatchesPattern method defined in the Recognizer base class. Returns true if the candlestick subset is a Long-Legged Doji
        /// </summary>
        /// <param name="subset"></param>
        /// <returns></returns>
        protected override bool subsetMatchesPattern(List<Candlestick> subset)
        {
            if (subset[0].isLongLeggedDoji)
            {
                return true;
            }
            return false;
        }
    }


    internal class Recognizer_DragonflyDoji : Recognizer
    {
        /// <summary>
        /// Calls the base class constructor
        /// </summary>
        public Recognizer_DragonflyDoji() : base("Dragonfly Doji", 1) { }


        /// <summary>
        /// Implements the abstract subsetMatchesPattern method defined in the Recognizer base class. Returns true if the candlestick subset is a Dragonfly Doji
        /// </summary>
        /// <param name="subset"></param>
        /// <returns></returns>
        protected override bool subsetMatchesPattern(List<Candlestick> subset)
        {
            if (subset[0].isDragonflyDoji)
            {
                return true;
            }
            return false;
        }
    }


    internal class Recognizer_GravestoneDoji : Recognizer
    {
        /// <summary>
        /// Calls the base class constructor
        /// </summary>
        public Recognizer_GravestoneDoji() : base("Gravestone Doji", 1) { }


        /// <summary>
        /// Implements the abstract subsetMatchesPattern method defined in the Recognizer base class. Returns true if the candlestick subset is a Gravestone Doji
        /// </summary>
        /// <param name="subset"></param>
        /// <returns></returns>
        protected override bool subsetMatchesPattern(List<Candlestick> subset)
        {
            if (subset[0].isGravestoneDoji)
            {
                return true;
            }
            return false;
        }
    }


    internal class Recognizer_WhiteMarubozu : Recognizer
    {
        /// <summary>
        /// Calls the base class constructor
        /// </summary>
        public Recognizer_WhiteMarubozu() : base("White Marubozu", 1) { }


        /// <summary>
        /// Implements the abstract subsetMatchesPattern method defined in the Recognizer base class. Returns true if the candlestick subset is a White Marubozu
        /// </summary>
        /// <param name="subset"></param>
        /// <returns></returns>
        protected override bool subsetMatchesPattern(List<Candlestick> subset)
        {
            if (subset[0].isWhiteMarubozu)
            {
                return true;
            }
            return false;
        }
    }


    internal class Recognizer_BlackMarubozu : Recognizer
    {
        /// <summary>
        /// Calls the base class constructor
        /// </summary>
        public Recognizer_BlackMarubozu() : base("Black Marubozu", 1) { }


        /// <summary>
        /// Implements the abstract subsetMatchesPattern method defined in the Recognizer base class. Returns true if the candlestick subset is a Black Marubozu
        /// </summary>
        /// <param name="subset"></param>
        /// <returns></returns>
        protected override bool subsetMatchesPattern(List<Candlestick> subset)
        {
            if (subset[0].isBlackMarubozu)
            {
                return true;
            }
            return false;
        }
    }


    internal class Recognizer_BullishHammer : Recognizer
    {
        /// <summary>
        /// Calls the base class constructor
        /// </summary>
        public Recognizer_BullishHammer() : base("Bullish Hammer", 1) { }


        /// <summary>
        /// Implements the abstract subsetMatchesPattern method defined in the Recognizer base class. Returns true if the candlestick subset is a Bullish Hammer
        /// </summary>
        /// <param name="subset"></param>
        /// <returns></returns>
        protected override bool subsetMatchesPattern(List<Candlestick> subset)
        {
            if (subset[0].isBullishHammer)
            {
                return true;
            }
            return false;
        }
    }


    internal class Recognizer_BearishHammer : Recognizer
    {
        /// <summary>
        /// Calls the base class constructor
        /// </summary>
        public Recognizer_BearishHammer() : base("Bearish Hammer", 1) { }


        /// <summary>
        /// Implements the abstract subsetMatchesPattern method defined in the Recognizer base class. Returns true if the candlestick subset is a Bearish Hammer
        /// </summary>
        /// <param name="subset"></param>
        /// <returns></returns>
        protected override bool subsetMatchesPattern(List<Candlestick> subset)
        {
            if (subset[0].isBearishHammer)
            {
                return true;
            }
            return false;
        }
    }


    internal class Recognizer_BullishInvertedHammer : Recognizer
    {
        /// <summary>
        /// Calls the base class constructor
        /// </summary>
        public Recognizer_BullishInvertedHammer() : base("Bullish Inverted Hammer", 1) { }


        /// <summary>
        /// Implements the abstract subsetMatchesPattern method defined in the Recognizer base class. Returns true if the candlestick subset is a Bullish Inverted Hammer
        /// </summary>
        /// <param name="subset"></param>
        /// <returns></returns>
        protected override bool subsetMatchesPattern(List<Candlestick> subset)
        {
            if (subset[0].isBullishInvertedHammer)
            {
                return true;
            }
            return false;
        }
    }


    internal class Recognizer_BearishInvertedHammer : Recognizer
    {
        /// <summary>
        /// Calls the base class constructor
        /// </summary>
        public Recognizer_BearishInvertedHammer() : base("Bearish Inverted Hammer", 1) { }


        /// <summary>
        /// Implements the abstract subsetMatchesPattern method defined in the Recognizer base class. Returns true if the candlestick subset is a Bearish Inverted Hammer
        /// </summary>
        /// <param name="subset"></param>
        /// <returns></returns>
        protected override bool subsetMatchesPattern(List<Candlestick> subset)
        {
            if (subset[0].isBearishInvertedHammer)
            {
                return true;
            }
            return false;
        }
    }


    internal class Recognizer_BullishEngulfing : Recognizer
    {
        /// <summary>
        /// Calls the base class constructor
        /// </summary>
        public Recognizer_BullishEngulfing() : base("Bullish Engulfing", 2) { }


        /// <summary>
        /// Implements the abstract subsetMatchesPattern method defined in the Recognizer base class. Returns true if the candlestick subset is a Bullish Engulfing
        /// </summary>
        /// <param name="subset"></param>
        /// <returns></returns>
        protected override bool subsetMatchesPattern(List<Candlestick> subset)
        {
            double prevOpen = subset[0].Open;
            double prevClose = subset[0].Close;
            if (subset[0].isBearish && subset[1].isBullish && subset[1].Close > prevOpen && subset[1].Open < prevClose)
            {
                return true;
            }
            return false;
        }
    }


    internal class Recognizer_BearishEngulfing : Recognizer
    {
        /// <summary>
        /// Calls the base class constructor
        /// </summary>
        public Recognizer_BearishEngulfing() : base("Bearish Engulfing", 2) { }


        /// <summary>
        /// Implements the abstract subsetMatchesPattern method defined in the Recognizer base class. Returns true if the candlestick subset is a Bearish Engulfing
        /// </summary>
        /// <param name="subset"></param>
        /// <returns></returns>
        protected override bool subsetMatchesPattern(List<Candlestick> subset)
        {
            double prevOpen = subset[0].Open;
            double prevClose = subset[0].Close;
            if (subset[0].isBullish && subset[1].isBearish && subset[1].Open > prevClose && subset[1].Close < prevOpen)
            {
                return true;
            }
            return false;
        }
    }
}
