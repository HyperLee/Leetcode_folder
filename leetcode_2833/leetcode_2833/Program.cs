namespace leetcode_2833;

class Program
{
    /// <summary>
    /// 2833. Furthest Point From Origin
    /// https://leetcode.com/problems/furthest-point-from-origin/description/?envType=daily-question&envId=2026-04-24
    /// 2833. 距離原點最遠的點
    /// https://leetcode.cn/problems/furthest-point-from-origin/description/?envType=daily-question&envId=2026-04-24
    ///
    /// You are given a string moves of length n consisting only of characters 'L', 'R', and '_'.
    /// The string represents your movement on a number line starting from the origin 0.
    /// In the ith move, you can choose one of the following directions:
    ///   - move to the left  if moves[i] = 'L' or moves[i] = '_'
    ///   - move to the right if moves[i] = 'R' or moves[i] = '_'
    /// Return the distance to the furthest point from the origin you can get to at the end of your moves.
    ///
    /// 給定一個長度為 n、僅由字元 'L'、'R' 和 '_' 組成的字串 moves。
    /// 該字串代表你在數線上從原點 0 出發的移動方式。
    /// 在第 i 次移動時，你可以選擇以下方向之一：
    ///   - 若 moves[i] = 'L' 或 moves[i] = '_'，則向左移動
    ///   - 若 moves[i] = 'R' 或 moves[i] = '_'，則向右移動
    /// 回傳在完成所有移動後，你能距離原點最遠的距離。
    /// </summary>
    /// <param name="args">程式進入點參數</param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        // 測試資料 1: moves = "L_RL__R" → L=2, R=2, _=3 → |2-2|+3 = 3
        Console.WriteLine(solution.FurthestDistanceFromOrigin("L_RL__R")); // 預期輸出: 3

        // 測試資料 2: moves = "_R__LL_" → L=2, R=1, _=4 → |2-1|+4 = 5
        Console.WriteLine(solution.FurthestDistanceFromOrigin("_R__LL_")); // 預期輸出: 5

        // 測試資料 3: moves = "_______" → L=0, R=0, _=7 → |0-0|+7 = 7
        Console.WriteLine(solution.FurthestDistanceFromOrigin("_______")); // 預期輸出: 7
    }

    /// <summary>
    /// 計算從原點出發，在完成所有移動後能距離原點的最遠距離。
    ///
    /// 解題思路（一次遍歷）：
    /// 令 L 為 moves 中 'L' 的數量，R 為 moves 中 'R' 的數量，B 為 moves 中 '_' 的數量。
    ///
    /// 在不考慮 '_' 的情況下，當前位置與原點的距離為 |L - R|。
    /// 為了使得最終位置距離原點最遠，應將所有 '_' 全部移動到與當前偏移方向相同的方向，
    /// 因此最遠距離為 |L - R| + B。
    ///
    /// 時間複雜度：O(n)，其中 n 為 moves 的長度（單次遍歷）
    /// 空間複雜度：O(1)，只使用常數額外空間
    /// </summary>
    /// <param name="moves">
    /// 由 'L'（向左移動）、'R'（向右移動）、'_'（可自由選擇方向）組成的移動字串
    /// </param>
    /// <returns>從原點可到達的最遠距離</returns>
    /// <example>
    /// <code>
    /// FurthestDistanceFromOrigin("L_RL__R") // 回傳 3
    /// FurthestDistanceFromOrigin("_R__LL_") // 回傳 5
    /// FurthestDistanceFromOrigin("_______") // 回傳 7
    /// </code>
    /// </example>
    public int FurthestDistanceFromOrigin(string moves)
    {
        int leftCount = 0;        // 'L' 的總數量
        int rightCount = 0;       // 'R' 的總數量
        int underscoreCount = 0;  // '_' 的總數量（可自由選擇方向）

        // 一次遍歷，統計各字元出現次數
        foreach (char move in moves)
        {
            if (move == 'L')
            {
                leftCount++;
            }
            else if (move == 'R')
            {
                rightCount++;
            }
            else if (move == '_')
            {
                underscoreCount++;
            }
        }

        // 固定移動的淨偏移量為 |L - R|，再將所有 '_' 配合偏移方向加上去，即為最遠距離
        int maxDistance = Math.Abs(leftCount - rightCount) + underscoreCount;
        return maxDistance;
    }
}
