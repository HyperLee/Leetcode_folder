namespace leetcode_165;

class Program
{
    /// <summary>
    /// 165. Compare Version Numbers
    /// https://leetcode.com/problems/compare-version-numbers/description/?envType=daily-question&envId=2025-09-23
    /// 165. 比較版本號 (中文題目描述)
    /// 給定兩個版本字串 `version1` 與 `version2`，比較它們的大小。
    /// 版本字串由以點號 '.' 分隔的多個修訂（revision）組成。每個修訂的值取其整數轉換，忽略前置的零。
    /// 從左到右比較每個修訂的數值；若其中一個版本的修訂數較少，則將缺失的修訂視為 0。
    /// 回傳：
    ///   - 若 version1 < version2，回傳 -1
    ///   - 若 version1 > version2，回傳 1
    ///   - 否則回傳 0
    ///
    /// 原題連結（中文）：
    /// https://leetcode.cn/problems/compare-version-numbers/description/?envType=daily-question&envId=2025-09-23
    ///
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        // 測試資料陣列：每個元素為 Tuple(version1, version2)
        var tests = new (string, string)[]
        {
            ("1.01", "1.001"),
            ("1.0", "1.0.0"),
            ("0.1", "1.1"),
            ("1.0.1", "1"),
            ("7.5.2.4", "7.5.3"),
            ("1.0.0.0.0", "1"),
            ("1.2", "1.10"),
        };

        var program = new Program();

        Console.WriteLine("Compare Version Tests:\n");

        foreach (var (v1, v2) in tests)
        {
            int r1 = program.CompareVersion(v1, v2);
            int r2 = program.CompareVersion2(v1, v2);
            Console.WriteLine($"{v1}  vs  {v2}  => CompareVersion: {r1}, CompareVersion2: {r2}");
        }
    }


    /// <summary>
    /// 解法一：字串切割比對
    /// 
    /// 【解題思路】
    /// 1. 使用 Split('.') 將版本字串切割成修訂號陣列
    /// 2. 逐一比較每個位置的修訂號數值
    /// 3. 若某個版本的修訂號較少，則將缺失部分視為 0
    /// 
    /// 【時間複雜度】O(n + m)，其中 n 和 m 分別是兩個版本字串的長度
    /// 【空間複雜度】O(n + m)，用於儲存切割後的修訂號陣列
    /// 
    /// 【範例】
    /// version1 = "1.01", version2 = "1.001" → 結果：0 (相等)
    /// version1 = "1.0", version2 = "1.0.0" → 結果：0 (相等，缺失部分視為 0)
    /// version1 = "0.1", version2 = "1.1" → 結果：-1 (version1 較小)
    /// </summary>
    /// <param name="version1">第一個版本字串</param>
    /// <param name="version2">第二個版本字串</param>
    /// <returns>比較結果：-1 表示 version1 < version2，1 表示 version1 > version2，0 表示相等</returns>
    public int CompareVersion(string version1, string version2)
    {
        // 步驟 1：將版本字串以點號分割成修訂號陣列
        string[] v1 = version1.Split('.');
        string[] v2 = version2.Split('.');
        
        // 步驟 2：迴圈比較每個位置的修訂號
        // 使用 OR 條件確保處理所有修訂號，包括長度不同的情況
        for (int i = 0; i < v1.Length || i < v2.Length; i++)
        {
            // 初始化當前位置的修訂號值，預設為 0
            int x = 0;
            int y = 0;
            
            // 步驟 3：取得 version1 當前位置的修訂號
            // 若索引超出範圍則保持為 0（處理缺失修訂號的情況）
            if (i < v1.Length)
            {
                x = int.Parse(v1[i]); // 將字串轉換為整數，自動忽略前置零
            }
            
            // 步驟 4：取得 version2 當前位置的修訂號
            if (i < v2.Length)
            {
                y = int.Parse(v2[i]); // 將字串轉換為整數，自動忽略前置零
            }
            
            // 步驟 5：比較當前位置的修訂號
            if (x < y) return -1; // version1 較小
            if (x > y) return 1;  // version1 較大
            
            // 若相等則繼續比較下一個修訂號
        }
        
        // 所有修訂號都相等
        return 0;
    }


    /// <summary>
    /// 解法二：雙指針方式比對
    /// 分割版本號碼時候同時解析出修訂號的數值進行比對
    /// 
    /// 【解題思路】
    /// 1. 使用雙指針 i 和 j 分別遍歷兩個版本字串
    /// 2. 在遇到點號或字串結尾時，將累積的數字作為修訂號
    /// 3. 邊解析邊比較，無需額外的儲存空間
    /// 4. 當其中一個字串解析完畢時，另一個字串的剩餘部分視為 0
    /// 
    /// 【時間複雜度】O(n + m)，其中 n 和 m 分別是兩個版本字串的長度
    /// 【空間複雜度】O(1)，只使用常數額外空間，比解法一更優化
    /// 
    /// 【範例解析過程】
    /// version1 = "1.01", version2 = "1.001"
    /// 第一輪：解析 "1" vs "1" → 相等，繼續
    /// 第二輪：解析 "01" vs "001" → 1 vs 1 → 相等
    /// 結果：0 (相等)
    /// 
    /// 【優勢】
    /// - 不需要 Split 操作，節省記憶體
    /// - 即時比較，遇到差異可提早返回
    /// - 處理前置零更直接（透過數學運算）
    /// </summary>
    /// <param name="version1">第一個版本字串</param>
    /// <param name="version2">第二個版本字串</param>
    /// <returns>比較結果：-1 表示 version1 < version2，1 表示 version1 > version2，0 表示相等</returns>
    public int CompareVersion2(string version1, string version2)
    {
        // 取得兩個版本字串的長度
        int n = version1.Length;
        int m = version2.Length;
        
        // 初始化雙指針
        int i = 0; // version1 的當前位置
        int j = 0; // version2 的當前位置
        
        // 步驟 1：當任一字串還有字元未處理時繼續迴圈
        while (i < n || j < m)
        {
            // 初始化當前修訂號的數值
            int x = 0; // version1 當前修訂號
            int y = 0; // version2 當前修訂號
            
            // 步驟 2：解析 version1 的當前修訂號
            // 從當前位置開始，直到遇到點號或字串結尾
            while (i < n && version1[i] != '.')
            {
                // 將字元轉換為數字並累積到修訂號中
                // 透過 x * 10 + digit 的方式處理多位數
                // version1[i] - '0' 將字元轉換為對應的數字
                x = x * 10 + (version1[i] - '0');
                i++; // 移動到下一個字元
            }
            
            // 步驟 3：解析 version2 的當前修訂號
            // 邏輯同上，處理 version2
            while (j < m && version2[j] != '.')
            {
                y = y * 10 + (version2[j] - '0');
                j++; // 移動到下一個字元
            }
            
            // 步驟 4：比較當前修訂號的數值
            if (x < y) return -1; // version1 的當前修訂號較小
            if (x > y) return 1;  // version1 的當前修訂號較大
            
            // 步驟 5：跳過點號，準備處理下一個修訂號
            // 若已到達字串結尾，i++ 或 j++ 不會造成問題（下次迴圈會結束）
            i++; // 跳過 version1 的點號
            j++; // 跳過 version2 的點號
        }
        
        // 所有修訂號都相等
        return 0;
    }
}
