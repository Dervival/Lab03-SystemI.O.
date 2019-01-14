using System;
using System.IO;

namespace hangmanGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            //By default, looks for a wordbank.txt file in the same folder as Program.cs. If it doesn't exist, create it with default words.
            string wordBankPath = "../../../wordbank.txt";
            string[] wordBankInitial = new string[] { "hello", "kitten", "example", "ghoti" };
            if (!File.Exists(wordBankPath))
            {
                CreateTextFile(wordBankPath, wordBankInitial);
            }
            RunHangmanMenu(wordBankPath);
        }

        /// <summary>
        /// Basic menu for deciding between playing the game, viewing/updating the word bank, and exiting the program.
        /// </summary>
        /// <param name="wordBankPath">The path to the word bank that the hangman game uses.</param>
        static void RunHangmanMenu(string wordBankPath)
        {
            Console.WriteLine("Welcome to my word guessing game.");
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("\nPlease select a menu option");
                Console.WriteLine("1. Play game");
                Console.WriteLine("2. View, add, or remove words");
                Console.WriteLine("3. Exit game");
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        RunHangmanGame(SelectRandomWordFromBank(wordBankPath));
                        break;
                    case "2":
                        SelectWordBankUpdateAction(wordBankPath);
                        break;
                    case "3":
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Sorry, I didn't understand that input. Please try again.");
                        break;
                }
            }
            
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
            Console.WriteLine("\nWhat word would you like to add? Only the first word entered will be added.");
            string userInput = Console.ReadLine();
            string[] inputArray = userInput.Split(" ");
            AppendToTextFile(target, inputArray[0]);
        }

        /// <summary>
        /// Allows a user to choose a word to delete by index.
        /// </summary>
        /// <param name="target">Path to the file containing the word bank.</param>
        /// <param name="wordBankContents">Contents of the word bank contained at target path.</param>
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

        /// <summary>
        /// Removes a word from the word bank based on the index selected.
        /// </summary>
        /// <param name="target">Path to the file being modified.</param>
        /// <param name="wordBankContents">Contents of the word bank to delete the word from.</param>
        /// <param name="index">Index of word to be deleted.</param>
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

        /// <summary>
        /// Given a wordbank, selects a random word and returns it. 
        /// </summary>
        /// <param name="path">Path to the word bank file.</param>
        /// <returns>A random word from the word bank.</returns>
        public static string SelectRandomWordFromBank(string path)
        {
            string[] wordBankContents = ReadTextFile(path);
            //Random class documentation - https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netframework-4.7.2
            Random rand = new Random();
            int randomIndex = rand.Next(wordBankContents.Length);
            return wordBankContents[randomIndex];
        }

        /// <summary>
        /// Runs the hangman game, using a random word (returned from SelectRandomWordFromBank)
        /// If reworked, break this into multiple methods; this is really hacked together
        /// </summary>
        /// <param name="actualWord">A word picked from the word bank/param>
        public static void RunHangmanGame(string actualWord)
        {
            bool[] charExistsInWord = new bool[26];
            char[] charArray = actualWord.ToLower().ToCharArray();
            for(int i = 0; i < charArray.Length; i++)
            {
                int intChar = Convert.ToInt32(charArray[i]);
                //ASCII 'a' converts to 97, 'z' to 122
                if(intChar > 96 && intChar < 123)
                {
                    charExistsInWord[intChar - 97] = true;
                }
            }
            bool[] charGuessed = new bool[26];
            int guessesRemaining = 26;
            while (guessesRemaining > 0)
            {
                Console.WriteLine("You have " + guessesRemaining + " guesses remaining");
                bool validGuess = false;
                while (!validGuess)
                {
                    Console.WriteLine("Current state of word being guessed");
                    for (int i = 0; i < charArray.Length; i++)
                    {
                        int intChar = Convert.ToInt32(charArray[i]) - 97;
                        if (intChar > -1 && intChar < 26)
                        {
                            if (charGuessed[intChar])
                            {
                                Console.Write(charArray[i]);
                            }
                            else
                            {
                                Console.Write("_");
                            }
                        }
                    }
                    Console.Write("\nPreviously guessed letters: ");
                    for (int i = 0; i < charGuessed.Length; i++)
                    {
                        if (charGuessed[i] == true)
                        {
                            Console.Write(Convert.ToChar(i + 97) + ", ");
                        }
                    }
                    Console.WriteLine("\nGuess a letter");
                    string userInput = Console.ReadLine();
                    int userChoiceInt;
                    if (userInput == "")
                    {
                        userChoiceInt = -1;
                    }
                    else
                    { 
                        char userChoice = userInput.ToCharArray()[0];
                        userChoiceInt = Convert.ToInt32(userChoice) - 97;
                    }
                    if (userChoiceInt > -1 && userChoiceInt < 26)
                    {
                        charGuessed[userChoiceInt] = true;
                        validGuess = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input - try again\n");
                    }
                }
                guessesRemaining--;
                bool wordComplete = true;
                for(int i = 0; i < charExistsInWord.Length; i++)
                {
                    //Console.WriteLine("At index " + i + ", charExistsInWord is " + charExistsInWord[i] + " and charGuessed is " + charGuessed[i]);
                    if(charExistsInWord[i] == true && charGuessed[i] == false)
                    {
                        wordComplete = false;
                    }
                }
                if (wordComplete)
                {
                    Console.WriteLine("Congratulations you win");
                    return;
                }
            }
        }
    }
}
