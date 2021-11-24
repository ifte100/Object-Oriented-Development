using System;
using System.Collections.Generic;
using System.Text;

namespace Task_7._1
{
    class DepositTransaction:Transaction
    {
        private Account _account;

        public DepositTransaction(Account account, decimal amount):base(amount)
        {
            this._account = account;
        }

        public override void Print()
        {
            Console.WriteLine("The name of account: " + this._account.Name);
            //Console.WriteLine("The amount to deposit: " + this._amount);

        
            if (_success == true)
            {
                Console.WriteLine("{0} was deposited ", this._amount);
            }
            if (Reversed == true)
            {
                Console.WriteLine("Reverse of {0} account happened", this._account.Name);
            }
            else if (_success == false)
            {
                Console.WriteLine("Transaction did not happen sorry.");
            }
        }

        public override void Execute()
        {

            try
            {

                base.Execute();
                _success = _account.Deposit(_amount);
            }
            catch (InvalidOperationException exception)
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
                    throw new InvalidOperationException("The original deposit transaction was unsuccessful. Nothing to rollback.");
                }

                else if (_success == true)
                {
                    base.Rollback();
                    _account.Withdraw(_amount);
                    Console.WriteLine("Rollback happened. Amount withdrawn out.");
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
