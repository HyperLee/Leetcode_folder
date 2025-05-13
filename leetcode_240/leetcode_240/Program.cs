namespace leetcode_240;

class Program
{
    /// <summary>
    /// LeetCode 240. 搜索二維矩陣 II
    /// 題目描述：
    /// 給定一個 m x n 的整數矩陣 matrix，該矩陣每一行從左到右遞增、每一列從上到下遞增。
    /// 請判斷給定的目標值 target 是否存在於矩陣中。
    /// 
    /// 範例：
    /// matrix = [
    ///   [1, 4, 7, 11, 15],
    ///   [2, 5, 8, 12, 19],
    ///   [3, 6, 9, 16, 22],
    ///   [10, 13, 14, 17, 24],
    ///   [18, 21, 23, 26, 30]
    /// ]
    /// target = 5 回傳 true
    /// target = 20 回傳 false
    /// 
    /// https://leetcode.com/problems/search-a-2d-matrix-ii/description/
    /// https://leetcode.cn/problems/search-a-2d-matrix-ii/description/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[][] matrix = new int[][]
        {
            new int[] {1, 4, 7, 11, 15},
            new int[] {2, 5, 8, 12, 19},
            new int[] {3, 6, 9, 16, 22},
            new int[] {10, 13, 14, 17, 24},
            new int[] {18, 21, 23, 26, 30}
        };
        int target1 = 5;
        int target2 = 20;

        Program p = new Program();

        // 使用暴力法驗證
        bool result1 = p.SearchMatrix(matrix, target1);
        bool result2 = p.SearchMatrix(matrix, target2);
        Console.WriteLine($"[暴力法] 搜尋 {target1} 結果: {result1}"); // 預期 true
        Console.WriteLine($"[暴力法] 搜尋 {target2} 結果: {result2}"); // 預期 false

        // 使用二分搜尋法驗證
        bool result3 = p.SearchMatrix_binary(matrix, target1);
        bool result4 = p.SearchMatrix_binary(matrix, target2);
        Console.WriteLine($"[二分法] 搜尋 {target1} 結果: {result3}"); // 預期 true
        Console.WriteLine($"[二分法] 搜尋 {target2} 結果: {result4}"); // 預期 false

        // 使用從右上角開始搜尋法驗證
        bool result5 = p.SearchMatrix_RightTop(matrix, target1);
        bool result6 = p.SearchMatrix_RightTop(matrix, target2);
        Console.WriteLine($"[右上角法] 搜尋 {target1} 結果: {result5}"); // 預期 true
        Console.WriteLine($"[右上角法] 搜尋 {target2} 結果: {result6}"); // 預期 false
    }


    /// <summary>
    /// 方法1: 暴力法
    /// 逐行、逐列檢查二維矩陣中是否存在目標值 target。
    /// 時間複雜度為 O(m*n)，其中 m 為行數，n 為列數。
    /// 若找到目標值則回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="matrix">二維整數陣列，每一個 row 代表一行</param>
    /// <param name="target">要搜尋的目標值</param>
    /// <returns>若找到目標值則回傳 true，否則回傳 false</returns>
    public bool SearchMatrix(int[][] matrix, int target) 
    {
        // 逐行遍歷矩陣
        foreach(int[] row in matrix)
        {
            // 逐列檢查當前行的每個元素
            foreach(int element in row)
            {
                // 若元素等於目標值，立即回傳 true
                if(element == target)
                {
                    return true;
                }
            }
        }
        // 若全部檢查完都沒找到，回傳 false
        return false;
    }

    /// <summary>
    /// 方法2: 二分法
    /// 對每一行(row)使用二分搜尋法來尋找目標值 target。
    /// 時間複雜度為 O(m*logn)，其中 m 為行數，n 為列數。
    /// 若任一行找到目標值則回傳 true，否則回傳 false。
    /// 
    /// ref:
    /// https://leetcode.cn/problems/search-a-2d-matrix-ii/solutions/1062538/sou-suo-er-wei-ju-zhen-ii-by-leetcode-so-9hcx/
    /// </summary>
    /// <param name="matrix">二維整數陣列，每一個 row 代表一行</param>
    /// <param name="target">要搜尋的目標值</param>
    /// <returns>若找到目標值則回傳 true，否則回傳 false</returns>
    public bool SearchMatrix_binary(int[][] matrix, int target) 
    {
        // 逐行遍歷矩陣
        foreach(int[] row in matrix)
        {
            // 對當前行使用二分搜尋法
            int index = Search(row, target);
            // 若找到目標值，立即回傳 true
            if(index >= 0)
            {
                return true;
            }
        }
        // 若全部行都沒找到，回傳 false
        return false;
    }

    /// <summary>
    /// 一維陣列的二分搜尋法。
    /// 簡單說就是對每一行做二分法搜尋。
    /// 在已排序的整數陣列 nums 中搜尋目標值 target。
    /// 若找到則回傳目標值的索引，否則回傳 -1。
    /// </summary>
    /// <param name="nums">已排序的一維整數陣列</param>
    /// <param name="target">要搜尋的目標值</param>
    /// <returns>若找到目標值則回傳其索引，否則回傳 -1</returns>
    public int Search(int[] nums, int target) 
    {
        int low = 0;
        int high = nums.Length - 1;
        // 當 low <= high 時持續搜尋
        while (low <= high)
        {
            // 取中間索引
            int mid = low + (high - low) / 2;
            int num = nums[mid];
            // 若中間值等於目標值，回傳索引
            if (num == target)
            {
                return mid;
            }
            // 若中間值小於目標值，縮小搜尋範圍到右半部
            else if (num < target)
            {
                low = mid + 1;
            }
            // 若中間值大於目標值，縮小搜尋範圍到左半部
            else
            {
                high = mid - 1;
            }
        }
        // 若沒找到，回傳 -1
        return -1;
    }


    /// <summary>
    /// 方法3: 從右上角開始搜尋法
    /// 從矩陣的右上角 (第一行最後一列) 開始，根據當前元素與目標值的比較結果，
    /// 決定往左(減少列)或往下(增加行)移動，直到找到目標值或超出邊界。
    /// 時間複雜度為 O(m+n)，其中 m 為行數，n 為列數。
    /// 若找到目標值則回傳 true，否則回傳 false。
    /// 
    /// ref:
    /// https://leetcode.cn/problems/search-a-2d-matrix-ii/solutions/2783938/tu-jie-pai-chu-fa-yi-tu-miao-dong-python-kytg/
    /// </summary>
    /// <param name="matrix">二維整數陣列，每一個 row 代表一行</param>
    /// <param name="target">要搜尋的目標值</param>
    /// <returns>若找到目標值則回傳 true，否則回傳 false</returns>
    public bool SearchMatrix_RightTop(int[][] matrix, int target) 
    {
        // 從右上角 (第0行, 最後一列) 開始
        int i = 0;
        int j = matrix[0].Length - 1;
        // 當 i 未超過行數且 j 未小於0時持續搜尋
        while (i < matrix.Length && j >= 0)
        {
            // 若當前元素等於目標值，回傳 true
            if (matrix[i][j] == target)
            {
                return true;
            }
            // 若當前元素大於目標值，往左移動(減少列)
            else if (matrix[i][j] > target)
            {
                j--;
            }
            // 若當前元素小於目標值，往下移動(增加行)
            else
            {
                i++;
            }
        }
        // 若沒找到，回傳 false
        return false;
    }
}
