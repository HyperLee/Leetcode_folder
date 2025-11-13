using System;

namespace leetcode_3228;

class Program
{
    /// <summary>
    /// 3228. Maximum Number of Operations to Move Ones to the End
    /// 題目鏈結：https://leetcode.cn/problems/maximum-number-of-operations-to-move-ones-to-the-end/
    ///
    /// 解題說明：使用貪心 + 計數策略，從左到右掃描字串並維護已遇到的 '1' 數量。
    /// 每當遇到一段連續的 '0' 時，該零段可以讓之前的每一個 '1' 各執行一次向右移動，
    /// 因此把目前已見的 '1' 數量累加到答案。從左往右的貪心可以讓先出現的 '1' 被更多後續零段 "利用"，
    /// 從而最大化操作次數。
    /// </summary>
    /// <param name="args">命令列參數（未使用）</param>
    static void Main(string[] args)
    {
        // 加入範例測試資料，並印出每個測試的結果
        var program = new Program();
        string[] tests = new string[]
        {
            "010010",
            "0",
            "1",
            "10",
            "01",
            "0011",
            "",
            "101010",
        };

        foreach (var t in tests)
        {
            try
            {
                int result = program.MaxOperations(t);
                Console.WriteLine($"s = \"{t}\", MaxOperations = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"s = \"{t}\", Exception: {ex.GetType().Name} - {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 計算將所有 '1' 移動到字串末端能執行的最大操作次數（輸入為二元字串）。
    /// 採用從左到右的貪心掃描：維護已見的 '1' 數量，遇到每一段連續的 '0' 時，
    /// 把已見的 '1' 數量累加到結果中。
    /// </summary>
    /// <param name="s">輸入二元字串（只包含 '0' 與 '1'）。若為 <c>null</c>，會拋出 <see cref="ArgumentNullException"/>。</param>
    /// <returns>回傳可執行的最大操作次數（int）。</returns>
    /// <exception cref="ArgumentNullException">當 <paramref name="s"/> 為 null 時拋出。</exception>
    /// <remarks>時間複雜度：O(n)。額外空間：O(1)。</remarks>
    public int MaxOperations(string s)
    {
        if (s is null) throw new ArgumentNullException(nameof(s));

        // 已遇到的 '1' 數量（會被後續的每一段零所使用）
        int onesCount = 0;
        // 累計的最大操作次數
        int operations = 0;
        // 當前掃描索引
        int index = 0;

        // 從左到右掃描字串
        while (index < s.Length)
        {
            if (s[index] == '0')
            {
                // 跳過同一段連續的零（索引移到該零段的最後一個零）
                while (index + 1 < s.Length && s[index + 1] == '0')
                {
                    index++;
                }
                // 每一段零都能讓先前的每一個 '1' 各執行一次向右移動
                operations += onesCount;
            }
            else
            {
                // 遇到 '1'，將其計入，以供未來遇到的零段使用
                onesCount++;
            }

            // 前進到下一個位置
            index++;
        }

        return operations;
    }
}
