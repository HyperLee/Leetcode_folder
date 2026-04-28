namespace leetcode_2033;

class Program
{
    /// <summary>
    /// 2033. Minimum Operations to Make a Uni-Value Grid
    /// https://leetcode.com/problems/minimum-operations-to-make-a-uni-value-grid/description/?envType=daily-question&envId=2026-04-28
    /// 2033. 获取单值网格的最小操作数
    /// https://leetcode.cn/problems/minimum-operations-to-make-a-uni-value-grid/description/?envType=daily-question&envId=2026-04-28
    ///
    /// English:
    /// You are given a 2D integer grid of size m x n and an integer x. In one operation,
    /// you can add x to or subtract x from any element in the grid.
    /// A uni-value grid is a grid where all the elements of it are equal.
    /// Return the minimum number of operations to make the grid uni-value.
    /// If it is not possible, return -1.
    ///
    /// 繁體中文：
    /// 給定一個大小為 m x n 的二維整數網格 grid 以及一個整數 x。每一次操作，
    /// 你可以對網格中的任一元素加上 x 或減去 x。
    /// 單值網格 (uni-value grid) 是指網格中所有元素皆相等的網格。
    /// 請回傳將網格變成單值網格所需的最少操作次數；若無法做到，則回傳 -1。
    /// </summary>
    /// <param name="args">命令列參數。</param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試案例 1：grid = [[2,4],[6,8]], x = 2 → 預期輸出 4
        // 中位數為 6，|2-6|+|4-6|+|6-6|+|8-6| = 4+2+0+2 = 8，8/2 = 4
        int[][] grid1 =
        [
            [2, 4],
            [6, 8],
        ];
        int x1 = 2;
        Console.WriteLine($"測試案例 1: {program.MinOperations(grid1, x1)} (預期: 4)");

        // 測試案例 2：grid = [[1,5],[2,3]], x = 1 → 預期輸出 5
        // 中位數為 3 (排序後 [1,2,3,5])，|1-3|+|2-3|+|3-3|+|5-3| = 2+1+0+2 = 5
        int[][] grid2 =
        [
            [1, 5],
            [2, 3],
        ];
        int x2 = 1;
        Console.WriteLine($"測試案例 2: {program.MinOperations(grid2, x2)} (預期: 5)");

        // 測試案例 3：grid = [[1,2],[3,4]], x = 2 → 預期輸出 -1
        // 1 % 2 = 1 但 2 % 2 = 0，模 x 結果不一致，無解
        int[][] grid3 =
        [
            [1, 2],
            [3, 4],
        ];
        int x3 = 2;
        Console.WriteLine($"測試案例 3: {program.MinOperations(grid3, x3)} (預期: -1)");
    }

    /// <summary>
    /// 計算將二維網格 <paramref name="grid"/> 中所有元素變成相同值的最小操作次數，
    /// 每次操作可對任一元素加上或減去 <paramref name="x"/>。
    /// </summary>
    /// <remarks>
    /// 解題核心觀念：
    /// <list type="number">
    /// <item>
    /// <description>
    /// 無解判斷：對於任意整數 k，(grid[i][j] + k·x) mod x = grid[i][j] mod x，
    /// 也就是元素在加減 x 之後對 x 取模的結果不變。因此若網格中存在任兩個元素
    /// 對 x 取模結果不同，則無法變成相同值，回傳 -1。
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// 中位數貪心：將所有元素拉平到同一個值時，目標值取所有數的中位數可使
    /// 「絕對差總和」∑|v - target| 最小（中位數貪心定理）。
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// 操作次數：因每次操作改變量為 x，故總操作次數 = (∑|v - median|) / x。
    /// </description>
    /// </item>
    /// </list>
    /// 時間複雜度 O(mn log(mn))，主要由排序決定；空間複雜度 O(mn)。
    /// </remarks>
    /// <param name="grid">大小為 m x n 的二維整數網格。</param>
    /// <param name="x">每次操作可加上或減去的整數值。</param>
    /// <returns>使網格成為單值網格所需的最少操作次數；若無法達成則回傳 -1。</returns>
    public int MinOperations(int[][] grid, int x)
    {
        // 將二維網格攤平成一維陣列以便排序與取中位數
        int k = grid.Length * grid[0].Length;
        int[] arr = new int[k];
        int idx = 0;
        // 以左上角元素對 x 取模的結果作為基準值，後續所有元素都必須與其相同
        int target = grid[0][0] % x;

        // 1. 判斷是否無解，並同步將二維網格攤平
        foreach(int[] row in grid)
        {
            foreach(int num in row)
            {
                // 每個元素對 x 取模後的結果必須相同，否則無法透過加減 x 來達成單值網格
                if(num % x != target)
                {
                    return -1;
                }
                arr[idx++] = num;
            }
        }

        // 2. 計算 grid 中所有元素的中位數
        //    根據中位數貪心定理，將所有數變成中位數可使絕對差總和最小
        Array.Sort(arr);
        int median = arr[k / 2];

        // 3. 計算將所有元素變成中位數所需的操作次數
        //    因每次操作改變量為 x，故總操作次數 = (∑|v - median|) / x
        int operations = 0;
        foreach(int num in arr)
        {
            operations += Math.Abs(num - median);
        }

        return operations / x;
    }
}
