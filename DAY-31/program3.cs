// Program to check if a string contains Balanced Parentheses (using a Stack)
using System;
using System.Collections.Generic;

class Program
{
    static bool IsBalanced(string input)
    {
        Stack<char> stack = new Stack<char>();
        foreach (char ch in input)
        {
            if (ch == '(' || ch == '{' || ch == '[')
            {
                stack.Push(ch);
            }
            else if (ch == ')' || ch == '}' || ch == ']')
            {
                if (stack.Count == 0) return false;
                char top = stack.Pop();
                if ((ch == ')' && top != '(') ||
                    (ch == '}' && top != '{') ||
                    (ch == ']' && top != '['))
                {
                    return false;
                }
            }
        }
        return stack.Count == 0;
    }

    static void Main()
    {
        string input = "{[()]}";
        Console.WriteLine("String is balanced: " + IsBalanced(input));
    }
}