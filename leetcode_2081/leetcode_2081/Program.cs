namespace leetcode_2081;

class Program
{
    /// <summary>
    /// 2081. Sum of k-Mirror Numbers
    /// https://leetcode.com/problems/sum-of-k-mirror-numbers/description/?envType=daily-question&envId=2025-06-23
    /// 2081. k 镜像数字的和
    /// https://leetcode.cn/problems/sum-of-k-mirror-numbers/description/?envType=daily-question&envId=2025-06-23
    /// 
    /// 給定一個進位制 k 和數字 n，k-鏡像數是指在十進位和 k 進位下都為回文數（正著和反著讀都一樣，且不能有前導零）的正整數。
    /// 請回傳最小的 n 個 k-鏡像數的總和。
    /// 例如：9 是 2-鏡像數，因為 9（十進位）和 1001（二進位）都是回文數。
    /// 但 4 不是 2-鏡像數，因為 4（二進位為 100）不是回文數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("=== LeetCode 2081: Sum of k-Mirror Numbers ===");
        
        Program program = new Program();
        
        // 測試範例
        int k = 2, n = 5;
        
        Console.WriteLine($"測試：k = {k}, n = {n}");
        
        // 方法一：逐步生成
        DateTime start1 = DateTime.Now;
        long result1 = program.KMirror(k, n);
        TimeSpan time1 = DateTime.Now - start1;
        Console.WriteLine($"方法一結果：{result1}，執行時間：{time1.TotalMilliseconds:F2} ms");
        
        // 方法二：預計算
        DateTime start2 = DateTime.Now;
        long result2 = program.KMirror2(k, n);
        TimeSpan time2 = DateTime.Now - start2;
        Console.WriteLine($"方法二結果：{result2}，執行時間：{time2.TotalMilliseconds:F2} ms");
        
        Console.WriteLine($"結果一致：{result1 == result2}");
    }

    // 用於存儲數字在 k 進位下的各位數字，最多支援 100 位
    private readonly int[] digit = new int[100];

    /// <summary>
    /// 計算最小的 n 個 k-鏡像數的總和
    /// 
    /// 解題思路：
    /// 1. k-鏡像數必須同時在十進位和 k 進位下都是回文數
    /// 2. 由於十進位回文數的數量遠少於所有數字，我們先產生十進位回文數，再檢查它們是否也是 k 進位回文數
    /// 3. 按長度遞增的順序產生十進位回文數：1位、2位、3位...
    /// 4. 對於每個長度，分別產生奇數長度和偶數長度的回文數
    /// 5. 檢查每個十進位回文數是否也是 k 進位回文數，如果是則加入結果
    /// 
    /// 演算法流程：
    /// - 使用 left 變數追蹤當前生成回文數的左半部分起始值
    /// - 對每個位數長度，分別生成奇數長度和偶數長度的回文數
    /// - 透過反轉左半部分來建構完整的回文數
    /// - 檢查建構出的十進位回文數是否也是 k 進位回文數
    /// 
    /// ref:https://leetcode.cn/problems/sum-of-k-mirror-numbers/solutions/1115277/k-jing-xiang-shu-zi-de-he-by-leetcode-so-nyos/?envType=daily-question&envId=2025-06-23
    /// 
    /// </summary>
    /// <param name="k">進位制基數</param>
    /// <param name="n">需要找到的 k-鏡像數個數</param>
    /// <returns>最小的 n 個 k-鏡像數的總和</returns>
    public long KMirror(int k, int n)
    {
        int left = 1, count = 0;  // left：當前生成回文數的左半部分起始值，count：已找到的k-鏡像數個數
        long sum = 0;             // 累計k-鏡像數的總和

        // 持續搜尋直到找到 n 個 k-鏡像數
        while (count < n)
        {
            int right = left * 10;  // 當前位數範圍的上界

            // op = 0 表示產生奇數長度回文數，op = 1 表示產生偶數長度回文數
            for (int op = 0; op < 2; op++)
            {
                // 枚舉左半部分的所有可能值
                for (int i = left; i < right && count < n; i++)
                {
                    long combined = i;  // 從左半部分開始建構完整的回文數

                    // 決定需要反轉的部分
                    // 奇數長度：去掉中間位數 (i / 10)，因為中間位數不需要重複
                    // 偶數長度：完整反轉 (i)，所有位數都需要反轉
                    int x = (op == 0 ? i / 10 : i);

                    // 將左半部分反轉並接在後面，形成完整的回文數
                    // 例如：123 -> 12321 (奇數) 或 123 -> 123321 (偶數)
                    while (x > 0)
                    {
                        combined = combined * 10 + x % 10;  // 將 x 的最後一位接到 combined 後面
                        x /= 10;  // 移除 x 的最後一位
                    }

                    // 檢查這個十進位回文數是否也是 k 進位回文數
                    if (IsPalindrome(combined, k))
                    {
                        sum += combined;  // 累加到總和
                        count++;          // 增加已找到的數量
                    }
                }
            }
            left = right;  // 移動到下一個位數範圍
        }
        return sum;
    }

    /// <summary>
    /// 檢查一個數字在指定進位制下是否為回文數
    /// 
    /// 方法：
    /// 1. 將數字轉換為 k 進位制的各位數字
    /// 2. 使用雙指標從兩端向中間比較
    /// 3. 如果所有對應位置的數字都相等，則為回文數
    /// </summary>
    /// <param name="x">要檢查的數字</param>
    /// <param name="k">進位制基數</param>
    /// <returns>如果是 k 進位回文數則回傳 true，否則回傳 false</returns>
    private bool IsPalindrome(long x, int k)
    {
        int length = -1;  // 記錄位數長度（從 0 開始計算）        
        // 將數字轉換為 k 進位制，並存儲各位數字
        while (x > 0)
        {
            length++;                        // 增加位數計數
            digit[length] = (int)(x % k);    // 取得最低位並存儲
            x /= k;                          // 移除最低位
        }

        // 使用雙指標檢查是否為回文數
        // i 從最低位開始，j 從最高位開始，向中間移動
        for (int i = 0, j = length; i < j; i++, j--)
        {
            if (digit[i] != digit[j])  // 如果對應位置的數字不相等
            {
                return false;          // 不是回文數
            }
        }
        return true;  // 所有對應位置都相等，是回文數
    }
    

    // ==================== 方法二：預計算 + 前綴和優化 ====================
    
    private static readonly int MAX_N = 30;
    private static readonly List<long>[] ans = new List<long>[10];
    private static bool initialized = false;

    /// <summary>
    /// 初始化預計算所有 k-鏡像數
    /// 
    /// 優化策略：
    /// 1. 預先計算所有需要的 k-鏡像數並存儲
    /// 2. 使用前綴和快速計算區間總和
    /// 3. 查詢時直接返回結果，時間複雜度 O(1)
    /// 
    /// 這種方法比每次都重新計算要快很多，特別適合多次查詢的場景
    /// </summary>
    private void Init()
    {
        if (initialized)
        {
            return;  // 避免重複初始化
        }
        initialized = true;

        // 初始化所有 k 值對應的列表
        for (int i = 0; i < ans.Length; i++)
        {
            ans[i] = new List<long>();
        }

        // 按位數遞增生成十進位回文數
        for (int baseNum = 1; ; baseNum *= 10)
        {
            // 生成奇數長度回文數，例如 baseNum = 10，生成範圍是 101 ~ 999
            for (int i = baseNum; i < baseNum * 10; i++)
            {
                long x = i;  // 從左半部分開始建構
                
                // 反轉左半部分（去掉中間位數）接在後面
                for (int t = i / 10; t > 0; t /= 10)
                {
                    x = x * 10 + t % 10;
                }
                
                // 檢查這個回文數是否滿足所有 k 值的要求
                if (DoPalindrome(x))
                {
                    return;  // 所有 k 值都已收集到足夠數量
                }
            }
            
            // 生成偶數長度回文數，例如 baseNum = 10，生成範圍是 1001 ~ 9999
            for (int i = baseNum; i < baseNum * 10; i++)
            {
                long x = i;  // 從左半部分開始建構
                
                // 反轉完整的左半部分接在後面
                for (int t = i; t > 0; t /= 10)
                {
                    x = x * 10 + t % 10;
                }
                
                // 檢查這個回文數是否滿足所有 k 值的要求
                if (DoPalindrome(x))
                {
                    return;  // 所有 k 值都已收集到足夠數量
                }
            }
        }
    }

    /// <summary>
    /// 處理一個十進位回文數，檢查它在各個進位制下是否也是回文數
    /// </summary>
    /// <param name="x">十進位回文數</param>
    /// <returns>如果所有 k 值都已收集到足夠數量則回傳 true</returns>
    private bool DoPalindrome(long x)
    {
        bool done = true;
        
        // 檢查該數字在 k=2 到 k=9 各個進位制下是否為回文數
        for (int k = 2; k < 10; k++)
        {
            // 如果該 k 值還需要更多數字，且 x 在 k 進位下是回文數
            if (ans[k].Count < MAX_N && IsKPalindrome(x, k))
            {
                ans[k].Add(x);  // 加入該 k 值的結果列表
            }
            
            // 檢查該 k 值是否還需要更多數字
            if (ans[k].Count < MAX_N)
            {
                done = false;
            }
        }
        
        // 如果還有 k 值沒收集夠，繼續搜尋
        if (!done)
        {
            return false;
        }

        // 所有 k 值都已收集夠數量，轉換為前綴和以快速查詢
        for (int k = 2; k < 10; k++)
        {
            // 原地計算前綴和，ans[k][i] 表示前 i+1 個 k-鏡像數的總和
            List<long> s = ans[k];
            for (int i = 1; i < MAX_N; i++)
            {
                s[i] = s[i] + s[i - 1];
            }
        }
        return true;
    }

    /// <summary>
    /// 優化版本的 k 進位回文數檢查
    /// 
    /// 基於 LeetCode 9. 回文數的優化方法：
    /// 只反轉數字的一半進行比較，避免溢出問題
    /// 
    /// 特殊處理：
    /// - 如果數字能被 k 整除（末位為0），則不是有效的 k 進位回文數
    /// - 因為這會導致前導零的問題
    /// </summary>
    /// <param name="x">要檢查的數字</param>
    /// <param name="k">進位制基數</param>
    /// <returns>如果是 k 進位回文數則回傳 true</returns>
    private bool IsKPalindrome(long x, int k)
    {
        // 如果數字能被 k 整除，則在 k 進位下以 0 結尾，不是有效回文數
        if (x % k == 0)
        {
            return false;
        }
        
        long rev = 0;  // 反轉的後半部分
        
        // 只反轉一半的數字進行比較
        // 當 rev >= x / k 時，表示已經處理了一半或超過一半的位數
        while (rev < x / k)
        {
            rev = rev * k + x % k;  // 將當前最低位加到 rev 的最高位
            x /= k;                 // 移除當前最低位
        }
        
        // 比較兩種情況：
        // 1. rev == x：偶數位數的情況
        // 2. rev == x / k：奇數位數的情況（去掉中間位數）
        return rev == x || rev == x / k;
    }

    /// <summary>
    /// 方法二：使用預計算結果快速查詢
    /// 
    /// 時間複雜度：O(1) 查詢，O(預計算) 初始化
    /// 空間複雜度：O(MAX_N * 8) 存儲所有結果
    /// 
    /// ref:https://leetcode.cn/problems/sum-of-k-mirror-numbers/solutions/1113431/da-biao-zuo-fa-by-endlesscheng-7ojf/?envType=daily-question&envId=2025-06-23
    /// 
    /// </summary>
    /// <param name="k">進位制基數</param>
    /// <param name="n">需要找到的 k-鏡像數個數</param>
    /// <returns>最小的 n 個 k-鏡像數的總和</returns>
    public long KMirror2(int k, int n)
    {
        Init();  // 確保已初始化
        return ans[k][n - 1];  // 直接回傳前綴和結果
    }
}
