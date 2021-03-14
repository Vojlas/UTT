using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniversalTimerTool.Model;
using System.Collections.Generic;
using System;

namespace UniversalTimerToolTests.ModelTesting
{
    [TestClass]
    public class TimeModelTest
    {
        [TestMethod]
        public void TestTimeAddSecFunction()
        {
            Dictionary<Time, Time> testingPairs = new Dictionary<Time, Time>();
            testingPairs.Add(new Time(0, 0, 0), new Time(0, 0, 1));
            testingPairs.Add(new Time(0, 0, 59), new Time(0, 1, 0));
            testingPairs.Add(new Time(0, 59, 0), new Time(0, 59, 1)); 
            testingPairs.Add(new Time(0, 59, 59), new Time(1, 0, 0));
            testingPairs.Add(new Time(0, 59, 1), new Time(0, 59, 2));
            testingPairs.Add(new Time(59, 2, 59), new Time(59, 3, 0));

            foreach (KeyValuePair<Time,Time> pair in testingPairs)
            {
                pair.Key.AddSec();

                //Help for debugging ;)
                //System.Threading.Thread.Sleep(100);

                Assert.AreEqual(pair.Key.Hours, pair.Value.Hours);
                Assert.AreEqual(pair.Key.Minutes, pair.Value.Minutes);
                Assert.AreEqual(pair.Key.Seconds, pair.Value.Seconds);
            }
        }

        [TestMethod]
        public void TestTimeAddFunction()
        {            
            Dictionary<expresion, Time> testingPairs = new Dictionary<expresion, Time>();
            testingPairs.Add(new expresion(new Time(0, 0, 0), new Time(0, 0, 0)), new Time(0, 0, 0));
            testingPairs.Add(new expresion(new Time(0, 0, 0), new Time(0, 0, 1)), new Time(0, 0, 1));
            testingPairs.Add(new expresion(new Time(0, 0, 1), new Time(0, 0, 0)), new Time(0, 0, 1));
            testingPairs.Add(new expresion(new Time(0, 1, 0), new Time(0, 1, 0)), new Time(0, 2, 0));
            testingPairs.Add(new expresion(new Time(0, 0, 0), new Time(1, 0, 0)), new Time(1, 0, 0));
            testingPairs.Add(new expresion(new Time(0, 0, 59), new Time(0, 0, 0)), new Time(0, 0, 59));
            testingPairs.Add(new expresion(new Time(0, 0, 59), new Time(0, 0, 1)), new Time(0, 1, 0));
            testingPairs.Add(new expresion(new Time(0, 0, 59), new Time(0, 0, 6)), new Time(0, 1, 5));
            testingPairs.Add(new expresion(new Time(0, 59, 40), new Time(0, 3, 4)), new Time(1, 2, 44));
            testingPairs.Add(new expresion(new Time(0, 59, 59), new Time(0, 59, 59)), new Time(1, 59, 58));

            foreach (KeyValuePair<expresion, Time> pair in testingPairs)
            {
                Time addition;
                addition = pair.Key.timeA.Add(pair.Key.timeB);

                //Help for debugging ;)
                //System.Threading.Thread.Sleep(100);

                Assert.AreEqual(addition.Hours, pair.Value.Hours);
                Assert.AreEqual(addition.Minutes, pair.Value.Minutes);
                Assert.AreEqual(addition.Seconds, pair.Value.Seconds);
            }
        }

        [Ignore]
        public class expresion
        {
            public Time timeA { get; set; }
            public Time timeB { get; set; }
            public expresion(Time A, Time B)
            {
                this.timeA = A;
                this.timeB = B;
            }
        }
    }
}
