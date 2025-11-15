namespace leetcode_3234;

class Program
{
    static void Main(string[] args)
    {
        // If args are provided we treat them as input; otherwise use a default sample input
        string input;
        if (args != null && args.Length > 0)
        {
            input = string.Join(" ", args);
        }
        else
        {
            // 設定預設輸入 (可依題目需要修改為 JSON / 多個行)
            input = "Alice"; // <- 預設範例，不需要互動輸入
        }

        // 輸出處理結果 (請把 Solve 改為你的題解邏輯)
        var result = Solve(input);
        Console.WriteLine(result);
    }

    // 範例 Solve，會返回已處理的字串，請替換為你的題解
    static string Solve(string input)
    {
        // 假設題目要回傳 "Hello, <name>!" 之類的處理
        return $"Hello, {input}!";
    }
}
