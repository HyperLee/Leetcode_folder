using System;
using System.Collections.Generic;

namespace leetcode_2211;

class Program
{
    /// <summary>
    /// 2211. Count Collisions on a Road
    /// https://leetcode.com/problems/count-collisions-on-a-road/description/?envType=daily-question&envId=2025-12-04
    /// 2211. 统计道路上的碰撞次数
    /// https://leetcode.cn/problems/count-collisions-on-a-road/description/?envType=daily-question&envId=2025-12-04
    /// 
    /// 2211. 道路上的碰撞次數（繁體中文翻譯）
    /// 有 n 輛車位在一條無限長的道路上。車輛由左至右編號為 0 到 n - 1，且每輛車位於一個唯一的位置。
    /// 你會得到一個長度為 n 的字串 directions，其中 directions[i] 可能是 'L'、'R' 或 'S'。
    /// 分別代表第 i 輛車向左移動、向右移動或停在原地；所有移動車輛的速度相同。
    /// 碰撞次數的計算規則如下：
    /// - 兩輛相向移動的車相撞時，碰撞次數增加 2。
    /// - 移動的車與靜止的車相撞時，碰撞次數增加 1。
    /// 發生碰撞後，參與碰撞的車將停在碰撞點並不再移動；除此之外，車輛不會改變狀態或方向。
    /// 回傳道路上會發生的總碰撞次數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solver = new Program();

        // 已知的 LeetCode 測資與期望值
        var tests = new Dictionary<string, int>
        {
            { "RLRSLL", 5 },
            { "LLRRS", 2 },
            { "SSSS", 0 }
        };

        Console.WriteLine("\nCountCollisions 範例測試:");
        foreach (var kv in tests)
        {
            var input = kv.Key;
            var expected = kv.Value;
            var result = solver.CountCollisions(input);
            Console.WriteLine($"Input: {input} -> Output: {result} (Expected: {expected})");
        }

        // 額外示範幾個不同輸入
        var samples = new[] { "R", "L", "RS", "LR", "RRSLL" };
        Console.WriteLine("\n其他測試:");
        foreach (var s in samples)
        {
            Console.WriteLine($"Input: {s} -> Output: {solver.CountCollisions(s)}");
        }
    }

    /// <summary>
    /// 計算道路上會發生的總碰撞次數。
    /// 方法說明：移除最左端連續向左移動的車，以及最右端連續向右移動的車後，
    /// 在剩下的區間內，任何非 'S'（非靜止） 的車都會發生碰撞，因此計數即可得出結果。
    /// </summary>
    /// <param name="directions">長度為 n 的字串，'L'＝向左、'R'＝向右、'S'＝靜止。</param>
    /// <returns>回傳道路上會發生的總碰撞次數 (int)。</returns>
    public int CountCollisions(string directions)
    {
        int n = directions.Length;

        int l = 0;
        // 最左側連續向左移動的車永遠不會與其他車相撞，因為它們只會向左離開其他車
        while(l < n && directions[l] == 'L')
        {
            l++;
        }

        int r = n;
        // 最右側連續向右移動的車永遠也不會與其他車相撞，因為它們只會向右離開其他車
        while(r > l && directions[r - 1] == 'R')
        {
            r--;
        }

        int cnt = 0;
        // 在剩餘的區間內，任何非 'S'（也就是 'L' 或 'R'）的車最終都會與其他車發生碰撞
        // 因此只要統計該區間內非靜止車的數量即可
        for (int i = l; i < r; i++)
        {
            if (directions[i] != 'S')
            {
                cnt++;
            }
        }
        // 時間複雜度: O(n)，僅需遍歷字串一次
        // 空間複雜度: O(1)，僅使用常數額外空間
        return cnt;
    }
}
