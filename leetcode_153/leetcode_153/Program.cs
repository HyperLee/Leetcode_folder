namespace leetcode_153;

class Program
{
    /// <summary>
    /// 153. Find Minimum in Rotated Sorted Array
    /// https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/description/?envType=problem-list-v2&envId=oizxjoit
    /// 153. 寻找旋转排序数组中的最小值
    /// https://leetcode.cn/problems/find-minimum-in-rotated-sorted-array/description/
    /// 
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
            Console.WriteLine($"最小值為: {result}\n");
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
                right = mid;
            }
        }
        
        // 當左右邊界相遇時，即找到最小值
        return nums[left];
    }
}
