namespace leetcode_3201;

class Program
{
    /// <summary>
    /// 3201. Find the Maximum Length of Valid Subsequence I
    /// https://leetcode.com/problems/find-the-maximum-length-of-valid-subsequence-i/description/
    /// 3201. 找出有效子序列的最大长度 I
    /// https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-i/description/?envType=daily-question&envId=2025-07-16
    /// 
    /// 給定一個整數陣列 nums。
    /// 
    /// 有一個長度為 x 的 nums 子序列被稱為有效，若滿足：
    /// (sub[0] + sub[1]) % 2 == (sub[1] + sub[2]) % 2 == ... == (sub[x - 2] + sub[x - 1]) % 2。
    /// 
    /// 請回傳 nums 最長有效子序列的長度。
    /// 
    /// 子序列是可以從原陣列刪除部分元素（或不刪除）且不改變剩餘元素順序所得到的陣列。
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        // 測試資料 1
        int[] nums1 = { 1, 2, 3, 4 };
        Console.WriteLine($"Input: [1,2,3,4]  Output: {new Program().MaximumLength(nums1)}");
        Console.WriteLine($"Input: [1,2,3,4]  MaximumLengthEnum Output: {new Program().MaximumLengthEnum(nums1)}"); // MaximumLengthEnum 測試

        // 測試資料 2
        int[] nums2 = { 1, 2, 1, 1, 2, 1, 2 };
        Console.WriteLine($"Input: [1,2,1,1,2,1,2]  Output: {new Program().MaximumLength(nums2)}");
        Console.WriteLine($"Input: [1,2,1,1,2,1,2]  MaximumLengthEnum Output: {new Program().MaximumLengthEnum(nums2)}"); // MaximumLengthEnum 測試

        // 測試資料 3
        int[] nums3 = { 1, 3 };
        Console.WriteLine($"Input: [1,3]  Output: {new Program().MaximumLength(nums3)}");
        Console.WriteLine($"Input: [1,3]  MaximumLengthEnum Output: {new Program().MaximumLengthEnum(nums3)}"); // MaximumLengthEnum 測試
    }

 
    /// <summary>
    /// 方法一：動態規劃 - 考察子序列的最後兩項
    /// 
    /// 解題思路：
    /// 1. 數學分析：
    ///    對於等式 (a+b) % k = (b+c) % k，根據模運算性質可以移項得到：
    ///    (a+b-(b+c)) % k = 0，化簡為 (a-c) % k = 0
    ///    這意味著 a 與 c 關於模 k 同餘，即 sub[i] 與 sub[i+2] 關於模 k 同餘。
    /// 
    /// 2. 問題轉換：
    ///    有效子序列的偶數項 sub[0],sub[2],sub[4],... 都關於模 k 同餘
    ///    奇數項 sub[1],sub[3],sub[5],... 都關於模 k 同餘
    ///    問題等價於：求最長子序列的長度，該子序列的奇數項都相同，偶數項都相同。
    /// 
    /// 3. 動態規劃策略：
    ///    維護二維陣列 f[y, x]，表示最後兩項模 k 分別為 y 和 x 的子序列長度。
    ///    遍歷過程範例（nums=[1,2,1,2,1,2]）：
    ///    - 遍歷到 1 時：在「末尾為 1,2 的子序列」末尾添加 1，得到「末尾為 2,1 的子序列」
    ///    - 遍歷到 2 時：在「末尾為 2,1 的子序列」末尾添加 2，得到「末尾為 1,2 的子序列」
    /// 
    /// 4. 狀態轉移方程：
    ///    對於 x = nums[i] % k，在「最後兩項模 k 分別為 x 和 y 的子序列」末尾添加 nums[i]
    ///    則「最後兩項模 k 分別為 y 和 x 的子序列」長度增加 1：
    ///    f[y, x] = f[x, y] + 1
    /// 
    /// <example>
    /// <code>
    /// int[] nums = { 1, 2, 1, 2, 1, 2 };
    /// int result = new Program().MaximumLength(nums); // result = 6
    /// </code>
    /// </example>
    /// 
    /// 時間複雜度：O(n × k)，其中 n 為陣列長度，k = 2
    /// 空間複雜度：O(k²)，即 O(1)
    /// 
    /// 參考：
    /// https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-i/solutions/2826593/deng-jie-zhuan-huan-dong-tai-gui-hua-pyt-7l4b/?envType=daily-question&envId=2025-07-16
    /// https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-ii/solutions/2826591/deng-jie-zhuan-huan-dong-tai-gui-hua-pyt-z2fs/
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <returns>最長有效子序列長度</returns>
    public int MaximumLength(int[] nums)
    {
        int k = 2; // 題目要求模 2，只考慮餘數 0（偶數）和 1（奇數）
        
        // 邊界檢查：處理空陣列或 null 的情況
        if (nums is null || nums.Length == 0)
        {
            return 0;
        }
        
        int ans = 0; // 記錄所有可能子序列的最大長度
        
        // f[y, x]: 表示最後兩項模 k 分別為 y 和 x 的子序列長度
        // 例如：f[0, 1] 表示倒數第二項為偶數、最後一項為奇數的子序列長度
        int[,] f = new int[k, k]; 
        
        // 遍歷原陣列，逐一處理每個元素
        foreach (var num in nums)
        {
            int x = num % k; // 計算目前元素對 k 的餘數（0 或 1）
            
            // 對於每個可能的倒數第二項餘數 y，更新狀態
            for (int y = 0; y < k; y++)
            {
                // 核心狀態轉移：
                // 概念上：在「最後兩項模 k 分別為 x 和 y」的子序列末尾加上目前元素 num
                // 實際上：我們只關心子序列的「長度」，不關心具體元素值
                // 
                // 解釋：f[x, y] 表示「最後兩項模 k 分別為 x 和 y」的子序列長度
                //      當我們在這個子序列末尾添加一個模 k 為 x 的元素時，
                //      新子序列變成「最後兩項模 k 分別為 y 和 x」，長度增加 1
                // 
                // 為什麼是 +1 而不是 +num？
                // 因為 f 陣列儲存的是「長度」，不是「元素總和」
                // 每添加一個元素，無論元素值是多少，長度都只增加 1
                f[y, x] = f[x, y] + 1;
                
                // 更新全域最大長度
                ans = Math.Max(ans, f[y, x]);
            }
        }
        
        // 回傳所有可能子序列中的最大長度
        return ans;
        
        /* 
         * 狀態轉移詳細範例說明：
         * 
         * 假設 nums = [1, 2, 3]，k = 2
         * 
         * 初始狀態：f = [[0,0], [0,0]]
         * 
         * 處理 num = 1 (x = 1 % 2 = 1)：
         *   y=0: f[0,1] = f[1,0] + 1 = 0 + 1 = 1  // 表示「倒數第二項為0，最後一項為1」的子序列長度為1
         *   y=1: f[1,1] = f[1,1] + 1 = 0 + 1 = 1  // 表示「倒數第二項為1，最後一項為1」的子序列長度為1
         * 
         * 處理 num = 2 (x = 2 % 2 = 0)：
         *   y=0: f[0,0] = f[0,0] + 1 = 0 + 1 = 1  // 子序列 [2]
         *   y=1: f[1,0] = f[0,1] + 1 = 1 + 1 = 2  // 子序列 [1,2]，長度為2
         * 
         * 處理 num = 3 (x = 3 % 2 = 1)：
         *   y=0: f[0,1] = f[1,0] + 1 = 2 + 1 = 3  // 子序列 [1,2,3]，長度為3
         *   y=1: f[1,1] = f[1,1] + 1 = 1 + 1 = 2  // 其他可能的子序列
         * 
         * 關鍵理解：
         * 1. f[y,x] 儲存的是「長度」，不是元素值的總和
         * 2. 每次添加元素時，長度增加1，所以是 +1 而不是 +num
         * 3. 我們關心的是「有多少個元素」，不是「元素值是多少」
         */
    }

    /// <summary>
    /// 方法二：枚舉元素的奇偶性
    /// 
    /// 解題思路：
    /// 
    /// 1. 核心觀察：
    ///    根據有效子序列的定義 (sub[i] + sub[i+1]) % 2 恆定，可以推導出：
    ///    - 若相鄰元素和為偶數，則兩者奇偶性相同（都奇數或都偶數）
    ///    - 若相鄰元素和為奇數，則兩者奇偶性不同（一奇一偶）
    ///    - 子序列中奇數位置（index 1,3,5...）元素奇偶性相同
    ///    - 子序列中偶數位置（index 0,2,4...）元素奇偶性相同
    /// 
    /// 2. 四種可能的奇偶性模式：
    ///    - 模式 1 {0,0}：全為偶數（奇數下標偶數，偶數下標偶數）
    ///    - 模式 2 {0,1}：奇數下標偶數，偶數下標奇數（交替模式）
    ///    - 模式 3 {1,0}：奇數下標奇數，偶數下標偶數（交替模式）
    ///    - 模式 4 {1,1}：全為奇數（奇數下標奇數，偶數下標奇數）
    /// 
    /// 3. 貪心策略：
    ///    對每種模式，遍歷原陣列，若目前元素符合該位置的奇偶性要求，
    ///    就貪心地加入子序列（子序列長度 +1）。
    /// 
    /// 4. 範例分析：
    ///    nums = [1,2,1,1,2,1,2]
    ///    - 模式 {0,1}：期望序列為 [偶,奇,偶,奇,...]
    ///      遍歷：1(奇,位置0,不符) → 2(偶,位置0,符合,cnt=1) → 1(奇,位置1,符合,cnt=2) 
    ///            → 1(奇,位置2需偶,不符) → 2(偶,位置2,符合,cnt=3) → 1(奇,位置3,符合,cnt=4) 
    ///            → 2(偶,位置4,符合,cnt=5)
    ///      結果：長度為 5 的子序列 [2,1,2,1,2]
    /// 
    /// <example>
    /// <code>
    /// 測試範例 1：交替模式
    /// int[] nums1 = { 1, 2, 1, 2, 1, 2 };
    /// int result1 = new Program().MaximumLengthEnum(nums1); // result = 6
    /// 
    /// 測試範例 2：混合模式
    /// int[] nums2 = { 1, 2, 1, 1, 2, 1, 2 };
    /// int result2 = new Program().MaximumLengthEnum(nums2); // result = 5
    /// </code>
    /// </example>
    /// 
    /// 時間複雜度：O(4n) = O(n)，其中 n 為陣列長度，需遍歷 4 次
    /// 空間複雜度：O(1)，只使用常數額外空間
    /// 
    /// 參考：
    /// https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-i/solutions/3717152/zhao-chu-you-xiao-zi-xu-lie-de-zui-da-ch-1n3j/?envType=daily-question&envId=2025-07-16
    /// 
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <returns>最長有效子序列長度</returns>
    public int MaximumLengthEnum(int[] nums)
    {
        // 邊界檢查：處理空陣列或 null 的情況
        if (nums is null || nums.Length == 0)
        {
            return 0;
        }
        
        // res 用來記錄所有模式下的最大長度
        int res = 0;
        
        // patterns 定義四種奇偶性模式：
        // patterns[i, 0] 表示子序列偶數位置（第0,2,4...個元素）應有的奇偶性
        // patterns[i, 1] 表示子序列奇數位置（第1,3,5...個元素）應有的奇偶性
        // 0 代表偶數，1 代表奇數
        int[,] patterns = new int[4, 2] 
        { 
            { 0, 0 }, // 模式1：全為偶數 (偶數位置=偶數, 奇數位置=偶數)
            { 0, 1 }, // 模式2：偶數位置為偶數，奇數位置為奇數 (交替：偶奇偶奇...)
            { 1, 0 }, // 模式3：偶數位置為奇數，奇數位置為偶數 (交替：奇偶奇偶...)
            { 1, 1 }  // 模式4：全為奇數 (偶數位置=奇數, 奇數位置=奇數)
        };
        
        // 枚舉四種奇偶性模式
        for (int i = 0; i < 4; i++)
        {
            int cnt = 0; // 記錄目前模式下的子序列長度
            
            // 遍歷 nums 陣列中的每個元素
            foreach (int num in nums)
            {
                // 核心邏輯：判斷目前元素 num 是否符合當前模式下的奇偶性要求
                
                // cnt % 2 計算目前子序列的「位置索引」：
                // - cnt % 2 == 0：表示這是子序列的偶數位置（第0,2,4...個元素）
                // - cnt % 2 == 1：表示這是子序列的奇數位置（第1,3,5...個元素）
                
                // patterns[i, cnt % 2] 獲取該位置應有的奇偶性：
                // - patterns[i, 0]：偶數位置應有的奇偶性
                // - patterns[i, 1]：奇數位置應有的奇偶性
                
                // num % 2 計算目前元素的奇偶性：
                // - num % 2 == 0：目前元素為偶數
                // - num % 2 == 1：目前元素為奇數
                
                if (num % 2 == patterns[i, cnt % 2])
                {
                    // 若目前元素的奇偶性符合模式要求，則將其加入子序列
                    // 貪心策略：只要符合就立即加入，子序列長度加一
                    cnt++;
                }
                
                // 如果不符合，則跳過該元素，繼續檢查下一個元素
                // 這體現了貪心的特性：我們總是嘗試延長目前的子序列
            }
            
            // 更新全域最大長度
            // 在所有模式中記錄最長的子序列長度
            res = Math.Max(res, cnt);
        }
        
        // 回傳所有模式下的最大長度
        return res;
        
        /*
         * 演算法運作範例：
         * 
         * 輸入：nums = [1, 2, 1, 1, 2, 1, 2]
         * 
         * 模式 0 {0,0} - 全偶數：
         *   遍歷：1(奇,不符) → 2(偶,符合,cnt=1) → 1(奇,不符) → 1(奇,不符) → 2(偶,符合,cnt=2) → 1(奇,不符) → 2(偶,符合,cnt=3)
         *   結果：cnt = 3
         * 
         * 模式 1 {0,1} - 偶奇交替：
         *   遍歷：1(奇,位置0需偶,不符) → 2(偶,位置0需偶,符合,cnt=1) → 1(奇,位置1需奇,符合,cnt=2) 
         *         → 1(奇,位置2需偶,不符) → 2(偶,位置2需偶,符合,cnt=3) → 1(奇,位置3需奇,符合,cnt=4) 
         *         → 2(偶,位置4需偶,符合,cnt=5)
         *   結果：cnt = 5，對應子序列 [2,1,2,1,2]
         * 
         * 模式 2 {1,0} - 奇偶交替：
         *   遍歷：1(奇,位置0需奇,符合,cnt=1) → 2(偶,位置1需偶,符合,cnt=2) → 1(奇,位置2需奇,符合,cnt=3) 
         *         → 1(奇,位置3需偶,不符) → 2(偶,位置3需偶,符合,cnt=4) → 1(奇,位置4需奇,符合,cnt=5) 
         *         → 2(偶,位置5需偶,符合,cnt=6)
         *   但這個分析有誤，實際上位置3時 cnt=3，需要奇數，1符合，所以 cnt=4
         *   正確遍歷：1(符合,cnt=1) → 2(符合,cnt=2) → 1(符合,cnt=3) → 1(不符) → 2(符合,cnt=4) → 1(不符) → 2(符合,cnt=5)
         *   結果：cnt = 5
         * 
         * 模式 3 {1,1} - 全奇數：
         *   遍歷：1(奇,符合,cnt=1) → 2(偶,不符) → 1(奇,符合,cnt=2) → 1(奇,符合,cnt=3) → 2(偶,不符) → 1(奇,符合,cnt=4) → 2(偶,不符)
         *   結果：cnt = 4
         * 
         * 最終答案：max(3, 5, 5, 4) = 5
         */
    }
}
