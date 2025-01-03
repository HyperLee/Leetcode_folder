namespace leetcode_057
{
    internal class Program
    {
        /// <summary>
        /// 57. Insert Interval
        /// https://leetcode.com/problems/insert-interval/description/
        /// 
        /// 57. 插入区间
        /// https://leetcode.cn/problems/insert-interval/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 1, 2 },
                 new int[]{ 3, 5 },
                 new int[]{ 6, 7 },
                 new int[]{ 8, 10 },
                 new int[]{ 12, 16 }
            };

            int[] newInterval = {4, 8 };

            var res = Insert(input, newInterval);

            // 不規則陣列輸出
            for (int i = 0; i < res.Length; i++)
            {
                Console.Write("Element({0}): ", i);

                for (int j = 0; j < res[i].Length; j++)
                {
                    Console.Write("{0}{1}", res[i][j], j == (res[i].Length - 1) ? "" : ", ");
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/insert-interval/solutions/472151/cha-ru-qu-jian-by-leetcode-solution/
        /// https://leetcode.cn/problems/insert-interval/solutions/1530702/by-stormsunshine-4xkf/
        /// 
        /// merged 基本上只會有一次
        /// 因為輸入的 intervals 區間 左區間有依據大小 遞增排序過
        /// 且不會有重疊部分
        /// 當遇到 如範例2 這種測試
        /// 他會連續好幾個區間都要合併,
        /// 最後再進入  if (!merged) 判斷式
        /// 一次性加入 ansList
        /// 
        /// </summary>
        /// <param name="intervals"></param>
        /// <param name="newInterval"></param>
        /// <returns></returns>
        public static int[][] Insert(int[][] intervals, int[] newInterval)
        {
            // 插入區間左邊界
            int left = newInterval[0];
            // 插入區間右邊界
            int right = newInterval[1];
            // 是否已經 merge 過
            bool merged = false;
            // 答案列表
            List<int[]> ansList = new List<int[]>();

            foreach (int[] interval in intervals)
            {
                if (interval[0] > right)
                {
                    // 當前區間 在插入區間(newInterval)的右側且無交集
                    if (!merged)
                    {
                        // 將合併過的區間加入 ansList
                        ansList.Add(new int[] { left, right});
                        merged = true;
                    }
                    ansList.Add(interval);
                }
                else if (interval[1] < left)
                {
                    // 當前區間 在插入區間(newInterval)的左側且無交集
                    ansList.Add(interval);
                }
                else
                {
                    // 當前區間 與插入區間(newInterval)有交集, 計算他們的合併部分
                    // 左區間找小的, 右區間找大的. 這樣就可以把區間合併成一個新的重疊區間
                    left = Math.Min(left, interval[0]);
                    right = Math.Max(right, interval[1]);
                }
            }

            if (!merged)
            {
                ansList.Add(new int[] { left, right });
            }

            // 轉成題目要求輸出格式 
            int[][] ans = new int[ansList.Count][];
            for(int i = 0; i < ansList.Count; i++)
            {
                ans[i] = ansList[i];
            }

            return ans;
        }
    }
}
