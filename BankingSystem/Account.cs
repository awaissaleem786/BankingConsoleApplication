using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    class Account
    {
        //list of transactions
        public string Name;
        public int AccountNumber;
        public int Balance;

        //List of transactions

        LinkedList<Transaction> transactionslist;

        public Account(string name, int accountNumber, int balance)
        {
            Name = name;
            AccountNumber = accountNumber;
            Balance = balance;
            transactionslist = new LinkedList<Transaction>();
        }

        public void AddTrascation(int amount, string typeOfAmount)
        {
            Transaction transaction;
            if (typeOfAmount.ToLower() == "debit")
            {
                transaction = new Transaction(amount, 0);
                Balance += amount;
                transactionslist.AddLast(transaction);

            }
            else
            {
                throw new ArgumentException("Invalid transaction type");
            }
        }
        public void Withdraw(int amount ,string typeOfAmount)
        {
            

            if (typeOfAmount.ToLower() == "credit")
            {
                if(amount>Balance)
                {
                    Console.WriteLine("Insufient balance:");
                    return;
                }
            Transaction  transaction = new Transaction(0, amount);
                Balance -= amount;
                transactionslist.AddLast(transaction);
            }
            else
            {
                throw new ArgumentException("Invalid transaction type");
            }
        }
        public void PrintTranstion()
        {
           
            foreach (Transaction t in transactionslist)
            {
                Console.WriteLine(t.ToString());
            }

            //print balance
            //print transaction
        }

    }
}
