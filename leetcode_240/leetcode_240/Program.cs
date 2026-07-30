namespace leetcode_240;

class Program
{
    /// <summary>
    /// LeetCode 240. 搜索二維矩陣 II
    /// 題目描述：
    /// 給定一個 m x n 的整數矩陣 matrix，該矩陣每一行從左到右遞增、每一列從上到下遞增。
    /// 請判斷給定的目標值 target 是否存在於矩陣中。
    /// 
    /// 範例：
    /// matrix = [
    ///   [1, 4, 7, 11, 15],
    ///   [2, 5, 8, 12, 19],
    ///   [3, 6, 9, 16, 22],
    ///   [10, 13, 14, 17, 24],
    ///   [18, 21, 23, 26, 30]
    /// ]
    /// target = 5 回傳 true
    /// target = 20 回傳 false
    /// 
    /// https://leetcode.com/problems/search-a-2d-matrix-ii/description/
    /// https://leetcode.cn/problems/search-a-2d-matrix-ii/description/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 建立六組符合題目排序條件的固定案例，依序執行暴力法、逐列二分搜尋與右上角排除法。
    /// 每組輸入皆為至少一列、至少一欄的非遞減矩陣，方法會將實際結果與預期布林值比較，
    /// 最後輸出每項 PASS/FAIL 與通過總數。
    /// </summary>
    private static void RunSamples()
    {
        int[][] officialMatrix =
        [
            [1, 4, 7, 11, 15],
            [2, 5, 8, 12, 19],
            [3, 6, 9, 16, 22],
            [10, 13, 14, 17, 24],
            [18, 21, 23, 26, 30]
        ];

        SampleCase[] samples =
        [
            new("官方範例：命中矩陣中的值", officialMatrix, 5, true),
            new("官方範例：目標值不存在", officialMatrix, 20, false),
            new("單元素矩陣：命中唯一元素", [[1]], 1, true),
            new("單元素矩陣：目標值不存在", [[1]], 2, false),
            new(
                "矩形矩陣：命中負數與重複值",
                [
                    [-5, -3, -3, 4],
                    [-3, -1, 2, 8],
                    [0, 2, 5, 10]
                ],
                -3,
                true),
            new(
                "矩形矩陣：目標值超出上界",
                [
                    [1, 4, 7],
                    [2, 5, 8]
                ],
                9,
                false)
        ];

        Program solution = new();
        (string Name, Func<int[][], int, bool> Search)[] methods =
        [
            ("暴力法", solution.SearchMatrix),
            ("逐列二分搜尋", solution.SearchMatrix_binary),
            ("右上角排除法", solution.SearchMatrix_RightTop)
        ];

        int passed = 0;
        int total = samples.Length * methods.Length;

        for (int i = 0; i < samples.Length; i++)
        {
            SampleCase sample = samples[i];
            Console.WriteLine($"案例 {i + 1}：{sample.Name}");
            Console.WriteLine($"輸入：matrix = {FormatMatrix(sample.Matrix)}, target = {sample.Target}");
            Console.WriteLine($"Expected = {sample.Expected}");

            foreach ((string methodName, Func<int[][], int, bool> search) in methods)
            {
                bool actual = search(sample.Matrix, sample.Target);
                bool isPassed = actual == sample.Expected;
                passed += isPassed ? 1 : 0;
                Console.WriteLine(
                    $"  {methodName}：Actual = {actual} => {(isPassed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passed}/{total} 項驗證通過");
    }

    /// <summary>
    /// 將至少包含一列的鋸齒狀整數陣列轉成易於閱讀的單行矩陣字串。
    /// 輸入陣列不會被修改，輸出格式為 <c>[[row1], [row2]]</c>。
    /// </summary>
    /// <param name="matrix">要格式化的非 null 二維整數陣列。</param>
    /// <returns>保留列界線與元素順序的矩陣字串。</returns>
    private static string FormatMatrix(int[][] matrix)
    {
        return $"[{string.Join(", ", matrix.Select(row => $"[{string.Join(", ", row)}]"))}]";
    }

    /// <summary>
    /// 使用暴力法搜尋矩陣，逐列檢查所有元素，不依賴矩陣的排序性。
    /// 輸入須為非 null 且至少包含一個元素的整數矩陣；找到 <paramref name="target"/>
    /// 時回傳 <see langword="true"/>，完整掃描後仍未找到則回傳 <see langword="false"/>。
    /// </summary>
    /// <param name="matrix">至少含一列、一欄的整數矩陣。</param>
    /// <param name="target">要搜尋的目標整數。</param>
    /// <returns>矩陣包含目標值時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
    public bool SearchMatrix(int[][] matrix, int target)
    {
        foreach (int[] row in matrix)
        {
            foreach (int element in row)
            {
                if (element == target)
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// 使用逐列二分搜尋法尋找目標值，利用每一列由左至右非遞減的條件縮小搜尋範圍。
    /// 輸入須為非 null、至少一列一欄且每列皆已排序的矩陣；任一列找到目標值時回傳
    /// <see langword="true"/>，所有列都搜尋完仍未找到則回傳 <see langword="false"/>。
    /// </summary>
    /// <param name="matrix">至少含一列、一欄，且各列非遞減排列的整數矩陣。</param>
    /// <param name="target">要搜尋的目標整數。</param>
    /// <returns>矩陣包含目標值時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
    public bool SearchMatrix_binary(int[][] matrix, int target)
    {
        foreach (int[] row in matrix)
        {
            int index = Search(row, target);
            if (index >= 0)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 在非遞減整數陣列中使用二分搜尋尋找目標值，每次比較後排除不可能的一半區間。
    /// 輸入須為非 null 且已排序的陣列；找到時回傳其中一個相符索引，否則回傳 <c>-1</c>。
    /// </summary>
    /// <param name="nums">依非遞減順序排列的非 null 整數陣列。</param>
    /// <param name="target">要搜尋的目標整數。</param>
    /// <returns>任一相符元素的索引；不存在時回傳 <c>-1</c>。</returns>
    public int Search(int[] nums, int target)
    {
        int low = 0;
        int high = nums.Length - 1;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;
            int num = nums[mid];
            if (num == target)
            {
                return mid;
            }
            else if (num < target)
            {
                // 中間值與其左側都不可能等於更大的 target。
                low = mid + 1;
            }
            else
            {
                // 中間值與其右側都不可能等於更小的 target。
                high = mid - 1;
            }
        }

        return -1;
    }

    /// <summary>
    /// 從右上角開始搜尋，同時利用列與欄皆非遞減的條件：較大時左移排除一欄，
    /// 較小時下移排除一列。輸入須為非 null、至少一列一欄且列欄皆已排序的矩陣；
    /// 找到目標值時回傳 <see langword="true"/>，走出矩陣邊界則回傳
    /// <see langword="false"/>。
    /// </summary>
    /// <param name="matrix">至少含一列、一欄，且各列各欄皆非遞減排列的整數矩陣。</param>
    /// <param name="target">要搜尋的目標整數。</param>
    /// <returns>矩陣包含目標值時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
    public bool SearchMatrix_RightTop(int[][] matrix, int target)
    {
        int i = 0;
        int j = matrix[0].Length - 1;

        while (i < matrix.Length && j >= 0)
        {
            if (matrix[i][j] == target)
            {
                return true;
            }
            else if (matrix[i][j] > target)
            {
                // 當前值下方只會更大，左移即可排除整個目前欄。
                j--;
            }
            else
            {
                // 當前值左側只會更小，下移即可排除整個目前列。
                i++;
            }
        }

        return false;
    }

    /// <summary>
    /// 表示一組固定搜尋案例，包含案例名稱、符合排序條件的矩陣、目標值與預期結果。
    /// </summary>
    /// <param name="Name">顯示於 console 的案例名稱。</param>
    /// <param name="Matrix">至少含一列、一欄，且列欄皆非遞減排列的矩陣。</param>
    /// <param name="Target">要搜尋的目標整數。</param>
    /// <param name="Expected">目標值是否應存在於矩陣中。</param>
    private sealed record SampleCase(string Name, int[][] Matrix, int Target, bool Expected);
}
