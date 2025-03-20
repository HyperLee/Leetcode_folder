namespace leetcode_127;

class Program
{
    /// <summary>
    /// 127. Word Ladder
    /// https://leetcode.com/problems/word-ladder/description/
    /// 127. 单词接龙
    /// https://leetcode.cn/problems/word-ladder/description/ 
    /// 
    /// 這個問題要求找到從起始單字 beginWord 到目標單字 endWord 的最短轉換序列。
    /// 每次轉換只能改變一個字母，並且每個中間單字必須存在於給定的字典 wordList 中。 
    /// 
    /// 題目描述
    /// 給定兩個單字 beginWord 和 endWord，以及一個字典 wordList，找出從 beginWord 到 endWord 的最短轉換序列的長度。
    /// 如果無法完成轉換，則返回 0。
    /// 
    /// 規則
    /// 每次轉換只能改變一個字母。
    /// 每個中間單字必須存在於 wordList 中。
    /// beginWord 不需要在 wordList 中。
    /// 轉換序列中的每個相鄰單字只能相差一個字母。 
    static void Main(string[] args)
    {
        Program solution = new Program();

        // 測試案例 1: 正常轉換路徑
        string beginWord1 = "hit";
        string endWord1 = "cog";
        IList<string> wordList1 = new List<string> { "hot", "dot", "dog", "lot", "log", "cog" };
        Console.WriteLine($"Test Case 1: {solution.LadderLength(beginWord1, endWord1, wordList1)}"); // 預期輸出: 5
        // hit -> hot -> dot -> dog -> cog

        // 測試案例 2: 目標詞不在字典中
        string beginWord2 = "hit";
        string endWord2 = "cog";
        IList<string> wordList2 = new List<string> { "hot", "dot", "dog", "lot", "log" };
        Console.WriteLine($"Test Case 2: {solution.LadderLength(beginWord2, endWord2, wordList2)}"); // 預期輸出: 0

        // 測試案例 3: 無法轉換到目標詞
        string beginWord3 = "hit";
        string endWord3 = "xyz";
        IList<string> wordList3 = new List<string> { "hot", "dot", "dog", "lot", "log" };
        Console.WriteLine($"Test Case 3: {solution.LadderLength(beginWord3, endWord3, wordList3)}"); // 預期輸出: 0
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
}
