using System;
namespace BankLibrary {
    public abstract class Account : IAccount {
        protected internal event AccountStateHandler Withdrawed; // вывод денег
        protected internal event AccountStateHandler Added; // внос денег
        protected internal event AccountStateHandler Opened; // открытие счета
        protected internal event AccountStateHandler Closed; // закрытие счета
        protected internal event AccountStateHandler Calculated; // начисление процентов на счет

        static int counter = 0;
        protected int _day = 0; // дней с момента открытия счета 


        public Account(decimal sum, int percentage) {
            Sum = sum;
            Percentage = percentage;
            Id = ++counter;
        }

        public decimal Sum { get; private set; } // текущая сумма
        public int Percentage { get; private set; } // процент начислений
        public int Id { get; private set; } // уникальный идентификатор счета

        private void CallEvent(AccountEventArgs e, AccountStateHandler handler) {
            if (e != null)
                handler?.Invoke(this, e);
        }

        protected virtual void OnOpened(AccountEventArgs e) {
            CallEvent(e, Opened);
        }

        protected virtual void OnAdded(AccountEventArgs e) {
            CallEvent(e, Added);
        }

        protected virtual void OnWithdraw(AccountEventArgs e) {
            CallEvent(e, Withdrawed);
        }

        protected virtual void OnCalculated(AccountEventArgs e) {
            CallEvent(e, Calculated);
        }

        protected virtual void OnClosed(AccountEventArgs e) {
            CallEvent(e, Closed);
        }

        public virtual void Put(decimal sum) {
            Sum += sum;
            OnAdded(new AccountEventArgs($"На счет поступило : {0}", sum));
        }

        public decimal Withdraw(decimal sum) {
            decimal result = 0;
            if (Sum > sum) {
                Sum -= sum;
                result = sum;
                OnWithdraw(new AccountEventArgs($"Сумма {sum} снята со счета {Id}", sum));
            } else {
                OnWithdraw(new AccountEventArgs($"Недостаточно денег на счете {Id}", 0));
            }
            return result;
        }
    }
}
