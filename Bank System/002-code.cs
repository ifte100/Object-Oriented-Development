using System;
using System.Collections.Generic;
using System.Text;

namespace Task_7._1
{
    class TransferTransaction:Transaction
    {
        private Account _fromAccount;
        private Account _toAccount;
        //private decimal _amount;
        //private bool _executed, _reversed;
        private DepositTransaction _deposit;
        private WithdrawTransaction _withdraw;

        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount):base(amount)
        {
            _withdraw = new WithdrawTransaction(fromAccount, amount);
            _deposit = new DepositTransaction(toAccount, amount);
            //this._amount = amount;
            this._fromAccount = fromAccount;
            this._toAccount = toAccount;
        }

        public override void Print()
        {

            Console.WriteLine("Transferring {0} from {1}'s account to {2}'s account.", this._amount, this._fromAccount.Name, this._toAccount.Name);

            //_fromAccount.Print();
            //_toAccount.Print();
            _withdraw.Print();
            _deposit.Print();


        }

        public override void Execute()
        {

            try
            {
                if (Executed == true)
                {
                    throw new InvalidOperationException("THE TRANSACTION HAS ALREADY HAPPENED!");
                }
                if (this._amount > _fromAccount.Balance)
                {
                    throw new InvalidOperationException("Not enough funds to complete the transaction!");
                }
                else
                {
                    _withdraw.Execute();
                    _deposit.Execute();
                    Executed = true;

                }
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

                if (Executed == false)
                {
                    throw new InvalidOperationException("Original transaction did not happen. Nothing to rollback.");
                }
                else if (Executed == true && Reversed == false)
                {
                    _withdraw.Rollback();
                    _deposit.Rollback();
                    Reversed = true;
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
