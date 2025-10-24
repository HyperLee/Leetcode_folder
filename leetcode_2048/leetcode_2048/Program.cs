namespace leetcode_2048;

class Program
{
    /// <summary>
    /// 2048. Next Greater Numerically Balanced Number
    /// https://leetcode.com/problems/next-greater-numerically-balanced-number/description/?envType=daily-question&envId=2025-10-24
    /// 2048. 下一个更大的数值平衡数
    /// https://leetcode.cn/problems/next-greater-numerically-balanced-number/description/?envType=daily-question&envId=2025-10-24
    /// 
    /// 一個整數 x 是數值平衡的，如果對於 x 中的每個數字 d，x 中恰好有 d 個該數字的出現。
    /// 給定一個整數 n，返回嚴格大於 n 的最小的數值平衡數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("LeetCode 2048: Next Greater Numerically Balanced Number");
        Console.WriteLine("====================================================");
        Console.WriteLine();

        // 建立測試案例
        int[] testCases = { 1, 1000, 3000, 0, 10, 100, 10000 };

        // 建立三種解題方法的實例
        Program solution = new Program();

        foreach (int n in testCases)
        {
            Console.WriteLine($"測試案例: n = {n}");
            Console.WriteLine("-".PadRight(30, '-'));

            // 方法一：暴力枚舉
            var startTime1 = DateTime.Now;
            int result1 = solution.NextBeautifulNumber_BruteForce(n);
            var endTime1 = DateTime.Now;
            var duration1 = endTime1 - startTime1;

            Console.WriteLine($"方法一 (暴力枚舉): {result1}");
            Console.WriteLine($"執行時間: {duration1.TotalMilliseconds:F3} ms");

            // 方法二：預先生成
            var startTime2 = DateTime.Now;
            int result2 = solution.NextBeautifulNumber_PreGenerated(n);
            var endTime2 = DateTime.Now;
            var duration2 = endTime2 - startTime2;

            Console.WriteLine($"方法二 (預先生成): {result2}");
            Console.WriteLine($"執行時間: {duration2.TotalMilliseconds:F3} ms");

            // 方法三：回溯法
            var startTime3 = DateTime.Now;
            int result3 = solution.NextBeautifulNumber_Backtracking(n);
            var endTime3 = DateTime.Now;
            var duration3 = endTime3 - startTime3;

            Console.WriteLine($"方法三 (回溯法): {result3}");
            Console.WriteLine($"執行時間: {duration3.TotalMilliseconds:F3} ms");

            // 驗證結果一致性
            if (result1 == result2 && result2 == result3)
            {
                Console.WriteLine("✅ 所有方法結果一致");
            }
            else
            {
                Console.WriteLine("❌ 方法結果不一致！");
            }

            Console.WriteLine();
        }

        Console.WriteLine("測試完成！");
        Console.WriteLine("按任意鍵繼續...");
    }

    #region 方法一：暴力枚舉（Brute Force）

    /// <summary>
    /// 方法一：暴力枚舉
    /// 從 n+1 開始逐一檢查每個數字，直到找到第一個數值平衡數。
    /// 時間複雜度：O(m * log m)，m 為檢查的數字數量
    /// 空間複雜度：O(1)
    /// </summary>
    /// <param name="n">給定的整數</param>
    /// <returns>嚴格大於 n 的最小數值平衡數</returns>
    public int NextBeautifulNumber_BruteForce(int n)
    {
        // 從 n+1 開始遍歷每個數字
        for (int num = n + 1; num <= 10000000; num++)
        {
            if (IsBalanced(num))
            {
                return num;
            }
        }

        return -1;
    }

    /// <summary>
    /// 檢查一個數字是否為數值平衡數
    /// </summary>
    /// <param name="num">要檢查的數字</param>
    /// <returns>是否為數值平衡數</returns>
    private bool IsBalanced(int num)
    {
        // 統計每個數字的出現次數（0-9）
        int[] count = new int[10];
        int temp = num;

        // 統計各位數字的出現次數
        while (temp > 0)
        {
            count[temp % 10]++;
            temp /= 10;
        }

        // 檢查是否滿足數值平衡條件
        for (int digit = 0; digit < 10; digit++)
        {
            // 如果數字 digit 出現了，但出現次數不等於 digit 本身
            if (count[digit] > 0 && count[digit] != digit)
            {
                return false;
            }
        }

        return true;
    }

    #endregion

    #region 方法二：預先生成所有數值平衡數

    /// <summary>
    /// 方法二：預先生成所有數值平衡數
    /// 觀察到數值平衡數的數量有限，預先生成所有可能的平衡數，然後用二分搜尋找到答案。
    /// 在 [1, 10^6] 範圍內，數值平衡數非常少（約 80 個）。
    /// 時間複雜度：O(log k)，k 為平衡數的總數
    /// 空間複雜度：O(k)
    /// </summary>
    /// <param name="n">給定的整數</param>
    /// <returns>嚴格大於 n 的最小數值平衡數</returns>
    public int NextBeautifulNumber_PreGenerated(int n)
    {
        // 預先生成的數值平衡數列表（已排序）
        List<int> balancedNumbers = GenerateBalancedNumbers();

        // 使用二分搜尋找到第一個大於 n 的數值平衡數
        int left = 0, right = balancedNumbers.Count - 1;
        int result = int.MaxValue;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (balancedNumbers[mid] > n)
            {
                result = balancedNumbers[mid];
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }

        return result;
    }

    /// <summary>
    /// 生成所有可能的數值平衡數
    /// 直接枚舉所有可能的數值平衡數
    /// </summary>
    /// <returns>排序後的數值平衡數列表</returns>
    private List<int> GenerateBalancedNumbers()
    {
        List<int> result = new List<int>();

        // 暴力枚舉所有可能的數值平衡數
        // 在 10^6 範圍內，數值平衡數很少
        for (int num = 1; num <= 1000000; num++)
        {
            if (IsBalanced(num))
            {
                result.Add(num);
            }
        }

        return result;
    }

    #endregion

    #region 方法三：回溯法直接生成

    /// <summary>
    /// 方法三：回溯法直接生成下一個數值平衡數
    /// 從 n+1 的位數開始，使用回溯法構造可能的數值平衡數。
    /// 時間複雜度：O(k)，k 為可能的數值平衡數數量
    /// 空間複雜度：O(d)，d 為數字位數
    /// </summary>
    /// <param name="n">給定的整數</param>
    /// <returns>嚴格大於 n 的最小數值平衡數</returns>
    public int NextBeautifulNumber_Backtracking(int n)
    {
        // 從 n+1 的位數開始嘗試
        int digits = n.ToString().Length;

        for (int length = digits; length <= 7; length++)
        {
            int result = FindBalancedNumberWithLength(n, length);
            if (result != -1)
            {
                return result;
            }
        }

        return -1;
    }

    /// <summary>
    /// 找到指定位數的、大於 n 的最小數值平衡數
    /// </summary>
    /// <param name="n">給定的整數</param>
    /// <param name="length">目標位數</param>
    /// <returns>符合條件的數值平衡數，若不存在則返回 -1</returns>
    private int FindBalancedNumberWithLength(int n, int length)
    {
        List<int> candidates = new List<int>();

        // 使用回溯法生成指定位數的所有數值平衡數
        BacktrackGenerate(length, new int[10], 0, 0, candidates);

        // 找到第一個大於 n 的數
        candidates.Sort();
        foreach (int num in candidates)
        {
            if (num > n)
            {
                return num;
            }
        }

        return -1;
    }

    /// <summary>
    /// 回溯生成指定位數的數值平衡數
    /// </summary>
    /// <param name="remainingLength">剩餘位數</param>
    /// <param name="digitCount">每個數字的使用次數</param>
    /// <param name="currentNum">當前構造的數字</param>
    /// <param name="numDigits">已使用的位數</param>
    /// <param name="result">結果列表</param>
    private void BacktrackGenerate(int remainingLength, int[] digitCount, long currentNum, int numDigits, List<int> result)
    {
        // 如果已經構造完所有位數
        if (numDigits == remainingLength)
        {
            // 檢查是否滿足數值平衡條件
            if (IsBalancedWithCount(digitCount))
            {
                result.Add((int)currentNum);
            }
            return;
        }

        // 嘗試每個數字 1-7（避免前導 0，且 8、9 需要太多位數）
        for (int digit = (numDigits == 0 ? 1 : 0); digit <= 7; digit++)
        {
            // 剪枝：如果加入這個數字會導致出現次數超過數字本身的值
            if (digitCount[digit] < digit)
            {
                digitCount[digit]++;
                BacktrackGenerate(remainingLength, digitCount, currentNum * 10 + digit, numDigits + 1, result);
                digitCount[digit]--;
            }
        }
    }

    /// <summary>
    /// 檢查數字計數陣列是否滿足數值平衡條件
    /// </summary>
    /// <param name="digitCount">每個數字的出現次數</param>
    /// <returns>是否滿足數值平衡條件</returns>
    private bool IsBalancedWithCount(int[] digitCount)
    {
        for (int digit = 0; digit < 10; digit++)
        {
            // 如果數字出現了，但出現次數不等於數字本身
            if (digitCount[digit] > 0 && digitCount[digit] != digit)
            {
                return false;
            }
        }
        return true;
    }

    #endregion

    /// <summary>
    /// LeetCode 原始方法名稱
    /// 使用方法一：暴力枚舉
    /// </summary>
    /// <param name="n">給定的整數</param>
    /// <returns>嚴格大於 n 的最小數值平衡數</returns>
    public int NextBeautifulNumber(int n)
    {
        return NextBeautifulNumber_BruteForce(n);
    }
}
