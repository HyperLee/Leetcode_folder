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
        /// 4. Median of Two Sorted Arrays
        /// https://leetcode.com/problems/median-of-two-sorted-arrays/
        /// 
        /// 4. 寻找两个正序数组的中位数
        /// https://leetcode.cn/problems/median-of-two-sorted-arrays/description/
        /// 
        /// 將題目提供個兩個陣列, 求出中位數 數值是多少
        /// 不是陣列 index, 是求出平均數值
        /// 
        /// 對於有限的數集，可以通過把所有觀察值高低排序後找出正中間的一個作爲中位數。
        /// 如果觀察值有偶數個，則中位數不唯一，通常取最中間的兩個數值的平均數作爲中位數。
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
        /// 
        /// 解法:
        /// 1. 宣告 int[] merged 用來暫存
        /// 2. 將 nums1 與 nums2 合併 (nums1 尾端串接 nums2) 放到 merged
        /// 3. 因要計算中位數, 所以要將 merged 排序
        /// 4. 根據長度奇偶數不同, 有不同計算方式
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
            
            // sort 小至大 asc
            Array.Sort(merged);

            // 求出中位數
            int length = merged.Length;
            if (length % 2 == 0)
            {
                // 偶數
                int index = length / 2;
                // * 1.0 ==> 轉 double. 小數點下位數顯示
                return (merged[index - 1] + merged[index]) * 1.0 / 2;
            }
            else
            {
                // 奇數, (length - 1) => 去除小數點, 取整數 index 
                int index = (length - 1) / 2;
                return merged[index];
            }

        }

    }
}
