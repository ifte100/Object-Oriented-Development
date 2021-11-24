using System;
using System.Collections.Generic;
using System.Text;

namespace Task_7._1
{
    class Bank
    {
        private List<Account> accounts;
        private List<Transaction>_transactions;

        public Bank()
        {
            accounts = new List<Account>();
            _transactions = new List<Transaction>();
        }

        public void addAccount(Account account)
        {
            accounts.Add(account);
        }

        public Account getAccount(String name)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if(accounts[i].Name == name)
                {
                    return this.accounts[i];
                }
          
            }
            return null;

        }

        public void ExecuteTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            transaction.Execute();
        }

        public void RollbackTransaction(Transaction transaction)
        {
            transaction.Rollback();
        }

        public void PrintTransactionHistory()
        {
            for(int i = 0; i < _transactions.Count; i++)
            {
                if (_transactions[i].Reversed == false)
                {
                    Console.Write("ID:" + i);
                    Console.WriteLine();
                    Console.WriteLine(new string('-', 30));
                    Console.WriteLine(_transactions[i].DateStamp);
                    _transactions[i].Print();
                    if (_transactions[i].Executed == true)
                    {
                        Console.WriteLine("Already an executed Transaction");
                    }
                    if (_transactions[i].Success == true)
                    {
                        Console.WriteLine("Already a successful Transaction");
                    }
                    if (_transactions[i].Reversed == false)
                    {
                        Console.WriteLine("Not rolled back");
                    }
                }
                
                Console.WriteLine(new string('-', 30));
            }
        }

        public List<Transaction> transaction_List
        {
            get { return _transactions; }
        }

        public List<Account> account_List
        {
            get { return accounts; }
        }
    }
}