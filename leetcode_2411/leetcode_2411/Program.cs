namespace leetcode_2411;

class Program
{
    /// <summary>
    /// 2411. Smallest Subarrays With Maximum Bitwise OR
    /// https://leetcode.com/problems/smallest-subarrays-with-maximum-bitwise-or/description/?envType=daily-question&envId=2025-07-29
    /// 2411. 按位或最大的最小子數組長度
    /// https://leetcode.cn/problems/smallest-subarrays-with-maximum-bitwise-or/description/?envType=daily-question&envId=2025-07-29
    /// 
    /// 題目描述:
    /// 給你一個長度為 n 的 0 索引陣列 nums，陣列由非負整數組成。
    /// 對於每個索引 i（0 <= i < n），你需要找出一個最小長度的非空子陣列，
    /// 這個子陣列從 i 開始（包含 i），其按位或的值等於從 i 到 n-1 所有子陣列按位或的最大值。
    /// 
    /// 換句話說，設 Bij 為子陣列 nums[i...j] 的按位或，
    /// 你需要找到最小的 j，使得 nums[i...j] 的按位或等於 max(Bik)，其中 i <= k <= n-1。
    /// 
    /// 按位或是將陣列中所有數字進行按位或運算的結果。
    /// 
    /// 返回一個長度為 n 的整數陣列 answer，其中 answer[i] 是從 i 開始的最小長度子陣列，
    /// 其按位或等於最大可能的按位或。
    /// 
    /// 子陣列是陣列中連續且非空的元素序列。
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[][] testCases = new int[][]
        {
            new int[] {1, 0, 2, 1, 3},
            new int[] {0, 1, 2, 3, 4},
            new int[] {7, 7, 7, 7},
            new int[] {1},
            new int[] {8, 1, 2, 12, 7, 6}
        };

        var program = new Program();
        for (int i = 0; i < testCases.Length; i++)
        {
            int[] input = testCases[i];
            Console.WriteLine($"TestCase {i + 1}: [{string.Join(", ", input)}]");
            int[] res1 = program.SmallestSubarrays((int[])input.Clone());
            int[] res2 = program.SmallestSubarraysV2((int[])input.Clone());
            Console.WriteLine($"解法一: [{string.Join(", ", res1)}]");
            Console.WriteLine($"解法二: [{string.Join(", ", res2)}]");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 解法一：反向滑動視窗
    /// 
    /// 解題思路：
    /// 1. 由於要獲取的資訊都在 nums[i] 的右側，所以採用倒著滑窗的方法
    /// 2. 外層迴圈枚舉左端點 left，內層迴圈縮小右端點 right
    /// 3. 當子陣列 [left,right] 的按位或值等於 [left,right-1] 的按位或值時，
    ///    說明 nums[right] 對結果沒有貢獻，可以縮小視窗右端點
    /// 4. 使用單調堆疊的思想，保證堆疊中至少有兩個數，方便判斷視窗是否可以縮小
    /// 
    /// 時間複雜度：O(n log U)，其中 U 是陣列中的最大值
    /// 空間複雜度：O(1)，不考慮輸出陣列
    /// 
    /// ref:https://leetcode.cn/problems/smallest-subarrays-with-maximum-bitwise-or/solutions/1830911/by-endlesscheng-zai1/?envType=daily-question&envId=2025-07-29
    /// 
    /// </summary>
    /// <param name="nums">輸入的非負整數陣列</param>
    /// <returns>每個位置開始的最小子陣列長度陣列</returns>
    public int[] SmallestSubarrays(int[] nums)
    {
        int n = nums.Length;
        int[] res = new int[n];
        
        // 最後一個元素的子陣列長度必為 1
        res[n - 1] = 1;
        
        // 邊界情況：只有一個元素時直接返回
        if (n == 1)
        {
            return res;
        }

        // 保證堆疊中至少有兩個數，方便判斷視窗右端點是否可以縮小
        // 將最後兩個元素進行按位或運算，建構初始堆疊
        nums[n - 1] |= nums[n - 2];
        
        int leftOr = 0;      // 當前左端點到某個位置的按位或結果
        int right = n - 1;   // 滑動視窗的右端點
        int bottom = n - 2;  // 堆疊底部的位置
        
        // 從倒數第二個元素開始向左遍歷
        for (int left = n - 2; left >= 0; left--)
        {
            // 將當前元素加入到按位或運算中
            leftOr |= nums[left];
            
            // 當子陣列 [left,right] 的按位或值等於 [left,right-1] 的按位或值時
            // 說明 nums[right] 對結果沒有貢獻，可以縮小視窗右端點
            while (right > left && (leftOr | nums[right]) == (leftOr | nums[right - 1]))
            {
                right--; // 縮小右端點
                
                // 當堆疊中只剩一個數時（bottom >= right）
                if (bottom >= right)
                {
                    // 重新建構一個堆疊，堆疊底部為 left，堆疊頂部為 right
                    // 這樣可以保持堆疊的單調性質
                    for (int i = left + 1; i <= right; i++)
                    {
                        nums[i] |= nums[i - 1];
                    }
                    bottom = left;  // 更新堆疊底部位置
                    leftOr = 0;     // 重置左側按位或結果
                }
            }
            
            // 計算從 left 開始的最小子陣列長度
            res[left] = right - left + 1;
        }
        
        return res;
    }

    /// <summary>
    /// 解法二：記錄每個二進制位的最近出現位置
    /// 
    /// 解題思路：
    /// 1. 對於陣列中的元素 nums[i]，它最多包含 31 個二進制位
    /// 2. 對於第 bit 個二進制位：
    ///    - 如果是 1，與任何數進行按位或運算後，這個二進制位仍然是 1
    ///    - 如果是 0，需要找到最小的 j（j > i），使得 nums[j] 的第 bit 個二進制位是 1
    /// 3. 按照下標從大到小遍歷陣列，用 pos 陣列記錄每個二進制位最近一次出現為 1 的位置
    /// 4. 對於 nums[i] 的第 bit 個二進制位：
    ///    - 如果是 1，將 pos[bit] 更新為 i
    ///    - 如果是 0 且 pos[bit] 不為 -1，右邊界至少要為 pos[bit]
    /// 
    /// 時間複雜度：O(n × 31) = O(n)，其中 n 是陣列長度
    /// 空間複雜度：O(31) = O(1)，不考慮輸出陣列
    /// 
    /// ref: https://leetcode.cn/problems/smallest-subarrays-with-maximum-bitwise-or/solutions/
    /// 
    /// </summary>
    /// <param name="nums">輸入的非負整數陣列</param>
    /// <returns>每個位置開始的最小子陣列長度陣列</returns>
    public int[] SmallestSubarraysV2(int[] nums)
    {
        int n = nums.Length;
        int[] pos = new int[31];  // 記錄每個二進制位最近一次出現為 1 的位置
        Array.Fill(pos, -1);      // 初始化為 -1，表示尚未出現
        int[] ans = new int[n];   // 結果陣列
        
        // 從右到左遍歷陣列
        for (int i = n - 1; i >= 0; --i)
        {
            int j = i;  // 初始化右邊界為當前位置
            
            // 檢查每個二進制位（0 到 30，共 31 位）
            for (int bit = 0; bit < 31; ++bit)
            {
                // 檢查 nums[i] 的第 bit 位是否為 0
                if ((nums[i] & (1 << bit)) == 0)
                {
                    // 如果第 bit 位為 0，且該位之前出現過 1
                    if (pos[bit] != -1)
                    {
                        // 更新右邊界為該位最近出現 1 的位置
                        j = Math.Max(j, pos[bit]);
                    }
                }
                else
                {
                    // 如果第 bit 位為 1，更新該位最近出現的位置
                    pos[bit] = i;
                }
            }
            
            // 計算從 i 開始的最小子陣列長度
            ans[i] = j - i + 1;
        }
        
        return ans;
    }
}
