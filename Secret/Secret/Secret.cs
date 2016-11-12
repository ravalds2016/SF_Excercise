using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
/*
 * You are given a function 'secret()' that accepts a single integer parameter
 * and returns an integer. In your favorite programming language, 
 * write a command-line program that takes one command-line argument (a number)
 * and determines if the secret() function is additive [secret(x+y) = secret(x) + secret(y)],
 * for all combinations x and y, where x and y are all prime numbers
 * less than the number passed via the command-line argument.
 * 
 * Describe how to run your examples. 
 * 
 * Please generate the list of primes without using built-in functionality.

 * ********* My understanding and thought process *********
 * I broke this problem into two sub problems 
 * 1) Finding all prime numbers below user specified number (PrimeLister class) 
 * 2) For all non repeating combinations of x and y check if secret() is additive or not (AdditiveChecker class).
 */
namespace Secret
{
    class Client
    {
        static void Main(string[] args)
        {
            //Check if input is supplied
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter integer greater than 1");
                return;
            }

            int userInput;
            bool isNumber = int.TryParse(args[0], out userInput);

            //check if user input is integer and greater than 1 because smallest prime is 2
            if (isNumber == false || userInput <= 1)
            {                  
                Console.WriteLine("Please enter positive integer greater than 1");
                Console.ReadKey();
                return;
            }
            
            PrimeLister primeNumberLister = new PrimeLister();           
            List<int> primes = primeNumberLister.getPrimes(userInput);                        
            
            //if there are any prime numbers less than number passed via command line
            if (primes.Count > 0)
            {
                AdditiveChecker additiveCheckerInstance = new AdditiveChecker();                
                if (additiveCheckerInstance.isSecretAdditive(primes))
                    Console.WriteLine("secret() is additive");
                else
                    Console.WriteLine("secret() is not additive");
            }
            else
            {
                Console.WriteLine("No prime numbers found less than given input number");
            }
            Console.ReadKey();            
        }

        //Class contains two methods 1) secret() 2) isSecretAdditive
        class AdditiveChecker
        {
            /*This method takes list of primes and checks for all combinations of prime numbers x and y excluding repetitive combinations 
             for e.g. (2,3) = (3,2)*/
            public bool isSecretAdditive(List<int> primes)
            {
                for (int iIndex = 0; iIndex < primes.Count; iIndex++)
                    {
                        // starting jIndex from iIndex thus elimintating repetitive combinations
                        for (int jIndex = iIndex; jIndex < primes.Count; jIndex++)
                        {
                            //to see non repetitive pairs of prime numbers uncomment below line of code
                            //Console.WriteLine(primes[iIndex]+","+primes[jIndex]);
                            if (secret(primes[iIndex] + primes[jIndex]) != secret(primes[iIndex]) + secret(primes[jIndex]))
                                return false;
                        }
                    }
                    return true;                                   
            }

            /*As the problem states secret() function takes an integer which are all prime numbers in our problem and returns integer. 
            */
            int secret(int anInteger)
            {
                return anInteger;
            }
        }

        class PrimeLister
        {
            /*This method finds all primes less than number specified by command line argument as per sieve of eratosthenes algorithm. 
             * Which uses boolean array of size specified by user input having false as default values for all of its elements. 
             * It has list<int> "primes" holding all prime numbers less than user specified number. 
             * This method contains logic to start with iIndex = 2 which is smallest prime number and marking all  elements of 
             * array as "true" (true value specifies composite number) starting from index iIndex*iIndex 
             * having indexes as multiples of iIndex (this indexes are composite numbers), excluding initial iIndex as per sieve of eratosthenes algorithm.*/

            public List<int> getPrimes(int userInput)
            {
                  //array of bools indexed from 0 to userInput-1, by default all items are marked false in array.
                  bool[] array = new bool[userInput]; 

                //list of primes
                  List<int> primes = new List<int>();

                  /*starting from iIndex = 2 to userInput-1 as we need all prime numbers below user input. 
                   *Below code starts from iIndex=2 and marks all multiples
                  of iIndex(which are composite numbers) as "true" as per sieve of eratosthenes algorithm. */
                  for (int iIndex = 2; iIndex <= userInput - 1; iIndex++)
                  {
                      if (array[iIndex] == false) // if iIndex is prime number
                      {
                          primes.Add(iIndex); //adding prime number to primes list
                          Console.WriteLine(iIndex);
                          /*Even though iIndex is prime number, executing below loop only when iIndex*iIndex <= userInput-1 because if 
                           *   iIndex*iIndex > userInput-1 then all of iIndex's multiples(which are composite numbers) upto userInput-1 
                           * have already been marked "true" at this point as they are also multiples of smaller primes than current prime number iIndex*/
                          
                              //marking of composite indexes starts from iIndex*iIndex
                              for (int jIndex = iIndex * iIndex; jIndex <= userInput - 1; jIndex = jIndex + iIndex)
                                {
                                    array[jIndex] = true; // marking composite indexes as "true"
                                }                                                    
                      }
                  }
                  return primes;
            }
        }
    }
}
