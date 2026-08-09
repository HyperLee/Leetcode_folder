using System;
using System.Collections.Generic;

namespace leetcode_2211;

class Program
{
    /// <summary>
    /// <para>
    /// 2211. Count Collisions on a Road
    /// https://leetcode.com/problems/count-collisions-on-a-road/description/
    ///
    /// There are n cars at unique points on an infinite road, numbered 0 through n - 1 from left to right. directions[i] is 'L', 'R', or 'S' when car i moves left, moves right, or stays. All moving cars have equal speed. Opposite-moving cars add 2 collisions; a moving car hitting a stationary car adds 1. Cars involved in a collision become stationary. Return the total collisions.
    ///
    /// Example 1:
    /// Input: directions = "RLRSLL"
    /// Output: 5
    /// Explanation: Cars 0 and 1 collide, changing the count from 0 + 2 = 2. Cars 2 and 3 change it to 2 + 1 = 3. Cars 3 and 4 change it to 3 + 1 = 4. Car 5 later hits stationary car 4, changing it to 4 + 1 = 5.
    ///
    /// Example 2:
    /// Input: directions = "LLRR"
    /// Output: 0
    /// Explanation: No cars collide, so the total is 0.
    ///
    /// Constraints:
    /// - 1 &lt;= directions.length &lt;= 10^5
    /// - directions[i] is 'L', 'R', or 'S'.
    /// </para>
    /// <para>
    /// 2211. 統計道路上的碰撞次數
    /// https://leetcode.cn/problems/count-collisions-on-a-road/description/
    ///
    /// 無限長道路上有 n 輛位於不同位置的車，從左到右編號 0 到 n - 1。directions[i] 為 'L'、'R'、'S'，表示車 i 向左、向右或靜止；所有移動車輛速度相同。相向車輛碰撞增加 2 次，移動車撞上靜止車增加 1 次。碰撞後相關車輛會靜止。回傳碰撞總次數。
    ///
    /// 範例 1：
    /// 輸入：directions = "RLRSLL"
    /// 輸出：5
    /// 說明：車 0 與 1 碰撞，使次數從 0 + 2 = 2；車 2 與 3 使其成為 2 + 1 = 3；車 3 與 4 使其成為 3 + 1 = 4；車 5 之後撞上靜止的車 4，使其成為 4 + 1 = 5。
    ///
    /// 範例 2：
    /// 輸入：directions = "LLRR"
    /// 輸出：0
    /// 說明：沒有車輛碰撞，因此總數為 0。
    ///
    /// 限制條件：
    /// - 1 &lt;= directions.length &lt;= 10^5
    /// - directions[i] 為 'L'、'R' 或 'S'。
    /// </para>
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
