using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_169
{
    internal class Program
    {
        /// <summary>
        /// 169. Majority Element
        /// https://leetcode.com/problems/majority-element/
        /// 169. 多数元素
        /// https://leetcode.cn/problems/majority-element/
        /// 
        /// 問題描述：Majority Element
        /// 在長度為 n 的陣列 nums 中，Majority Element 是指出現次數超過 n/ 2 的元素（也就是出現次數超過陣列一半的元素）。題目保證這樣的元素一定存在。
        /// 
        /// 解法二比較簡短, 排序特性.
        /// 但是偏好解法一, 比較直覺好理解
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 2, 2, 1, 1, 1, 2, 2 };

            Console.WriteLine("method1: " + MajorityElement(input));
            Console.WriteLine("method2: " + MajorityElement2(input));
        }


        /// <summary>
        /// Dictionary 統計每個出現的數字以及頻率
        /// key: 數字
        /// Value: 頻率
        /// 
        /// 統計完畢之後, 找出頻率超過 n 的第一個出現的回傳
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MajorityElement(int[] nums)
        {
            // key: nums[i], Value: 出現次數(頻率)
            Dictionary<int, int> dic = new Dictionary<int, int>();
            foreach (int num in nums) 
            {
                if(dic.ContainsKey(num))
                {
                    dic[num]++;
                }
                else
                {
                    dic.Add(num, 1);
                }
            }

            int ans = 0;
            // 題目要求 要超過 n / 2 數量
            int n = nums.Length / 2;

            foreach (KeyValuePair<int, int> num in dic) 
            {
                // value 超過 n 
                if(num.Value > n)
                {
                    // 回傳答案是 key
                    ans = num.Key;
                    break;
                }
            }

            return ans;

        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/majority-element/solutions/146074/duo-shu-yuan-su-by-leetcode-solution/
        /// https://leetcode.cn/problems/majority-element/solutions/1496128/169-duo-shu-yuan-su-by-stormsunshine-zwvw/
        /// 
        /// 排序後的特性
        /// 在陣列 nums 中，若某個數是 Majority Element，則它的出現次數超過了陣列長度的一半（n / 2）。
        /// 當陣列排序後，Majority Element 必定會佔據整個陣列的中間部分。
        ///  例如，假設 n = 9，n / 2 = 4，那麼排序後的陣列中間索引是 nums[4]。
        ///  Majority Element 的出現次數超過一半，因此中間索引必定是 Majority Element。
        ///  
        /// 數學性質
        /// 排序後，陣列中超過一半的元素（Majority Element）必定會覆蓋陣列的中間位置，因為：
        /// 1. 若某元素的出現次數超過 n / 2，那麼它至少佔據陣列的一半以上。
        /// 2. 當陣列排序後，該元素必然會成為中間部分的元素。
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MajorityElement2(int[] nums)
        {
            // sort
            Array.Sort(nums);
            // 返回中間位置的元素
            return nums[nums.Length / 2];
        }

    }
}
