namespace leetcode_401;

class Program
{
    /// <summary>
    /// English — Binary Watch (LeetCode 401)
    /// <para>
    /// A binary watch has 4 LEDs on the top to represent the hours (0–11), and 6 LEDs on the bottom to represent the minutes (0–59). Each LED represents a zero or one, with the least significant bit on the right.
    /// Given an integer <c>turnedOn</c> which represents the number of LEDs that are currently on (ignoring AM/PM), return all possible times the watch could represent. You may return the answer in any order.
    /// The hour must not contain a leading zero (e.g. "01:00" is invalid — use "1:00"). The minute must consist of two digits and may contain a leading zero (e.g. "10:02").
    /// </para>
    /// 繁體中文 — 二進位手錶 (LeetCode 401)
    /// <para>
    /// 手錶上方有 4 顆 LED 表示小時（0–11），下方有 6 顆 LED 表示分鐘（0–59）。每顆 LED 表示 0 或 1，最右邊為最低位元。
    /// 給定整數 <c>turnedOn</c> 表示目前亮起的 LED 數量（不考慮上午/下午），回傳手錶可能代表的所有時間，回傳順序不限。
    /// 小時不得有前導零（例如 "01:00" 為無效，應為 "1:00"）；分鐘必須為兩位數，必要時補零（例如 "10:2" 應為 "10:02"）。
    /// </para>
    /// </summary>
    /// <param name="args">命令列參數（未使用）。</param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試案例 1：turnedOn = 1，只有 1 顆 LED 亮起
        Console.WriteLine("=== 測試案例 1：turnedOn = 1 ===");
        IList<string> result1 = program.ReadBinaryWatch(1);
        Console.WriteLine($"方法一（枚舉時分）: [{string.Join(", ", result1)}]");
        IList<string> result1B = program.ReadBinaryWatch2(1);
        Console.WriteLine($"方法二（二進位枚舉）: [{string.Join(", ", result1B)}]");
        IList<string> result1C = program.ReadBinaryWatch3(1);
        Console.WriteLine($"方法三（回溯剪枝）: [{string.Join(", ", result1C)}]");

        // 測試案例 2：turnedOn = 0，沒有 LED 亮起，只有 "0:00"
        Console.WriteLine("\n=== 測試案例 2：turnedOn = 0 ===");
        IList<string> result2 = program.ReadBinaryWatch(0);
        Console.WriteLine($"方法一（枚舉時分）: [{string.Join(", ", result2)}]");
        IList<string> result2B = program.ReadBinaryWatch2(0);
        Console.WriteLine($"方法二（二進位枚舉）: [{string.Join(", ", result2B)}]");
        IList<string> result2C = program.ReadBinaryWatch3(0);
        Console.WriteLine($"方法三（回溯剪枝）: [{string.Join(", ", result2C)}]");

        // 測試案例 3：turnedOn = 2，有 2 顆 LED 亮起
        Console.WriteLine("\n=== 測試案例 3：turnedOn = 2 ===");
        IList<string> result3 = program.ReadBinaryWatch(2);
        Console.WriteLine($"方法一（枚舉時分）: [{string.Join(", ", result3)}]");
        IList<string> result3B = program.ReadBinaryWatch2(2);
        Console.WriteLine($"方法二（二進位枚舉）: [{string.Join(", ", result3B)}]");
        IList<string> result3C = program.ReadBinaryWatch3(2);
        Console.WriteLine($"方法三（回溯剪枝）: [{string.Join(", ", result3C)}]");

        // 測試案例 4：turnedOn = 9，邊界測試（結果應為空）
        Console.WriteLine("\n=== 測試案例 4：turnedOn = 9（邊界） ===");
        IList<string> result4 = program.ReadBinaryWatch(9);
        Console.WriteLine($"方法一（枚舉時分）: [{string.Join(", ", result4)}]");
        IList<string> result4B = program.ReadBinaryWatch2(9);
        Console.WriteLine($"方法二（二進位枚舉）: [{string.Join(", ", result4B)}]");
        IList<string> result4C = program.ReadBinaryWatch3(9);
        Console.WriteLine($"方法三（回溯剪枝）: [{string.Join(", ", result4C)}]");
    }

    /// <summary>
    /// 方法一：枚舉時分（Enumerate Hours and Minutes）
    /// <para>
    /// 解題思路：直接枚舉所有合法的小時 [0, 11] 與分鐘 [0, 59] 組合，
    /// 分別計算小時與分鐘的二進位表示中 1 的個數（即亮燈數），
    /// 若兩者之和恰好等於 <paramref name="turnedOn"/>，則該時間為合法答案。
    /// </para>
    /// <para>
    /// 時間複雜度：O(12 × 60) = O(720)，常數級。
    /// 空間複雜度：O(1)，不計回傳結果的額外空間。
    /// </para>
    /// <example>
    /// <code>
    /// var program = new Program();
    /// IList&lt;string&gt; result = program.ReadBinaryWatch(1);
    /// // result: ["0:01", "0:02", "0:04", "0:08", "0:16", "0:32", "1:00", "2:00", "4:00", "8:00"]
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="turnedOn">目前亮起的 LED 數量（0 ≤ turnedOn ≤ 10）。</param>
    /// <returns>所有合法時間字串的清單。</returns>
    public IList<string> ReadBinaryWatch(int turnedOn)
    {
        IList<string> ans = new List<string>();

        // 枚舉所有小時（0 ~ 11）
        for (int h = 0; h < 12; ++h)
        {
            // 枚舉所有分鐘（0 ~ 59）
            for (int m = 0; m < 60; ++m)
            {
                // 計算小時與分鐘二進位中 1 的個數之和，若等於 turnedOn 則為合法時間
                if (BitCount(h) + BitCount(m) == turnedOn)
                {
                    // 格式化：小時不補零，分鐘補零至兩位
                    ans.Add(h + ":" + (m < 10 ? "0" : "") + m);
                }
            }
        }

        return ans;
    }

    /// <summary>
    /// 計算整數二進位表示中 1 的個數（Hamming Weight / Population Count）。
    /// <para>
    /// 使用位元操作的分治法，將相鄰位元組逐步合併計算，不需要迴圈。
    /// </para>
    /// </summary>
    /// <param name="i">要計算的整數。</param>
    /// <returns>二進位表示中 1 的個數。</returns>
    private static int BitCount(int i)
    {
        // 每 2 位元一組，計算每組中 1 的個數
        i = i - ((i >> 1) & 0x55555555);
        // 每 4 位元一組，合併相鄰的 2 位元組結果
        i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
        // 每 8 位元一組，合併相鄰的 4 位元組結果
        i = (i + (i >> 4)) & 0x0f0f0f0f;
        // 每 16 位元一組
        i = i + (i >> 8);
        // 每 32 位元一組
        i = i + (i >> 16);
        // 取最低 6 位元即為結果（最多 32 個 1）
        return i & 0x3f;
    }

    /// <summary>
    /// 方法二：二進位枚舉（Binary Enumeration）
    /// <para>
    /// 解題思路：將 10 顆 LED 視為一個 10 位元的二進位數，高 4 位元代表小時，低 6 位元代表分鐘。
    /// 枚舉所有 2^10 = 1024 種組合，篩選出位元中 1 的個數恰好等於 <paramref name="turnedOn"/>，
    /// 且小時 &lt; 12、分鐘 &lt; 60 的合法結果。
    /// </para>
    /// <para>
    /// 時間複雜度：O(1024) = O(1)，常數級。
    /// 空間複雜度：O(1)，不計回傳結果的額外空間。
    /// </para>
    /// <example>
    /// <code>
    /// var program = new Program();
    /// IList&lt;string&gt; result = program.ReadBinaryWatch2(1);
    /// // result: ["0:01", "0:02", "0:04", "0:08", "0:16", "0:32", "1:00", "2:00", "4:00", "8:00"]
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="turnedOn">目前亮起的 LED 數量（0 ≤ turnedOn ≤ 10）。</param>
    /// <returns>所有合法時間字串的清單。</returns>
    public IList<string> ReadBinaryWatch2(int turnedOn)
    {
        IList<string> ans = new List<string>();

        // 枚舉所有 10 位元的組合（0 ~ 1023）
        for (int i = 0; i < 1024; ++i)
        {
            // 用位元運算取出高 4 位元（小時）和低 6 位元（分鐘）
            int h = i >> 6;
            int m = i & 63;

            // 檢查小時與分鐘是否在合法範圍，且亮燈數等於 turnedOn
            if (h < 12 && m < 60 && BitCount(i) == turnedOn)
            {
                // 格式化：小時不補零，分鐘補零至兩位
                ans.Add(h + ":" + (m < 10 ? "0" : "") + m);
            }
        }

        return ans;
    }

    /// <summary>
    /// 方法三：回溯剪枝（Backtracking with Pruning）
    /// <para>
    /// 解題思路：將 10 顆 LED 編號 0 ~ 9，其中 LED 0 ~ 3 對應小時的位元值（1, 2, 4, 8），
    /// LED 4 ~ 9 對應分鐘的位元值（1, 2, 4, 8, 16, 32）。
    /// 使用回溯法從 LED 集合中選取恰好 <paramref name="turnedOn"/> 顆亮燈，
    /// 並在遞迴過程中進行剪枝：若目前小時 ≥ 12 或分鐘 ≥ 60，則提前回溯。
    /// </para>
    /// <para>
    /// 時間複雜度：O(C(10, turnedOn))，組合數最多為 C(10,5) = 252。
    /// 空間複雜度：O(turnedOn)，遞迴堆疊深度。
    /// </para>
    /// <example>
    /// <code>
    /// var program = new Program();
    /// IList&lt;string&gt; result = program.ReadBinaryWatch3(1);
    /// // result: ["1:00", "2:00", "4:00", "8:00", "0:01", "0:02", "0:04", "0:08", "0:16", "0:32"]
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="turnedOn">目前亮起的 LED 數量（0 ≤ turnedOn ≤ 10）。</param>
    /// <returns>所有合法時間字串的清單。</returns>
    public IList<string> ReadBinaryWatch3(int turnedOn)
    {
        IList<string> ans = new List<string>();

        // 10 顆 LED 分別對應的數值：前 4 顆是小時位元（1,2,4,8），後 6 顆是分鐘位元（1,2,4,8,16,32）
        int[] hourValues = [1, 2, 4, 8, 0, 0, 0, 0, 0, 0];
        int[] minuteValues = [0, 0, 0, 0, 1, 2, 4, 8, 16, 32];

        // 啟動回溯搜尋，初始小時 = 0、分鐘 = 0，從第 0 顆 LED 開始選取
        Backtrack(ans, hourValues, minuteValues, turnedOn, 0, 0, 0);

        return ans;
    }

    /// <summary>
    /// 回溯遞迴函式：從第 <paramref name="start"/> 顆 LED 開始，選取剩餘需要的亮燈數。
    /// </summary>
    /// <param name="ans">儲存合法結果的清單。</param>
    /// <param name="hourValues">每顆 LED 對小時的貢獻值。</param>
    /// <param name="minuteValues">每顆 LED 對分鐘的貢獻值。</param>
    /// <param name="remaining">剩餘需要點亮的 LED 數量。</param>
    /// <param name="start">本次遞迴開始選取的 LED 索引（避免重複組合）。</param>
    /// <param name="hour">目前累計的小時值。</param>
    /// <param name="minute">目前累計的分鐘值。</param>
    private void Backtrack(
        IList<string> ans,
        int[] hourValues,
        int[] minuteValues,
        int remaining,
        int start,
        int hour,
        int minute)
    {
        // 剪枝：小時超過 11 或分鐘超過 59，此路不通，提前回溯
        if (hour > 11 || minute > 59)
        {
            return;
        }

        // 終止條件：已選完所有需要的 LED，記錄合法時間
        if (remaining == 0)
        {
            ans.Add(hour + ":" + (minute < 10 ? "0" : "") + minute);

            return;
        }

        // 從 start 開始枚舉可選的 LED，確保組合不重複
        for (int i = start; i < 10; ++i)
        {
            // 剪枝：剩餘可選的 LED 數量不足以湊滿 remaining 顆
            if (10 - i < remaining)
            {
                break;
            }

            // 選取第 i 顆 LED，累加其對小時與分鐘的貢獻
            Backtrack(
                ans,
                hourValues,
                minuteValues,
                remaining - 1,
                i + 1,
                hour + hourValues[i],
                minute + minuteValues[i]);
        }
    }
}
