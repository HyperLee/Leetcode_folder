namespace leetcode_869;

class Program
{
    /// <summary>
    /// 869. Reordered Power of 2
    /// https://leetcode.com/problems/reordered-power-of-2/description/?envType=daily-question&envId=2025-08-10
    /// 869. 重新排序得到 2 的冪
    /// https://leetcode.cn/problems/reordered-power-of-2/description/?envType=daily-question&envId=2025-08-10
    /// 
    /// 解題說明：
    /// 給定一個整數 n，可以重新排列 n 的數字（可原順序），使得首位數字不是 0。
    /// 目標是判斷是否存在一種排列方式，使其對應的整數為 2 的冪。
    /// 
    /// 解法：
    /// 1. 將 n 轉為字元陣列，並排序，方便去重。
    /// 2. 使用回溯法（DFS）枚舉所有不重複的全排列，過程中跳過前導 0。
    /// 3. 每產生一個排列，判斷其對應的整數是否為 2 的冪（利用位元運算 n & (n-1) == 0）。
    /// 4. 若有任一排列符合條件，立即回傳 true，否則全部檢查完回傳 false。
    /// </summary>
    /// <param name="args">主程式參數</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        // 多組測試資料
        var prog = new Program();
        int[] testCases = { 1, 10, 16, 24, 46, 61, 128, 256, 821, 218, 123, 1024, 4096, 0, 100, 222, 46, 46, 46 };
        foreach (var n in testCases)
        {
            bool result = prog.ReorderedPowerOf2(n);
            Console.WriteLine($"n={n}, 是否可重排為2的冪: {result}");
        }
    }

    // 用於標記每個數字是否已被使用
    private bool[] vis;

    /// <summary>
    /// 判斷給定整數 n 是否可以重排為 2 的冪
    /// </summary>
    /// <param name="n">待檢查的整數</param>
    /// <returns>若可重排為 2 的冪則回傳 true，否則 false</returns>
    public bool ReorderedPowerOf2(int n)
    {
        // 將 n 轉為字元陣列，方便排列組合
        char[] nums = n.ToString().ToCharArray();
        Array.Sort(nums); // 排序，便於去重
        vis = new bool[nums.Length]; // 初始化標記陣列
        return Backtrack(nums, 0, 0); // 從 idx=0, num=0 開始回溯
    }

    /// <summary>
    /// 回溯法枚舉所有不重複的全排列，並檢查是否為 2 的冪
    /// </summary>
    /// <param name="nums">數字字元陣列（已排序）</param>
    /// <param name="idx">目前排列到第幾位</param>
    /// <param name="num">目前已組成的數字</param>
    /// <returns>若存在一種排列為 2 的冪則回傳 true</returns>
    public bool Backtrack(char[] nums, int idx, int num)
    {
        // 若已排列完所有位數，檢查是否為 2 的冪
        if (idx == nums.Length)
        {
            return IsPowerOfTwo(num);
        }

        for (int i = 0; i < nums.Length; i++)
        {
            // 跳過：
            // 1. 首位為 0
            // 2. 已經使用過的數字
            // 3. 重複數字（同層只取第一個）
            if ((num == 0 && nums[i] == '0') // 首位不能為 0
                || vis[i] // 已用過
                || (i > 0 && !vis[i - 1] && nums[i] == nums[i - 1])) // 跳過重複
            {
                continue;
            }
            vis[i] = true; // 標記已使用
            // 遞迴進入下一層，組成新數字
            if (Backtrack(nums, idx + 1, num * 10 + (nums[i] - '0')))
            {
                return true; // 找到一個有效排列，直接回傳
            }
            vis[i] = false; // 回溯，取消標記
        }
        return false; // 所有排列都不符合
    }

    /// <summary>
    /// 判斷一個整數是否為 2 的冪（利用位元運算）
    /// </summary>
    /// <param name="n">待檢查的整數</param>
    /// <returns>若為 2 的冪則回傳 true</returns>
    public bool IsPowerOfTwo(int n)
    {
        // 2 的冪只有一個位元為 1，n & (n-1) == 0
        return n > 0 && (n & (n - 1)) == 0;
    }

}
