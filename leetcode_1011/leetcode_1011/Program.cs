namespace leetcode_1011;

class Program
{
    /// <summary>
    /// 1011. Capacity To Ship Packages Within D Days
    /// https://leetcode.com/problems/capacity-to-ship-packages-within-d-days/description/
    ///
    /// Problem:
    /// A conveyor belt has packages that must be shipped from one port to another within days days.
    /// The ith package on the conveyor belt has a weight of weights[i]. Each day, we load the ship with packages on the conveyor belt (in the order given by weights). We may not load more weight than the maximum weight capacity of the ship.
    /// Return the least weight capacity of the ship that will result in all the packages on the conveyor belt being shipped within days days.
    ///
    /// 繁體中文翻譯：
    /// 一條輸送帶上有數個包裹，必須在 days 天內從一個港口運送到另一個港口。
    /// 輸送帶上第 i 個包裹的重量為 `weights[i]`。每天按 `weights` 的順序裝船，但當日裝載總重量不得超過船的最大載重。
    /// 請回傳能使所有包裹在 days 天內運完的最小船載重能力。
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        Console.WriteLine("LeetCode 1011. Capacity To Ship Packages Within D Days");
        Console.WriteLine(new string('=', 50));

        var solution = new Program();

        // 測試案例 1: weights = [1,2,3,4,5,6,7,8,9,10], days = 5
        // 預期輸出: 15
        // 解釋: 船的最小載重能力為 15，可以在 5 天內運完所有包裹
        // 第 1 天: 1, 2, 3, 4, 5 (總重: 15)
        // 第 2 天: 6, 7 (總重: 13)
        // 第 3 天: 8 (總重: 8)
        // 第 4 天: 9 (總重: 9)
        // 第 5 天: 10 (總重: 10)
        int[] weights1 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        int days1 = 5;
        int result1 = solution.ShipWithinDays(weights1, days1);
        Console.WriteLine($"測試案例 1: weights = [{string.Join(", ", weights1)}], days = {days1}");
        Console.WriteLine($"結果: {result1}, 預期: 15, {(result1 == 15 ? "✓ 通過" : "✗ 失敗")}");
        Console.WriteLine();

        // 測試案例 2: weights = [3,2,2,4,1,4], days = 3
        // 預期輸出: 6
        // 解釋: 船的最小載重能力為 6，可以在 3 天內運完所有包裹
        // 第 1 天: 3, 2 (總重: 5)
        // 第 2 天: 2, 4 (總重: 6)
        // 第 3 天: 1, 4 (總重: 5)
        int[] weights2 = [3, 2, 2, 4, 1, 4];
        int days2 = 3;
        int result2 = solution.ShipWithinDays(weights2, days2);
        Console.WriteLine($"測試案例 2: weights = [{string.Join(", ", weights2)}], days = {days2}");
        Console.WriteLine($"結果: {result2}, 預期: 6, {(result2 == 6 ? "✓ 通過" : "✗ 失敗")}");
        Console.WriteLine();

        // 測試案例 3: weights = [1,2,3,1,1], days = 4
        // 預期輸出: 3
        // 解釋: 船的最小載重能力為 3，可以在 4 天內運完所有包裹
        // 第 1 天: 1, 2 (總重: 3)
        // 第 2 天: 3 (總重: 3)
        // 第 3 天: 1, 1 (總重: 2)
        // 第 4 天: (無包裹，提前完成)
        int[] weights3 = [1, 2, 3, 1, 1];
        int days3 = 4;
        int result3 = solution.ShipWithinDays(weights3, days3);
        Console.WriteLine($"測試案例 3: weights = [{string.Join(", ", weights3)}], days = {days3}");
        Console.WriteLine($"結果: {result3}, 預期: 3, {(result3 == 3 ? "✓ 通過" : "✗ 失敗")}");
    }

    /*
        要在 D 天内运完所有包裹，那么每天至少的承载量为 sum / D
        但是，因为一次至少运 1 个包裹，而这个包裹的重量可大可小，那么可能 weights[i] > sum / D
        假设包裹最大的重量为 maxWeight
        因此，最低承载量应该为 capacity = max(sum / D, maxWeight);

        最直接的方法，就是直接从 capacity 开始，每次 capacity++ 进行尝试，但这样效率很低
        因此我们可以使用二分查找， left = capacity， right = sum，即最低承载量为 capacity,最高的承载量为 包裹总量
        我们判断承载量 mid 是否能在 D 天内装完（无需刚好 D 天，只需要 [1, D] 即可），如果不能，表示承载量太小
        ，则 left = mid + 1，否则 right = mid
        因为必定存在一个答案，因此当退出循环， left = right 时，就是答案
    */

    /// <summary>
    /// 使用二分搜尋法找出能在指定天數內運完所有包裹的最小船載重能力。
    /// </summary>
    /// <remarks>
    /// <para><b>演算法：二分搜尋 (Binary Search)</b></para>
    /// <para><b>時間複雜度：</b>O(n × log(sum - max))，其中 n 為包裹數量</para>
    /// <para><b>空間複雜度：</b>O(1)</para>
    /// 
    /// <para><b>解題思路：</b></para>
    /// <list type="number">
    ///   <item>
    ///     <description>
    ///       確定搜尋範圍：
    ///       <list type="bullet">
    ///         <item>左邊界 (最小載重)：單一包裹最大重量 max(weights)，因為船至少要能裝下最重的包裹</item>
    ///         <item>右邊界 (最大載重)：所有包裹重量總和 sum(weights)，代表一天就能運完全部</item>
    ///       </list>
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       二分搜尋過程：
    ///       <list type="bullet">
    ///         <item>取中間值 mid 作為當前測試的載重能力</item>
    ///         <item>模擬運送過程，計算以 mid 載重需要的天數</item>
    ///         <item>若需要天數 ≤ days，表示載重可能過大，嘗試更小的值 (right = mid)</item>
    ///         <item>若需要天數 > days，表示載重不足，需要更大的值 (left = mid + 1)</item>
    ///       </list>
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>當 left == right 時，即為答案</description>
    ///   </item>
    /// </list>
    /// 
    /// <para><b>參考資料：</b></para>
    /// <see href="https://leetcode.cn/problems/capacity-to-ship-packages-within-d-days/solution/zai-d-tian-nei-song-da-bao-guo-de-neng-l-ntml/">LeetCode 官方題解</see>
    /// </remarks>
    /// <param name="weights">包裹重量陣列，依序排列在輸送帶上</param>
    /// <param name="days">必須在幾天內運完所有包裹</param>
    /// <returns>能在指定天數內運完所有包裹的最小船載重能力</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int[] weights = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    /// int result = solution.ShipWithinDays(weights, 5); // 回傳 15
    /// </code>
    /// </example>
    public int ShipWithinDays(int[] weights, int days)
    {
        // ==================== 步驟一：確定二分搜尋的範圍 ====================
        // left: 最小可能載重 = 最重的單一包裹 (因為船至少要能裝下每個包裹)
        // right: 最大可能載重 = 所有包裹重量總和 (代表一天運完全部)
        int left = 0, right = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            left = Math.Max(left, weights[i]);  // 取得最大包裹重量
            right += weights[i];                 // 計算總重量
        }

        // ==================== 步驟二：二分搜尋最小載重能力 ====================
        // 在 [max(weights), sum(weights)] 區間內搜尋
        while (left < right)
        {
            // 計算中間值作為當前測試的載重能力
            // 使用 left + (right - left) / 2 避免整數溢位
            int mid = left + ((right - left) / 2);

            // ==================== 步驟三：模擬運送過程 ====================
            // need: 以載重 mid 運送所有包裹需要的天數
            // cur: 當天已裝載的重量
            int need = 1, cur = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                // 檢查當前包裹加上去是否會超過載重限制
                if (cur + weights[i] > mid)
                {
                    // 超過載重限制，需要新的一天來運送
                    ++need;     // 天數加一
                    cur = 0;    // 重置當天載重
                }

                // 將當前包裹裝上船
                cur += weights[i];
            }

            // ==================== 步驟四：根據結果調整搜尋範圍 ====================
            if (need <= days)
            {
                // 需要天數 ≤ 限制天數，載重能力足夠 (可能還有餘裕)
                // 嘗試更小的載重，但 mid 是合法答案，不能排除
                right = mid;
            }
            else
            {
                // 需要天數 > 限制天數，載重能力不足
                // 需要更大的載重能力
                left = mid + 1;
            }
        }

        // 當 left == right 時，即為最小載重能力
        return left;
    }
}
