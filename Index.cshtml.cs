using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ScientificCalculatorWebApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public double FirstNumber { get; set; }

        [BindProperty]
        public double SecondNumber { get; set; }

        public string Result { get; set; } = string.Empty;

        public void OnPost(string operation)
        {
            // Clear result
            Result = string.Empty;

            if (string.IsNullOrEmpty(operation))
            {
                Result = "Please select an operation.";
                return;
            }

            // Handle basic operations
            if (operation == "add" || operation == "subtract" || operation == "multiply" || operation == "divide")
            {
                if (!ModelState.IsValid || !TryParseNumbers(out double firstNumber, out double secondNumber))
                {
                    Result = "Please enter valid numbers.";
                    return;
                }

                switch (operation)
                {
                    case "add":
                        Result = (firstNumber + secondNumber).ToString();
                        break;
                    case "subtract":
                        Result = (firstNumber - secondNumber).ToString();
                        break;
                    case "multiply":
                        Result = (firstNumber * secondNumber).ToString();
                        break;
                    case "divide":
                        if (secondNumber != 0)
                        {
                            Result = (firstNumber / secondNumber).ToString();
                        }
                        else
                        {
                            Result = "Cannot divide by zero.";
                        }
                        break;
                }
            }
            // Handle advanced operations
            else if (operation == "square" || operation == "cube")
            {
                if (!ModelState.IsValid || !TryParseNumber(out double value))
                {
                    Result = "Please enter a valid number.";
                    return;
                }

                switch (operation)
                {
                    case "square":
                        Result = (value * value).ToString();
                        break;
                    case "cube":
                        Result = (value * value * value).ToString();
                        break;
                }
            }
            else if (operation == "power")
            {
                if (!ModelState.IsValid || !TryParseBaseAndExponent(out double baseValue, out double exponentValue))
                {
                    Result = "Please enter valid numbers.";
                    return;
                }

                Result = Math.Pow(baseValue, exponentValue).ToString();
            }
            else
            {
                Result = "Invalid operation.";
            }
        }

        private bool TryParseNumbers(out double firstNumber, out double secondNumber)
        {
            bool firstNumberParsed = double.TryParse(FirstNumber.ToString(), out firstNumber);
            bool secondNumberParsed = double.TryParse(SecondNumber.ToString(), out secondNumber);
            return firstNumberParsed && secondNumberParsed;
        }

        private bool TryParseNumber(out double value)
        {
            return double.TryParse(FirstNumber.ToString(), out value);
        }

        private bool TryParseBaseAndExponent(out double baseValue, out double exponentValue)
        {
            bool baseParsed = double.TryParse(FirstNumber.ToString(), out baseValue);
            bool exponentParsed = double.TryParse(SecondNumber.ToString(), out exponentValue);
            return baseParsed && exponentParsed;
        }
    }
}
