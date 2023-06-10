using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1909
{
    internal class Program
    {
        /// <summary>
        /// 1909. Remove One Element to Make the Array Strictly Increasing
        /// https://leetcode.com/problems/remove-one-element-to-make-the-array-strictly-increasing/
        /// 
        /// 1909. 删除一个元素使数组严格递增
        /// https://leetcode.cn/problems/remove-one-element-to-make-the-array-strictly-increasing/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 1, 2, 10, 5, 7 };

            Console.WriteLine(CanBeIncreasing(input));
            Console.ReadKey();

        }



        /// <summary>
        /// https://leetcode.cn/problems/remove-one-element-to-make-the-array-strictly-increasing/solution/by-frank_practise-p80y/
        /// array裡面元素, 按順序一個一個測試
        /// 一次刪除一個 i 元素
        /// 刪除之後去跑 迴圈 看是不是 遞增
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static bool CanBeIncreasing(int[] nums)
        {

            for (int i = 0; i < nums.Length; i++)
            {
                List<int> list = new List<int>(nums);
                // 刪除 i 的值, 去Sort_OK測試 是不是遞增
                list.RemoveAt(i);

                if (Sort_OK(list))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 判斷是不是遞增
        /// i > i - 1
        /// 
        /// cnt加總要與 list.Count - 1 相同
        /// 代表這個迴圈順序都是遞增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool Sort_OK(List<int> list)
        {
            int cnt = 0;

            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] > list[i - 1])
                {
                    cnt++;
                }
            }

            return cnt == list.Count - 1;
        }

    }
}
