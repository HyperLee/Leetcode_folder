namespace leetcode_3736;

class Program
{
    /// <summary>
    /// 3736. Minimum Moves to Equal Array Elements III
    /// https://leetcode.com/problems/minimum-moves-to-equal-array-elements-iii/description/
    /// 3736. 最小操作次数使数组元素相等 III
    /// https://leetcode.cn/problems/minimum-moves-to-equal-array-elements-iii/description/
    /// English
    /// You are given an integer array nums.
    /// 
    /// In one move, you may increase the value of any single element nums[i] by 1.
    /// 
    /// Return the minimum total number of moves required so that all elements in nums become equal.
    /// 
    /// 繁體中文
    /// 給定一個整數陣列 nums。
    /// 
    /// 每次操作中，你可以將任意一個元素 nums[i] 的值增加 1。
    /// 
    /// 請回傳使 nums 中所有元素都相等所需的最少總操作次數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一：贪心
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MinMoves(int[] nums)
    {
        int max = nums.Max();
        int moves = 0;
        foreach (var num in nums)
        {
            moves += max - num;
        }
        return moves;
    }

    /// <summary>
    /// 方法一：贪心
    /// 不採用 nums.Max()
    /// 改用 foreach 找出最大數值, 再計算移動次數
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MinMoves2(int[] nums)
    {
        int max = nums[0];
        int moves = 0;

        // 找出最大數值
        foreach(int x in nums)
        {
            max = Math.Max(max, x);
        }

        // 計算移動次數
        foreach(int x in nums)
        {
            moves += max - x;
        }
        return moves;
    }
}
