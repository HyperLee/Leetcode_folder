namespace leetcode_3354;

class Program
{
    /// <summary>
    /// 3354. Make Array Elements Equal to Zero
    /// https://leetcode.com/problems/make-array-elements-equal-to-zero/description/?envType=daily-question&envId=2025-10-28
    /// 3354. 使数组元素等于零
    /// https://leetcode.cn/problems/make-array-elements-equal-to-zero/description/?envType=daily-question&envId=2025-10-28
    /// 
    /// 題目描述：
    /// 給定一個整數陣列 nums。
    /// 
    /// 首先選擇一個起始位置 curr，使得 nums[curr] == 0，並選擇一個移動方向（向左或向右）。
    /// 
    /// 之後，重複執行以下過程：
    /// - 如果 curr 超出範圍 [0, n - 1]，此過程結束。
    /// - 如果 nums[curr] == 0，則按照當前方向移動，如果向右移動則 curr 加 1，如果向左移動則 curr 減 1。
    /// - 否則如果 nums[curr] > 0：
    ///   - 將 nums[curr] 減 1。
    ///   - 反轉移動方向（左變右，右變左）。
    ///   - 朝新方向移動一步。
    /// 
    /// 如果在過程結束時，nums 中的每個元素都變為 0，則初始位置 curr 和移動方向的選擇被視為有效。
    /// 
    /// 返回可能的有效選擇數量。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解題思路：前綴和
    /// 
    /// 核心概念：
    /// 把整個過程看成是「打磚塊」遊戲，對於每一個選取的初始位置，
    /// 有一個小球在左右方向上來回彈跳，每次遇到正數就反彈，同時將正數減少 1。
    /// 
    /// 算法說明：
    /// 1. 為了消除所有的正數，假設初始方向向右，那麼初始位置兩邊元素的和應該相等，
    ///    或者左邊元素和比右邊元素和大 1，此時球會向右消除完右邊後，反彈回來消除左邊，最後從右邊離開。
    /// 2. 初始方向向左時，情況是對稱的：右邊元素和應該等於或比左邊元素和大 1。
    /// 3. 枚舉每一個為 0 的位置作為初始位置，並利用前綴和計算出左右兩邊的元素和，
    ///    判斷是否為有效選擇方案。
    /// 
    /// 時間複雜度：O(n)
    /// 空間複雜度：O(1)
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <returns>可能的有效選擇數量</returns>
    public int CountValidSelections(int[] nums)
    {
        int n = nums.Length;
        int res = 0;
        // 計算陣列總和
        int sum = nums.Sum();
        int left = 0;   // 左邊元素和
        int right = sum; // 右邊元素和

        // 枚舉每一個位置
        for (int i = 0; i < n; i++)
        {
            // 如果當前位置為 0，可以作為初始位置
            if (nums[i] == 0)
            {
                // 向右移動：如果左邊和 >= 右邊和，且差值 <= 1，則為有效選擇
                // left == right：兩邊相等，剛好消除所有元素
                // left == right + 1：左邊多 1，向右消除完右邊後反彈回來消除左邊，最後從右邊離開
                if (left - right >= 0 && left - right <= 1)
                {
                    res++;
                }

                // 向左移動：如果右邊和 >= 左邊和，且差值 <= 1，則為有效選擇
                // right == left：兩邊相等，剛好消除所有元素
                // right == left + 1：右邊多 1，向左消除完左邊後反彈回來消除右邊，最後從左邊離開
                if (right - left >= 0 && right - left <= 1)
                {
                    res++;
                }
            }
            else
            {
                // 更新前綴和：當前元素加入左邊，從右邊移除
                left += nums[i];
                right -= nums[i];
            }
        }
        return res;
    }
}
