using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_049
{
    internal class Program
    {
        /// <summary>
        /// 49. Group Anagrams
        /// https://leetcode.com/problems/group-anagrams/
        /// 49. 字母异位词分组
        /// https://leetcode.cn/problems/group-anagrams/?envType=study-plan-v2&envId=top-interview-150
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] input  = { "eat", "tea", "tan", "ate", "nat", "bat" };
            //Console.WriteLine(GroupAnagrams(input));
            GroupAnagrams(input);
            Console.ReadKey();
        }


        /// <summary>
        /// 參考來源:
        /// https://leetcode.cn/problems/group-anagrams/solutions/520655/jie-by-long-yu-8-8zd0/?envType=study-plan-v2&envId=top-interview-150
        /// 
        /// 思路就是弄个字典，把每个字符串排序比较，排序的string作为key
        /// ,值为strs[i]，遍历完strs,在从dic取值
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>

        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            var dic = new Dictionary<string, IList<string>>();
            IList<IList<string>> res = new List<IList<string>>();

            for (int i = 0; i < strs.Length; i++)
            {
                // 輸入的每個單字 取出然後排序 暫存至str
                char[] a = strs[i].ToArray();
                Array.Sort(a);
                string str = String.Join("", a.Select(x => x.ToString()).ToArray());

                if (dic.ContainsKey(str))
                {
                    // 已存在就加入
                    dic[str].Add(strs[i]);
                }
                else
                {
                    // 不存在就新增
                    dic[str] = new List<string> { strs[i] };
                }

            }

            // 取出所需放入res
            foreach (var item in dic.Keys)
            {
                res.Add(dic[item]);
            }

            // CMD輸出
            foreach(var item in res) 
            {
                for(int i = 0; i < item.Count; i++)
                {
                    Console.WriteLine(item[i]);
                }
            }

            return res;

        }

    }
}
