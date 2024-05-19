using BrainFuck;

namespace Tests;

[TestFixture]
    public class BrainLuckTest

    {        
        [SetUp]
        public void Setup(){}        

        [TestCase("Hello World!", "++++++++++[>+++++++>++++++++++>+++>+<<<<-]>++.>+.+++++++..+++.>++.<<+++++++++++++++.>.+++.------.--------.>+.", "")]
        [TestCase("\u0048", ",>,<[>[->+>+<<]>>[-<<+>>]<<<-]>>.","\u0008\u0009")]
        //[TestCase("Codewars", ",+[-.,+]", "Codewars\u00FF")]
        //[TestCase("Codewars", ",[.[-],]","Codewars\u0000")]
        public static void SampleTest(string actual, string code, string input)
        {
            string expected = Kata.BrainLuck(code, input);
            Assert.That(expected, Is.EqualTo(actual));
        }
    }