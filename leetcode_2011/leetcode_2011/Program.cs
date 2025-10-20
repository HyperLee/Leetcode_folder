namespace leetcode_2011;

class Program
{
    /// <summary>
    /// 2011. Final Value of Variable After Performing Operations
    /// https://leetcode.com/problems/final-value-of-variable-after-performing-operations/description/?envType=daily-question&envId=2025-10-20
    /// 2011. 執行操作後的變數值
    /// https://leetcode.cn/problems/final-value-of-variable-after-performing-operations/description/?envType=daily-question&envId=2025-10-20
    ///
    /// 題目描述 (中文):
    /// 有一種程式語言只有四種操作和一個變數 X：
    /// ++X 與 X++ 將變數 X 的值增加 1；
    /// --X 與 X-- 將變數 X 的值減少 1。
    /// 起始時 X 的值為 0。
    /// 給定一個字串陣列 operations，包含一系列操作；執行所有操作後，回傳 X 的最終值。
    /// </summary>
    /// <param name="args">命令列參數（未使用）</param>
    static void Main(string[] args)
    {
        // 範例測試：
        string[] ops1 = new string[] { "--X", "X++", "X++" }; // 預期結果 1
        string[] ops2 = new string[] { "++X", "++X", "X++" }; // 預期結果 3
        string[] ops3 = new string[] { "X--", "--X", "++X", "X++", "--X" }; // 預期結果 -1

        var program = new Program();
        Console.WriteLine($"Result1: {program.FinalValueAfterOperations(ops1)}");
        Console.WriteLine($"Result2: {program.FinalValueAfterOperations(ops2)}");
        Console.WriteLine($"Result3: {program.FinalValueAfterOperations(ops3)}");
    }

    /// <summary>
    /// 模擬題目要求：初始時令 x = 0，遍歷字串陣列 <paramref name="operations"/>，
    /// 當遇到 "++X" 或 "X++" 時，x 加 1；否則（"--X" 或 "X--"）x 減 1。
    /// 回傳執行所有操作後的最終 x 值。
    ///
    /// 解題說明：直接模擬即可。只需檢查每個操作字串中是否包含 '+' 字元，
    /// 若有則累加，否則扣除。由於操作字串固定長度（長度為 3），
    /// 或可根據字元比較精確判斷，但使用 Contains('+') 足以且簡潔。
    ///
    /// 時間複雜度：O(n)，n = operations.Length。每個字串長度恆定，判斷為 O(1)。
    /// 空間複雜度：O(1)，只使用常數額外空間。
    ///
    /// 邊界情況：
    /// - operations 為 null 或長度為 0：回傳 0。
    /// - 操作字串若非預期格式（例如空字串），此實作會將其視為減 1（因為沒有 '+'），
    ///   若需嚴格驗證應額外檢查字串內容並丟出例外或忽略無效輸入。
    /// </summary>
    /// <param name="operations">操作字串陣列（例如 "++X", "X--"）</param>
    /// <returns>執行完所有操作後的變數 x 的最終值</returns>
    public int FinalValueAfterOperations(string[] operations)
    {
        // 防禦性檢查
        if (operations is null || operations.Length == 0)
        {
            return 0;
        }

        int x = 0;

        foreach (string operation in operations)
        {
            // 若操作包含 '+'，則視為遞增，否則遞減
            // 因為題目限定只有四種操作，這樣的檢查是安全且快速的
            if (!string.IsNullOrEmpty(operation) && operation.Contains('+'))
            {
                x++;
            }
            else
            {
                x--;
            }
        }

        return x;
    }
}
