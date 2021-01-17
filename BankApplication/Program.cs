using System;
using BankLibrary;

namespace BankApplication {
    class Program {
        static void Main(string[] args) {
            Bank<Account> bank = new Bank<Account>("Юнит");
            bool alive = true;
            while (alive) {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("1. Открыть счет \t 2. Вывести средства  \t 3. Добавить на счет");
                Console.WriteLine("4. Закрыть счет \t 5. Пропустить день \t 6. Выйти из программы");
                Console.Write("Введите номер пункта: ");
                Console.ForegroundColor = color;

                try {
                    int comand = Convert.ToInt32(Console.ReadLine());

                    switch (comand) {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            Withdraw(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                    }
                    bank.CalculatePercentage();  
                }
                catch (Exception e){
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = color;
                }
            }
        }

        private static void OpenAccount(Bank<Account> bank) {
            Console.Write("Укажите сумму для создания счета: ");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Выберите тип счета: 1. До востребования 2. Депозит - ");

            AccountType accountType;

            int type = Convert.ToInt32(Console.ReadLine());

            if (type == 1)
                accountType = AccountType.Ordinary;
            else
                accountType = AccountType.Deposit;

            bank.Open(accountType, sum, AddSumHandler, WithdrawSumHandler,
                (o, e) => Console.WriteLine(e.Message), CloseAccountHandler, OpenAccountHandler);
        }

        private static void Withdraw(Bank<Account> bank) {
            Console.Write("Укажите сумму для вывода со счета: ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Введите id счета: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Withdraw(sum, id);
        }

        private static void Put(Bank<Account> bank) {
            Console.Write("Укажите сумму, чтобы положить на счет: ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Введите id счета: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Put(sum, id);
        }

        private static void CloseAccount(Bank<Account> bank) {
            Console.Write("Введите id счета, который надо закрыть:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Close(id);
        }

        private static void AddSumHandler(object sender, AccountEventArgs e) {
            Console.WriteLine(e.Message);
        }

        private static void WithdrawSumHandler(object sender, AccountEventArgs e) {
            Console.WriteLine(e.Message);
        }

        private static void CloseAccountHandler(object sender, AccountEventArgs e) {
            Console.WriteLine(e.Message);
        }

        private static void OpenAccountHandler(object sender, AccountEventArgs e) {
            Console.WriteLine(e.Message);
        }
    }
}
