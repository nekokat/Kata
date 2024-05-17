using BrainFuck;

namespace Tests;

[TestFixture]
    public class BrainLuckTest

    {        
        [SetUp]
        public void Setup(){}
        

        [TestCase("Codewars", ",+[-.,+]", "Codewarsÿ")]
        [TestCase("Codewars", ",[.[-],]","Codewars")]
        [TestCase("H", ",>,<[>[->+>+<<]>>[-<<+>>]<<<-]>>.","H")]
        public void SampleTest(string actual, string code, string input)
        {
            string expected = Kata.BrainLuck(code, input);
            Assert.That(expected, Is.EqualTo(actual));
        }
    }