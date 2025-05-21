using System;

namespace leetcode_3355;


class Program
{
    /// <summary>
    /// 3355. Zero Array Transformation I
    /// https://leetcode.com/problems/zero-array-transformation-i/description/?envType=daily-question&envId=2025-05-20
    /// 3355. 零数组变换 I
    /// https://leetcode.cn/problems/zero-array-transformation-i/description/?envType=daily-question&envId=2025-05-20
    /// 
    /// 題目描述：
    /// 給你一個整數陣列 nums，你可以對 nums 中的每個元素進行以下操作：
    /// 1. 選擇一個索引 i 使得 0 <= i < nums.length
    /// 2. 將 nums[i] 的值減少 1
    /// 
    /// 另外，給定一個查詢陣列 queries，每個查詢包含兩個整數 [l, r]，表示對索引從 l 到 r 的所有元素同時進行上述操作一次。
    /// 問題要求判斷是否可以通過執行給定的查詢操作，將 nums 中的所有元素都變為 0。
    /// 
    /// 解題思路：
    /// 本題可以使用差分陣列技術高效處理區間操作。通過建立差分陣列，我們可以在 O(1) 時間內對一個區間進行加值或減值操作，
    /// 然後透過前綴和計算出每個位置實際被操作的次數，最後檢查這些操作次數是否足夠將原始陣列的所有元素變為 0。
    /// </summary>
    /// <param name="args">程式執行參數</param>
    static void Main(string[] args)
    {
        Console.WriteLine("零陣列轉換演示程式");
        Console.WriteLine("------------------");

        // 建立測試實例
        Program instance = new Program();

        // 測試案例 1: 可以轉換為全零陣列的情況
        Console.WriteLine("測試案例 1:");
        int[] nums1 = { 2, 1, 3, 2 };
        int[][] queries1 = {
            new int[] { 0, 1 },
            new int[] { 1, 3 },
            new int[] { 0, 3 },
            new int[] { 0, 3 }  // 加入一次額外操作
        };
        Console.WriteLine($"輸入陣列: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"操作區間: {QueriesString(queries1)}");

        // 方法一測試
        bool result1Method1 = IsZeroArray(nums1, queries1);
        Console.WriteLine($"方法一結果: {(result1Method1 ? "可以轉換為全零陣列" : "無法轉換為全零陣列")}");

        // 方法二測試
        bool result1Method2 = IsZeroArray2(nums1, queries1);
        Console.WriteLine($"方法二結果: {(result1Method2 ? "可以轉換為全零陣列" : "無法轉換為全零陣列")}");

        // 方法三測試
        bool result1Method3 = instance.IsZeroArray3(nums1, queries1);
        Console.WriteLine($"方法三結果: {(result1Method3 ? "可以轉換為全零陣列" : "無法轉換為全零陣列")}");

        Console.WriteLine();

        // 測試案例 2: 無法轉換為全零陣列的情況
        Console.WriteLine("測試案例 2:");
        int[] nums2 = { 3, 2, 1, 4 };
        int[][] queries2 = {
            new int[] { 0, 2 },
            new int[] { 1, 3 }
        };
        Console.WriteLine($"輸入陣列: [{string.Join(", ", nums2)}]");
        Console.WriteLine($"操作區間: {QueriesString(queries2)}");

        // 方法一測試
        bool result2Method1 = IsZeroArray(nums2, queries2);
        Console.WriteLine($"方法一結果: {(result2Method1 ? "可以轉換為全零陣列" : "無法轉換為全零陣列")}");

        // 方法二測試
        bool result2Method2 = IsZeroArray2(nums2, queries2);
        Console.WriteLine($"方法二結果: {(result2Method2 ? "可以轉換為全零陣列" : "無法轉換為全零陣列")}");

        // 方法三測試
        bool result2Method3 = instance.IsZeroArray3(nums2, queries2);
        Console.WriteLine($"方法三結果: {(result2Method3 ? "可以轉換為全零陣列" : "無法轉換為全零陣列")}");

        Console.WriteLine();

        // 測試案例 3: 剛好能轉換為全零陣列的邊界情況
        Console.WriteLine("測試案例 3:");
        int[] nums3 = { 1, 2, 1 };
        int[][] queries3 = {
            new int[] { 0, 0 },
            new int[] { 1, 1 },
            new int[] { 2, 2 },
            new int[] { 0, 2 }
        };
        Console.WriteLine($"輸入陣列: [{string.Join(", ", nums3)}]");
        Console.WriteLine($"操作區間: {QueriesString(queries3)}");

        // 方法一測試
        bool result3Method1 = IsZeroArray(nums3, queries3);
        Console.WriteLine($"方法一結果: {(result3Method1 ? "可以轉換為全零陣列" : "無法轉換為全零陣列")}");

        // 方法二測試
        bool result3Method2 = IsZeroArray2(nums3, queries3);
        Console.WriteLine($"方法二結果: {(result3Method2 ? "可以轉換為全零陣列" : "無法轉換為全零陣列")}");

        // 方法三測試
        bool result3Method3 = instance.IsZeroArray3(nums3, queries3);
        Console.WriteLine($"方法三結果: {(result3Method3 ? "可以轉換為全零陣列" : "無法轉換為全零陣列")}");

        // 測試案例 4: 極端情況 - 所有元素都是 0
        Console.WriteLine("\n測試案例 4:");
        int[] nums4 = { 0, 0, 0 };
        int[][] queries4 = { };  // 不需要任何操作
        Console.WriteLine($"輸入陣列: [{string.Join(", ", nums4)}]");
        Console.WriteLine($"操作區間: {(queries4.Length == 0 ? "無操作" : QueriesString(queries4))}");

        // 方法一測試
        bool result4Method1 = IsZeroArray(nums4, queries4);
        Console.WriteLine($"方法一結果: {(result4Method1 ? "可以轉換為全零陣列" : "無法轉換為全零陣列")}");

        // 方法二測試
        bool result4Method2 = IsZeroArray2(nums4, queries4);
        Console.WriteLine($"方法二結果: {(result4Method2 ? "可以轉換為全零陣列" : "無法轉換為全零陣列")}");

        // 方法三測試
        bool result4Method3 = instance.IsZeroArray3(nums4, queries4);
        Console.WriteLine($"方法三結果: {(result4Method3 ? "可以轉換為全零陣列" : "無法轉換為全零陣列")}");
    }

    /// <summary>
    /// 將查詢操作陣列格式化為便於顯示的字串
    /// </summary>
    /// <param name="queries">查詢操作陣列</param>
    /// <returns>格式化後的字串</returns>
    private static string QueriesString(int[][] queries)
    {
        return string.Join(", ", queries.Select(q => $"[{q[0]}, {q[1]}]"));
    }


    /// <summary>
    /// 判斷是否可以將陣列轉換為全零陣列 (方法一)
    /// 
    /// 解題思路：
    /// 本方法使用差分陣列技術來高效處理區間操作。差分陣列是一種特殊的資料結構，特別適合處理多次區間修改的情境。
    /// 
    /// 差分陣列原理：
    /// 1. 對於區間 [l,r] 的操作，只需在差分陣列 diff [l] 位置加上操作值，在 diff [r+1] 位置減去操作值
    /// 2. 透過前綴和累加差分陣列，可以還原出每個位置實際被操作的總次數
    /// 3. 這種方法的優勢在於不需要重複操作區間中的每個元素，而是只需標記區間的起始和結束位置
    /// 
    /// 解題步驟：
    /// 1. 創建一個長度為 n+1 的差分陣列 diff，初始值都為 0
    /// 2. 對於每個查詢 [l,r]，我們將 diff [l] 加 1，將 diff [r+1] 減 1
    /// 3. 累加差分陣列得到前綴和，此時 diff [i] 表示位置 i 的操作總次數
    /// 4. 檢查每個 nums [i] 是否小於等於其對應的操作次數，如果是則可轉為 0，否則不能
    /// 
    /// 時間複雜度：O (n + q)，其中 n 是陣列長度，q 是查詢操作數量
    /// 空間複雜度：O (n)，用於儲存差分陣列
    /// </summary>
    /// <param name="nums"> 原始整數陣列，包含需要轉換為 0 的元素 </param>
    /// <param name="queries"> 查詢操作陣列，每個查詢為 [l,r] 表示對區間 [l,r] 執行減 1 操作 </param>
    /// <returns > 如果可以通過給定的操作將所有元素變為 0 則返回 true，否則返回 false</returns>
    public static bool IsZeroArray(int[] nums, int[][] queries)
    {
        // 獲取陣列長度
        int n = nums.Length;
        // 建立差分陣列，長度為 n+1 以處理邊界情況
        int[] diff = new int[n + 1];

        // 遍歷每個查詢操作
        foreach (int[] q in queries)
        {
            // 對區間 [l,r] 進行操作標記
            // 在差分陣列的左邊界加 1，表示從此位置開始進行操作
            diff[q[0]]++;
            // 在差分陣列的右邊界 + 1 處減 1，表示在此位置結束操作
            diff[q[1] + 1]--;
        }

        // 累加差分陣列得到每個位置的實際操作次數
        int operationCount = 0;
        for (int i = 0; i < n; i++)
        {
            // 累加得到當前位置的操作次數
            operationCount += diff[i];
            // 檢查操作次數是否足夠將原值變為 0
            // 如果原始值大於可減去的次數，則無法變成 0
            if (nums[i] > operationCount)
            {
                // 該元素無法變成 0，直接返回 false
                return false;
            }
        }
        // 所有元素都可以變成 0，返回 true
        return true;
    }


    /// <summary>
    /// 判斷是否可以將陣列轉換為全零陣列 (方法二)
    /// 
    /// 解題思路：
    /// 本方法採用差分陣列的逆向思考，先將原始陣列轉換為其差分陣列表示形式，再檢查操作後是否能將所有元素變為0。
    /// 
    /// 差分陣列與前綴和的關係：
    /// 1. 對於原始陣列 nums，其差分陣列 diff 的首項為 nums[0]
    /// 2. 後續每一項 diff[i] = nums[i] - nums[i-1]，表示相鄰元素間的差值
    /// 3. 透過這種表示，原始陣列的任何區間操作都能轉換為差分陣列的端點操作
    /// 
    /// 操作處理策略：
    /// 1. 將原始陣列轉為差分形式，表示每個位置的變化量
    /// 2. 對於每個查詢區間 [l,r]，我們在左邊界 l 減 1（表示從此位置開始減少），在右邊界後 r+1 加 1（表示此處恢復）
    /// 3. 通過累加差分陣列，檢查每個累加結果是否小於等於0，如果有大於0的情況，表示該處無法變為0
    /// 
    /// 時間複雜度：O(n + q)，其中 n 是陣列長度，q 是查詢操作數量
    /// 空間複雜度：O(n)，用於儲存差分陣列
    /// </summary>
    /// <param name="nums">原始整數陣列，包含需要轉換為 0 的元素</param>
    /// <param name="queries">查詢操作陣列，每個查詢為 [l,r] 表示對區間 [l,r] 執行減 1 操作</param>
    /// <returns>如果可以通過給定的操作將所有元素變為 0 則返回 true，否則返回 false</returns>
    public static bool IsZeroArray2(int[] nums, int[][] queries)
    {
        int n = nums.Length;
        // 建立差分陣列表示形式，將原陣列轉為差分形式
        int[] diff = new int[n + 1];
        // 初始化差分陣列的第一個元素為原陣列的第一個值
        diff[0] = nums[0];
        // 計算差分陣列，每個元素為相鄰元素的差值
        for (int i = 1; i < n; i++)
        {
            diff[i] = nums[i] - nums[i - 1];
        }
        // 遍歷每個查詢操作，更新差分陣列
        foreach (int[] q in queries)
        {
            // 在左邊界減 1，表示從此處開始減少元素值
            diff[q[0]]--;
            // 在右邊界之後加 1，表示此處恢復，不再減少
            diff[q[1] + 1]++;
        }

        if (diff[0] > 0)
        {
            // 第一個元素無法變為 0，直接返回 false
            return false;
        }
        // 檢查每個位置是否可以轉換為 0
        for (int i = 1; i < n; i++)
        {
            // 累加差分值還原為原陣列經過操作後的結果
            diff[i] += diff[i - 1];
            // 如果結果大於 0，表示該位置無法變為 0
            if (diff[i] > 0)
            {
                return false;
            }
        }
        // 所有元素都可以變成 0，返回 true
        return true;
    }


    /// <summary>
    /// 判斷是否可以將陣列轉換為全零陣列 (方法三)
    /// 
    /// 解題思路：
    /// 使用差分陣列技術直接計算每個位置的操作次數。對於每個查詢區間 [l,r]，在差分陣列 deltaArray[l] 
    /// 位置加 1，並在 deltaArray[r+1] 位置減 1。透過前綴和計算得到每個位置的操作總次數，
    /// 然後與原始陣列對應位置的值進行比較，判斷是否可以將每個元素變為0。
    /// 
    /// 差分陣列的特性：
    /// 1. 對區間 [l,r] 加上一個值 v，只需要在差分陣列上做兩次操作：d[l] += v 和 d[r+1] -= v
    /// 2. 通過對差分陣列求前綴和，可以還原出原陣列的變化
    /// 
    /// 實作差異：
    /// 1. 與方法一不同，此方法使用一個額外的陣列 operationCounts 來儲存每個位置的操作次數
    /// 2. 較明確地區分差分陣列與前綴和計算的過程，使邏輯更加清晰
    /// 3. 提高了程式碼的可讀性，犧牲一些額外的空間複雜度
    /// 
    /// 時間複雜度：O(n + q)，其中 n 是陣列長度，q 是查詢操作數量
    /// 空間複雜度：O(n)，用於存儲差分陣列和操作次數陣列
    /// </summary>
    /// <param name="nums">原始整數陣列，包含需要轉換為 0 的元素</param>
    /// <param name="queries">查詢操作陣列，每個查詢為 [l,r] 表示對區間 [l,r] 執行減 1 操作</param>
    /// <returns>如果可以通過給定的操作將所有元素變為 0 則返回 true，否則返回 false</returns>
    public bool IsZeroArray3(int[] nums, int[][] queries)
    {
        // 建立差分陣列，用於記錄區間操作
        int[] deltaArray = new int[nums.Length + 1];
        // 遍歷每個查詢操作
        foreach (int[] query in queries)
        {
            // 在差分陣列的左邊界增加1次操作
            deltaArray[query[0]]++;
            // 在差分陣列的右邊界後減少1次操作，維持差分陣列特性
            deltaArray[query[1] + 1]--;
        }
        // 計算每個位置的操作總次數
        int[] operationCounts = new int[deltaArray.Length];
        int currentOperations = 0;
        for (int i = 0; i < deltaArray.Length; i++)
        {
            // 累加差分值得到當前位置的操作次數
            currentOperations += deltaArray[i];
            operationCounts[i] = currentOperations;
        }
        // 檢查每個位置是否可以轉換為0
        for (int i = 0; i < nums.Length; i++)
        {
            // 如果操作次數小於原始值，則無法將該元素變為0
            if (operationCounts[i] < nums[i])
            {
                // 找到無法變為0的元素，返回false
                return false;
            }
        }
        // 所有元素都可以變成0，返回true
        return true;
    }

}
