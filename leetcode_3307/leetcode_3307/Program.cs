namespace leetcode_3307;

class Program
{
    /// <summary>
    /// 3307. Find the K-th Character in String Game II
    /// https://leetcode.com/problems/find-the-k-th-character-in-string-game-ii/description/
    /// <para>
    /// Alice and Bob are playing a game. Initially, Alice has word = "a".
    ///
    /// You are given a positive integer k and an integer array operations, where operations[i] is the type of the i-th operation.
    ///
    /// Bob asks Alice to perform all operations in order:
    /// - If operations[i] == 0, append a copy of word to itself.
    /// - If operations[i] == 1, change every character in word to its next English letter to generate a new string, and append it to the original word. For example, "c" produces "cd", and "zb" produces "zbac".
    ///
    /// Return the k-th character in word after all operations.
    ///
    /// The character 'z' changes to 'a' in the second operation type.
    ///
    /// Example 1:
    /// Input: k = 5, operations = [0,0,0]
    /// Output: "a"
    /// Explanation: Starting with "a", append "a" to obtain "aa"; append "aa" to obtain "aaaa"; append "aaaa" to obtain "aaaaaaaa".
    ///
    /// Example 2:
    /// Input: k = 10, operations = [0,1,0,1]
    /// Output: "b"
    /// Explanation: Starting with "a", the four operations produce "aa", "aabb", "aabbaabb", and "aabbaabbbbccbbcc" respectively.
    ///
    /// Constraints:
    /// - 1 &lt;= k &lt;= 10^14
    /// - 1 &lt;= operations.length &lt;= 100
    /// - operations[i] is 0 or 1.
    /// - The input guarantees that word has at least k characters after all operations.
    /// </para>
    /// <para>
    /// 3307. 字串遊戲 II 中的第 K 個字元
    /// https://leetcode.cn/problems/find-the-k-th-character-in-string-game-ii/description/
    ///
    /// Alice 與 Bob 正在玩遊戲。起初 Alice 擁有 word = "a"。
    ///
    /// 給定正整數 k 與整數陣列 operations，其中 operations[i] 表示第 i 個操作的類型。
    ///
    /// Bob 要求 Alice 依序執行所有操作：
    /// - 若 operations[i] == 0，將 word 的副本附加到 word 本身。
    /// - 若 operations[i] == 1，將 word 的每個字元變成下一個英文字母以產生新字串，再附加到原始 word。例如，"c" 會產生 "cd"，"zb" 會產生 "zbac"。
    ///
    /// 回傳執行所有操作後 word 的第 k 個字元。
    ///
    /// 在第二種操作中，字元 'z' 會變成 'a'。
    ///
    /// 範例 1：
    /// 輸入：k = 5, operations = [0,0,0]
    /// 輸出："a"
    /// 解釋：從 "a" 開始，依序附加 "a" 得到 "aa"、附加 "aa" 得到 "aaaa"、附加 "aaaa" 得到 "aaaaaaaa"。
    ///
    /// 範例 2：
    /// 輸入：k = 10, operations = [0,1,0,1]
    /// 輸出："b"
    /// 解釋：從 "a" 開始，四次操作依序得到 "aa"、"aabb"、"aabbaabb" 與 "aabbaabbbbccbbcc"。
    ///
    /// 限制條件：
    /// - 1 &lt;= k &lt;= 10^14
    /// - 1 &lt;= operations.length &lt;= 100
    /// - operations[i] 是 0 或 1。
    /// - 輸入保證執行所有操作後 word 至少有 k 個字元。
    /// </para>
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
