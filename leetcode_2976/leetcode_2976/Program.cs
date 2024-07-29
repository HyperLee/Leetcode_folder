namespace leetcode_2976
{
    internal class Program
    {
        /// <summary>
        /// 2976. Minimum Cost to Convert String I
        /// https://leetcode.com/problems/minimum-cost-to-convert-string-i/description/?envType=daily-question&envId=2024-07-27
        /// 
        /// 2976. 转换字符串的最小成本 I
        /// https://leetcode.cn/problems/minimum-cost-to-convert-string-i/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string source = "abcd";
            string target = "acbe";
            char[] original = {'a', 'b', 'c', 'c', 'e', 'd' };
            char[] changed = { 'b', 'c', 'b', 'e', 'b', 'e' };
            int[] cost = { 2, 5, 5, 1, 2, 20 };
            Console.WriteLine(MinimumCost(source, target, original, changed, cost));
            Console.ReadKey();
        }



        /// <summary>
        /// Floyd-Warshall 算法
        /// 
        /// ref:
        /// https://leetcode.cn/problems/minimum-cost-to-convert-string-i/solutions/2577903/floyd-suan-fa-by-endlesscheng-ug8s/
        /// https://leetcode.cn/problems/minimum-cost-to-convert-string-i/solutions/2577890/floyd-qiu-zui-duan-lu-by-tsreaper-1psb/
        /// 
        /// 
        /// int.MaxValue / 2 => 避免相加之後, 算術溢未
        /// 
        /// dis[i][j] 表示字母 i 通過若干次替換操作變成字母 j 的最小成本。
        /// 
        /// 題目要求要將 source 替換成 target
        /// 透過使用 Floyd-Warshall 算法
        /// 建立雙向圖
        /// 將 original 指向 changed 得出花費 cost
        /// 建立之後,透過算法找出最小花費
        /// 
        /// 得出最小花費
        /// 再套回去給
        /// source 替換 target 查詢換算出來
        /// 即可
        /// </summary>
        /// <param name="source">字符串</param>
        /// <param name="target">字符串</param>
        /// <param name="original">char array</param>
        /// <param name="changed">char array</param>
        /// <param name="cost">original 替換成 changed 所需的成本 </param>
        /// <returns></returns>
        public static long MinimumCost(string source, string target, char[] original, char[] changed, int[] cost)
        {
            // original與changed 長度26
            int[][] dis = new int[26][];
            //  雙向圖 初始化
            for(int i = 0; i < 26; i++)
            {
                dis[i] = new int[26];
                // 填入預設數值, 無限大
                Array.Fill(dis[i], int.MaxValue / 2);
            }

            // 雙向圖. 填入 cost 花費資料
            // original向changed 替換所需的 cost
            for (int i = 0; i < cost.Length; i++)
            {
                int x = original[i] - 'a';
                int y = changed[i] - 'a';

                dis[x][y] = Math.Min(dis[x][y], cost[i]);
            }

            // 找出中間點 k, 使得 i 到 j 可以是最短距離
            // 當 (i, j) >= (i, k) + (k, j) 時候
            // 可以更新為 (i, j) = (i, k) + (k, j)
            for (int k = 0; k < 26; k++)
            {
                // 自己與自己 距離 0
                dis[k][k] = 0;
                for (int i = 0; i < 26; i++)
                {
                    for(int j = 0; j < 26; j++)
                    {
                        dis[i][j] = Math.Min(dis[i][j], dis[i][k] + dis[k][j]);
                    }
                }
            }

            // 上方已經換算出每個char替換需要花費, 
            // 這邊來計算答案, 每個位置替換的花費
            long ans = 0;
            for(int i = 0; i < source.Length; i++)
            {
                // 寫法1
                int d = dis[source[i] - 'a'][target[i] - 'a'];
                if (d >= int.MaxValue / 2)
                {
                    // x 無法替換成 y, 回傳 -1
                    // 無限大, 代表沒替換成功
                    return -1;
                }

                ans += d;

                /* // 寫法2
                int x = source[i] - 'a';
                int y = target[i] - 'a';

                // 不同字母替換
                if(x != y)
                {
                    if (dis[x][y] >= int.MaxValue / 2)
                    {
                        // x 無法替換成 y, 回傳 -1
                        // 無限大, 代表沒替換成功
                        return -1;
                    }
                    
                    // 否則答案增加把 x 替換成 y 的最小花費
                    ans += dis[x][y];
                }
                */

            }

            return ans;
        }
    }
}
