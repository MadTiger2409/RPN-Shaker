using System;
using System.Collections.Generic;
using System.Text;

namespace RPNShaker.Logic
{
    public static class RPNCoverter
    {
        // String for output
        private static string output = "";

        // Counter for input
        private static int inputMarker = 0;

        // Stack list for ONP
        private static Stack<string> stack = new Stack<string>();

        // Array of operations and values
        private readonly static List<KeyValuePair<string, int>> operations = new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>("(",0),
            new KeyValuePair<string, int>("+",1),
            new KeyValuePair<string, int>("-",1),
            new KeyValuePair<string, int>(")",1),
            new KeyValuePair<string, int>("*",2),
            new KeyValuePair<string, int>("/",2),
            new KeyValuePair<string, int>("SIN",4),
            new KeyValuePair<string, int>("COS",4),
            new KeyValuePair<string, int>("TG",4),
            new KeyValuePair<string, int>("CTG",4),
            new KeyValuePair<string, int>("NEG",4),
        };

        /// <summary>
        /// <para> List for inputs</para>
        /// </summary>
        public static List<string> input = new List<string>();

        public static void ClearAll()
        {
            input.Clear();
            output = "";
            inputMarker = 0;
        }

        // Make List private and create public methods for adding variables and operators
        public static void AddToInputList(string character)
        {
            input.Add(character);
        }

        public static void RemoveFromInputList()
        {
            if (input.Count > 0)
            {
                input.RemoveAt(input.Count - 1);
            }
        }

        // Checking if a current charakter is a operation symbol
        private static bool IsOperationSymbol(string input)
        {
            var y = operations.Find(x => x.Key.Contains(input));
            if (y.Key == input)
            {
                return true;
            }
            else { return false; }
        }

        // Check if operation symbol from input have less value than symbol on stack
        private static bool HaveLessValue(int InputValue)
        {
            if (stack.Count == 0)
            {
                return false;
            }
            else
            {
                var y = operations.Find(x => x.Key.Contains(stack.Peek()));

                if (InputValue > y.Value)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        // Return input operation symbol value
        private static int GiveOperatorValue(string operatorsymbol)
        {
            var y = operations.Find(x => x.Key.Contains(operatorsymbol));
            return y.Value;
        }

        // Add rest of operators to output
        private static void AddRestOperators()
        {
            for (int i = stack.Count - 1; i >= 0; i--)
            {
                output += stack.Pop() + " ";
            }
        }

        private static void WorkOnOperationSymbol()
        {
            // If stack is empty.
            if (stack.Count == 0)
            {
                stack.Push(input[inputMarker].ToString());
                inputMarker++;
            }
            // If stack isn't empty.
            else if (stack.Count > 0)
            {
                // Working on closing bracket
                if (input[inputMarker].ToString() == operations[3].Key)
                {
                    while (stack.Peek() != operations[0].Key)
                    {
                        output += stack.Pop() + " ";
                    }

                    stack.Pop();
                    inputMarker++;
                }
                // If value of symbol from input is smaller than value of symbol on stack.
                // Or if symbol is a opening bracker.
                // Push symbol from input on stack and increase input marker.
                else if (((HaveLessValue(GiveOperatorValue(input[inputMarker].ToString())) == false) && (input[inputMarker].ToString() != operations[3].Key)) || (input[inputMarker].ToString() == operations[0].Key))
                {
                    stack.Push(input[inputMarker].ToString());
                    inputMarker++;
                }
                else
                {
                    // While value of symbol from input is smaller than value of symbol on stack.
                    // Take operator from stack to output.
                    while ((HaveLessValue(GiveOperatorValue(input[inputMarker].ToString())) == true))
                    {
                        output += stack.Pop() + " ";
                    }
                }
            }
        }

        /// <summary>
        /// <para> Converting from normal notatnion to RPN and returning resoult in String.</para>
        /// </summary>
        public static string NormalToRPN()
        {
            while (inputMarker < input.Count)
            {
                // For every character in imput.
                // Helps to not repeat operations.
                string currentInput = input[inputMarker].ToString();
                bool anOperationSymbol = IsOperationSymbol(currentInput);

                // If character is a equation variable.
                // Add to output and increase input counter.
                if (anOperationSymbol == false)
                {
                    output += input[inputMarker] + " ";
                    inputMarker++;
                }
                // If character is a operation symbol.
                else if (anOperationSymbol == true)
                {
                    WorkOnOperationSymbol();
                }
            }
            AddRestOperators();
            return output;
        }
    }
}
