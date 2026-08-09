namespace leetcode_3227;

class Program
{
    /// <summary>
    /// 3227. Vowels Game in a String
    /// https://leetcode.com/problems/vowels-game-in-a-string/description/
    /// <para>
    /// Alice and Bob are playing a game on a string s. They take turns, with Alice playing first:
    /// - On Alice's turn, she must remove any non-empty substring from s that contains an odd number of vowels.
    /// - On Bob's turn, he must remove any non-empty substring from s that contains an even number of vowels.
    ///
    /// The first player unable to make a move loses. Both players play optimally.
    ///
    /// Return true if Alice wins, and false otherwise.
    ///
    /// The English vowels are a, e, i, o, and u.
    ///
    /// Example 1:
    /// Input: s = "leetcoder"
    /// Output: true
    /// Explanation: Alice first deletes [leetco] from [leetco]der, which contains 3 vowels, leaving "der". Bob deletes [d], which contains 0 vowels, leaving "er". Alice deletes [er], which contains 1 vowel. The string is empty on Bob's turn, so Alice wins.
    ///
    /// Example 2:
    /// Input: s = "bbcd"
    /// Output: false
    /// Explanation: Alice has no valid move on her first turn, so she loses.
    ///
    /// Constraints:
    /// - 1 &lt;= s.length &lt;= 10^5
    /// - s consists only of lowercase English letters.
    /// </para>
    /// <para>
    /// 3227. 字串中的母音遊戲
    /// https://leetcode.cn/problems/vowels-game-in-a-string/description/
    ///
    /// Alice 與 Bob 在字串 s 上玩遊戲。兩人輪流操作，由 Alice 先手：
    /// - Alice 的回合必須從 s 移除任意一個含奇數個母音的非空子字串。
    /// - Bob 的回合必須從 s 移除任意一個含偶數個母音的非空子字串。
    ///
    /// 第一個無法操作的玩家輸掉遊戲。兩人都採用最佳策略。
    ///
    /// 若 Alice 獲勝則回傳 true，否則回傳 false。
    ///
    /// 英文母音為 a、e、i、o、u。
    ///
    /// 範例 1：
    /// 輸入：s = "leetcoder"
    /// 輸出：true
    /// 解釋：Alice 先從 [leetco]der 刪除含 3 個母音的 [leetco]，留下 "der"。Bob 刪除含 0 個母音的 [d]，留下 "er"。Alice 刪除含 1 個母音的 [er]。輪到 Bob 時字串已空，因此 Alice 獲勝。
    ///
    /// 範例 2：
    /// 輸入：s = "bbcd"
    /// 輸出：false
    /// 解釋：Alice 第一回合沒有合法操作，因此輸掉遊戲。
    ///
    /// 限制條件：
    /// - 1 &lt;= s.length &lt;= 10^5
    /// - s 只由小寫英文字母組成。
    /// </para>
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
