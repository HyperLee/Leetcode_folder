namespace leetcode_1438
{
    internal class Program
    {
        /// <summary>
        /// 1438. Longest Continuous Subarray With Absolute Diff Less Than or Equal to Limit
        /// https://leetcode.com/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/description/?envType=daily-question&envId=2024-06-23
        /// 1438. 绝对差不超过限制的最长连续子数组
        /// https://leetcode.cn/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 8, 2, 4, 7 };
            int limit = 4;

            Console.WriteLine(LongestSubarray(input, limit));
        }


        /// <summary>
        /// https://leetcode.cn/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/solutions/1767774/by-chusep-knqg/
        /// https://leetcode.cn/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/solutions/612773/he-gua-de-shu-ju-jie-gou-hua-dong-chuang-v46j/
        /// https://leetcode.cn/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/solutions/230223/longest-continuous-subarray-by-ikaruga/
        /// https://leetcode.cn/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/solutions/612688/jue-dui-chai-bu-chao-guo-xian-zhi-de-zui-5bki/
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static int LongestSubarray(int[] nums, int limit)
        {
            int max = 0;
            int min = int.MaxValue;
            int n = nums.Length;
            int res = 0;

            for(int i = 0, start = 0; i < n; i++)
            {
                // 初始化/每輪更新 最大最小數值
                min = Math.Min(min, nums[i]);
                max = Math.Max(max, nums[i]);

                // 當絕對差值超過 limit時候
                if(max - min > limit)
                {
                    // 將滑動視窗 起始點移動至 i(想像成視窗往右移動)
                    // , 因位置移動, 所以需要更新最大最小數值
                    start = i;
                    min = nums[i];
                    max = nums[i];

                    // 不斷將開始位置往前(左邊界移動)推到超過limit位置
                    while (Math.Abs(nums[i] - nums[start]) <= limit)
                    {
                        // 視窗右邊走到 nums[i]位置, 此時更新視窗左邊位置
                        // 視窗右邊界固定不動 nums[i], 此時開始更新視窗左邊界
                        // 找出視窗內合乎 絕對差值內的. 更新最大最小數值
                        max = Math.Max(max, nums[start]);
                        min = Math.Min (min, nums[start]);
                        start--;
                    }

                    // 開始位置為不滿足時的最後一位
                    start++;
                }

                // 更新結果, 長度為當前位置扣除子數組開始時候位置+1
                res = Math.Max(res, i - start + 1);
            }

            return res;
        }
    }
}
