using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace leetcode_819
{
    internal class Program
    {
        /// <summary>
        /// leetcode 819 Most Common Word
        /// https://leetcode.com/problems/most-common-word/
        /// 最常见的单词
        /// https://leetcode.cn/problems/most-common-word/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string paragraph = @"L, P! X! C; u! P? w! P. G, S? l? X? D. w? m? f? v, x? i. z; x' m! U' M! j? V; l. S! j? r, K. O? k? p? p, H! 
t! z' X! v. u; F, h; s? X? K. y, Y! L; q! y? j, o? D' y? F' Z; E? W; W' W! n! p' U. N; w? V' y! Q; J, o! T? g? 
o! N' M? X? w! V. w? o' k.W.y, k; o' m! r; i, n. k, w; U? S? t; O' g' z. V. N? z, W? j! m? W! h; t! V' T! Z? R' 
w, w? y? y; O' w; r? q. G, V. x? n, Y; Q. s? S. G. f, s! U? l. o! i. L; Z' X! u.y, Q.q; Q, D; V.m.q.s? Y, U; p?
u!q ? h ? O.W' y? Z! x! r. E, R, r' X' V, b. z, x! Q; y, g' j; j.q; W; v' X! J' H? i' o? n, Y. X! x? h? u; T? l! o? z. 
K' z' s; L? p? V' r. L? Y; V! V' S.t? Z' T' Y.s? i? Y! G? r; Y; T! h! K; M.k.U; A! V? R? C' x! X. M; z' V! w.N.T? Y'
 w? n, Z, Z? Y' R; V' f; V' I; t? X? Z; l? R, Q! Z. R. R, O. S! w; p' T.u? U!n, V, M.p? Q, O? q' t. B, k. u. H' T; T? S;
            Y! S! i? q!K' z' S! v; L.x; q; W? m? y, Z! x.y.j? N' R' I? r? V! Z; s, O? s; V, I, e? U' w! T? T! u; U! e? w? z; t! C! z? 
 U, p' p! r. x; U! Z; u! j; T! X! N' F? n!P' t, X. s; q'";

            string[] banned = { "m", "i", "s", "w", "y", "d", "q", "l", "a", "p", "n", "t", "u", "b", "o", "e", "f", "g", "c", "x" };

            Console.WriteLine(MostCommonWord(paragraph, banned));
            Console.ReadKey();
        }


        /// <summary>
        /// 最簡單 最蠢的方法
        /// 把 符號都 replace 一遍
        /// 
        /// 利用 dic 統計 每個單字 出現 次數
        /// 最後用 OrderByDescending 來排序 大至小
        /// 取出 最大的那一個 唯一解答
        /// </summary>
        /// <param name="paragraph"></param>
        /// <param name="banned"></param>
        /// <returns></returns>
        public static string MostCommonWord(string paragraph, string[] banned)
        {
            // key: 單字 ; value:累積出現次數
            Dictionary<string, int> dic = new Dictionary<string, int>();

            // 取代為空格, 避免原始字串連續無空白 case 變成連續的文字
            paragraph = paragraph.Replace(",", " ").Replace(".", " ").Replace("!", " ").Replace("@", " ").Replace("#", " ").Replace("$", " ").Replace("%", "  ").ToLower();
            paragraph = paragraph.Replace("!", " ").Replace("\\", " ").Replace("/", " ").Replace("?", " ").Replace("'", " ").Replace(";", " ");

            // 統一轉小寫
            paragraph = paragraph.ToLower();

            string[] subs = paragraph.Split(' ');

            for (int i = 0; i < subs.Length; i++)
            {
                // 排除 空白 空格!?
                if (subs[i].ToString().Trim() != "")
                {
                    if (dic.ContainsKey(subs[i]))
                    {
                        dic[subs[i]]++;
                    }
                    else
                    {
                        dic.Add(subs[i], 1);
                    }
                }
            }

            // 去除 banned 單字
            foreach(string a in banned) 
            {
                if(dic.ContainsKey(a))
                {
                    dic.Remove(a);
                }
            }


            // sort 出現次數 value  大至小排序
            Dictionary<string, int> dic2 = new Dictionary<string, int>();
            dic2 = dic.OrderByDescending(o => o.Value).ToDictionary(o => o.Key, p => p.Value);

            // 取出第一個 也是最多次數的
            return dic2.First().Key;

        }

    }
}
