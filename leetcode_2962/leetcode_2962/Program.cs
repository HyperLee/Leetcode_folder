using System.Net.Mail;

namespace leetcode_2962
{
    internal class Program
    {
        /// <summary>
        /// 2962. Count Subarrays Where Max Element Appears at Least K Times
        /// https://leetcode.com/problems/count-subarrays-where-max-element-appears-at-least-k-times/description/?envType=daily-question&envId=2024-03-29
        /// 2962. 统计最大元素出现至少 K 次的子数组
        /// https://leetcode.cn/problems/count-subarrays-where-max-element-appears-at-least-k-times/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 3, 2, 3, 3 };
            int k = 2;
            Console.WriteLine(CountSubarrays(input, k));
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/count-subarrays-where-max-element-appears-at-least-k-times/solutions/2561054/2962-tong-ji-zui-da-yuan-su-chu-xian-zhi-t910/
        /// https://leetcode.cn/problems/count-subarrays-where-max-element-appears-at-least-k-times/solutions/2560940/hua-dong-chuang-kou-fu-ti-dan-pythonjava-xvwg/
        /// 
        /// 
        /// 滑動視窗概念題型
        /// [start, end] 整體視窗往右滑動
        /// 
        /// end先往右, 如果end 元素為maxnum 就累加其次數
        /// 當 次數達到 k時候
        /// 此時需要考慮把 最左邊start的元素移除
        /// 這樣才能納入新的subarray組合
        /// 每次替除組合 就可以加入新的組合
        /// 故結果只要統計start次數 即可知道有多少種組合
        /// 
        /// 請注意 題目 function 回傳是 long 不是 int
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static long CountSubarrays(int[] nums, int k)
        {
            int maxnum = 0;
            foreach (int i in nums) 
            {
                // 找出 nums中最大的element
                maxnum = Math.Max(maxnum, i);
            }

            long res = 0;
            int start = 0, end = 0;
            int length = nums.Length;
            // 統計element出現次數
            int maxcount = 0;

            while(end < length)
            {
                if (nums[end] == maxnum) 
                {
                    // element 數值為 最大元素,
                    // 累加 出現次數
                    maxcount++;
                }

                while(maxcount == k)
                {
                    // 當最大元素 出現次數達到 k
                    // 要讓視窗start往右滑動
                    // 如果 start 符合 maxnum 就要扣除 次數累加
                    if (nums[start] == maxnum) 
                    {
                        maxcount--;
                    }
                    start++;
                }

                res += start;
                end++;
            }

            return res;
        }
    }
}
