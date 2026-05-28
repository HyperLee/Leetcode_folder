namespace leetcode_080;

class Program
{
    /// <summary>
    /// 80. Remove Duplicates from Sorted Array II
    /// https://leetcode.com/problems/remove-duplicates-from-sorted-array-ii/description/
    /// 80. 刪除排序陣列中的重複項 II
    /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array-ii/description/
    ///
    /// English:
    /// Given an integer array nums sorted in non-decreasing order, remove some duplicates in-place
    /// such that each unique element appears at most twice. The relative order of the elements
    /// should be kept the same.
    ///
    /// Since it is impossible to change the length of the array in some languages, place the result
    /// in the first part of nums. If there are k elements after removing duplicates, then the first
    /// k elements of nums should hold the final result. It does not matter what remains beyond the
    /// first k elements. Return k after placing the final result in the first k slots of nums.
    ///
    /// Do not allocate extra space for another array. Modify the input array in-place with O(1)
    /// extra memory.
    ///
    /// Custom Judge:
    /// int[] nums = [...];
    /// int[] expectedNums = [...];
    /// int k = removeDuplicates(nums);
    /// assert k == expectedNums.length;
    /// for (int i = 0; i &lt; k; i++) {
    ///     assert nums[i] == expectedNums[i];
    /// }
    ///
    /// 繁體中文:
    /// 給定一個以非遞減順序排序的整數陣列 nums，請原地移除部分重複元素，使每個不同元素最多
    /// 出現兩次。元素的相對順序必須保持不變。
    ///
    /// 因為在某些語言中無法改變陣列長度，所以必須把結果放在 nums 的前半部。若移除重複項後
    /// 有 k 個元素，則 nums 的前 k 個位置應保存最終結果；第 k 個位置之後留下什麼內容都不重要。
    /// 在把最終結果放入 nums 的前 k 個位置後，回傳 k。
    ///
    /// 不要為另一個陣列配置額外空間。你必須使用 O(1) 額外記憶體，直接原地修改輸入陣列。
    ///
    /// 自訂判題:
    /// int[] nums = [...];
    /// int[] expectedNums = [...];
    /// int k = removeDuplicates(nums);
    /// assert k == expectedNums.length;
    /// for (int i = 0; i &lt; k; i++) {
    ///     assert nums[i] == expectedNums[i];
    /// }
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
