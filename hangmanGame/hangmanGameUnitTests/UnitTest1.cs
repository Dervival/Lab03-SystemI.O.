using System;
using Xunit;
using hangmanGame;

namespace hangmanGameUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void EnsureReadTextFileReadsFile()
        {
            //arrange
            string testPath = "test.txt";
            //act
            Program.CreateTextFile(testPath, testPath);
            string[] testContents = Program.ReadTextFile(testPath);
            //assert
            Assert.True(testContents.Length > 0);
            //cleanup
            Program.DeleteTextFile(testPath);
        }
        [Fact]
        public void EnsureCreateCanTakeInStringArray()
        {
            //arrange
            string testPath = "test.txt";
            string[] testText = new string[] { "test1", "test2", "test3" };
            //act
            Program.CreateTextFile(testPath, testText);
            string[] testContents = Program.ReadTextFile(testPath);
            //assert
            Assert.Equal(3, testContents.Length);
        }
        [Fact]
        public void EnsureAppendToTextFileAppendsToFile()
        {
            //arrange
            string testPath = "test.txt";
            //act
            Program.CreateTextFile(testPath, testPath);
            string[] testContents = Program.ReadTextFile(testPath);
            Program.AppendToTextFile(testPath, "new line");
            string[] testContents2 = Program.ReadTextFile(testPath); 
            //assert
            Assert.True(testContents2.Length > testContents.Length);
        }
        [Fact]
        public void EnsureAppendToTextFileAppendsMultipleLines()
        {
            //arrange
            string testPath = "test.txt";
            string[] testText = new string[] { "test1", "test2", "test3" };
            //act
            Program.CreateTextFile(testPath, testPath);
            Program.AppendToTextFile(testPath, testText);
            string[] testContents = Program.ReadTextFile(testPath);
            //assert
            Assert.Equal(4, testContents.Length);
        }
        [Fact]
        public void EnsureAppendToTextFileDoesNotModifyPreviousContent()
        {
            //arrange
            string testPath = "test.txt";
            //act
            Program.CreateTextFile(testPath, testPath);
            string[] testContents = Program.ReadTextFile(testPath);
            Program.AppendToTextFile(testPath, "new line");
            string[] testContents2 = Program.ReadTextFile(testPath);
            //assert
            for(int i = 0; i < testContents.Length; i++)
            {
                Assert.Equal(testContents[i], testContents2[i]);
            }
        }
        [Fact]
        public void EnsureRemoveWordReducesWordBankCount()
        {
            //arrange
            string testPath = "test.txt";
            string[] testText = new string[] { "test1", "test2", "test3" };
            //act
            Program.CreateTextFile(testPath, testText);
            string[] testContents = Program.ReadTextFile(testPath);
            Program.RemoveWordFromBank(testPath, testContents, 0);
            string[] testContents2 = Program.ReadTextFile(testPath);
            //assert
            Assert.Equal(testContents.Length -1 , testContents2.Length);
        }
        [Fact]
        public void EnsureRemoveWordRemovesWordChosenByIndex()
        {
            //arrange
            string testPath = "test.txt";
            string[] testText = new string[] { "test1", "test2", "test3" };
            //act
            Program.CreateTextFile(testPath, testText);
            string[] testContents = Program.ReadTextFile(testPath);
            Program.RemoveWordFromBank(testPath, testContents, 0);
            testContents = Program.ReadTextFile(testPath);
            //assert
            Assert.Equal("test2", testContents[0]);
        }
    }
}
