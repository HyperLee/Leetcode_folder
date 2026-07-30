namespace leetcode_973
{
    internal class Program
    {
        /// <summary>
        /// 973. K Closest Points to Origin
        /// https://leetcode.com/problems/k-closest-points-to-origin/description/
        /// 
        /// 973. 最接近原点的 K 个点
        /// https://leetcode.cn/problems/k-closest-points-to-origin/description/
        /// 
        /// 題目:
        /// 給定一個點的陣列，其中 points[i] = [xi, yi] 表示 X-Y 平面上的一個點，以及一個整數 k，返回距離原點 (0, 0) 最近的 k 個點。
        /// 在 X-Y 平面上兩點之間的距離是歐幾里得距離  (i.e., √(x1 - x2)^2 + (y1 - y2)^2).
        /// 你可以以任意順序返回答案。答案保證是唯一的（除了順序之外）。
        /// </summary>
        /// <remarks>
        /// 不需要命令列參數；主程式會執行七組合法測試案例，並以不考慮回傳順序、
        /// 但保留重複座標次數的方式比對預期與實際結果，最後輸出通過數。
        /// </remarks>
        /// <param name="args">未使用的命令列參數。</param>
        static void Main(string[] args)
        {
            SampleCase[] cases =
            [
                new("官方範例一", [[1, 3], [-2, 2]], 1, [[-2, 2]]),
                new("官方範例二", [[3, 3], [5, -1], [-2, 4]], 2, [[3, 3], [-2, 4]]),
                new("單一原點", [[0, 0]], 1, [[0, 0]]),
                new("取出全部座標", [[1, 0], [0, 1], [-1, 0]], 3, [[1, 0], [0, 1], [-1, 0]]),
                new("正負座標混合", [[-5, 4], [-6, -5], [4, 6]], 1, [[-5, 4]]),
                new(
                    "座標上下界",
                    [[10000, 10000], [-10000, 9999], [1, -1]],
                    2,
                    [[1, -1], [-10000, 9999]]),
                new("重複座標", [[1, 1], [1, 1], [3, 3]], 2, [[1, 1], [1, 1]])
            ];

            int passedChecks = 0;

            Console.WriteLine("LeetCode 973：最接近原點的 K 個點");
            Console.WriteLine();

            for (int i = 0; i < cases.Length; i++)
            {
                SampleCase sample = cases[i];
                int[][] input = ClonePoints(sample.Points);
                int[][] actual = KClosest(input, sample.K);
                bool passed = actual.Length == sample.Expected.Length
                    && HaveSamePoints(sample.Expected, actual);

                if (passed)
                {
                    passedChecks++;
                }

                Console.WriteLine($"案例 {i + 1}：{sample.Name}");
                Console.WriteLine($"輸入：points = {FormatPoints(sample.Points)}, k = {sample.K}");
                Console.WriteLine($"預期：{FormatPoints(sample.Expected)}");
                Console.WriteLine($"實際：{FormatPoints(actual)}");
                Console.WriteLine($"結果：{(passed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedChecks}/{cases.Length} 項驗證通過");
        }

        /// <summary>
        /// 依每個座標到原點的平方歐幾里得距離由小到大排序，再取出前 <paramref name="k"/> 個點。
        /// 輸入須符合題目條件：<c>1 &lt;= k &lt;= points.Length &lt;= 10^4</c>，
        /// 且每個座標都包含兩個介於 <c>-10^4</c> 到 <c>10^4</c> 的整數。
        /// 平方距離與實際距離的大小關係相同，因此不必開根號，也不會引入浮點誤差。
        /// </summary>
        /// <remarks>
        /// 此方法會重新排列 <paramref name="points"/> 的外層陣列。時間複雜度為
        /// O(n log n)，排序的額外空間為 O(log n)，回傳陣列占用 O(k) 空間。
        /// 回傳陣列中的每個元素仍參考原本的座標陣列。
        /// </remarks>
        /// <param name="points">符合題目限制的二維座標陣列。</param>
        /// <param name="k">要回傳的最近座標數量。</param>
        /// <returns>排序後距離原點最近的前 <paramref name="k"/> 個座標；座標順序可任意。</returns>
        public static int[][] KClosest(int[][] points, int k)
        {
            // 平方函式在非負範圍單調遞增，直接比較 x² + y² 即可省略開根號。
            Array.Sort(
                points,
                (first, second) => SquaredDistance(first).CompareTo(SquaredDistance(second)));

            int[][] closest = new int[k][];
            Array.Copy(points, 0, closest, 0, k);
            return closest;
        }

        /// <summary>
        /// 計算單一二維座標到原點的平方歐幾里得距離。
        /// 輸入須包含合法的 X、Y 座標；回傳 <c>x² + y²</c>，供排序時比較距離大小。
        /// 在題目座標限制內，結果不會超過 <see cref="int.MaxValue"/>。
        /// </summary>
        /// <param name="point">包含 X、Y 兩個整數的座標。</param>
        /// <returns>座標到原點的平方距離。</returns>
        private static int SquaredDistance(int[] point)
        {
            return point[0] * point[0] + point[1] * point[1];
        }

        /// <summary>
        /// 深層複製二維座標陣列，讓會原地排序的解法不會改動測試案例本身。
        /// 輸入為合法座標陣列；回傳具有獨立外層陣列及獨立座標陣列的副本。
        /// </summary>
        /// <param name="points">要複製的二維座標陣列。</param>
        /// <returns>內容相同但不共享任何陣列實例的深層副本。</returns>
        private static int[][] ClonePoints(int[][] points)
        {
            int[][] clone = new int[points.Length][];

            for (int i = 0; i < points.Length; i++)
            {
                // 內層也必須複製，避免未來的原地座標操作污染固定測資。
                clone[i] = [.. points[i]];
            }

            return clone;
        }

        /// <summary>
        /// 比較兩個座標陣列是否包含相同的座標多重集合。
        /// 輸入中的每個元素須為二維座標；比較忽略排列順序，但相同座標的出現次數必須一致。
        /// 回傳兩者是否代表同一份答案，且不會修改任一輸入。
        /// </summary>
        /// <param name="expected">預期的座標陣列。</param>
        /// <param name="actual">實際的座標陣列。</param>
        /// <returns>長度、座標值及各座標出現次數都相同時回傳 true，否則回傳 false。</returns>
        private static bool HaveSamePoints(int[][] expected, int[][] actual)
        {
            if (expected.Length != actual.Length)
            {
                return false;
            }

            Dictionary<(int X, int Y), int> remainingCounts = new();

            foreach (int[] point in expected)
            {
                (int X, int Y) coordinate = (point[0], point[1]);
                remainingCounts[coordinate] = remainingCounts.GetValueOrDefault(coordinate) + 1;
            }

            foreach (int[] point in actual)
            {
                (int X, int Y) coordinate = (point[0], point[1]);

                // 用計數而非單純 HashSet，才能正確辨識重複座標是否少一個或多一個。
                if (!remainingCounts.TryGetValue(coordinate, out int count))
                {
                    return false;
                }

                if (count == 1)
                {
                    remainingCounts.Remove(coordinate);
                }
                else
                {
                    remainingCounts[coordinate] = count - 1;
                }
            }

            return remainingCounts.Count == 0;
        }

        /// <summary>
        /// 將二維座標陣列轉換為適合 console 與 README 閱讀的巢狀方括號格式。
        /// 輸入可為空陣列；回傳例如 <c>[[1, 3], [-2, 2]]</c> 的字串，
        /// 並保留輸入座標目前的排列順序。
        /// </summary>
        /// <param name="points">要格式化的二維座標陣列。</param>
        /// <returns>以逗號及空格分隔的巢狀方括號字串。</returns>
        private static string FormatPoints(int[][] points)
        {
            return $"[{string.Join(", ", points.Select(point => $"[{point[0]}, {point[1]}]"))}]";
        }

        /// <summary>
        /// 表示一筆固定測試案例。
        /// 輸入包含案例名稱、合法座標、k 與預期座標；供主程式逐案執行並輸出驗證結果。
        /// </summary>
        /// <param name="Name">案例顯示名稱。</param>
        /// <param name="Points">符合題目限制的輸入座標。</param>
        /// <param name="K">要選取的最近座標數量。</param>
        /// <param name="Expected">預期回傳的座標多重集合。</param>
        private sealed record SampleCase(string Name, int[][] Points, int K, int[][] Expected);
    }
}