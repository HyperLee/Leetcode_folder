namespace leetcode_1716;

class Program
{
    /// <summary>
    /// 1716. Calculate Money in Leetcode Bank
    /// https://leetcode.com/problems/calculate-money-in-leetcode-bank/description/?envType=daily-question&envId=2025-10-25
    /// 1716. 计算力扣银行的钱
    /// https://leetcode.cn/problems/calculate-money-in-leetcode-bank/description/?envType=daily-question&envId=2025-10-25
    ///
    /// Hercy wants to save money for his first car. He puts money in the Leetcode bank every day.
    /// He starts by putting in $1 on Monday, the first day. Every day from Tuesday to Sunday, he will put in $1 more than the day before. On every subsequent Monday, he will put in $1 more than the previous Monday.
    /// Given n, return the total amount of money he will have in the Leetcode bank at the end of the nth day.
    ///
    /// 中文翻譯：
    /// Hercy 想存錢買他的第一輛車。他每天都會存錢到力扣銀行。
    /// 在第一個星期一（第 1 天）他存入 $1。在接下來的星期二到星期日，每天比前一天多存 $1。在每個後續的星期一，他會比前一個星期一多存 $1。
    /// 給定 n，返回第 n 天結束時他在力扣銀行裡總共有多少錢。
    ///
    /// </summary>
    /// <param name="args">命令列參數（未使用）</param>
    static void Main(string[] args)
    {
        // 測試範例：直接執行不需輸入參數
        var p = new Program();
        var tests = new[] { 4, 10, 20, 28, 100 };
        foreach (var n in tests)
        {
            var a = p.TotalMoney(n);
            var b = p.TotalMoney2(n);
            Console.WriteLine($"n = {n}, TotalMoney = {a}, TotalMoney2 = {b}");
        }
    }


    /// <summary>
    /// 數學推導解法（Mathematical Optimization Approach）
    /// 
    /// 解題思路：
    /// 將 n 天拆解為完整週數 w 與剩餘天數 d (n = 7w + d)。
    /// 利用等差數列求和公式計算每週的總金額，避免逐天累加。
    /// 
    /// 數學公式推導：
    /// - 第 i 週（0-indexed）的存款序列為：(1+i), (2+i), ..., (7+i)
    /// - 第 i 週的總和 = 7(1+i) + (0+1+2+...+6) = 7(1+i) + 21 = 28 + 7i
    /// - 完整週的總和 = Σ(i=0 to w-1)[28 + 7i] = 28w + 7w(w-1)/2
    /// - 剩餘 d 天的總和（從第 w 週週一開始）= d(1+w) + d(d-1)/2
    /// 
    /// 時間複雜度：O(w)，其中 w = ⌊n/7⌋，約等於 O(n/7)
    /// 空間複雜度：O(1)
    /// 
    /// 範例 (n=10):
    /// - weeks = 1, days = 3
    /// - 完整週 0: 28 + 7×0 = 28
    /// - 剩餘 3 天: 3×(1+1) + 3×2/2 = 6 + 3 = 9
    /// - 總計: 28 + 9 = 37
    /// </summary>
    /// <param name="n">天數，必須為正整數</param>
    /// <returns>第 n 天結束時銀行的總金額</returns>
    public int TotalMoney(int n)
    {
        // 將 n 天分解為完整週數與剩餘天數
        int weeks = n / 7;  // 完整週數 w
        int days = n % 7;   // 剩餘天數 d (0 ≤ d < 7)

        int total = 0;

        // 計算完整週的金額
        // 第 i 週（0-indexed）的週一存款為 (1 + i)
        // 該週總和 = 7 * weekStart + (0+1+2+...+6) = 7 * weekStart + 21
        for (int i = 0; i < weeks; i++)
        {
            int weekStart = 1 + i; // 每週一的存款金額
            total += 7 * weekStart + (7 * (7 - 1)) / 2; // 每週的總存款金額 = 7*weekStart + 21
        }

        // 計算剩餘天數的金額
        // 剩餘天數屬於第 w 週，該週週一存款為 (1 + weeks)
        // 總和 = d * weekStart + (0+1+...+(d-1)) = d * weekStart + d(d-1)/2
        if (days > 0)
        {
            int weekStart = 1 + weeks; // 剩餘天數所在週的週一存款金額
            total += days * weekStart + (days * (days - 1)) / 2; // 剩餘天數的總存款金額
        }

        return total;
    }

    /// <summary>
    /// 暴力模擬解法（Simulation Approach）
    /// 
    /// 解題思路：
    /// 直接模擬題目描述的存款過程，逐天計算並累加。
    /// 維護當前週數和週內天數，根據公式計算每日存款金額。
    /// 
    /// 演算法步驟：
    /// 1. 初始化週數 week = 1，天數 day = 1
    /// 2. 對每一天 i (0 到 n-1)：
    ///    - 當日存款 = week + day - 1
    ///    - 累加到總金額
    ///    - day++，若 day > 7 則重置為 1 並 week++
    /// 
    /// 時間複雜度：O(n)，需要遍歷所有 n 天
    /// 空間複雜度：O(1)
    /// 
    /// 範例 (n=10):
    /// - Day 0-6 (Week 1): 1+2+3+4+5+6+7 = 28
    /// - Day 7-9 (Week 2): 2+3+4 = 9
    /// - 總計: 28 + 9 = 37
    /// 
    /// 優點：程式碼直觀易懂，完全遵循題意
    /// 缺點：對於大數值效能較差
    /// </summary>
    /// <param name="n">天數，必須為正整數</param>
    /// <returns>第 n 天結束時銀行的總金額</returns>
    public int TotalMoney2(int n)
    {
        int week = 1;  // 當前週數（1-indexed，第一週為 1）
        int day = 1;   // 當前週內的天數（1-7，1 代表週一）
        int res = 0;   // 累計總金額
        
        // 逐天模擬存款過程
        for (int i = 0; i < n; i++)
        {
            // 當日存款 = 週數基準 + 週內天數偏移 - 1
            // 例如：第一週週一 = 1 + 1 - 1 = 1，第二週週一 = 2 + 1 - 1 = 2
            res += week + day - 1;
            
            day++; // 移至下一天
            
            // 若超過週日（day > 7），重置到下週一
            if (day > 7)
            {
                day = 1;    // 重置為週一
                week++;     // 進入下一週
            }
        }
        
        return res;
    }
}
