namespace leetcode_035;

class Program
{
    /// <summary>
    /// 35. Search Insert Position
    /// https://leetcode.com/problems/search-insert-position/description/
    /// 35. 搜索插入位置
    /// https://leetcode.cn/problems/search-insert-position/description/
    /// 
    /// English:
    /// Given a sorted array of distinct integers and a target value, return the index if the target is found.
    /// If not, return the index where it would be if it were inserted in order.
    /// You must write an algorithm with O(log n) runtime complexity.
    /// 
    /// 繁體中文:
    /// 給定一個由不重複整數組成且已排序的陣列，以及一個目標值。如果找到目標值，回傳其索引。
    /// 如果沒有找到，回傳它依照排序順序應該被插入的位置索引。
    /// 你必須撰寫一個時間複雜度為 O(log n) 的演算法。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        (int[] Nums, int Target, int Expected)[] testCases =
        [
            ([1, 3, 5, 6], 5, 2),
            ([1, 3, 5, 6], 2, 1),
            ([1, 3, 5, 6], 0, 0),
            ([1, 3, 5, 6], 7, 4),
            ([1], 0, 0),
            ([], 8, 0),
        ];

        foreach ((int[] nums, int target, int expected) in testCases)
        {
            int actual = solution.SearchInsert(nums, target);
            string result = actual == expected ? "PASS" : "FAIL";

            Console.WriteLine($"nums = [{string.Join(", ", nums)}], target = {target}, expected = {expected}, actual = {actual} => {result}");
        }
    }

    /// <summary>
    /// 使用二分查找回傳目標值在已排序陣列中的索引，或不存在時依排序應插入的位置。
    /// 解題概念是尋找第一個大於等於 <paramref name="target"/> 的下標，讓「找到目標」與「計算插入點」共用同一個 lower bound 判斷。
    /// </summary>
    /// <param name="nums">以遞增順序排序且元素不重複的整數陣列；若為空陣列，會回傳 0。</param>
    /// <param name="target">要查找或插入的目標值。</param>
    /// <returns>若 <paramref name="target"/> 存在則回傳其索引；否則回傳保持排序後的插入索引，範圍為 0 到 <c>nums.Length</c>。</returns>
    public int SearchInsert(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Length - 1;
        int answer = nums.Length;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (nums[mid] >= target)
            {
                // nums[mid] 已符合插入位置條件，繼續往左找是否有更小的可行索引。
                answer = mid;
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }

        return answer;
    }
}
