using System.Numerics;

namespace leetcode_209;

class Program
{
    /// <summary>
    /// 209. Minimum Size Subarray Sum
    /// https://leetcode.com/problems/minimum-size-subarray-sum/description/
    ///
    /// Given an array of positive integers nums and a positive integer target, return the minimal length of a subarray whose sum is greater than or equal to target. If there is no such subarray, return 0 instead.
    ///
    /// 209. 最小大小子陣列總和
    /// https://leetcode.cn/problems/minimum-size-subarray-sum/description/
    ///
    /// 給定一個正整數陣列 nums 和一個正整數 target，請回傳其總和大於或等於 target 的子陣列中，長度最小者的長度。若不存在這樣的子陣列，則回傳 0。
    /// </summary>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一: 暴力法
    /// 暴力法是最直观的方法。初始化子数组的最小长度为无穷大，枚举数组 nums 中的每个下标作为子数组的开始下标，对于每个开始
    /// 下标 i，需要找到大于或等于 i 的最小下标 j，使得从 nums[i] 到 nums[j] 的元素和大于或等于 s，并更新子数组的最小长度（此
    /// 时子数组的长度是 j−i+1）。
    /// </summary>
    /// <param name="target"></param>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MinSubArrayLen(int target, int[] nums)
    {
        int n = nums.Length;
        if(n == 0)
        {
            return 0;
        }

        int res = int.MaxValue;
        for(int i = 0; i < n; i++)
        {
            int sum = 0;
            for(int j = i; j < n; j++)
            {
                sum += nums[j];
                if(sum >= target)
                {
                    // 更新子数组的最小长度（此时子数组的长度是 j-i+1）
                    res = Math.Min(res, j - i + 1);
                    break;
                }
            }
        }
        return res == int.MaxValue ? 0 : res;
    }

    /// <summary>
    /// 解法二:滑動視窗
    /// 
    /// 初始化都是從 0 開始
    /// left: 開始位置
    /// right:結束位置
    /// 
    /// right 往右跑,跑一個就加總.
    /// 直到符合 target 停止.
    /// 這樣就是第一個解
    /// 
    /// 下一輪就把 left 往右一格
    /// 既然 left 往右那麼 sum 就要扣除原先 left 位置的數值
    /// 再來繼續上一步驟 right 繼續往右走
    /// 
    /// 重覆此行為 直到 right < n
    /// 
    /// 子序列長度: right - left +1
    /// 陣列 index 從 0 開始
    /// 
    /// 時間複雜度: O(n), n 是 nums[] 長度
    /// 空間複雜度: O(1)
    /// </summary>
    /// <param name="target"></param>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MinSubArrayLen2(int target, int[] nums)
    {
        int n = nums.Length;
        if(n == 0)
        {
            return 0;
        }

        int res = int.MaxValue;
        int left = 0;
        int right = 0;
        int sum = 0;

        while(right < n)
        {
            sum += nums[right];

            while(sum >= target)
            {
                // 找到符合target之後 更新長度
                res = Math.Min(res, right - left + 1);

                // left往右 繼續跑下一輪直道 sum < target 停止
                // 用意是找出最佳解, 第一個符合的不一定是最佳解
                sum -= nums[left];
                // 先把原先左邊界移除, 然後往右移動
                left++;
            }
            right++;
        }
        return res == int.MaxValue ? 0 : res;
    }
}
