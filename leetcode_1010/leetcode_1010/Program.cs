namespace leetcode_1010;

class Program
{
    /// <summary>
    /// 1010. Pairs of Songs With Total Durations Divisible by 60
    /// https://leetcode.com/problems/pairs-of-songs-with-total-durations-divisible-by-60/description/
    /// 1010. 總持續時間可被 60 整除的歌曲
    /// https://leetcode.cn/problems/pairs-of-songs-with-total-durations-divisible-by-60/description/
    ///
    /// English:
    /// You are given a list of songs where the i-th song has a duration of time[i] seconds.
    /// Return the number of pairs of songs for which their total duration in seconds is divisible by 60.
    /// Formally, we want the number of indices i, j such that i &lt; j with (time[i] + time[j]) % 60 == 0.
    ///
    /// 繁體中文：
    /// 給定一個歌曲陣列，其中第 i 首歌的長度為 time[i] 秒。
    /// 請回傳所有歌曲配對中，總秒數可被 60 整除的配對數量。
    /// 形式化地說，找出所有 i, j 且 i &lt; j，並且 (time[i] + time[j]) % 60 == 0 的索引對數量。
    /// </summary>
    /// <param name="args">命令列參數。</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var program = new Program();

        // 測試資料 1：LeetCode 官方範例 1
        // 輸入 [30, 20, 150, 100, 40]
        // 配對：(30,150)=180, (20,100)=120, (20,40)=60 → 3 組
        int[] time1 = { 30, 20, 150, 100, 40 };
        Console.WriteLine($"Test1 expect=3");
        Console.WriteLine($"  NumPairsDivisibleBy60   : {program.NumPairsDivisibleBy60((int[])time1.Clone())}");
        Console.WriteLine($"  NumPairsDivisibleBy60_2 : {program.NumPairsDivisibleBy60_2((int[])time1.Clone())}");
        Console.WriteLine($"  NumPairsDivisibleBy60_3 : {program.NumPairsDivisibleBy60_3((int[])time1.Clone())}");

        // 測試資料 2：LeetCode 官方範例 2
        // 輸入 [60, 60, 60]，任兩首都能整除 → C(3,2) = 3 組
        int[] time2 = { 60, 60, 60 };
        Console.WriteLine($"Test2 expect=3");
        Console.WriteLine($"  NumPairsDivisibleBy60   : {program.NumPairsDivisibleBy60((int[])time2.Clone())}");
        Console.WriteLine($"  NumPairsDivisibleBy60_2 : {program.NumPairsDivisibleBy60_2((int[])time2.Clone())}");
        Console.WriteLine($"  NumPairsDivisibleBy60_3 : {program.NumPairsDivisibleBy60_3((int[])time2.Clone())}");

        // 測試資料 3：邊界 - 只有一首歌，無法配對 → 0
        int[] time3 = { 30 };
        Console.WriteLine($"Test3 expect=0");
        Console.WriteLine($"  NumPairsDivisibleBy60   : {program.NumPairsDivisibleBy60((int[])time3.Clone())}");
        Console.WriteLine($"  NumPairsDivisibleBy60_2 : {program.NumPairsDivisibleBy60_2((int[])time3.Clone())}");
        Console.WriteLine($"  NumPairsDivisibleBy60_3 : {program.NumPairsDivisibleBy60_3((int[])time3.Clone())}");

        // 測試資料 4：含餘數 30 的配對
        // [30, 30, 30, 30] → C(4,2) = 6 組
        int[] time4 = { 30, 30, 30, 30 };
        Console.WriteLine($"Test4 expect=6");
        Console.WriteLine($"  NumPairsDivisibleBy60   : {program.NumPairsDivisibleBy60((int[])time4.Clone())}");
        Console.WriteLine($"  NumPairsDivisibleBy60_2 : {program.NumPairsDivisibleBy60_2((int[])time4.Clone())}");
        Console.WriteLine($"  NumPairsDivisibleBy60_3 : {program.NumPairsDivisibleBy60_3((int[])time4.Clone())}");

        // 測試資料 5：沒有任何配對可整除
        int[] time5 = { 1, 2, 3, 4, 5 };
        Console.WriteLine($"Test5 expect=0");
        Console.WriteLine($"  NumPairsDivisibleBy60   : {program.NumPairsDivisibleBy60((int[])time5.Clone())}");
        Console.WriteLine($"  NumPairsDivisibleBy60_2 : {program.NumPairsDivisibleBy60_2((int[])time5.Clone())}");
        Console.WriteLine($"  NumPairsDivisibleBy60_3 : {program.NumPairsDivisibleBy60_3((int[])time5.Clone())}");
    }

    /// <summary>
    /// 解法一：一次遍歷 + 餘數計數表（邊走邊配對）
    ///
    /// 解題說明：
    /// 1. 兩數相加能被 60 整除，等價於兩數「對 60 取餘數」之和為 0 或 60。
    ///    也就是 (a % 60 + b % 60) % 60 == 0。
    /// 2. 將所有可能餘數（0 ~ 59）各自出現次數統計在長度 60 的陣列中。
    /// 3. 採用「邊遍歷、邊查詢、邊累加、最後才更新計數」的作法：
    ///    - 對目前這首歌 time[i]，計算餘數 remainder = time[i] % 60。
    ///    - 其所需配對餘數為 complement = (60 - remainder) % 60。
    ///      這裡外層再取一次 % 60，是為了讓 remainder == 0 的情況 complement 也變回 0，
    ///      避免越界到索引 60。
    ///    - 先把 remainderCounts[complement] 累加到答案，再把自己的餘數登記到計數表，
    ///      就不會把「自己」錯誤地和「自己」配成一對。
    ///
    /// 時間複雜度：O(n)；空間複雜度：O(1)（固定長度 60 的陣列）。
    /// </summary>
    /// <param name="time">歌曲長度陣列。</param>
    /// <returns>秒數和可被 60 整除的配對數量。</returns>
    public int NumPairsDivisibleBy60(int[] time)
    {
        // 配對總數
        int pairCount = 0;

        // remainderCounts[r] 代表目前已遍歷過、餘數為 r 的歌曲數量
        int[] remainderCounts = new int[60];

        for (int i = 0; i < time.Length; i++)
        {
            // 當前歌曲對 60 取餘數
            int remainder = time[i] % 60;

            // 需要的互補餘數；當 remainder == 0 時，complement 也要是 0，
            // 用 (60 - remainder) % 60 可同時處理 remainder == 0 的特例。
            int complement = (60 - remainder) % 60;

            // 先查詢：目前已有多少首歌與這首可以配對
            pairCount += remainderCounts[complement];

            // 再登記：把自己放入計數表，提供給後續的歌曲配對
            remainderCounts[remainder]++;
        }

        return pairCount;
    }

    /// <summary>
    /// 解法二：一次遍歷 + 餘數計數表（與解法一等價，以 if/else 顯式處理餘數 0）
    ///
    /// 解題說明：
    /// 1. 觀察同解法一：只看 time[i] % 60 即可決定能否配對。
    /// 2. 與解法一不同之處：餘數為 0 的互補餘數仍是 0，若直接寫 remainderCounts[60 - 0]
    ///    會越界，因此以 if/else 分成兩種情況：
    ///    - remainder != 0：要找的是 remainderCounts[60 - remainder]。
    ///    - remainder == 0：要找的是 remainderCounts[0]（同樣餘數 0 彼此配對）。
    /// 3. 同樣採「先查詢再登記」的順序，避免自配對。
    ///
    /// 時間複雜度：O(n)；空間複雜度：O(1)。
    /// </summary>
    /// <param name="time">歌曲長度陣列。</param>
    /// <returns>秒數和可被 60 整除的配對數量。</returns>
    public int NumPairsDivisibleBy60_2(int[] time)
    {
        // 配對總數
        int pairCount = 0;

        // 餘數計數表：remainderCounts[r] = 已遍歷且餘數為 r 的歌曲數
        int[] remainderCounts = new int[60];

        for (int i = 0; i < time.Length; i++)
        {
            // 取餘數（只看餘數即可決定配對）
            int remainder = time[i] % 60;

            if (remainder != 0)
            {
                // 非 0 餘數：配對對象的餘數為 60 - remainder
                pairCount += remainderCounts[60 - remainder];
            }
            else
            {
                // 餘數為 0：配對對象同樣為餘數 0，若寫 60 - 0 會越界
                pairCount += remainderCounts[0];
            }

            // 登記自己
            remainderCounts[remainder]++;
        }

        return pairCount;
    }

    /// <summary>
    /// 解法三：組合數學（先統計、後計算）
    ///
    /// 解題說明：
    /// 1. 第一輪遍歷：僅統計每個餘數出現次數 cnt[0..59]。
    /// 2. 第二輪用組合公式計算配對數，依餘數分為三類：
    ///    (a) 餘數 0：兩首餘數 0 的歌相加仍整除，於 cnt[0] 首中取 2，
    ///        共 C(cnt[0], 2) = cnt[0] × (cnt[0] - 1) / 2 組。
    ///    (b) 餘數 30：同理，30 + 30 = 60 整除，共 C(cnt[30], 2) 組。
    ///    (c) 餘數 i ∈ [1, 29]：配對對象為 60 - i，共 cnt[i] × cnt[60 - i] 組。
    ///        i ∈ [31, 59] 會在此處以 60 - i ∈ [1, 29] 被「對稱」計到，
    ///        因此不再重複列入，避免重覆計算。
    /// 3. 結果可能超過 int 範圍，中間以 long 累加後再轉回 int。
    ///
    /// 時間複雜度：O(n + 60) = O(n)；空間複雜度：O(1)。
    /// </summary>
    /// <param name="time">歌曲長度陣列。</param>
    /// <returns>秒數和可被 60 整除的配對數量。</returns>
    public int NumPairsDivisibleBy60_3(int[] time)
    {
        // 餘數直方圖：remainderCounts[r] = 餘數 r 的歌曲數量
        int[] remainderCounts = new int[60];

        // 第一輪：統計所有歌曲的餘數分佈
        foreach (int t in time)
        {
            remainderCounts[t % 60]++;
        }

        // 累加器使用 long，避免 cnt[i] × cnt[60 - i] 中途溢位
        long pairCount = 0;

        // 餘數 1 ~ 29 與其互補餘數 59 ~ 31 配對
        for (int i = 1; i < 30; i++)
        {
            pairCount += (long)remainderCounts[i] * remainderCounts[60 - i];
        }

        // 加上餘數 0 彼此的 C(n,2)，以及餘數 30 彼此的 C(n,2)
        pairCount += (long)remainderCounts[0] * (remainderCounts[0] - 1) / 2
                   + (long)remainderCounts[30] * (remainderCounts[30] - 1) / 2;

        return (int)pairCount;
    }
}
