namespace leetcode_424;

class Program
{
    /// <summary>
    /// 424. Longest Repeating Character Replacement
    /// https://leetcode.com/problems/longest-repeating-character-replacement/description/?envType=problem-list-v2&envId=oizxjoit
    /// 424. 替换后的最长重复字符
    /// https://leetcode.cn/problems/longest-repeating-character-replacement/description/
    /// 
    /// 解題概念：
    /// 使用滑動窗口 (sliding window) 的方法來解決問題。
    /// 我們需要找到一個子字串，該子字串可以通過最多 k 次替換操作將其變成由相同字符組成的最長子字串。
    /// 核心邏輯是維護一個窗口，窗口內的字符可以通過 k 次替換形成一個有效的子字串。
    /// 當窗口內的字符數量超過 maxCount + k 時，縮小窗口。
    /// 
    /// 時間複雜度：O(n)，其中 n 是字串的長度。
    /// 空間複雜度：O(1)，因為我們只使用了一個固定大小的陣列來記錄字符頻率。
    /// 
    /// 這種解法的巧妙之處在於維護maxCount變數時，只增不減。即使在左指針移動時，對應的字符計數減少可能影響最大出現次數，程式碼也沒有重新計算maxCount。
    /// 這是因為在窗口內的字符數量不會減少，只有當窗口大小超過maxCount + k時才會縮小窗口。這樣可以保證maxCount始終是正確的。
    /// 對於這個問題，我們只關心找到最長的有效子字串，而不需要保證每個窗口都是精確最優的。
    /// </summary>
    /// <param name="args"></param> 
    static void Main(string[] args)
    {
        // 測試資料
        string test1 = "AABABBA";
        int k1 = 1;
        Console.WriteLine($"Input: s = \"{test1}\", k = {k1}");
        Console.WriteLine($"Output: {new Program().CharacterReplacement(test1, k1)}"); // 預期輸出: 4

        string test2 = "ABAB";
        int k2 = 2;
        Console.WriteLine($"Input: s = \"{test2}\", k = {k2}");
        Console.WriteLine($"Output: {new Program().CharacterReplacement(test2, k2)}"); // 預期輸出: 4

        string test3 = "AAAA";
        int k3 = 2;
        Console.WriteLine($"Input: s = \"{test3}\", k = {k3}");
        Console.WriteLine($"Output: {new Program().CharacterReplacement(test3, k3)}"); // 預期輸出: 4

        string test4 = "AABABBA";
        int k4 = 2;
        Console.WriteLine($"Input: s = \"{test4}\", k = {k4}");
        Console.WriteLine($"Output: {new Program().CharacterReplacement(test4, k4)}"); // 預期輸出: 5
    }

    /// <summary>
    /// 計算替換後的最長重複字符子字串長度。
    /// 
    /// </summary>
    /// <param name="s">輸入字串</param>
    /// <param name="k">最多允許替換的次數</param>
    /// <returns>最長重複字符子字串的長度</returns> 
    public int CharacterReplacement(string s, int k)
    {
        // 步驟 1: 獲取字串長度並處理邊界情況
        int len = s.Length;
        if (len < 2)
        {
            // 如果字串長度小於 2，直接返回字串長度，因為這種情況下不需要替換
            return len;
        }

        // 步驟 2: 初始化滑動窗口的左右邊界
        int left = 0;  // 窗口的左邊界
        int right = 0; // 窗口的右邊界

        // 步驟 3: 初始化結果變數和追蹤窗口內字符頻率所需的變數
        int res = 0;       // 儲存最終結果（最長重複字符子字串的長度）
        int maxCount = 0;  // 窗口內出現次數最多的字符的計數
        int[] count = new int[26]; // 用於記錄窗口內每個字符的出現次數（假設全是大寫英文字母A-Z）

        // 步驟 4: 開始滑動窗口算法
        while (right < len)
        {
            // 步驟 4.1: 擴展窗口 - 添加右邊界的字符到窗口中
            count[s[right] - 'A']++; // 增加當前字符的計數

            // 步驟 4.2: 更新窗口內出現最多的字符的計數
            // 只在發現更大的計數時更新 maxCount，這是一個重要的優化
            if (count[s[right] - 'A'] > maxCount)
            {
                maxCount = count[s[right] - 'A'];
            }

            // 步驟 4.3: 移動右邊界，擴大窗口
            right++;

            // 步驟 4.4: 檢查窗口是否需要縮小
            // 窗口大小 - 出現最多的字符次數 = 需要替換的字符數量
            // 如果需要替換的字符數量超過了 k，則需要縮小窗口
            if (right - left > maxCount + k)
            {
                // 步驟 4.4.1: 縮小窗口 - 從窗口中移除左邊界的字符
                count[s[left] - 'A']--; // 減少左邊界字符的計數

                // 步驟 4.4.2: 移動左邊界，縮小窗口
                left++;

                // 步驟 4.4.3: 早期返回優化
                // 如果剩餘未處理的字符長度+當前窗口長度不可能超過已找到的最大長度，則提前返回
                if (res >= right - left + (len - right))
                {
                    return res;
                }
            }

            // 步驟 4.5: 更新結果，記錄當前窗口的長度
            res = Math.Max(res, right - left);
        }

        // 步驟 5: 返回最終結果
        return res;
    }
}
