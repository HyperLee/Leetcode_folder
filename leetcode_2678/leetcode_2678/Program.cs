namespace leetcode_2678;

class Program
{
    /// <summary>
    /// 2678. Number of Senior Citizens
    /// https://leetcode.com/problems/number-of-senior-citizens/description/
    ///
    /// English:
    /// You are given a 0-indexed array of strings named details. Each string has
    /// length 15 and stores one passenger's ten-digit phone number, one gender
    /// character, two age digits, and two seat-number digits, in that order.
    /// Return the number of passengers who are strictly more than 60 years old.
    ///
    /// 2678. 老人的數目
    /// https://leetcode.cn/problems/number-of-senior-citizens/description/
    ///
    /// 繁體中文：
    /// 給定索引從 0 開始的字串陣列 details。每個長度為 15 的字串依序儲存
    /// 乘客的十位數電話號碼、一個性別字元、兩位數年齡與兩位數座位號碼。
    /// 請回傳年齡嚴格大於 60 歲的乘客人數。
    /// </summary>
    /// <remarks>
    /// 程式進入點。建立固定測試案例，逐一呼叫解法並比較預期與實際結果；
    /// 若任一案例失敗，程序會以非零結束碼結束。
    /// </remarks>
    /// <param name="args">命令列參數；此範例不使用。</param>
    static void Main(string[] args)
    {
        string[] upperBoundDetails = Enumerable.Range(0, 100)
            .Select(index =>
            {
                char gender = (index % 3) switch
                {
                    0 => 'M',
                    1 => 'F',
                    _ => 'O'
                };

                return $"{index:D10}{gender}{index:D2}{index:D2}";
            })
            .ToArray();

        (string Name, string[] Details, int Expected, string InputDescription)[] testCases =
        [
            (
                "官方範例一",
                ["7868190130M7522", "5303914400F9211", "9273338290F4010"],
                2,
                "[7868190130M7522, 5303914400F9211, 9273338290F4010]"),
            (
                "官方範例二",
                ["1313579440F2036", "2921522980M5644"],
                0,
                "[1313579440F2036, 2921522980M5644]"),
            ("年齡恰好 60", ["0000000000M6000"], 0, "[0000000000M6000]"),
            ("年齡恰好 61", ["0000000001F6101"], 1, "[0000000001F6101]"),
            ("最小年齡 00", ["0000000002O0002"], 0, "[0000000002O0002]"),
            ("最大年齡 99", ["0000000003M9903"], 1, "[0000000003M9903]"),
            (
                "性別不影響年齡判斷",
                ["0000000004M5904", "0000000005F6005", "0000000006O6106"],
                1,
                "[0000000004M5904, 0000000005F6005, 0000000006O6106]"),
            (
                "100 筆上限，年齡 00 到 99",
                upperBoundDetails,
                39,
                "100 passenger records with ages 00 through 99")
        ];

        int passed = 0;

        Console.WriteLine("LeetCode 2678 - Number of Senior Citizens");
        Console.WriteLine();

        foreach ((string name, string[] details, int expected, string inputDescription) in testCases)
        {
            passed += RunTestCase(name, details, expected, inputDescription) ? 1 : 0;
        }

        Console.WriteLine($"Summary: {passed}/{testCases.Length} checks passed.");

        if (passed != testCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 逐筆讀取乘客資料中索引 11、12 的年齡數字，計算年齡嚴格大於 60 歲的人數。
    /// 輸入必須符合題目契約：陣列長度為 1 到 100，每筆資料長度為 15，且年齡欄位為兩個數字字元。
    /// </summary>
    /// <param name="details">依題目格式編碼的乘客資料陣列。</param>
    /// <returns>年齡嚴格大於 60 歲的乘客人數。</returns>
    public static int CountSeniors(string[] details)
    {
        int seniorCount = 0;

        foreach (string detail in details)
        {
            // 年齡固定在索引 11、12；直接轉換兩個數字字元可避免建立子字串。
            int age = ((detail[11] - '0') * 10) + (detail[12] - '0');

            if (age > 60)
            {
                seniorCount++;
            }
        }

        return seniorCount;
    }

    /// <summary>
    /// 執行單一固定案例，呼叫老人數量解法並比較預期與實際結果。
    /// 輸入為符合題目格式的乘客資料；此方法輸出案例明細並回傳是否通過。
    /// </summary>
    /// <param name="name">顯示於主控台的案例名稱。</param>
    /// <param name="details">每筆長度為 15 的乘客資料。</param>
    /// <param name="expected">預期的老人數量。</param>
    /// <param name="inputDescription">供主控台顯示的精簡輸入說明。</param>
    /// <returns>實際結果等於預期結果時為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
    private static bool RunTestCase(
        string name,
        string[] details,
        int expected,
        string inputDescription)
    {
        int actual = CountSeniors(details);
        bool passed = actual == expected;

        Console.WriteLine($"[{(passed ? "PASS" : "FAIL")}] {name}");
        Console.WriteLine($"  Input:    {inputDescription}");
        Console.WriteLine($"  Expected: {expected}");
        Console.WriteLine($"  Actual:   {actual}");
        Console.WriteLine();

        return passed;
    }
}