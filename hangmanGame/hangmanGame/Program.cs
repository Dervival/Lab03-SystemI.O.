using System;
using System.IO;

namespace hangmanGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //basic CRUD tests through the terminal - create the word bank wordbank.txt, read the initial contents, add a new line, read the new state of contents, delete the word bank
            string wordBankPath = "../../../wordbank.txt";
            string[] wordBankInitial = new string[] { "Created empty file", "Hello world", "Goodbye" };
            string wordBankAddition = "New line to be added";
            CreateTextFile(wordBankPath, wordBankInitial);
            string[] wordBankContents = ReadTextFile(wordBankPath);
            Console.WriteLine("Word bank currently contains: ");
            for(int i = 0; i < wordBankContents.Length; i++)
            {
                Console.WriteLine(wordBankContents[i]);
            }
            SelectWordBankUpdateAction(wordBankPath);
            //AppendToTextFile(wordBankPath, wordBankAddition);
            //wordBankContents = ReadTextFile(wordBankPath);
            //Console.WriteLine("Word bank currently contains: ");
            //for (int i = 0; i < wordBankContents.Length; i++)
            //{
            //    Console.WriteLine(wordBankContents[i]);
            //}
            Console.WriteLine(SelectRandomWordFromBank(wordBankPath));
            Console.WriteLine(SelectRandomWordFromBank(wordBankPath));
            Console.WriteLine(SelectRandomWordFromBank(wordBankPath));
            Console.WriteLine(SelectRandomWordFromBank(wordBankPath));
        }

        /// <summary>
        /// Creates a new text file containing the content of text.
        /// </summary>
        /// <param name="target">Path to file being created.</param>
        /// <param name="words">String to write to the new text file.</param>
        public static void CreateTextFile(string target, string text)
        {
            //Multiple ways stream I/O can throw exceptions - most common will be IOException or NotSupportedExceptions
            try
            {
                //using code block will close the file automatically when the code block is exited
                using (StreamWriter streamWriter = new StreamWriter(target))
                {
                    try
                    {
                        streamWriter.WriteLine(text);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("IOException occurred: " + e);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("NotSupportedException occurred: " + e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic exception occurred: " + e);
            }
        }

        /// <summary>
        /// Creates a new text file that contains the content contained in words - each value in words will be written on a separate line.
        /// </summary>
        /// <param name="target">Path to file being created.</param>
        /// <param name="words">Array of strings to write to the new text file.</param>
        public static void CreateTextFile(string target, string[] words)
        {
            if (words.Length > 0)
            {
                CreateTextFile(target, words[0]);
                if (words.Length > 1)
                {
                    for (int i = 1; i < words.Length; i++)
                    {
                        AppendToTextFile(target, words[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Takes in a text file, translates its contents into an array of strings, and returns the array. If an exception occurs, returns an empty array.
        /// </summary>
        /// <param name="target">Path to the file being read.</param>
        /// <returns></returns>
        public static string[] ReadTextFile(string target)
        {
            string[] readLines = new string[] { };
            try
            {
                readLines = File.ReadAllLines(target);
            }
            catch (IOException e)
            {
                Console.WriteLine("IOException occurred: " + e);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("NotSupportedException occurred: " + e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic exception occurred: " + e);
            }
            return readLines;
        }

        /// <summary>
        /// Appends the given string text to the targeted file.
        /// </summary>
        /// <param name="target">Path to targeted file.</param>
        /// <param name="text">Text to append to file.</param>
        public static void AppendToTextFile(string target, string text)
        {
            try
            {
                using (StreamWriter streamWriter = File.AppendText(target))
                {
                    streamWriter.WriteLine(text);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("IOException occurred: " + e);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("NotSupportedException occurred: " + e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic exception occurred: " + e);
            }
        }

        /// <summary>
        /// Appends the given array of strings to the targeted file, line by line.
        /// </summary>
        /// <param name="target">Path to targeted file.</param>
        /// <param name="text">Text to append to file.</param>
        public static void AppendToTextFile(string target, string[] lines)
        {
            for(int i = 0; i < lines.Length; i++)
            {
                AppendToTextFile(target, lines[i]);
            }
        }

        /// <summary>
        /// Deletes the targeted file.
        /// </summary>
        /// <param name="target">Path to file to be deleted.</param>
        public static void DeleteTextFile(string target)
        {
            try
            {
                File.Delete(target);
            }
            catch (IOException e)
            {
                Console.WriteLine("IOException occurred: " + e);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("NotSupportedException occurred: " + e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic exception occurred: " + e);
            }
        }

        /// <summary>
        /// Shows the user the current state of the word bank and lets them add or remove words.
        /// </summary>
        /// <param name="target">Path to the file containing the word bank.</param>
        public static void SelectWordBankUpdateAction(string target)
        {
            string[] fileContents = ReadTextFile(target);
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nCurrent bank of words: ");
                for (int i = 0; i < fileContents.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + fileContents[i]);
                }
                Console.WriteLine("\nChoose an option from below: ");
                Console.WriteLine("#1: Add a word");
                Console.WriteLine("#2: Delete a word");
                Console.WriteLine("#3: Exit menu");
                string userInput = Console.ReadLine();
                int userChoice = 0;
                try
                {
                    Int32.TryParse(userInput, out userChoice);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Generic exception occurred: " + e);
                }
                switch (userChoice)
                {
                    case 1:
                        AddWordToWordBank(target);
                        fileContents = ReadTextFile(target);
                        break;
                    case 2:
                        ChooseWordForRemoval(target, fileContents);
                        fileContents = ReadTextFile(target);
                        break;
                    case 3:
                        exit = true;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Adds a word to the word bank based on user input.
        /// </summary>
        /// <param name="target">Path to the file containing the word bank.</param>
        public static void AddWordToWordBank(string target)
        {
            Console.WriteLine("\nWhat word would you like to add?");
            string userInput = Console.ReadLine();
            AppendToTextFile(target, userInput);
        }

        public static void ChooseWordForRemoval(string target, string[] wordBankContents)
        {
            int numberofWords = wordBankContents.Length;
            if(numberofWords < 2)
            {
                Console.WriteLine("The word bank cannot be empty. Please try adding new words before deleting any.");
                return;
            }
            Console.WriteLine("\nWhat word number would you like to delete?");
            
            string userInput = Console.ReadLine();
            int userChoice = 0;
            try
            {
                Int32.TryParse(userInput, out userChoice);
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic exception occurred: " + e);
            }
            if(userChoice > numberofWords || userChoice < 1)
            {
                Console.WriteLine("That number does not correspond to a word in the word bank. Returning to previous menu.");
                return;
            }
            else
            {
                RemoveWordFromBank(target, wordBankContents, userChoice - 1);
                return;
            }
        }

        public static void RemoveWordFromBank(string target, string[] wordBankContents, int index)
        {
            int numberofWords = wordBankContents.Length;
            string[] newWordBank = new string[numberofWords - 1];
            for (int i = 0; i < index; i++)
            {
                newWordBank[i] = wordBankContents[i];
            }
            for (int i = index + 1; i < numberofWords; i++)
            {
                newWordBank[i - 1] = wordBankContents[i];
            }
            CreateTextFile(target, newWordBank);
            return;
        }

        public static string SelectRandomWordFromBank(string path)
        {
            string[] wordBankContents = ReadTextFile(path);
            //Random class documentation - https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netframework-4.7.2
            Random rand = new Random();
            int randomIndex = rand.Next(wordBankContents.Length);
            return wordBankContents[randomIndex];
        }
    }
}
