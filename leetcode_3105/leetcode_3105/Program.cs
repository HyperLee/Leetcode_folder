namespace leetcode_3105;

class Program
{
    /// <summary>
    /// 3105. Longest Strictly Increasing or Strictly Decreasing Subarray
    /// https://leetcode.com/problems/longest-strictly-increasing-or-strictly-decreasing-subarray/description/
    /// 3105. 最长的严格递增或递减子数组
    /// https://leetcode.cn/problems/longest-strictly-increasing-or-strictly-decreasing-subarray/
    /// <para>
    /// You are given an array of integers <c>nums</c>. Return the length of the longest
    /// subarray of <c>nums</c> which is either strictly increasing or strictly decreasing.
    /// </para>
    /// <para>
    /// 給定一個整數陣列 <c>nums</c>，請回傳其中最長的嚴格遞增或嚴格遞減連續子陣列的長度。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一：朴素模拟
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int LongestMonotonicSubarray(int[] nums)
    {
        int cnt1 = 1, cnt2 = 1;
        int ans = 1;
        int n = nums.Length;

        for(int i = 1; i < n; i++)
        {
            if(nums[i] <= nums[i - 1])
            {
                ans = Math.Max(ans, cnt1);
                cnt1 = 1;
            }
            else
            {
                cnt1++;
            }

            if(nums[i] >= nums[i - 1])
            {
                ans = Math.Max(ans, cnt2);
                cnt2 = 1;
            }
            else
            {
                cnt2++;
            }
        }

        ans = Math.Max(ans, Math.Max(cnt1, cnt2));
        return ans;
    }

    /// <summary>
    /// 方法二：基调标志 + 模拟
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int LongestMonotonicSubarray2(int[] nums)
    {
        int ans = 1;
        int i = 0;
        int n = nums.Length;
        while(i < n - 1)
        {
            if(nums[i + 1] == nums[i])
            {
                i++;
                continue;
            }

            int j = i;
            bool inc = nums[i + 1] > nums[i];
            i += 2;
            while(i < n && nums[i] != nums[i - 1] && (nums[i] > nums[i - 1] == inc))
            {
                i++;
            }
            ans = Math.Max(ans, i - j);
            i--;
        }
        return ans;
    }
}
