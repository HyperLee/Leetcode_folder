internal class Program
{
    /// <summary>
    /// 416. Partition Equal Subset Sum
    /// https://leetcode.com/problems/partition-equal-subset-sum/description/
    /// 416. 分割等和子集
    /// https://leetcode.cn/problems/partition-equal-subset-sum/description//// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        int[] input = {1,5,11,5};

        Console.WriteLine("res: " + CanPartition(input));
    }


    /// <summary>
   /// 1.分割的兩部分是子集，所以不需要考慮順序
    /// 2.分割的兩個子集的和是相等的, 合併起來為輸入的陣列並且每個元素只能使用一次
    /// 3.單獨一個元素也是一個子集, 空元素和全部元素不是子集 
    /// 
    /// dp[i][j]: i 表示前 i 個元素，j 表示和為 j
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static bool CanPartition(int[] nums)
    {
        int sum = 0;
        // 計算所有元素的總和
        foreach (var num in nums)
        {
            sum += num;
        }

        // 如果總和是奇數，返回 false
        // 偶數才能分成兩個相等的子集
        if (sum % 2 != 0)
        {
            return false;
        }

        int n = nums.Length;
        // 將 sum 除以 2，問題轉換為是否存在一個子集的和為 sum / 2
        sum /= 2;
        // dp[i, j] = true 如果在 nums[0, i] 中有一個子集的和為 j
        bool[,] dp = new bool[n + 1, sum + 1];

        // 初始化：dp[i, 0] = true 表示對於任意的前 i 個數字，
        // 都可以構成和為 0 的子集（即不選取任何元素）
        for (int i = 0; i <= n; i++)
        {
            dp[i, 0] = true;
        }

        // 動態規劃
        for (int i = 1; i <= n; i++)
        {
            // 從 1 遍歷到 sum
            for (int j = 1; j <= sum; j++)
            {
                // 如果 j - nums[i - 1] < 0，表示當前元素大於 sum
                // 所以我們不能包含它
                if (j - nums[i - 1] < 0)
                {
                    // 如果我們不能包含當前元素，那麼結果與前一個相同
                    dp[i, j] = dp[i - 1, j];
                }
                else
                {
                    // 如果我們能找到一個子集的和為 j - nums[i - 1]，那麼我們可以包含當前元素
                    dp[i, j] = dp[i - 1, j] || dp[i - 1, j - nums[i - 1]];
                }
            }
        }

        return dp[n, sum];
    }
}