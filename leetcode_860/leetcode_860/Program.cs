namespace leetcode_860
{
    internal class Program
    {
        /// <summary>
        /// 860. Lemonade Change
        /// https://leetcode.com/problems/lemonade-change/description/
        /// <para>
        /// At a lemonade stand, each lemonade costs $5. Customers queue and order one at a time in the order given by bills. Each customer buys one lemonade and pays with a $5, $10, or $20 bill. You must give correct change so that each customer's net payment is $5.
        ///
        /// You have no change initially.
        ///
        /// Given an integer array bills where bills[i] is the bill paid by the i-th customer, return true if you can give every customer correct change, or false otherwise.
        ///
        /// Example 1:
        /// Input: bills = [5,5,5,10,20]
        /// Output: true
        /// Explanation: From the first 3 customers, collect three $5 bills. From the fourth, collect $10 and return $5. For the fifth, return a $10 bill and a $5 bill. Every customer receives correct change, so return true.
        ///
        /// Example 2:
        /// Input: bills = [5,5,10,10,20]
        /// Output: false
        /// Explanation: Collect two $5 bills from the first two customers. For the next two, collect $10 and return $5 each time. For the final customer, you cannot return $15 because only two $10 bills remain. Not every customer receives correct change, so return false.
        ///
        /// Constraints:
        /// - 1 &lt;= bills.length &lt;= 10^5
        /// - bills[i] is 5, 10, or 20.
        /// </para>
        /// <para>
        /// 860. 檸檬水找零
        /// https://leetcode.cn/problems/lemonade-change/description/
        ///
        /// 檸檬水攤每杯檸檬水售價 $5。顧客依 bills 指定的順序排隊並逐一購買；每位顧客只買一杯，並以 $5、$10 或 $20 紙鈔付款。必須正確找零，使每位顧客的淨付款為 $5。
        ///
        /// 一開始手上沒有任何零錢。
        ///
        /// 給定整數陣列 bills，其中 bills[i] 是第 i 位顧客支付的紙鈔。若能為每位顧客正確找零，回傳 true；否則回傳 false。
        ///
        /// 範例 1：
        /// 輸入：bills = [5,5,5,10,20]
        /// 輸出：true
        /// 解釋：先從前 3 位顧客依序收取三張 $5。第四位支付 $10，找回 $5。第五位則找回一張 $10 與一張 $5。所有顧客都得到正確找零，因此回傳 true。
        ///
        /// 範例 2：
        /// 輸入：bills = [5,5,10,10,20]
        /// 輸出：false
        /// 解釋：先從前兩位顧客收取兩張 $5。接下來兩位各支付 $10 並各找回 $5。最後一位需要找回 $15，但手上只剩兩張 $10，無法找零。並非每位顧客都得到正確找零，因此回傳 false。
        ///
        /// 限制條件：
        /// - 1 &lt;= bills.length &lt;= 10^5
        /// - bills[i] 是 5、10 或 20。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行七組固定案例，逐案驗證貪心找零演算法。
        /// 輸入由方法內定義，皆為只包含 5、10、20 的非空帳單陣列；
        /// 輸出每案的輸入、預期結果、實際結果、PASS/FAIL 與通過總數。
        /// </summary>
        private static void RunSamples()
        {
            SampleCase[] samples =
            [
                new("官方範例：所有顧客皆可正確找零", [5, 5, 5, 10, 20], true),
                new("官方範例：最後一位顧客無法找零", [5, 5, 10, 10, 20], false),
                new("最小長度：收到 5 元不必找零", [5], true),
                new("起手收到 10 元", [10], false),
                new("起手收到 20 元", [20], false),
                new("使用三張 5 元找 20 元", [5, 5, 5, 20], true),
                new("優先使用 10 元加 5 元保留零錢", [5, 5, 5, 5, 5, 10, 20, 10, 10], true)
            ];

            int passedChecks = 0;

            for (int index = 0; index < samples.Length; index++)
            {
                SampleCase sample = samples[index];
                bool actual = LemonadeChange(sample.Bills);
                bool passed = actual == sample.Expected;

                passedChecks += passed ? 1 : 0;

                Console.WriteLine($"案例 {index + 1}：{sample.Name}");
                Console.WriteLine($"bills = {FormatBills(sample.Bills)}");
                Console.WriteLine($"預期：{sample.Expected.ToString().ToLowerInvariant()}");
                Console.WriteLine($"實際：{actual.ToString().ToLowerInvariant()} => {(passed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedChecks}/{samples.Length} 項驗證通過");
            if (passedChecks != samples.Length)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 將帳單陣列格式化為緊湊的方括號字串，供驗收輸出與 README 對照。
        /// 輸入為任意整數陣列，輸出格式如 <c>[5,10,20]</c>。
        /// </summary>
        /// <param name="bills">要格式化的帳單陣列。</param>
        /// <returns>以逗號分隔各張帳單的方括號字串。</returns>
        private static string FormatBills(int[] bills)
        {
            return $"[{string.Join(",", bills)}]";
        }

        /// <summary>
        /// 依顧客順序模擬交易，使用貪心策略判斷是否能為每位顧客正確找零。
        /// 輸入須為只包含 5、10、20 的非空帳單陣列；方法不會修改輸入。
        /// 所有交易都能完成時回傳 <see langword="true"/>，首次無法找零時回傳 <see langword="false"/>。
        /// </summary>
        /// <param name="bills">依顧客付款順序排列的帳單陣列。</param>
        /// <returns>是否能按照順序為全部顧客正確找零。</returns>
        public static bool LemonadeChange(int[] bills)
        {
            // 20 元不會用來找零，因此只需追蹤手上的 5 元與 10 元張數。
            int fiveDollarBills = 0;
            int tenDollarBills = 0;

            foreach (int bill in bills)
            {
                if (bill == 5)
                {
                    fiveDollarBills++;
                }
                else if (bill == 10)
                {
                    fiveDollarBills--;
                    tenDollarBills++;
                }
                else if (tenDollarBills > 0)
                {
                    // 收到 20 元時優先找 10+5，盡量保留用途更廣的 5 元鈔票。
                    fiveDollarBills--;
                    tenDollarBills--;
                }
                else
                {
                    fiveDollarBills -= 3;
                }

                // 5 元是所有找零組合的必要面額，張數不足時後續也無法補救。
                if (fiveDollarBills < 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 表示一組驗收案例，保存案例名稱、付款順序與預期是否能完成全部交易。
        /// </summary>
        /// <param name="Name">案例顯示名稱。</param>
        /// <param name="Bills">符合題目限制的帳單陣列。</param>
        /// <param name="Expected">預期是否能為全部顧客找零。</param>
        private sealed record SampleCase(string Name, int[] Bills, bool Expected);
    }
}
