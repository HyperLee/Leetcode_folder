namespace leetcode_1399;

class Program
{
    /// <summary>
    /// 1399. Count Largest Group
    /// https://leetcode.com/problems/count-largest-group/description/?envType=daily-question&envId=2025-04-23
    /// 1399. 统计最大组的数目
    /// https://leetcode.cn/problems/count-largest-group/description/?envType=daily-question&envId=Invalid%20Date
    /// 
    /// 給你一個整數 n。
    /// 對於從 1 到 n 的每一個整數，根據 各位數字的總和 來將它們分組。
    /// 請你回傳 具有最多數字的群組 的數量。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 建立多組測試資料進行驗證
        int[] testCases = { 2, 13, 24, 45, 100 };
        
        // 遍歷所有測試案例
        foreach (int n in testCases)
        {
            // 呼叫函式並輸出結果
            int result = CountLargestGroup(n);
            Console.WriteLine($"當 n = {n} 時，具有最多數字的群組數量為：{result}");
            
            // 額外顯示詳細分組情況 (僅針對較小的輸入值)
            if (n <= 20)
            {
                PrintGroupDetails(n);
                Console.WriteLine(new string('-', 30));
            }
        }
    }

    /// <summary>
    /// 輔助函式：印出分組的詳細資訊，僅用於測試
    /// </summary>
    /// <param name="n">輸入整數範圍上限</param>
    private static void PrintGroupDetails(int n)
    {
        Dictionary<int, List<int>> groups = new Dictionary<int, List<int>>();
        
        // 從 1 遍歷到 n
        for (int i = 1; i <= n; i++)
        {
            int digitSum = GetDigitSum(i);
            
            if (!groups.ContainsKey(digitSum))
            {
                groups[digitSum] = new List<int>();
            }
            
            groups[digitSum].Add(i);
        }
        
        Console.WriteLine("詳細分組情況：");
        foreach (var group in groups)
        {
            Console.Write($"  數字總和 {group.Key}: [");
            Console.Write(string.Join(", ", group.Value));
            Console.WriteLine($"] => {group.Value.Count} 個數字");
        }
    }

    /// <summary>
    /// 主函式：計算具有最多數字的群組有幾個
    ///
    /// 【解題思路的出發點】
    /// 1. 此題的關鍵在於理解「數字總和」(digit sum)的概念：
    ///    例如數字 123 的數字總和是 1+2+3=6，而 29 的數字總和是 2+9=11
    /// 2. 我們要找的是具有相同「數字總和」的數字組合中，哪些組合的大小最大
    /// 3. 使用「字典」是此題的最佳選擇，因為我們需要高效率地儲存每個數字總和對應的數量
    /// 4. 基本策略：
    ///    a. 計算 1 到 n 的每個數字的數字總和
    ///    b. 統計每個數字總和出現的次數
    ///    c. 找出出現次數最多的數字總和，並計算有幾種數字總和達到這個次數
    /// 5. 時間複雜度：O(n * log n)，其中 log n 是計算單個數字的數字總和所需時間
    /// 6. 空間複雜度：O(n)，最壞情況下每個數字的數字總和都不同
    /// 
    /// 核心邏輯是：
    /// 1.計算數字的「數字總和」。
    /// 2.使用字典分群並統計群組大小。
    /// 3.找出最大群組大小，並計算達到該大小的群組數量。
    /// </summary>
    /// <param name="n">輸入整數範圍上限</param>
    /// <returns>最大的群組數量</returns>
    public static int CountLargestGroup(int n)
    {
        // 使用 Dictionary 來存每個「數字總和」對應的數量
        // key 是數字總和，value 是對應的數字個數
        // 例如：數字總和 1 對應的數字有 1, 10, 100，則 groupSizes[1] = 3
        Dictionary<int, int> groupSizes = new Dictionary<int, int>();

        // 從 1 遍歷到 n
        for (int i = 1; i <= n; i++)
        {
            // 計算 i 的數字總和
            int digitSum = GetDigitSum(i);

            // 如果該數字總和沒出現過，初始化為 0
            if (!groupSizes.ContainsKey(digitSum))
            {
                groupSizes[digitSum] = 0;
            }

            // 對應群組的數量加一
            groupSizes[digitSum]++;
        }

        // 找出 groupSizes.Value 最大值
        int maxValues = groupSizes.Values.Max();

        // 回傳 符合 groupSizes.Value 最大值的數量, 有幾個
        return groupSizes.Values.Count(count => count == maxValues);
    }


    /// <summary>
    /// 輔助函式：計算一個整數的數字總和
    /// 這個函式的目的就是： 👉 把一個整數的每一位數字加起來
    /// 🧾 總結說明
    /// number % 10：取出數字的「最後一位」
    /// number / 10：把最後一位「去掉」，繼續處理剩下的數字
    /// 這樣重複直到 number == 0，就處理完所有位數了
    /// </summary>
    /// <param name="number">目標數字</param>
    /// <returns>各位數字相加的總和</returns>
    static int GetDigitSum(int number)
    {
        int sum = 0;
        while (number > 0)
        {
            sum += number % 10; // 取出最後一位數字; 取出個位數並加總
            number /= 10;       // 去掉最後一位; 去掉個位數（整除 10）
        }
        return sum;
    }
}
