using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame
{
    internal class Program
    {

        //static char[] currentWordGuessing = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
        static char[] guessesLeft = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
        static int guesses = 7;
        static int score = 0;
        static char[] playerGuesses;

        static void Main(string[] args)
        {
            Run();
        }
        public static void Run()
        {
            
            char[] temp = null;
            char[] chosen = ChooseCategoryOfWord(temp);
            

            PlayGame(chosen);
        }

        public static void PlayGame(char[] chosenWord)
        {


            int wrongGuessCounter = 0;
            bool keepGuessing = true;
            bool win = false;
            playerGuesses = new char[chosenWord.Length];
            char[] wrongGuesses = new char[7];
            

            while (keepGuessing)
            {
                bool beginGuessing = false;

                if (win)
                {
                    Console.WriteLine("YOU WIN!");
                    score += 500; //award score for each correct letter??
                    keepGuessing = false;
               
                    Run();
                }

                if (!beginGuessing)
                {

                    GameInterface(playerGuesses, wrongGuesses, score);
                }

                Console.Write("Pick a Letter: ");
                string input = Console.ReadLine().ToLower();
                
                
             

                bool y = char.TryParse(input, out char characterPicked); 

                characterPicked = Char.ToLowerInvariant(characterPicked);

                int inputLength = input.Length; //this checks that the input is 1 character and no more and runs code if that's true

                if (String.IsNullOrWhiteSpace(input))
                {
                    win = CheckWin(chosenWord, playerGuesses);
                    Console.WriteLine("Your choice cannot be blank.");
                }

                else if (inputLength == 1)
                {
                    
                    bool match = chosenWord.Contains(characterPicked);  //this will need to change to c.Contains(ch)

                    if (match)
                    {
                        

                        //logic here to add match to the specific places matching the word to be guessed
                        if (chosenWord.Contains(characterPicked))
                        {
                            for (int i = 0; i < chosenWord.Length; i++)
                            {
                                if(chosenWord[i] == characterPicked)
                                {
                                    playerGuesses[i] = characterPicked;
                                }
                                
                            }
                        }
                        
                        inputLength = 0; //reset inputLength to ensure length check doesn't stay 1 if input is blank
                        
                        win = CheckWin(chosenWord, playerGuesses); //the checkwin is passing the chosen word to the area where we should guess 12/20/21
                    }

                    else
                    {
                        guesses--; //this is to reduce the amount of tries they can do and potentially lose the game

                        if (wrongGuesses.Contains(characterPicked))
                        {
                            Console.WriteLine("You've already guessed this.");
                        }
                        else
                        {
                            
                            wrongGuesses[wrongGuessCounter] = characterPicked;
                            wrongGuessCounter++;

                        }

                        inputLength = 0;

                        
                        if (guesses == 0)
                        {
                            Console.WriteLine("You've lost the game, but you win at life.");
                            Console.ReadLine();
                            ResetGame();
                            Run();
                        }
                        

                    }

                }

                else
                {

                    Console.WriteLine("You entered more than one letter {0}", inputLength);
                }
                GameInterface(playerGuesses, wrongGuesses, score);
                beginGuessing = true;

            }
        }

        public static char[] ChooseCategoryOfWord(char[] arr)  //there is no need to pass the arr in but I do need a return char[] but only from two statement, the final return value is an issue im having on returning
        {

            string[] place = { "california", "toronto", "greenville", "denver", "charlotte" };
            string[] person = { "sam", "maria", "orlando", "chaz", "samuel", "california", "toronto", "greenville", "denver", "charlotte" };


            bool run = true;

            while (run)
            {

                Console.WriteLine("Choose a Category: 1 for Place. 2 for Person");
                
                string userChoose = Console.ReadLine();
                int categoryChoice;

                bool isNumber = int.TryParse(userChoose, out categoryChoice);

                if (isNumber && categoryChoice == 1 || isNumber && categoryChoice == 2)
                {
                    if (categoryChoice == 1)
                    {
                        Console.WriteLine("You have chosen to play HangMan with a Place.");
                        arr = ChooseRandomWord(place); //pick place
                        run = false;
                    }
                    else if (categoryChoice == 2)
                    {
                        Console.WriteLine("You have chosen to play HangMan with a Person.");
                        arr = ChooseRandomWord(person); //pick person
                        run = false;
                    }else
                    {
                        Console.WriteLine($"Please choose either 1 or 2.");
                    }
                }

            }
            return arr;
        }

        public static char[] ChooseRandomWord(string[] arr)
        {
            Random random = new Random();
            var randomIndex = random.Next(0, arr.Length);

            string chosenRandomWord = arr[randomIndex];

            //Console.WriteLine($"The word is { chosenRandomWord }");  //this will show the random word for debug

            char[] charArray = chosenRandomWord.ToCharArray();

            return charArray;

        }

        public static void GameInterface(char[] playerGuessArray, char[] guessesRemaining, int currentScore)
        {
            if(playerGuessArray.Length == 3)
            {
                Console.Clear();
               
                Console.WriteLine($"---------------Current Score:{currentScore} -----------");
                Console.WriteLine($"| Current Word:     {playerGuessArray[0]} {playerGuessArray[1]} {playerGuessArray[2]}                 |");  //this line shows the current correct guesses and remaining blank
                Console.WriteLine($"|                                             |");
                Console.WriteLine($"| Guesses: [{guessesRemaining[0]}] [{ guessesRemaining[1]}] [{guessesRemaining[2]}] [{guessesRemaining[3]}] [{guessesRemaining[4]}] [{guessesRemaining[5]}] [{guessesRemaining[6]}]        |"); // this will show how many tries you have left, X indicating a fail
                Console.WriteLine($"-----------------------------------------------");
                

            }

            if (playerGuessArray.Length == 4)
            {
                Console.Clear();
                //GuessesLeftRefresh();
                Console.WriteLine($"---------------Current Score:{score} -----------");
                Console.WriteLine($"| Current Word:     {playerGuessArray[0]} {playerGuessArray[1]} {playerGuessArray[2]} {playerGuessArray[3]}                   |");  //this line shows the current correct guesses and remaining blank
                Console.WriteLine($"|                                             |");
                Console.WriteLine($"| Guesses: [{guessesRemaining[0]}] [{ guessesRemaining[1]}] [{guessesRemaining[2]}] [{guessesRemaining[3]}] [{guessesRemaining[4]}] [{guessesRemaining[5]}] [{guessesRemaining[6]}]        |"); // this will show how many tries you have left, X indicating a fail
                Console.WriteLine($"-----------------------------------------------");
               

            }

            if (playerGuessArray.Length == 5)
            {
                Console.Clear();
                //GuessesLeftRefresh();
                Console.WriteLine($"---------------Current Score:{score} -----------");
                Console.WriteLine($"| Current Word:     {playerGuessArray[0]} {playerGuessArray[1]} {playerGuessArray[2]} {playerGuessArray[3]} {playerGuessArray[4]}                 |");  //this line shows the current correct guesses and remaining blank
                Console.WriteLine($"|                                             |");
                Console.WriteLine($"| Guesses: [{guessesRemaining[0]}] [{ guessesRemaining[1]}] [{guessesRemaining[2]}] [{guessesRemaining[3]}] [{guessesRemaining[4]}] [{guessesRemaining[5]}] [{guessesRemaining[6]}]        |"); // this will show how many tries you have left, X indicating a fail
                Console.WriteLine($"-----------------------------------------------");
                
            }

            if (playerGuessArray.Length == 6)
            {
                Console.Clear();
                //GuessesLeftRefresh();
                Console.WriteLine($"---------------Current Score:{score} -----------");
                Console.WriteLine($"| Current Word:     {playerGuessArray[0]} {playerGuessArray[1]} {playerGuessArray[2]} {playerGuessArray[3]} {playerGuessArray[4]} {playerGuessArray[5]}               |");  //this line shows the current correct guesses and remaining blank
                Console.WriteLine($"|                                             |");
                Console.WriteLine($"| Guesses: [{guessesRemaining[0]}] [{ guessesRemaining[1]}] [{guessesRemaining[2]}] [{guessesRemaining[3]}] [{guessesRemaining[4]}] [{guessesRemaining[5]}] [{guessesRemaining[6]}]        |"); // this will show how many tries you have left, X indicating a fail
                Console.WriteLine($"-----------------------------------------------");
               
            }

            if (playerGuessArray.Length == 7)
            {
                Console.Clear();
                //GuessesLeftRefresh();
                Console.WriteLine($"---------------Current Score:{score} -----------");
                Console.WriteLine($"| Current Word:     {playerGuessArray[0]} {playerGuessArray[1]} {playerGuessArray[2]} {playerGuessArray[3]} {playerGuessArray[4]} {playerGuessArray[5]} {playerGuessArray[6]}             |");  //this line shows the current correct guesses and remaining blank
                Console.WriteLine($"|                                             |");
                Console.WriteLine($"| Guesses: [{guessesRemaining[0]}] [{ guessesRemaining[1]}] [{guessesRemaining[2]}] [{guessesRemaining[3]}] [{guessesRemaining[4]}] [{guessesRemaining[5]}] [{guessesRemaining[6]}]        |"); // this will show how many tries you have left, X indicating a fail
                Console.WriteLine($"-----------------------------------------------");
               
            }

           

        }

        public static void ResetGame()
        {
            Console.Clear();
            char[] guessArrayDisplayReset = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
            char[] guessesLeftReset = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
            //set arrays back to original value
            playerGuesses = guessArrayDisplayReset;
            guessesLeft = guessesLeftReset;
            score = 0;
            guesses = 7;


        }

        public static bool CheckWin(char[] arr, char[] c)
        {
            var s1 = new string(arr.ToArray());
            var s2 = new string(c.ToArray());
            bool win = false;

            //Console.WriteLine($"Hidden Word: {s1} Correct Guesses: {s2}."); //debug line

            if (s1 == s2)
            {
                win = true;
            }

            return win;
        }

    }
}
