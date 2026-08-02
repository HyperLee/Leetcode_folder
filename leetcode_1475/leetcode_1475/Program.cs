namespace leetcode_1475
{
    internal class Program
    {
        /// <summary>
        /// 1475. Final Prices With a Special Discount in a Shop
        /// https://leetcode.com/problems/final-prices-with-a-special-discount-in-a-shop/description/?envType=daily-question&envId=2024-12-18
        /// 
        /// 1475. 商品折扣后的最终价格
        /// https://leetcode.cn/problems/final-prices-with-a-special-discount-in-a-shop/description/
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