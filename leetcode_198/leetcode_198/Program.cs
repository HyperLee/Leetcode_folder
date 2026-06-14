namespace leetcode_198;

class Program
{
    /// <summary>
    /// 198. House Robber
    /// https://leetcode.com/problems/house-robber/description/
    /// 198. 打家劫舍
    /// https://leetcode.cn/problems/house-robber/description/
    ///
    /// You are a professional robber planning to rob houses along a street. Each house has a certain amount of money
    /// stashed, the only constraint stopping you from robbing each of them is that adjacent houses have security systems
    /// connected and it will automatically contact the police if two adjacent houses were broken into on the same night.
    ///
    /// Given an integer array nums representing the amount of money of each house, return the maximum amount of money
    /// you can rob tonight without alerting the police.
    ///
    /// 你是一名專業的小偷，正計劃沿著一條街偷竊每一間房子。每間房子都藏有一定數量的現金，
    /// 唯一限制你不能每間都偷的是：相鄰的房屋裝有彼此連動的防盜系統，
    /// 如果同一晚有兩間相鄰的房屋被闖入，系統就會自動通知警方。
    ///
    /// 給定一個整數陣列 nums，表示每間房子存放的金額，請回傳你今晚在不驚動警方的情況下，
    /// 最多可以偷到的金額。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一: 動態規劃, 為優化空間版本
    /// 題目有說, 不能偷相鄰(連續)房屋, 要隔間偷
    /// 如果只有一間房屋, 最大值就是 第一間 dp[0] = nums[0]
    /// 如果有兩間房屋, 就是找出兩者中最大的 dp[1] = max(nums[0],nums[1])。
    /// 當 i >= 2
    /// 1. 如果偷窃第 i 间房屋，则不能偷窃第 i − 1 间房屋，最高金额为下标范围 [0,i − 2] 中能够偷窃到的最高金额加 nums[i]
    /// ，此时的最高金额是 dp[i − 2] + nums[i]。
    ///  => 簡單說偷竊第 i 間就是前 i - 2 間的總和 dp[i - 2] 加上第 i 間的數值 nums[i]
    /// 2. 如果不偷窃第 i 间房屋，则最高金额为下标范围 [0,i − 1] 中能够偷窃到的最高金额，此时的最高金额是 dp[i − 1]。
    ///  => 不偷竊第 i 間也就是取前 i - 1 間的總和 dp[i - 1]
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int Rob(int[] nums)
    {
        int n = nums.Length;
        // 總長度只有 1, 直接回傳那一間的數值
        if(n == 1)
        {
            return nums[0];
        }

        int[] dp = new int[n];
        dp[0] = nums[0];
        // 長度 2, 取出這兩間最大者
        dp[1] = Math.Max(nums[0], nums[1]);

        // 由於一間與兩間已經列舉, 第三間開始要用計算的
        // 前 i 間之總和
        for(int i = 2; i < n; i++)
        {
            // 前者是偷竊第 i 間, 前 i 輪總和 + 第 i 間的價值
            // 後者是不偷竊的第 i 間
            dp[i] = Math.Max(dp[i - 2] + nums[i], dp[i - 1]);
        }

        // 陣列從 0 開始取 n - 1 才不會超出邊界
        return dp[n - 1];
    }

    /// <summary>
    /// 解法二: 解法一的空間優化版本
    /// 
    /// 最大值加總方式 有沒有覺得很類似 fibonacci 
    /// 既然是這樣 那就修改寫法 優化
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int Rob2(int[] nums)
    {
        int n = nums.Length;
        if(n == 1)
        {
            return nums[0];
        }

        if(n == 2)
        {
            return Math.Max(nums[0], nums[1]);
        }

        int prev = nums[0];
        int curr = Math.Max(nums[0], nums[1]);
        for(int i = 2; i < n; i++)
        {
            int next = Math.Max(prev + nums[i], curr);
            prev = curr;
            curr = next;
        }
        return curr;
    }
}
