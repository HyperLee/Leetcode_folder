using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2215
{
    internal class Program
    {
        /// <summary>
        /// 2215. Find the Difference of Two Arrays
        /// https://leetcode.com/problems/find-the-difference-of-two-arrays/
        /// 
        /// 2215. 找出两数组的不同
        /// https://leetcode.cn/problems/find-the-difference-of-two-arrays/
        /// 
        /// 给你两个下标从 0 开始的整数数组 nums1 和 nums2 ，请你返回一个长度为 2 的
        /// 列表 answer ，其中：
        /// answer[0] 是 nums1 中所有 不 存在于 nums2 中的 不同 整数组成的列表。
        /// answer[1] 是 nums2 中所有 不 存在于 nums1 中的 不同 整数组成的列表。
        /// 注意：列表中的整数可以按 任意 顺序返回。
        /// 
        /// 回傳不要同時存在於兩個array中的元素,且去除重覆
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums1 = new int[] { 1, 2, 3 };
            int[] nums2 = new int[] { 2, 4, 6 };

            //Console.WriteLine(FindDifference(nums1, nums2));
            FindDifference(nums1, nums2);
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/find-the-difference-of-two-arrays/solution/c_hashset_api_intersectwith_exceptwith-b-7kru/
        /// 解題概念 hashset  hashtable
        /// 
        /// HashSet<T> 類別
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.generic.hashset-1?view=net-7.0
        /// 
        /// IntersectWith 	交叉 口  修改目前的 HashSet<T> 物件，以便僅包含該物件和指定之集合中同時出現的項目。
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.generic.hashset-1.intersectwith?view=net-7.0
        /// 
        /// ExceptWith 	設定減法 將指定集合中的所有項目從目前的 HashSet<T> 物件中移除。
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.generic.hashset-1.exceptwith?view=net-7.0
        /// 
        /// hashset教學 建議先看這篇 才比較知道如何應用
        /// https://www.gushiciku.cn/pl/p5zK/zh-tw
        /// 所謂的HashSet，它是一個高效能，無序的集合，因此HashSet它並不能做排序操作，也不能包含任何重複的元素
        /// ，Hashset 也不能像陣列那樣使用索引，所以在 HashSet 上你無法使用 for 迴圈
        /// ，只能使用 foreach 進行迭代，HashSet 通常用在處理元素的唯一性上有著超高的效能。
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public static IList<IList<int>> FindDifference(int[] nums1, int[] nums2)
        {
            HashSet<int> set1 = new HashSet<int>(nums1); // add nums1
            HashSet<int> set2 = new HashSet<int>(nums2); // add nums2
            HashSet<int> set3 = new HashSet<int>(set1); // add nums1
            
            set3.IntersectWith(set2); // 只有兩個 HashSet 中都存在的元素才會輸出set3
            set1.ExceptWith(set3);  // 它返回的元素為：set1中有，set3中沒有 的最終結果
            set2.ExceptWith(set3); // 它返回的元素為：set2中有，set3中沒有 的最終結果

            IList<IList<int>> ret = new List<IList<int>>();
            ret.Add(set1.ToList()); // add set1
            ret.Add(set2.ToList()); // add set2

            foreach (var num in ret)
            {
                foreach(var value in num)
                {
                    // 輸出陣列值
                    Console.WriteLine(value);
                }
            }

            return ret;

        }


       /// <summary>
       /// 非hash
       /// 單純字串比對
       /// =>未完成
       /// </summary>
       /// <param name="nums1"></param>
       /// <param name="nums2"></param>
       /// <returns></returns>

        public static IList<IList<int>> FindDifference2(int[] nums1, int[] nums2)
        {

            StringBuilder sb1 = new StringBuilder();
            for(int i = 0; i < nums1.Length; i++)
            {
                sb1.Append(nums1[i]);
            }

            StringBuilder sb2 = new StringBuilder();
            for(int i = 0; i < nums2.Length; i++)
            {
                sb2.Append(nums2[i]);
            }

            IList<IList<int>> ret = new List<IList<int>>();

            for(int i = 0; i < sb1.Length; i++)
            {
                int a = nums1[i];
                
            }

            return ret;
        }

    }
}
