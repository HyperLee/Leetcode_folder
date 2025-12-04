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

        // 使用 CountCollisions2 的測試，並與 CountCollisions 做比對
        Console.WriteLine("\nCountCollisions2 範例測試:");
        foreach (var kv in tests)
        {
            var input = kv.Key;
            var expected = kv.Value;
            var result2 = solver.CountCollisions2(input);
            Console.WriteLine($"Input: {input} -> Output: {result2} (Expected: {expected})");
        }

        Console.WriteLine("\n其他測試（比對 CountCollisions vs CountCollisions2）:");
        foreach (var s in samples)
        {
            var r1 = solver.CountCollisions(s);
            var r2 = solver.CountCollisions2(s);
            Console.WriteLine($"Input: {s} -> CountCollisions: {r1}, CountCollisions2: {r2}, Equal: {r1 == r2}");
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

    /// <summary>
    /// 使用單次遍歷與狀態計數器來計算碰撞次數。
    /// 思路說明：
    /// - 我們以 <c>pendingRightCount</c> 來記錄目前尚未被處理的、連續出現的 'R' 車輛數量。
    ///   - 當遇到 'R' 時，代表往右移動的車，若目前處於一段 'R' 序列中（pendingRightCount >= 0），便將 pendingRightCount++，否則 pendingRightCount = 1 開始一段新的 'R' 序列。
    /// - 當遇到 'S'（靜止）時：若前面有 'R'（pendingRightCount > 0），則這些 'R' 都會與 'S' 發生碰撞（每輛 R+1 次），所以將 pendingRightCount 的值加到結果中，並將 pendingRightCount 歸零（表示該段 'R' 已處理完畢）。
    /// - 當遇到 'L'（往左移動）時：若前面有 'R'（pendingRightCount >= 0），表示一段或多段 'R' 與此 'L' 相向，
    ///   對於一段有 <c>pendingRightCount</c> 個 <c>R</c> 的情形，與 <c>L</c> 相撞總共會產生 <c>pendingRightCount + 1</c> 次碰撞（每個 R 與 L 各 1 次，最後的 L 也算 1 次），因此將 <c>pendingRightCount + 1</c> 加到結果中，並將 pendingRightCount 設為 0（碰撞後變為停下來）。
    /// - 若遇到 'L' 時 pendingRightCount 為 -1（代表前面沒有未處理的 'R'），則該 'L' 不會與任一前方車輛碰撞。
    /// 
    /// 時間複雜度：O(n)（單次遍歷）
    /// 空間複雜度：O(1)（常數額外空間）
    /// </summary>
    /// <param name="directions">方向字串：'L'、'R'、'S'。</param>
    /// <returns>總碰撞次數。</returns>
    public int CountCollisions2(string directions)
    {
        int res = 0;
        // pendingRightCount 的語意：
        // -1 表示目前沒有前置的 'R'（尚無待處理的向右移動車）
        // >= 0 表示目前有連續出現的 'R' 數量（待處理）
        int pendingRightCount = -1;

        foreach(char c in directions)
        {
            if(c == 'L')
            {
                // 遇到 'L'：若之前有一段 'R'（pendingRightCount >= 0），那麼該段所有 'R' 與此 'L' 會發生碰撞。
                // 其產生的碰撞數為 pendingRightCount + 1（pendingRightCount 個 R 各與 L 相撞，且 L 本身也與一部車相撞算 1 次）
                    if(pendingRightCount >= 0)
                {
                    res += pendingRightCount + 1;
                    // 碰撞後都變成靜止（或已處理），重置 pendingRightCount
                    pendingRightCount = 0;
                }
            }
            else if(c =='S')
            {
                // 遇到 'S'：若之前有一段 'R'（pendingRightCount > 0），則每個 R 都會撞上此 S，各算一次碰撞
                    if(pendingRightCount > 0)
                {
                    res += pendingRightCount;
                }
                // 碰撞後或遇 S 時都不再有待處理的 R
                pendingRightCount = 0;
            }
            else
            {
                // 遇到 'R'：若 pendingRightCount >= 0，代表上一個字元也是 'R' 或已開始計數，則累加 R 的數量；
                // 否則（pendingRightCount == -1）代表開始一段新的 R
                    if(pendingRightCount >= 0)
                {
                    pendingRightCount++;
                }
                else
                {
                    pendingRightCount = 1;
                }
            }
        }
        return res;
    }
}
