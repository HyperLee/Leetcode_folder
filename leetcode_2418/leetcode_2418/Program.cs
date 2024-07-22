using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2418
{
    internal class Program
    {
        /// <summary>
        /// 2418. Sort the People
        /// https://leetcode.com/problems/sort-the-people/
        /// 2418. 按身高排序
        /// https://leetcode.cn/problems/sort-the-people/description/
        /// 
        /// 按照身高排序輸出
        /// 给你一个字符串数组 names ，和一个由 互不相同 的正整数组成的数组 heights 。两个数组的长度均为 n 。
        /// 对于每个下标 i，names[i] 和 heights[i] 表示第 i 个人的名字和身高。
        /// 请按身高 降序 顺序返回对应的名字数组 names 。
        /// names: 名稱
        /// heights: 身高
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] names =  {"Mary", "John", "Emma"};
            int[] heights = {180, 165, 170};

            Console.WriteLine("方法1");
            SortPeople(names, heights);
            Console.WriteLine("---------------");
            Console.WriteLine("方法2");
            SortPeople2(names, heights);
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/sort-the-people/solution/by-c-chzch-aq0g/
        /// 
        /// 身高不會一樣, 但是名稱有可能會
        /// 所以拿身高當作索引來建立
        /// 
        /// Dictionary<key, value>
        /// key: 身高
        /// Vlue: 名稱
        /// 
        /// 將資料組整個排序
        /// 和方法二不同,
        /// 方法二是只存下標
        /// 經過排序過後.
        /// 要從原先資料裡面取出那個排序過後資料
        /// 
        /// </summary>
        /// <param name="names"></param>
        /// <param name="heights"></param>
        /// <returns></returns>
        public static string[] SortPeople(string[] names, int[] heights)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            for (int i = 0; i < names.Length; i++)
            {
                dict.Add(heights[i], names[i]);
            }

            // 排序 遞減; 用身高去排序
            var query = dict.OrderByDescending(k => k.Key);
            // 輸出 res
            string[] res = new string[names.Length];
            int index = 0;
            foreach (var item in query)
            {
                // 將結果塞入res 輸出
                res[index] = item.Value;
                index++;
            }

            // 
            foreach (var s in res)
            {
                Console.WriteLine(s);
            }

            return res;
        }


        /// <summary>
        /// 方法二 改用 陣列去處理 沒有使用 Dictionary
        /// 陣列儲存 index(下標) 
        /// 
        /// https://leetcode.cn/problems/sort-the-people/solutions/2242694/an-shen-gao-pai-xu-by-leetcode-solution-p6bk/
        /// https://leetcode.cn/problems/sort-the-people/solutions/1848000/python-yi-xing-by-endlesscheng-xnvy/
        /// https://leetcode.cn/problems/sort-the-people/solutions/2244309/python3javacgotypescript-yi-ti-yi-jie-pa-ixpo/
        /// https://leetcode.cn/problems/sort-the-people/solutions/2589753/2418-an-shen-gao-pai-xu-by-stormsunshine-w5ev/
        /// 
        /// 身高不會一樣, 但是名稱有可能會
        /// 所以拿身高當作索引來建立
        /// </summary>
        /// <param name="names"></param>
        /// <param name="heights"></param>
        /// <returns></returns>
        public static string[] SortPeople2(string[] names, int[] heights)
        {
            int n = names.Length;
            int[] indices = new int[n];
            // 儲存 索引數組(index, 下標)
            for(int i = 0; i < n; i++)
            {
                indices[i] = i;
            }

            // 將 indices 排序; 遞減
            Array.Sort(indices, (a, b) => heights[b] - heights[a]);

            string[] res = new string[n];
            for(int i = 0; i < n; i++)
            {
                // 因是indices只有儲存index,
                // 所以名稱取法要從 names裡面取出 indices; names[indices[i]]
                res[i] = names[indices[i]];
            }

            // 
            foreach(var s in res)
            {
                Console.WriteLine(s);
            }

            return res;
        }

    }
}
