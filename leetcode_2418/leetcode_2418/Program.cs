using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2418
{
    internal class Program
    {
        /// <summary>
        /// https://leetcode.com/problems/sort-the-people/
        /// leetcode 2418
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

            SortPeople2(names, heights);
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/sort-the-people/solution/by-pan-pan-sn-eseo/
        /// </summary>
        /// <param name="names"></param>
        /// <param name="heights"></param>
        /// <returns></returns>
        public static string[] SortPeople(string[] names, int[] heights)
        {
            //审题很重要，身高各不相同，名字却有可能重复
            Dictionary<int, string> dic = new Dictionary<int, string>();
            for (int i = 0; i < names.Length; i++)
                dic.Add(heights[i], names[i]);

            var orderDic = dic.OrderByDescending(k => k.Key);

            return orderDic.Select(v => v.Value).ToArray();
        }


        /// <summary>
        /// https://leetcode.cn/problems/sort-the-people/solution/by-c-chzch-aq0g/
        /// 
        /// 身高不會一樣
        /// 但是名稱會
        /// 所以 身高當作key
        /// 名稱當作 值
        /// </summary>
        /// <param name="names"></param>
        /// <param name="heights"></param>
        /// <returns></returns>
        public static string[] SortPeople2(string[] names, int[] heights)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            for (int i = 0; i < names.Count(); i++)
            {
                dict.Add(heights[i], names[i]);
            }
            var query = dict.OrderByDescending(k => k.Key);
            string[] res = new string[names.Count()];
            int index = 0;
            foreach (var p in query)
            {
                res[index] = p.Value;
                index++;
                Console.WriteLine(p.Value);
            }
            return res;
        }

    }
}
