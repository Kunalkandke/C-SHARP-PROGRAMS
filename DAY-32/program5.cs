// Program to Mask Sensitive Data (e.g., masking all but the last 4 digits of a card)
using System;

class Program
{
    static void Main()
    {
        string cardNumber = "1234567812345678";
        Console.WriteLine("Original Card Number: " + cardNumber);
        Console.WriteLine("Masked Card Number: " + MaskCardNumber(cardNumber));
    }

    static string MaskCardNumber(string card)
    {
        if (card.Length <= 4) return card;
        string last4 = card.Substring(card.Length - 4);
        string masked = new string('*', card.Length - 4) + last4;
        return masked;
    }
}