// Program to simulate an ATM State (Account, ATM, Transaction classes)
using System;

class Account
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }

    public Account(string number, decimal balance)
    {
        AccountNumber = number;
        Balance = balance;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount > Balance)
            return false;
        Balance -= amount;
        return true;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }
}

class Transaction
{
    public Account Account { get; set; }
    public decimal Amount { get; set; }

    public Transaction(Account account, decimal amount)
    {
        Account = account;
        Amount = amount;
    }

    public void Execute(string type)
    {
        if (type == "withdraw")
        {
            if (Account.Withdraw(Amount))
                Console.WriteLine($"Withdrawn {Amount}. New Balance: {Account.Balance}");
            else
                Console.WriteLine("Insufficient balance.");
        }
        else if (type == "deposit")
        {
            Account.Deposit(Amount);
            Console.WriteLine($"Deposited {Amount}. New Balance: {Account.Balance}");
        }
    }
}

class ATM
{
    public void ProcessTransaction(Transaction transaction, string type)
    {
        Console.WriteLine("Processing transaction...");
        transaction.Execute(type);
    }
}

class Program
{
    static void Main()
    {
        Account myAccount = new Account("123456", 5000);
        ATM atm = new ATM();

        Transaction t1 = new Transaction(myAccount, 1000);
        atm.ProcessTransaction(t1, "withdraw");

        Transaction t2 = new Transaction(myAccount, 2000);
        atm.ProcessTransaction(t2, "deposit");
    }
}