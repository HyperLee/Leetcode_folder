using System.Linq;

namespace leetcode_153;

class Program
{
    /// <summary>
    /// <para>
    /// 153. Find Minimum in Rotated Sorted Array
    /// https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/description/
    ///
    /// Suppose an array of length n sorted in ascending order is rotated between 1 and n times. For example,
    /// nums = [0,1,2,4,5,6,7] might become:
    /// - [4,5,6,7,0,1,2] if rotated 4 times.
    /// - [0,1,2,4,5,6,7] if rotated 7 times.
    /// Rotating [a[0],a[1],a[2],...,a[n-1]] once produces [a[n-1],a[0],a[1],a[2],...,a[n-2]].
    /// Given the sorted rotated array nums of unique elements, return its minimum element.
    /// You must write an algorithm that runs in O(log n) time.
    ///
    /// Example 1:
    /// Input: nums = [3,4,5,1,2]
    /// Output: 1
    /// Explanation: The original array was [1,2,3,4,5] rotated 3 times.
    ///
    /// Example 2:
    /// Input: nums = [4,5,6,7,0,1,2]
    /// Output: 0
    /// Explanation: The original array was [0,1,2,4,5,6,7] rotated 4 times.
    ///
    /// Example 3:
    /// Input: nums = [11,13,15,17]
    /// Output: 11
    /// Explanation: The original array was [11,13,15,17] rotated 4 times.
    ///
    /// Constraints:
    /// - n == nums.length
    /// - 1 &lt;= n &lt;= 5000
    /// - -5000 &lt;= nums[i] &lt;= 5000
    /// - All integers in nums are unique.
    /// - nums is sorted and rotated between 1 and n times.
    /// </para>
    /// <para>
    /// 153. 尋找旋轉排序陣列中的最小值
    /// https://leetcode.cn/problems/find-minimum-in-rotated-sorted-array/description/
    ///
    /// 假設一個長度為 n、按遞增順序排序的陣列被旋轉 1 到 n 次。例如 nums = [0,1,2,4,5,6,7] 可能變成：
    /// - 若旋轉 4 次，得到 [4,5,6,7,0,1,2]。
    /// - 若旋轉 7 次，得到 [0,1,2,4,5,6,7]。
    /// 將 [a[0],a[1],a[2],...,a[n-1]] 旋轉一次會得到 [a[n-1],a[0],a[1],a[2],...,a[n-2]]。
    /// 給定由相異元素組成的旋轉排序陣列 nums，回傳其中的最小元素。
    /// 你必須撰寫時間複雜度為 O(log n) 的演算法。
    ///
    /// 範例 1：
    /// 輸入：nums = [3,4,5,1,2]
    /// 輸出：1
    /// 解釋：原始陣列為 [1,2,3,4,5]，旋轉了 3 次。
    ///
    /// 範例 2：
    /// 輸入：nums = [4,5,6,7,0,1,2]
    /// 輸出：0
    /// 解釋：原始陣列為 [0,1,2,4,5,6,7]，旋轉了 4 次。
    ///
    /// 範例 3：
    /// 輸入：nums = [11,13,15,17]
    /// 輸出：11
    /// 解釋：原始陣列為 [11,13,15,17]，旋轉了 4 次。
    ///
    /// 限制條件：
    /// - n == nums.length
    /// - 1 &lt;= n &lt;= 5000
    /// - -5000 &lt;= nums[i] &lt;= 5000
    /// - nums 中所有整數都相異。
    /// - nums 已排序，並旋轉了 1 到 n 次。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        // 測試案例
        int[][] testCases = new int[][]
        {
            new int[] { 3, 4, 5, 1, 2 },        // 旋轉 3 次的排序陣列
            new int[] { 4, 5, 6, 7, 0, 1, 2 },  // 旋轉 4 次的排序陣列
            new int[] { 11, 13, 15, 17 },       // 未旋轉的排序陣列
            new int[] { 2, 1 }                  // 最小的測試案例
        };

        // 執行測試並顯示結果
        for (int i = 0; i < testCases.Length; i++)
        {
            int result = solution.FindMin(testCases[i]);
            Console.WriteLine($"測試案例 {i + 1}: [{string.Join(", ", testCases[i])}]");
            Console.WriteLine($"[解法一] 二分法 最小值為: {result}\n");
            // 解法二：直接用 LINQ Min()
            int resultLinq = solution.FindMinLinq(testCases[i]);
            Console.WriteLine($"[解法二] LINQ Min() 最小值為: {resultLinq}\n");
        }
    }

    /// <summary>
    /// 解題思路：
    /// 1. 使用二分搜尋法找出最小值
    /// 2. 由於陣列經過旋轉，會形成兩個遞增的子陣列
    /// 3. 最小值會是第二個遞增子陣列的起始點
    /// 4. 比較中間值和右邊界值來判斷最小值在哪個區間：
    ///    - 若中間值大於右邊界值，表示最小值在右半部
    ///    - 若中間值小於右邊界值，表示最小值在左半部（包含中間值）
    /// 時間複雜度：O(log n)
    /// 空間複雜度：O(1)
    /// </summary>
    /// <param name="nums">旋轉過的排序陣列</param>
    /// <returns>陣列中的最小值</returns>
    public int FindMin(int[] nums)
    {
        int left = 0, right = nums.Length - 1;
        
        while (left < right)
        {
            // 計算中間索引，避免整數溢位
            int mid = left + (right - left) / 2;
            
            // 如果中間值大於右邊界值
            // 代表最小值一定在右半部，將左邊界移到中間值之後
            if (nums[mid] > nums[right])
            {
                left = mid + 1;
            }
            // 如果中間值小於或等於右邊界值
            // 代表最小值在左半部（包含中間值），將右邊界移到中間值
            else
            {
                // 縮小邊界範圍, 靠近左邊界
                right = mid;
            }
        }
        
        // 當左右邊界相遇時，即找到最小值
        return nums[left];
    }

    /// <summary>
    /// 解法二：直接使用 LINQ Min() 找出陣列中的最小值。
    /// 此方法時間複雜度為 O(n)，效能較差，僅適合一般找最小值需求，
    /// 不適合用於練習二分搜尋法或 LeetCode O(log n) 題型。
    /// </summary>
    /// <param name="nums">任意整數陣列</param>
    /// <returns>陣列中的最小值</returns>
    public int FindMinLinq(int[] nums)
    {
        return nums.Min();
    }
}
