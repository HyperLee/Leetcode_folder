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

    /// <summary>
    /// 解法二：透過偵測 <c>'1' → '0'</c> 的轉換來辨識零段起點，並在每個零段的第一個零處
    /// 將目前已見的 '1' 數量累加到答案中。
    ///
    /// 演算法直覺：每一段連續的零（zero segment）能讓其左側所有的車（'1'）各執行一次向右移動，
    /// 因此對於每一段零，只需把該段左側已見的 '1' 數量加一次到總操作數即可。
    /// 本方法在掃描時維護一個計數器來累計已見的 '1'，當遇到字元由 '1' 轉為 '0'（即零段開始）時，
    /// 把該計數器加到結果中。
    /// </summary>
    /// <param name="s">輸入二元字串，應僅包含字元 '0' 或 '1'（若為 <c>null</c>，呼叫端會發生例外）。</param>
    /// <returns>回傳可執行的最大操作次數（int）。</returns>
    /// <remarks>
    /// - 時間複雜度：O(n)，對字串做一次線性掃描。
    /// - 額外空間：此實作以 <c>ToCharArray()</c> 建立字元陣列，為 O(n) 額外空間；若改為直接以 <c>s[i]</c> 存取，可降為 O(1)。
    /// - 注意事項：目前方法未檢查非法字元，若輸入可能包含非 '0'/'1' 字元，應加入輸入驗證並在遇到非法字元時拋出 <c>ArgumentException</c>。
    /// </remarks>
    public int MaxOperations2(string s)
    {
        // 注意：未對 null 做明確檢查，呼叫端若傳入 null 會導致 NullReferenceException。
        // 保持原本實作，僅加入說明性註解（如需可在此加入：if (s is null) throw new ArgumentNullException(nameof(s));）。

        // 將字串轉為字元陣列以加快索引存取（此步會產生 O(n) 的額外空間）
        char[] arr = s.ToCharArray();

        // ans：累計的最大操作次數；cnt1：至目前位置為止已見的 '1' 數量
        int ans = 0;
        int cnt1 = 0;

        // 從左到右掃描字元陣列
        for (int i = 0; i < arr.Length; i++)
        {
            // 如果當前字元為 '1'，把它加入已見的 '1' 數量
            if (s[i] == '1')
            {
                cnt1++;
            }
            // 如果當前字元為 '0' 且前一字元為 '1'，代表找到一段零的開始（1 -> 0），
            // 對每一段零只需將左側已見的 '1' 數量加一次到答案（不論該零段長度為何）
            else if (i > 0 && arr[i - 1] == '1')
            {
                ans += cnt1;
            }
        }

        // 回傳計算結果。若有溢位疑慮，可將 ans 與 cnt1 改為 long。
        return ans;
    }
}
