using System.Drawing;

namespace leetcode_2078;

class Program
{
    /// <summary>
    /// 2078. Two Furthest Houses With Different Colors
    /// https://leetcode.com/problems/two-furthest-houses-with-different-colors/description/?envType=daily-question&envId=2026-04-20
    ///
    /// There are n houses evenly lined up on the street, and each house is beautifully painted.
    /// You are given a 0-indexed integer array colors of length n, where colors[i] represents the color of the ith house.
    /// Return the maximum distance between two houses with different colors.
    /// The distance between the ith and jth houses is abs(i - j), where abs(x) is the absolute value of x.
    ///
    /// 2078. 兩棟顏色不同且距離最遠的房子
    /// https://leetcode.cn/problems/two-furthest-houses-with-different-colors/description/?envType=daily-question&envId=2026-04-20
    ///
    /// 街道上有 n 棟房屋排成一排，每棟房屋都塗上了漂亮的顏色。
    /// 給你一個長度為 n 的 0 索引整數陣列 colors，其中 colors[i] 表示第 i 棟房屋的顏色。
    /// 請回傳兩棟顏色不同的房屋之間的最大距離。
    /// 第 i 棟和第 j 棟房屋之間的距離為 abs(i - j)，其中 abs(x) 為 x 的絕對值。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試案例 1：colors = [1, 1, 1, 6, 1, 1, 1]，預期輸出 = 3
        // 最遠的顏色不同房子對：index 0 (color=1) vs index 3 (color=6)，或 index 3 (color=6) vs index 6 (color=1)
        int[] colors1 = [1, 1, 1, 6, 1, 1, 1];
        Console.WriteLine($"Test 1: {program.MaxDistance(colors1)}"); // Expected: 3

        // 測試案例 2：colors = [1, 8, 3, 8, 3]，預期輸出 = 4
        // 最遠的顏色不同房子對：index 0 (color=1) vs index 4 (color=3)
        int[] colors2 = [1, 8, 3, 8, 3];
        Console.WriteLine($"Test 2: {program.MaxDistance(colors2)}"); // Expected: 4

        // 測試案例 3：colors = [0, 1]，預期輸出 = 1
        // 僅有兩棟房子，顏色不同，距離為 1
        int[] colors3 = [0, 1];
        Console.WriteLine($"Test 3: {program.MaxDistance(colors3)}"); // Expected: 1
    }

    /// <summary>
    /// 方法一：枚舉（Enumeration / Brute Force）
    /// <br/>
    /// 解題概念與出發點：
    /// <br/>
    /// 題目要求找出兩棟顏色不同的房子的最遠距離。由於 n 最大為 100，
    /// 暴力枚舉所有房子對的時間複雜度 O(n²) 完全可接受。
    /// 我們從「下標較小的房子 i」出發，往右枚舉「下標較大的房子 j」，
    /// 只要兩棟房子顏色不同，就計算並嘗試更新最大距離。
    /// <br/>
    /// 演算法步驟：
    /// <list type="number">
    ///   <item>外層迴圈：枚舉左側房子 i（i 從 0 到 n-1）。</item>
    ///   <item>內層迴圈：枚舉右側房子 j（j 從 i+1 到 n-1），確保 j &gt; i，距離恆為正。</item>
    ///   <item>若 colors[i] != colors[j]，以 j - i 更新最大距離。</item>
    ///   <item>所有配對枚舉完畢後，返回最大距離。</item>
    /// </list>
    /// <br/>
    /// 時間複雜度：O(n²)，n 為房子數量。<br/>
    /// 空間複雜度：O(1)，只使用常數額外空間。
    /// </summary>
    /// <param name="colors">長度為 n 的 0 索引整數陣列，colors[i] 表示第 i 棟房屋的顏色。</param>
    /// <returns>兩棟顏色不同的房屋之間的最大距離。</returns>
    public int MaxDistance(int[] colors)
    {
        int n = colors.Length;
        int maxDistance = 0;

        // 枚舉所有 (i, j) 對，i < j，確保只計算正向距離
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                // 僅當兩棟房子顏色不同時，才更新最大距離
                if (colors[i] != colors[j])
                {
                    maxDistance = Math.Max(maxDistance, j - i);
                }
            }
        }

        return maxDistance;
    }
}
