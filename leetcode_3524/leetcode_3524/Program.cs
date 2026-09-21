namespace leetcode_3524;

class Program
{
    /// <summary>
    /// 3524. Find X Value of Array I
    /// https://leetcode.com/problems/find-x-value-of-array-i/description/
    /// 3524. 求出陣列的 X 值 I
    /// https://leetcode.cn/problems/find-x-value-of-array-i/description/
    ///
    /// English:
    /// Given an array of positive integers nums, and a positive integer k.
    ///
    /// You are allowed to perform an operation once on nums, where in each operation you can remove any non-overlapping prefix and suffix from nums such that nums remains non-empty.
    ///
    /// You need to find the x-value of nums, which is the number of ways to perform this operation so that the product of the remaining elements leaves a remainder of x when divided by k.
    ///
    /// Return an array result of size k where result[x] is the x-value of nums for 0 <= x <= k - 1.
    ///
    /// A prefix of an array is a subarray that starts from the beginning of the array and extends to any point within it.
    ///
    /// A suffix of an array is a subarray that starts at any point within the array and extends to the end of the array.
    ///
    /// Note that the prefix and suffix to be chosen for the operation can be empty.
    ///
    /// Traditional Chinese（繁體中文）:
    /// 給定一個由正整數組成的陣列 nums，以及一個正整數 k。
    ///
    /// 你可以對 nums 執行一次操作。在每次操作中，你可以從 nums 移除任意不重疊的前綴和後綴，使 nums 維持非空。
    ///
    /// 你需要找出 nums 的 x 值，也就是執行此操作後，剩餘元素的乘積除以 k 的餘數為 x 的操作方式數量。
    ///
    /// 回傳一個大小為 k 的陣列 result，其中 result[x] 是 nums 的 x 值，且 0 <= x <= k - 1。
    ///
    /// 陣列的前綴是從陣列開頭開始，延伸到其中任一位置的子陣列。
    ///
    /// 陣列的後綴是從其中任一位置開始，延伸到陣列結尾的子陣列。
    ///
    /// 請注意，操作中選擇移除的前綴和後綴可以為空。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
