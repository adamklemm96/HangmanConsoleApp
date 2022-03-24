using System;

namespace HangMan_consoleApp
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The program to run Hangman as an console application.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // Show the welcome screen 
            void Welcome()
            {
                Console.Clear();
                Console.WriteLine("=====================");
                Console.WriteLine("HANGMAN V1.0");
                Console.WriteLine("=====================");
            }

            // Load the words from the text file and pick one random word 
            string PickTheWord()
            {
                string[] allLines =
                    File.ReadAllLines(@"C:\Users\adam.klemm\source\repos\HangMan_consoleApp\words\wordlist.txt");

                Random rand = new Random();

                return allLines[rand.Next(0, allLines.Length)];
            }

            // Take the picked word count it's lenght and display its lenght in the app 
            string ShowTheGuess(string word, List<char> guessedLetters)
            {
                List<char> wordArray = word.ToList();
                List<string> wordProgress = new List<string>();

                foreach (char letter in wordArray)
                {
                    if (guessedLetters.Contains(letter))
                    {
                        Console.Write(letter + " ");
                        wordProgress.Add(letter.ToString());
                    }
                    else
                    {
                        Console.Write("_ ");
                        wordProgress.Add("_");
                    }
                }

                return string.Join(string.Empty, wordProgress);
            }

            List<char> userInput(List<char> guessedLetters)
            {
                bool validInput;
                string guessedLetter;

                do
                {
                    Console.WriteLine("\n=============");
                    Console.WriteLine(string.Format("Guessed letters => {0}", string.Join(", ", guessedLetters)));
                    Console.WriteLine("=============\n");
                    Console.WriteLine("\nEnter your guess: ");

                    guessedLetter = Console.ReadLine();
                    validInput = ValidateInput(guessedLetter.ToLower(), guessedLetters);
                }
                while (!validInput);

                guessedLetters.Add(char.Parse(guessedLetter));
                return guessedLetters;
            }

            void DisplayHangMan(int lives)
            {
                switch (lives)
                {
                    case 0:
                        Console.WriteLine("-----");
                        Console.WriteLine("|   o");
                        Console.WriteLine("|  \\|/");
                        Console.WriteLine("|  / \\ ");
                        Console.WriteLine("|");
                        Console.WriteLine("===");

                        break;
                    case 1:
                        Console.WriteLine("-----");
                        Console.WriteLine("|   o");
                        Console.WriteLine("|  \\|/");
                        Console.WriteLine("|  /  ");
                        Console.WriteLine("|");
                        Console.WriteLine("===");

                        break;
                    case 2:
                        Console.WriteLine("-----");
                        Console.WriteLine("|   o");
                        Console.WriteLine("|  \\|/");
                        Console.WriteLine("|    ");
                        Console.WriteLine("|");
                        Console.WriteLine("===");

                        break;
                    case 3:
                        Console.WriteLine("-----");
                        Console.WriteLine("|   o");
                        Console.WriteLine("|  \\|");
                        Console.WriteLine("|    ");
                        Console.WriteLine("|");
                        Console.WriteLine("===");

                        break;
                    case 4:
                        Console.WriteLine("-----");
                        Console.WriteLine("|   o");
                        Console.WriteLine("|   |");
                        Console.WriteLine("|    ");
                        Console.WriteLine("|");
                        Console.WriteLine("===");

                        break;
                    case 5:
                        Console.WriteLine("-----");
                        Console.WriteLine("|   o");
                        Console.WriteLine("|   ");
                        Console.WriteLine("|    ");
                        Console.WriteLine("|");
                        Console.WriteLine("===");

                        break;
                    case 6:
                        Console.WriteLine("-----");
                        Console.WriteLine("|   ");
                        Console.WriteLine("|   ");
                        Console.WriteLine("|    ");
                        Console.WriteLine("|");
                        Console.WriteLine("===");

                        break;
                    case 7:
                        Console.WriteLine("");
                        Console.WriteLine("|   ");
                        Console.WriteLine("|   ");
                        Console.WriteLine("|    ");
                        Console.WriteLine("|");
                        Console.WriteLine("===");

                        break;
                    case 8:
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("===");

                        break;
                }
            }

            bool DidPlayerGuessTheWord(string word, string guess)
            {
                if (word == guess)
                {
                    Console.WriteLine("You guessed the word !!!");
                    return true;
                }

                return false;
            }

            bool ValidateInput(string input, List<char> guessedLetters)
            {
                if (!Regex.IsMatch(input, @"[a-z]{1}"))
                {
                    Console.WriteLine("Wrong input, please insert only one character");
                    return false;
                }

                if (guessedLetters.Contains(char.Parse(input)))
                {
                    Console.WriteLine("You've already guessed this one");
                    return false;
                }

                return true;    
            }

            int livesRemaining(int lives, string guessedWord, char letter)
            {
                List<char> guessedWordList = guessedWord.ToList();
                if (!guessedWordList.Contains(letter))
                {
                    lives--;
                }

                return lives;
            }

            bool playerWon;
            bool gameOn = true;

            while (gameOn)
            {
                Console.Clear();
                int lives = 8;
                string guessedWord;
                List<char> guessedLetters = new List<char>();
                string word = PickTheWord();

                do
                {
                    Welcome();
                    Console.WriteLine();
                    Console.WriteLine($"Lives remaining => {lives} \n");
                    guessedWord = ShowTheGuess(word, guessedLetters);
                    Console.WriteLine();
                    DisplayHangMan(lives);
                    Console.WriteLine();
                    playerWon = DidPlayerGuessTheWord(word, guessedWord);
                    if (!playerWon)
                    {
                        guessedLetters = userInput(guessedLetters);
                    }
                    lives = livesRemaining(lives, word, guessedLetters[guessedLetters.Count() - 1]);

                }
                while (lives > 0 & !playerWon);

                if (playerWon)
                {
                    Console.Clear();
                    Console.WriteLine("You won the game!");
                    Console.WriteLine("To quit the game press 'Q' to continue press any other key");
                    string key = Console.ReadLine();

                    if (key.ToLower() == "q")
                    {
                        gameOn = false;
                    }
                }

                if (lives == 0)
                {
                    Console.Clear();
                    Console.WriteLine("You lost");
                    Console.WriteLine($"The word is {word}");
                    Console.WriteLine("To quit the game press 'Q' to continue press any other key");
                    string key = Console.ReadLine();
                    if (key.ToLower() == "q")
                    {
                        gameOn = false;
                    }
                } 
            }
        }
    }
}
