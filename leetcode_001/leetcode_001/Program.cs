namespace leetcode_001;

class Program
{
    /// <summary>
    /// 1. Two Sum
    /// https://leetcode.com/problems/two-sum/description/
    /// <para>
    /// You are given an array of integers nums and an integer target. Return indices of the two numbers such that they add up to target.
    ///
    /// You may assume that each input has exactly one solution, and you may not use the same element twice.
    /// You can return the answer in any order.
    ///
    /// Example 1:
    /// Input: nums = [2,7,11,15], target = 9
    /// Output: [0,1]
    /// Explanation: Because nums[0] + nums[1] == 9, we return [0, 1].
    ///
    /// Example 2:
    /// Input: nums = [3,2,4], target = 6
    /// Output: [1,2]
    ///
    /// Example 3:
    /// Input: nums = [3,3], target = 6
    /// Output: [0,1]
    ///
    /// Constraints:
    /// - 2 &lt;= nums.length &lt;= 10^4
    /// - -10^9 &lt;= nums[i] &lt;= 10^9
    /// - -10^9 &lt;= target &lt;= 10^9
    /// - Only one valid answer exists.
    ///
    /// Follow-up: Can you come up with an algorithm that has less than O(n^2) time complexity?
    /// </para>
    /// <para>
    /// 1. 兩數之和
    /// https://leetcode.cn/problems/two-sum/description/
    ///
    /// 給定一個整數陣列 nums 和一個整數 target，請回傳兩個數字的索引，使它們的總和等於 target。
    ///
    /// 你可以假設每組輸入恰好只有一個解，且同一個元素不可重複使用。
    /// 答案可以任意順序回傳。
    ///
    /// 範例 1：
    /// 輸入：nums = [2,7,11,15], target = 9
    /// 輸出：[0,1]
    /// 解釋：因為 nums[0] + nums[1] == 9，所以回傳 [0, 1]。
    ///
    /// 範例 2：
    /// 輸入：nums = [3,2,4], target = 6
    /// 輸出：[1,2]
    ///
    /// 範例 3：
    /// 輸入：nums = [3,3], target = 6
    /// 輸出：[0,1]
    ///
    /// 限制條件：
    /// - 2 &lt;= nums.length &lt;= 10^4
    /// - -10^9 &lt;= nums[i] &lt;= 10^9
    /// - -10^9 &lt;= target &lt;= 10^9
    /// - 僅存在一個有效答案。
    ///
    /// 進階：你能設計出時間複雜度低於 O(n^2) 的演算法嗎？
    /// </para>
    /// </summary>
    static void Main(string[] args)
    {
        // 測試資料 1
        int[] nums1 = { 2, 7, 11, 15 };
        int target1 = 9;
        var res1 = TwoSum(nums1, target1);
        Console.WriteLine($"測試1: ref: [{res1[0]},{res1[1]}]");

        // 測試資料 2
        int[] nums2 = { 3, 2, 4 };
        int target2 = 6;
        var res2 = TwoSum(nums2, target2);
        Console.WriteLine($"測試2: ref: [{res2[0]},{res2[1]}]");

        // 測試資料 3
        int[] nums3 = { 3, 3 };
        int target3 = 6;
        var res3 = TwoSum(nums3, target3);
        Console.WriteLine($"測試3: ref: [{res3[0]},{res3[1]}]");

        // 測試資料 4 (無解情境)
        int[] nums4 = { 1, 2, 3 };
        int target4 = 7;
        var res4 = TwoSum(nums4, target4);
        Console.WriteLine($"測試4: {(res4.Length == 0 ? "無解" : $"ref: [{res4[0]},{res4[1]}]")}");
    }


    /// <summary>
    /// 解題思路：
    /// 1. 使用 Dictionary 作為 Hash Table，儲存已遍歷過的數字及其索引
    /// 2. 對於每個數字 nums[i]，計算目標值與當前數字的差值 (target - nums[i])
    /// 3. 如果差值存在於 Dictionary 中，表示找到配對，返回兩個數字的索引
    /// 4. 如果沒找到，將當前數字和索引加入 Dictionary
    /// 
    /// 時間複雜度：O(n) - 只需遍歷一次數組
    /// 空間複雜度：O(n) - 需要額外的 Dictionary 儲存已遍歷的數字
    /// 
    /// 優化重點：
    /// - 使用 Dictionary 將查找時間從 O(n) 降至 O(1)
    /// - 一邊遍歷一邊存儲，避免重複遍歷
    /// - 使用索引器替代 Add 方法，程式碼更簡潔
    /// 
    /// https://www.itread01.com/content/1543410439.html
    /// https://ithelp.ithome.com.tw/articles/10217042
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <param name="target">目標和</param>
    /// <returns>符合目標和的兩個數字的索引陣列</returns>
    public static int[] TwoSum(int[] nums, int target)
    {
        // 建立 Dictionary 作為雜湊表，key 為數字值，value 為索引位置
        Dictionary<int, int> dic = new Dictionary<int, int>();
        
        // 使用 for 迴圈遍歷整個陣列
        for(int i = 0; i < nums.Length; i++)
        {
            // 計算需要尋找的配對數字 (目標值減去當前數字)
            int left = target - nums[i];
            
            // 檢查雜湊表中是否已經存在配對的數字
            if(dic.ContainsKey(left))
            {
                // 找到配對！回傳兩個數字的索引
                // dic[left] 是之前存入的索引，i 是當前索引
                return new int[] { dic[left], i };
            }

            // 若當前數字不在雜湊表中，將其加入
            // 只存儲第一次出現的索引，避免重複值覆蓋原有的索引
            if (!dic.ContainsKey(nums[i]))
            {
                dic[nums[i]] = i; // 使用索引器語法，較 Add 方法更簡潔
            }
        }

        // 若找不到符合的配對，回傳空陣列
        return Array.Empty<int>(); // 使用 Empty<int>() 替代 null，更安全
    }
}
