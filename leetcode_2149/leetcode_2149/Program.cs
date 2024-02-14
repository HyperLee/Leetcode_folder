using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2149
{
    internal class Program
    {
        /// <summary>
        /// 2149. Rearrange Array Elements by Sign
        /// https://leetcode.com/problems/rearrange-array-elements-by-sign/description/?envType=daily-question&envId=2024-02-14
        /// 2149. 按符号重排数组
        /// https://leetcode.cn/problems/rearrange-array-elements-by-sign/description/
        /// 
        /// 輸入順序 不能變
        /// 輸出要 一正一負 這樣排序輸出
        /// 開頭第一個要正數
        /// 長度為 n 必為偶數
        /// 故 正數: n / 2 個
        ///    負數: n / 2 個
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 3, 1, -2, -5, 2, -4 };
            var ans = RearrangeArray2(input);
            foreach (int i in ans) 
            {
                Console.Write(i + ", ");
            }
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/rearrange-array-elements-by-sign/solutions/2599112/2149-an-fu-hao-zhong-pai-shu-zu-by-storm-ai7n/
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] RearrangeArray(int[] nums)
        {
            int n = nums.Length;
            int[] result = new int[n];

            // even:偶數位置, odd:奇數位置; 偶數奇數要交叉放 故距離 + 2
            for(int i = 0, even = 0, odd = 1; i < n; i++)
            {
                if (nums[i] > 0)
                {
                    result[even] = nums[i];
                    even += 2;
                }
                else
                {
                    result[odd] = nums[i];
                    odd += 2;
                }

            }

            return result;
        }


        /// <summary>
        /// 方法2
        /// 利用 list 
        /// 分別存放 正數 與 負數
        /// 最後組合一起 在輸出
        /// 
        /// 我個人偏好這方法 比較好理解
        /// 
        /// 長度為 n 必為偶數
        /// 故 正數: n / 2 個
        ///    負數: n / 2 個
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] RearrangeArray2(int[] nums)
        {
            List<int> result = new List<int>();
            List<int> even = new List<int>();
            List<int> odd = new List<int>();

            for(int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > 0)
                {
                    // 正數 偶數位置
                    even.Add(nums[i]);
                }
                else
                {
                    // 負數 奇數位置
                    odd.Add(nums[i]);
                }
            }

            // 將正數與負數組合再一起
            // 跑 nums.Length / 2 次數即可
            for (int i = 0; i < (nums.Length / 2); i++)
            {
                result.Add(even[i]);
                result.Add(odd[i]);
            }

            return result.ToArray();
        }
    }
}
