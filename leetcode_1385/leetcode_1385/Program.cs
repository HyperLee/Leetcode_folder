namespace leetcode_1385;

class Program
{
    /// <summary>
    /// 1385. Find the Distance Value Between Two Arrays
    /// https://leetcode.com/problems/find-the-distance-value-between-two-arrays/description/
    /// 1385. 兩個陣列間的距離值
    /// https://leetcode.cn/problems/find-the-distance-value-between-two-arrays/description/
    ///
    /// English:
    /// Given two integer arrays arr1 and arr2, and the integer d,
    /// return the distance value between the two arrays.
    /// The distance value is defined as the number of elements arr1[i]
    /// such that there is not any element arr2[j] where |arr1[i]-arr2[j]| &lt;= d.
    ///
    /// 繁體中文：
    /// 給定兩個整數陣列 arr1 和 arr2，以及整數 d，回傳這兩個陣列之間的距離值。
    /// 距離值定義為：arr1 中符合以下條件的元素 arr1[i] 的數量：
    /// 不存在任何 arr2[j]，使得 |arr1[i]-arr2[j]| &lt;= d。
    /// 換句話說，計算 arr1 中與 arr2 所有元素的絕對差皆大於 d 的元素數量。
    /// </summary>
    /// <param name="args"></param>
    /// <remarks>
    /// 固定、無須輸入的測試入口：逐一比較四種解法的預期與實際結果，輸出通過狀態與總計。
    /// 每次呼叫使用獨立陣列副本；若任一檢查失敗，程式結束碼為 1，全部通過則為 0。
    /// </remarks>
    static void Main(string[] args)
    {
        (string Name, int[] Arr1, int[] Arr2, int D, int Expected)[] testCases =
        [
            ("官方範例一", [4, 5, 8], [10, 9, 1, 8], 2, 2),
            ("官方範例二", [1, 4, 2, 3], [-4, -3, 6, 10, 20, 30], 3, 2),
            ("官方範例三", [2, 1, 100, 3], [-5, -2, 10, -3, 7], 6, 1),
            ("距離恰等於 d", [4, 8], [6], 2, 0),
            ("d 為零且包含命中", [1, 2, 3], [2], 0, 2),
            ("重複元素分別計數", [4, 4, 8, 8], [8, 8], 2, 2),
            ("負數", [-10, -5, -1], [-3, -8], 1, 3),
            ("全部符合", [1, 2, 3], [10, 20], 2, 3),
            ("全部不符合", [1, 2, 3], [2], 1, 0),
            ("插入位置在最前", [-5], [10, 0, 5], 2, 1),
            ("插入位置在中間", [4], [10, 0, 8], 2, 1),
            ("插入位置在最後", [15], [10, 0, 5], 2, 1),
            ("右鄰居距離恰等於 d", [6], [10, 0, 8], 2, 0),
            ("左鄰居距離恰等於 d", [2], [10, 0, 8], 2, 0),
            ("單元素精確命中", [7], [7], 0, 0),
            ("數值上下界及最大 d", [-1000, 1000], [900, -900], 100, 0),
            ("數值上下界最大差", [-1000], [1000], 100, 1),
            ("最大陣列長度", Enumerable.Repeat(-1000, 500).ToArray(),
                Enumerable.Repeat(1000, 500).ToArray(), 100, 500),
            ("重複左界命中", [5], [3, 3, 10], 2, 0),
            ("候選位於區間內", [5], [1, 6, 10], 2, 0),
            ("候選恰超過右界", [5], [1, 8, 10], 2, 1),
            ("全部元素恰低於左界", [5], [2], 2, 1)
        ];

        Program solution = new Program();
        (string Name, Func<int[], int[], int, int> Solve)[] solutions =
        [
            ("解法一", solution.FindTheDistanceValue),
            ("解法二", solution.FindTheDistanceValue2),
            ("解法三", solution.FindTheDistanceValue3),
            ("解法四", solution.FindTheDistanceValue4)
        ];

        int passed = 0;
        foreach ((string name, int[] arr1, int[] arr2, int d, int expected) in testCases)
        {
            foreach ((string solutionName, Func<int[], int[], int, int> solve) in solutions)
            {
                if (RunTest(name, solutionName, solve, arr1, arr2, d, expected))
                {
                    passed++;
                }
            }
        }

        int total = testCases.Length * solutions.Length;
        Console.WriteLine($"總計：{testCases.Length} 組案例，{total} 次檢查，通過 {passed}，失敗 {total - passed}。");
        Environment.ExitCode = passed == total ? 0 : 1;
    }

    /// <summary>
    /// 執行一個固定案例：複製符合題目限制的輸入陣列，呼叫指定解法並比較預期值，
    /// 輸出案例、解法、預期與實際結果，回傳是否通過；副本可隔離原地排序的影響。
    /// </summary>
    /// <param name="caseName">案例名稱。</param>
    /// <param name="solutionName">解法名稱。</param>
    /// <param name="solve">接受兩個陣列與距離上限、回傳距離值的解法。</param>
    /// <param name="arr1">待計數的非空整數陣列。</param>
    /// <param name="arr2">比較用的非空整數陣列。</param>
    /// <param name="d">非負的排除距離上限。</param>
    /// <param name="expected">人工推導的預期距離值。</param>
    /// <returns>實際結果等於預期值時為 true，否則為 false。</returns>
    private static bool RunTest(string caseName, string solutionName,
        Func<int[], int[], int, int> solve, int[] arr1, int[] arr2, int d, int expected)
    {
        int actual = solve((int[])arr1.Clone(), (int[])arr2.Clone(), d);
        bool passed = actual == expected;
        Console.WriteLine($"[{(passed ? "PASS" : "FAIL")}] {caseName} / {solutionName}：預期 {expected}，實際 {actual}");
        return passed;
    }

    /// <summary>
    /// 解法一：雙迴圈模擬，為每個 arr1 元素尋找距離小於等於 d 的反例，找到就提前停止。
    /// 輸入遵循題目限制；回傳與 arr2 每個元素的絕對差都嚴格大於 d 的 arr1 元素數量。
    /// </summary>
    /// <param name="arr1">待計數陣列，長度 1 至 500，元素介於 -1000 至 1000。</param>
    /// <param name="arr2">比較用陣列，長度 1 至 500，元素介於 -1000 至 1000；不會被修改。</param>
    /// <param name="d">排除距離上限，介於 0 至 100；差值等於 d 也須排除。</param>
    /// <returns>符合條件的元素數量，範圍為 0 至 arr1.Length；重複元素分別計數。</returns>
    public int FindTheDistanceValue(int[] arr1, int[] arr2, int d)
    {
        int cnt = 0;
        foreach (int x in arr1)
        {
            bool isValid = true;
            foreach (int y in arr2)
            {
                // 只要一個反例就能排除 x，剩餘比較不會改變結論。
                if (Math.Abs(x - y) <= d)
                {
                    isValid = false;
                    break;
                }
            }
            if (isValid)
            {
                cnt++;
            }
        }
        return cnt;
    }

    /// <summary>
    /// 解法二：雙迴圈搭配布林 AND，將所有絕對差大於 d 的條件累積為一個有效旗標。
    /// 輸入遵循題目限制；所有比較皆成立才計數，回傳兩陣列的距離值。
    /// </summary>
    /// <param name="arr1">待計數陣列，長度 1 至 500，元素介於 -1000 至 1000。</param>
    /// <param name="arr2">比較用陣列，長度 1 至 500，元素介於 -1000 至 1000；不會被修改。</param>
    /// <param name="d">排除距離上限，介於 0 至 100。</param>
    /// <returns>與 arr2 所有元素的絕對差皆大於 d 的 arr1 元素數量，重複元素分別計數。</returns>
    public int FindTheDistanceValue2(int[] arr1, int[] arr2, int d)
    {
        int cnt = 0;
        foreach (int x in arr1)
        {
            bool isValid = true;
            foreach (int y in arr2)
            {
                // bool 的 &= 不短路；一旦變為 false，後續 AND 也無法使它恢復 true。
                isValid &= Math.Abs(x - y) > d;
            }

            // 將「全部比較皆成立」轉為 1，否則加 0。
            cnt += isValid ? 1 : 0;
        }
        return cnt;
    }

    /// <summary>
    /// 解法三：先排序 arr2，再以二分搜尋定位每個 x；未命中時只檢查插入位置兩側的最近鄰居。
    /// 輸入遵循題目限制；回傳所有絕對差皆大於 d 的 arr1 元素數量。
    /// </summary>
    /// <param name="arr1">待計數陣列，長度 1 至 500，元素介於 -1000 至 1000。</param>
    /// <param name="arr2">比較用陣列，長度 1 至 500，元素介於 -1000 至 1000；會被原地升冪排序。</param>
    /// <param name="d">排除距離上限，介於 0 至 100。</param>
    /// <returns>符合條件的 arr1 元素數量，重複元素分別計數。</returns>
    /// <remarks>若需保留 arr2 原始順序，呼叫端應先複製陣列再傳入。</remarks>
    public int FindTheDistanceValue3(int[] arr1, int[] arr2, int d)
    {
        Array.Sort(arr2);
        int cnt = 0;

        foreach (int x in arr1)
        {
            int p = Array.BinarySearch(arr2, x);
            if (p < 0)
            {
                // 未命中的負回傳值是插入索引的位元補數；~p 還原索引，不是邏輯否定。
                p = ~p;
            }

            bool isValid = true;
            if (p < arr2.Length)
            {
                // 命中時差為 0，必定排除；未命中時 p 是右側最近元素的位置。
                isValid &= Math.Abs(arr2[p] - x) > d;
            }

            // && 短路檢查左鄰居索引；最前插入時沒有左鄰居。
            if (p - 1 >= 0 && p - 1 < arr2.Length)
            {
                // 排序後更左側的元素距離只會更大，因此只需檢查這個左鄰居。
                isValid &= Math.Abs(arr2[p - 1] - x) > d;
            }
            cnt += isValid ? 1 : 0;
        }
        return cnt;
    }

    /// <summary>
    /// 解法四：排序 arr2 並搜尋排除區間左界 x-d，只檢查命中元素或未命中時的插入位置候選。
    /// 輸入遵循題目限制；區間 [x-d, x+d] 內沒有元素時才計數，回傳兩陣列的距離值。
    /// </summary>
    /// <param name="arr1">待計數陣列，長度 1 至 500，元素介於 -1000 至 1000。</param>
    /// <param name="arr2">比較用陣列，長度 1 至 500，元素介於 -1000 至 1000；會被原地升冪排序。</param>
    /// <param name="d">排除距離上限，介於 0 至 100；封閉區間的兩端點皆須排除。</param>
    /// <returns>與 arr2 所有元素的絕對差皆大於 d 的 arr1 元素數量，重複元素分別計數。</returns>
    /// <remarks>若需保留 arr2 原始順序，呼叫端應先複製；題目範圍內 x-d 與 x+d 不會溢位。</remarks>
    public int FindTheDistanceValue4(int[] arr1, int[] arr2, int d)
    {
        Array.Sort(arr2);
        int cnt = 0;

        foreach (int x in arr1)
        {
            int i = Array.BinarySearch(arr2, x - d);
            if (i < 0)
            {
                // 未命中時還原插入位置：第一個大於左界的元素，或陣列長度。
                i = ~i;
            }

            // 命中左界即有反例（包含重複值）；未命中時只需確認候選是否已超過右界。
            // || 短路：插入位置等於長度時不讀取 arr2[i]，避免越界。
            if (i == arr2.Length || arr2[i] > x + d)
            {
                cnt++;
            }
        }
        return cnt;
    }
}
