using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_088
{
    internal class Program
    {
        /// <summary>
        /// 88. Merge Sorted Array
        /// https://leetcode.com/problems/merge-sorted-array/
        /// 
        /// 88. 合并两个有序数组
        /// https://leetcode.cn/problems/merge-sorted-array/description/
        /// 
        /// 題目要求 合併後 放在 nums1 回傳
        /// 不是用 function 回傳
        /// 
        /// 回傳是 遞增 排序
        /// 小 至 大
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums1 = { 1, 2, 3, 0, 0, 0 };
            int m = 3;
            int[] nums2 = { 2, 5, 6 };
            int n = 3;

            //Merge(nums1, m, nums2, n);
            Merge2(nums1, m, nums2, n);
            //Merge3(nums1, m, nums2, n);
            Console.ReadKey();
        }


        /// <summary>
        /// 方法一：逆向双指针
        /// https://leetcode.cn/problems/merge-sorted-array/solution/he-bing-liang-ge-you-xu-shu-zu-by-leetco-rrb0/
        /// 
        /// </summary>
        /// <param name="nums1">non-decreasing order array</param>
        /// <param name="m">nums1.Length</param>
        /// <param name="nums2">non-decreasing order array</param>
        /// <param name="n">nums2.Length</param>
        public static void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            // 陣列 index 從 0 開始
            int p1 = m - 1;
            int p2 = n - 1;
            int tail = m + n - 1;
            int cur;

            while(p1 >=0 || p2 >=0)
            {
                if(p1 == -1)
                {
                    // p1 結束 取 p2
                    cur = nums2[p2--];
                }
                else if(p2 == -1)
                {
                    // p2 結束 取 p1
                    cur = nums1[p1--];
                }
                else if (nums1[p1] > nums2[p2])
                {
                    // 因為是逆向, 所以取大得出來.
                    cur = nums1[p1--];
                }
                else
                {
                    cur = nums2[p2--];
                }

                // 先放到 陣列最尾端, 從尾端往前放
                nums1[tail--] = cur;
            }

            Console.Write("method1: ");
            foreach (var s in nums1)
            {
                //Console.WriteLine(s);
                Console.Write(s + ", ");
            }

        }


        /// <summary>
        /// 方法二：雙指針
        /// https://leetcode.cn/problems/merge-sorted-array/solution/he-bing-liang-ge-you-xu-shu-zu-by-leetco-rrb0/
        /// 
        /// 依序去 nums1 與 nums2 中取出 element
        /// 然後比對 哪邊 小
        /// 小的取出來 然後放到 合併後的 新陣列裡面
        /// 新陣列長度 = m + n
        /// 陣列起始 index 從 0 開始
        /// </summary>
        /// <param name="nums1">non-decreasing order array</param>
        /// <param name="m">nums1.Length</param>
        /// <param name="nums2">non-decreasing order array</param>
        /// <param name="n">nums2.Length</param>
        public static void Merge2(int[] nums1, int m, int[] nums2, int n)
        {
            int p1 = 0, p2 = 0;
            // 暫存
            int[] sorted = new int[m + n];
            int cur;

            // 取出 cur, 放入 sorted 裡面
            while (p1 < m || p2 < n)
            {
                if(p1 == m)
                {
                    // p1 結束 取 p2
                    cur = nums2[p2++];
                }
                else if(p2 == n)
                {
                    // p2 結束 取 p1
                    cur = nums1[p1++];
                }
                else if (nums1[p1] < nums2[p2])
                {
                    // 取小的出來. 先放到陣列前面
                    cur = nums1[p1++];
                }
                else
                {

                    cur = nums2[p2++];
                }

                // 陣列 index 0 開始, 扣 1
                sorted[p1 + p2 - 1] = cur;
            }

            // sorted 放到 nums1 回傳答案
            for (int i = 0; i < m + n; i++)
            {
                nums1[i] = sorted[i];
            }

            Console.WriteLine("");
            Console.Write("method2: ");
            foreach (var s in nums1)
            {
                Console.Write(s + ", ");
            }
        }


        /// <summary>
        /// 方法三: 直接合併在排序
        /// </summary>
        /// <param name="nums1">non-decreasing order array</param>
        /// <param name="m">nums1.Length</param>
        /// <param name="nums2">non-decreasing order array</param>
        /// <param name="n">nums2.Length</param>
        public static void Merge3(int[] nums1, int m, int[] nums2, int n)
        {
            // 合併後長度 = nums1 長度 加上 nums2 長度
            for (int i = 0; i < n; i++)
            {
                // nums1 結尾接上 nums2 數值
                nums1[m + i] = nums2[i];
            }

            // 排序 遞增
            Array.Sort(nums1);

            Console.WriteLine("");
            Console.Write("method3: ");
            foreach (var s in nums1)
            {
                Console.Write(s + ", ");
            }
        }


    }
}
