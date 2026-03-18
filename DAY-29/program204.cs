// program to convert an Infix expression to a Postfix expression (Shunting-yard algorithm).
using System;
using System.Collections.Generic;

class Program
{
    static int Precedence(char op)
    {
        return op switch
        {
            '+' or '-' => 1,
            '*' or '/' => 2,
            '^' => 3,
            _ => 0
        };
    }

    static bool IsOperator(char c)
    {
        return c == '+' || c == '-' || c == '*' || c == '/' || c == '^';
    }

    static string InfixToPostfix(string infix)
    {
        Stack<char> stack = new Stack<char>();
        string postfix = "";

        foreach (char c in infix)
        {
            if (char.IsLetterOrDigit(c))
            {
                postfix += c;
            }
            else if (c == '(')
            {
                stack.Push(c);
            }
            else if (c == ')')
            {
                while (stack.Count > 0 && stack.Peek() != '(')
                {
                    postfix += stack.Pop();
                }
                stack.Pop(); // Remove '('
            }
            else if (IsOperator(c))
            {
                while (stack.Count > 0 && Precedence(stack.Peek()) >= Precedence(c))
                {
                    postfix += stack.Pop();
                }
                stack.Push(c);
            }
        }

        while (stack.Count > 0)
        {
            postfix += stack.Pop();
        }

        return postfix;
    }

    static void Main(string[] args)
    {
        string infix = "A+B*(C^D-E)^(F+G*H)-I";
        string postfix = InfixToPostfix(infix);
        Console.WriteLine("Infix Expression: " + infix);
        Console.WriteLine("Postfix Expression: " + postfix);
    }
}