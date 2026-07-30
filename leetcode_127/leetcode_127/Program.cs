namespace leetcode_127;

class Program
{
    /// <summary>
    /// 127. Word Ladder
    /// https://leetcode.com/problems/word-ladder/description/
    /// 
    /// 127. 单词接龙
    /// https://leetcode.cn/problems/word-ladder/description/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 執行固定的單詞接龍案例，分別驗證三種 BFS 解法，並輸出每項檢查的預期值、實際值與通過狀態。
    /// 案例輸入皆符合題目限制；每次呼叫都建立獨立字典副本，最後輸出通過數與總檢查數。
    /// </summary>
    private static void RunSamples()
    {
        Program solution = new Program();
        SampleCase[] samples =
        [
            new(
                "官方可達範例",
                "hit",
                "cog",
                ["hot", "dot", "dog", "lot", "log", "cog"],
                5),
            new(
                "終點不在字典",
                "hit",
                "cog",
                ["hot", "dot", "dog", "lot", "log"],
                0),
            new(
                "一次字母變換",
                "log",
                "dog",
                ["hot", "dot", "dog", "lot", "log"],
                2),
            new(
                "終點存在但不連通",
                "hit",
                "cog",
                ["hot", "dot", "tod", "cog"],
                0),
            new(
                "單字母邊界",
                "a",
                "c",
                ["b", "c"],
                2)
        ];

        (string Name, Func<string, string, IList<string>, int> Solve)[] methods =
        [
            (nameof(LadderLength), solution.LadderLength),
            (nameof(LadderLength2), solution.LadderLength2),
            (nameof(LadderLength3), solution.LadderLength3)
        ];

        int passed = 0;
        int total = samples.Length * methods.Length;

        for (int index = 0; index < samples.Length; index++)
        {
            SampleCase sample = samples[index];
            Console.WriteLine($"案例 {index + 1}：{sample.Name}");
            Console.WriteLine(
                $"輸入：beginWord = \"{sample.BeginWord}\", endWord = \"{sample.EndWord}\", " +
                $"wordList = {FormatWordList(sample.WordList)}");

            foreach ((string methodName, Func<string, string, IList<string>, int> solve) in methods)
            {
                int actual = solve(
                    sample.BeginWord,
                    sample.EndWord,
                    sample.WordList.ToList());
                bool isPassed = actual == sample.Expected;
                passed += isPassed ? 1 : 0;

                Console.WriteLine(
                    $"  {methodName} | Expected: {sample.Expected} | Actual: {actual} | " +
                    $"{(isPassed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passed}/{total} 項檢查通過");
    }

    /// <summary>
    /// 將單詞集合格式化為易讀且固定的陣列表示，供範例輸出與 README 對照。
    /// </summary>
    /// <param name="words">符合題目限制的小寫英文字串集合。</param>
    /// <returns>以雙引號包住各單詞的陣列字串。</returns>
    private static string FormatWordList(IEnumerable<string> words)
    {
        return $"[{string.Join(", ", words.Select(word => $"\"{word}\""))}]";
    }

    /// <summary>
    /// 描述一組固定驗收案例，包含案例名稱、起點、終點、可用字典與預期最短序列長度。
    /// </summary>
    /// <param name="Name">顯示於主控台的案例名稱。</param>
    /// <param name="BeginWord">轉換序列的起始單詞。</param>
    /// <param name="EndWord">轉換序列的目標單詞。</param>
    /// <param name="WordList">轉換途中允許使用的單詞。</param>
    /// <param name="Expected">預期的最短轉換序列長度；無解時為 0。</param>
    private sealed record SampleCase(
        string Name,
        string BeginWord,
        string EndWord,
        string[] WordList,
        int Expected);

    /// <summary>
    /// 使用 HashSet 前沿實作雙向 BFS，求出從起點到終點的最短轉換序列長度。
    /// 搜尋同時從兩端進行，每輪展開節點較少的前沿，並以逐字元替換產生相鄰單詞；
    /// 兩個前沿相遇時，當前 BFS 層級即為最短答案。
    /// </summary>
    /// <param name="beginWord">起始單詞；假設由小寫英文字母組成，且與目標單詞等長但不相同。</param>
    /// <param name="endWord">目標單詞；必須出現在字典中才可能形成有效序列。</param>
    /// <param name="wordList">不含重複值、且每個單詞都與起點等長的可用字典。</param>
    /// <returns>包含起點與終點的最短轉換序列單詞數；不存在有效序列時回傳 0。</returns>
    public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        HashSet<string> wordSet = new HashSet<string>(wordList);
        if (!wordSet.Contains(endWord))
        {
            return 0;
        }

        HashSet<string> beginSet = new HashSet<string> { beginWord };
        HashSet<string> endSet = new HashSet<string> { endWord };
        int length = 1;

        while (beginSet.Count > 0 && endSet.Count > 0)
        {
            // 每輪只展開較小的前沿，降低雙向搜尋實際產生的鄰居數。
            if (beginSet.Count > endSet.Count)
            {
                var temp = beginSet;
                beginSet = endSet;
                endSet = temp;
            }

            HashSet<string> nextLevel = new HashSet<string>();

            foreach (string word in beginSet)
            {
                char[] wordChars = word.ToCharArray();

                for (int i = 0; i < wordChars.Length; i++)
                {
                    char originalChar = wordChars[i];

                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        if (c == originalChar)
                        {
                            continue;
                        }

                        wordChars[i] = c;
                        string newWord = new string(wordChars);

                        // 變換結果落在另一端前沿，代表兩個同層搜尋首次相遇。
                        if (endSet.Contains(newWord))
                        {
                            return length + 1;
                        }

                        // Remove 同時完成合法性檢查與去重，避免相同單詞再次進入前沿。
                        if (wordSet.Remove(newWord))
                        {
                            nextLevel.Add(newWord);
                        }
                    }

                    wordChars[i] = originalChar;
                }
            }

            beginSet = nextLevel;
            length++;
        }

        return 0;
    }

    /// <summary>
    /// 使用 Queue 實作由起點出發的單向 BFS，求出最短轉換序列長度。
    /// Dictionary 同時記錄已拜訪單詞與其層級；FIFO 順序保證第一次取出目標單詞時，
    /// 該層級就是最短序列長度。
    /// </summary>
    /// <param name="beginWord">起始單詞；假設由小寫英文字母組成，且與目標單詞等長但不相同。</param>
    /// <param name="endWord">目標單詞；必須出現在字典中才可能形成有效序列。</param>
    /// <param name="wordList">不含重複值、且每個單詞都與起點等長的可用字典。</param>
    /// <returns>包含起點與終點的最短轉換序列單詞數；不存在有效序列時回傳 0。</returns>
    public int LadderLength2(string beginWord, string endWord, IList<string> wordList)
    {
        HashSet<string> wordSet = new HashSet<string>(wordList);
        if (!wordSet.Contains(endWord))
        {
            return 0;
        }

        Queue<string> queue = new Queue<string>();
        queue.Enqueue(beginWord);

        Dictionary<string, int> visited = new Dictionary<string, int>
        {
            [beginWord] = 1
        };

        while (queue.Count > 0)
        {
            string currentWord = queue.Dequeue();
            int level = visited[currentWord];

            if (currentWord == endWord)
            {
                return level;
            }

            char[] wordArray = currentWord.ToCharArray();
            for (int i = 0; i < wordArray.Length; i++)
            {
                char original = wordArray[i];

                for (char c = 'a'; c <= 'z'; c++)
                {
                    if (c == original)
                    {
                        continue;
                    }

                    wordArray[i] = c;
                    string newWord = new string(wordArray);

                    // 層級在入列時鎖定，確保同一單詞只會以最短距離被處理。
                    if (wordSet.Contains(newWord) && !visited.ContainsKey(newWord))
                    {
                        visited[newWord] = level + 1;
                        queue.Enqueue(newWord);
                    }
                }

                wordArray[i] = original;
            }
        }

        return 0;
    }

    /// <summary>
    /// 使用兩組 Queue 與 Dictionary 實作按層展開的雙向 BFS，求出最短轉換序列長度。
    /// 每輪選擇待處理節點較少的一端，完整展開該層；兩端拜訪紀錄相遇時，
    /// 合併兩側層級即可得到最短序列長度。
    /// </summary>
    /// <param name="beginWord">起始單詞；假設由小寫英文字母組成，且與目標單詞等長但不相同。</param>
    /// <param name="endWord">目標單詞；必須出現在字典中才可能形成有效序列。</param>
    /// <param name="wordList">不含重複值、且每個單詞都與起點等長的可用字典。</param>
    /// <returns>包含起點與終點的最短轉換序列單詞數；不存在有效序列時回傳 0。</returns>
    public int LadderLength3(string beginWord, string endWord, IList<string> wordList)
    {
        HashSet<string> wordSet = new HashSet<string>(wordList);
        if (!wordSet.Contains(endWord))
        {
            return 0;
        }

        Queue<string> beginQueue = new Queue<string>();
        Queue<string> endQueue = new Queue<string>();

        beginQueue.Enqueue(beginWord);
        endQueue.Enqueue(endWord);

        Dictionary<string, int> beginVisited = new Dictionary<string, int>
        {
            [beginWord] = 1
        };
        Dictionary<string, int> endVisited = new Dictionary<string, int>
        {
            [endWord] = 1
        };

        while (beginQueue.Count > 0 && endQueue.Count > 0)
        {
            // Queue 中只保留同一方向的下一層；選擇較小者可降低實際搜尋分支。
            bool isFromBegin = beginQueue.Count <= endQueue.Count;
            int result = ExpandQueue(
                isFromBegin ? beginQueue : endQueue,
                isFromBegin ? beginVisited : endVisited,
                isFromBegin ? endVisited : beginVisited,
                wordSet);

            if (result > 0)
            {
                return result;
            }
        }

        return 0;
    }

    /// <summary>
    /// 完整展開指定方向目前的 BFS 層，產生尚未拜訪的合法鄰居，
    /// 並檢查它們是否已由另一方向抵達。
    /// </summary>
    /// <param name="queue">只包含目前方向待展開層級的佇列。</param>
    /// <param name="visited">目前方向已拜訪單詞及其層級。</param>
    /// <param name="otherVisited">另一方向已拜訪單詞及其層級。</param>
    /// <param name="wordSet">符合題目輸入條件的可用單詞集合。</param>
    /// <returns>本層找到的最短完整序列長度；尚未相遇時回傳 0。</returns>
    private int ExpandQueue(Queue<string> queue, Dictionary<string, int> visited,
        Dictionary<string, int> otherVisited, HashSet<string> wordSet)
    {
        int levelSize = queue.Count;
        int shortestConnection = int.MaxValue;

        // 必須處理完整層後再決定答案，避免先遇到較長的跨向連線。
        for (int count = 0; count < levelSize; count++)
        {
            string currentWord = queue.Dequeue();
            int level = visited[currentWord];
            char[] wordArray = currentWord.ToCharArray();

            for (int i = 0; i < wordArray.Length; i++)
            {
                char original = wordArray[i];

                for (char c = 'a'; c <= 'z'; c++)
                {
                    if (c == original)
                    {
                        continue;
                    }

                    wordArray[i] = c;
                    string newWord = new string(wordArray);

                    if (otherVisited.TryGetValue(newWord, out int otherLevel))
                    {
                        // currentWord 已包含目前端的層級；newWord 包含另一端的層級，兩者不重複。
                        shortestConnection = Math.Min(
                            shortestConnection,
                            level + otherLevel);
                        continue;
                    }

                    if (wordSet.Contains(newWord) && !visited.ContainsKey(newWord))
                    {
                        visited[newWord] = level + 1;
                        queue.Enqueue(newWord);
                    }
                }

                wordArray[i] = original;
            }
        }

        return shortestConnection == int.MaxValue ? 0 : shortestConnection;
    }
}
