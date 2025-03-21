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
        Program solution = new Program();

        // 測試用例1
        string beginWord1 = "hit";
        string endWord1 = "cog";
        IList<string> wordList1 = new List<string> { "hot","dot","dog","lot","log","cog" };
        Console.WriteLine($"測試用例1結果 (LadderLength): {solution.LadderLength(beginWord1, endWord1, wordList1)}"); // 預期輸出: 5
        Console.WriteLine($"測試用例1結果 (LadderLength2): {solution.LadderLength2(beginWord1, endWord1, wordList1)}"); // 預期輸出: 5
        Console.WriteLine($"測試用例1結果 (LadderLength3): {solution.LadderLength3(beginWord1, endWord1, wordList1)}"); // 預期輸出: 5

        // 測試用例2
        string beginWord2 = "hit";
        string endWord2 = "cog";
        IList<string> wordList2 = new List<string> { "hot","dot","dog","lot","log" };
        Console.WriteLine($"測試用例2結果 (LadderLength): {solution.LadderLength(beginWord2, endWord2, wordList2)}"); // 預期輸出: 0
        Console.WriteLine($"測試用例2結果 (LadderLength2): {solution.LadderLength2(beginWord2, endWord2, wordList2)}"); // 預期輸出: 0
        Console.WriteLine($"測試用例2結果 (LadderLength3): {solution.LadderLength3(beginWord2, endWord2, wordList2)}"); // 預期輸出: 0

        // 測試用例3
        string beginWord3 = "log";
        string endWord3 = "dog";
        IList<string> wordList3 = new List<string> { "hot","dot","dog","lot","log" };
        Console.WriteLine($"測試用例3結果 (LadderLength): {solution.LadderLength(beginWord3, endWord3, wordList3)}"); // 預期輸出: 2
        Console.WriteLine($"測試用例3結果 (LadderLength2): {solution.LadderLength2(beginWord3, endWord3, wordList3)}"); // 預期輸出: 2
        Console.WriteLine($"測試用例3結果 (LadderLength3): {solution.LadderLength3(beginWord3, endWord3, wordList3)}"); // 預期輸出: 2

        // 比較兩種方法的執行時間
        Console.WriteLine("\n效能比較測試：");
        var startTime1 = DateTime.Now;
        solution.LadderLength(beginWord1, endWord1, wordList1);
        var endTime1 = DateTime.Now;
        Console.WriteLine($"LadderLength 執行時間: {(endTime1 - startTime1).TotalMilliseconds}ms");

        var startTime2 = DateTime.Now;
        solution.LadderLength2(beginWord1, endWord1, wordList1);
        var endTime2 = DateTime.Now;
        Console.WriteLine($"LadderLength2 執行時間: {(endTime2 - startTime2).TotalMilliseconds}ms");

        var startTime3 = DateTime.Now;
        solution.LadderLength3(beginWord1, endWord1, wordList1);
        var endTime3 = DateTime.Now;
        Console.WriteLine($"LadderLength3 執行時間: {(endTime3 - startTime3).TotalMilliseconds}ms");
    }

    /// <summary>
    /// 使用雙向BFS（廣度優先搜索）解決單詞接龍問題
    /// 解題思路：
    /// 1. 使用雙向BFS，同時從起點和終點開始搜索，當兩個搜索相遇時即找到最短路徑
    /// 2. 通過每次選擇較小的集合進行擴展，可以顯著減少搜索空間
    /// 3. 對每個單詞的每個字符位置，嘗試替換為a-z的所有可能，檢查是否在字典中
    /// 
    /// 時間複雜度：O(N * 26 * L)，其中 N 是字典中的單詞數量，L 是單詞長度
    /// 空間複雜度：O(N)，用於存儲訪問過的單詞
    /// 
    /// 為什麼要每次擴展較小的集合？
    /// 在雙向 BFS 中，每次擴展 節點較少的集合 是為了減少搜尋範圍，加速找到答案。這樣做的核心原因有三個：
    /// 1.減少擴展的節點數量，降低時間複雜度
    /// 2.防止不必要的計算，提升效率
    /// 3.平衡搜尋範圍，避免某一端擴展過快導致效率下降 
    /// </summary>
    /// <param name="beginWord">起始單詞</param>
    /// <param name="endWord">目標單詞</param>
    /// <param name="wordList">可用的單詞字典</param>
    /// <returns>最短轉換序列的長度，若無法轉換則返回0</returns>
    public int LadderLength(string beginWord, string endWord, IList<string> wordList) 
    {
        // 將wordList轉換為HashSet以提高查詢效率
        HashSet<string> wordSet = new HashSet<string>(wordList);
        if (!wordSet.Contains(endWord)) 
        {
            return 0; // 如果終點詞不在字典中，無法完成轉換，直接返回 0
        }
        
        // 初始化雙向BFS的起點集合和終點集合
        // BFS 起點
        HashSet<string> beginSet = new HashSet<string> { beginWord };
        // BFS 終點
        HashSet<string> endSet = new HashSet<string> { endWord };
        // 初始化路徑長度(轉換步驟計數)，包含起始單詞
        // 初始為 1（包含 beginWord）
        int length = 1; 
        
        // 開始 BFS 搜索
        while (beginSet.Count > 0 && endSet.Count > 0) 
        {
            // 優化：始終從較小的集合開始擴展，以減少搜索空間
            if (beginSet.Count > endSet.Count) 
            {
                var temp = beginSet;
                beginSet = endSet;
                endSet = temp;
            }
            
            // 創建新的 HashSet 用於存儲下一層要訪問的單詞
            HashSet<string> nextLevel = new HashSet<string>();
            
            // 遍歷當前 BFS 層的所有單詞
            foreach (string word in beginSet) 
            {
                // 轉為字元陣列，方便修改字母
                char[] wordChars = word.ToCharArray();
                
                // 嘗試變更單詞中的每個字符
                for (int i = 0; i < wordChars.Length; i++) 
                {
                    // 保存原來的字母
                    char originalChar = wordChars[i];
                    
                    // 嘗試替換為a-z的每個字母
                    for (char c = 'a'; c <= 'z'; c++) 
                    {
                        // 跳過相同的字符
                        if (c == originalChar) 
                        {
                            continue; 
                        }
                        
                        wordChars[i] = c;
                        // 轉回字串
                        string newWord = new string(wordChars);
                        
                        // 檢查是否找到終點（兩個搜索相遇）
                        // 如果變換後的單詞存在於對方 BFS 集合，代表兩端相遇
                        if (endSet.Contains(newWord)) 
                        {
                            // 找到最短路徑
                            return length + 1;
                        }
                        
                        // 如果新單詞在字典中，加入下一層 BFS 待訪問集合
                        if (wordSet.Contains(newWord)) 
                        {
                            nextLevel.Add(newWord);
                            // 從字典中移除以防止重複訪問; 為了避免重複訪問，立即移除
                            wordSet.Remove(newWord); 
                        }
                    }
                    
                    // 恢復原始字符，準備下一個位置(字母)的嘗試
                    wordChars[i] = originalChar; 
                }
            }
            
            // 更新當前層為下一層的單詞集合; 更新 BFS 佇列，進入下一層
            beginSet = nextLevel; 
            // 路徑長度加1
            length++; 
        }
        
        // 若 BFS 完全展開仍無法找到目標，返回 0; 無法找到有效轉換路徑
        return 0;
    }


    /// <summary>
    /// 使用單向BFS（廣度優先搜索）解決單詞接龍問題
    /// 解題思路：
    /// 1. 使用簡單的BFS從起點開始搜索，直到找到終點或搜索完所有可能
    /// 2. 使用 Queue 來實現BFS，確保按層級順序訪問單詞
    /// 3. 使用 Dictionary 記錄已訪問的單詞及其層級，避免重複訪問
    /// 4. 對每個單詞的每個位置，嘗試替換為其他字母，檢查是否在字典中
    /// 
    /// 時間複雜度：O(N * 26 * L)，其中 N 是字典中的單詞數量，L 是單詞長度
    /// 空間複雜度：O(N)，用於存儲訪問過的單詞
    /// 
    /// 與 LadderLength 方法的比較：
    /// 1. 這個方法實現更簡單，使用單向BFS
    /// 2. 使用 Dictionary 代替 HashSet 來追踪訪問狀態
    /// 3. 不需要在搜索過程中從字典中移除單詞
    /// 4. 空間效率略低於雙向BFS，但程式碼更容易理解
    /// </summary>
    /// <param name="beginWord">起始單詞</param>
    /// <param name="endWord">目標單詞</param>
    /// <param name="wordList">可用的單詞字典</param>
    /// <returns>最短轉換序列的長度，若無法轉換則返回0</returns>
    public int LadderLength2(string beginWord, string endWord, IList<string> wordList) 
    {
        // 將 wordList 轉換為 HashSet 以提高查詢效率
        HashSet<string> wordSet = new HashSet<string>(wordList);
        // 如果終點詞不在字典中，無法完成轉換，直接返回 0
        if (!wordSet.Contains(endWord)) return 0;

        // 初始化 BFS 隊列，並加入起始單詞
        Queue<string> queue = new Queue<string>();
        queue.Enqueue(beginWord);
        
        // 使用 Dictionary 記錄已訪問的單詞和其對應的層級
        Dictionary<string, int> visited = new Dictionary<string, int>();
        visited[beginWord] = 1;  // 初始層級為 1

        // 開始 BFS 搜索
        while (queue.Count > 0)
        {
            // 從隊列中取出當前待處理的單詞
            string currentWord = queue.Dequeue();
            // 獲取當前單詞所在的層級
            int level = visited[currentWord];

            // 如果找到目標單詞，返回當前層級
            if (currentWord == endWord) return level;

            // 將當前單詞轉換為字符數組，方便修改
            char[] wordArray = currentWord.ToCharArray();
            // 嘗試變更單詞中的每個字符
            for (int i = 0; i < wordArray.Length; i++)
            {
                // 保存原始字符
                char original = wordArray[i];
                // 嘗試用 a-z 替換當前位置的字符
                for (char c = 'a'; c <= 'z'; c++)
                {
                    wordArray[i] = c;
                    string newWord = new string(wordArray);
                    
                    // 如果新單詞在字典中且未被訪問過
                    if (wordSet.Contains(newWord) && !visited.ContainsKey(newWord))
                    {
                        // 記錄新單詞的層級（當前層級+1）
                        visited[newWord] = level + 1;
                        // 將新單詞加入隊列，等待處理
                        queue.Enqueue(newWord);
                    }
                }
                // 恢復原始字符，準備處理下一個位置
                wordArray[i] = original;
            }
        }
        
        // 如果無法找到轉換路徑，返回 0
        return 0;
    }

    /// <summary>
    /// 使用Queue實作雙向BFS（廣度優先搜索）解決單詞接龍問題
    /// 解題思路：
    /// 1. 使用兩個Queue和Dictionary來實現雙向BFS
    /// 2. 從起點和終點同時開始搜索，直到兩個搜索相遇
    /// 3. 使用Dictionary記錄每個方向已訪問的單詞及其層級
    /// </summary>
    public int LadderLength3(string beginWord, string endWord, IList<string> wordList)
    {
        // 步驟1: 建立單詞字典並檢查終點詞是否存在
        HashSet<string> wordSet = new HashSet<string>(wordList);
        if (!wordSet.Contains(endWord)) return 0;

        // 步驟2: 初始化雙向BFS所需的資料結構
        // 起點和終點的佇列
        Queue<string> beginQueue = new Queue<string>();
        Queue<string> endQueue = new Queue<string>();
        // 記錄已訪問單詞的字典，key是單詞，value是層級
        Dictionary<string, int> beginVisited = new Dictionary<string, int>();
        Dictionary<string, int> endVisited = new Dictionary<string, int>();

        // 步驟3: 將起點和終點加入各自的佇列和已訪問字典
        beginQueue.Enqueue(beginWord);
        endQueue.Enqueue(endWord);
        beginVisited[beginWord] = 1; // 層級從1開始計數
        endVisited[endWord] = 1;

        // 步驟4: 開始雙向BFS搜索
        // 當兩個方向的佇列都不為空時繼續搜索
        while (beginQueue.Count > 0 && endQueue.Count > 0)
        {
            // 步驟5: 選擇較小的佇列進行擴展，以優化搜索效率
            bool isFromBegin = beginQueue.Count <= endQueue.Count;
            // 呼叫ExpandQueue進行搜索擴展
            var result = ExpandQueue(
                isFromBegin ? beginQueue : endQueue,          // 選擇要擴展的佇列
                isFromBegin ? beginVisited : endVisited,     // 當前方向的已訪問字典
                isFromBegin ? endVisited : beginVisited,     // 另一方向的已訪問字典
                wordSet
            );

            // 步驟6: 檢查是否找到路徑
            if (result > 0) return result;
        }

        // 步驟7: 若搜索完畢仍未找到路徑，返回0
        return 0;
    }

    /// <summary>
    /// 擴展當前佇列中的節點，尋找可能的轉換路徑
    /// </summary>
    /// <param name="queue">當前要處理的佇列</param>
    /// <param name="visited">當前方向的已訪問字典</param>
    /// <param name="otherVisited">另一方向的已訪問字典</param>
    /// <param name="wordSet">單詞字典</param>
    /// <returns>找到路徑時返回總長度，否則返回0</returns>
    private int ExpandQueue(Queue<string> queue, Dictionary<string, int> visited,
        Dictionary<string, int> otherVisited, HashSet<string> wordSet)
    {
        // 步驟1: 從佇列中取出當前要處理的單詞
        string currentWord = queue.Dequeue();
        // 獲取當前單詞的層級
        int level = visited[currentWord];

        // 步驟2: 將單詞轉換為字元陣列以便修改
        char[] wordArray = currentWord.ToCharArray();

        // 步驟3: 嘗試改變單詞中的每個字母
        for (int i = 0; i < wordArray.Length; i++)
        {
            // 保存原始字母，以便後續還原
            char original = wordArray[i];
            
            // 步驟4: 嘗試用a-z替換當前位置的字母
            for (char c = 'a'; c <= 'z'; c++)
            {
                wordArray[i] = c;
                string newWord = new string(wordArray);

                // 步驟5: 檢查是否與另一個方向的搜索相遇
                if (otherVisited.ContainsKey(newWord))
                {
                    // 找到相遇點，返回總路徑長度
                    // 減1是因為相遇點在兩個方向都被計算了一次
                    return level + otherVisited[newWord] - 1;
                }

                // 步驟6: 檢查新單詞是否合法且未被訪問過
                if (wordSet.Contains(newWord) && !visited.ContainsKey(newWord))
                {
                    // 記錄新單詞的層級（當前層級+1）
                    visited[newWord] = level + 1;
                    // 將新單詞加入佇列，等待後續處理
                    queue.Enqueue(newWord);
                }
            }
            
            // 步驟7: 還原當前位置的字母，準備處理下一個位置
            wordArray[i] = original;
        }

        // 步驟8: 若未找到相遇點，返回0
        return 0;
    }
}
