// Unit Testing using NUnit


// Simulated demo — manual runner (no NuGet required)
using System;

class StringHelper
{
    public string Reverse(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        char[] chars = input.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    public bool IsPalindrome(string input) => input == Reverse(input);

    public string ToTitleCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return input;
        var words = input.Split(' ');
        for (int i = 0; i < words.Length; i++)
            words[i] = char.ToUpper(words[i][0]) + words[i][1..].ToLower();
        return string.Join(' ', words);
    }
}

class NUnitTestingDemo
{
    static int passed = 0, failed = 0;

    static void Assert_That<T>(string testName, T actual, T expected)
    {
        if (Equals(actual, expected)) { Console.WriteLine($"  [PASS] {testName}"); passed++; }
        else { Console.WriteLine($"  [FAIL] {testName} — Expected: {expected}, Got: {actual}"); failed++; }
    }

    static void Main()
    {
        Console.WriteLine("=== Unit Testing with NUnit (simulated) ===\n");
        var h = new StringHelper();

        // [Test] — Reverse
        Assert_That("Reverse('hello') == 'olleh'",      h.Reverse("hello"), "olleh");
        Assert_That("Reverse('') == ''",                h.Reverse(""),      "");

        // [TestCase] — IsPalindrome
        Assert_That("IsPalindrome('madam') == true",    h.IsPalindrome("madam"),   true);
        Assert_That("IsPalindrome('racecar') == true",  h.IsPalindrome("racecar"), true);
        Assert_That("IsPalindrome('hello') == false",   h.IsPalindrome("hello"),   false);

        // [Test] — ToTitleCase
        Assert_That("ToTitleCase('hello world')",       h.ToTitleCase("hello world"), "Hello World");

        Console.WriteLine($"\nResults: {passed} passed, {failed} failed.");
    }
}
