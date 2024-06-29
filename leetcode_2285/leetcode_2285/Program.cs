namespace leetcode_2285
{
    internal class Program
    {
        /// <summary>
        /// 2285. Maximum Total Importance of Roads
        /// https://leetcode.com/problems/maximum-total-importance-of-roads/description/?envType=daily-question&envId=2024-06-28
        /// 2285. 道路的最大总重要性
        /// https://leetcode.cn/problems/maximum-total-importance-of-roads/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 0, 1 },
                 new int[]{ 1, 2 },
                 new int[]{ 2, 3 },
                 new int[]{ 0, 2 },
                 new int[]{ 1, 3 },
                 new int[]{ 2, 4 }
            };

            Console.WriteLine(MaximumImportance(5, input));

        }



        /// <summary>
        /// https://leetcode.cn/problems/maximum-total-importance-of-roads/solutions/1523886/by-endlesscheng-9p6y/
        /// https://leetcode.cn/problems/maximum-total-importance-of-roads/solutions/2636473/2285-dao-lu-de-zui-da-zong-zhong-yao-xin-p3zl/
        /// https://leetcode.cn/problems/maximum-total-importance-of-roads/solutions/1523888/by-shou-hu-zhe-t-li9y/
        /// 
        /// 
        /// 每個城市對外的邊(道路)越多條, 代表越重要
        /// 所以先計算每個城市有多少條邊
        /// 然後 排序 小至大
        /// 越多條道路者, 重要性越高
        /// 結果計算方式:
        /// 累加 (每個城市幾條道路(邊) * 城市重要性數值)
        /// 最後累計結果
        /// </summary>
        /// <param name="n"></param>
        /// <param name="roads"></param>
        /// <returns></returns>
        public static long MaximumImportance(int n, int[][] roads)
        {
            // 統計 每個城市有多少條道路(邊)
            long[] CitiesRoad = new long[n];

            for (int i = 0; i < roads.Length; i++)
            {
                CitiesRoad[roads[i][0]]++;
                CitiesRoad[roads[i][1]]++;
            }

            // 排序 小至大
            Array.Sort(CitiesRoad);

            long sum = 0;
            for(int i = 0; i < n; i++)
            {
                // 城市重要性數值是從1開始, 故 i + 1
                // 每個城市幾條道路(邊) * 城市重要性數值
                sum += CitiesRoad[i] * (i + 1);
            }

            return sum;

        }
    }
}
