using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_912
{
    internal class Program
    {
        /// <summary>
        /// leetcode 920 Sort an Array
        /// https://leetcode.com/problems/sort-an-array/description/
        /// 
        /// 912. 排序数组
        /// https://leetcode.cn/problems/sort-an-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = new int[] { 5, 2, 3, 1 , 99, 77};

            //Console.WriteLine(SortArray(nums));
            SortArray2(nums);
            SortArray3(nums);
            Console.ReadKey();
        }


        /// <summary>
        /// 偷懶 直接用 現成的function
        /// 
        /// 排序寫法 可參考
        /// https://leetcode.cn/problems/sort-an-array/solution/by-stormsunshine-mjb5/
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] SortArray(int[] nums)
        {
            Array.Sort(nums);

            foreach (int i in nums)
            {
                Console.Write(i + " ");
            }

            return nums;

        }


        /// <summary>
        /// bubble sort
        /// 
        /// https://leetcode.cn/problems/sort-an-array/solutions/178305/pai-xu-shu-zu-by-leetcode-solution/
        /// https://leetcode.cn/problems/sort-an-array/solutions/179489/fu-xi-ji-chu-pai-xu-suan-fa-java-by-liweiwei1419/
        /// https://leetcode.cn/problems/sort-an-array/solutions/1496053/by-stormsunshine-mjb5/
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] SortArray2(int[] nums)
        {
            int length = nums.Length;

            for(int i = 1; i < length; i++)
            {
                for(int j = 0; j < length - 1; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        int temp = nums[j];
                        nums[j] = nums[j + 1];
                        nums[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("SortArray2: ");
            foreach (int i in nums)
            {
                Console.Write(i + " ");
            }

            return nums;

        }



        /// <summary>
        /// 插入排序
        /// 每次将一个数字插入一个有序的数组里，成为一个长度更长的有序数组，有限次操作以后，数组整体有序。
        /// 
        /// 1. 初始狀態： 將第一個元素視為已經排序。
        /// 2. 取下未排序的元素： 從待排序的元素中取出第一個元素。
        /// 3. 插入到已排序的序列： 從後向前掃描已排序的序列，找到比它小的元素，將所有比
        ///    它大的元素向後移一位。
        /// 4. 插入元素： 將新元素插入到找到的位置後。
        /// 5. 重複步驟2-4： 直到所有元素都被插入。
        /// 
        /// https://leetcode.cn/problems/sort-an-array/solutions/178305/pai-xu-shu-zu-by-leetcode-solution/
        /// https://leetcode.cn/problems/sort-an-array/solutions/179489/fu-xi-ji-chu-pai-xu-suan-fa-java-by-liweiwei1419/
        /// https://leetcode.cn/problems/sort-an-array/solutions/1496053/by-stormsunshine-mjb5/
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] SortArray3(int[] nums)
        {
            int n = nums.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = nums[i];
                int j = i - 1;

                // 将大于key的元素向右移动
                while (j >= 0 && nums[j] > key)
                {
                    nums[j + 1] = nums[j];
                    j--;
                }
                nums[j + 1] = key;
            }

            Console.WriteLine(" ");
            Console.WriteLine("SortArray3: ");
            foreach (int i in nums)
            {
                Console.Write(i + " ");
            }

            return nums;
        }

    }
}
