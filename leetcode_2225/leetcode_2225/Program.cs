using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2225
{
    internal class Program
    {
        /// <summary>
        /// 2225. Find Players With Zero or One Losses
        /// https://leetcode.com/problems/find-players-with-zero-or-one-losses/description/?envType=daily-question&envId=2024-01-15
        /// 2225. 找出输掉零场或一场比赛的玩家
        /// https://leetcode.cn/problems/find-players-with-zero-or-one-losses/description/
        /// 
        /// 不規則陣列
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/arrays#jagged-arrays
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // [][]:不規則陣列, 
            int[][] input = 
            { 
                new int[] { 1, 3 }, 
                new int[] { 2, 3 }, 
                new int[] { 3, 6 },
                new int[] { 5, 6 },
                new int[] { 5, 7 },
                new int[] { 4, 5 },
                new int[] { 4, 8 },
                new int[] { 4, 9 },
                new int[] { 10, 4 },
                new int[] { 10, 9 }
            };

            //Console.WriteLine(FindWinners(input));
            FindWinners(input);

            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.com/problems/find-players-with-zero-or-one-losses/solutions/4567354/c-solution-for-find-players-with-zero-or-one-losses-problem/
        /// 
        /// 不規則陣列
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/arrays#jagged-arrays
        /// 
        /// 題目要回傳 [贏家, 只輸一次] 這種規格資料
        /// 
        /// 
        /// 實作方法應該有好幾種,
        /// Dictionary
        /// List
        /// hash
        /// 比較想用Dictionary. 個人習慣
        /// https://leetcode.cn/problems/find-players-with-zero-or-one-losses/solutions/2604661/2225-zhao-chu-shu-diao-ling-chang-huo-yi-4zaf/
        /// 目前tryadd無法使用
        /// 還要想看怎麼改寫
        /// 
        /// Hash 不會儲存重複資料, 一筆資料只會有一次
        /// </summary>
        /// <param name="matches">matches[i] = [winner_i, loser_i] </param>
        /// <returns></returns>

        public static IList<IList<int>> FindWinners(int[][] matches)
        {
            // 贏家, 沒輸過
            HashSet<int> nolosers = new HashSet<int>();
            // 輸家, 只輸一次
            HashSet<int> onelosers = new HashSet<int>();
            // 輸家, 輸很多次
            HashSet<int> manylosers = new HashSet<int>();

            foreach (int[] match in matches) 
            {
                int winner = match[0];
                int loser = match[1];

                // 贏家紀錄, 不能有輸過的紀錄 
                if (!onelosers.Contains(winner) && !manylosers.Contains(winner))
                {
                    nolosers.Add(winner);
                }

                // 輸家, 輸過就算非贏家
                if(nolosers.Contains(loser))
                {
                    // 輸了就要移出贏家紀錄
                    nolosers.Remove(loser);
                    // 加入 輸家的紀錄
                    onelosers.Add(loser);
                }
                else if(onelosers.Contains(loser))
                {
                    // 輸超過一次就要歸類為輸很多次, 且移出輸一次的紀錄
                    onelosers.Remove(loser);
                    manylosers.Add(loser);
                }
                else if(manylosers.Contains(loser))
                {
                    // 輸很多次就繼續歸類為同一類紀錄
                    continue;
                }
                else
                {
                    onelosers.Add(loser);
                }

            }

            // 輸出規格, 
            int[][] result = new int[2][];
            result[0] = nolosers.OrderBy(x => x).ToArray();
            result[1] = onelosers.OrderBy(x => x).ToArray();

            // 輸出顯示 不規則陣列
            for (int i = 0; i < result.Length; i++)
            {
                Console.Write("Element({0}): ", i);

                for (int j = 0; j < result[i].Length; j++)
                {
                    Console.Write("{0}{1}", result[i][j], j == (result[i].Length - 1) ? "" : " ");
                }

                Console.WriteLine();
            }

            return result;

        }
    }
}
