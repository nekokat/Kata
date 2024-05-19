using BrainFuck;

namespace Tests;

[TestFixture]
    public class BrainLuckTest
    {        
        [SetUp]
        public void Setup(){}

        [TestCase("Hello World!", "++++++++++[>+++++++>++++++++++>+++>+<<<<-]>++.>+.+++++++..+++.>++.<<+++++++++++++++.>.+++.------.--------.>+.", "")]
        [TestCase("H", ",>,<[>[->+>+<<]>>[-<<+>>]<<<-]>>.","\x0008\x0009")]
        [TestCase("Codewars", ",[.[-],]","Codewars\x0000")]
        [TestCase("Codewars", ",+[-.,+]", "Codewars\x0597")]
        public static void SampleTest(string actual, string code, string input)
        {
            string expected = Kata.BrainLuck(code, input);
            Assert.That(expected, Is.EqualTo(actual));
        }
    }