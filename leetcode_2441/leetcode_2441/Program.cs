namespace leetcode_2441
{
    internal class Program
    {
        /// <summary>
        /// 2441. Largest Positive Integer That Exists With Its Negative
        /// https://leetcode.com/problems/largest-positive-integer-that-exists-with-its-negative/description/?envType=daily-question&envId=2024-05-02
        /// 2441. 与对应负数同时存在的最大正整数
        /// https://leetcode.cn/problems/largest-positive-integer-that-exists-with-its-negative/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { -1, 2, -3, 3 };
            Console.WriteLine(FindMaxK2(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 排序 + 雙指針
        /// 
        /// 找到回傳正數, 也就是right
        /// [left, right]
        /// 是成對的, 一正一負
        /// 
        /// ref:
        /// https://leetcode.cn/problems/largest-positive-integer-that-exists-with-its-negative/solutions/2266809/yu-dui-ying-fu-shu-tong-shi-cun-zai-de-z-kg8f/
        /// https://leetcode.cn/problems/largest-positive-integer-that-exists-with-its-negative/solutions/1895719/by-endlesscheng-jjtb/
        /// https://leetcode.cn/problems/largest-positive-integer-that-exists-with-its-negative/solutions/2626017/2441-yu-dui-ying-fu-shu-tong-shi-cun-zai-n0zd/
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int FindMaxK(int[] nums)
        {
            // 排序 小至大
            Array.Sort(nums);

            int left = 0, right = nums.Length - 1;
            while(left < right)
            {
                if (nums[left] + nums[right] == 0)
                {
                    // 找到答案, 回傳 nums[right]正數
                    return nums[right];
                }
                else if (nums[left] + nums[right] > 0)
                {
                    // 正數太多, 正數要減少 往右縮小差異
                    right--;
                }
                else
                {
                    // 負數太多, 負數要減少 往左縮小差異
                    left++;
                }
            }

            // 找不到答案回傳 -1
            return -1;
        }



        /// <summary>
        /// Hash
        /// 
        /// hash判斷要小心
        /// 因為內層判斷最大值有使用絕對值取正數
        /// 外層if那邊 要取相反數
        /// 不然會判斷錯誤
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int FindMaxK2(int[] nums)
        {
            int ans = -1;
            ISet<int> _hash = new HashSet<int>();

            foreach (int x in nums) 
            {
                // -x 存在於hash, 這邊要注意是符合的 相反數
                // 題目要求是一正一負, 所以判斷相反數是沒問題的
                if(_hash.Contains(-x))
                {
                    // 絕對值取正數
                    // 不斷更新,找出最大 正數
                    ans = Math.Max(ans, Math.Abs(x));
                }

                // 把數值加入hash
                _hash.Add(x);
            }

            return ans;
        }
    }
}
