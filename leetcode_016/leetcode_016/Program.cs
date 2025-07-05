namespace leetcode_016;

class Program
{
    /// <summary>
    /// 16. 3Sum Closest
    /// https://leetcode.com/problems/3sum-closest/description/
    /// 16. 最接近的三數之和
    /// https://leetcode.cn/problems/3sum-closest/description/
    /// 
    /// 題目描述：
    /// 給定一個長度為 n 的整數陣列 nums 和一個整數 target，請你從 nums 中找出三個整數，使得它們的和最接近 target。
    /// 返回這三個整數的和。
    /// 你可以假設每組輸入只會對應一個答案。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[] nums1 = {-1, 2, 1, -4};
        int target1 = 1;
        int result1 = ThreeSumClosest(nums1, target1);
        Console.WriteLine($"範例1: 輸入: nums = [-1,2,1,-4], target = 1，輸出: {result1}");

        int[] nums2 = {0, 0, 0};
        int target2 = 1;
        int result2 = ThreeSumClosest(nums2, target2);
        Console.WriteLine($"範例2: 輸入: nums = [0,0,0], target = 1，輸出: {result2}");

        int[] nums3 = {1, 1, 1, 0};
        int target3 = -100;
        int result3 = ThreeSumClosest(nums3, target3);
        Console.WriteLine($"範例3: 輸入: nums = [1,1,1,0], target = -100，輸出: {result3}");
    }

    /// <summary>
    /// 傳回最接近 target 的三數之和。
    /// 解題說明：
    /// 先將陣列排序，然後固定一個數字 a，利用雙指針法在剩下的區間內尋找 b、c，使 a+b+c 最接近 target。
    /// 每次計算當前三數和與 target 的距離，若更接近則更新答案。
    /// 若三數和等於 target，直接回傳。
    /// 為避免重複計算，遇到重複元素時跳過。
    /// 時間複雜度 O(n^2)。
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <param name="target">目標值</param>
    /// <returns>最接近 target 的三數之和</returns>
    public static int ThreeSumClosest(int[] nums, int target)
    {
        // 先排序，方便雙指針移動
        Array.Sort(nums);
        int n = nums.Length;
        int best = int.MaxValue; // 初始化答案為極大值

        // 枚舉第一個數字 a
        for (int i = 0; i < n; ++i)
        {
            // 跳過重複的 a，避免重複計算
            if (i > 0 && nums[i] == nums[i - 1])
            {
                continue;
            }
            // 設定雙指針，j 指向 a 右邊，k 指向陣列尾端
            int j = i + 1, k = n - 1;
            while (j < k)
            {
                int sum = nums[i] + nums[j] + nums[k]; // 計算三數和
                // 如果剛好等於 target，直接回傳
                if (sum == target)
                {
                    return target;
                }

                // 若更接近 target，則更新答案
                if (Math.Abs(sum - target) < Math.Abs(best - target))
                {
                    best = sum;
                }

                if (sum > target)
                {
                    // 如果和大於 target，k 左移，並跳過重複元素
                    int k0 = k - 1;
                    while (j < k0 && nums[k0] == nums[k])
                    {
                        --k0;
                    }
                    k = k0;
                }
                else
                {
                    // 如果和小於 target，j 右移，並跳過重複元素
                    int j0 = j + 1;
                    while (j0 < k && nums[j0] == nums[j])
                    {
                        ++j0;
                    }
                    j = j0;
                }
            }
        }
        // 回傳最接近 target 的三數和
        return best;
    }
}
