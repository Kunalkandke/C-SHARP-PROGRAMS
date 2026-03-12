using System;

class BankAccount
{
    public string Name;
    public double Balance;

    public void Deposit(double amount)
    {
        Balance += amount;
        Console.WriteLine("Deposited: " + amount);
    }

    public void Withdraw(double amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            Console.WriteLine("Withdrawn: " + amount);
        }
        else
        {
            Console.WriteLine("Insufficient Balance");
        }
    }

    public void ShowBalance()
    {
        Console.WriteLine("Current Balance: " + Balance);
    }
}

class Program
{
    static void Main()
    {
        BankAccount acc = new BankAccount();
        acc.Name = "Amit";
        acc.Balance = 1000;

        acc.Deposit(500);
        acc.Withdraw(300);
        acc.ShowBalance();
    }
}