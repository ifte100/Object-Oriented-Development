using System;
using System.Collections.Generic;
using System.Text;

namespace Task_7._1
{
    class Account
    {
        private decimal _balance;
        private string _name;

        public Account(String name, decimal balance)
        {
            this._balance = balance;
            this._name = name;
        }

        public bool Deposit(decimal amount)
        {

            if (amount < 0)
            {
                Console.WriteLine("The transaction failed. Input positive monetary value!");
                return false;
            }
            else
            {
                this._balance += amount;
                //Console.WriteLine("Succesful transaction! Added {0} to the account.", amount);
                return true;
            }

        }

        public bool Withdraw(decimal amount)
        {
            //if (amount > this._balance)
            //{
            //    Console.WriteLine("Insufficient fund!!!");
            //    return false;
            //}
            if (amount < 0)
            {
                Console.WriteLine("The transaction failed. Input positive monetary value!");
                return false;
            }
            else
            {
                this._balance -= amount;
                // Console.WriteLine("Successful!! Withdrawn {0} amount of money.", amount);
                return true;
            }
        }

        public void Print()
        {
            Console.WriteLine("The name of the account is: " + this._name);
            Console.WriteLine("The balance in the account is: " + this._balance);
        }

        public String Name
        {
            get { return _name; }
        }

        public decimal Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

    }
}
