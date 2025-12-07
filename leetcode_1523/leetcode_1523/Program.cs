namespace leetcode_1523;

class Program
{
    /// <summary>
    /// 1523. Count Odd Numbers in an Interval Range
    /// https://leetcode.com/problems/count-odd-numbers-in-an-interval-range/description/?envType=daily-question&envId=2025-12-07
    /// 1523. 在區間範圍內統計奇數數目
    ///
    /// Description:
    /// Given two non-negative integers low and high. Return the count of odd numbers between low and high (inclusive).
    ///
    /// 繁體中文題目描述：
    /// 給定兩個非負整數 low 和 high，請回傳在區間 [low, high]（包含邊界）內的奇數數量。
    /// https://leetcode.cn/problems/count-odd-numbers-in-an-interval-range/description/?envType=daily-question&envId=2025-12-07
    /// 
    /// 
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // Instance to call instance methods (all solutions are instance methods)
        Program solution = new Program();

        // Test cases: an array of tuples (low, high, expected)
        (int low, int high, int expected)[] tests = new (int, int, int)[]
        {
            (3, 7, 3),      // odds are 3,5,7
            (4, 10, 3),     // odds are 5,7,9
            (1, 1, 1),      // single odd
            (2, 2, 0),      // single even
            (0, 0, 0),      // 0 is even
            (0, 1, 1),      // 1 is odd
            (0, 2, 1),      // 1 is the only odd
            (10, 15, 3)     // 11,13,15
        };

        Console.WriteLine("Testing CountOdds, CountOdds2, CountOdds3:\n");

        foreach (var (low, high, expected) in tests)
        {
            int r1 = solution.CountOdds(low, high);
            int r2 = solution.CountOdds2(low, high);
            int r3 = solution.CountOdds3(low, high);

            Console.WriteLine($"Range [{low}, {high}] - Expected {expected}: CountOdds = {r1}, CountOdds2 = {r2}, CountOdds3 = {r3}");
        }
    }

    /// <summary>
    /// 計算區間 [low, high] 中奇數的數量。
    /// 解法：高於或等於 0 的整數，高的奇數計數為 (high + 1) / 2，低的奇數計數為 low / 2，兩者相減得到區間內奇數數量。
    /// </summary>
    /// <param name="low"></param>
    /// <param name="high"></param>
    /// <returns></returns>
    public int CountOdds(int low, int high)
    {
        return (high + 1) / 2 - (low / 2);
    }

    /// <summary>
    /// <summary>
    /// 計算區間 [low, high] 中奇數的數量（暴力解法）。
    /// 解法說明：使用迴圈逐一檢查區間內每個數是否為奇數，時間複雜度 O(n)，空間複雜度 O(1)。
    /// </summary>
    /// <param name="low"></param>
    /// <param name="high"></param>
    /// <returns></returns>
    public int CountOdds2(int low, int high)
    {
        int count = 0;
        // 逐一檢查每個數字，若為奇數則計數
        for (int i = low; i <= high; i++)
        {
            if(i % 2 != 0)
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// 計算區間 [low, high] 中奇數的數量（前綴計數方法）。
    /// 解法說明：先定義 Pre(x) 回傳區間 [0, x] 中奇數的數量，則區間 [low, high] 中奇數數量為 Pre(high) - Pre(low - 1)。
    /// 由於 Pre(x) 等於 (x + 1) / 2，因此整體時間複雜度 O(1)，空間複雜度 O(1)。
    /// 
    /// 簡單說分兩階段計算:
    /// 1. 計算 0 到 high 的奇數數量 Pre(high)
    /// 2. 計算 0 到 low-1 的奇數數量 Pre(low - 1)
    /// 3. 用 Pre(high) 減去 Pre(low - 1) 得到區間 [low, high] 的奇數數量
    /// 
    /// 舉例說明:
    /// - 假設 low = 3, high = 7
    /// - Pre(7) 計算 0 到 7 的奇數數
    /// - Pre(2) 計算 0 到 2 的奇數數
    /// - 區間 [3, 7] 的奇數數量 = Pre(7) - Pre(2)
    /// 
    /// 注意為什麼是 Pre(low - 1) 而不是 Pre(low)：
    /// - Pre(low) 包含了 low 本身，如果 low 是奇數，會多計算一次 low。
    /// - 因此我們用 Pre(low - 1) 來確保只計算到 low 之前的奇數數量，避免重複計算 low。
    /// </summary>
    /// <param name="low"></param>
    /// <param name="high"></param>
    /// <returns></returns>
    public int CountOdds3(int low, int high)
    {
        // 使用 Pre(x) 計算 0..x 中奇數個數，兩者相減得到區間 [low, high] 的奇數個數
        return Pre(high) - Pre(low - 1);
    }

    /// <summary>
    /// 將 (x + 1) / 2 改寫為位移運算：等同於整數除以 2
    /// Pre(x) 返回 [0,x] 範圍內奇數的數量。例：x=0 -> 0, x=1 -> 1, x=2 -> 1, x=3 -> 2
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public int Pre(int x)
    {
        return (x + 1) >> 1;
    }
}

