using BrainFuck;

namespace Tests;

[TestFixture]
    public class BrainLuckTest
    {        
        (string actual, string code, string input)[] data ;

        [SetUp]
        public void Setup()
        {
            data = new (string actual, string code, string input)[] {
                ("Hello World!", "++++++++++[>+++++++>++++++++++>+++>+<<<<-]>++.>+.+++++++..+++.>++.<<+++++++++++++++.>.+++.------.--------.>+.", ""),
                (char.ConvertFromUtf32(72), ",>,<[>[->+>+<<]>>[-<<+>>]<<<-]>>.", char.ConvertFromUtf32(8)+char.ConvertFromUtf32(9)),
                ("Codewars", ",[.[-],]","Codewars"+char.ConvertFromUtf32(0)),
                ("Codewars", ",+[-.,+]","Codewars"+char.ConvertFromUtf32(255))
            };
        }

        [Test]
        public void SampleTest()
        {
            foreach ((string actual, string code, string input) item in data){
                string expected = Kata.BrainLuck(item.code, item.input);
                Assert.That(expected, Is.EqualTo(item.actual));
            }
        }
    }