using System;
using System.Collections.Generic;
using System.Text;

namespace Task_7._1
{
    class WithdrawTransaction:Transaction
    {
        private Account _account;
        //private decimal _amount;
        //private bool _executed, _reversed;

        public WithdrawTransaction(Account account, decimal amount):base(amount)
        {
            this._account = account;
        }
        
        public override void Print()
        {
            Console.WriteLine("The name of account: " + this._account.Name);
            //Console.WriteLine("The amount to withdraw: " + this._amount);

            if(_success == true)
            {
                Console.WriteLine("The withdrawal of {0} was done", this._amount);
            }
            if(Reversed == true)
            {
                Console.WriteLine("Reverse of {0} account happened", this._account.Name);
            }
            else if(_success == false)
            {
                Console.WriteLine("Transaction did not happen sorry.");
            }

        }

        public override void Execute()
        {
            

            try
            {
                if (Executed == true)
                {
                    throw new InvalidOperationException("THE TRANSACTION HAS ALREADY HAPPENED!");
                }

                if (this._amount > this._account.Balance)
                {
                    throw new InvalidOperationException("Not enough balance in the account!");
                }
                else
                {
                    base.Execute();
                    _success = _account.Withdraw(_amount);

                }
            }
            catch(InvalidOperationException exception)
            {
                Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
            }

        }

        public override void Rollback()
        {
            try
            {
                if (Reversed == true)
                {
                    throw new InvalidOperationException("The reversing has been already done.");
                }

                if (_success == false)
                {
                    throw new InvalidOperationException("The original withdrawal transaction was unsuccessful. Nothing to rollback.");
                }

                else if (_success == true)
                {
                    base.Rollback();
                    _account.Deposit(_amount);
                    Console.WriteLine("Rollback happened. Amount deposited back.");
                }
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
            }
        }


        public override bool Success
        {
            get { return _success; }
        }

    }
}
