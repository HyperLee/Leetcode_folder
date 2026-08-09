using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;

namespace leetcode_3542;

class Program
{
    /// <summary>
    /// 3542. Minimum Operations to Convert All Elements to Zero
    /// https://leetcode.com/problems/minimum-operations-to-convert-all-elements-to-zero/description/
    /// <para>
    /// You are given an array nums of size n, consisting of non-negative integers. Your task is to apply some (possibly zero) operations on the array so that all elements become 0.
    ///
    /// In one operation, you can select a subarray [i, j] (where 0 &lt;= i &lt;= j &lt; n) and set all occurrences of the minimum non-negative integer in that subarray to 0.
    ///
    /// Return the minimum number of operations required to make all elements in the array 0.
    ///
    /// Example 1:
    /// Input: nums = [0,2]
    /// Output: 1
    /// Explanation: Select [1,1], which is [2]. Its minimum non-negative integer is 2; setting every 2 to 0 produces [0,0]. Thus the minimum number of operations is 1.
    ///
    /// Example 2:
    /// Input: nums = [3,1,2,1]
    /// Output: 3
    /// Explanation: Select [1,3] = [1,2,1] and set every 1 to 0, producing [3,0,2,0]. Select [2,2] = [2] and set every 2 to 0, producing [3,0,0,0]. Select [0,0] = [3] and set every 3 to 0, producing [0,0,0,0]. Thus the minimum number of operations is 3.
    ///
    /// Example 3:
    /// Input: nums = [1,2,1,2,1,2]
    /// Output: 4
    /// Explanation: Select [0,5] = [1,2,1,2,1,2] and set every 1 to 0, producing [0,2,0,2,0,2]. Then select [1,1], [3,3], and [5,5] in turn; each is [2], and the arrays become [0,0,0,2,0,2], [0,0,0,0,0,2], and [0,0,0,0,0,0]. Thus the minimum number of operations is 4.
    ///
    /// Constraints:
    /// - 1 &lt;= n == nums.length &lt;= 10^5
    /// - 0 &lt;= nums[i] &lt;= 10^5
    /// </para>
    /// <para>
    /// 3542. 將所有元素變為 0 的最少操作次數
    /// https://leetcode.cn/problems/minimum-operations-to-convert-all-elements-to-zero/description/
    ///
    /// 給定大小為 n、由非負整數組成的陣列 nums。你需要對陣列執行若干次（也可能為零次）操作，使所有元素都變成 0。
    ///
    /// 一次操作中，可以選擇子陣列 [i, j]（其中 0 &lt;= i &lt;= j &lt; n），並將該子陣列中最小非負整數的所有出現位置設為 0。
    ///
    /// 回傳使陣列中所有元素變成 0 所需的最少操作次數。
    ///
    /// 範例 1：
    /// 輸入：nums = [0,2]
    /// 輸出：1
    /// 解釋：選擇 [1,1]，即 [2]。其最小非負整數為 2；將所有 2 設為 0 後得到 [0,0]。因此最少操作次數為 1。
    ///
    /// 範例 2：
    /// 輸入：nums = [3,1,2,1]
    /// 輸出：3
    /// 解釋：選擇 [1,3] = [1,2,1]，將所有 1 設為 0，得到 [3,0,2,0]。選擇 [2,2] = [2]，將所有 2 設為 0，得到 [3,0,0,0]。選擇 [0,0] = [3]，將所有 3 設為 0，得到 [0,0,0,0]。因此最少操作次數為 3。
    ///
    /// 範例 3：
    /// 輸入：nums = [1,2,1,2,1,2]
    /// 輸出：4
    /// 解釋：選擇 [0,5] = [1,2,1,2,1,2]，將所有 1 設為 0，得到 [0,2,0,2,0,2]。接著依序選擇 [1,1]、[3,3] 與 [5,5]；它們各自都是 [2]，陣列依序變成 [0,0,0,2,0,2]、[0,0,0,0,0,2] 與 [0,0,0,0,0,0]。因此最少操作次數為 4。
    ///
    /// 限制條件：
    /// - 1 &lt;= n == nums.length &lt;= 10^5
    /// - 0 &lt;= nums[i] &lt;= 10^5
    /// </para>
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1: nums = [1,2,1,2,1,2]
        // 預期輸出: 4
        // 解釋: 第一次操作將所有 1 變成 0,然後需要 3 次操作分別處理三個 2
        int[] test1 = [1, 2, 1, 2, 1, 2];
        Console.WriteLine($"測試案例 1: [{string.Join(", ", test1)}]");
        Console.WriteLine($"方法一結果: {solution.MinOperations(test1)}");
        Console.WriteLine($"方法二結果: {solution.MinOperations2([1, 2, 1, 2, 1, 2])}");
        Console.WriteLine($"預期結果: 4\n");

        // 測試案例 2: nums = [2,5,3,8,3]
        // 預期輸出: 4
        int[] test2 = [2, 5, 3, 8, 3];
        Console.WriteLine($"測試案例 2: [{string.Join(", ", test2)}]");
        Console.WriteLine($"方法一結果: {solution.MinOperations(test2)}");
        Console.WriteLine($"方法二結果: {solution.MinOperations2([2, 5, 3, 8, 3])}");
        Console.WriteLine($"預期結果: 4\n");

        // 測試案例 3: nums = [0]
        // 預期輸出: 0
        // 解釋: 陣列已經全為 0,不需要任何操作
        int[] test3 = [0];
        Console.WriteLine($"測試案例 3: [{string.Join(", ", test3)}]");
        Console.WriteLine($"方法一結果: {solution.MinOperations(test3)}");
        Console.WriteLine($"方法二結果: {solution.MinOperations2([0])}");
        Console.WriteLine($"預期結果: 0\n");

        // 測試案例 4: nums = [1,2,3,2,1]
        // 預期輸出: 3
        int[] test4 = [1, 2, 3, 2, 1];
        Console.WriteLine($"測試案例 4: [{string.Join(", ", test4)}]");
        Console.WriteLine($"方法一結果: {solution.MinOperations(test4)}");
        Console.WriteLine($"方法二結果: {solution.MinOperations2([1, 2, 3, 2, 1])}");
        Console.WriteLine($"預期結果: 3\n");

        // 測試案例 5: nums = [5,4,3,2,1]
        // 預期輸出: 5
        // 解釋: 遞減序列,每個元素都需要單獨操作
        int[] test5 = [5, 4, 3, 2, 1];
        Console.WriteLine($"測試案例 5: [{string.Join(", ", test5)}]");
        Console.WriteLine($"方法一結果: {solution.MinOperations(test5)}");
        Console.WriteLine($"方法二結果: {solution.MinOperations2([5, 4, 3, 2, 1])}");
        Console.WriteLine($"預期結果: 5\n");

        // 測試案例 6: nums = [1,1,1,1]
        // 預期輸出: 1
        // 解釋: 所有元素相同,一次操作即可將所有元素變為 0
        int[] test6 = [1, 1, 1, 1];
        Console.WriteLine($"測試案例 6: [{string.Join(", ", test6)}]");
        Console.WriteLine($"方法一結果: {solution.MinOperations(test6)}");
        Console.WriteLine($"方法二結果: {solution.MinOperations2([1, 1, 1, 1])}");
        Console.WriteLine($"預期結果: 1\n");
    }

    /// <summary>
    /// 方法一: 單調遞增堆疊解法
    /// 
    /// 解題思路:
    /// 1. 維護一個單調遞增堆疊,表示當前遞增的非零元素序列
    /// 2. 規律一: 把若干相同的最小值同時變為 0,可以節省操作次數
    /// 3. 規律二: 如果兩個相同的數之間有更小的數,則他們一定不能一起被變為 0
    /// 4. 遍歷陣列時:
    ///    - 如果堆疊頂元素大於當前元素 a,根據規律二,堆疊頂元素需要彈出
    ///    - 如果 a 為 0,跳過(已經不需要操作)
    ///    - 如果堆疊為空或堆疊頂元素小於 a,需要新的一次操作來覆蓋 a,將 a 加入堆疊並將操作次數加一
    /// 
    /// 時間複雜度: O(n) - 每個元素最多入堆疊和出堆疊各一次
    /// 空間複雜度: O(n) - 堆疊最壞情況下儲存所有元素
    /// 
    /// ref:
    /// https://leetcode.cn/problems/minimum-operations-to-convert-all-elements-to-zero/solutions/3819899/jiang-suo-you-yuan-su-bian-wei-0-de-zui-6f2r3/?envType=daily-question&envId=2025-11-10
    /// </summary>
    /// <param name="nums">非負整數陣列</param>
    /// <returns>使所有元素變為 0 所需的最少操作數</returns>
    public int MinOperations(int[] nums)
    {
        var s = new List<int>(); // 單調遞增堆疊
        int res = 0; // 操作次數

        foreach (int a in nums)
        {
            // 如果堆疊頂元素大於當前元素,彈出堆疊頂
            // 因為根據規律二,堆疊頂元素不可能和之後的元素一起操作
            while (s.Count > 0 && s[s.Count - 1] > a)
            {
                s.RemoveAt(s.Count - 1);
            }

            // 如果當前元素已經為 0,跳過
            if (a == 0)
            {
                continue;
            }

            // 如果堆疊為空或堆疊頂元素小於當前元素
            // 說明需要新的一次操作來覆蓋當前元素
            if (s.Count == 0 || s[s.Count - 1] < a)
            {
                s.Add(a); // 將當前元素加入堆疊
                res++; // 操作次數加一
            }
            // 如果堆疊頂元素等於當前元素,則不需要入堆疊
            // 因為可以在同一次操作中將它們都變為 0
        }

        return res;
    }
    
    /// <summary>
    /// 方法二: 優化的單調堆疊解法(空間優化版)
    /// 
    /// 解題思路:
    /// 此解法使用分治思想結合單調堆疊:
    /// 1. 先通過一次操作,把陣列的最小值都變成 0(如果最小值已經是 0 則跳過)
    /// 2. 此時陣列被這些 0 劃分成若干段,後續操作只能在每段內部
    /// 3. 從左往右遍歷,只在「必須要操作」的時候才把答案加一
    /// 4. 必須操作的情況: 當某個元素左右兩側都有比它小的數時
    /// 
    /// 實作細節:
    /// - 直接把 nums 當作堆疊使用,top 表示堆疊頂索引,節省空間
    /// - 遍歷時維護單調遞增堆疊:
    ///   * 如果當前元素比堆疊頂小,彈出堆疊頂並將答案加一(堆疊頂左右兩側都有更小的數,必須操作)
    ///   * 如果當前元素等於堆疊頂,無需入堆疊(可在同一次操作中都變成 0)
    ///   * 否則將當前元素入堆疊
    /// - 遍歷結束後,堆疊中剩餘的元素都需要操作一次
    /// 
    /// 時間複雜度: O(n) - 每個元素最多入堆疊和出堆疊各一次
    /// 空間複雜度: O(1) - 直接使用輸入陣列作為堆疊,不需要額外空間
    /// 
    /// ref:
    /// https://leetcode.cn/problems/minimum-operations-to-convert-all-elements-to-zero/solutions/3673804/cong-fen-zhi-dao-dan-diao-zhan-jian-ji-x-mzbl/?envType=daily-question&envId=2025-11-10
    /// </summary>
    /// <param name="nums">非負整數陣列</param>
    /// <returns>使所有元素變為 0 所需的最少操作數</returns>
    public int MinOperations2(int[] nums)
    {
        int res = 0; // 記錄必須操作的次數
        int top = -1; // 堆疊頂索引(把 nums 當作堆疊)
        
        foreach (int a in nums)
        {
            // 當堆疊頂元素大於當前元素時
            // 說明堆疊頂左邊有更小的數(堆疊倒數第二個),右邊(當前元素)也有更小的數
            // 因此堆疊頂必須單獨操作一次
            while (top >= 0 && nums[top] > a)
            {
                top--; // 出堆疊
                res++; // 堆疊頂元素必須操作,答案加一
            }
            
            // 如果當前元素與堆疊頂相同,那麼可以在同一次操作中都變成 0
            // 因此當前元素無需入堆疊
            // 這保證了堆疊中沒有重複元素,形成嚴格遞增堆疊
            if (top < 0 || a != nums[top])
            {
                nums[++top] = a; // 入堆疊
            }
        }
        
        // 遍歷結束後,堆疊中剩餘的元素(嚴格遞增序列)都需要操作一次
        // top + 1 是堆疊中元素的數量
        // 如果 nums[0] > 0,表示第一個元素不是 0,需要額外一次操作將最小值變為 0
        return res + top + (nums[0] > 0 ? 1 : 0);
    }
}
