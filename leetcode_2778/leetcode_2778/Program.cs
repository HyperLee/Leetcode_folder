namespace leetcode_2778;

class Program
{
    /// <summary>
    /// 2778. Sum of Squares of Special Elements
    /// https://leetcode.com/problems/sum-of-squares-of-special-elements/description/
    /// 2778. 特殊元素平方和
    /// https://leetcode.cn/problems/sum-of-squares-of-special-elements/
    ///
    /// English:
    /// You are given a 1-indexed integer array nums of length n.
    ///
    /// An element nums[i] of nums is called special if i divides n, i.e. n % i == 0.
    ///
    /// Return the sum of the squares of all special elements of nums.
    ///
    /// 繁體中文：
    /// 給定一個長度為 n、索引從 1 開始的整數陣列 nums。
    ///
    /// 如果 i 能整除 n，也就是 n % i == 0，則 nums 中的元素 nums[i] 稱為特殊元素。
    ///
    /// 回傳 nums 中所有特殊元素的平方和。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一：枚举
    /// 我们枚举所有下标，将特殊元素的平方累加即可。
    /// 在代码实现中，数组下标从 0 开始，因此当下标为 i 时，需要判断 n 是否能被 i+1 整除。
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int SumOfSquares(int[] nums)
    {
        int n = nums.Length;
        int ans = 0;
        for (int i = 0; i < n; i++)
        {
            if (n % (i + 1) == 0)
            {
                ans += nums[i] * nums[i];
            }
        }
        return ans;
    }

    /// <summary>
    /// 方法二：枚举因数
    /// 由于只有 n 的因数对应的元素是特殊元素，我们可以只枚举这些因数。
    /// 根据题意，如果 i 是 n 的因子，那我们也找到了另一个因子  n/i。所以只需枚举  n以内的 i。
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int SumOfSquares2(int[] nums)
    {
        int n = nums.Length;
        int ans = 0;
        for (int i = 1; i * i <= n; i++)
        {
            if (n % i == 0)
            {
                // 注意数组的下标还是从 0 开始的
                ans += nums[i - 1] * nums[i - 1];
                if (i * i != n)
                {
                    // 避免重复统计
                    ans += nums[n / i - 1] * nums[n / i - 1];
                }
            }
        }
        return ans;
    }
}