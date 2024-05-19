using BrainFuck;

namespace Tests;

[TestFixture]
    public class BrainLuckTest

    {        
        [SetUp]
        public void Setup(){}        

        [TestCase("Codewars", ",+[-.,+]", "Codewars\u00ff")]
        [TestCase("Codewars", ",[.[-],]","Codewars\u0000")]
        [TestCase("\u0072", ",>,<[>[->+>+<<]>>[-<<+>>]<<<-]>>.","\u0008\u0009")]
        public static void SampleTest(string actual, string code, string input)
        {
            string expected = Kata.BrainLuck(code, input);
            Assert.That(expected, Is.EqualTo(actual));
        }
    }