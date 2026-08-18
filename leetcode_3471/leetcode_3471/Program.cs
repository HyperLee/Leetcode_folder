namespace leetcode_3471;

class Program
{
    /// <summary>
    /// 3471. Find the Largest Almost Missing Integer
    /// https://leetcode.com/problems/find-the-largest-almost-missing-integer/description
    /// 3471. 找出最大的幾乎缺失整數
    /// https://leetcode.cn/problems/find-the-largest-almost-missing-integer/description/
    ///
    /// <para>English original:</para>
    /// <para>You are given an integer array nums and an integer k.</para>
    /// <para>An integer x is almost missing from nums if x appears in exactly one subarray of size k within nums.</para>
    /// <para>Return the largest almost missing integer from nums. If no such integer exists, return -1.</para>
    /// <para>A subarray is a contiguous sequence of elements within an array.</para>
    ///
    /// <para>繁體中文翻譯：</para>
    /// <para>給定一個整數陣列 nums 和一個整數 k。</para>
    /// <para>如果整數 x 在 nums 中恰好出現在一個大小為 k 的子陣列中，則稱 x 為 nums 中的幾乎缺失整數。</para>
    /// <para>請回傳 nums 中最大的幾乎缺失整數。如果不存在這樣的整數，請回傳 -1。</para>
    /// <para>子陣列是陣列中一段連續的元素序列。</para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一：分类讨论
    /// 思路与算法
    /// 该算法基于 k 的取值大小进行了三种分类讨论：
    /// 
    /// 当 k=n 时：
    /// 整个数组就等于这一个滑动窗口。因此数组中的所有数字都仅满足出现在一个大小为 k 的子数组中，我们要找最大的几近缺失整
    /// 数，其实就是直接返回数组的最大值即可。
    /// 
    /// 当 k=1 时：
    /// 滑动窗口的长度为 1。题目要求找只出现过一次的数字，这就等同于寻找整个数组中全局唯一且最大的那个数字。因此我们统计完
    /// 频数后，直接从大到小进行遍历，遇到第一个出现仅一次的数字就是最大的答案。
    /// 
    /// 当 1<k<n 时：
    /// 除了数组头部和尾部这两个元素之外，所有卡在中间的元素都必定会被滑动窗口覆盖至少 2 次。所以中间元素必然不符合要求。满
    /// 足要求的数字只能是数组首位元素或末尾元素。我们只需判断这两个元素是否在整个数组里只出现过一次。若成立，则返回它们之中
    /// 满足条件的最大值即可。
    /// 
    /// 
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public int LargestInteger(int[] nums, int k)
    {
        int n = nums.Length;
        if(n == k)
        {
            return nums.Max();
        }

        int[] count = new int[51];
        foreach(int x in nums)
        {
            count[x]++;
        }

        if(k == 1)
        {
            for(int i = 50; i >= 0; i--)
            {
                if(count[i] == 1)
                {
                    return i;
                }
            }
            return -1;
        }

        int res = -1;
        if(count[nums[0]] == 1)
        {
            res = Math.Max(res, nums[0]);
        }

        if(count[nums[n - 1]] == 1)
        {
            res = Math.Max(res, nums[n - 1]);
        }
        return res;
    }

}
