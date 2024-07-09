namespace leetcode_1701
{
    internal class Program
    {
        /// <summary>
        /// 1701. Average Waiting Time
        /// https://leetcode.com/problems/average-waiting-time/?envType=daily-question&envId=2024-07-09
        /// 1701. 平均等待时间
        /// https://leetcode.cn/problems/average-waiting-time/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 1, 2 },
                 new int[]{ 2, 5 },
                 new int[]{ 4, 3 }
            };

            Console.WriteLine(AverageWaitingTime(input));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/average-waiting-time/solutions/2579810/1701-ping-jun-deng-dai-shi-jian-by-storm-bl74/
        /// https://leetcode.cn/problems/average-waiting-time/solutions/536627/average-waiting-time-by-ikaruga-3iej/
        /// 
        /// 題目有說餐廳只有一位廚師
        /// 做菜順序依據陣列的先後
        /// 不能插隊, 依序做菜
        /// 所以每一梯次客人實際開始時間
        /// 都是上一任結束時間之後才開始
        /// 等客人吃到菜餚時間為
        /// 開始時間 + 做菜需要耗時時間
        /// 
        /// 開始時間需要小心, 要從 上一任客人結束時間 與 到達時間 
        /// 去取出 最大的那一個才是真的開始時間
        /// 
        /// 每一梯次真正的等待時間為
        /// 結束時間 - 開始時間
        /// 
        /// 最終整間餐廳平均等待時間
        /// 就把總共等待耗時累積加總 / 客人數量 即可
        /// 
        /// 可以先看題目範例說明
        /// 輸入陣列為[到達時間, 等待時間]
        /// 第一輪客人可以直接用到達時間 直接開始計算
        /// 但是 第二梯次客人, 就需要改用上一任客人結束時間與到達時間 去取出最大值
        /// 來當作 實際上 開始時間
        /// 
        /// 每一梯次所需要的等待時間為: 結束時間 - 到達時間
        /// 這樣才是真的等待時間
        /// 
        /// 最後 取全部的平均等待時間: 總共等待時間 / 輸入陣列人數
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        public static double AverageWaitingTime(int[][] customers)
        {
            // 總共等待時間
            double totaltime = 0;
            // 上一個客人結束時間
            int preend = 0;

            foreach (int[] customer in customers)
            {
                // 到達時間與做菜需要時間
                int arrival = customer[0], time = customer[1];
                
                // 每一批客人結束時間: (到達時間/上一個結束時間) + time(本身需要時間) or 可以理解為 開始時間 + 運行時間
                int end = Math.Max(arrival, preend) + time;
                
                // 累積計算每批客人實際等待時間: 結束時間 - 到達時間
                totaltime += end - arrival;

                // 更新上一個客人結束時間
                preend = end;
            }

            return totaltime / customers.Length;
        }
    }
}
