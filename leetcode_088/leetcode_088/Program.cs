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
        /// leetcode 088
        /// https://leetcode.com/problems/merge-sorted-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums1 = { 1, 2, 3, 0, 0, 0 };
            int m = 3;
            int[] nums2 = { 2, 5, 6 };
            int n = 3;

            Merge(nums1, m, nums2, n);
            Merge2(nums1, m, nums2, n);
            Merge3(nums1, m, nums2, n);
            Console.ReadKey();
        }


        /// <summary>
        /// 方法三：逆向双指针
        /// https://leetcode.cn/problems/merge-sorted-array/solution/he-bing-liang-ge-you-xu-shu-zu-by-leetco-rrb0/
        /// 
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="m"></param>
        /// <param name="nums2"></param>
        /// <param name="n"></param>
        public static void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int p1 = m - 1;
            int p2 = n - 1;
            int tail = m + n - 1;
            int cur;
            while(p1 >=0 || p2 >=0)
            {
                if(p1 == -1)
                {
                    cur = nums2[p2--];
                }
                else if(p2 == -1)
                {
                    cur = nums1[p1--];
                }
                else if (nums1[p1] > nums2[p2])
                {
                    cur = nums1[p1--];
                }
                else
                {
                    cur = nums2[p2--];
                }
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
        /// 方法二：双指针
        /// https://leetcode.cn/problems/merge-sorted-array/solution/he-bing-liang-ge-you-xu-shu-zu-by-leetco-rrb0/
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="m"></param>
        /// <param name="nums2"></param>
        /// <param name="n"></param>
        public static void Merge2(int[] nums1, int m, int[] nums2, int n)
        {
            int p1 = 0, p2 = 0;
            int[] sorted = new int[m + n];
            int cur;

            while(p1 < m || p2 < n)
            {
                if(p1 == m)
                {
                    cur = nums2[p2++];
                }
                else if(p2 == n)
                {
                    cur = nums1[p1++];
                }
                else if (nums1[p1] < nums2[p2])
                {
                    cur = nums1[p1++];
                }
                else
                {
                    cur = nums2[p2++];
                }
                sorted[p1 + p2 - 1] = cur;
            }
            for(int i = 0; i != m + n; ++i)
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
        /// 方法一：直接合并后排序
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="m"></param>
        /// <param name="nums2"></param>
        /// <param name="n"></param>
        public static void Merge3(int[] nums1, int m, int[] nums2, int n)
        {
            for(int i = 0; i !=n; ++i)
            {
                nums1[m + i] = nums2[i];
            }
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
