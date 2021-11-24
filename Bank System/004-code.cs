using System;
using System.Collections.Generic;

namespace Task_7._1
{
    class BankSystem
    {
        public enum MenuOption
        {
            WITHDRAW,
            DEPOSIT,
            PRINT,
            TRANSFER,
            ADDACCOUNT,
            PRINTTRANSACTIONHISTORY,
            QUIT
        }

        public static MenuOption ReadUserOption()
        {

            string option;
            int value;

            do
            {
                Console.WriteLine("1. Withdraw Money");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Print");
                Console.WriteLine("4. Transfer Money");
                Console.WriteLine("5. Add Account");
                Console.WriteLine("6. Print transaction history");
                Console.WriteLine("7. Quit");
                option = Console.ReadLine();
                bool result = Int32.TryParse(option, out value);
                if (!result)
                {
                    Console.WriteLine("You should input numeric value.");
                }
                else if (result == true)
                {
                    Console.WriteLine("You have chosen {0}.", value);
                }
            } while (value < 1 || value > 7);

            return (MenuOption)(value - 1);
        }

        public static void DoDeposit(Bank bank)
        {
            Account account = findAccount(bank);
            if (account != null)
            {
                Console.WriteLine("Amount to deposit?: ");
                try
                {
                    decimal depAmount = Convert.ToDecimal(Console.ReadLine());
                    DepositTransaction firstDepo = new DepositTransaction(account, depAmount);
                    bank.ExecuteTransaction(firstDepo);
                    firstDepo.Print();

                    if (firstDepo.Success == true)
                    {
                        Console.WriteLine("Do you want to rollback the deposit? Type yes or no.");
                        string asking = Console.ReadLine();
                        asking.ToLower();
                        if (asking == "yes")
                        {
                            firstDepo.Rollback();
                        }
                        else if (asking == "no")
                        {
                            Console.WriteLine("End of the transaction!");
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Amount entered is not a monetary value.");
                }
            }
        }


        public static void DoWithdraw(Bank bank)
        {
            Account account = findAccount(bank);
            if (account != null)
            {
                Console.WriteLine("Amount to withdraw?: ");
                try
                {
                    decimal withAmount = Convert.ToDecimal(Console.ReadLine());
                    WithdrawTransaction firstTrans = new WithdrawTransaction(account, withAmount);
                    bank.ExecuteTransaction(firstTrans);
                    firstTrans.Print();

                    if (firstTrans.Success == true)
                    {
                        Console.WriteLine("Do you want to rollback the withdrawal? Type yes or no.");
                        string asking = Console.ReadLine();
                        asking.ToLower();
                        if (asking == "yes")
                        {
                            firstTrans.Rollback();
                        }
                        else if (asking == "no")
                        {
                            Console.WriteLine("End of the transaction!");
                        }
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("Amount entered is not a monetary value.");
                }
            }

        }

        public static void DoTransfer(Bank bank)
        {
            Console.WriteLine("Transferring from?: ");
            Account fromAccount = findAccount(bank);


            if (fromAccount != null)
            {
                try
                {
                    Console.WriteLine("Transferring to?: ");
                    Account toAccount = findAccount(bank);

                    if (toAccount != null)
                    {
                        Console.WriteLine("Amount to Transfer?: ");
                        decimal transferAmount = Convert.ToDecimal(Console.ReadLine());
                        if (transferAmount > 0)
                        {
                            TransferTransaction firstTransfer = new TransferTransaction(fromAccount, toAccount, transferAmount);
                            bank.ExecuteTransaction(firstTransfer);
                            firstTransfer.Print();
                            if (firstTransfer.Executed == true)
                            {
                                Console.WriteLine("Do you want to rollback the transfer? Type yes or no.");
                                string asking = Console.ReadLine();
                                asking.ToLower();
                                if (asking == "yes")
                                {
                                    firstTransfer.Rollback();
                                }
                                else if (asking == "no")
                                {
                                    Console.WriteLine("End of the transaction!");
                                }
                            }
                        }
                        else if (transferAmount < 0)
                        {
                            throw new InvalidOperationException("Please input postive monetary value.");
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Amount entered is not a monetary value.");
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
                }
            }
        }

        public static void DoPrint(Bank bank)
        {
            Account account = findAccount(bank);
            if (account != null)
            {
                account.Print();
            }
        }

        public static void addAccount(Bank bank)
        {
            Console.WriteLine("Please input name of the account: ");
            string name = Console.ReadLine();
            Console.WriteLine("Starting balance in the account: ");
            decimal balance = Convert.ToDecimal(Console.ReadLine());

            if (balance > 0)
            {
                Account newAcc = new Account(name, balance);
                bank.addAccount(newAcc);
            }
            else
            {
                Console.WriteLine("Please input positive monetary value.");
            }
        }

        private static Account findAccount(Bank bank)
        {
            Console.WriteLine("Please enter name of the account: ");
            String name = Console.ReadLine();
            Account getName = bank.getAccount(name);

            if (getName == null)
            {
                Console.WriteLine("No account was found with the name." + getName);
                return null;
            }
            else
            {
                return bank.getAccount(name);
            }

        }

        public static void DoRollback(Bank bank)
        {

            List<Transaction> transaction = bank.transaction_List;
            if (transaction.Count != 0)
            {
                bank.PrintTransactionHistory();
                Console.WriteLine("Do you want to rollback a transaction? yes/no?");
                Console.Write(">>>");
                string asking = Console.ReadLine().ToLower();

                try
                {
                    if (asking == "yes")
                    {
                        Console.WriteLine("Which transaction you want to rollback? Choose ID number: ");

                        int index = Convert.ToInt32(Console.ReadLine());

                        for (int i = 0; i < transaction.Count; i++)
                        {
                            if (transaction[index] == transaction[i])
                            {
                                bank.RollbackTransaction(transaction[i]);
                            }
                          
                        }

                    }
                    else if(asking == "no")
                    {
                        Console.WriteLine("END");
                    }
                    else
                    {
                        Console.WriteLine("Please only enter yes or no.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please input numbers only.");
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch(ArgumentOutOfRangeException)
                {
                    Console.WriteLine("ID number does not exist. Unsuccesful rollback.");
                }
            }
            else
            {
                Console.WriteLine("No transaction was made in this bank.");
            }


        }

        static void Main(string[] args)
        {
            Bank firstBank = new Bank();
            MenuOption selection;

            do
            {
                selection = ReadUserOption();

                switch (selection)
                {
                    case MenuOption.WITHDRAW:
                        DoWithdraw(firstBank);
                        break;
                    case MenuOption.DEPOSIT:
                        DoDeposit(firstBank);
                        break;
                    case MenuOption.TRANSFER:
                        DoTransfer(firstBank);
                        break;
                    case MenuOption.PRINT:
                        DoPrint(firstBank);
                        break;
                    case MenuOption.ADDACCOUNT:
                        addAccount(firstBank);
                        break;
                    case MenuOption.PRINTTRANSACTIONHISTORY:
                        DoRollback(firstBank);
                        break;
                    case MenuOption.QUIT:
                        Console.WriteLine("See you again!");
                        break;

                }
            } while (selection != MenuOption.QUIT);



        }
    }
}
