namespace leetcode_440;

class Program
{
    /// <summary>
    /// 440. K-th Smallest in Lexicographical Order
    /// https://leetcode.com/problems/k-th-smallest-in-lexicographical-order/description/?envType=daily-question&envId=2025-06-09
    /// 440. 字典序的第K小数字
    /// https://leetcode.cn/problems/k-th-smallest-in-lexicographical-order/description/?envType=daily-question&envId=2025-06-09
    /// 
    /// 給定兩個整數 n 和 k，返回範圍 [1, n] 中字典序第 k 小的整數。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        (int N, int K, int Expected)[] testCases =
        [
            (13, 2, 10),
            (1, 1, 1),
            (100, 10, 17),
            (13, 13, 9),
            (1000, 100, 188),
            (1_000_000_000, 1, 1)
        ];

        Program program = new Program();
        int passedCount = 0;

        for (int index = 0; index < testCases.Length; index++)
        {
            (int n, int k, int expected) = testCases[index];
            int actual = program.FindKthNumber(n, k);
            bool passed = actual == expected;

            if (passed)
            {
                passedCount++;
            }

            Console.WriteLine($"案例 {index + 1}：n = {n}, k = {k}");
            Console.WriteLine($"預期：{expected}");
            Console.WriteLine($"實際：{actual}");
            Console.WriteLine($"結果：{(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passedCount}/{testCases.Length} 筆測試通過");
    }


    /// <summary>
    /// 在不建立完整排序清單的情況下，找出 <c>[1, n]</c> 中字典序第 <paramref name="k"/> 小的整數。
    /// 將十進位前綴視為隱式十叉字典樹，利用前綴子樹的節點數決定跳過整棵子樹或深入下一層，
    /// 直到走完以 1 為起點的 <paramref name="k"/> 個字典序位置。
    /// </summary>
    /// <param name="n">搜尋範圍上限；輸入需滿足 <c>1 &lt;= n &lt;= 10^9</c>。</param>
    /// <param name="k">以 1 為起點的字典序排名；輸入需滿足 <c>1 &lt;= k &lt;= n</c>。</param>
    /// <returns><c>[1, n]</c> 範圍內字典序第 <paramref name="k"/> 小的整數。</returns>
    public int FindKthNumber(int n, int k)
    {
        int curr = 1;
        k--;

        while (k > 0)
        {
            int steps = GetSteps(curr, n);
            if (steps <= k)
            {
                // 目標不在目前前綴子樹中，整棵跳過並移向下一個相鄰前綴。
                curr++;
                k -= steps;
            }
            else
            {
                // 目標仍在目前前綴子樹中，深入最左側子節點並消耗目前節點。
                curr *= 10;
                k--;
            }
        }

        return curr;
    }


    /// <summary>
    /// 計算以 <paramref name="curr"/> 為十進位前綴時，<c>[1, n]</c> 中屬於該前綴子樹的節點總數。
    /// 每一層以半開區間 <c>[first, last)</c> 表示相同前綴的連續數字，
    /// 將區間截斷至 <paramref name="n"/> 後逐層累加，不需要實際建立字典樹。
    /// </summary>
    /// <param name="curr">要計數的正整數前綴。</param>
    /// <param name="n">搜尋範圍上限；需大於或等於 <paramref name="curr"/> 才會產生非零計數。</param>
    /// <returns>以 <paramref name="curr"/> 為前綴且不超過 <paramref name="n"/> 的整數數量。</returns>
    public int GetSteps(int curr, int n)
    {
        int steps = 0;
        long first = curr;
        long last = curr + 1;
        while (first <= n)
        {
            // 每層只累加落在 [1, n] 內的前綴區間，long 可避免放大邊界時溢位。
            steps += (int)Math.Min(n + 1, last) - (int)first;
            first *= 10;
            last *= 10;
        }

        return steps;
    }
}
