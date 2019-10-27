﻿using System;
using System.IO;
using System.Security;

namespace Crypt
{
    class Program
    {
        
        static readonly Random Random = new Random();

        /// <summary>
        /// Method for reading int value
        /// </summary>
        /// <returns>Read int value</returns>
        static int Read()
        {
            int n;
            bool flag = false;
            do
            {
                if(flag)
                    Console.WriteLine("Input was incorrect, try again");
                flag = true;
            } while (!int.TryParse(Console.ReadLine(), out n));

            return n;
        }
        
        /// <summary>
        /// Method for reading file
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Text that was read</returns>
        static string ReadingFile(string path)
        {
            string fileText = null;
            try
            {
                fileText = File.ReadAllText(path);
                Console.WriteLine("File was successful read");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Incorrect path");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Not such directory");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File wasn't found'");
            }
            catch (SecurityException)
            {
                Console.WriteLine("Security error");
            }
            catch (IOException)
            {
                Console.WriteLine("Something wrong with file");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Something go wrong {exception.Message}");
            }

            return fileText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="name"></param>
        static void WriteFile(string text, string name)
        {
            try
            {
                File.WriteAllText(name,  text);
                Console.WriteLine("File was saved");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Incorrect path");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Not such directory");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File wasn't found'");
            }
            catch (SecurityException)
            {
                Console.WriteLine("Security error");
            }
            catch (IOException)
            {
                Console.WriteLine("Something wrong with file");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Something go wrong {exception.Message}");
            }
        }

        static string RandomAlphabet()
        {
            string alphabet = "";

            for (int i = 0; i < 26; i++)
            {
                char later;
                do
                {
                    later = (char)Random.Next('a', 'z' + 1);
                }while(alphabet.Contains(later));

                alphabet += later;
            }

            return alphabet;
        }
        
        static string PairCode(string text)
        {
            string alphabet = RandomAlphabet();
            string result = "";
            foreach (char letter in text)
            {
                if (letter >= 'a' && letter <= 'z')
                {
                    result += alphabet[(alphabet.IndexOf(letter) + 13) % 26];
                }
                else
                {
                    if (letter >= 'A' && letter <= 'Z')
                    {
                        result += (char) (alphabet[(alphabet.IndexOf((char) (letter + 32)) + 13) % 26] - 32);
                    }
                    else
                    {
                        result += letter;
                    }
                }
            }

            Console.WriteLine($"Text was encrypted. key = {alphabet}");
            return result;
        }

        static string PairDeCode(string text, string key)
        {
            string result = "";
            
            foreach (char letter in text)
            {
                if (letter >= 'a' && letter <= 'z')
                {
                    result += key[(key.IndexOf(letter) + 13) % 26];
                }
                else
                {
                    if (letter >= 'A' && letter <= 'Z')
                    {
                        result += (char) (key[(key.IndexOf((char) (letter + 32)) + 13) % 26] - 32);
                    }
                    else
                    {
                        result += letter;
                    }
                }
            }

            Console.WriteLine("Text was successfully decrypted");
            return result;
        }

        static string RomCode(string text)
        { 
           int key = Random.Next();
           while (key % 26 == 0)
           {
               key = Random.Next();
           }
           string result = "";
           foreach (char letter in text)
           {
               if (letter >= 'a' && letter <= 'z')
               {
                   result += (char)((letter - 'a' + key) % 26 + 'a');
               }
               else
               {
                   if (letter >= 'A' && letter <= 'Z')
                   {
                       result += (char)((letter - 'A') + key % 26 + 'A');
                   }
                   else
                   {
                       result += letter;
                   }
               }
           }

           Console.WriteLine($"Text was successfully encrypted. key = {key}");
           return result;
        }
        
        static string RomDeCode(string text, int key)
        {
            string result = "";
            foreach (char letter in text)
            {
                if (letter >= 'a' && letter <= 'z')
                {
                    result += (char)((letter - 'a' + 26 - (key % 26)) % 26 + 'a');
                }
                else
                {
                    if (letter >= 'A' && letter <= 'Z')
                    {
                        result += (char)((letter - 'A' + 26 - (key % 26)) % 26 + 'A');
                    }
                    else
                    {
                        result += letter;
                    }
                }
            }

            Console.WriteLine("Text was successfully decrypted.");
            return result;
        }

        static void Menu()
        {
            string text = "";
            do
            {
                Console.WriteLine("1. Read all text from \n" +
                                  "2. Print \n" +
                                  "3. Rom encrypt \n" +
                                  "4. Rom decrypt \n" +
                                  "5. Pair encrypt \n" +
                                  "6. Pair decrypt \n" +
                                  "7. Save in \n" +
                                  "8. Exit");
                switch (Read())
                {
                    case 1:
                        Console.WriteLine("Input path");
                        text = ReadingFile(Console.ReadLine());
                        break;
                    case 2:
                        Console.WriteLine(text);
                        break;
                    case 3:
                        text = RomCode(text);
                        break;
                    case 4:
                        Console.WriteLine("Input Key");
                        text = RomDeCode(text, Read());
                        break;
                    case 5:
                        text = PairCode(text);
                        break;
                    case 6:
                        Console.WriteLine("Input Key");
                        text = PairDeCode(text, Console.ReadLine());
                        break;
                    case 7:
                        Console.WriteLine("Input Path");
                        WriteFile(text,Console.ReadLine());
                        break;
                    case 8:
                        Console.WriteLine("Thank you for using, by!");
                        return;
                    default:
                        Console.WriteLine("Wrong number");
                        break;
                }   
            } while (true);
        }
        
        static void Main()
        {
            Menu();
        }
    }
}