namespace leetcode_1105
{
    internal class Program
    {
        /// <summary>
        /// 1105. Filling Bookcase Shelves
        /// https://leetcode.com/problems/filling-bookcase-shelves/description/?envType=daily-question&envId=2024-07-31
        /// 
        /// 1105. 填充书架
        /// https://leetcode.cn/problems/filling-bookcase-shelves/description/
        /// 
        /// 將書本依據順序放入書架
        /// 看最多需要幾層
        /// 
        /// 書籍厚度之和 小於等於 書架寬度
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 1, 1 },
                 new int[]{ 2, 3 },
                 new int[]{ 2, 3 },
                 new int[]{ 1, 1 },
                 new int[]{ 1, 1 },
                 new int[]{ 1, 1 },
                 new int[]{ 1, 2 }
            };

            int shelfWidth = 4;

            Console.WriteLine(MinHeightShelves(input, shelfWidth));

            Console.ReadKey();
        }



        /// <summary>
        /// ref: DP 動態規劃
        /// https://leetcode.cn/problems/filling-bookcase-shelves/solutions/2239727/tian-chong-shu-jia-by-leetcode-solution-b7py/
        /// 這個聯結有圖示說明, 比較好理解
        /// https://leetcode.cn/problems/filling-bookcase-shelves/solutions/11480/dong-tai-gui-hua-python3-by-smoon1989/    
        /// https://leetcode.cn/problems/filling-bookcase-shelves/solutions/11508/1105-tian-chong-shu-jia-dong-tai-gui-hua-by-ivan_a/
        /// https://leetcode.cn/problems/filling-bookcase-shelves/solutions/2240688/jiao-ni-yi-bu-bu-si-kao-dong-tai-gui-hua-0vg6/
        /// 
        /// 書架放置方法是由上往下放
        /// 
        /// 
        /// 當我們要放置前 i 本書的時候, 假定前 j 本書放在書架上, 其中 j < i, 前 j 本書放好後
        /// 剩餘的 i - j 本書放在最後一層書架上, 這一層書架的高度就是這部份書的高度最大值
        /// 由此推出 dp[i] = Min(dp[j], max(books[k]))
        /// k 表示遍歷最後這一層的 i - j 本書
        /// 從而找到這一層最大值
        /// 
        /// 每一層高度都是由最高的那本書決定
        /// 所以盡量要把高度大的放一起
        /// 比較能縮小高度
        /// 但是書本要依據順序放, 所以要考慮下一層
        /// 同時需要注意寬度
        /// 想將大量書籍放在同一層
        /// 必須在寬度範圍內才可以
        /// 
        /// 這邊 array順序是從 1 開始不是 0
        /// 
        /// 倒敘枚舉,是為了取出 前 i 本書的高度
        /// 直到因書架寬度限制不能擺放地 i 本書的擺放
        /// 到同一層的那一本
        /// </summary>
        /// <param name="books">書籍資料 [thickness_i, height_i] 厚度(寬度), 高度</param>
        /// <param name="shelfWidth">書架總寬度</param>
        /// <returns></returns>
        public static int MinHeightShelves(int[][] books, int shelfWidth)
        {
            int n = books.Length;

            // 編號從 1 開始, 故 n + 1 => 第 n 本書
            int[] dp = new int[n + 1];
            
            // 初始化給予最大值, 共 1000 本書 * 每本書最大高度 1000
            Array.Fill(dp, 1000000);

            // 0 位置 還沒放書, 高度為 0
            dp[0] = 0;

            for(int i = 0; i < n; i++)
            {
                // 高度, 寬度
                int maxheight = 0, curwidth = 0;

                // 倒序枚舉, 計算出各種擺放方式的高度
                // 得出最小高度
                for(int j = i; j >= 0; j--)
                {
                    curwidth += books[j][0];
                    if(curwidth > shelfWidth)
                    {
                        // 空間不足, 無法放書
                        break;
                    }

                    // 更新 j 到 i 的最大高度
                    maxheight = Math.Max(maxheight, books[j][1]);

                    // dp[i] 的高度 與 前 j 個高度 + 當前高度 取出最小值
                    dp[i + 1] = Math.Min(dp[i + 1], dp[j] + maxheight);
                }
            }

            return dp[n];

        }
    }
}
