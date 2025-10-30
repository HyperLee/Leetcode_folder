namespace leetcode_1526;

class Program
{
    /// <summary>
    /// 1526. Minimum Number of Increments on Subarrays to Form a Target Array
    /// https://leetcode.com/problems/minimum-number-of-increments-on-subarrays-to-form-a-target-array/description/?envType=daily-question&envId=2025-10-30
    /// 1526. 形成目标数组的子数组最少增加次数
    /// https://leetcode.cn/problems/minimum-number-of-increments-on-subarrays-to-form-a-target-array/description/?envType=daily-question&envId=2025-10-30
    /// 給你一個整數陣列 target。你有一個與 target 大小相同的整數陣列 initial，所有元素最初都是零。
    /// 在一次操作中，你可以從 initial 中選擇任何子陣列，並將每個值增加一。
    /// 返回從 initial 形成 target 陣列所需的最少操作數。
    /// 測試案例生成使得答案適合 32 位整數。
    /// 
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試案例 1
        int[] target1 = [1, 2, 3, 2, 1];
        Program solution1 = new Program();
        int result1 = solution1.MinNumberOperations(target1);
        Console.WriteLine($"測試案例 1: target = [{string.Join(", ", target1)}]");
        Console.WriteLine($"輸出: {result1}");
        Console.WriteLine($"說明: 第一次操作將 [0,0,0,0,0] 變為 [1,1,1,1,1]，第二次操作將 [1,1,1,1,1] 變為 [1,2,2,2,1]，第三次操作將 [1,2,2,2,1] 變為 [1,2,3,2,1]");
        Console.WriteLine();

        // 測試案例 2
        int[] target2 = [3, 1, 1, 2];
        Program solution2 = new Program();
        int result2 = solution2.MinNumberOperations(target2);
        Console.WriteLine($"測試案例 2: target = [{string.Join(", ", target2)}]");
        Console.WriteLine($"輸出: {result2}");
        Console.WriteLine($"說明: 操作 1 將 [0,0,0,0] 變為 [1,1,1,0]，操作 2 將 [1,1,1,0] 變為 [1,1,1,1]，操作 3 將 [1,1,1,1] 變為 [2,1,1,2]，操作 4 將 [2,1,1,2] 變為 [3,1,1,2]");
        Console.WriteLine();

        // 測試案例 3
        int[] target3 = [3, 1, 5, 4, 2];
        Program solution3 = new Program();
        int result3 = solution3.MinNumberOperations(target3);
        Console.WriteLine($"測試案例 3: target = [{string.Join(", ", target3)}]");
        Console.WriteLine($"輸出: {result3}");
        Console.WriteLine($"說明: 最少需要 7 次操作");
        Console.WriteLine();

        // 測試案例 4
        int[] target4 = [1, 1, 1, 1];
        Program solution4 = new Program();
        int result4 = solution4.MinNumberOperations(target4);
        Console.WriteLine($"測試案例 4: target = [{string.Join(", ", target4)}]");
        Console.WriteLine($"輸出: {result4}");
        Console.WriteLine($"說明: 只需一次操作將整個陣列從 [0,0,0,0] 變為 [1,1,1,1]");
        Console.WriteLine();
    }

    /// <summary>
    /// 使用差分陣列求解最少操作次數
    /// 
    /// 解題思路：
    /// 1. 差分陣列定義：d[i] = target[i] - target[i-1] (當 i > 0 時)，d[0] = target[0]
    /// 2. 核心觀察：每次對子陣列 [L, R] 的增加操作，只會影響差分陣列中兩個位置：
    ///    - d[L] 增加 1
    ///    - d[R+1] 減少 1 (如果 R+1 存在)
    /// 3. 反向思考：從 target 陣列變回全零陣列，相當於將差分陣列的所有元素變為 0
    /// 4. 最少操作數 = 差分陣列中所有正數的和
    /// 5. 簡化計算：ans = target[0] + Σ max(0, target[i] - target[i-1])
    /// 
    /// 時間複雜度：O(n)，其中 n 是陣列長度
    /// 空間複雜度：O(1)，只使用常數額外空間
    /// 
    /// 範例說明：
    /// target = [1, 2, 3, 2, 1]
    /// 差分陣列 d = [1, 1, 1, -1, -1]
    /// 正數和 = 1 + 1 + 1 = 3，這就是最少操作次數
    /// </summary>
    /// <param name="target">目標陣列</param>
    /// <returns>最少操作次數</returns>
    public int MinNumberOperations(int[] target)
    {
        int n = target.Length;
        
        // 初始化結果為第一個元素（差分陣列的第一個值）
        int res = target[0];
        
        // 遍歷陣列，計算差分陣列中的正數和
        for (int i = 1; i < n; i++)
        {
            // 計算當前位置的差分值 d[i] = target[i] - target[i-1]
            // 只累加正數部分（即上升的部分）
            // Math.Max(0, ...) 確保只加上正的差分值
            res += Math.Max(0, target[i] - target[i - 1]);
        }
        
        return res;
    }
}
