namespace leetcode_081;

class Program
{
    /// <summary>
    /// 81. Search in Rotated Sorted Array II
    /// https://leetcode.com/problems/search-in-rotated-sorted-array-ii/description/
    /// 81. 搜尋旋轉排序陣列 II
    /// https://leetcode.cn/problems/search-in-rotated-sorted-array-ii/description/
    ///
    /// English:
    /// There is an integer array nums sorted in non-decreasing order
    /// (not necessarily with distinct values).
    /// Before being passed to your function, nums is rotated at an unknown pivot
    /// index k (0 <= k < nums.Length), such that the resulting array is
    /// [nums[k], nums[k+1], ..., nums[n-1], nums[0], nums[1], ..., nums[k-1]]
    /// (0-indexed). For example, [0,1,2,4,4,4,5,6,6,7] might be rotated at
    /// pivot index 5 and become [4,5,6,6,7,0,1,2,4,4].
    /// Given the array nums after the rotation and an integer target, return
    /// true if target is in nums, or false if it is not in nums.
    /// You must decrease the overall operation steps as much as possible.
    ///
    /// Traditional Chinese:
    /// 有一個以非遞減順序排序的整數陣列 nums，其中的值不一定互不相同。
    /// 在傳入函式之前，nums 會在未知的樞紐索引 k
    /// (0 <= k < nums.Length) 處旋轉，使結果陣列變成
    /// [nums[k], nums[k+1], ..., nums[n-1], nums[0], nums[1], ..., nums[k-1]]
    /// (索引從 0 開始)。例如，[0,1,2,4,4,4,5,6,6,7] 可能在樞紐索引 5
    /// 旋轉後變成 [4,5,6,6,7,0,1,2,4,4]。
    /// 給定旋轉後的陣列 nums 和一個整數 target，如果 target 存在於 nums 中，
    /// 回傳 true；否則回傳 false。
    /// 你必須盡可能減少整體操作步驟。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一:
    /// 直覺方法, 每個數值取出比對然後回傳結果
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool Search(int[] nums, int target)
    {
        int n = nums.Length;
        for(int i = 0; i < n; i++)
        {
            if(nums[i] == target)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 解法二: 二分法
    /// 思路:
    /// 对于数组中有重复元素的情况，二分查找时可能会有 a[l]=a[mid]=a[r]，此时无法判断区间 [l,mid] 和区间 [mid+1,r] 哪个是有序的。
    /// 例如 nums=[3,1,2,3,3,3,3]，target=2，首次二分时无法判断区间 [0,3] 和区间 [4,6] 哪个是有序的。
    /// 对于这种情况，我们只能将当前二分区间的左边界加一，右边界减一，然后在新区间上继续二分查找。
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool Search2(int[] nums, int target)
    {
        int n = nums.Length;
        if(n == 0)
        {
            return false;
        }
        if(n == 1)
        {
            return nums[0] == target;
        }

        int left = 0;
        int right = n - 1;
        while(left <= right)
        {
            int mid = left + (right - left) / 2;
            if(nums[mid] == target)
            {
                return true;
            }

            if(nums[left] == nums[mid] && nums[mid] == nums[right])
            {
                left++;
                right--;
            }
            else if(nums[left] <= nums[mid])
            {
                if(nums[left] <= target && target < nums[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            else
            {
                if(nums[mid] < target && target <= nums[n - 1])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 解法三: 二分法
    /// 本题与 33 题的区别是有相同元素，这会导致在二分查找时，可能会遇到恰好二分元素 nums[mid] 与数组末尾元素 nums[n−1] 相
    /// 同的情况，此时无法确定答案在左半区间中还是右半区间中。
    /// 
    /// 既然无法确定最小值所在区间，那么干脆去掉 nums 的最后一个数，继续二分。换句话说，此时问题变成了一个规模为 n−1 的
    /// 子问题。
    /// 
    /// 你可能会有疑问：这会不会碰巧去掉了 target？
    /// 这是不会的：
    /// - 如果去掉的数是 target，那么 nums[mid] 也等于 target，这说明 target 仍然在数组中。
    /// - 如果去掉的数不是 target，那么我们排除了一个不等于 target 的数。
    /// 为了方便写代码，我们可以把 right 当作「数组最后一个数的下标」:
    /// - 如果 nums[mid]=nums[right]，那么和上面一样，去掉 nums[right]，也就是把 right 减一。
    /// - 如果 check(nums[mid])=true，那么下标大于 mid 的数都在 target 的右边，都可以去掉，也就是把 right 更新为 mid
    /// - 如果 check(nums[mid])=false，和 33 题一样，把 left 更新为 mid。
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool Search3(int[] nums, int target)
    {
        int left = -1;
        int right = nums.Length - 1; // 開區間(-1, n - 1)

        // 開區間不為空
        while(left + 1 < right)
        {
            int mid = left + (right - left) / 2;
            if(nums[mid] == nums[right])
            {
                right--;
            }
            else if(check(nums, target, right, mid))
            {
                right = mid;
            }
            else
            {
                left = mid;
            }
        }
        return nums[right] == target;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <param name="right"></param>
    /// <param name="i"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <param name="right"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    private bool check(int[] nums, int target, int right, int i)
    {
        int x = nums[i];
        if(x > nums[right])
        {
            return target > nums[right] && x >= target;
        }

        return target > nums[right] || x >= target;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool Search4(int[] nums, int target)
    {
        int n = nums.Length;

        if (n == 0) return false;

        int l = 0, r = n - 1;

        // 恢復二段性
        while (l < r && nums[0] == nums[r])
        {
            r--;
        }

        // 第一次二分，找旋轉點
        while (l < r)
        {
            int mid = (l + r + 1) >> 1;

            if (nums[mid] >= nums[0])
            {
                l = mid;
            }
            else
            {
                r = mid - 1;
            }
        }

        int idx = n;

        if (nums[r] >= nums[0] && r + 1 < n)
        {
            idx = r + 1;
        }

        // 第二次二分，找目標值
        int ans = Find(nums, 0, idx - 1, target);

        if (ans != -1)
        {
            return true;
        }

        ans = Find(nums, idx, n - 1, target);

        return ans != -1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="l"></param>
    /// <param name="r"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    private int Find(int[] nums, int l, int r, int target)
    {
        if(l > r)
        {
            return -1;
        }

        while(l < r)
        {
            int mid = (l + r) >> 1;
            if(nums[mid] >= target)
            {
                r = mid;
            }
            else
            {
                l = mid + 1;
            }
        }
        return nums[r] == target ? r : -1;
    }
}
