namespace leetcode_135;

class Program
{
    /// <summary>
    /// 135. Candy
    /// https://leetcode.com/problems/candy/description/
    /// 135. 分发糖果
    /// https://leetcode.cn/problems/candy/description/?envType=daily-question&envId=2025-06-02
    /// 
    /// 有 n 個小孩站成一排。給你一個整數陣列 ratings 表示每個小孩的評分。
    /// 你需要給這些小孩分發糖果，需遵守下列規則：
    /// 1. 每個小孩至少分到 1 顆糖果。
    /// 2. 評分較高的小孩必須比他相鄰的小孩分到更多糖果。
    /// 請返回你需要準備的最少糖果數量，才能分發給這些小孩。
    /// 
    /// 解題提示：
    /// 1. 可以用兩次遍歷（從左到右、從右到左）來確保每個小孩都滿足規則。
    /// 2. 先從左到右，若 ratings[i] > ratings[i-1]，則 candies[i] = candies[i-1] + 1。
    /// 3. 再從右到左，若 ratings[i] > ratings[i+1]，則 candies[i] = Math.Max(candies[i], candies[i+1] + 1)。
    /// 4. 最後將 candies 陣列總和即為答案。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 建立固定測試案例，依序執行兩次遍歷與單次遍歷解法，並統計每一項結果是否符合預期。
    /// 輸入條件由方法內的七組合法 ratings 陣列提供；輸出為各案例的 Expected、Actual、
    /// PASS/FAIL，以及全部十四項驗證的通過數量。
    /// </summary>
    private static void RunSamples()
    {
        SampleCase[] cases =
        [
            new("官方範例一", [1, 0, 2], 5),
            new("官方範例二（相同評分）", [1, 2, 2], 4),
            new("連續上升後下降", [1, 3, 4, 5, 2], 11),
            new("完全遞減", [5, 4, 3, 2, 1], 15),
            new("對稱山峰", [1, 2, 3, 2, 1], 9),
            new("單一小孩", [1], 1),
            new("谷底", [2, 1, 2], 5)
        ];

        Program solver = new();
        (string Name, Func<int[], int> Solve)[] solutions =
        [
            ("方法一：兩次遍歷", solver.Candy),
            ("方法二：單次遍歷", solver.Candy2)
        ];

        int passedChecks = 0;
        int totalChecks = cases.Length * solutions.Length;

        Console.WriteLine("LeetCode 135 - 分發糖果");
        Console.WriteLine();

        foreach ((string solutionName, Func<int[], int> solve) in solutions)
        {
            Console.WriteLine(solutionName);

            for (int i = 0; i < cases.Length; i++)
            {
                if (RunCase(i + 1, cases[i], solve))
                {
                    passedChecks++;
                }
            }
        }

        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
    }

    /// <summary>
    /// 執行單一解法與案例，將輸入複製後交給演算法，避免不同解法之間共享可變狀態。
    /// 輸入包含案例編號、合法測試資料與待驗證函式；輸出為實際結果是否等於預期值，
    /// 並同步在主控台列印可供 README 使用的穩定驗證格式。
    /// </summary>
    /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
    /// <param name="sample">包含案例名稱、評分陣列與預期糖果總數的測試資料。</param>
    /// <param name="solve">接收評分陣列並回傳最少糖果數的解法。</param>
    /// <returns>實際結果與預期結果相同時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
    private static bool RunCase(int caseNumber, SampleCase sample, Func<int[], int> solve)
    {
        int actual = solve((int[])sample.Ratings.Clone());
        bool passed = actual == sample.Expected;

        Console.WriteLine($"案例 {caseNumber}：{sample.Name}");
        Console.WriteLine($"輸入：{FormatArray(sample.Ratings)}");
        Console.WriteLine($"Expected：{sample.Expected}");
        Console.WriteLine($"Actual：{actual} => {(passed ? "PASS" : "FAIL")}");
        Console.WriteLine();

        return passed;
    }

    /// <summary>
    /// 將整數陣列格式化為具有固定逗號與空格的中括號表示法。
    /// 輸入可為空但不可為 <see langword="null"/> 的陣列；輸出例如 <c>[1, 0, 2]</c>，
    /// 供主控台輸出與 README 範例保持一致。
    /// </summary>
    /// <param name="values">要格式化的整數陣列。</param>
    /// <returns>以中括號包住、以逗號及空格分隔的陣列文字。</returns>
    private static string FormatArray(int[] values)
    {
        return $"[{string.Join(", ", values)}]";
    }

    /// <summary>
    /// 使用兩次遍歷計算符合相鄰評分規則的最少糖果數。
    /// 先由左至右滿足左鄰居約束，再由右至左以較大值補足右鄰居約束，
    /// 因而能同時保留兩個方向的最低合法分配。
    /// 輸入須為非 <see langword="null"/> 的評分陣列；題目保證長度至少為 1，
    /// 方法亦保留空陣列回傳 0 的既有行為。輸出為完成合法分配所需的最少糖果總數。
    /// </summary>
    /// <param name="ratings">依站位順序排列的每位小孩評分。</param>
    /// <returns>滿足所有相鄰評分規則的最少糖果總數。</returns>
    public int Candy(int[] ratings)
    {
        int n = ratings.Length;
        if (n == 0)
        {
            return 0;
        }

        int[] candies = new int[n];
        for (int i = 0; i < n; i++)
        {
            candies[i] = 1;
        }

        // 第一趟只處理左鄰居約束：評分上升時，糖果必須比左側多一顆。
        for (int i = 1; i < n; i++)
        {
            if (ratings[i] > ratings[i - 1])
            {
                candies[i] = candies[i - 1] + 1;
            }
        }

        // 第二趟補足右鄰居約束；取最大值才能保留第一趟已建立的左側約束。
        for (int i = n - 2; i >= 0; i--)
        {
            if (ratings[i] > ratings[i + 1])
            {
                candies[i] = Math.Max(candies[i], candies[i + 1] + 1);
            }
        }

        int totalCandies = 0;
        foreach (int candy in candies)
        {
            totalCandies += candy;
        }

        return totalCandies;
    }


    /// <summary>
    /// 使用一次遍歷與遞增、遞減序列狀態計算最少糖果數。
    /// <c>inc</c> 記錄最近上升序列峰值的糖果數，<c>dec</c> 記錄目前下降長度，
    /// <c>pre</c> 記錄前一位糖果數；當下降長度碰到峰值時額外補一顆，
    /// 使峰頂仍多於相鄰小孩。輸入須為非 <see langword="null"/> 且至少含一筆評分的陣列；
    /// 輸出為符合所有相鄰評分規則的最少糖果總數。
    /// </summary>
    /// <param name="ratings">依站位順序排列，且至少包含一個元素的評分陣列。</param>
    /// <returns>滿足所有相鄰評分規則的最少糖果總數。</returns>
    /// <remarks>
    /// 參考：
    /// https://leetcode.cn/problems/candy/solutions/533150/fen-fa-tang-guo-by-leetcode-solution-f01p/
    /// </remarks>
    public int Candy2(int[] ratings)
    {
        int n = ratings.Length;
        int res = 1;
        int inc = 1;
        int dec = 0;
        int pre = 1;

        for (int i = 1; i < n; i++)
        {
            if (ratings[i] >= ratings[i - 1])
            {
                // 上升時延續糖果數；持平沒有大小約束，因此重新從一顆開始。
                dec = 0;
                pre = ratings[i] == ratings[i - 1] ? 1 : pre + 1;
                res += pre;
                inc = pre;
            }
            else
            {
                dec++;
                if (dec == inc)
                {
                    // 下降序列追上峰值時補一顆，確保峰頂仍嚴格高於右鄰居。
                    dec++;
                }
                res += dec;
                pre = 1;
            }
        }

        return res;
    }

    /// <summary>
    /// 表示一組可重複執行的糖果分配測試資料。
    /// 輸入為案例名稱、符合題目限制的評分陣列及預期最少糖果數；
    /// 輸出由不可變屬性保存，供兩種解法共享同一份驗證規格。
    /// </summary>
    /// <param name="Name">案例的教學名稱。</param>
    /// <param name="Ratings">依站位順序排列的評分陣列。</param>
    /// <param name="Expected">預期的最少糖果總數。</param>
    private sealed record SampleCase(string Name, int[] Ratings, int Expected);
}
