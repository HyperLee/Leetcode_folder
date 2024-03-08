using System;

namespace leetcode_3005
{
    internal class Program
    {
        /// <summary>
        /// 3005. Count Elements With Maximum Frequency
        /// https://leetcode.com/problems/count-elements-with-maximum-frequency/description/?envType=daily-question&envId=2024-03-08
        /// 3005. 最大频率元素计数
        /// https://leetcode.cn/problems/count-elements-with-maximum-frequency/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 2, 2, 3, 1, 4 };
            Console.WriteLine(MaxFrequencyElements(input));
            Console.ReadKey();
        }



        /// <summary>
        /// https://leetcode.cn/problems/count-elements-with-maximum-frequency/solutions/2604667/3005-zui-da-pin-lu-yuan-su-ji-shu-by-sto-y05r/
        /// 
        /// 1.利用Dictionary 計算每一個元素出現的頻率
        /// 2.同時 找出 最大頻率次數
        /// 3.統計結束之後, 找出 Dictionary中各自頻率與最大頻率相符者
        ///   就累加其次數. 即為題目所求
        ///   
        /// 應該是能優化目前寫法
        /// 看別人寫似乎比較簡單
        /// https://leetcode.cn/problems/count-elements-with-maximum-frequency/solutions/2603738/on-yi-ci-bian-li-pythonjavacgo-by-endles-0jye/
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MaxFrequencyElements(int[] nums)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            int maxFrequencyElements = 0, maxFrequency = 0;

            // 統計每個value出現的頻率
            foreach (int value in nums) 
            {
                if(dic.ContainsKey(value))
                {
                    dic[value]++;
                }
                else
                {
                    dic.Add(value, 1);
                }

                // 找出nums中最大頻率
                maxFrequency = Math.Max(maxFrequency, dic[value]);
            }

            // 找出 符合最大頻率者的元素數量有多少個
            foreach (KeyValuePair<int, int> kvp in dic) 
            {
                // 出現的頻率與最大頻率相同
                if (kvp.Value == maxFrequency)
                {
                    // 最大頻率元素數量
                    // 累加次數. 要注意每一個元素獨自算一次所以是累加 kvp.Value
                    maxFrequencyElements += kvp.Value;
                }
            }

            return maxFrequencyElements;
        }
    }
}
