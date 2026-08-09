namespace leetcode_1105
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1105. Filling Bookcase Shelves
        /// https://leetcode.com/problems/filling-bookcase-shelves/description/
        ///
        /// You are given an array books where books[i] = [thickness_i, height_i] indicates the thickness and
        /// height of the i-th book. You are also given an integer shelfWidth.
        /// We want to place these books in order onto bookcase shelves that have a total width shelfWidth.
        /// We choose some of the books to place on this shelf such that the sum of their thickness is less than
        /// or equal to shelfWidth, then build another level of the shelf of the bookcase so that the total height
        /// of the bookcase has increased by the maximum height of the books we just put down. We repeat this
        /// process until there are no more books to place.
        /// Note that at each step of the above process, the order of the books we place is the same order as the
        /// given sequence of books.
        /// - For example, if we have an ordered list of 5 books, we might place the first and second book onto
        /// the first shelf, the third book on the second shelf, and the fourth and fifth book on the last shelf.
        /// Return the minimum possible height that the total bookshelf can be after placing shelves in this manner.
        ///
        /// Example 1:
        /// Input: books = [[1,1],[2,3],[2,3],[1,1],[1,1],[1,1],[1,2]], shelfWidth = 4
        /// Output: 6
        /// Illustration: https://assets.leetcode.com/uploads/2019/06/24/shelves.png
        /// Explanation: The sum of the heights of the 3 shelves is 1 + 3 + 2 = 6.
        /// Notice that book number 2 does not have to be on the first shelf.
        ///
        /// Example 2:
        /// Input: books = [[1,3],[2,4],[3,2]], shelfWidth = 6
        /// Output: 4
        ///
        /// Constraints:
        /// 1 &lt;= books.length &lt;= 1000
        /// 1 &lt;= thickness_i &lt;= shelfWidth &lt;= 1000
        /// 1 &lt;= height_i &lt;= 1000
        /// </para>
        /// <para>
        /// 1105. 填滿書架
        /// https://leetcode.cn/problems/filling-bookcase-shelves/description/
        ///
        /// 給定陣列 books，其中 books[i] = [thickness_i, height_i] 表示第 i 本書的厚度與高度。
        /// 另外給定整數 shelfWidth。
        /// 我們要依序將這些書放到總寬度為 shelfWidth 的書架層板上。
        /// 選擇若干本書放在目前層板上，使其厚度總和小於或等於 shelfWidth；接著建立書架的下一層，
        /// 此時書架總高度會增加剛放置之書籍的最大高度。重複此過程，直到沒有書可放為止。
        /// 請注意，在上述過程的每一步中，書籍的放置順序都必須與給定的書籍序列相同。
        /// - 例如，若有依序排列的 5 本書，可以將第 1、2 本放在第一層，第 3 本放在第二層，
        /// 第 4、5 本放在最後一層。
        /// 請回傳依此方式放置所有層板後，整個書架可能達到的最小高度。
        ///
        /// 範例 1：
        /// 輸入：books = [[1,1],[2,3],[2,3],[1,1],[1,1],[1,1],[1,2]], shelfWidth = 4
        /// 輸出：6
        /// 示意圖：https://assets.leetcode.com/uploads/2019/06/24/shelves.png
        /// 解釋：3 層書架的高度總和為 1 + 3 + 2 = 6。
        /// 請注意，第 2 本書不一定要放在第一層。
        ///
        /// 範例 2：
        /// 輸入：books = [[1,3],[2,4],[3,2]], shelfWidth = 6
        /// 輸出：4
        ///
        /// 限制條件：
        /// 1 &lt;= books.length &lt;= 1000
        /// 1 &lt;= thickness_i &lt;= shelfWidth &lt;= 1000
        /// 1 &lt;= height_i &lt;= 1000
        /// </para>
        /// </summary>
        /// <param name="args">未使用；程式執行內建的固定測試資料。</param>
        /// <remarks>
        /// 輸出每個案例的預期值、三種解法的實際值與 PASS/FAIL；全部驗證通過時設定結束碼為 0。
        /// </remarks>
        static void Main(string[] args)
        {
            Environment.ExitCode = RunSamples() ? 0 : 1;
        }

        /// <summary>
        /// 執行固定書本案例，逐一驗證三種計算書架最小高度的解法。
        /// 此方法不接受輸入；輸出每個案例的書本、書架寬度、預期值、實際值與通過狀態，
        /// 並回傳所有答案與輸入不變檢查是否全部通過。
        /// </summary>
        /// <returns>全部檢查通過時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
        private static bool RunSamples()
        {
            (string Name, int[][] Books, int ShelfWidth, int Expected, string Display, bool RunBruteForce)[] cases =
            [
                ("官方範例一", [[1, 1], [2, 3], [2, 3], [1, 1], [1, 1], [1, 1], [1, 2]], 4, 6, "[[1,1],[2,3],[2,3],[1,1],[1,1],[1,1],[1,2]]", true),
                ("官方範例二", [[1, 3], [2, 4], [3, 2]], 6, 4, "[[1,3],[2,4],[3,2]]", true),
                ("單本書邊界", [[1, 7]], 1, 7, "[[1,7]]", true),
                ("重複書本且全放同層", [[1, 2], [1, 2], [1, 2]], 3, 2, "[[1,2],[1,2],[1,2]]", true),
                ("重複書本且必須各自分層", [[2, 3], [2, 3], [2, 3]], 2, 9, "[[2,3],[2,3],[2,3]]", true),
                ("分層取捨", [[1, 3], [2, 4], [2, 2]], 3, 6, "[[1,3],[2,4],[2,2]]", true),
                ("1000 本書上界", CreateUpperBoundBooks(), 1000, 1000, "1000 本書，厚度皆為 1，高度為 1..1000", false)
            ];

            int passedChecks = 0;
            int totalChecks = 0;

            foreach ((string name, int[][] books, int shelfWidth, int expected, string display, bool runBruteForce) in cases)
            {
                (int passed, int total) = RunTestCase(name, books, shelfWidth, expected, display, runBruteForce);
                passedChecks += passed;
                totalChecks += total;
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過。");
            return passedChecks == totalChecks;
        }

        /// <summary>
        /// 執行一組書本案例並比較三種解法，同時確認每種解法都不修改輸入。
        /// 輸入須符合題目限制；輸出各解法的答案與通過狀態，並回傳通過數與執行數。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="books">依原始順序排列的書本，每本書為「厚度、高度」。</param>
        /// <param name="shelfWidth">每層書架可使用的最大寬度。</param>
        /// <param name="expected">案例的最小總高度。</param>
        /// <param name="display">適合輸出至主控台的書本描述。</param>
        /// <param name="runBruteForce">是否執行指數級暴力搜尋。</param>
        /// <returns>本案例的通過檢查數與實際執行檢查數。</returns>
        private static (int Passed, int Total) RunTestCase(
            string name,
            int[][] books,
            int shelfWidth,
            int expected,
            string display,
            bool runBruteForce)
        {
            int[][] input1 = CloneBooks(books);
            int[][] input2 = CloneBooks(books);
            int actual1 = MinHeightShelves(input1, shelfWidth);
            int actual2 = MinHeightShelves2(input2, shelfWidth);
            bool passed1 = actual1 == expected && HaveSameBooks(books, input1);
            bool passed2 = actual2 == expected && HaveSameBooks(books, input2);

            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"輸入：books = {display}");
            Console.WriteLine($"書架寬度：{shelfWidth}");
            Console.WriteLine($"預期：{expected}");
            Console.WriteLine($"解法一（Bottom-up DP）實際：{actual1} => {(passed1 ? "PASS" : "FAIL")}");
            Console.WriteLine($"解法二（Top-down DP）實際：{actual2} => {(passed2 ? "PASS" : "FAIL")}");

            int passedChecks = (passed1 ? 1 : 0) + (passed2 ? 1 : 0);
            int totalChecks = 2;

            if (runBruteForce)
            {
                int[][] input3 = CloneBooks(books);
                int actual3 = MinHeightShelves3(input3, shelfWidth);
                bool passed3 = actual3 == expected && HaveSameBooks(books, input3);
                Console.WriteLine($"解法三（暴力搜尋）實際：{actual3} => {(passed3 ? "PASS" : "FAIL")}");
                passedChecks += passed3 ? 1 : 0;
                totalChecks++;
            }
            else
            {
                Console.WriteLine("解法三（暴力搜尋）：SKIP（指數級複雜度，不執行 1000 本書案例）");
            }

            Console.WriteLine();
            return (passedChecks, totalChecks);
        }

        /// <summary>
        /// 建立題目數量上界的 1000 本書，每本厚度為 1，高度依序為 1 到 1000。
        /// 此資料在寬度 1000 的書架上可全部放在同一層，輸出用於驗證兩種多項式時間 DP。
        /// </summary>
        /// <returns>符合題目上界且預期最小總高度為 1000 的書本陣列。</returns>
        private static int[][] CreateUpperBoundBooks()
        {
            const int bookCount = 1000;
            int[][] books = new int[bookCount][];

            for (int index = 0; index < bookCount; index++)
            {
                books[index] = [1, index + 1];
            }

            return books;
        }

        /// <summary>
        /// 深層複製書本資料，使每種解法取得互不共用內層陣列的輸入。
        /// 輸入須為符合題目格式的非空書本陣列；輸出內容相同但可獨立修改的複本。
        /// </summary>
        /// <param name="books">要複製的書本資料。</param>
        /// <returns>與輸入具有相同厚度與高度的新陣列。</returns>
        private static int[][] CloneBooks(int[][] books)
        {
            int[][] clone = new int[books.Length][];

            for (int index = 0; index < books.Length; index++)
            {
                clone[index] = (int[])books[index].Clone();
            }

            return clone;
        }

        /// <summary>
        /// 比較兩份書本資料的長度、每本書的欄位數與所有數值是否完全相同。
        /// 輸入須為已初始化的書本陣列；輸出用於確認解法執行後仍保留原始輸入內容。
        /// </summary>
        /// <param name="first">比較基準資料。</param>
        /// <param name="second">要與基準比較的資料。</param>
        /// <returns>兩份資料的結構與所有數值皆相同時為 <see langword="true"/>。</returns>
        private static bool HaveSameBooks(int[][] first, int[][] second)
        {
            if (first.Length != second.Length)
            {
                return false;
            }

            for (int index = 0; index < first.Length; index++)
            {
                if (!first[index].AsSpan().SequenceEqual(second[index]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 使用 bottom-up 動態規劃計算依序擺放全部書本後的最小書架總高度。
        /// <c>dp[i]</c> 表示前 <c>i</c> 本書的最佳答案；對每個結尾倒序枚舉最後一層的起點，
        /// 將「起點之前的最佳高度」加上「最後一層最高書本」後取最小值。
        /// </summary>
        /// <param name="books">符合題目限制且依原始順序排列的書本，每本書為「厚度、高度」。</param>
        /// <param name="shelfWidth">每層書架可使用的最大寬度。</param>
        /// <returns>依序放完所有書本可達成的最小書架總高度。</returns>
        /// <remarks>
        /// 時間複雜度為 O(n²)，額外空間複雜度為 O(n)，且不修改輸入資料。
        /// 參考：https://leetcode.cn/problems/filling-bookcase-shelves/solutions/2239727/tian-chong-shu-jia-by-leetcode-solution-b7py/
        /// </remarks>
        public static int MinHeightShelves(int[][] books, int shelfWidth)
        {
            int[] dp = new int[books.Length + 1];
            Array.Fill(dp, int.MaxValue);

            // 沒有書本時不需要任何書架，這是後續狀態轉移的基底。
            dp[0] = 0;

            for (int bookCount = 1; bookCount <= books.Length; bookCount++)
            {
                int currentWidth = 0;
                int currentShelfHeight = 0;

                // 倒序擴張最後一層；一旦超寬，更早的書也不可能放進同一層。
                for (int startIndex = bookCount - 1; startIndex >= 0; startIndex--)
                {
                    currentWidth += books[startIndex][0];
                    if (currentWidth > shelfWidth)
                    {
                        break;
                    }

                    currentShelfHeight = Math.Max(currentShelfHeight, books[startIndex][1]);
                    dp[bookCount] = Math.Min(dp[bookCount], dp[startIndex] + currentShelfHeight);
                }
            }

            return dp[books.Length];
        }

        /// <summary>
        /// 使用 top-down 動態規劃計算依序擺放全部書本後的最小書架總高度。
        /// 從第一本尚未擺放的書開始枚舉目前層的結尾，並以記憶化快取每個起點的最小剩餘高度。
        /// </summary>
        /// <param name="books">符合題目限制且依原始順序排列的書本，每本書為「厚度、高度」。</param>
        /// <param name="shelfWidth">每層書架可使用的最大寬度。</param>
        /// <returns>依序放完所有書本可達成的最小書架總高度。</returns>
        /// <remarks>時間複雜度為 O(n²)，額外空間複雜度為 O(n)，且不修改輸入資料。</remarks>
        public static int MinHeightShelves2(int[][] books, int shelfWidth)
        {
            int[] memo = new int[books.Length];
            return FindMinimumHeight(0, books, shelfWidth, memo);
        }

        /// <summary>
        /// 回傳從指定書本開始擺放的最小剩餘高度，並快取已計算的起點。
        /// 輸入索引範圍為 0 到書本數量；到達陣列尾端時輸出 0，其他狀態輸出最佳分層高度。
        /// </summary>
        /// <param name="startIndex">第一本尚未擺放的書本索引。</param>
        /// <param name="books">符合題目限制的書本資料。</param>
        /// <param name="shelfWidth">每層書架可使用的最大寬度。</param>
        /// <param name="memo">以起點索引保存最小剩餘高度的記憶化陣列。</param>
        /// <returns>從 <paramref name="startIndex"/> 起擺放所有剩餘書本的最小總高度。</returns>
        private static int FindMinimumHeight(int startIndex, int[][] books, int shelfWidth, int[] memo)
        {
            if (startIndex == books.Length)
            {
                return 0;
            }

            if (memo[startIndex] != 0)
            {
                return memo[startIndex];
            }

            int minimumHeight = int.MaxValue;
            int currentWidth = 0;
            int currentShelfHeight = 0;

            for (int endIndex = startIndex; endIndex < books.Length; endIndex++)
            {
                currentWidth += books[endIndex][0];
                if (currentWidth > shelfWidth)
                {
                    break;
                }

                // 固定目前層的書本範圍後，遞迴解決下一本書開始的相同子問題。
                currentShelfHeight = Math.Max(currentShelfHeight, books[endIndex][1]);
                int remainingHeight = FindMinimumHeight(endIndex + 1, books, shelfWidth, memo);
                minimumHeight = Math.Min(minimumHeight, currentShelfHeight + remainingHeight);
            }

            memo[startIndex] = minimumHeight;
            return minimumHeight;
        }

        /// <summary>
        /// 使用不含記憶化的暴力搜尋計算依序擺放全部書本後的最小書架總高度。
        /// 解法枚舉每個合法的連續分層位置，適合用小型輸入展示完整搜尋空間，不適合題目上界。
        /// </summary>
        /// <param name="books">符合題目限制且依原始順序排列的書本，每本書為「厚度、高度」。</param>
        /// <param name="shelfWidth">每層書架可使用的最大寬度。</param>
        /// <returns>依序放完所有書本可達成的最小書架總高度。</returns>
        /// <remarks>時間複雜度為 O(2ⁿ)，遞迴堆疊空間複雜度為 O(n)，且不修改輸入資料。</remarks>
        public static int MinHeightShelves3(int[][] books, int shelfWidth)
        {
            return ExploreMinimumHeight(0, books, shelfWidth);
        }

        /// <summary>
        /// 從指定索引開始暴力枚舉目前層的所有合法結尾，回傳剩餘書本的最小分層高度。
        /// 輸入索引範圍為 0 到書本數量；到達陣列尾端時輸出 0。
        /// </summary>
        /// <param name="startIndex">第一本尚未擺放的書本索引。</param>
        /// <param name="books">符合題目限制的書本資料。</param>
        /// <param name="shelfWidth">每層書架可使用的最大寬度。</param>
        /// <returns>從 <paramref name="startIndex"/> 起擺放所有剩餘書本的最小總高度。</returns>
        private static int ExploreMinimumHeight(int startIndex, int[][] books, int shelfWidth)
        {
            if (startIndex == books.Length)
            {
                return 0;
            }

            int minimumHeight = int.MaxValue;
            int currentWidth = 0;
            int currentShelfHeight = 0;

            for (int endIndex = startIndex; endIndex < books.Length; endIndex++)
            {
                currentWidth += books[endIndex][0];
                if (currentWidth > shelfWidth)
                {
                    break;
                }

                // 每個合法結尾都代表一種分層選擇；此版本刻意不快取重複子問題。
                currentShelfHeight = Math.Max(currentShelfHeight, books[endIndex][1]);
                int remainingHeight = ExploreMinimumHeight(endIndex + 1, books, shelfWidth);
                minimumHeight = Math.Min(minimumHeight, currentShelfHeight + remainingHeight);
            }

            return minimumHeight;
        }
    }
}