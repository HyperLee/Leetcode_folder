namespace leetcode_033;

class Program
{
    /// <summary>
    /// 33. Search in Rotated Sorted Array
    /// https://leetcode.com/problems/search-in-rotated-sorted-array/description/
    /// 33. 搜索旋转排序数组
    /// https://leetcode.cn/problems/search-in-rotated-sorted-array/description/
    ///
    /// English:
    /// There is an integer array nums sorted in ascending order (with distinct values).
    ///
    /// Prior to being passed to your function, nums is possibly left rotated at an unknown index k
    /// (1 &lt;= k &lt; nums.length) such that the resulting array is
    /// [nums[k], nums[k+1], ..., nums[n-1], nums[0], nums[1], ..., nums[k-1]] (0-indexed).
    /// For example, [0,1,2,4,5,6,7] might be left rotated by 3 indices and become
    /// [4,5,6,7,0,1,2].
    ///
    /// Given the array nums after the possible rotation and an integer target, return the index of
    /// target if it is in nums, or -1 if it is not in nums.
    ///
    /// You must write an algorithm with O(log n) runtime complexity.
    ///
    /// 繁體中文:
    /// 有一個整數陣列 nums，原本依照遞增順序排序，且所有值都不重複。
    ///
    /// 在傳入你的函式之前，nums 可能會在未知索引 k 處被向左旋轉
    /// (1 &lt;= k &lt; nums.length)，使得產生的陣列為
    /// [nums[k], nums[k+1], ..., nums[n-1], nums[0], nums[1], ..., nums[k-1]] (以 0 為索引起點)。
    /// 例如，[0,1,2,4,5,6,7] 可能向左旋轉 3 個索引，變成 [4,5,6,7,0,1,2]。
    ///
    /// 給定可能已被旋轉後的陣列 nums，以及一個整數 target，如果 target 存在於 nums 中，
    /// 回傳它的索引；如果不存在，則回傳 -1。
    ///
    /// 你必須撰寫一個時間複雜度為 O(log n) 的演算法。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一:
    /// 單純用迴圈跑過一輪 比對
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public int Search(int[] nums, int target)
    {
        for(int i = 0; i < nums.Length; i++)
        {
            if(nums[i] == target)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 方法二: 二分法
    /// 輸入的 nums[] 有旋轉過, 所以原先的 遞增順序
    /// 會被切割成左右兩塊.
    /// 
    /// 使用兩次二分
    /// 抓出 target 坐落在 nums[] 的左邊或是右邊
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public int Search2(int[] nums, int target)
    {
        int n = nums.Length;
        int i = findMin(nums);

        // target 在第一段
        if(target > nums[n - 1])
        {
            // 開區間(-1, i)
            return lowerBound(nums, -1, i, target);
        }

        // target 在第二段
        // 開區間 (i - 1, n)
        return lowerBound(nums, i - 1, n, target);
    }

    /// <summary>
    ///  寻找旋转排序数组中的最小值
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int findMin(int[] nums)
    {
        int n = nums.Length;
        int left = -1;
        // 開區間(-1, n - 1)
        int right = n - 1;

        // 開區間 不為空
        while(left + 1 < right)
        {
            int mid = left + (right - left) / 2;
            if(nums[mid] < nums[n - 1])
            {
                right = mid;
            }
            else
            {
                left = mid;
            }
        }
        return right;
    }

    /// <summary>
    /// 有序数组中找 target 的下标
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public int lowerBound(int[] nums, int left, int right, int target)
    {
        // 開區間不為空
        // 循環不變量
        // nums[left] < target
        // nums[right] >= target
        while(left + 1 < right)
        {
            int mid = left + (right - left) / 2;
            if(nums[mid] < target)
            {
                // 範圍縮小到 (mid, right)
                left = mid;
            }
            else
            {
                // 範圍縮小到 (left, mid)
                right = mid;
            }
        }
        return nums[right] == target ? right : -1;
    }
}
