namespace leetcode_2043;

class Program
{
    /// <summary>
    /// 2043. Simple Bank System
    /// https://leetcode.com/problems/simple-bank-system/description/?envType=daily-question&envId=2025-10-26
    /// 2043. 简易银行系统
    /// https://leetcode.cn/problems/simple-bank-system/description/?envType=daily-question&envId=2025-10-26
    /// 
    /// 您被要求為一家受歡迎的銀行編寫程式，以自動化所有 incoming 交易（轉帳、存款和提款）。
    /// 銀行有 n 個帳戶，編號從 1 到 n。
    /// 每個帳戶的初始餘額存儲在 0 索引的整數陣列 balance 中，第 (i + 1) 個帳戶的初始餘額為 balance[i]。
    /// 
    /// 執行所有有效的交易。一個交易是有效的，如果：
    /// 
    /// 給定的帳戶號碼在 1 到 n 之間，以及
    /// 從帳戶中提取或轉帳的金額小於或等於帳戶的餘額。
    /// 
    /// 實作 Bank 類別：
    /// 
    /// Bank(long[] balance) 使用 0 索引的整數陣列 balance 初始化物件。
    /// 
    /// boolean transfer(int account1, int account2, long money) 從編號為 account1 的帳戶轉帳 money 美元到編號為 
    /// account2 的帳戶。如果交易成功，返回 true，否則返回 false。
    /// 
    /// boolean deposit(int account, long money) 將 money 美元存入編號為 account 的帳戶。如果交易成功，返回 true，
    /// 否則返回 false。
    /// 
    /// boolean withdraw(int account, long money) 從編號為 account 的帳戶提取 money 美元。如果交易成功，
    /// 返回 true，否則返回 false。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        long[] initialBalance = { 10, 100, 20, 50, 30 };
        Bank bank = new Bank(initialBalance);

        // 測試 Transfer
        Console.WriteLine("Transfer(3, 4, 10): " + bank.Transfer(3, 4, 10)); // true
        Console.WriteLine("Transfer(1, 2, 30): " + bank.Transfer(1, 2, 30)); // false (餘額不足)
        Console.WriteLine("Transfer(10, 1, 10): " + bank.Transfer(10, 1, 10)); // false (帳戶不存在)

        // 測試 Deposit
        Console.WriteLine("Deposit(5, 40): " + bank.Deposit(5, 40)); // true
        Console.WriteLine("Deposit(6, 10): " + bank.Deposit(6, 10)); // false (帳戶不存在)

        // 測試 Withdraw
        Console.WriteLine("Withdraw(5, 20): " + bank.Withdraw(5, 20)); // true
        Console.WriteLine("Withdraw(1, 50): " + bank.Withdraw(1, 50)); // false (餘額不足)
        Console.WriteLine("Withdraw(0, 10): " + bank.Withdraw(0, 10)); // false (帳戶不存在)

        // 印出最終餘額
        Console.WriteLine("Final balances:");
        for (int i = 0; i < initialBalance.Length; i++)
        {
            Console.WriteLine($"Account {i + 1}: {bank.balances[i]}");
        }

        // 官方測試資料
        Console.WriteLine("\nOfficial Test Case:");
        long[] officialBalance = { 10, 100, 20, 50, 30 };
        Bank officialBank = new Bank(officialBalance);

        // 初始化 (null)
        Console.Write("Bank initialized: null\n");

        // withdraw(3, 10) -> true
        Console.WriteLine("withdraw(3, 10): " + officialBank.Withdraw(3, 10));

        // transfer(5, 1, 20) -> true
        Console.WriteLine("transfer(5, 1, 20): " + officialBank.Transfer(5, 1, 20));

        // deposit(5, 20) -> true
        Console.WriteLine("deposit(5, 20): " + officialBank.Deposit(5, 20));

        // transfer(3, 4, 15) -> false
        Console.WriteLine("transfer(3, 4, 15): " + officialBank.Transfer(3, 4, 15));

        // withdraw(10, 50) -> false
        Console.WriteLine("withdraw(10, 50): " + officialBank.Withdraw(10, 50));
    }
}


/// <summary>
/// 
/// </summary>
public class Bank
{
    public long[] balances;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="balance"></param>
    public Bank(long[] balance)
    {
        this.balances = balance;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="account1"></param>
    /// <param name="account2"></param>
    /// <param name="money"></param>
    /// <returns></returns>
    public bool Transfer(int account1, int account2, long money)
    {
        if (account1 < 1 || account1 > balances.Length || account2 < 1 || account2 > balances.Length)
        {
            return false;
        }
        if (balances[account1 - 1] < money)
        {
            return false;
        }
        balances[account1 - 1] -= money;
        balances[account2 - 1] += money;
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="account"></param>
    /// <param name="money"></param>
    /// <returns></returns>
    public bool Deposit(int account, long money)
    {
        if (account < 1 || account > balances.Length)
        {
            return false;
        }
        balances[account - 1] += money;
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="account"></param>
    /// <param name="money"></param>
    /// <returns></returns>
    public bool Withdraw(int account, long money)
    {
        if (account < 1 || account > balances.Length)
        {
            return false;
        }
        if (balances[account - 1] < money)
        {
            return false;
        }
        balances[account - 1] -= money;
        return true;
    }
}
