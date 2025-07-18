namespace leetcode_2163;

class Program
{
    /// <summary>
    /// 2163. Minimum Difference in Sums After Removal of Elements
    /// https://leetcode.com/problems/minimum-difference-in-sums-after-removal-of-elements/description/?envType=daily-question&envId=2025-07-18
    /// 2163. 刪除元素後和的最小差值
    /// https://leetcode.cn/problems/minimum-difference-in-sums-after-removal-of-elements/description/?envType=daily-question&envId=2025-07-18
    /// 
    /// 題目描述：
    /// 給你一個下標從 0 開始、長度為 3 * n 的整數陣列 nums。
    /// 你可以移除 nums 中恰好 n 個元素，剩下的 2 * n 個元素會被分成兩個部分：
    /// 前 n 個元素屬於第一部分，和為 sumfirst；接下來 n 個元素屬於第二部分，和為 sumsecond。
    /// 差值為 sumfirst - sumsecond。
    /// 請返回移除 n 個元素後，兩部分和的最小差值。
    /// </summary>
    static void Main(string[] args)
    {
        var solution = new Program();
        
        // 測試案例 1: nums = [3,1,2,6,5,4], n = 2
        // 預期結果: -1
        int[] nums1 = {3, 1, 2, 6, 5, 4};
        long result1 = solution.MinimumDifference(nums1);
        long result1_v2 = solution.MinimumDifference2(nums1);
        Console.WriteLine($"測試案例 1: nums = [{string.Join(", ", nums1)}]");
        Console.WriteLine($"解法一結果: {result1}, 解法二結果: {result1_v2}, 預期: -1");
        Console.WriteLine();

        // 測試案例 2: nums = [7,9,8,6,2,6], n = 2  
        // 預期結果: -1
        int[] nums2 = {7, 9, 8, 6, 2, 6};
        long result2 = solution.MinimumDifference(nums2);
        long result2_v2 = solution.MinimumDifference2(nums2);
        Console.WriteLine($"測試案例 2: nums = [{string.Join(", ", nums2)}]");
        Console.WriteLine($"解法一結果: {result2}, 解法二結果: {result2_v2}, 預期: -1");
        Console.WriteLine();

        // 測試案例 3: nums = [7,9,8,6,2,6,1,1,1], n = 3
        // 較大的測試案例
        int[] nums3 = {7, 9, 8, 6, 2, 6, 1, 1, 1};
        long result3 = solution.MinimumDifference(nums3);
        long result3_v2 = solution.MinimumDifference2(nums3);
        Console.WriteLine($"測試案例 3: nums = [{string.Join(", ", nums3)}]");
        Console.WriteLine($"解法一結果: {result3}, 解法二結果: {result3_v2}");
    }


    /// <summary>
    /// 使用優先佇列求解刪除元素後和的最小差值
    /// 
    /// 解題思路：
    /// 1. 在 [n, 2n] 中選擇分割點 k，前 k 個數屬於第一部分，後 3n-k 個數屬於第二部分
    /// 2. 第一部分選擇 n 個最小元素，第二部分選擇 n 個最大元素
    /// 3. 使用大根堆維護第一部分的 n 個最小元素
    /// 4. 使用小根堆維護第二部分的 n 個最大元素
    /// 5. 計算所有可能的差值並返回最小值
    /// 
    /// ref:https://leetcode.cn/problems/minimum-difference-in-sums-after-removal-of-elements/solutions/1249409/shan-chu-yuan-su-hou-he-de-zui-xiao-chai-ah0j/?envType=daily-question&envId=2025-07-18
    /// 
    /// </summary>
    /// <param name="nums">長度為 3*n 的整數陣列</param>
    /// <returns>移除 n 個元素後兩部分和的最小差值</returns>
    public long MinimumDifference(int[] nums)
    {
        int n3 = nums.Length;   // 陣列總長度 3*n
        int n = n3 / 3;         // 每部分需要的元素個數
        long[] part1 = new long[n + 1];  // 儲存第一部分的最小和
        long sum = 0;           // 當前第一部分的總和

        // 使用大根堆維護第一部分的 n 個最小元素
        // 大根堆會將最大值放在堆頂，便於移除較大元素
        var ql = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b - a));
        
        // 初始化：將前 n 個元素放入大根堆
        for (int i = 0; i < n; i++)
        {
            sum += nums[i];
            ql.Enqueue(nums[i], nums[i]);
        }

        part1[0] = sum;  // 記錄 nums[0..n-1] 中 n 個最小元素的和
        
        // 處理 [n, 2n) 範圍，動態維護第一部分的最小和
        for (int i = n; i < n * 2; i++)
        {
            sum += nums[i];           // 加入新元素
            ql.Enqueue(nums[i], nums[i]);
            sum -= ql.Dequeue();      // 移除最大元素，保持 n 個最小元素
            part1[i - (n - 1)] = sum; // 記錄 nums[0..i] 中 n 個最小元素的和
        }

        // 處理第二部分：使用小根堆維護 n 個最大元素
        long part2 = 0;
        var qr = new PriorityQueue<int, int>();  // 小根堆，堆頂是最小值
        
        // 初始化：將後 n 個元素放入小根堆
        for (int i = n * 3 - 1; i >= n * 2; i--)
        {
            part2 += nums[i];
            qr.Enqueue(nums[i], nums[i]);
        }

        // 初始差值：part1[n] 對應 nums[0..2n-1] 中 n 個最小元素的和
        long res = part1[n] - part2;
        
        // 逆序處理 [n, 2n) 範圍，動態維護第二部分的最大和
        for (int i = n * 2 - 1; i >= n; i--)
        {
            part2 += nums[i];           // 加入新元素
            qr.Enqueue(nums[i], nums[i]);
            part2 -= qr.Dequeue();      // 移除最小元素，保持 n 個最大元素
            // 更新最小差值：part1[i-n] 對應 nums[0..i-1] 中 n 個最小元素的和
            res = Math.Min(res, part1[i - n] - part2);
        }

        return res;
    }

    /// <summary>
    /// 解法二：枚举分割位置求解刪除元素後和的最小差值
    /// 
    /// 解題思路：
    /// 1. 删除元素是障眼法，把重点放在怎么选 2n 个数上
    /// 2. 把 nums 分割成两部分，第一部分选 n 个数求和 s1，第二部分选 n 个数求和 s2
    /// 3. 为了让 s1 - s2 尽量小，s1 越小越好，s2 越大越好
    /// 4. 计算前缀最小和 preMin[i]：nums[0] 到 nums[i] 中最小的 n 个元素之和
    /// 5. 计算后缀最大和 sufMax[i]：nums[i] 到 nums[3n-1] 中最大的 n 个元素之和
    /// 6. 答案为 preMin[i] - sufMax[i+1] 中的最小值
    /// 
    /// 时间复杂度：O(n log n)
    /// 空间复杂度：O(n)
    /// </summary>
    /// <param name="nums">長度為 3*n 的整數陣列</param>
    /// <returns>移除 n 個元素後兩部分和的最小差值</returns>
    public long MinimumDifference2(int[] nums)
    {
        int m = nums.Length;    // 陣列總長度 3*n
        int n = m / 3;          // 每部分需要的元素個數
        
        // 使用小根堆維護後綴最大和的 n 個最大元素
        var minPQ = new PriorityQueue<int, int>();
        long sum = 0;
        
        // 初始化：從右側開始，將最後 n 個元素加入小根堆
        for (int i = m - 1; i >= m - n; i--)
        {
            minPQ.Enqueue(nums[i], nums[i]);
            sum += nums[i];
        }

        // 計算後綴最大和陣列
        long[] sufMax = new long[m - n + 1];
        sufMax[m - n] = sum;    // 最右側位置的後綴最大和
        
        // 從右往左處理，維護 n 個最大元素
        for (int i = m - n - 1; i >= n; i--)
        {
            int v = nums[i];
            // 如果當前元素比堆頂（最小元素）大，則替換
            if (v > minPQ.Peek())
            {
                sum += v - minPQ.Dequeue();
                minPQ.Enqueue(v, v);
            }
            sufMax[i] = sum;
        }

        // 使用大根堆維護前綴最小和的 n 個最小元素
        var maxPQ = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b - a));
        long preMin = 0;    // 前綴最小和
        
        // 初始化：將前 n 個元素加入大根堆
        for (int i = 0; i < n; i++)
        {
            maxPQ.Enqueue(nums[i], nums[i]);
            preMin += nums[i];
        }

        // 初始答案：i=n-1 時的答案
        long ans = preMin - sufMax[n];
        
        // 從左往右處理，維護 n 個最小元素並計算答案
        for (int i = n; i < m - n; i++)
        {
            int v = nums[i];
            // 如果當前元素比堆頂（最大元素）小，則替換
            if (v < maxPQ.Peek())
            {
                preMin += v - maxPQ.Dequeue();
                maxPQ.Enqueue(v, v);
            }
            ans = Math.Min(ans, preMin - sufMax[i + 1]);
        }
        
        return ans;
    }
}
