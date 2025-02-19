namespace leetcode_056
{
    internal class Program
    {
        /// <summary>
        /// 56. Merge Intervals
        /// https://leetcode.com/problems/merge-intervals/description/
        /// 56. 合并区间
        /// https://leetcode.cn/problems/merge-intervals/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 1, 3 },
                 new int[]{ 2, 6 },
                 new int[]{ 8, 10 },
                 new int[]{ 15, 18 }
            };

            var result = Merge(input);
            foreach (var item in result)
            {
                Console.WriteLine(string.Join(",", item));
            }
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/merge-intervals/solutions/203562/he-bing-qu-jian-by-leetcode-solution/
        /// https://leetcode.cn/problems/merge-intervals/solutions/2798138/jian-dan-zuo-fa-yi-ji-wei-shi-yao-yao-zh-f2b3/
        /// https://leetcode.cn/problems/merge-intervals/solutions/1530698/by-stormsunshine-kvvc/
        /// 
        /// Array.Sort(intervals, (p, q) => p[0].CompareTo(q[0]));
        /// 按照每個子陣列（區間）的第一個元素（起始值）進行升序排序。
        /// 
        /// (p, q) => p[0].CompareTo(q[0])：
        /// 這是一個 Lambda 表達式，用於指定排序的比較邏輯。
        /// p 和 q 是 intervals 陣列中的兩個元素（即兩個一維陣列）。
        /// p[0] 和 q[0] 分別是這兩個一維陣列的第一個元素。
        /// 
        /// p[0].CompareTo(q[0])：
        /// CompareTo 方法用於比較兩個值。
        /// 如果 p[0] 小於 q[0]，則返回負數，表示 p 應排在 q 之前。
        /// 如果 p[0] 等於 q[0]，則返回 0，表示 p 和 q 的順序不變。
        /// 如果 p[0] 大於 q[0]，則返回正數，表示 p 應排在 q 之後。
        /// 
        /// p[0] 代表起點，p[1] 代表終點。
        /// p[0] <= ans[m - 1][1]：如果當前區間的起點 p[0] 小於等於最後一個合併區間的終點 ans[m-1][1]，代表這兩個區間重疊，我們可以進行合併。
        /// 
        /// 時間複雜度：O(n log n)
        /// 空間複雜度：O(n)，需要額外空間存儲結果
        /// </summary>
        /// <param name="intervals"></param>
        /// <returns></returns>
        public static int[][] Merge(int[][] intervals)
        {
            // 先按照区间起始位置排序, 左邊界開始由小至大
            // 這樣可以保證後面的區間的左邊界一定大於等於前面的區間的左邊界
            Array.Sort(intervals, (p, q) => p[0].CompareTo(q[0]));
            // 用來存放合併後的區間
            List<int[]> result = new List<int[]>();

            foreach (var p in intervals)
            {
                // ans.Count 代表合併後的區間數量
                int m = result.Count;
                // 如果当前区间与前一个区间相交; 可以合併
                // 檢查是否有重疊：
                // 如果結果清單不為空且當前區間的起始值小於等於前一個區間的結束值
                if (m > 0 && p[0] <= result[m - 1][1])
                {
                    // 更新前一个区间的右端点为两者中的最大值; 更新右端點最大值
                    // 取這兩者的最大值，確保合併後的終點涵蓋更大的範圍。
                    result[m - 1][1] = Math.Max(result[m - 1][1], p[1]);
                }
                else // 不相交, 無法合併
                {
                    // 否则，将当前区间添加为新的不相交区间; 下面兩個寫法意思相同
                    //ans.Add(new int[] { p[0], p[1] });
                    // 這裡不用 new 一個新的陣列，直接將 p 加入即可; 因為 p 已經是一個新的陣列
                    result.Add(p);
                }
            }

            return result.ToArray();
        }
    }
}
