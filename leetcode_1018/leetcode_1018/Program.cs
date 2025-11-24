using System;
using System.Collections.Generic;
using System.Linq;

namespace leetcode_1018;

class Program
{
    /// <summary>
    /// 1018. Binary Prefix Divisible By 5
    /// https://leetcode.com/problems/binary-prefix-divisible-by-5/description/?envType=daily-question&envId=2025-11-24
    /// 1018. 可被 5 整除的二进制前缀
    /// https://leetcode.cn/problems/binary-prefix-divisible-by-5/description/?envType=daily-question&envId=2025-11-24
    ///
    /// Problem (EN):
    /// You are given a binary array nums (0-indexed).
    /// We define x_i as the number whose binary representation is the subarray nums[0..i]
    /// (from most-significant-bit to least-significant-bit).
    /// Return an array of booleans answer where answer[i] is true if x_i is divisible by 5.
    ///
    /// 題目（中文翻譯）:
    /// 給定一個二進位陣列 nums（0-indexed）。
    /// 我們定義 x_i 為一個數字，它的二進位表示為子陣列 nums[0..i]（從最高位到最低位）。
    /// 回傳布林陣列 answer，使得 answer[i] 為 true 當且僅當 x_i 能被 5 整除。
    ///
    /// 範例：
    /// - nums = [1,0,1] => x0=1, x1=2, x2=5 => answer = [false, false, true]
    /// </summary>
    /// <param name="args">命令列引數</param>
    static void Main(string[] args)
    {
        // 測試案例
        var tests = new int[][] {
            new int[] {1,0,1},  // example from problem
            new int[] {0},      // 0 is divisible by 5
            new int[] {0,0},    // consecutive zeros remain 0 -> true
            new int[] {1,1,1,1,1} // arbitrary example
        };

        foreach (var test in tests)
        {
            var result = PrefixesDivBy5(test);
            Console.WriteLine($"Input: [{string.Join(',', test)}] => Output: {FormatBoolList(result)}");
        }
    }

    /// <summary>
    /// 逐步維持餘數 r = x_i % 5，而不是計算完整 x_i（避免 overflow）：
    ///
    /// 解題說明：
    /// 我們不需要完整地計算每個 x_i 的整數值 (這可能會溢位)，只需要知道 x_i % 5 的餘數。
    /// 若將前一個值 x_{i-1} 左移一位再加上當前位元 bit，代表 x_i = x_{i-1} * 2 + bit。
    /// 因此 r_i = (r_{i-1} * 2 + bit) % 5；當 r_i == 0 時，表示能被 5 整除。
    ///
    /// 時間複雜度：O(n)，n 為輸入陣列長度（每個元素做 O(1) 運算）
    /// 空間複雜度：O(n)，回傳陣列所需
    /// </summary>
    /// <param name="nums">輸入的二進位陣列</param>
    /// <returns>一個 IList&lt;bool&gt;，表示對應 prefix 是否可被 5 整除</returns>
    public static IList<bool> PrefixesDivBy5(int[] nums)
    {
        var res = new List<bool>(nums.Length);
        int r = 0;
        foreach(var bit in nums)
        {
            // r * 2: 左移一位 (相當於乘以 2)
            // + bit: 將當前位加入
            // % 5: 只保留與 5 的餘數，讓 r 始終維持在 [0,4] 範圍內，避免溢位
            r = (r * 2 + bit) % 5;
            res.Add(r == 0); // 若餘數為 0，代表可被 5 整除
        }
        return res;        
    }

    // 將 IList<bool> 格式化以便輸出 (轉成小寫 true/false 的字串)
    private static string FormatBoolList(IList<bool> arr)
    {
        return "[" + string.Join(", ", arr.Select(b => b ? "true" : "false")) + "]";
    }
}
