namespace leetcode_2221;

class Program
{
    /// <summary>
    /// 2221. Find Triangular Sum of an Array
    /// https://leetcode.com/problems/find-triangular-sum-of-an-array/description/?envType=daily-question&envId=2025-09-30
    /// 2221. 陣列的三角和
    /// https://leetcode.cn/problems/find-triangular-sum-of-an-array/description/?envType=daily-question&envId=2025-09-30
    /// 
    /// 給定一個 0 索引的整數陣列 nums，其中 nums[i] 是 0 到 9 之間的數字（包括 0 和 9）。
    /// nums 的三角和是經過以下過程後 nums 中唯一元素的價值：
    /// 讓 nums 包含 n 個元素。如果 n == 1，結束過程。否則，建立一個新的 0 索引整數陣列 newNums，長度為 n - 1。
    /// 對於每個索引 i，其中 0 <= i < n - 1，將 newNums[i] 的價值設為 (nums[i] + nums[i+1]) % 10，其中 % 表示模運算子。
    /// 將陣列 nums 替換為 newNums。
    /// 重複整個過程從步驟 1 開始。
    /// 返回 nums 的三角和。
    /// 
    /// 解題思路：
    /// 本題提供三種解法：
    /// 1. 模擬法：直接按照題意建立新陣列，適合理解題目邏輯
    /// 2. 原地修改：優化空間複雜度，直接在原陣列上操作
    /// 3. 數學解法：使用二項式係數（楊輝三角）計算，展現數學之美
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Solution solution = new();

        // 測試案例 1: 基本範例
        int[] nums1 = [1, 2, 3, 4, 5];
        Console.WriteLine($"測試 1 - 輸入: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"方法 1 結果: {solution.TriangularSum_Method1((int[])nums1.Clone())}");
        Console.WriteLine($"方法 2 結果: {solution.TriangularSum_Method2((int[])nums1.Clone())}");
        Console.WriteLine($"方法 3 結果: {solution.TriangularSum_Method3((int[])nums1.Clone())}");
        Console.WriteLine();

        // 測試案例 2: 單一元素
        int[] nums2 = [5];
        Console.WriteLine($"測試 2 - 輸入: [{string.Join(", ", nums2)}]");
        Console.WriteLine($"方法 1 結果: {solution.TriangularSum_Method1((int[])nums2.Clone())}");
        Console.WriteLine($"方法 2 結果: {solution.TriangularSum_Method2((int[])nums2.Clone())}");
        Console.WriteLine($"方法 3 結果: {solution.TriangularSum_Method3((int[])nums2.Clone())}");
        Console.WriteLine();

        // 測試案例 3: 兩個元素
        int[] nums3 = [9, 8];
        Console.WriteLine($"測試 3 - 輸入: [{string.Join(", ", nums3)}]");
        Console.WriteLine($"方法 1 結果: {solution.TriangularSum_Method1((int[])nums3.Clone())}");
        Console.WriteLine($"方法 2 結果: {solution.TriangularSum_Method2((int[])nums3.Clone())}");
        Console.WriteLine($"方法 3 結果: {solution.TriangularSum_Method3((int[])nums3.Clone())}");
        Console.WriteLine();

        // 測試案例 4: 包含 0 的情況
        int[] nums4 = [0, 0, 0];
        Console.WriteLine($"測試 4 - 輸入: [{string.Join(", ", nums4)}]");
        Console.WriteLine($"方法 1 結果: {solution.TriangularSum_Method1((int[])nums4.Clone())}");
        Console.WriteLine($"方法 2 結果: {solution.TriangularSum_Method2((int[])nums4.Clone())}");
        Console.WriteLine($"方法 3 結果: {solution.TriangularSum_Method3((int[])nums4.Clone())}");
        Console.WriteLine();

        // 測試案例 5: 較大的陣列
        int[] nums5 = [1, 1, 1, 1, 1, 1];
        Console.WriteLine($"測試 5 - 輸入: [{string.Join(", ", nums5)}]");
        Console.WriteLine($"方法 1 結果: {solution.TriangularSum_Method1((int[])nums5.Clone())}");
        Console.WriteLine($"方法 2 結果: {solution.TriangularSum_Method2((int[])nums5.Clone())}");
        Console.WriteLine($"方法 3 結果: {solution.TriangularSum_Method3((int[])nums5.Clone())}");
        Console.WriteLine();

        // 測試案例 6: 大陣列（修復溢位問題）
        int[] nums6 = [0, 3, 3, 4, 1, 2, 6, 4, 9, 3, 5, 1, 7, 7, 3, 0, 3, 2, 5, 1, 9, 0, 2, 6, 3, 9, 2, 5, 9, 2, 6, 4, 2, 9, 7, 2, 0, 3, 0, 1, 1, 2, 7, 8, 6, 4, 4, 5];
        Console.WriteLine($"測試 6 - 大陣列測試（長度 {nums6.Length}）");
        Console.WriteLine($"方法 1 結果: {solution.TriangularSum_Method1((int[])nums6.Clone())}");
        Console.WriteLine($"方法 2 結果: {solution.TriangularSum_Method2((int[])nums6.Clone())}");
        Console.WriteLine($"方法 3 結果: {solution.TriangularSum_Method3((int[])nums6.Clone())} (預期: 5)");
        Console.WriteLine();

        // 效能比較測試
        Console.WriteLine("========== 效能比較測試 ==========");
        PerformanceBenchmark();
    }

    /// <summary>
    /// 效能基準測試：比較陣列版本 vs List 版本
    /// </summary>
    static void PerformanceBenchmark()
    {
        Solution solution = new();
        int[] testSizes = [10, 50, 100, 500, 1000];
        const int iterations = 10000;

        foreach (int size in testSizes)
        {
            // 建立測試資料
            int[] testData = new int[size];
            for (int i = 0; i < size; i++)
            {
                testData[i] = i % 10;
            }

            // 測試方法 1（陣列版本）
            var sw1 = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                solution.TriangularSum_Method1((int[])testData.Clone());
            }
            sw1.Stop();

            // 測試 List 版本
            var sw2 = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                solution.TriangularSum_ListVersion((int[])testData.Clone());
            }
            sw2.Stop();

            // 計算差異百分比
            double diff = ((double)sw2.ElapsedMilliseconds - sw1.ElapsedMilliseconds) / sw1.ElapsedMilliseconds * 100;
            string winner = sw1.ElapsedMilliseconds < sw2.ElapsedMilliseconds ? "陣列版本更快" : "List 版本更快";

            Console.WriteLine($"\n陣列大小: {size,4} | 迭代次數: {iterations,6}");
            Console.WriteLine($"  方法 1 (陣列): {sw1.ElapsedMilliseconds,6} ms");
            Console.WriteLine($"  List 版本:    {sw2.ElapsedMilliseconds,6} ms");
            Console.WriteLine($"  效能差異: {diff,6:F2}% ({winner})");
        }
    }

    /// <summary>
    /// Solution 類別包含三種解法
    /// </summary>
    public class Solution
    {
        /// <summary>
        /// 方法 1: 模擬法
        /// 時間複雜度: O(n²) - 外層迴圈 n 次，內層迴圈從 n-1 遞減到 1
        /// 空間複雜度: O(n) - 需要額外陣列儲存每一輪的結果
        /// 
        /// 演算法步驟:
        /// 1. 檢查邊界條件：如果陣列長度為 1，直接返回該元素
        /// 2. 進入迴圈，每次建立新陣列 newNums
        /// 3. 將相鄰元素相加並取模 10，存入新陣列
        /// 4. 用新陣列取代原陣列
        /// 5. 重複直到陣列長度為 1
        /// </summary>
        public int TriangularSum_Method1(int[] nums)
        {
            int n = nums.Length;

            // 邊界條件：如果只有一個元素，直接返回
            if (n == 1)
            {
                return nums[0];
            }

            // 當陣列長度大於 1 時，持續進行運算
            while (n > 1)
            {
                // 建立新陣列，長度為當前長度減 1
                int[] newNums = new int[n - 1];

                // 計算相鄰元素之和並取模 10
                for (int i = 0; i < n - 1; i++)
                {
                    newNums[i] = (nums[i] + nums[i + 1]) % 10;
                }

                // 更新陣列和長度
                nums = newNums;
                n--;
            }

            // 返回最後剩下的唯一元素
            return nums[0];
        }

        /// <summary>
        /// 方法 2: 原地修改法（空間優化）
        /// 時間複雜度: O(n²) - 與方法 1 相同
        /// 空間複雜度: O(1) - 不需要額外陣列，直接在原陣列上操作
        /// 
        /// 演算法步驟:
        /// 1. 使用變數 currentLength 追蹤當前有效長度
        /// 2. 每次運算直接將結果寫回原陣列的前面位置
        /// 3. 這樣可以重複使用原陣列空間，不需要建立新陣列
        /// 4. 逐步縮減有效長度直到剩下一個元素
        /// </summary>
        public int TriangularSum_Method2(int[] nums)
        {
            int currentLength = nums.Length;

            // 當有效長度大於 1 時，持續進行運算
            while (currentLength > 1)
            {
                // 直接在原陣列上修改，將計算結果寫回前面的位置
                for (int i = 0; i < currentLength - 1; i++)
                {
                    // 將相鄰兩數之和取模 10，存回 nums[i]
                    nums[i] = (nums[i] + nums[i + 1]) % 10;
                }

                // 有效長度減 1
                currentLength--;
            }

            // 返回第一個位置的元素（即最終結果）
            return nums[0];
        }

        /// <summary>
        /// 方法 3: 數學解法（二項式係數 / 楊輝三角）- 使用模運算優化
        /// 時間複雜度: O(n²) - 計算楊輝三角需要 n(n-1)/2 次操作
        /// 空間複雜度: O(n) - 儲存一行楊輝三角係數
        /// 
        /// 數學原理:
        /// 觀察發現，最終結果實際上是各元素乘以對應的二項式係數後相加
        /// result = Σ(nums[i] * C(n-1, i)) % 10
        /// 
        /// 其中 C(n-1, i) 是組合數「從 n-1 個中選 i 個」
        /// 這些係數剛好對應楊輝三角（帕斯卡三角形）的第 n-1 行
        /// 
        /// 例如: nums = [1,2,3,4,5] (n=5)
        /// 係數為 C(4,0), C(4,1), C(4,2), C(4,3), C(4,4)
        /// 即: 1, 4, 6, 4, 1
        /// result = (1*1 + 2*4 + 3*6 + 4*4 + 5*1) % 10 = 48 % 10 = 8
        /// 
        /// 實作策略：
        /// 1. 使用楊輝三角的加法性質逐行計算，避免乘除法可能的溢位
        /// 2. 利用模運算性質：(a+b) % m = ((a%m) + (b%m)) % m
        /// 3. 在計算過程中就取模 10，確保數值不會過大
        /// 4. 從右往左更新陣列，避免覆蓋還需使用的值
        /// 
        /// 注意事項：
        /// - 若使用 C(n,k) = C(n,k-1) * (n-k+1) / k 公式會有溢位風險
        /// - 當 n 較大時（如 n=48），C(47,23) 會超過 int 範圍
        /// - 本實作使用楊輝三角性質完全避免溢位問題
        /// </summary>
        public int TriangularSum_Method3(int[] nums)
        {
            int n = nums.Length;

            // 邊界條件：單一元素直接返回
            if (n == 1)
            {
                return nums[0];
            }

            // 使用楊輝三角的性質：每一行可以從上一行計算得出
            // 我們直接用一維陣列滾動計算，節省空間
            // 因為最終只需要 mod 10，我們在過程中就保持 mod 10
            long[] coefficients = new long[n];
            coefficients[0] = 1;

            // 逐行計算楊輝三角，直到第 n-1 行
            for (int row = 1; row < n; row++)
            {
                // 從右往左計算，避免覆蓋還需要使用的值
                for (int col = row; col > 0; col--)
                {
                    // 楊輝三角性質：每個數等於上一行相鄰兩數之和
                    // 為了避免大數溢位，我們在這裡取模
                    coefficients[col] = (coefficients[col] + coefficients[col - 1]) % 10;
                }
            }

            // 計算加權和
            long result = 0;
            for (int i = 0; i < n; i++)
            {
                result = (result + (long)nums[i] * coefficients[i]) % 10;
            }

            return (int)result;
        }

        /// <summary>
        /// List 版本：使用 List&lt;int&gt; 而非陣列
        /// 這是題目提供的參考解法
        /// 時間複雜度: O(n²)
        /// 空間複雜度: O(n)
        /// 
        /// 與方法 1 的主要差異：
        /// 1. 使用 List&lt;int&gt; 而非 int[]
        /// 2. List 需要動態擴容，可能造成額外的記憶體配置和複製
        /// 3. List.Add() 和 List.Count 有輕微的效能開銷
        /// </summary>
        public int TriangularSum_ListVersion(int[] nums)
        {
            List<int> current = new List<int>(nums);

            while (current.Count > 1)
            {
                List<int> newNums = new List<int>();

                for (int i = 0; i < current.Count - 1; ++i)
                {
                    newNums.Add((current[i] + current[i + 1]) % 10);
                }

                current = newNums;
            }

            return current[0];
        }
    }
}
