using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_540
{
    internal class Program
    {
        /// <summary>
        /// leetcode 540  Single Element in a Sorted Array  
        /// 有序数组中的单一元素
        /// https://leetcode.com/problems/single-element-in-a-sorted-array/
        /// 
        /// 與leetcode 136 類似 解法共用
        /// https://leetcode.com/problems/single-number/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] num = new int[] { 1, 1, 2, 3, 3, 4, 4, 8, 8 };

            Console.WriteLine(SingleNonDuplicate3(num));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/single-element-in-a-sorted-array/solution/you-xu-shu-zu-zhong-de-dan-yi-yuan-su-by-y8gh/
        /// 導入 位元運作 XOR 原理
        /// https://zh.wikipedia.org/zh-tw/%E4%BD%8D%E6%93%8D%E4%BD%9C#%E6%8C%89%E4%BD%8D%E5%BC%82%E6%88%96%EF%BC%88XOR%EF%BC%89
        /// 兩樹相同為0, 不同為1
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int SingleNonDuplicate(int[] nums)
        {
            int left = 0;
            int right = nums.Length - 1;
            while (left < right) 
            {
                int mid = left + ((right - left) / 2);
                if (nums[mid] == nums[mid ^ 1])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return nums[left];
        }


        /// <summary>
        /// 字典方式 實作
        /// Dictionary
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int SingleNonDuplicate2(int[] nums)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for(int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(nums[i]))
                {
                    dic[nums[i]]++;
                }
                else
                {
                    dic[nums[i]] = 1;
                }
            }

            foreach(var res in dic.Keys)
            {
                if (dic[res] == 1)
                {
                    return res;
                }
            }

            return -1;
        }



        /// <summary>
        /// XOR 作法
        /// https://ithelp.ithome.com.tw/articles/10213278
        /// 因 相同為0 不同為1 , 即可以找出 不同那一個人
        /// 使用XOR順序 不重要 因為 相同就是會為0, 不同就是唯一
        /// XOR具有交換律和結合律。
        /// 將它的運算的數字左右調換，或者哪個先做，哪個後做，均不影響結果。
        /// 也就是說，將每個數都進行XOR後，結果會留存下那個唯一單獨的數字。
        /// 
        /// input: { 1, 1, 2, 3, 3, 4, 4, 8, 8 }
        /// 一開始預設 res = 0;
        /// 1 xor 0 = 1
        /// 1 xor 1 = 0
        /// 2 xor 0 = 2
        /// 2 xor 3 = 1
        /// 1 xor 3 = 2
        /// 2 xor 4 = 6
        /// 6 xor 4 = 2
        /// 2 xor 8 = 10
        /// 10 xor 8 = 2
        /// ==> res = 2;
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int SingleNonDuplicate3(int[] nums)
        {
            int res = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                res = res ^ nums[i];
            }

            return res;
        }

    }
}
