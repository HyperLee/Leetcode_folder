using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace leetcode_3623;

class Program
{
    /// <summary>
    /// 3623. Count Number of Trapezoids I
    /// https://leetcode.com/problems/count-number-of-trapezoids-i/description/?envType=daily-question&envId=2025-12-02
    /// 3623. 统计梯形的数目 I
    /// https://leetcode.cn/problems/count-number-of-trapezoids-i/description/?envType=daily-question&envId=2025-12-02
    /// 
    /// 方法一：哈希表 + 几何数学
    /// 解題說明：
    /// 题目要求统计由给定点集组成的“水平梯形”的数量。水平梯形的两条平行边须在不同的水平线（不同的 y 值）上。
    /// 我们可以把点按 y 值分组：对于某一高度 y，有 p_y 个点，则该高度上能构成的“水平边”的数量为 C(p_y, 2) = p_y * (p_y - 1) / 2。
    /// 任意两个不同高度（y1, y2）分别选一条边即可构成一个水平梯形，因此总梯形数等于对所有高度对 (i, j) 求 edges_i * edges_j 的和。
    /// 直接枚举所有高度对会是 O(n^2)（按高度个数计），不过我们可以利用累加和来将时间复杂度降到 O(n)：
    /// 1. 先统计每个 y 对应的点数 p_y；
    /// 2. 令 edges_y = C(p_y, 2)；
    /// 3. 逐一遍历所有 edges_y，维护累加和 sum（表示之前高度的 edges 之和），每次 res += edges_y * sum，然后更新 sum += edges_y。
    /// 最后对结果取模 1e9 + 7 返回。
    /// 
    /// 复杂度分析：
    /// - 时间：O(n)，n 为点的数量（分组过程遍历点列表，之后遍历不同 y 值）。
    /// - 空间：O(k)，k 为不同 y 值数量（用于哈希表统计）。
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 建立 Program 實例以調用非靜態方法
        Program solution = new Program();

        // 測試案例 1：兩行各有 3 個點 -> 每行邊數 C(3,2) = 3，兩行組合數為 3 * 3 = 9
        int[][] points1 = new int[][] {
            new int[] {0, 0}, new int[] {1, 0}, new int[] {2, 0},
            new int[] {0, 1}, new int[] {1, 1}, new int[] {2, 1}
        };
        Console.WriteLine("Test 1 (expected 9): " + solution.CountTrapezoids(points1));

        // 測試案例 2：僅一行 3 個點 -> 沒有兩條不同高度的邊，應為 0
        int[][] points2 = new int[][] {
            new int[] {0, 0}, new int[] {1, 0}, new int[] {2, 0}
        };
        Console.WriteLine("Test 2 (expected 0): " + solution.CountTrapezoids(points2));

        // 測試案例 3：兩行，第一行 2 個點 (C=1)，第二行 3 個點 (C=3) -> 1 * 3 = 3
        int[][] points3 = new int[][] {
            new int[] {0, 0}, new int[] {1, 0},
            new int[] {0, 1}, new int[] {1, 1}, new int[] {2, 1}
        };
        Console.WriteLine("Test 3 (expected 3): " + solution.CountTrapezoids(points3));
    }

    /// <summary>
    /// 計算由給定點集所能組成水平梯形的總數。
    /// - 輸入：二維整數陣列 points，points[i] = [x_i, y_i]
    /// - 輸出：可選擇的水平梯形總數，對 1e9+7 取模。
    /// </summary>
    /// <param name="points">一系列的點座標</param>
    /// <returns>水平梯形的數量（mod 1e9+7）</returns>
    public int CountTrapezoids(int[][] points)
    {
        // pointCount: 統計每個 y 值出現的點個數 p_y
        Dictionary<int, int> pointCount = new Dictionary<int, int>();
        const int MOD = 1000000007;
        long res = 0;
        long sum = 0;

        // 統計每個 y 對應的點數
        foreach (int[] point in points)
        {
            int y = point[1];
            if (pointCount.ContainsKey(y))
            {
                pointCount[y]++;
            }
            else
            {
                pointCount[y] = 1;
            }
        }

        // 針對每個 y 計算對應的邊數 edges = C(p_y, 2)，並利用累加和 sum 計算 pairs
        foreach (int pNum in pointCount.Values)
        {
            // 如果 pNum < 2，則 C(pNum,2) = 0
            long edge = (long)pNum * (pNum - 1) / 2; // C(pNum, 2)
            // 對於當前高度，其可與之前所有高度的邊組合成梯形，數量為 edge * sum
            res = (res + edge * sum) % MOD;
            // 將當前高度的邊加入 sum，供後續高度配對
            sum = (sum + edge) % MOD;
        }
        return (int)res;
    }
}
