﻿using System;

namespace Individuellt_projekt_Bankomaten___Linus_Aspelöf_SUT23;
// Linus Aspelöf SUT23

/* Seperating User and Account into seperate classes for easier
   manipulation and flexibility*/
class User
{
    public string Username { get; set; }
    public string Pin { get; set; }
    public List<Account> Accounts { get; set; }
    public string[] UserDetails { get; set; }

    public User(string username, string pin, string[] userDetails)
    {
        Username = username;
        Pin = pin;
        Accounts = new List<Account>();
        UserDetails = userDetails;
    }
}

class Account
{
    public string Name { get; set; }
    public decimal Balance { get; set; }

    public Account(string name, decimal balance)
    {
        Name = name;
        Balance = balance;
    }
}
class Program
{
    /* Creates a static list with the User object for use in other methods
       Also adds a list element with accounts for each user*/
    static List<User> users = new List<User>
    {
        new User("user1", "1234", new string[] {"John Doe", "user1@example.com"})
        {
            Accounts = new List<Account>
            {
                new Account("Lönekonto", 5000.0m),
                new Account("Sparkonto", 10000.0m)
            }
        },
        new User("user2", "5678", new string[] {"John Doe", "user2@example.com"})
        {
            Accounts = new List<Account>
            {
                new Account("Lönekonto", 7500.0m),
                new Account("Sparkonto", 8000.0m)
            }
        },
        new User("user3", "4321", new string[] {"Jane Doe", "user3@example.com"})
        {
            Accounts = new List<Account>
            {
                new Account("Lönekonto", 6000.0m),
                new Account("Sparkonto", 12000.0m)
            }
        },
        new User("user4", "8765", new string[] {"Tony Stark", "user4@example.com"})
        {
            Accounts = new List<Account>
            {
                new Account("Lönekonto", 9000.0m),
                new Account("Sparkonto", 6000.0m)
            }
        },
        new User("user5", "9876", new string[] {"Obi-Wan Kenobi", "user5@example.com"})
        {
            Accounts = new List<Account>
            {
                new Account("Lönekonto", 6500.0m),
                new Account("Sparkonto", 10500.0m)
            }
        }
    };

    static User currentUser = null;
    static int loginAttempts = 0;

    static void Main(string[] args)
    {
        Login();
    }

    // Menu for easy navigation
    static void Menu()
    {
        bool run = true;

        while (run)
        {
            Console.Clear();
            Console.WriteLine("\n\tVälj en av följande alternativ:\n" +
                "\n\t[1] Se dina konton och saldo" +
                "\n\t[2] Visa E-postadress" +
                "\n\t[3] Överföring mellan konton" +
                "\n\t[4] Ta ut pengar" +
                "\n\t[5] Logga ut");
            Console.Write("\n\t");

            if (int.TryParse(Console.ReadLine(), out int input))
            {
                switch (input)
                {
                    case 1:
                        BankBalance();
                        Console.WriteLine("\n\tTryck \"Enter\" för att Fortsätta");
                        Console.ReadKey();
                        break;
                    case 2:
                        ViewEmail();
                        break;
                    case 3:
                        BankTransfer();
                        break;
                    case 4:
                        Withdraw();
                        break;
                    case 5:
                        Logout();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\n\tError: Ogiltigt input, Vänligen ange 1-4.");
                Console.WriteLine("\n\tTryck \"Enter\" för att Fortsätta");
                Console.ReadKey();
            }
        }
    }

    static void ViewEmail()
    {
        Console.Clear();
        Console.WriteLine("\n\t-- E-postadress --");
        Console.WriteLine($"\n\tE-postadress: {currentUser.UserDetails[1]}");

        Console.WriteLine("\n\tTryck \"Enter\" för att Fortsätta ");
        Console.ReadKey();
    }

    /* Method to verify login credentials
       Looks to find Username and Pin, throws user out if
       3 failed attempts*/
    static void Login()
    {
        Console.Clear();
        Console.WriteLine("\n\tVälkommen till Bankomaten!");
        while (loginAttempts < 3)
        {
            Console.Write("\n\tAnge ditt Användar-ID: ");
            string username = Console.ReadLine();

            Console.Write("\n\tAnge din pinkod: ");
            string pin = Console.ReadLine();

            User UserLogin = users.Find(u => u.Username == username && u.Pin == pin);

            if (UserLogin != null)
            {
                currentUser = UserLogin;
                loginAttempts = 0;
                Console.WriteLine($"\n\tInloggning lyckades. Välkommen, {currentUser.UserDetails[0]}!" +
                    "\n\tTryck \"Enter\" för att Fortsätta ");
                Console.ReadKey();
                Menu();
            }
            else
            {
                Console.WriteLine("\n\tFelaktigt användarnamn eller pinkod. Försök igen.");
                loginAttempts++;
            }
        }
        Console.WriteLine("\n\tFör många felaktiga inloggningsförsök. " +
            "\n\tProgrammet avslutas....");
        Console.ReadKey();
    }

    // Method to check bank balance
    static void BankBalance()
    {
        Console.WriteLine("\n\tDina konton & saldo:");

        foreach (var account in currentUser.Accounts)
        {
            Console.WriteLine($"\n\t{account.Name}: {account.Balance:C}");
        }
    }

    // Method to transfer between accounts
    static void BankTransfer()
    {
        Console.Clear();
        Console.WriteLine("\n\t-- Överföring mellan konton --");
        BankBalance();

        Console.Write("\n\tVälj konto att ta pengar från: ");
        string fromAcct = Console.ReadLine();

        Console.Write("\n\tVälj konto att flytta pengar till: ");
        string toAcct = Console.ReadLine();

        Console.Write("\n\tAnge summa att flytta: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount)) 
        {
            Account fromAccount = currentUser.Accounts.Find(acc => acc.Name == fromAcct);
            Account toAccount = currentUser.Accounts.Find(acc => acc.Name == toAcct);

            if (fromAccount != null && toAccount != null)
            {
                if (fromAccount.Balance >= amount)
                {
                    fromAccount.Balance -= amount;
                    toAccount.Balance += amount;
                    Console.WriteLine("\n\tÖverföringen lyckades.");
                    BankBalance();
                    Console.WriteLine("\n\tTryck \"Enter\" för att Fortsätta ");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\n\tError: Du har för lite pengar på kontot.");
                    Console.WriteLine("\n\tTryck \"Enter\" för att Fortsätta ");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("\n\tEtt eller båda konton finns inte.");
                Console.WriteLine("\n\tTryck \"Enter\" för att Fortsätta ");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("\n\tError: Ogiltigt input.");
            Console.WriteLine("\n\tTryck \"Enter\" för att Fortsätta ");
            Console.ReadKey();
        }
    }

    // Method to Withdraw from accounts
    static void Withdraw()
    {
        Console.Clear();
        Console.WriteLine("\n\t-- Ta ut pengar --");
        BankBalance();

        Console.Write("\n\tVälj konto att ta ut pengar från: ");
        string FromAcct = Console.ReadLine();

        Console.Write("\n\tAnge summa att ta ut: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            Account account = currentUser.Accounts.Find(acc => acc.Name == FromAcct);

            if(account != null)
            {
                if(account.Balance >= amount)
                {
                    Console.Write("\n\tAnge din pinkod för bekräftelse: ");
                    string pin = Console.ReadLine();
                    if (pin == currentUser.Pin)
                    {
                        account.Balance -= amount;
                        Console.WriteLine($"\n\tUttaget lyckades." +
                            $"\n\t{FromAcct}: {account.Balance:C}");
                        Console.WriteLine("\n\tTryck \"Enter\" för att Fortsätta ");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("\n\tFelaktig pinkod.");
                        Console.WriteLine("\n\tTryck \"Enter\" för att Fortsätta ");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("\n\tError: Du har för lite pengar på kontot.");
                    Console.WriteLine("\n\tTryck \"Enter\" för att Fortsätta ");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("\n\tKontot finns inte.");
                Console.WriteLine("\n\tTryck \"Enter\" för att Fortsätta ");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("\n\tError: Ogiltigt input.");
            Console.WriteLine("\n\tTryck \"Enter\" för att Fortsätta ");
            Console.ReadKey();
        }
    }

    // Method to logout and return to login screen
    static void Logout()
    {
        currentUser = null;
        Console.Clear();
        Console.WriteLine("\n\tDu loggas ut...");
        Console.WriteLine("\n\tTryck \"Enter\" för att Fortsätta ");
        Console.ReadKey();
        Login();
    }
}
