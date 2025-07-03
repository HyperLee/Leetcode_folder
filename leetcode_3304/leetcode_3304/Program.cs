namespace leetcode_3304;

class Program
{
    /// <summary>
    /// 3304. Find the K-th Character in String Game I
    /// https://leetcode.com/problems/find-the-k-th-character-in-string-game-i/description/?envType=daily-question&envId=2025-07-03
    /// 3304. 找出第 K 個字符 I
    /// https://leetcode.cn/problems/find-the-k-th-character-in-string-game-i/description/?envType=daily-question&envId=2025-07-03
    /// 
    /// 題目描述（繁體中文翻譯）：
    /// Alice 和 Bob 正在玩一個遊戲。最初，Alice 有一個字串 word = "a"。
    /// 你給定一個正整數 k。
    /// 現在 Bob 會要求 Alice 永遠執行以下操作：
    /// 將 word 中的每個字元變為英文字母表中的下一個字元，然後將其附加到原始 word 之後。
    /// 例如，對 "c" 執行操作會生成 "cd"，對 "zb" 執行操作會生成 "zbac"。
    /// 返回經過足夠多次操作後，word 至少有 k 個字元時的第 k 個字元。
    /// 注意：字元 'z' 變為 'a'。
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 題目範例測試資料
        var prog = new Program();
        int[] testCases = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 26, 27, 52, 53, 100 };
        Console.WriteLine("數學反推法與 bitCount 高效法對比：");
        foreach (var k in testCases)
        {
            char result1 = prog.KthCharacter(k);
            char result2 = prog.KthCharacterBitCount(k);
            Console.WriteLine($"k = {k}, 數學反推法: {result1}, bitCount 高效法: {result2}");
        }
    }


    /// <summary>
    /// 根據題目規則，計算第 k 個字符。
    /// 解題思路：
    /// 每次操作後字串長度倍增，且後半段是前半段每個字元+1（z 變 a）。
    /// 反推第 k 個字元來自哪一層的第 1 個字元，統計經過幾次加一操作。
    /// 
    /// ref:https://leetcode.cn/problems/find-the-k-th-character-in-string-game-i/solutions/3708678/zhao-chu-di-k-ge-zi-fu-i-by-leetcode-sol-9epa/?envType=daily-question&envId=2025-07-03
    /// 
    /// 本解法利用反推直接求出第 k 個字元。
    /// </summary>
    /// <param name="k">要求的第 k 個字元（1-indexed）</param>
    /// <returns>第 k 個字元</returns>
    public char KthCharacter(int k)
    {
        // res 統計經過幾次加一操作
        int res = 0;
        int t = 0;
        // 反推直到回到第 1 個字元
        while (k != 1)
        {
            // t 為 k 的最高位 2 的冪次
            // 問：2 的多少次方(t) = k？
            t = (int)Math.Log(k, 2);

            // 判斷 k 是否剛好是 2 的冪
            if ((1 << t) == k)
            {
                // 表這個位置剛好是新一層字串的開頭，因此要將 t 減一，回到上一層。
                t--;
            }

            // k 減去對應的 2^t，回溯到前一層對應位置
            k = k - (1 << t);
            res++;
        }
        
        // 'a' 經過 res 次加一取模 26
        return (char)('a' + (res % 26));
    }

    /// <summary>
    /// 進階寫法：直接利用 k-1 的二進位 1 的個數，計算第 k 個字元。
    /// 
    /// ref:https://leetcode.cn/problems/find-the-k-th-character-in-string-game-i/solutions/2934326/o1-zuo-fa-yi-xing-dai-ma-jie-jue-pythonj-zgqh/?envType=daily-question&envId=2025-07-03
    /// 
    /// 本解法主要是求出經過幾次「+1」操作
    /// </summary>
    /// <param name="k">要求的第 k 個字元（1-indexed）</param>
    /// <returns>第 k 個字元</returns>
    public char KthCharacterBitCount(int k)
    {
        int count = CountBits(k - 1);
        return (char)('a' + (count % 26));
    }


    /// <summary>
    /// 輔助函式：計算一個整數的二進位 1 的個數
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    private int CountBits(int n)
    {
        int count = 0;
        while (n != 0)
        {
            count += n & 1;
            n >>= 1;
        }
        return count;
    }
}
