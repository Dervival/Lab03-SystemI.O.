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
    }
}
