namespace leetcode_3227;

class Program
{
    /// <summary>
    /// 3227. Vowels Game in a String
    /// https://leetcode.com/problems/vowels-game-in-a-string/description/?envType=daily-question&envId=2025-09-12
    /// 3227. 字符串元音遊戲
    /// https://leetcode.cn/problems/vowels-game-in-a-string/description/?envType=daily-question&envId=2025-09-12
    ///
    /// 中文題目翻譯：
    /// Alice 與 Bob 在一個字串 s 上玩遊戲，兩人輪流操作，Alice 先手。
    /// - 在 Alice 的回合，她必須從 s 中刪除任意非空的子字串，該子字串必須包含奇數個母音。
    /// - 在 Bob 的回合，他必須從 s 中刪除任意非空的子字串，該子字串必須包含偶數個母音。
    /// 若在某個回合玩家無法進行操作，該玩家即輸掉遊戲。我們假設雙方皆採最佳策略。
    /// 請回傳當 Alice 與 Bob 最佳對弈時，Alice 是否會贏（true 表示 Alice 獲勝，false 表示 Alice 失敗）。
    /// 英文母音為：a, e, i, o, u。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 範例測試資料
        var tests = new string[]
        {
            "abc",     // 包含母音 a -> true
            "rhythm",  // 無母音 -> false
            "bcdex",   // 包含母音 e -> true
            "",        // 空字串 -> false
            "u"        // 單一母音 -> true
        };

        var solver = new Program();
        Console.WriteLine("Vowels Game 測試結果：");
        foreach (var t in tests)
        {
            bool result = solver.DoesAliceWin(t);
            Console.WriteLine($"s=\"{t}\" -> AliceWins: {result}");
        }
    }

    /// <summary>
    /// 判斷 Alice 是否能在此遊戲中獲勝。
    /// </summary>
    /// <remarks>
    /// 一般性的關鍵觀察：
    /// 遊戲的勝負其實只取決於整個字串中母音的總數。
    ///
    /// - 若字串中沒有母音：
    ///   Alice 一開始就無法進行合法的刪除（她必須刪掉一段包含奇數個母音的子字串），因此必輸，回傳 false。
    ///
    /// - 若字串中至少有一個母音：
    ///   Alice 可以先刪掉一個只含單一母音的子字串（例如一個母音字元本身），保證她至少能進行一次合法操作並掌握主導權。
    ///   因此只要母音數 > 0，Alice 即可必勝，回傳 true。
    ///
    /// 結論：僅需檢查字串中是否有母音（a, e, i, o, u）。
    /// 
    /// 如果母音總數是奇數那麼 Alice 一定會贏，因為她可以透過刪除一個母音來讓 Bob 面對偶數個母音的情況。
    /// 如果母音總數是偶數因為 Alice 先手所以她也能贏，因為她可以透過刪除一個母音來讓 Bob 面對奇數個母音的情況。
    /// 偶數 - 奇數 = 奇數
    /// 需要注意母音數量為零的情況，這種情況下 Alice 無法進行任何操作，因此她會輸掉遊戲。
    /// 0 是偶數
    /// </remarks>
    /// <param name="s">輸入字串（小寫英文字母）</param>
    /// <returns>若 Alice 必勝回傳 true，否則回傳 false</returns>
    public bool DoesAliceWin(string s)
    {
        // 定義母音集合
        HashSet<char> vowels = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
        // 遍歷字串，檢查是否有至少一個母音
        foreach (char c in s)
        {
            if (vowels.Contains(c))
            {
                // 找到一個母音就可以直接判斷 Alice 會贏
                return true;
            }
        }
        // 如果整串都沒有母音，Alice 一開始就不能操作 -> Bob 贏
        return false;
    }
}
