namespace leetcode_1356;

class Program
{
    /// <summary>
    /// <summary>
    /// 1356. Sort Integers by The Number of 1 Bits
    /// Problem description:
    /// You are given an integer array arr. Sort the integers in the array
    /// in ascending order by the number of 1's in their binary representation
    /// and in case of two or more integers have the same number of 1's you
    /// have to sort them in ascending order.
    /// <para/>
    /// Return the array after sorting it.
    ///
    /// 題目說明（繁體中文）:
    /// 給定一個整數陣列 arr。請根據每個整數二進位表示中 1 的數量，以遞增順序對陣列進行排序，
    /// 若兩個或多個整數具有相同的 1 的數量，則按數值大小升序排序。
    ///
    /// 傳回排序後的陣列。
    ///
    /// 參考連結：https://leetcode.com/problems/sort-integers-by-the-number-of-1-bits/
    /// 及中文版：https://leetcode.cn/problems/sort-integers-by-the-number-of-1-bits/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1：官方範例
        // 輸入: [0,1,2,3,4,5,6,7,8]
        // 期望: [0,1,2,4,8,3,5,6,7]
        int[] arr1 = [0, 1, 2, 3, 4, 5, 6, 7, 8];
        int[] result1 = solution.SortByBits(arr1);
        Console.WriteLine("測試 1: " + string.Join(", ", result1));
        // => 0, 1, 2, 4, 8, 3, 5, 6, 7

        // 測試案例 2：官方範例
        // 輸入: [1024,512,256,128,64,32,16,8,4,2,1]
        // 期望: [1,2,4,8,16,32,64,128,256,512,1024]
        int[] arr2 = [1024, 512, 256, 128, 64, 32, 16, 8, 4, 2, 1];
        int[] result2 = solution.SortByBits(arr2);
        Console.WriteLine("測試 2: " + string.Join(", ", result2));
        // => 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024

        // 測試 PopCount（Brian Kernighan 演算法）
        Console.WriteLine($"PopCount(7)  = {PopCount(7)}");   // 7  = 111₂ => 3
        Console.WriteLine($"PopCount(10) = {PopCount(10)}");  // 10 = 1010₂ => 2

        // 測試 PopCount2（逐位相除法）
        Console.WriteLine($"PopCount2(7)  = {PopCount2(7)}");  // => 3
        Console.WriteLine($"PopCount2(10) = {PopCount2(10)}"); // => 2
    }

    /// <summary>
    /// 解法：先升序排序，再依 1-bit 數量分桶（Bucket Sort 概念）。
    ///
    /// 解題思路：
    ///   1. 先對原始陣列做升序排序，確保同一桶內的元素已依數值排好。
    ///   2. 使用 <see cref="SortedDictionary{TKey,TValue}"/> 以「二進制中 1 的個數」為 key，
    ///      對應的整數列表為 value，自動依 key 升序維護桶的順序。
    ///   3. 遍歷排序後陣列，將每個元素依其 1-bit 數量放入對應的桶。
    ///   4. 依序從 key 最小的桶取出元素，填入結果陣列，即可得到排序結果。
    ///
    /// 時間複雜度：O(n log n)（排序主導），空間複雜度：O(n)（桶空間）
    ///
    /// 範例：
    ///   輸入: [0,1,2,3,4,5,6,7,8]
    ///   桶（key=0）: [0]
    ///   桶（key=1）: [1,2,4,8]
    ///   桶（key=2）: [3,5,6]
    ///   桶（key=3）: [7]
    ///   輸出: [0,1,2,4,8,3,5,6,7]
    /// </summary>
    /// <param name="arr">待排序的整數陣列</param>
    /// <returns>依 1-bit 數量升序（同數量時依數值升序）排列的陣列</returns>
    public int[] SortByBits(int[] arr)
    {
        // 步驟 1：先對原陣列升序排序，確保同桶內數值由小到大
        Array.Sort(arr);

        // 步驟 2：SortedDictionary 以 1-bit 個數為 key，自動維持 key 升序
        //   key  : 二進制中 1 的個數
        //   value: 該 1-bit 數量對應的數值列表（已因前面 Sort 而升序）
        var dict = new SortedDictionary<int, List<int>>();

        for(int i = 0; i < arr.Length; i++)
        {
            // 計算目前元素的 1-bit 數量，作為桶的索引
            int key = PopCount2(arr[i]);

            if(dict.ContainsKey(key))
            {
                // 桶已存在，直接加入
                dict[key].Add(arr[i]);
            }
            else
            {
                // 建立新桶
                dict.Add(key, new List<int>() { arr[i] });
            }
        }

        // 步驟 3：依 key 升序（SortedDictionary 保證）逐一取出元素，填入結果陣列
        var ret = new int[arr.Length];
        int idx = 0;

        foreach(var kvp in dict)
        {
            foreach(var num in kvp.Value)
            {
                ret[idx++] = num;
            }
        }

        return ret;
    }

    /// <summary>
    /// 計算整數 n 的二進制表示中，1 的個數（Hamming Weight / Popcount）。
    ///
    /// 演算法：Brian Kernighan's Bit Trick
    ///   核心觀察：n &amp; (n - 1) 每次執行都會清除 n 的最低有效位元（lowest set bit）。
    ///   例如：
    ///     n = 1100₂  (12)      n - 1 = 1011₂  (11)
    ///     n & (n-1) = 1000₂  (8)   ← 最低的 1 被清除
    ///     n = 1000₂  (8)       n - 1 = 0111₂  (7)
    ///     n & (n-1) = 0000₂  (0)   ← 最低的 1 被清除
    ///     共執行 2 次 => 12 的二進制有 2 個 1
    ///
    /// 優點：迴圈次數等於 1-bit 的個數，比逐位檢查更快。
    /// 時間複雜度：O(k)，k 為 1-bit 個數；空間複雜度：O(1)
    /// </summary>
    /// <param name="n">非負整數</param>
    /// <returns>n 的二進制中 1 的個數</returns>
    public static int PopCount(int n)
    {
        int counter = 0;

        while(n > 0)
        {
            counter++;         // 發現一個 1-bit
            n = n & (n - 1);   // 清除最低有效位元（lowest set bit）
        }

        return counter;
    }


    /// <summary>
    /// 計算整數 n 的二進制表示中，1 的個數（Hamming Weight / Popcount）。
    ///
    /// 演算法：逐位相除法（Division / Modulo）
    ///   原理：透過不斷對 2 取餘數與整除，模擬手動轉換二進制的過程。
    ///   每次 n % 2 取得最低位元（0 或 1），再將 n 右移一位（n /= 2）。
    ///   當最低位為 1 時，累加計數器。
    ///   例如：n = 6  (110₂)
    ///     6 % 2 = 0  → res = 0,  n = 3
    ///     3 % 2 = 1  → res = 1,  n = 1
    ///     1 % 2 = 1  → res = 2,  n = 0
    ///     結果 = 2（6 的二進制 110 有 2 個 1）
    ///
    /// 注意：此方法對負數需謹慎（C# 的 / 與 % 對負數為截斷除法），
    ///       本題輸入為非負整數故不受影響。
    /// 時間複雜度：O(log n)；空間複雜度：O(1)
    /// </summary>
    /// <param name="n">非負整數</param>
    /// <returns>n 的二進制中 1 的個數</returns>
    public static int PopCount2(int n)
    {
        int res = 0;

        while(n != 0)
        {
            res += (n % 2); // 取最低位元：若為 1 則累加
            n /= 2;         // 右移一位（相當於 n >>= 1）
        }

        return res;
    }    
}
