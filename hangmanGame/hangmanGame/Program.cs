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
            string wordBankInitial = "Created empty file";
            string wordBankAddition = "New line to be added";
            CreateTextFile(wordBankPath, wordBankInitial);
            string[] wordBankContents = ReadTextFile(wordBankPath);
            Console.WriteLine("Word bank currently contains: ");
            for(int i = 0; i < wordBankContents.Length; i++)
            {
                Console.WriteLine(wordBankContents[i]);
            }
            AppendToTextFile(wordBankPath, wordBankAddition);
            wordBankContents = ReadTextFile(wordBankPath);
            Console.WriteLine("Word bank currently contains: ");
            for (int i = 0; i < wordBankContents.Length; i++)
            {
                Console.WriteLine(wordBankContents[i]);
            }
            DeleteTextFile(wordBankPath);
        }

        public static int Test()
        {
            return 1;
        }

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
            catch(IOException e)
            {
                Console.WriteLine("IOException occurred: " + e);
                throw;
            }
            catch(NotSupportedException e)
            {
                Console.WriteLine("NotSupportedException occurred: " + e);
                throw;
            }
            catch(Exception e)
            {
                Console.WriteLine("Generic exception occurred: " + e);
                throw;
            }
        }

        public static string[] ReadTextFile(string target)
        {
            string[] readLines;
            try
            {
                readLines = File.ReadAllLines(target);
            }
            catch (IOException e)
            {
                Console.WriteLine("IOException occurred: " + e);
                throw;
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("NotSupportedException occurred: " + e);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic exception occurred: " + e);
                throw;
            }
            return readLines;
        }

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
                throw;
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("NotSupportedException occurred: " + e);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic exception occurred: " + e);
                throw;
            }
        }

        public static void DeleteTextFile(string target)
        {
            try
            {
                File.Delete(target);
            }
            catch (IOException e)
            {
                Console.WriteLine("IOException occurred: " + e);
                throw;
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("NotSupportedException occurred: " + e);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic exception occurred: " + e);
                throw;
            }
        }
    }
}
