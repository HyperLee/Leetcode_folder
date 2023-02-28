using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_004
{
    class Program
    {
        /// <summary>
        /// leetcode 004
        /// https://leetcode.com/problems/median-of-two-sorted-arrays/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums1 = { 1, 2, 9 };
            int[] nums2 = { 3 };
            Console.WriteLine(FindMedianSortedArrays(nums1, nums2));
            Console.ReadKey();
        }

        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10221455
        /// https://www.delftstack.com/zh-tw/howto/csharp/merge-two-arrays-in-csharp/
        /// https://docs.microsoft.com/zh-tw/dotnet/csharp/how-to/concatenate-multiple-strings#code-try-4 // String.Concat 或 String.Join
        /// 
        /// Version1: 在 C# 中使用 Array.Copy() 方法合併兩個陣列
        /// 1. 使用Array.Copy 合併兩陣列
        /// 2. Array.Sort 排序大小
        /// 3. 偶數長度取 最中間的兩個數值的平均數
        ///    奇數長度取 中間值出來即可
        /// 4. 目前看起來 速度不佳
        /// 
        /// Version2: 在 C# 中使用 LINQ 方法合併兩個陣列
        /// 1. 我們可以使用 Concat() 函式合併兩個陣列的元素。 然後，我們可以使用 ToArray() 函式將結果轉換為陣列。
        /// 2. Concat(x) 函式在 C# 中的呼叫物件末尾連線引數 x 的元素。然後，我們可以使用 ToArray() 函式將結果轉換為陣列。
        /// 3,4 步驟同上
        /// 
        /// 
        /// version3: 可以用list方式 來做 陣列串接
        /// https://blog.csdn.net/qq_45244974/article/details/115320059
        /// 
        /// 中位數定義 wiki
        /// https://zh.wikipedia.org/wiki/%E4%B8%AD%E4%BD%8D%E6%95%B8
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int[] arr1 = nums1;
            int[] arr2 = nums2;
            int[] merged = new int[arr1.Length + arr2.Length];

            /* // Version1 combine
            Array.Copy(arr1, merged, arr1.Length);
            Array.Copy(arr2, 0, merged, arr1.Length, arr2.Length);
            */

            // Version2 combine
            merged = arr1.Concat(arr2).ToArray();
            
            // sort
            Array.Sort(merged);
            int length = merged.Length;
            if (length % 2 == 0)
            {
                // 偶數
                int index = length / 2;
                return (merged[index - 1] + merged[index]) * 1.0 / 2;
            }
            else
            {
                // 奇數
                int index = (length - 1) / 2;
                return merged[index];
            }

        }

    }
}
