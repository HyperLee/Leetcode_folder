namespace leetcode_3420;

class Program
{
    /// <summary>
    /// 3420. Count Non-Decreasing Subarrays After K Operations
    /// https://leetcode.com/problems/count-non-decreasing-subarrays-after-k-operations/description/
    /// <para>
    /// You are given an array <c>nums</c> of <c>n</c> integers and an integer <c>k</c>.
    ///
    /// For each subarray of <c>nums</c>, you can apply up to <c>k</c> operations on it. In each operation, you increment any element of the subarray by 1.
    ///
    /// Note that each subarray is considered independently, meaning changes made to one subarray do not persist to another.
    /// Return the number of subarrays that you can make non-decreasing after performing at most <c>k</c> operations.
    ///
    /// An array is said to be non-decreasing if each element is greater than than or equal to its previous element, if it exists.
    ///
    /// /// Example 1:
    /// Input: nums = [6,3,1,2,4,4], k = 7
    /// Output: 17
    ///
    /// Explanation:
    /// Out of all 21 possible subarrays of <c>nums</c>, only the subarrays [6, 3, 1], [6, 3, 1, 2], [6, 3, 1, 2, 4] and [6, 3, 1, 2, 4, 4] cannot be made non-decreasing after applying up to <c>k</c> = 7 operations. Thus, the number of non-decreasing subarrays is 21 - 4 = 17.
    ///
    /// Example 2:
    /// Input: nums = [6,3,1,3,6], k = 4
    /// Output: 12
    ///
    /// Explanation:
    /// The subarray [3, 1, 3, 6] along with all subarrays of <c>nums</c> with three or fewer elements, except [6, 3, 1], can be made non-decreasing after <c>k</c> operations. There are 5 subarrays of a single element, 4 subarrays of two elements, and 2 subarrays of three elements except [6, 3, 1], so there are 1 + 5 + 4 + 2 = 12 subarrays that can be made non-decreasing.
    ///
    /// Constraints:
    /// - 1 &lt;= nums.length &lt;= 10^5
    /// - 1 &lt;= nums[i] &lt;= 10^9
    /// - 1 &lt;= k &lt;= 10^9
    /// </para>
    /// <para>
    /// 3420. 統計 K 次操作以內得到非遞減子陣列的數目
    /// https://leetcode.cn/problems/count-non-decreasing-subarrays-after-k-operations/description/
    ///
    /// 給定一個包含 <c>n</c> 個整數的陣列 <c>nums</c>，以及一個整數 <c>k</c>。
    ///
    /// 對於 <c>nums</c> 的每個子陣列，你最多可以對該子陣列執行 <c>k</c> 次操作。每次操作中，將子陣列中的任意一個元素加 1。
    ///
    /// 請注意，每個子陣列都會獨立計算，也就是說，對某個子陣列所做的變更不會延續到其他子陣列。
    /// 請回傳最多執行 <c>k</c> 次操作後，可以調整為非遞減的子陣列數量。
    ///
    /// 若陣列中每個元素（若有前一個元素）都大於或等於前一個元素，則稱此陣列為非遞減。
    ///
    /// 範例 1：
    /// 輸入：nums = [6,3,1,2,4,4], k = 7
    /// 輸出：17
    ///
    /// 解釋：
    /// 在 <c>nums</c> 所有 21 個可能的子陣列中，只有 [6, 3, 1]、[6, 3, 1, 2]、[6, 3, 1, 2, 4] 和 [6, 3, 1, 2, 4, 4] 無法在最多執行 <c>k</c> = 7 次操作後變成非遞減。因此，非遞減子陣列的數量為 21 - 4 = 17。
    ///
    /// 範例 2：
    /// 輸入：nums = [6,3,1,3,6], k = 4
    /// 輸出：12
    ///
    /// 解釋：
    /// 子陣列 [3, 1, 3, 6]，以及 <c>nums</c> 中所有長度不超過 3 的子陣列（[6, 3, 1] 除外），都可以在 <c>k</c> 次操作內變成非遞減。長度為 1 的子陣列有 5 個，長度為 2 的有 4 個，長度為 3 的有 2 個（不包含 [6, 3, 1]），因此可變成非遞減的子陣列共有 1 + 5 + 4 + 2 = 12 個。
    ///
    /// 限制條件：
    /// - 1 &lt;= nums.length &lt;= 10^5
    /// - 1 &lt;= nums[i] &lt;= 10^9
    /// - 1 &lt;= k &lt;= 10^9
    /// </para>
    /// </summary>
    /// <param name="args">命令列參數；此範例不使用。</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
