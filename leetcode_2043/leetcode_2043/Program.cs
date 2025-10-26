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
/// 簡易銀行系統
/// 
/// 解題思路：模擬
/// 已有的帳號為 1 到 n，分別對三種操作進行分析：
/// 
/// transfer 操作：
/// 如果要進行操作的帳號不在已有的帳號中，即 account1 > n 或者 account2 > n，那麼交易無效。
/// 如果帳號 account1 的餘額小於 money，那麼交易無效。
/// 交易有效時，我們將帳號 account1 的餘額減少 money，帳號 account2 的餘額增加 money。
/// 
/// deposit 操作：
/// 如果要進行操作的帳號不在已有的帳號中，即 account > n，那麼交易無效。
/// 交易有效時，我們將帳號 account 的餘額增加 money。
/// 
/// withdraw 操作：
/// 如果要進行操作的帳號不在已有的帳號中，即 account > n，那麼交易無效。
/// 如果帳號 account 的餘額小於 money，那麼交易無效。
/// 交易有效時，我們將帳號 account 的餘額減少 money。
/// 
/// 時間複雜度：O(1)，每次操作都是常數時間
/// 空間複雜度：O(n)，需要儲存 n 個帳戶的餘額
/// </summary>
public class Bank
{
    /// <summary>
    /// 儲存所有帳戶的餘額陣列，索引 i 對應帳號 i+1
    /// </summary>
    public long[] balances;

    /// <summary>
    /// 初始化銀行系統
    /// </summary>
    /// <param name="balance">0 索引的整數陣列，第 (i + 1) 個帳戶的初始餘額為 balance[i]</param>
    public Bank(long[] balance)
    {
        this.balances = balance;
    }

    /// <summary>
    /// 轉帳操作：從 account1 轉帳 money 到 account2
    /// 驗證條件：
    /// 1. 兩個帳號都必須在有效範圍內 (1 到 n)
    /// 2. account1 的餘額必須大於等於轉帳金額
    /// </summary>
    /// <param name="account1">轉出帳號 (1-based)</param>
    /// <param name="account2">轉入帳號 (1-based)</param>
    /// <param name="money">轉帳金額</param>
    /// <returns>交易成功返回 true，否則返回 false</returns>
    public bool Transfer(int account1, int account2, long money)
    {
        // 驗證帳號是否有效：帳號必須在 1 到 n 的範圍內
        if (account1 < 1 || account1 > balances.Length || account2 < 1 || account2 > balances.Length)
        {
            return false;
        }
        
        // 驗證餘額是否足夠：轉出帳號的餘額必須大於等於轉帳金額
        if (balances[account1 - 1] < money)
        {
            return false;
        }
        
        // 執行轉帳：從 account1 扣除金額，並增加到 account2
        balances[account1 - 1] -= money;
        balances[account2 - 1] += money;
        return true;
    }

    /// <summary>
    /// 存款操作：向指定帳號存入金額
    /// 驗證條件：
    /// 1. 帳號必須在有效範圍內 (1 到 n)
    /// </summary>
    /// <param name="account">存款帳號 (1-based)</param>
    /// <param name="money">存款金額</param>
    /// <returns>交易成功返回 true，否則返回 false</returns>
    public bool Deposit(int account, long money)
    {
        // 驗證帳號是否有效：帳號必須在 1 到 n 的範圍內
        if (account < 1 || account > balances.Length)
        {
            return false;
        }
        
        // 執行存款：將金額增加到指定帳號
        balances[account - 1] += money;
        return true;
    }

    /// <summary>
    /// 提款操作：從指定帳號提取金額
    /// 驗證條件：
    /// 1. 帳號必須在有效範圍內 (1 到 n)
    /// 2. 帳號餘額必須大於等於提款金額
    /// </summary>
    /// <param name="account">提款帳號 (1-based)</param>
    /// <param name="money">提款金額</param>
    /// <returns>交易成功返回 true，否則返回 false</returns>
    public bool Withdraw(int account, long money)
    {
        // 驗證帳號是否有效：帳號必須在 1 到 n 的範圍內
        if (account < 1 || account > balances.Length)
        {
            return false;
        }
        
        // 驗證餘額是否足夠：帳號餘額必須大於等於提款金額
        if (balances[account - 1] < money)
        {
            return false;
        }
        
        // 執行提款：從指定帳號扣除金額
        balances[account - 1] -= money;
        return true;
    }
}
