namespace leetcode_287;

class Program
{
    /// <summary>
    /// 287. Find the Duplicate Number
    /// https://leetcode.com/problems/find-the-duplicate-number/description/
    /// 287. 寻找重复数
    /// https://leetcode.cn/problems/find-the-duplicate-number/description/
    ///
    /// Given an array of integers nums containing n + 1 integers where each integer is in the range [1, n] inclusive.
    /// There is only one repeated number in nums, return this repeated number.
    /// You must solve the problem without modifying the array nums and using only constant extra space.
    ///
    /// 給定一個整數陣列 nums，其中包含 n + 1 個整數，且每個整數都在 [1, n] 的範圍內（含端點）。
    /// 陣列中只有一個重複出現的數字，請回傳這個重複的數字。
    /// 你必須在不修改陣列 nums，且只使用常數額外空間的條件下解出此題。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一:陣列排序
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int FindDuplicate(int[] nums)
    {
        Array.Sort(nums);

        for(int i = 1; i < nums.Length; i++)
        {
            if(nums[i] == nums[i - 1])
            {
                return nums[i];
            }
        }
        return -1;
    }

    /// <summary>
    /// 使用 Floyd 快慢指標解題
    /// 
    /// 可以參考 142. Linked List Cycle II
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int FindDuplicate2(int[] nums)
    {
         // 0 一定不在环上，适合作为起点
        int slow = 0;
        int fast = 0;
        // 第一階段：找到快慢指標相遇點
        while(true)
        {
            // 等价于 slow = slow.next
            slow = nums[slow];
            // 等价于 fast = fast.next.next
            fast = nums[nums[fast]];

            if(slow == fast)
            {
                // 快慢指针移动到同一个节点
                break;
            }
        }

        // 第二階段：找到環的入口，也就是重複的數字
        // 再用一个指针，从起点出发
        slow = 0;
        while(slow != fast)
        {
            slow = nums[slow];
            fast = nums[fast];
        }
        return slow;
    }

    /// <summary>
    /// 解法三:二分法
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int FindDuplicate3(int[] nums)
    {
        int n = nums.Length;
        int left = 1;
        int right = n - 1;
        int res = -1;

        while(left <= right)
        {
            int mid = left + (right - left) / 2;
            int count = 0;

            // 計算小於等於 mid 的數字個數
            foreach(int num in nums)
            {
                if(num <= mid)
                {
                    count++;
                }
            }

            if(count > mid)
            {
                // 重複的數字在左半邊
                res = mid;
                right = mid - 1;
            }
            else
            {
                // 重複的數字在右半邊
                left = mid + 1;
            }
        }
        return res;
    }
}
