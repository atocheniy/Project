using Project.Models;
using System.Collections;
using System.Collections.Generic;
using Project.Services;

namespace Tests
{
    public class Tests
    {
        private FrequencyService _frequencyService = new FrequencyService();

        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void Test1()
        {
            var text = "";

            var res = _frequencyService.Analyze(text);
            Assert.That(res.Count, Is.EqualTo(0));
        }//Test1


        [Test]
        public void Test2()
        {
            var text = "hello world";

            var res = _frequencyService.Analyze(text);
            Assert.That(res.Count, Is.EqualTo(2));
            Assert.That(res[0].Word, Is.EqualTo("hello"));
            Assert.That(res[0].Count, Is.EqualTo(1));
            Assert.That(res[1].Word, Is.EqualTo("world"));
            Assert.That(res[1].Count, Is.EqualTo(1));
        }//Test2



        [Test]
        public void Test3()
        {
            var text = "hello world hello HELLO";

            var res = _frequencyService.Analyze(text);
            Assert.That(res.Count, Is.EqualTo(2));
            Assert.That(res[0].Count, Is.EqualTo(3));
            Assert.That(res[1].Count, Is.EqualTo(1));
        }//Test3

    }
}
