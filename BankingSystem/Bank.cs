using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;

namespace BankingSystem
{
    class Bank
    {       

        public void dataInsert()
        {
            string connectionString = "Server=localhost;Database=BankingSystem;Integrated Security=True;";
            string query = "INSERT INTO Accounts (AccountNumber, Name,Balance) VALUES (@AccountNumber,@Name,@Balance)";

            using (var connection =new SqlConnection(connectionString))
            {

                foreach(var employee in accountlist)
                {
                    using (var command =new SqlCommand(query,connection))
                    {
                        command.Parameters.AddWithValue("@AccountNumber", AccountNumber);
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@Balance", Balance);
                    }
                }
            }
        }
        LinkedList<Account>accountlist;

        public Bank()
        {
            accountlist = new LinkedList<Account>();
        }

        public static void MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("======================= *** WELCOME TO HBL BANK *** =======================");
            Console.WriteLine();
            Console.WriteLine("=============================== *** ENTER YOUR CHOICE *** ===============================");
            Console.WriteLine();
            Console.WriteLine("1]. ADD Account \t\t\t 2].Delete Account \t\t\t 3].Check Balance");
            Console.WriteLine();
            Console.WriteLine("4]. ADD Funds \t\t\t 5].Withdraw funds      \t\t\t 6].print Transtions");
            Console.WriteLine();
            Console.WriteLine("=============================== *** ENTER 0 TO EXIT *** ===============================");
        }
       
        public void AddAccount()
        {
            Console.WriteLine("Enter the Name:");
           string  Name = Console.ReadLine();
            Console.WriteLine("Enter the Account number:");
          int  AccountNumber= int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the Balance:");
          int  Balance =int.Parse(Console.ReadLine());
            Account newAccount = new Account(Name, AccountNumber, Balance);
            accountlist.AddLast(newAccount);
            Console.WriteLine("Account added successfully!");
            Start();

        }
        public void RemoveAccount(int number)
        {
            Account accountToRemove = FindAccountNumber(number);

            //accountToRemove.AddTransaction()

            if (accountToRemove != null)
            {
                accountlist.Remove(accountToRemove);
                Console.WriteLine("Account has successfull remove");
            }
            Start();
        }

        public Account FindAccountNumber(int number)
        {
            foreach (Account account in accountlist)
            {
                if (account.AccountNumber == number)
                {
                    return account;
                }
            }
            return null;
        }
        public void AddFundsTransaction(int accountNumber, int amount)
        {
            Account account = FindAccountNumber(accountNumber);
            if (account != null)
            {

                account.AddTrascation(amount,"debit");
                Console.WriteLine("Funds added successfully!");
                Start();
            }
            else
            {
                Console.WriteLine("Account not found:");
            }

        }
        public void WithDrawTransaction(int accountNumber, int amount)
        {
            Account account = FindAccountNumber(accountNumber);
            if (account != null)
            {
                account.Withdraw(amount, "credit");
                Console.WriteLine("Funds withdrawn successfully!");
                Start();

            }
            else
            {
                Console.WriteLine("Account Not found");
            }
        }

        public void CheckBalance(int userAccountNumber)
        {
            foreach(Account account in accountlist)
            {
                if(account.AccountNumber==userAccountNumber)
                {
                    Console.WriteLine($"Account Balance for {account.Name}:{account.Balance}");
                   Start();
                }
                else
                {
                    Console.WriteLine("Account Not found:");
                }
            }
        }
       
        public void Start()
        {
            MainMenu();
            Console.WriteLine("Enter the choice:");
            int choice = int.Parse(Console.ReadLine());
            while (true)
            {
                switch (choice)
                {
                    case 1:
                        AddAccount();
                        break;
                    case 2:
                        Console.WriteLine("Enter the Account Number to Remove:");
                        int number = int.Parse(Console.ReadLine());
                        RemoveAccount(number);
                        break;
                    case 3:
                        Console.WriteLine("Enter the Account Number to Check Balance:");
                        int balance = int.Parse(Console.ReadLine());
                        CheckBalance(balance);
                        break;
                    case 4:
                        Console.WriteLine("Enter the Account Number to Add Funds:");
                        int accountNumber = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the amount to add:");
                        int  amount = int.Parse(Console.ReadLine());
                        AddFundsTransaction(accountNumber, amount);
                        break;
                    case 5:
                        Console.WriteLine("Enter the Account Number to withdraw Funds:");
                        int accountNum = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the amount to withdraw:");
                        int totalamount = int.Parse(Console.ReadLine());
                        WithDrawTransaction(accountNum, totalamount);
                        break;
                    case 6:
                        Console.WriteLine("Enter the Account Number to withdraw Funds:");
                        int checkaccount = int.Parse(Console.ReadLine());
                        CheckBalance(checkaccount);
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }
       

    }
}
