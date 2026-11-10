namespace leetcode_3693;

class Program
{
    /// <summary>
    /// 3693. Climbing Stairs II
    /// https://leetcode.com/problems/climbing-stairs-ii/description/
    ///
    /// English:
    /// You are climbing a staircase with n + 1 steps, numbered from 0 to n.
    /// You are also given a 1-indexed integer array costs of length n,
    /// where costs[i] is the cost of step i.
    /// From step i, you can jump only to step i + 1, i + 2, or i + 3.
    /// The cost of jumping from step i to step j is defined as:
    /// costs[j] + (j - i)².
    /// You start from step 0 with cost = 0.
    /// Return the minimum total cost to reach step n.
    ///
    /// 3693. 爬樓梯 II
    /// https://leetcode.cn/problems/climbing-stairs-ii/description/
    ///
    /// 繁體中文：
    /// 你正在攀爬一座共有 n + 1 個階梯的樓梯，階梯編號從 0 到 n。
    /// 給定一個長度為 n、索引從 1 開始的整數陣列 costs，
    /// 其中 costs[i] 表示第 i 個階梯的費用。
    /// 從階梯 i 出發，你只能跳到階梯 i + 1、i + 2 或 i + 3。
    /// 從階梯 i 跳到階梯 j 的費用定義為：
    /// costs[j] + (j - i)²，也就是目的階梯的費用加上跳躍距離的平方。
    /// 你從階梯 0 出發，初始費用為 0。
    /// 請回傳到達階梯 n 所需的最小總費用。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
