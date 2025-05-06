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
        /// <summary>
        /// LeetCode 57: 插入區間問題的解題入口
        /// </summary>
        /// <param name="args">命令列參數</param>
        static void Main(string[] args)
        {
            // 建立測試用的區間陣列，依照左邊界由小到大排序
            int[][] input = new int[][]
            {
                 new int[]{ 1, 2 },  // 第一個區間 [1,2]
                 new int[]{ 3, 5 },  // 第二個區間 [3,5]
                 new int[]{ 6, 7 },  // 第三個區間 [6,7]
                 new int[]{ 8, 10 }, // 第四個區間 [8,10]
                 new int[]{ 12, 16 } // 第五個區間 [12,16]
            };

            // 要插入的新區間 [4,8]
            int[] newInterval = {4, 8 };

            // 呼叫插入區間的方法，取得結果
            var res = Insert(input, newInterval);

            // 輸出結果：將二維陣列的每個元素按格式印出
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
        /// 插入區間演算法實作
        /// 
        /// 此演算法將一個新的區間插入到已排序且不重疊的區間陣列中
        /// 
        /// 參考資料:
        /// https://leetcode.cn/problems/insert-interval/solutions/472151/cha-ru-qu-jian-by-leetcode-solution/
        /// https://leetcode.cn/problems/insert-interval/solutions/1530702/by-stormsunshine-4xkf/
        /// 
        /// 特性說明：
        /// merged 基本上只會有一次標記，因為輸入的 intervals 區間已經按左邊界遞增排序且不重疊
        /// 當遇到多個區間需要合併的情況，如範例中 newInterval 為 [4,8] 時
        /// 它會先與 [3,5] 合併為 [3,8]，再與 [6,7] 合併，最後維持 [3,8]
        /// 然後再進入 if (!merged) 判斷式，一次性加入 ansList
        /// </summary>
        /// <param name="intervals">原始已排序的不重疊區間陣列</param>
        /// <param name="newInterval">要插入的新區間</param>
        /// <returns>插入新區間後的結果陣列</returns>
        public static int[][] Insert(int[][] intervals, int[] newInterval)
        {
            // 從新區間取得左右邊界值作為初始合併區間
            int left = newInterval[0];
            // 插入區間右邊界
            int right = newInterval[1];
            // 標記是否已經將合併後的區間加入答案列表
            bool merged = false;
            // 存儲最終結果的動態陣列
            List<int[]> ansList = new List<int[]>();

            // 遍歷原始已排序的區間陣列
            foreach (int[] interval in intervals)
            {
                if (interval[0] > right)
                {
                    // 情況1: 當前區間在插入區間的右側且無交集
                    // 例如: 當前區間 [12,16], 插入區間 [4,8]
                    // 當前區間比插入區間還要大, 所以在右邊
                    if (!merged)
                    {
                        // 如果還沒將合併區間加入答案，先加入合併後的區間
                        ansList.Add(new int[] { left, right});
                        // 標記已合併，避免重複加入
                        merged = true;
                    }
                    // 將當前區間直接加入答案
                    ansList.Add(interval);
                }
                else if (interval[1] < left)
                {
                    // 情況2: 當前區間在插入區間的左側且無交集
                    // 例如: 當前區間 [1,2], 插入區間 [4,8]
                    // 當前區間比插入區間還要小, 所以在左邊
                    // 直接將當前區間加入答案
                    ansList.Add(interval);
                }
                else
                {
                    // 情況3: 當前區間與插入區間有交集，需要合併
                    // 例如: 當前區間 [3,5] 或 [6,7]，插入區間 [4,8]
                    // 更新合併後區間的左右邊界
                    left = Math.Min(left, interval[0]);  // 取兩個區間左邊界的較小值
                    right = Math.Max(right, interval[1]); // 取兩個區間右邊界的較大值
                    // 注意：這裡不將合併結果立即加入答案，因為可能還需要與後續區間合併
                }
            }

            // 如果遍歷完所有區間後仍未加入合併區間，在此處加入
            if (!merged)
            {
                ansList.Add(new int[] { left, right });
            }

            // 將動態列表轉換為二維陣列格式返回 
            int[][] ans = new int[ansList.Count][];
            for(int i = 0; i < ansList.Count; i++)
            {
                ans[i] = ansList[i];
            }

            return ans;
        }
    }
}
