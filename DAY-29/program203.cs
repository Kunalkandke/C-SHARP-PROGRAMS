// Program to evaluate a Postfix mathematical expression using a Stack.
using System;
using System.Collections.Generic;

class Program
{
    static int EvaluatePostfix(string expression)
    {
        Stack<int> stack = new Stack<int>();

        foreach (char c in expression)
        {
            if (char.IsDigit(c))
            {
                stack.Push(c - '0');
            }
            else
            {
                int val2 = stack.Pop();
                int val1 = stack.Pop();

                switch (c)
                {
                    case '+': stack.Push(val1 + val2); break;
                    case '-': stack.Push(val1 - val2); break;
                    case '*': stack.Push(val1 * val2); break;
                    case '/': stack.Push(val1 / val2); break;
                    case '^': stack.Push((int)Math.Pow(val1, val2)); break;
                }
            }
        }

        return stack.Pop();
    }

    static void Main(string[] args)
    {
        string postfix = "231*+9-"; 
        int result = EvaluatePostfix(postfix);
        Console.WriteLine("Postfix Expression: " + postfix);
        Console.WriteLine("Result: " + result);
    }
}