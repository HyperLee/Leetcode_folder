namespace leetcode_2593;

class Program
{
    /// <summary>
    /// 2593. Find Score of an Array After Marking All Elements
    /// https://leetcode.com/problems/find-score-of-an-array-after-marking-all-elements/description/
    /// 2593. 标记所有元素后数组的分数
    /// https://leetcode.cn/problems/find-score-of-an-array-after-marking-all-elements/description/
    /// 
    /// 題目描述（中文）:
    /// 給定一個由正整數構成的陣列 nums。
    /// 初始 score = 0，對陣列套用下列演算法：
    /// 1. 選擇陣列中尚未標記的最小整數；若有多個相同的最小值，選擇索引最小的那個。
    /// 2. 將該被選中的整數的值加到 score。
    /// 3. 將該元素以及如果存在的相鄰左右兩個元素標記為已標記。
    /// 重複上述步驟直到所有元素都被標記。返回最終的 score。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試案例 1
        int[] test1 = [2, 1, 3, 4, 5, 2];
        Console.WriteLine("測試案例 1: [2,1,3,4,5,2]");
        Console.WriteLine($"解法一結果: {program.FindScore(test1)}");
        Console.WriteLine($"解法二結果: {program.FindScore2(test1)}");
        Console.WriteLine($"預期結果: 7");
        Console.WriteLine();

        // 測試案例 2
        int[] test2 = [2, 3, 5, 1, 3, 2];
        Console.WriteLine("測試案例 2: [2,3,5,1,3,2]");
        Console.WriteLine($"解法一結果: {program.FindScore(test2)}");
        Console.WriteLine($"解法二結果: {program.FindScore2(test2)}");
        Console.WriteLine($"預期結果: 5");
        Console.WriteLine();

        // 測試案例 3：單一元素
        int[] test3 = [5];
        Console.WriteLine("測試案例 3: [5]");
        Console.WriteLine($"解法一結果: {program.FindScore(test3)}");
        Console.WriteLine($"解法二結果: {program.FindScore2(test3)}");
        Console.WriteLine($"預期結果: 5");
        Console.WriteLine();

        // 測試案例 4：全部遞減
        int[] test4 = [5, 4, 3, 2, 1];
        Console.WriteLine("測試案例 4: [5,4,3,2,1]");
        Console.WriteLine($"解法一結果: {program.FindScore(test4)}");
        Console.WriteLine($"解法二結果: {program.FindScore2(test4)}");
        Console.WriteLine($"預期結果: 9 (1+3+5)");
        Console.WriteLine();

        // 測試案例 5：全部遞增
        int[] test5 = [1, 2, 3, 4, 5];
        Console.WriteLine("測試案例 5: [1,2,3,4,5]");
        Console.WriteLine($"解法一結果: {program.FindScore(test5)}");
        Console.WriteLine($"解法二結果: {program.FindScore2(test5)}");
        Console.WriteLine($"預期結果: 9 (1+3+5)");
        Console.WriteLine();

        // 測試案例 6：相同元素
        int[] test6 = [3, 3, 3, 3];
        Console.WriteLine("測試案例 6: [3,3,3,3]");
        Console.WriteLine($"解法一結果: {program.FindScore(test6)}");
        Console.WriteLine($"解法二結果: {program.FindScore2(test6)}");
        Console.WriteLine($"預期結果: 6 (3+3)");
        Console.WriteLine();
    }

    /// <summary>
    /// 解法一：模擬標記過程 + 排序
    /// 
    /// 核心思路：
    /// 1. 建立包含元素值與索引的二維陣列，以便追蹤每個元素的原始位置
    /// 2. 對陣列進行排序：優先按元素值升序排序，相同值時按索引升序排序
    /// 3. 使用布林陣列追蹤已標記的索引位置
    /// 4. 依序處理排序後的元素，若該位置未被標記則：
    ///    - 將元素值加入分數
    ///    - 標記該位置及其相鄰左右位置
    /// 
    /// 時間複雜度：O(n log n)，主要來自排序操作
    /// 空間複雜度：O(n)，需要額外的二維陣列和標記陣列
    /// </summary>
    /// <param name="nums">輸入的正整數陣列</param>
    /// <returns>依照題目規則計算後的最終分數</returns>
    public long FindScore(int[] nums)
    {
        long score = 0;
        int n = nums.Length;
        
        // 建立二維陣列，每個元素存儲 [元素值, 原始索引]
        int[][] numWithIndex = new int[n][];
        for (int i = 0; i < n; i++)
        {
            numWithIndex[i] = new int[2];
            numWithIndex[i][0] = nums[i]; // 元素值
            numWithIndex[i][1] = i;       // 原始索引
        }

        // 自訂排序規則：
        // 1. 首先按照元素值升序排序（較小的值優先）
        // 2. 當元素值相同時，按照索引升序排序（較小的索引優先）
        Array.Sort(numWithIndex, (a, b) =>
        {
            if (a[0] != b[0])
            {
                return a[0] - b[0]; // 按元素值升序
            }
            else
            {
                return a[1] - b[1]; // 按索引升序
            }
        });

        // 使用布林陣列記錄被標記的索引位置
        bool[] marked = new bool[n];
        
        // 依序處理排序後的元素
        for (int i = 0; i < n; i++)
        {
            int num = numWithIndex[i][0];   // 當前元素值
            int index = numWithIndex[i][1]; // 原始索引位置

            // 只處理尚未被標記的元素
            if (!marked[index])
            {
                // 將元素值累加到分數
                score += num;
                
                // 標記當前位置
                marked[index] = true;

                // 標記左邊相鄰位置（如果存在）
                if (index > 0)
                {
                    marked[index - 1] = true;
                }
                
                // 標記右邊相鄰位置（如果存在）
                // 注意：index < n - 1 確保不會越界
                if (index < n - 1)
                {
                    marked[index + 1] = true;
                }
            }
        }
        
        return score;
    }

    /// <summary>
    /// 解法二：轉換思維 + 分組循環（嚴格遞減子段）
    /// 
    /// 核心思路：
    /// 將 nums 視為由若干「嚴格遞減子段」組成的陣列
    /// 例如：[2,1,3,4,5,2] 可看成 [2,1] + [3] + [4] + [5,2]
    /// 
    /// 關鍵觀察：
    /// 1. 在嚴格遞減子段中，最小值（坡底）一定可以被選擇
    ///    - 它比前一個元素小（或是起點）
    ///    - 不會大於下一個元素（因為是坡底）
    /// 2. 選了坡底元素後，其左側每隔一個位置的元素都可以選
    ///    （因為不能選相鄰元素）
    /// 3. 選了坡底後，右側的相鄰元素不能選（被標記）
    /// 
    /// 演算法步驟：
    /// 1. 從左到右遍歷，每次前進 2 步（因為選了 i，i+1 就不能選）
    /// 2. 找到嚴格遞減的坡底位置
    /// 3. 從坡底往回（向左）每隔一個位置累加元素值
    /// 
    /// 時間複雜度：O(n)，每個元素最多被訪問兩次
    /// 空間複雜度：O(1)，只使用常數額外空間
    /// </summary>
    /// <param name="nums">輸入的正整數陣列</param>
    /// <returns>依照題目規則計算後的最終分數</returns>
    public long FindScore2(int[] nums)
    {
        long res = 0;
        
        // 以步長 2 遍歷（選了 i 後，i+1 不能選）
        for (int i = 0, n = nums.Length; i < n; i += 2)
        {
            int i0 = i; // 記錄當前子段的起點（坡頂）
            
            // 找到嚴格遞減子段的坡底
            // 持續向右移動，直到遇到非遞減的位置
            while (i + 1 < n && nums[i] > nums[i + 1])
            {
                ++i;
            }
            
            // 現在 i 指向坡底位置
            // 從坡底往回到坡頂，每隔一個位置累加元素值
            // 例如：[5,4,3,2] 中，從 2 開始往回：2 + 4
            for (int j = i; j >= i0; j -= 2)
            {
                res += nums[j];
            }
            // 注意：外層迴圈會 i += 2，所以下一次會跳過 i+1（被標記的位置）
        }
        
        return res;
    }
}
