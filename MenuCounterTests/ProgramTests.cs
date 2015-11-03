using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MenuCounter.Data_Contracts;

namespace MenuCounter.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadFileFromPathTest_Null()
        {
            //Arrange
            string filePath = null;

            //Act
            var actual = Program.LoadFileFromPath(filePath);

            //Assert
            IEnumerable<MenuRoot> expected = null;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void LoadFileFromPathTest_Empty()
        {
            //Arrange
            string filePath = string.Empty;

            //Act
            var actual = Program.LoadFileFromPath(filePath);

            //Assert
            IEnumerable<MenuRoot> expected = null;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void LoadFileFromPathTest_MalformedPath()
        {
            //Arrange
            string filePath = @"c!<>|:";

            //Act
            var actual = Program.LoadFileFromPath(filePath);

            //Assert
            IEnumerable<MenuRoot> expected = null;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LoadFileFromPathTest_FileNotFound()
        {
            //Arrange
            string filePath = "fakefile.txt";

            //Act
            var actual = Program.LoadFileFromPath(filePath);

            //Assert
            IEnumerable<MenuRoot> expected = null;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(SerializationException))]
        public void LoadFileFromPathTest_MalformedFile()
        {
            //Arrange
            string filePath = "badfile.txt";

            //Act
            var actual = Program.LoadFileFromPath(filePath);

            //Assert
            IEnumerable<MenuRoot> expected = null;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LoadFileFromPathTest_ExampleFile()
        {
            //Arrange
            string filePath = "example.txt";

            //Act
            var actual = Program.LoadFileFromPath(filePath);

            //Assert
            var expectedCount = 3;
            Assert.AreEqual(expectedCount, actual.Count());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateSumsTest_NullMenu()
        {
            //Arrange
            IEnumerable<MenuRoot> fileContents = null;

            //Act
            var actual = Program.CalculateSums(fileContents);

            //Assert
            IEnumerable<int> expected = null;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CalculateSumsTest_EmptyMenu()
        {
            //Arrange
            IEnumerable<MenuRoot> fileContents = new List<MenuRoot>();

            //Act
            var actual = Program.CalculateSums(fileContents);

            //Assert
            Assert.IsTrue(!actual.Any());
        }

        [TestMethod()]
        public void CalculateSumsTest_SingleMenuNullItems()
        {
            //Arrange
            List<Item> testItems = null;
            MenuNode testMenuNode = new MenuNode() {Items = testItems};
            MenuRoot testMenuRoot = new MenuRoot {Menu = testMenuNode};
            List<MenuRoot> fileContents = new List<MenuRoot> {testMenuRoot};

            //Act
            var actual = Program.CalculateSums(fileContents);

            //Assert
            Assert.IsTrue(!actual.Any());
        }

        [TestMethod()]
        public void CalculateSumsTest_SingleMenuEmptyItem()
        {
            //Arrange
            List<Item> testItems = new List<Item>();
            MenuNode testMenuNode = new MenuNode() {Items = testItems};
            MenuRoot testMenuRoot = new MenuRoot {Menu = testMenuNode};
            List<MenuRoot> fileContents = new List<MenuRoot> {testMenuRoot};

            //Act
            var actual = Program.CalculateSums(fileContents);

            //Assert
            var expected = 0;
            var expectedCount = 1;
            Assert.AreEqual(expectedCount, actual.Count());
            Assert.AreEqual(expected, actual.ElementAt(0));
        }

        [TestMethod()]
        public void CalculateSumsTest_SingleMenuSingleItemNoLabel()
        {
            //Arrange
            Item testItem = new Item() { ID = 5 };
            List<Item> testItems = new List<Item>() { testItem };
            MenuNode testMenuNode = new MenuNode() { Items = testItems };
            MenuRoot testMenuRoot = new MenuRoot { Menu = testMenuNode };
            List<MenuRoot> fileContents = new List<MenuRoot> { testMenuRoot };

            //Act
            var actual = Program.CalculateSums(fileContents);

            //Assert
            var expected = 0;
            var expectedCount = 1;
            Assert.AreEqual(expectedCount, actual.Count());
            Assert.AreEqual(expected, actual.ElementAt(0));
        }

        [TestMethod()]
        public void CalculateSumsTest_SingleMenuSingleItemLabel()
        {
            //Arrange
            Item testItem = new Item() {ID = 5, Label = "test"};
            List<Item> testItems = new List<Item>() {testItem};
            MenuNode testMenuNode = new MenuNode() {Items = testItems};
            MenuRoot testMenuRoot = new MenuRoot {Menu = testMenuNode};
            List<MenuRoot> fileContents = new List<MenuRoot> {testMenuRoot};

            //Act
            var actual = Program.CalculateSums(fileContents);

            //Assert
            var expected = 5;
            var expectedCount = 1;
            Assert.AreEqual(expectedCount, actual.Count());
            Assert.AreEqual(expected, actual.ElementAt(0));
        }

        [TestMethod()]
        public void CalculateSumsTest_SingleMenuMultiItemNoLabel()
        {
            //Arrange
            Item testItem = new Item() {ID = 5};
            Item testItem2 = new Item() {ID = 10};
            Item testItem3 = new Item() {ID = 15};
            List<Item> testItems = new List<Item>() {testItem, testItem2, testItem3};
            MenuNode testMenuNode = new MenuNode() {Items = testItems};
            MenuRoot testMenuRoot = new MenuRoot {Menu = testMenuNode};
            List<MenuRoot> fileContents = new List<MenuRoot> {testMenuRoot};

            //Act
            var actual = Program.CalculateSums(fileContents);

            //Assert
            var expected = 0;
            var expectedCount = 1;
            Assert.AreEqual(expectedCount, actual.Count());
            Assert.AreEqual(expected, actual.ElementAt(0));
        }

        [TestMethod()]
        public void CalculateSumsTest_SingleMenuMultiItemLabel()
        {
            //Arrange
            Item testItem = new Item() {ID = 5, Label = "test"};
            Item testItem2 = new Item() {ID = 10, Label = "test2"};
            Item testItem3 = new Item() {ID = 15, Label = "test3"};
            List<Item> testItems = new List<Item>() {testItem, testItem2, testItem3};
            MenuNode testMenuNode = new MenuNode() {Items = testItems};
            MenuRoot testMenuRoot = new MenuRoot {Menu = testMenuNode};
            List<MenuRoot> fileContents = new List<MenuRoot> {testMenuRoot};

            //Act
            var actual = Program.CalculateSums(fileContents);

            //Assert
            var expected = 30;
            var expectedCount = 1;
            Assert.AreEqual(expectedCount, actual.Count());
            Assert.AreEqual(expected, actual.ElementAt(0));
        }

        [TestMethod()]
        public void CalculateSumsTest_SingleMenuMultiItemSomeLabel()
        {
            //Arrange
            Item testItem = new Item() {ID = 5, Label = "test"};
            Item testItem2 = new Item() {ID = 10};
            Item testItem3 = new Item() {ID = 15, Label = "test3"};
            List<Item> testItems = new List<Item>() {testItem, testItem2, testItem3};
            MenuNode testMenuNode = new MenuNode() {Items = testItems};
            MenuRoot testMenuRoot = new MenuRoot {Menu = testMenuNode};
            List<MenuRoot> fileContents = new List<MenuRoot> {testMenuRoot};

            //Act
            var actual = Program.CalculateSums(fileContents);

            //Assert
            var expected = 20;
            var expectedCount = 1;
            Assert.AreEqual(expectedCount, actual.Count());
            Assert.AreEqual(expected, actual.ElementAt(0));
        }

        [TestMethod()]
        public void CalculateSumsTest_MultiMenuMultiItemSomeLabel()
        {
            //Arrange
            Item testItem = new Item() {ID = 5, Label = "test"};
            List<Item> testItems = new List<Item>() {testItem};
            MenuNode testMenuNode = new MenuNode() {Items = testItems};
            MenuRoot testMenuRoot = new MenuRoot {Menu = testMenuNode};
            Item testItem2 = new Item() {ID = 10};
            Item testItem3 = new Item() {ID = 15, Label = "test3"};
            List<Item> testItems2 = new List<Item>() {testItem2, testItem3};
            MenuNode testMenuNode2 = new MenuNode() {Items = testItems2};
            MenuRoot testMenuRoot2 = new MenuRoot {Menu = testMenuNode2};
            List<MenuRoot> fileContents = new List<MenuRoot> {testMenuRoot, testMenuRoot2};

            //Act
            var actual = Program.CalculateSums(fileContents);

            //Assert
            var expected = 5;
            var expected2 = 15;
            var expectedCount = 2;
            Assert.AreEqual(expectedCount, actual.Count());
            Assert.AreEqual(expected, actual.ElementAt(0));
            Assert.AreEqual(expected2, actual.ElementAt(1));
        }

        [TestMethod()]
        public void PrintResultsTest_Null()
        {
            using (var stringWriter = new StringWriter())
            {
                //Arrange
                Console.SetOut(stringWriter);
                IEnumerable<int> idSums = null;

                //Act
                Program.PrintResults(idSums);

                //Assert
                string expected = $"An unforeseen error occurred. Your results could not be calculated.{Environment.NewLine}";
                Assert.AreEqual<string>(expected, stringWriter.ToString());

                //Clean up
                var standardOut = new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = true};
                Console.SetOut(standardOut);
            }
        }

        [TestMethod()]
        public void PrintResultsTest_Empty()
        {
            using (var stringWriter = new StringWriter())
            {
                //Arrange
                Console.SetOut(stringWriter);
                IEnumerable<int> idSums = new List<int>();

                //Act
                Program.PrintResults(idSums);

                //Assert
                string expected = $"No countable items were found. The sum of nothing is 0.{Environment.NewLine}";
                Assert.AreEqual<string>(expected, stringWriter.ToString());

                //Clean up
                var standardOut = new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = true};
                Console.SetOut(standardOut);
            }
        }

        [TestMethod()]
        public void PrintResultsTest_Single()
        {
            using (var stringWriter = new StringWriter())
            {
                //Arrange
                Console.SetOut(stringWriter);
                IEnumerable<int> idSums = new List<int> {15};

                //Act
                Program.PrintResults(idSums);

                //Assert
                string expected = $"15{Environment.NewLine}";
                Assert.AreEqual<string>(expected, stringWriter.ToString());

                //Clean up
                var standardOut = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
                Console.SetOut(standardOut);
            }
        }

        [TestMethod()]
        public void PrintResultsTest_Multi()
        {
            using (var stringWriter = new StringWriter())
            {
                //Arrange
                Console.SetOut(stringWriter);
                IEnumerable<int> idSums = new List<int> { 5, 10, 15 };

                //Act
                Program.PrintResults(idSums);

                //Assert
                string expected = $"5{Environment.NewLine}10{Environment.NewLine}15{Environment.NewLine}";
                Assert.AreEqual<string>(expected, stringWriter.ToString());

                //Clean up
                var standardOut = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
                Console.SetOut(standardOut);
            }
        }
    }
}