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
        /// 
        /// String.Join:
        /// https://dotblogs.com.tw/webber18/2020/06/12/154200
        /// https://ithelp.ithome.com.tw/articles/10105683
        /// 
        /// Key: 將輸入的strs經過排序過後的 str
        /// value: 排序前的輸入字串 strs[i]
        /// 
        /// 
        ///  字母异位词:同樣char, 不同排序組合而成的一個單字或是片段
        ///  題目要求很簡單, 將同樣的 字母异位词 進行排列
        ///  相同的放在一起即可
        ///  
        ///  所以做法就是
        ///  1.遍歷每個輸入的單字, 將單字從新排列 ( 字母异位词 具有相同的char)
        ///  2.判斷每個輸入的單字是不是相同的排列, 相同就加入, 不同就新增
        ///  3.輸出資料, 這裡要注意. 輸出資料是輸出原先輸入的單字,將相同的字母异位词放在一起輸出
        ///  
        /// 宣告部分需要注意, 是 IList<IList<string>> 輸入, 輸出
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>

        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            Dictionary<string, IList<string>> dic = new Dictionary<string, IList<string>>();
            IList<IList<string>> res = new List<IList<string>>();

            for (int i = 0; i < strs.Length; i++)
            {
                // 遍歷每個輸入的單字, 型態轉為char
                char[] a = strs[i].ToArray();
                // 排序
                Array.Sort(a);
                // 型態轉換回字串
                string str = new string(a);

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

            // 依序將dic.Keys裡面的value取出來, 放到res輸出
            /* // 方法1:
            foreach (var item in dic.Keys)
            {
                res.Add(dic[item]);
            }
            */

            // 依序將dic.Keys裡面的value取出來, 放到res輸出
            // 方法2:
            foreach (KeyValuePair<string, IList<string>> kvp in dic)
            {
                res.Add(kvp.Value);
            }

            // Console 輸出 res
            Console.Write("res: [ ");
            foreach (var item in res) 
            {
                Console.Write("[");
                for(int i = 0; i < item.Count; i++)
                {
                    //Console.Write(item[i] + ", ");
                    if(i == item.Count - 1)
                    {
                        Console.Write(item[i]);
                    }
                    else
                    {
                        Console.Write(item[i] + ", ");
                    }
                }
                Console.Write("]");
            }
            Console.Write(" ]");

            return res;

        }

    }
}
