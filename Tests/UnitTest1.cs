using System.Collections;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //Обработка пустого текста
        [Test]
        public void Test1()
        {
            var words = dictionary.ExtractWords("");
            Assert.That(words.Count(), Is.EqualTo(0));
        }//Test1

        //Обработка пустой папки
        [Test]
        public void Test2()
        {
            Assert.Throws<FileNotFoundException>(() => dictionary.Create("C:\\Новая папка"));
        }//Test2

        //Обработка несуществующей папки
        [Test]
        public void Test3()
        {
            Assert.Throws<DirectoryNotFoundException>(() => dictionary.Create("C:\\Новая папка_2"));
        }//Test3

    }
}
