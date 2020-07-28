using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace QuadaxTheLeProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            //--Create Random Answer
            Random oRand = new Random();
            //--create array with answer
            string[] arrAnswer = new string[4];
            //--Populate it with random values, 1 to 6
            arrAnswer[0] = oRand.Next(1, 7).ToString();
            arrAnswer[1] = oRand.Next(1, 7).ToString();
            arrAnswer[2] = oRand.Next(1, 7).ToString();
            arrAnswer[3] = oRand.Next(1, 7).ToString();
            //--Debug
            Console.Write("(");
            for (int q = 0; q < 4; q++)
                Console.Write(arrAnswer[q]);
            Console.Write(")");
            //--boolean to keep playing game
            bool gameOn = true;
            int totalAttempts = 10;
            Console.WriteLine("Welcome to Mastermind. The answer is 4 digits long, each digit is 1 to 6. ?");

            while (gameOn == true)
            {
                Console.WriteLine("What is your guess? (you have " + totalAttempts.ToString() + " attempts left)");
                Console.Write("> ");
                //--Read player answer. declare variable here since it's immutable.
                string playerAnswer = Console.ReadLine();
                //--If answer is valid, see if it's right or wrong
                Console.Write("Result: ");
                if (fnValidateFormatOfAnswer(playerAnswer) == true)
                {
                    //--Only valid answers is considered an "attempt"
                    totalAttempts -= 1;
                    //--copy the correct answer array into a new array, which is only used here
                    string[] myCopiedAnswer = new string[4];
                    arrAnswer.CopyTo(myCopiedAnswer, 0);
                    //--check how many digits are correct (regardless of order)
                    int totalValuesFound = 0;
                    for (int p = 0; p < 4; p++) //--p=player answer
                    {
                        //--Get each character, one at a time
                        string thisCharacterValue = playerAnswer.Substring(p, 1);
                        //--Check it aainst all correct answers
                        for (int c = 0; c < 4; c++) //--c=correct answer
                        {
                            if (myCopiedAnswer[c] == thisCharacterValue)
                            {
                                totalValuesFound += 1; //increase count
                                myCopiedAnswer[c] = "X"; //change the value so we don't count it twice
                                break; //--exit since we don't want to check thisCharacterValue anymore
                            }

                        }
                    }
                        
                    //--Which answers are in the correct order?
                    int totalCorrectOrder = 0;
                    for (int x = 0; x < 4; x++)
                        if (playerAnswer.Substring(x, 1) == arrAnswer[x])
                            totalCorrectOrder += 1;
                    //--Give the player the results
                    for (int iCorrect = 0; iCorrect < totalCorrectOrder; iCorrect++)
                        Console.Write("+ ");
                    for (int iWrong = 0; iWrong < totalValuesFound - totalCorrectOrder; iWrong++)
                        Console.Write("- ");
                    Console.WriteLine();

                    if (totalCorrectOrder == 4)
                    {

                        Console.Beep();
                        Console.WriteLine("Winner winner chicken dinner! YOU WIN!");
                        gameOn = false;
                    }
                    else if (totalAttempts == 0)
                    {
                        Console.WriteLine("Game over man, game over! (you have no more attempts left)");
                        gameOn = false;
                    }

                }
                


            } //End while loop

            Console.WriteLine("Press Enter to Exit!");
            Console.ReadLine();
              
        }


        public static Boolean fnValidateFormatOfAnswer(string myAnswer)
        {
            //--4 characters?
            if (myAnswer.Length != 4)
            {
                string moreInfo = string.Empty;
                if (myAnswer.Length < 4)
                    moreInfo = " short";
                else
                    moreInfo = " long";
                Console.WriteLine("Your answer is too" + moreInfo + "! It must be 4 digits long!" + Environment.NewLine);
                return false;
            }
            //--All digits?
            for (int x = 0; x < 4; x++)
            {
                string varCheck = myAnswer.Substring(x, 1);
                int z = 0;
                bool isInteger = int.TryParse(varCheck, out z);
                if (isInteger == false)
                {
                    Console.WriteLine("Your answer is invalid! It must contain only digits!" + Environment.NewLine);
                    return false;
                }
            }

            //--all checks out, return true
            return true;
        }
    }
}
