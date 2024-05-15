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
        public void SampleTest(string actiual, string code, string input)
        {
            Assert.That(actiual, Is.EqualTo(Kata.BrainLuck(code, input)));
        }
    }