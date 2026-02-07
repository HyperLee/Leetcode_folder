namespace leetcode_1653;

class Program
{
    /// <summary>
    /// 1653. Minimum Deletions to Make String Balanced
    /// English:
    /// You are given a string s consisting only of characters 'a' and 'b'.
    /// You can delete any number of characters in s to make s balanced.
    /// s is balanced if there is no pair of indices (i, j) such that i &lt; j and s[i] = 'b' and s[j] = 'a'.
    /// Return the minimum number of deletions needed to make s balanced.
    ///
    /// 中文（繁體）:
    /// 給定一個只包含字元 'a' 和 'b' 的字串 s。
    /// 你可以刪除 s 中任意數量的字元，使 s 成為平衡字串。
    /// 當不存在索引對 (i, j) 滿足 i < j 且 s[i] = 'b' 且 s[j] = 'a' 時，字串 s 即為平衡。
    /// 回傳使 s 成為平衡字串所需的最少刪除數。
    ///
    /// 所謂平衡就是 不能出現 索引 index (i, j)
    /// s[i] = 'b'
    /// s[j] = 'a'
    /// => b 在 a 前面, 這就是不平衡
    /// 
    /// 簡單說下列三種方式 就是平衡
    /// 1. 全a 
    /// => 把 b 全部刪除
    /// 2. 全b 
    /// => 把 a 全部刪除
    /// 3. a, b 混合,但是 a 在前 b 在後( a 在 b 左邊)
    /// => 依據 case 刪除相鄰的 b or a 都可以達成
    /// Reference:
    /// https://leetcode.com/problems/minimum-deletions-to-make-string-balanced/
    /// https://leetcode.cn/problems/minimum-deletions-to-make-string-balanced/
    /// </summary>
    /// <param name="args">命令列引數</param>
    static void Main(string[] args)
    {
        Console.WriteLine("=== LeetCode 1653: Minimum Deletions to Make String Balanced ===");
        Console.WriteLine();

        // 測試案例
        string[] testCases = [
            "aababbab",
            "bbaaaaabb",
            "aaa",
            "bbb",
            "abababab",
            "ababaaaabbbbbaaababbbbbbaaabbaababbabbbbaabbbbaabbabbabaabbbababaa"
        ];

        var solution = new Program();

        for (int i = 0; i < testCases.Length; i++)
        {
            string s = testCases[i];
            int result1 = solution.MinimumDeletions(s);
            int result2 = MinimumDeletions2(s);

            Console.WriteLine($"測試案例 {i + 1}: \"{s}\"");
            Console.WriteLine($"  MinimumDeletions:  {result1}");
            Console.WriteLine($"  MinimumDeletions2: {result2}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 解法一：分隔線掃描法 (Two-Pass with Divider)
    /// 
    /// 核心概念：
    /// 想像在字串的任意位置劃一條分隔線，分隔線左邊應該全是 'a'，右邊應該全是 'b'。
    /// 例如：ba => b | a
    /// - 分隔線左邊的 'b' 需要刪除 (leftb)
    /// - 分隔線右邊的 'a' 需要刪除 (righta)
    /// - 刪除次數 = leftb + righta
    /// 
    /// 解題步驟：
    /// 1. 第一次遍歷：計算字串中總共有多少個 'a' (初始化 righta)
    /// 2. 第二次遍歷：在每個位置嘗試放置分隔線
    ///    - 遇到 'a'：righta-- (這個 'a' 移到分隔線左邊了，不需要刪除)
    ///    - 遇到 'b'：leftb++ (這個 'b' 在分隔線左邊，可能需要刪除)
    ///    - 記錄最小的 leftb + righta
    /// 
    /// 時間複雜度：O(n) - 兩次遍歷
    /// 空間複雜度：O(1) - 只使用常數空間
    /// 
    /// 註：只需要刪除 'a' 或 'b' 其中一種，不需要同時刪除
    /// </summary>
    /// <param name="s">輸入字串</param>
    /// <returns>最少刪除次數</returns>
    public int MinimumDeletions(string s)
    {
        // leftb: 分隔線左邊的 'b' 字元數量
        // righta: 分隔線右邊的 'a' 字元數量
        int leftb = 0;
        int righta = 0;

        // 第一次遍歷：計算整個字串中 'a' 的總數
        // 初始假設分隔線在最左邊，所有 'a' 都在右邊
        foreach (char c in s)
        {
            if (c == 'a')
            {
                righta++;
            }
        }

        // 初始刪除次數 = 0 個左邊的 'b' + 所有右邊的 'a'
        int minDeletions = leftb + righta;

        // 第二次遍歷：模擬分隔線從左到右移動
        foreach (char c in s)
        {
            if (c == 'a')
            {
                // 遇到 'a'：此 'a' 移到分隔線左邊，右邊的 'a' 減少
                righta--;
            }
            else
            {
                // 遇到 'b'：此 'b' 在分隔線左邊，左邊的 'b' 增加
                leftb++;
            }

            // 在當前分隔線位置，計算需要刪除的字元數
            // 並更新最小刪除次數
            minDeletions = Math.Min(minDeletions, leftb + righta);
        }

        return minDeletions;
    }

    /// <summary>
    /// 解法二：貪心演算法 + 一次遍歷 (Greedy One-Pass)
    /// 
    /// 核心概念：
    /// 當遇到「'b' 後面跟著 'a'」的不平衡情況時，有兩種選擇：
    /// 1. 刪除當前的 'a'
    /// 2. 刪除之前遇到的某個 'b'
    /// 貪心策略：選擇對後續影響最小的方案
    /// 
    /// 解題邏輯：
    /// - leftb：記錄目前遇到的 'b' 字元數量
    /// - res：記錄需要刪除的次數
    /// 
    /// 遍歷字串時：
    /// 1. 遇到 'b'：leftb++ (可能需要刪除的 'b' 候選)
    /// 2. 遇到 'a'：
    ///    - 如果前面有 'b' (leftb > 0)，表示出現不平衡
    ///    - 刪除這個 'a' 或刪除前面的一個 'b' 都可以
    ///    - 貪心選擇：刪除一個字元，並將 leftb-- (減少未來不平衡的可能)
    ///    - res++ (累計刪除次數)
    /// 
    /// 時間複雜度：O(n) - 一次遍歷
    /// 空間複雜度：O(1) - 只使用常數空間
    /// 
    /// 優勢：相比解法一，只需要一次遍歷，效率更高
    /// </summary>
    /// <param name="s">輸入字串</param>
    /// <returns>最少刪除次數</returns>
    public static int MinimumDeletions2(string s)
    {
        // leftb: 目前遇到的 'b' 字元數量 (可能需要刪除的 'b' 候選)
        // res: 累計刪除次數
        int leftb = 0, res = 0;

        foreach (char c in s) 
        {
            if (c == 'a')
            {
                // 遇到 'a' 且前面有 'b'，表示出現 "b...a" 不平衡模式
                if (leftb > 0)
                {
                    // 貪心策略：刪除一個字元來解決不平衡
                    // 可以刪除當前的 'a' 或之前的某個 'b'
                    // 選擇刪除並減少 leftb，降低未來不平衡的風險
                    res++;      // 累計刪除次數
                    leftb--;    // 減少一個 'b' 的計數（視為已處理）
                }
                // 如果 leftb == 0，表示前面沒有 'b'，這個 'a' 不造成不平衡
            }
            else
            {
                // 遇到 'b'：累計 'b' 的數量
                // 這些 'b' 是潛在需要刪除的候選（如果後面遇到 'a'）
                leftb++;
            }
        }

        return res;        
    }
}
