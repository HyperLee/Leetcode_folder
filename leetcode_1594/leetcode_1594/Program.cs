namespace leetcode_1594;

class Program
{
    /// <summary>
    /// 1594. Maximum Non Negative Product in a Matrix
    /// https://leetcode.com/problems/maximum-non-negative-product-in-a-matrix/description/?envType=daily-question&envId=2026-03-23
    /// 1594. 矩陣的最大非負積
    /// https://leetcode.cn/problems/maximum-non-negative-product-in-a-matrix/description/?envType=daily-question&envId=2026-03-23+
    ///
    /// You are given a m x n matrix grid. Initially, you are located at the top-left corner (0, 0),
    /// and in each step, you can only move right or down in the matrix.
    /// Among all possible paths starting from the top-left corner (0, 0) and ending in the
    /// bottom-right corner (m - 1, n - 1), find the path with the maximum non-negative product.
    /// The product of a path is the product of all integers in the grid cells visited along the path.
    /// Return the maximum non-negative product modulo 10^9 + 7.
    /// If the maximum product is negative, return -1.
    /// Notice that the modulo is performed after getting the maximum product.
    ///
    /// 給定一個 m x n 的矩陣 grid。初始位置在左上角 (0, 0)，
    /// 每一步只能向右或向下移動。
    /// 在所有從左上角 (0, 0) 到右下角 (m - 1, n - 1) 的路徑中，
    /// 找出乘積最大的非負路徑。路徑的乘積為沿途所有格子中整數的乘積。
    /// 回傳最大非負乘積對 10^9 + 7 取模的結果。
    /// 若最大乘積為負數，則回傳 -1。
    /// 注意：取模運算是在取得最大乘積之後才進行的。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
