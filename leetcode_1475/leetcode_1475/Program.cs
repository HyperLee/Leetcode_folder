namespace leetcode_1475
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1475. Final Prices With a Special Discount in a Shop
        /// https://leetcode.com/problems/final-prices-with-a-special-discount-in-a-shop/description/
        ///
        /// You are given an integer array prices where prices[i] is the price of the i-th item in a shop.
        ///
        /// There is a special discount for items in the shop. If you buy the i-th item, then you will receive a discount
        /// equivalent to prices[j], where j is the minimum index such that j &gt; i and prices[j] &lt;= prices[i]. Otherwise,
        /// you will not receive any discount at all.
        ///
        /// Return an integer array answer where answer[i] is the final price you will pay for the i-th item of the shop,
        /// considering the special discount.
        ///
        /// Example 1:
        /// Input: prices = [8,4,6,2,3]
        /// Output: [4,2,4,2,3]
        /// Explanation:
        /// For item 0 with prices[0]=8, prices[1]=4 is the discount, so the final price is 8 - 4 = 4.
        /// For item 1 with prices[1]=4, prices[3]=2 is the discount, so the final price is 4 - 2 = 2.
        /// For item 2 with prices[2]=6, prices[3]=2 is the discount, so the final price is 6 - 2 = 4.
        /// Items 3 and 4 do not receive any discount.
        ///
        /// Example 2:
        /// Input: prices = [1,2,3,4,5]
        /// Output: [1,2,3,4,5]
        /// Explanation: In this case, no item receives any discount.
        ///
        /// Example 3:
        /// Input: prices = [10,1,1,6]
        /// Output: [9,0,1,6]
        ///
        /// Constraints:
        /// - 1 &lt;= prices.length &lt;= 500
        /// - 1 &lt;= prices[i] &lt;= 1000
        /// </para>
        /// <para>
        /// 1475. 商品折扣後的最終價格
        /// https://leetcode.cn/problems/final-prices-with-a-special-discount-in-a-shop/description/
        ///
        /// 給定整數陣列 prices，其中 prices[i] 是商店內第 i 件商品的價格。
        ///
        /// 商店提供特殊折扣。購買第 i 件商品時，可以獲得等同於 prices[j] 的折扣，其中 j 是符合 j &gt; i 且
        /// prices[j] &lt;= prices[i] 的最小索引；若不存在這樣的索引，則不會獲得任何折扣。
        ///
        /// 回傳整數陣列 answer，其中 answer[i] 是套用特殊折扣後，第 i 件商品實際支付的最終價格。
        ///
        /// 範例 1：
        /// 輸入：prices = [8,4,6,2,3]
        /// 輸出：[4,2,4,2,3]
        /// 解釋：
        /// 商品 0 的 prices[0]=8，以 prices[1]=4 折抵，最終價格為 8 - 4 = 4。
        /// 商品 1 的 prices[1]=4，以 prices[3]=2 折抵，最終價格為 4 - 2 = 2。
        /// 商品 2 的 prices[2]=6，以 prices[3]=2 折抵，最終價格為 6 - 2 = 4。
        /// 商品 3 與商品 4 不會獲得任何折扣。
        ///
        /// 範例 2：
        /// 輸入：prices = [1,2,3,4,5]
        /// 輸出：[1,2,3,4,5]
        /// 解釋：此時所有商品都不會獲得任何折扣。
        ///
        /// 範例 3：
        /// 輸入：prices = [10,1,1,6]
        /// 輸出：[9,0,1,6]
        ///
        /// 限制條件：
        /// - 1 &lt;= prices.length &lt;= 500
        /// - 1 &lt;= prices[i] &lt;= 1000
        /// </para>
        /// </summary>
        /// <remarks>
        /// 以固定案例比較暴力模擬與單調堆疊解法，並驗證兩種方法都不會修改輸入陣列。
        /// </remarks>
        /// <param name="args">命令列參數；此範例程式不使用。</param>
        static void Main(string[] args)
        {
            (string Name, int[] Prices, int[] Expected)[] cases =
            [
                ("官方範例一：連續出現可用折扣", [8, 4, 6, 2, 3], [4, 2, 4, 2, 3]),
                ("官方範例二：右側價格皆較高", [1, 2, 3, 4, 5], [1, 2, 3, 4, 5]),
                ("官方範例三：相同價格可作為折扣", [10, 1, 1, 6], [9, 0, 1, 6]),
                ("重複值：每件商品取最近的相同價格", [5, 5, 5], [0, 0, 5]),
                ("防禦性案例：空陣列", [], [])
            ];

            int passedChecks = 0;
            int totalChecks = cases.Length * 4;

            for (int i = 0; i < cases.Length; i++)
            {
                passedChecks += RunCase(i + 1, cases[i].Name, cases[i].Prices, cases[i].Expected);
            }

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 執行單一測試案例，分別呼叫兩種解法並比較預期輸出，同時確認各自收到的輸入副本保持不變。
        /// 輸入價格與預期結果可為空陣列；回傳本案例通過的檢查數，範圍為 0 到 4。
        /// </summary>
        /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
        /// <param name="name">案例用途或情境說明。</param>
        /// <param name="prices">要驗證的原始商品價格。</param>
        /// <param name="expected">套用折扣後的預期價格。</param>
        /// <returns>兩個結果檢查與兩個輸入不變檢查中，通過的項目數。</returns>
        private static int RunCase(int caseNumber, string name, int[] prices, int[] expected)
        {
            int[] bruteForceInput = [.. prices];
            int[] monotonicStackInput = [.. prices];
            int[] bruteForceResult = FinalPrices(bruteForceInput);
            int[] monotonicStackResult = FinalPrices2(monotonicStackInput);

            bool bruteForceResultPassed = bruteForceResult.SequenceEqual(expected);
            bool bruteForceInputPassed = bruteForceInput.SequenceEqual(prices);
            bool monotonicStackResultPassed = monotonicStackResult.SequenceEqual(expected);
            bool monotonicStackInputPassed = monotonicStackInput.SequenceEqual(prices);

            Console.WriteLine($"Case {caseNumber}: {name}");
            Console.WriteLine($"Input: {FormatArray(prices)}");
            Console.WriteLine($"Expected: {FormatArray(expected)}");
            Console.WriteLine($"FinalPrices Actual: {FormatArray(bruteForceResult)}");
            Console.WriteLine($"FinalPrices Result: {(bruteForceResultPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"FinalPrices Input unchanged: {(bruteForceInputPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"FinalPrices2 Actual: {FormatArray(monotonicStackResult)}");
            Console.WriteLine($"FinalPrices2 Result: {(monotonicStackResultPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"FinalPrices2 Input unchanged: {(monotonicStackInputPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return Convert.ToInt32(bruteForceResultPassed)
                + Convert.ToInt32(bruteForceInputPassed)
                + Convert.ToInt32(monotonicStackResultPassed)
                + Convert.ToInt32(monotonicStackInputPassed);
        }

        /// <summary>
        /// 將整數陣列格式化為易讀且固定的方括號表示法，供主程式輸出測試資料與比對結果。
        /// 輸入必須是非 null 陣列；空陣列輸出為 <c>[]</c>。
        /// </summary>
        /// <param name="values">要格式化的整數陣列。</param>
        /// <returns>以逗號與空格分隔元素的字串。</returns>
        private static string FormatArray(int[] values)
        {
            return $"[{string.Join(", ", values)}]";
        }

        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/final-prices-with-a-special-discount-in-a-shop/solutions/1788169/shang-pin-zhe-kou-hou-de-zui-zhong-jie-g-ind3/
        /// https://leetcode.cn/problems/final-prices-with-a-special-discount-in-a-shop/solutions/1790738/by-ac_oier-hw5b/
        /// https://leetcode.cn/problems/final-prices-with-a-special-discount-in-a-shop/solutions/2642423/1475-shang-pin-zhe-kou-hou-de-zui-zhong-ln4to/
        /// 
        /// 方法一:直接模擬題目敘述 使用兩個迴圈來跑整個輸入資料 遍歷
        /// 
        /// 折扣條件:
        /// 如果你要买第 i 件商品，那么你可以得到与 prices[j] 相等的折扣，
        /// 1. j 是满足 j > i 
        /// 2. prices[j] <= prices[i] 
        /// 3. 如果没有满足条件的 j ，你将没有任何折扣。
        /// 
        /// 时间复杂度：O(n^2)，其中 n 为数组的长度。对于每个商品，我们需要遍历一遍数组查找符合题目要求的折扣。
        /// 空间复杂度：O(1)。返回值不计入空间复杂度。
        /// </summary>
        /// <param name="prices">商品價格陣列；可為空陣列，且方法不會修改其內容。</param>
        /// <returns>每件商品套用右側第一個不高於原價的折扣後所形成的新陣列。</returns>
        public static int[] FinalPrices(int[] prices)
        {
            int n = prices.Length;
            int[] res = new int[n];

            for (int i = 0; i < n; i++)
            {
                // 折扣
                int discount = 0;
                // 從 i + 1 開始向後遍歷, 找出 prices[j] <= prices[i] 的 index 
                for (int j = i + 1; j < n; j++)
                {
                    // 符合折扣條件
                    if (prices[j] <= prices[i])
                    {
                        discount = prices[j];
                        break;
                    }
                }

                res[i] = prices[i] - discount;
            }

            return res;
        }

        /// <summary>
        /// 計算每件商品套用特殊折扣後的最終價格，使用單調遞增索引堆疊記錄尚未找到折扣的商品。
        /// 輸入可為空陣列且不會被修改；每個索引最多進出堆疊一次，回傳新的最終價格陣列。
        /// 時間複雜度為 O(n)，額外空間複雜度為 O(n)。
        /// </summary>
        /// <param name="prices">商品價格陣列；可為空陣列，且方法不會修改其內容。</param>
        /// <returns>每件商品套用右側第一個不高於原價的折扣後所形成的新陣列。</returns>
        public static int[] FinalPrices2(int[] prices)
        {
            int[] result = [.. prices];
            Stack<int> pendingIndices = new();

            for (int i = 0; i < prices.Length; i++)
            {
                // 由左往右掃描，當前商品就是所有符合條件之待處理商品遇到的第一個折扣。
                while (pendingIndices.Count > 0 && prices[pendingIndices.Peek()] >= prices[i])
                {
                    int discountedIndex = pendingIndices.Pop();
                    result[discountedIndex] -= prices[i];
                }

                // 堆疊中的索引尚未取得折扣，其對應價格由底至頂保持嚴格遞增。
                pendingIndices.Push(i);
            }

            return result;
        }
    }
}