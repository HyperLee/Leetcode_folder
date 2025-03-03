namespace leetcode_139
{
    internal class Program
    {
        /// <summary>
        /// 139. Word Break
        /// https://leetcode.com/problems/word-break/
        /// 139. 单词拆分
        /// https://leetcode.cn/problems/word-break/
        /// 
        /// 給定一個字串 s 和一個字典 wordDict，如果 s 能夠被分割成由一個或多個字典中的單字所組成、以空格分隔的序列，則返回 true。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "leetcode";
            // IList<string> wordDict = new List<string>();
            // wordDict.Add("leet");
            // wordDict.Add("code");

            List<string> wordDict = new List<string> { "leet", "code" };

            Console.WriteLine("res1: " + WordBreak(s, wordDict));
            Console.WriteLine("res2: " + WordBreak2(s, wordDict));
        }


        /// <summary>
        /// ref: 動態規劃
        /// https://leetcode.cn/problems/word-break/solution/dan-ci-chai-fen-by-leetcode-solution/
        /// https://leetcode.cn/problems/word-break/solutions/2968135/jiao-ni-yi-bu-bu-si-kao-dpcong-ji-yi-hua-chrs/
        /// 
        /// 
        /// 注意：不要求字典中出现的单词全部都使用，并且字典中的单词可以重复使用。
        /// 
        /// 我们定义 dp[i] 表示字符串 s 前 i 个字符组成的字符串 s[0..i − 1] 是否能被空格拆分成若干个字典中出现的单词。
        /// 默认 j = 0 时 s1​ 为空串
        /// 对于边界条件，我们定义 dp[0] = true 表示空串且合法。
        /// 
        /// 初始化: 
        /// 創建一個布爾數組 dp，長度為 n + 1（n 是字符串 s 的長度），用來記錄每個子字符串是否可以被劃分。
        /// 初始化 dp = true，表示空字符串可以被劃分。
        /// 
        /// 動態規劃:
        /// * 循環遍歷字符串的每個位置 i（從 1 到 n）。
        /// * 對於每個位置 i，再循環遍歷之前的所有位置 j（從 0 到 i - 1 ）。
        /// * 如果 dp[j] 為 true 且子字符串 s[j:i] 在字典中，則將 dp[i] 設置為 true。
        /// 
        /// 返回結果: 最終返回 dp[n]，這表示整個字符串是否可以被劃分。
        /// 
        /// wordsDictSet：
        /// 將 wordDict 轉換成 HashSet<string>，用於快速查找單詞是否存在（時間複雜度 O(1)）。
        /// 同時去除重複單詞，避免多餘檢查。
        /// 
        /// dp：
        /// 一個布林陣列，長度為 s.Length + 1。
        /// dp[i] 表示字符串 s 的前 i 個字元（即 s[0..i-1]）是否可以被分割成 wordDict 中的單詞。
        /// dp[0] = true 表示空字符串是可以分割的（基礎情況）。
        /// 
        /// 主循環:
        /// i 表示當前考慮的子字符串 s[0..i-1] 的長度。
        /// 目標是計算 dp[i]，即 s[0..i-1] 是否可以分割。
        /// 
        /// 內部循環:
        /// j 表示分割點，將 s[0..i-1] 分成兩部分：
        /// s[0..j-1]（長度為 j），對應 dp[j]。
        /// s[j..i-1]（長度為 i-j），檢查是否在 wordDict 中。
        /// 
        /// 判斷和更新：
        /// dp[j]：表示 s[0..j-1] 是否可以分割成字典中的單詞。
        /// s.Substring(j, i - j)：取子字符串 s[j..i-1]。也可以視為 s[j..i]，長度為 i - j。
        /// wordsDictSet.Contains(...)：檢查這個子字符串是否在字典中。
        /// wordsDictSet.Contains(s.Substring(j, i - j)) 為 true：表示從索引 j 到 i 的子字串（長度為 i - j）是一個字典中的單字。
        /// 如果 s[0..j-1] 可以分割（dp[j] == true），且 s[j..i-1] 是字典中的單詞，則 s[0..i-1] 可以分割，設 dp[i] = true。
        /// 一旦找到一個可行的分割點，break 跳出內層迴圈，因為 dp[i] 已確定。
        /// 
        /// 1.輸入的字串必須可以拆分
        /// 2.拆分的單詞必須在字典中
        /// </summary>
        /// <param name="s">要比對字串</param>
        /// <param name="wordDict">字典</param>
        /// <returns></returns>
        public static bool WordBreak(string s, IList<string> wordDict)
        {
            // 使用 HashSet 儲存 wordDict，方便快速查找
            var wordsDictSet = new HashSet<string>(wordDict);
            // dp[i] 表示 s[0:i] 是否可以用 wordDict 中的單詞拼接
            // 字串 s 的前 i 個字元是否可以被 wordDict 中的單字組成。
            var dp = new bool[s.Length + 1];
            // 空字串可以被劃分
            dp[0] = true;

            // 主循環： 循環從 1 到 s.Length，檢查每個子字符串是否可以被劃分：
            for (int i = 1; i <= s.Length; i++)
            {
                // 內部循環：列舉長度 i 的各種字串長度 (j) 組合, 出來比對
                // 嘗試將 s[0:i] 劃分為 s[0:j] 和 s[j:i]
                for (int j = 0; j < i; j++)
                {
                    string t_a = dp[j].ToString();
                    string t_b = s.Substring(j, i - j);

                    // 判斷字串 s 是否存在於 wordDict 中
                    // j 都從 0 開始且 dp[0] = true
                    // 判斷和更新： 檢查 dp[j] 是否為 true 且 s.Substring(j, i - j) 是否存在於 wordsDictSet 中
                    if (dp[j] && wordsDictSet.Contains(s.Substring(j, i - j)))
                    {
                        // 更新 dp[i] 為 true，表示 s[0:i] 可被拆分
                        dp[i] = true;
                        break;
                    }
                }
            }

            return dp[s.Length];
        }


        /// <summary>
        /// ref: BFS
        /// 使用 BFS（廣度優先搜尋） 來解 Word Break 問題，是另一種有效的解法。
        /// 這種方法的主要思路是將 s 視為一張圖，每個可能的切割點 i 都是一個節點，若 s[j:i] 在 wordDict 中，則有邊從 j 連到 i。
        /// 
        /// 我們使用一個佇列 queue 來實現 BFS。初始時，將 0 加入 queue，表示從 s[0] 開始進行切割。
        /// 每次取出 queue 中的頂部元素 start，對於每個以 start 為起點的切割點 end，如果 s[start:end] 在 wordDict 中，則將 end 加入 queue。
        /// 思路
        /// 1. 使用佇列（Queue） 來儲存當前可以開始檢查的索引 start。
        /// 2. 從 0 開始，不斷嘗試從 start 到 end 的子字串 s[start:end] 是否在 wordDict。
        /// 3. 如果 end == s.Length，則代表 s 可以完全被拆分，返回 true。
        /// 4. 使用 visited 集合 來記錄已經檢查過的索引 start，避免重複計算。 
        /// </summary>
        /// <param name="s">要比對字串</param>
        /// <param name="wordDict">字典</param>
        /// <returns></returns>
        public static bool WordBreak2(string s, IList<string> wordDict)
        {
            // 儲存字典單詞，查詢速度 O(1)
            var wordSet = new HashSet<string>(wordDict);
            // 佇列用於 BFS
            var queue = new Queue<int>();
            // 記錄已經訪問過的索引，避免重複計算
            var visited = new HashSet<int>();
            // 初始狀態：從索引 0 開始嘗試
            queue.Enqueue(0);

            while (queue.Count > 0)
            {
                // 取出當前要檢查的起點索引
                int start = queue.Dequeue();

                // 避免重複檢查相同的 start
                // 若已訪問過該索引，跳過，避免重複計算

                if (visited.Contains(start))
                {
                    continue;
                }

                // 標記為已訪問
                visited.Add(start);
                // 嘗試所有可能的結束索引
                for (int end = start + 1; end <= s.Length; end++)
                {
                    string sub = s.Substring(start, end - start); // 擷取子字串 s[start:end]

                    if (wordSet.Contains(sub)) // 如果子字串存在於字典
                    {
                        if (end == s.Length) // 如果剛好走到字串末尾，代表成功拆分
                            return true;

                        queue.Enqueue(end); // 將新的索引點加入佇列，繼續探索
                    }
                }

            }

            // 若 BFS 完全結束仍未找到可行的拆分，則返回 false
            return false;
        }
    }
}
