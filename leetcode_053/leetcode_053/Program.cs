namespace leetcode_053
{
    internal class Program
    {
        /// <summary>
        /// 53. Maximum Subarray
        /// https://leetcode.com/problems/maximum-subarray/
        /// 53. 最大子数组和
        /// https://leetcode.cn/problems/maximum-subarray/?envType=daily-question&envId=Invalid%20Date
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { -2, 1, -3, 4, -1, 2, 1, -5, 4 };

            Console.WriteLine("res: " + MaxSubArray(input));
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/maximum-subarray/solutions/228009/zui-da-zi-xu-he-by-leetcode-solution/?envType=daily-question&envId=Invalid+Date
        /// 題目要求是要找出連續的位置總和
        /// 
        /// 前 i 個數值總和(前面總和) 與 現在 i 位置(當前數值) 對比取出最大者
        /// 即可當作是連續加總找出最大總和
        /// 
        /// pre = Math.Max(pre + num, num);
        /// 當遇到某個 index 特別大時候, 後續就會從這 index 開始往後累加計算
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MaxSubArray(int[] nums)
        {
            int pre = 0;
            // 取第一個數值當作預設
            int maxans = nums[0];

            foreach (int num in nums)
            {
                // 前 i 個連續加總 與 當下這個數值相比對. 找出連續子數組
                pre = Math.Max(pre + num, num);
                // 持續更新最大值
                maxans = Math.Max(pre, maxans);
            }

            return maxans;
        }
    }
}
