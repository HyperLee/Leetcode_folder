using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_026
{
    internal class Program
    {
        /// <summary>
        /// 26. Remove Duplicates from Sorted Array
        /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array/
        /// 
        /// 26. 删除有序数组中的重复项
        /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array/description/
        /// 
        /// 刪除重覆的 element
        /// 並且要保持原先輸入順序
        /// 每個 element 只能出現一次
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 1, 1, 2 };
            RemoveDuplicates(nums);

            int[] nums2 = { 1, 1, 2 };
            RemoveDuplicates2(nums2);

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
        /// 
        /// 回傳長度 index 從 1 開始不是 0
        /// 所以 slow 指向 下一個要取代的 index
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

            // slow 從 1 開始, 指向下一個需要取代的 index
            int fast = 1, slow = 1;

            while (fast < n)
            {
                // 不要連續兩個 element 都相同
                if (nums[fast] != nums[fast - 1])
                {
                    // 不重複的數字取代原先出現重複的項目位置
                    nums[slow] = nums[fast];
                    slow++;
                }

                fast++;
            }

            Console.WriteLine("方法1: ");
            Console.WriteLine("更新後陣列長度: " + slow);
            Console.Write("修正後 nums[");
            foreach (int i in nums) 
            {
                Console.Write(i + ", ");
            }
            Console.Write("]");
            Console.WriteLine();
            Console.WriteLine();

            return slow;
        }


        /// <summary>
        /// for 迴圈寫法
        /// 要注意 起始位置從 1 開始
        /// 因為要與 前一個比對 是不是 element 相同
        /// 所以從 1 開始 不是 0
        /// 
        /// 回傳長度 index 從 1 開始不是 0
        /// 所以 left 指向 下一個要取代的 index
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int RemoveDuplicates2(int[] nums)
        {
            int n = nums.Length;
            // 從 1 開始, 指向下一個需要取代的 index
            int left = 1;

            for (int right = 1; right < n; right++)
            {
                // 不要連續兩個 element 都相同
                if (nums[right] != nums[right - 1])
                {
                    // 不重複的數字取代原先出現重複的項目位置
                    nums[left] = nums[right];
                    left++;
                }
            }

            Console.WriteLine("方法2: ");
            Console.WriteLine("更新後陣列長度: " + left);
            Console.Write("修正後 nums[");
            foreach (var value in nums)
            {
                Console.Write(value + ", ");
            }
            Console.Write("]");

            return left;
        }


    }
}
