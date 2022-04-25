using System;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        private char[] _delimiterChars = { ',', '\n' };
        public Calculator()
        {
        }

        public int Add(string inputString)
        {
            bool containsNegatives = false;
            int sum = 0;
            string negatives = "";
            if (inputString.Equals(String.Empty))
            {
                return sum;
            } else
            {
                if (inputString.StartsWith("//"))
                {
                    _delimiterChars = _delimiterChars.Concat(new char[] { inputString[2] }).ToArray();
                    inputString = inputString.Substring("//.\n".Length);

                }
                string[] possibleNumbers = inputString.Split(_delimiterChars);
                for (int possibleNumberIndex = 0; possibleNumberIndex < possibleNumbers.Length; possibleNumberIndex++)
                {
                    string possibleNumber = possibleNumbers[possibleNumberIndex];
                    if (possibleNumber.Equals(String.Empty))
                    {
                        throw new ArgumentException($"Invalid inputformat");
                    }

                    int parsedNumber = ParsePossibleNumber(possibleNumber);
                    if (parsedNumber < 0)
                    {
                        containsNegatives = true;
                        if (possibleNumberIndex == 0)
                        {
                            negatives = possibleNumbers[possibleNumberIndex];
                        }
                        else
                        {
                            negatives += "," + possibleNumber;
                        }

                    }
                    else
                    {
                        sum += ParsePossibleNumber(possibleNumber);
                    }
                }

                CheckNegatives(containsNegatives, negatives);
            }

            return sum;
        }

        private static void CheckNegatives(bool containsNegatives, string negatives)
        {
            if (containsNegatives)
            {
                throw new ArgumentException($"Negatives not allowed: {negatives}");
            }
        }

        private int ParsePossibleNumber(string possibleNumber)
        {
            if (int.TryParse(possibleNumber, out int number))
            {
                return number;
            }
            else
            {
                throw new ArgumentException($"Invalid number: {possibleNumber}");
            }
        }
    }
}