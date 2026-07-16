namespace leetcode_930;

class Program
{
    /// <summary>
    /// 930. Binary Subarrays With Sum
    /// https://leetcode.com/problems/binary-subarrays-with-sum/description/
    /// 930. 和相同的二元子陣列
    /// https://leetcode.cn/problems/binary-subarrays-with-sum/description/
    /// Given a binary array nums and an integer goal, return the number of non-empty subarrays with a sum goal.
    ///
    /// A subarray is a contiguous part of the array.
    ///
    /// 給定一個二元陣列 nums 和一個整數 goal，請回傳總和等於 goal 的非空子陣列數量。
    ///
    /// 子陣列是陣列中一段連續的部分。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一：哈希表
    /// 假设原数组的前缀和数组为 sum，且子数组 (i,j] 的区间和为 goal，那么 sum[j]−sum[i]=goal。因此我们可以枚举 j ，每次查
    /// 询满足该等式的 i 的数量。
    /// 具体地，我们用哈希表记录每一种前缀和出现的次数，假设我们当前枚举到元素 nums[j]，我们只需要查询哈希表中元素 
    /// sum[j]−goal 的数量即可，这些元素的数量即对应了以当前 j 值为右边界的满足条件的子数组的数量。最后这些元素的总数量即
    /// 为所有和为 goal 的子数组数量。
    /// 在实际代码中，我们实时地更新哈希表，以防止出现 i≥j 的情况。
    /// 
    /// int val = 0;
    /// cnt.TryGetValue(sum - goal, out val);
    /// 這行簡單說就是去 cnt裡面找出 有沒有 存在符合 sum - goal 這個 key
    /// 存在就回傳value數值, 找不到就回傳val 宣告數值 0
    /// out輸出可以宣告為bool, 這邊是宣告為int
    /// 
    /// TryGetValue 類似 ContainsKey
    /// 但是 TryGetValue 取值比用 ContainsKey 更快。
    /// 
    /// sum有點類似前綴和概念, 
    /// 固定左邊, 然後右邊一直往右跑找出新的組合出來
    /// goal是達成目標
    /// 達到goal目標就把res累加
    /// res是最終回傳結果
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="goal"></param>
    /// <returns></returns>
    public int NumSubarraysWithSum(int[] nums, int goal)
    {
        int sum = 0;
        int res = 0;
        Dictionary<int, int> cnt = new Dictionary<int, int>();

        foreach(int num in nums)
        {
            // 統計 sum 有那幾個數值且放到cnt裡面統計次數
            // key: sum of value, Value: 該總和累積之次數
            if(cnt.ContainsKey(sum))
            {
                cnt[sum]++;
            }
            else
            {
                cnt.Add(sum, 1);
            }

            sum += num;

            // 找到與cnt裡面相符合就res+1; 即代表解法之一
            // P[j] - p[i] = s ==> sum - goal
            int val = 0;
            cnt.TryGetValue(sum - goal, out val);
        }
        return res;
    }

    /// <summary>
    /// 解法二: 滑動視窗
    /// 注意到对于方法一中每一个 j，满足 sum[j]−sum[i]=goal 的 i 总是落在一个连续的区间中，i 值取区间中每一个数都满足条
    /// 件。并且随着 j 右移，其对应的区间的左右端点也将右移，这样我们即可使用滑动窗口解决本题。
    /// 
    /// 具体地，我们令滑动窗口右边界为 right，使用两个左边界 left1​ 和 left2​ 表示左区间 [left1​,left2​)，此时有 left2​−left1​ 个区间满足條件
    /// 在实际代码中，我们需要注意 left1​≤left2​≤right+1，因此需要在代码中限制 left1​ 和 left2​ 不超出范围。
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="goal"></param>
    /// <returns></returns>
    public int NumSubarraysWithSum2(int[] nums, int goal)
    {
        int n = nums.Length;
        int left1 = 0;
        int left2 = 0;
        int right = 0;
        int sum1 = 0;
        int sum2 = 0;
        int res = 0;

        while(right < n)
        {
            sum1 += nums[right];
            while(left1 <= right && sum1 > goal)
            {
                sum1 -= nums[left1];
                left1++;
            }
            sum2 += nums[right];
            while(left2 <= right && sum2 >= goal)
            {
                sum2 -= nums[left2];
                left2++;
            }

            res += left2 - left1;
            right++;
        }
        return res;
    }
}
