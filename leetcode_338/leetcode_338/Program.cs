namespace leetcode_338;

class Program
{
    /// <summary>
    /// 338. Counting Bits
    /// https://leetcode.com/problems/counting-bits/description/?envType=problem-list-v2&envId=oizxjoit
    /// 338. 位元數
    /// https://leetcode.cn/problems/counting-bits/description/
    /// 
    /// ref:
    /// https://leetcode.cn/problems/counting-bits/solutions/7882/hen-qing-xi-de-si-lu-by-duadua/
    /// https://leetcode.cn/problems/counting-bits/solutions/631479/yi-bu-bu-fen-xi-tui-dao-chu-dong-tai-gui-3yog/
    /// https://leetcode.cn/problems/counting-bits/solutions/627418/bi-te-wei-ji-shu-by-leetcode-solution-0t1i/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        
        // 測試案例
        int[] testCases = { 2, 5, 8 };
        
        foreach (int n in testCases)
        {
            Console.WriteLine($"\n測試 n = {n}:");
            
            // 測試第一種解法
            int[] result1 = solution.CountBits(n);
            Console.WriteLine($"解法1結果: [{string.Join(", ", result1)}]");
            
            // 測試第二種解法
            int[] result2 = solution.CountBits2(n);
            Console.WriteLine($"解法2結果: [{string.Join(", ", result2)}]");
            
            // 驗證兩種解法結果是否相同
            bool isEqual = Enumerable.SequenceEqual(result1, result2);
            Console.WriteLine($"兩種解法結果{(isEqual ? "相同" : "不同")}");
        }
    }


    /// <summary>
    /// 計算從0到n的每個數字中1的位元數
    /// 使用動態規劃的方法，利用已計算的結果來加速計算
    /// 時間複雜度：O(n)，空間複雜度：O(n)
    /// 效能好,
    /// </summary>
    /// <param name="n">輸入範圍的上限值</param>
    /// <returns>返回一個整數陣列，其中ans[i]表示數字i的二進位表示中1的個數</returns>
    public int[] CountBits(int n) 
    {
        // 創建一個長度為n+1的陣列來儲存結果
        int[] dp = new int[n + 1];
        
        // 遍歷從1到n的每個數字
        for (int i = 1; i <= n; i++)
        {
            // 對於每個數字i，其二進位中1的個數等於：
            // 1. i右移一位後的數字中1的個數 (dp[i >> 1])
            // 2. 加上i的最後一位是否為1 (i & 1)
            dp[i] = dp[i >> 1] + (i & 1);
        }
        
        return dp;
    }

    /// <summary>
    /// 計算從0到n的每個數字中1的位元數的替代解法
    /// 使用動態規劃，基於奇偶性質來計算
    /// 時間複雜度：O(n)，空間複雜度：O(n)
    /// 
    /// ex:偶數
    ///  2 = 10       4 = 100       8 = 1000
    ///  3 = 11       5 = 101       9 = 1001
    /// 
    /// ex:奇數
    /// 0 = 0       1 = 1
    /// 2 = 10      3 = 11
    /// 
    /// 易讀
    /// 奇數：其1的個數等於前一個數(偶數)的1的個數加1
    /// 偶數：其1的個數等於該數除以2的數字的1的個數(直接除以2)
    /// <param name="n">輸入範圍的上限值</param>
    /// <returns>返回一個整數陣列，其中result[i]表示數字i的二進位表示中1的個數</returns>
    public int[] CountBits2(int n) 
    {
        // 創建結果陣列並初始化
        int[] result = new int[n + 1];
        // 由於0的二進位表示中沒有1，所以result[0] = 0
        // 這裡的result[0] = 0是多餘的，因為陣列初始化時已經是0了
        // result[0] = 0;

        // 遍歷從1到n的每個數字
        for (int i = 1; i <= n; i++)
        {
            if (i % 2 == 1)
            {
                // 對於奇數：其1的個數等於前一個數(偶數)的1的個數加1
                // 因為奇數是在偶數的二進位表示後加上一個1
                result[i] = result[i - 1] + 1;
            }
            else
            {
                // 對於偶數：其1的個數等於該數除以2的數字的1的個數
                // 因為偶數是將某個數左移一位（乘2）得到的，1的個數不變
                result[i] = result[i / 2];
            }
        }
        
        return result;
    }
}
