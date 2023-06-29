using System;
using System.Collections.Generic;
using System.Linq;

// A struct to hold a word and its score
struct Word
{
    public string text; // The word text
    public int score; // The word score based on its length
}

class Program
{
    // A list of possible words to use in the game
    static List<string> words = new List<string>() { "apple", "banana", "orange", "pear", "grape", "melon", "cherry", "lemon", "lime", "mango", "peach", "plum", "berry", "kiwi" };

    // A random number generator
    static Random random = new Random();

    // A method to generate a random set of letters
    static char[] GenerateLetters(int n)
    {
        char[] letters = new char[n]; // An array to hold the letters
        for (int i = 0; i < n; i++)
        {
            letters[i] = (char)('a' + random.Next(26)); // Generate a random letter from a to z
        }
        return letters;
    }

    // A method to check if a word is valid, i.e. it is in the words list and it can be formed from the given letters
    static bool IsValidWord(string word, char[] letters)
    {
        if (!words.Contains(word)) return false; // Check if the word is in the words list
        char[] wordLetters = word.ToCharArray(); // Convert the word to an array of letters
        foreach (char c in wordLetters) // For each letter in the word
        {
            if (!letters.Contains(c)) return false; // Check if the letter is in the given letters
            letters = letters.Where(x => x != c).ToArray(); // Remove the letter from the given letters
        }
        return true; // If all checks passed, return true
    }

    // A method to calculate the score of a word based on its length
    static int CalculateScore(string word)
    {
        int length = word.Length; // Get the word length
        if (length <= 3) return 1; // If the length is 3 or less, return 1 point
        if (length <= 5) return 2; // If the length is 4 or 5, return 2 points
        if (length <= 7) return 3; // If the length is 6 or 7, return 3 points
        return 5; // If the length is more than 7, return 5 points
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the word game!"); // Greet the user
        Console.WriteLine("You will be given a set of letters and you have to find words that can be formed from them."); // Explain the rules
        Console.WriteLine("The longer the word, the higher the score."); // Explain the scoring system
        Console.WriteLine("Type 'quit' to end the game."); // Explain how to quit

        int totalScore = 0; // A variable to hold the total score of the user

        while (true) // Loop until the user quits
        {
            char[] letters = GenerateLetters(7); // Generate a random set of 7 letters
            Console.WriteLine("\nYour letters are: {0}", string.Join(" ", letters)); // Display the letters to the user

            List<Word> foundWords = new List<Word>(); // A list to hold the words that the user has found

            while (true) // Loop until the user enters an empty input or quits
            {
                Console.Write("Enter a word: "); // Prompt the user to enter a word
                string input = Console.ReadLine().ToLower(); // Read and normalize the input

                if (input == "") break; // If the input is empty, break the loop
                if (input == "quit") return; // If the input is quit, end the game

                if (IsValidWord(input, letters)) // If the input is a valid word
                {
                    int score = CalculateScore(input); // Calculate its score

                    if (foundWords.Any(w => w.text == input)) // If the user has already entered this word before
                    {
                        Console.WriteLine("You have already entered this word. Try another one."); // Inform them and ask them to try another one
                    }
                    else // If this is a new word for the user
                    {
                        foundWords.Add(new Word() { text = input, score = score }); // Add it to the found words list
                        totalScore += score; // Update the total score
                        Console.WriteLine("Good job! You get {0} point(s) for this word. Your total score is {1}.", score, totalScore); // Congratulate them and show their score
                    }
                }
                else // If the input is not a valid word
                {
                    Console.WriteLine("This is not a valid word. Try another one."); // Inform them and ask them to try another one
                }
            }

            Console.WriteLine("\nYou have found {0} word(s) from the letters.", foundWords.Count); // Show how many words the user has found
            Console.WriteLine("Here are the words and their scores:"); // Show the words and their scores
            foreach (Word word in foundWords)
            {
                Console.WriteLine("{0}: {1} point(s)", word.text, word.score);
            }
        }
    }
}
