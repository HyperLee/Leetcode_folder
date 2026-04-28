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
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public int MinOperations(int[][] grid, int x)
    {
        int k = grid.Length * grid[0].Length;
        int[] arr = new int[k];
        int idx = 0;
        int target = grid[0][0] % x;

        // 1. 判斷是否無解
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
        Array.Sort(arr);
        int median = arr[k / 2];

        // 3. 計算將所有元素變成中位數所需的操作次數
        int operations = 0;
        foreach(int num in arr)
        {
            operations += Math.Abs(num - median);
        }

        return operations / x;
    }
}
