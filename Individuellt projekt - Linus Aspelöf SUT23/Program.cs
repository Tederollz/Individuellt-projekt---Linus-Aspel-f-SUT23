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
    // Creates a static list with the User object for use in other methods
    static List<User> users = new List<User>
    {
        new User("user1", "1234"),
        new User("user2", "5678"),
        new User("user3", "4321"),
        new User("user4", "8765"),
        new User("user5", "9876")
    };

    static User currentUser = null;
    static int loginAttempts = 0;
    static bool run = true;

    static void Main(string[] args)
    {
        Login();
    }

    // Menu for easy navigation
    static void Menu()
    {

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
                        //BankBalance
                        break;
                    case 2:
                        //BankTransfer
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
            }
            Console.WriteLine("\n\tTryck \"Enter\" för att Fortsätta");
            Console.ReadKey();
        }
    }

    // Method to verify login credentials
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

    }

    // Method to transfer between accounts
    static void BankTransfer()
    {

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
