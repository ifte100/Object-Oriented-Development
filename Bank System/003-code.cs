using System;
using System.Collections.Generic;
using System.Text;

namespace Task_7._1
{
    abstract class Transaction
    {
        protected decimal _amount;
        protected bool _success;
        private bool _executed;
        private bool _reversed;
        private DateTime _dateStamp;

        public Transaction(decimal amount)
        {
            this._amount = amount;
        }

        public abstract bool Success { get;}

        public bool Executed
        {
            get { return _executed; }
            protected set { _executed = value; }
        }
        public bool Reversed
        {
            get { return _reversed; }
            protected set { _reversed = value; }
        }

        public DateTime DateStamp { get { return _dateStamp; } }

        public abstract void Print();

        public virtual void Execute()
        {
            _dateStamp = DateTime.Now;
            Console.WriteLine(_dateStamp.ToString());
            Executed = true;
        }

        public virtual void Rollback()
        {
            _dateStamp = DateTime.Now;
            Console.WriteLine(_dateStamp.ToString());
            Reversed = true;
        }
    }
}
