namespace leetcode_3307;

class Program
{
    /// <summary>
    /// 3307. Find the K-th Character in String Game II
    /// https://leetcode.com/problems/find-the-k-th-character-in-string-game-ii/description/?envType=daily-question&envId=2025-07-04
    /// 3307. 找出第 K 個字符 II
    /// https://leetcode.cn/problems/find-the-k-th-character-in-string-game-ii/description/?envType=daily-question&envId=2025-07-04
    /// 
    /// 題目描述（繁體中文）：
    /// Alice 和 Bob 正在玩一個遊戲。最初，Alice 有一個字串 word = "a"。
    /// 你會得到一個正整數 k，還有一個整數陣列 operations，其中 operations[i] 代表第 i 次操作的型別。
    /// 現在 Bob 會要求 Alice 依序執行所有操作：
    ///   - 如果 operations[i] == 0，則將 word 複製一份並接在原本字串後面。
    ///   - 如果 operations[i] == 1，則將 word 中每個字元變成英文字母的下一個字元，然後將這個新字串接在原本字串後面。例如對 "c" 執行會得到 "cd"，對 "zb" 執行會得到 "zbac"。
    /// 請回傳執行所有操作後，word 的第 k 個字元。
    /// 注意：字元 'z' 變換後會變成 'a'。
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        // 測試資料
        var program = new Program();
        int[] operations1 = { 0, 1, 0, 1 };
        long k1 = 10;
        Console.WriteLine($"KthCharacter({k1}, [0,1,0,1]) = {program.KthCharacter(k1, operations1)}");
        Console.WriteLine($"KthCharacter_Math({k1}, [0,1,0,1]) = {program.KthCharacter_Math(k1, operations1)}");

        int[] operations2 = { 1, 1, 0, 0, 1 };
        long k2 = 20;
        Console.WriteLine($"KthCharacter({k2}, [1,1,0,0,1]) = {program.KthCharacter(k2, operations2)}");
        Console.WriteLine($"KthCharacter_Math({k2}, [1,1,0,0,1]) = {program.KthCharacter_Math(k2, operations2)}");
    }


    /// <summary>
    /// 解題說明：
    /// 本方法用於找出經過一系列操作後的第 k 個字符。每次操作有兩種型別：
    /// 0 表示將字串複製接在原字串後，1 表示將字串每個字元進行字母位移後接在原字串後。
    /// 透過觀察 k 的二進位結構，反推 k 屬於哪一次操作產生的區段，並根據 operations 陣列決定是否累加字母位移次數。
    /// 流程：
    /// 1. 以 while 迴圈反推 k 的來源，直到回到最初的 'a'。
    /// 2. 每次根據 k 的二進位最高位，判斷 k 屬於哪次操作，並根據 operations 決定是否累加。
    /// 3. 最終根據累加次數計算對應字母。
    /// 
    /// ref:https://leetcode.cn/problems/find-the-k-th-character-in-string-game-ii/solutions/3708679/zhao-chu-di-k-ge-zi-fu-ii-by-leetcode-so-kx1d/?envType=daily-question&envId=2025-07-04
    /// 
    /// </summary>
    /// <param name="k">查詢的字符位置（1-indexed）</param>
    /// <param name="operations">操作型別陣列，0 為複製，1 為字母位移</param>
    /// <returns>第 k 個字符</returns>
    public char KthCharacter(long k, int[] operations)
    {
        int res = 0; // 記錄字母位移次數
        int times = 0; // 當前操作次數

        // 反推 k 的來源，直到回到最初的 'a'
        while (k != 1)
        {
            // 取得 k 的二進位最高位（即 log2(k)）
            times = (int)Math.Log(k, 2);

            // 若 k 剛好是 2 的 times 次方，則屬於前一個操作區段
            if (k == (1L << times))
            {
                times--;
            }

            // 反推 k 在前一個區段的位置
            k = k - (1L << times);

            // 若該次操作為字母位移，則累加
            if (operations[times] != 0)
            {
                res++;
            }
        }

        // 回傳最終字母，考慮 26 個字母循環
        return (char)('a' + (res % 26));
    }


    /// <summary>
    /// 解法二：利用數學與二進位思維優化查找第 k 個字符。
    /// 
    /// 解題說明：
    /// 本方法不直接建立整個字串，而是透過 k 的二進位結構，判斷每一位對應的操作是否影響最終結果。
    /// 具體來說，將 k 轉為 0-indexed 後，從最高位元往下檢查，若該位為 1，則代表第 i 次操作產生的區段，
    /// 若該操作為字母位移則累加。最終將累加次數對 26 取餘，回傳對應字母。
    /// 此法大幅提升效率，適合處理大數據量情境。
    /// </summary>
    /// <param name="k">查詢的字符位置（1-indexed）</param>
    /// <param name="operations">操作型別陣列，0 為複製，1 為字母位移</param>
    /// <returns>第 k 個字符</returns>
    public char KthCharacter_Math(long k, int[] operations)
    {
        int res = 0; // 累計字母位移次數
        k--; // 轉為 0-indexed，方便二進位運算

        // 從最高位元開始檢查 k 的每一位
        for (int i = (int)Math.Log(k, 2); i >= 0; i--)
        {
            // 若第 i 位為 1，代表第 i 次操作產生的區段
            if (((k >> i) & 1) == 1)
            {
                // 若該次操作為字母位移，則累加
                res += operations[i];
            }
        }

        // 回傳最終字母，考慮 26 個字母循環
        return (char)('a' + (res % 26));
    }
    
}
