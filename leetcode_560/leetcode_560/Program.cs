using System.Net.Mail;

namespace leetcode_560;

class Program
{
    /// <summary>
    /// 560. Subarray Sum Equals K
    /// https://leetcode.com/problems/subarray-sum-equals-k/description/
    /// 560. 和為K 的子數組
    /// https://leetcode.cn/problems/subarray-sum-equals-k/description/
    /// 
    /// Given an array of integers nums and an integer k, return the total number of subarrays whose sum equals to k.
    /// A subarray is a contiguous non-empty sequence of elements within an array.
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();
        
        // 測試案例 1: nums = [1,1,1], k = 2
        int[] nums1 = [1, 1, 1];
        int k1 = 2;
        int result1 = solution.SubarraySum(nums1, k1);
        Console.WriteLine($"測試案例 1 (方法一): nums = [1,1,1], k = 2, 結果 = {result1} (預期: 2)");
        int result1b = solution.SubarraySum2(nums1, k1);
        Console.WriteLine($"測試案例 1 (方法二): nums = [1,1,1], k = 2, 結果 = {result1b} (預期: 2)");
        
        // 測試案例 2: nums = [1,2,3], k = 3
        int[] nums2 = [1, 2, 3];
        int k2 = 3;
        int result2 = solution.SubarraySum(nums2, k2);
        Console.WriteLine($"測試案例 2 (方法一): nums = [1,2,3], k = 3, 結果 = {result2} (預期: 2)");
        int result2b = solution.SubarraySum2(nums2, k2);
        Console.WriteLine($"測試案例 2 (方法二): nums = [1,2,3], k = 3, 結果 = {result2b} (預期: 2)");
        
        // 測試案例 3: nums = [1,-1,0], k = 0
        int[] nums3 = [1, -1, 0];
        int k3 = 0;
        int result3 = solution.SubarraySum(nums3, k3);
        Console.WriteLine($"測試案例 3 (方法一): nums = [1,-1,0], k = 0, 結果 = {result3} (預期: 3)");
        int result3b = solution.SubarraySum2(nums3, k3);
        Console.WriteLine($"測試案例 3 (方法二): nums = [1,-1,0], k = 0, 結果 = {result3b} (預期: 3)");
    }

    /// <summary>
    /// 方法一: 枚舉
    /// 
    /// 解題思路:
    /// 考慮以 i 結尾和為 k 的連續子數組個數，我們需要統計符合條件的下標 j 的個數，
    /// 其中 0 ≤ j ≤ i 且 [j..i] 這個子數組的和恰好為 k。
    /// 
    /// 核心概念:
    /// - 枚舉每個結尾位置 i
    /// - 對於每個結尾位置，反向枚舉起始位置 j (從 i 到 0)
    /// - 如果已知 [j, i] 的和，可以 O(1) 推出 [j-1, i] 的和
    /// - 只需在枚舉過程中累加元素，無需額外遍歷求和
    /// 
    /// 時間複雜度: O(n^2) - 兩層迴圈遍歷所有可能的子數組
    /// 空間複雜度: O(1) - 只使用常數額外空間
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <param name="k">目標和</param>
    /// <returns>和為 k 的連續子數組個數</returns>
    public int SubarraySum(int[] nums, int k)
    {
        int count = 0;
        
        // 枚舉每個結尾位置 i
        for(int start = 0; start < nums.Length; start++)
        {
            int sum = 0;
            
            // 從當前位置 start 反向枚舉起始位置 end
            // 這樣可以逐步累加元素，避免重複計算
            for(int end = start; end >= 0; end--)
            {
                // 將當前元素加入總和
                // 此時 sum 代表 [end, start] 區間的和
                sum += nums[end];
                
                // 如果區間和等於目標值 k，計數器加 1
                if(sum == k)
                {
                    count++;
                }
            }
        }
        
        return count;
    }

    /// <summary>
    /// 方法二: 前綴和 + 雜湊表優化
    /// 
    /// 解題思路:
    /// 方法一的瓶頸在於對每個 i，需要枚舉所有 j 來判斷是否符合條件。
    /// 我們可以使用前綴和配合雜湊表來優化這個過程。
    /// 
    /// 核心概念:
    /// 1. 定義 pre[i] 為 [0..i] 所有數的和
    /// 2. pre[i] = pre[i-1] + nums[i] (遞推關係)
    /// 3. [j..i] 子陣列和為 k 可轉化為: pre[i] - pre[j-1] = k
    /// 4. 移項得: pre[j-1] = pre[i] - k
    /// 5. 統計有多少個前綴和為 pre[i] - k 的位置即可
    /// 
    /// 雜湊表的作用:
    /// - 鍵: 前綴和的值
    /// - 值: 該前綴和出現的次數
    /// - 可在 O(1) 時間內查詢某個前綴和出現的次數
    /// 
    /// 為什麼初始化 map[0] = 1:
    /// 表示前綴和為 0 出現過一次，對應空陣列的情況。
    /// 這樣當 pre[i] = k 時，pre[i] - k = 0，可以找到這個匹配。
    /// 
    /// 時間複雜度: O(n) - 只需遍歷一次陣列
    /// 空間複雜度: O(n) - 雜湊表最多存儲 n 個不同的前綴和
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <param name="k">目標和</param>
    /// <returns>和為 k 的連續子陣列個數</returns>
    public int SubarraySum2(int[] nums, int k)
    {
        // 計數器：記錄符合條件的子陣列數量
        int count = 0;
        
        // 前綴和：記錄 [0..i] 的累加和
        int pre = 0;

        // 雜湊表：記錄每個前綴和出現的次數
        // Key: 前綴和的值
        // Value: 該前綴和出現的次數
        Dictionary<int, int> map = new Dictionary<int, int>();
        
        // 初始化：前綴和為 0 出現過 1 次（對應空陣列的情況）
        // 這樣當 pre[i] = k 時，可以找到 pre[i] - k = 0 的匹配
        map[0] = 1;

        // 從左到右遍歷陣列，邊更新前綴和邊計算答案
        for(int i = 0; i < nums.Length; i++)
        {
            // 更新前綴和：pre[i] = pre[i-1] + nums[i]
            pre += nums[i];
            
            // 查找是否存在前綴和為 (pre - k) 的位置
            // 如果存在，說明從那些位置到當前位置 i 的子陣列和為 k
            // pre[i] - pre[j-1] = k => pre[j-1] = pre[i] - k
            if(map.ContainsKey(pre - k))
            {
                // 將所有符合條件的子陣列數量加到計數器
                // map[pre - k] 表示前綴和為 (pre - k) 出現的次數
                count += map[pre - k];
            }

            // 將當前前綴和記錄到雜湊表中
            // 為後續的計算提供數據
            if(map.ContainsKey(pre))
            {
                // 如果該前綴和已存在，次數加 1
                map[pre]++;
            }
            else
            {
                // 如果是新的前綴和，初始化為 1
                map[pre] = 1;
            }
        }
        
        return count;
    }
}
