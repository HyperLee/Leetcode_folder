namespace leetcode_2591;

class Program
{
    /// <summary>
    /// 2591. Distribute Money to Maximum Children
    /// https://leetcode.com/problems/distribute-money-to-maximum-children/description/
    /// 2591. 将钱分给最多的儿童
    /// https://leetcode.cn/problems/distribute-money-to-maximum-children/description/
    ///
    /// English:
    /// You are given an integer money denoting the amount of money (in dollars) that you have
    /// and another integer children denoting the number of children that you must distribute
    /// the money to.
    ///
    /// You have to distribute the money according to the following rules:
    /// - All money must be distributed.
    /// - Everyone must receive at least 1 dollar.
    /// - Nobody receives 4 dollars.
    ///
    /// Return the maximum number of children who may receive exactly 8 dollars if you distribute
    /// the money according to the aforementioned rules. If there is no way to distribute the money,
    /// return -1.
    ///
    /// 繁體中文：
    /// 給定一個整數 money，表示你擁有的金額（以美元為單位），以及另一個整數 children，
    /// 表示必須將錢分配給多少名兒童。
    ///
    /// 你必須依照下列規則分配金錢：
    /// - 所有的錢都必須分配完畢。
    /// - 每個人至少必須收到 1 美元。
    /// - 任何人都不能收到 4 美元。
    ///
    /// 如果依照上述規則分配金錢，回傳最多可以有多少名兒童恰好收到 8 美元。
    /// 如果無法完成分配，則回傳 -1。
    /// </summary>
    /// <remarks>
    /// 使用固定測資依序執行三個解法，並輸出每次執行的實際值、預期值與 PASS/FAIL 結果。
    /// </remarks>
    /// <param name="args">命令列參數；目前的固定測試流程不使用此參數。</param>
    static void Main(string[] args)
    {
        Program solution = new();
        var solutions = new (string Name, Func<int, int, int> Solve)[]
        {
            ("DistMoney", solution.DistMoney),
            ("DistMoney2", solution.DistMoney2),
            ("DistMoney3", solution.DistMoney3)
        };
        var testCases = new (int Money, int Children, int Expected)[]
        {
            (20, 3, 1),
            (16, 2, 2),
            (2, 3, -1),
            (12, 2, 0),
            (17, 2, 1),
            (9, 2, 1)
        };

        int passedTests = 0;
        int totalTests = testCases.Length * solutions.Length;

        Console.WriteLine("LeetCode 2591 - Distribute Money to Maximum Children");
        Console.WriteLine();

        for (int caseIndex = 0; caseIndex < testCases.Length; caseIndex++)
        {
            (int money, int children, int expected) = testCases[caseIndex];
            Console.WriteLine(
                $"Case {caseIndex + 1}: money = {money}, children = {children}, expected = {expected}");

            foreach ((string name, Func<int, int, int> solve) in solutions)
            {
                int actual = solve(money, children);
                bool passed = actual == expected;

                if (passed)
                {
                    passedTests++;
                }

                Console.WriteLine(
                    $"  {name,-10} actual = {actual}, expected = {expected} => {(passed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"{passedTests}/{totalTests} tests passed.");
    }

    /// <summary>
    /// 計算最多能有多少名兒童恰好取得 8 元。
    /// 先替每人保留 1 元，再以額外 7 元代表把一人補到 8 元，接著利用商數與餘數
    /// 判斷可取得 8 元的人數，並修正最後一人會取得 4 元的特殊情況。
    /// </summary>
    /// <param name="money">要全部分完的金額，題目限制為 1 到 200。</param>
    /// <param name="children">必須取得金錢的兒童人數，題目限制為 2 到 30。</param>
    /// <returns>
    /// 最多能取得 8 元的兒童人數；若金額不足以讓每人至少取得 1 元，則回傳 -1。
    /// </returns>
    /// <remarks>時間複雜度為 O(1)，額外空間複雜度為 O(1)。</remarks>
    public int DistMoney(int money, int children)
    {
        // 先滿足每人至少 1 元；後續每增加 7 元，就能讓一人從 1 元變成 8 元。
        money -= children;

        if (money < 0)
        {
            return -1;
        }

        if (money / 7 == children && money % 7 == 0)
        {
            return children;
        }

        // 若只剩一人且餘下 3 元，該人會拿到 1 + 3 = 4 元。
        // 必須再拆掉一組 8 元，將金額分散給至少兩人，因此答案減少到 children - 2。
        if (money / 7 == children - 1 && money % 7 == 3)
        {
            return children - 2;
        }

        // 除了所有人剛好都取得 8 元外，至少保留一人吸收剩餘金額。
        return Math.Min(children - 1, money / 7);
    }

    /// <summary>
    /// 以展開式貪心流程計算最多能有多少名兒童恰好取得 8 元。
    /// 先給每人 1 元，再讓盡可能多人各取得額外 7 元，最後依剩餘人數與剩餘金額
    /// 修正「無人吸收餘額」及「最後一人會取得 4 元」兩種不合法分配。
    /// </summary>
    /// <param name="money">要全部分完的金額，題目限制為 1 到 200。</param>
    /// <param name="children">必須取得金錢的兒童人數，題目限制為 2 到 30。</param>
    /// <returns>
    /// 最多能取得 8 元的兒童人數；若金額不足以讓每人至少取得 1 元，則回傳 -1。
    /// </returns>
    /// <remarks>時間複雜度為 O(1)，額外空間複雜度為 O(1)。</remarks>
    public int DistMoney2(int money, int children)
    {
        // 先替每人保留題目要求的最低金額 1 元。
        money -= children;

        if (money < 0)
        {
            return -1;
        }

        // 每額外分配 7 元，就能把一人從 1 元補到 8 元。
        int ans = Math.Min(money / 7, children);

        // 更新完成貪心分配後的剩餘金額與尚未取得 8 元的人數。
        money -= ans * 7;
        children -= ans;

        // 所有人都已取得 8 元但仍有餘額時，必須讓其中一人超過 8 元。
        if (children == 0 && money > 0)
        {
            ans--;
        }

        // 最後一人若取得原本的 1 元加剩餘 3 元會變成 4 元，
        // 必須拆掉一組 8 元並重新分配，所以能取得 8 元的人數再減少一人。
        if (children == 1 && money == 3)
        {
            ans--;
        }

        return ans;
    }

    /// <summary>
    /// 以精簡貪心流程計算最多能有多少名兒童恰好取得 8 元。
    /// 此方法與 <see cref="DistMoney2(int, int)"/> 使用相同概念：先滿足每人 1 元，
    /// 再盡量以額外 7 元湊出 8 元，最後用一個合併條件修正兩種不合法的剩餘狀態。
    /// </summary>
    /// <param name="money">要全部分完的金額，題目限制為 1 到 200。</param>
    /// <param name="children">必須取得金錢的兒童人數，題目限制為 2 到 30。</param>
    /// <returns>
    /// 最多能取得 8 元的兒童人數；若金額不足以讓每人至少取得 1 元，則回傳 -1。
    /// </returns>
    /// <remarks>時間複雜度為 O(1)，額外空間複雜度為 O(1)。</remarks>
    public int DistMoney3(int money, int children)
    {
        if (money < children)
        {
            return -1;
        }

        // 先給每人 1 元，再以每組 7 元盡可能把兒童補到 8 元。
        money -= children;
        int cnt = Math.Min(money / 7, children);
        money -= cnt * 7;
        children -= cnt;

        // 無人吸收餘額，或最後一人會取得 4 元時，都必須拆掉一組 8 元。
        if ((children == 0 && money > 0) || (children == 1 && money == 3))
        {
            cnt--;
        }

        return cnt;
    }
}