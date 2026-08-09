namespace leetcode_1920;

class Program
{
    /// <summary>
    /// <para>
    /// 1920. Build Array from Permutation
    /// https://leetcode.com/problems/build-array-from-permutation/description/
    ///
    /// Given a zero-based permutation nums, build an array ans of the same length where ans[i] = nums[nums[i]] for every 0 &lt;= i &lt; nums.length, and return ans.
    ///
    /// A zero-based permutation is an array containing distinct integers from 0 to nums.length - 1 inclusive.
    ///
    /// Example 1:
    /// Input: nums = [0,2,1,5,3,4]
    /// Output: [0,1,2,4,5,3]
    /// Explanation:
    /// ans = [nums[nums[0]],nums[nums[1]],nums[nums[2]],nums[nums[3]],nums[nums[4]],nums[nums[5]]]
    /// = [nums[0],nums[2],nums[1],nums[5],nums[3],nums[4]]
    /// = [0,1,2,4,5,3]
    ///
    /// Example 2:
    /// Input: nums = [5,0,1,2,3,4]
    /// Output: [4,5,0,1,2,3]
    /// Explanation:
    /// ans = [nums[nums[0]],nums[nums[1]],nums[nums[2]],nums[nums[3]],nums[nums[4]],nums[nums[5]]]
    /// = [nums[5],nums[0],nums[1],nums[2],nums[3],nums[4]]
    /// = [4,5,0,1,2,3]
    ///
    /// Constraints:
    /// - 1 &lt;= nums.length &lt;= 1000
    /// - 0 &lt;= nums[i] &lt; nums.length
    /// - nums contains distinct elements.
    ///
    /// Follow-up: Can you solve it without extra space, using O(1) memory?
    /// </para>
    /// <para>
    /// 1920. 基於排列建立陣列
    /// https://leetcode.cn/problems/build-array-from-permutation/description/
    ///
    /// 給定一個從零開始的排列 nums，建立同樣長度的陣列 ans，使每個 0 &lt;= i &lt; nums.length 都滿足 ans[i] = nums[nums[i]]，並回傳 ans。
    ///
    /// 從零開始的排列，是由 0 到 nums.length - 1（含）之間所有相異整數組成的陣列。
    ///
    /// 範例 1：
    /// 輸入：nums = [0,2,1,5,3,4]
    /// 輸出：[0,1,2,4,5,3]
    /// 說明：
    /// ans = [nums[nums[0]],nums[nums[1]],nums[nums[2]],nums[nums[3]],nums[nums[4]],nums[nums[5]]]
    /// = [nums[0],nums[2],nums[1],nums[5],nums[3],nums[4]]
    /// = [0,1,2,4,5,3]
    ///
    /// 範例 2：
    /// 輸入：nums = [5,0,1,2,3,4]
    /// 輸出：[4,5,0,1,2,3]
    /// 說明：
    /// ans = [nums[nums[0]],nums[nums[1]],nums[nums[2]],nums[nums[3]],nums[nums[4]],nums[nums[5]]]
    /// = [nums[5],nums[0],nums[1],nums[2],nums[3],nums[4]]
    /// = [4,5,0,1,2,3]
    ///
    /// 限制條件：
    /// - 1 &lt;= nums.length &lt;= 1000
    /// - 0 &lt;= nums[i] &lt; nums.length
    /// - nums 中的元素皆不相同。
    ///
    /// 進階：能否不使用額外空間，以 O(1) 記憶體完成？
    /// </para>
    /// </summary>
    /// <param name="args">命令列引數（在此程式中未使用）</param>
    static void Main(string[] args)
    {
        // 建立 Program 的實例以呼叫非靜態方法
        Program solution = new Program();
        
        // 測試案例 1: 題目範例 nums = [0,2,1,5,3,4]
        int[] nums1 = new int[] { 0, 2, 1, 5, 3, 4 };
        int[] result1 = solution.BuildArray(nums1);
        
        // 預期結果: [0,1,2,4,5,3]
        // 計算方式：
        // ans[0] = nums[nums[0]] = nums[0] = 0
        // ans[1] = nums[nums[1]] = nums[2] = 1
        // ans[2] = nums[nums[2]] = nums[1] = 2
        // ans[3] = nums[nums[3]] = nums[5] = 4
        // ans[4] = nums[nums[4]] = nums[3] = 5
        // ans[5] = nums[nums[5]] = nums[4] = 3
        int[] expected1 = new int[] { 0, 1, 2, 4, 5, 3 };
        
        // 顯示測試結果
        Console.WriteLine("測試案例 1:");
        Console.WriteLine($"輸入: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"輸出: [{string.Join(", ", result1)}]");
        Console.WriteLine($"預期: [{string.Join(", ", expected1)}]");
        string testResult1 = AreArraysEqual(result1, expected1) ? "通過" : "失敗";
        Console.WriteLine($"結果: {testResult1}");
        Console.WriteLine();
        
        // 測試案例 2: 題目範例 nums = [5,0,1,2,3,4]
        int[] nums2 = new int[] { 5, 0, 1, 2, 3, 4 };
        int[] result2 = solution.BuildArray(nums2);
        
        // 預期結果: [4,5,0,1,2,3]
        // 計算方式：
        // ans[0] = nums[nums[0]] = nums[5] = 4
        // ans[1] = nums[nums[1]] = nums[0] = 5
        // ans[2] = nums[nums[2]] = nums[1] = 0
        // ans[3] = nums[nums[3]] = nums[2] = 1
        // ans[4] = nums[nums[4]] = nums[3] = 2
        // ans[5] = nums[nums[5]] = nums[4] = 3
        int[] expected2 = new int[] { 4, 5, 0, 1, 2, 3 };
        
        // 顯示測試結果
        Console.WriteLine("測試案例 2:");
        Console.WriteLine($"輸入: [{string.Join(", ", nums2)}]");
        Console.WriteLine($"輸出: [{string.Join(", ", result2)}]");
        Console.WriteLine($"預期: [{string.Join(", ", expected2)}]");
        string testResult2 = AreArraysEqual(result2, expected2) ? "通過" : "失敗";
        Console.WriteLine($"結果: {testResult2}");
    }

    /// <summary>
    /// 比較兩個陣列是否完全相同
    /// 輔助函式，用於驗證我們的解答是否正確
    /// </summary>
    /// <param name="arr1">第一個陣列</param>
    /// <param name="arr2">第二個陣列</param>
    /// <returns>如果兩個陣列內容完全相同則返回 true，否則返回 false</returns>
    private static bool AreArraysEqual(int[] arr1, int[] arr2)
    {
        // 檢查陣列長度是否相同
        if (arr1.Length != arr2.Length)
            return false;
        
        // 比較每一個元素
        for (int i = 0; i < arr1.Length; i++)
        {
            if (arr1[i] != arr2[i])
                return false;
        }
        
        return true;
    }

    /// <summary>
    /// 依照題目要求建立一個陣列，其中 ans [i] = nums [nums [i]]
    /// 
    /// 詳細流程解釋：
    /// 例如：如果 nums = [0,2,1,5,3,4]，則
    /// ans [0] = nums [nums [0]] = nums [0] = 0
    /// ans [1] = nums [nums [1]] = nums [2] = 1
    /// ans [2] = nums [nums [2]] = nums [1] = 2
    /// ans [3] = nums [nums [3]] = nums [5] = 4
    /// ans [4] = nums [nums [4]] = nums [3] = 5
    /// ans [5] = nums [nums [5]] = nums [4] = 3
    /// 
    /// for 迴圈會遍歷 nums 陣列中的每個位置 i，然後:
    /// 1. 找到 nums [i] 這個值
    /// 2. 用 nums [i] 作為索引，查詢 nums [nums [i]]
    /// 3. 將這個值放入 ans [i] 中
    /// 
    /// 時間複雜度：O (n) - 只需遍歷陣列一次
    /// 空間複雜度：O (n) - 需要建立一個新的陣列來儲存結果
    /// </summary>
    /// <param name="nums">輸入的整數陣列，大小為 n，0 <= nums[i] < n</param>
    /// <returns>建立的新陣列結果</returns>
    public int[] BuildArray(int[] nums)
    {
        // 取得輸入陣列的長度
        int n = nums.Length;
        
        // 建立與輸入陣列相同大小的結果陣列
        int[] ans = new int[n];
        
        // 使用 for 迴圈填充結果陣列
        for (int i = 0; i < n; i++)
        {
            // 對每個位置 i：
            // 1. 找到 nums[i] 的值
            // 2. 以這個值為索引，找到 nums[nums[i]]
            // 3. 將 nums[nums[i]] 存入 ans[i]
            ans[i] = nums[nums[i]];
        }
        
        // 返回建構好的結果陣列
        return ans;
    }
}
