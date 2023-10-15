using System;

namespace Individuellt_projekt_Bankomaten___Linus_Aspelöf_SUT23;
// Linus Aspelöf SUT23

/* Seperating User and Account into seperate classes for easier
   manipulation and flexibility*/
class User
{
    public string Username { get; set; }
    public string Pin { get; set; }
    public List<Account> Accounts { get; set; }

    public User(string username, string pin)
    {
        Username = username;
        Pin = pin;
        Accounts = new List<Account>();
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
        new User("user1", "1234")
        {
            Accounts = new List<Account>
            {
                new Account("Lönekonto", 5000.0m),
                new Account("Sparkonto", 10000.0m)
            }
        },
        new User("user2", "5678")
        {
            Accounts = new List<Account>
            {
                new Account("Lönekonto", 7500.0m),
                new Account("Sparkonto", 8000.0m)
            }
        },
        new User("user3", "4321")
        {
            Accounts = new List<Account>
            {
                new Account("Lönekonto", 6000.0m),
                new Account("Sparkonto", 12000.0m)
            }
        },
        new User("user4", "8765")
        {
            Accounts = new List<Account>
            {
                new Account("Lönekonto", 9000.0m),
                new Account("Sparkonto", 6000.0m)
            }
        },
        new User("user5", "9876")
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
            Console.WriteLine("\n\tVälj en av följande alternativ:" +
                "\n\t[1] Se dina konton och saldo" +
                "\n\t[2] Överföring mellan konton" +
                "\n\t[3] Ta ut pengar" +
                "\n\t[4] Logga ut");
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
                        BankTransfer();
                        break;
                    case 3:
                        //Withdraw
                        break;
                    case 4:
                        //Logout
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

    /* Method to verify login credentials
       Looks to find Username and Pin, throws user out if
       3 failed attempts*/
    static void Login()
    {
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
                Console.WriteLine("\n\tInloggning lyckades." +
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
                    return;

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
            Console.WriteLine("\n\tOgiltigt belopp.");
            Console.WriteLine("\n\tTryck \"Enter\" för att Fortsätta ");
            Console.ReadKey();
            return;
        }
    }

    // Method to Withdraw from accounts
    static void Withdraw()
    {

    }

    // Method to logout
    static void Logout()
    {
       
    }
}
