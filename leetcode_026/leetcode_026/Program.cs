﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_026
{
    internal class Program
    {
        /// <summary>
        /// leetcode 026
        /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 1, 1, 2 };

            //Console.WriteLine(RemoveDuplicates(nums));
            RemoveDuplicates(nums);

            Console.ReadKey();
        }



        /// <summary>
        /// 定义两个指针 fast 和 slows 分别为快指针和慢指针，快指针表示遍历数组到达的下标位置，慢指针表示下一
        /// 个不同元素要填入的下标位置，初始时两个指针都指向下标 1
        /// 。
        /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array/solution/shan-chu-pai-xu-shu-zu-zhong-de-zhong-fu-tudo/
        /// 
        /// 返回的長度,
        /// 只需要該長度內不重複即可
        /// 後續不用理會
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public static int RemoveDuplicates(int[] nums)
        {
            int n = nums.Length;

            if (n == 0)
            {
                return 0;
            }

            int fast = 1, slow = 1;

            while (fast < n)
            {
                // 说明 nums[fast] 和之前的元素都不同, 不要連續兩個一樣的
                if (nums[fast] != nums[fast - 1])
                {
                    // 不重複的數字取代原先出現重複的項目位置
                    nums[slow] = nums[fast];
                    ++slow;
                }
                ++fast;
            }

            Console.WriteLine("更新後陣列長度: " + slow);
            Console.WriteLine();

            Console.Write("修正後 nums[");
            foreach (int i in nums) 
            {
                Console.Write(i + ", ");
            }
            Console.Write("]");

            return slow;
        }


    }
}
