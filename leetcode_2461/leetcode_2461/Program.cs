using System;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices.Marshalling;

namespace leetcode_2461
{
    internal class Program
    {
        /// <summary>
        /// 2461. Maximum Sum of Distinct Subarrays With Length K
        /// https://leetcode.com/problems/maximum-sum-of-distinct-subarrays-with-length-k/description/?envType=daily-question&envId=2024-11-19
        /// 
        /// 2461. 长度为 K 子数组中的最大和
        /// https://leetcode.cn/problems/maximum-sum-of-distinct-subarrays-with-length-k/description/
        /// 
        /// 您被給定一個整數陣列 nums 和一個整數 k。請找出所有滿足以下條件的子陣列中，最大的子陣列總和：
        /// 子陣列的長度為 k，且
        /// 子陣列中的所有元素都是互不相同的。
        /// 返回滿足上述條件的所有子陣列中最大的子陣列總和。如果沒有任何子陣列符合條件，則返回 0。
        /// 子陣列是陣列中連續且非空的一段元素序列。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 5, 4, 2, 9, 9, 9 };
            int k = 3;
            Console.WriteLine(MaximumSubarraySum(input, k));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/maximum-sum-of-distinct-subarrays-with-length-k/solutions/2757534/2461-chang-du-wei-k-zi-shu-zu-zhong-de-z-ge3d/
        /// https://leetcode.cn/problems/maximum-sum-of-distinct-subarrays-with-length-k/solutions/1951940/hua-dong-chuang-kou-by-endlesscheng-m0gm/
        /// 
        /// 滑動視窗 + Hash
        /// 找出 subarray + 不重複數字
        /// 
        /// 
        /// 發現 Dictionary.TryAdd 忽然可以用了
        /// 以前都沒辦法編譯
        /// 這邊有示範改寫方法
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static long MaximumSubarraySum(int[] nums, int k)
        {
            // 将最大子数组和初始化为 0
            long maxSum = 0;
            long sum = 0;
            // hash table
            IDictionary<int, int> counts = new Dictionary<int, int>();
            int n = nums.Length;
            // 初始化(先找第一輪), 先塞前 k 個 element.  範圍: [0, k - 1]
            for (int i = 0; i < k; i++)
            {
                int num = nums[i];
                sum += num;

                #region TryAdd用法
                // 預設是 0, 有值就 + 1
                //counts.TryAdd(num, 0);
                //counts[num]++;
                #endregion

                if (counts.ContainsKey(num))
                {
                    counts[num]++;
                }
                else
                {
                    counts.Add(num, 1);
                }
            }

            // 如果 element 剛好 k 個, 使用該 sum 當成最大和 maxSum
            if (counts.Count == k)
            {
                maxSum = sum;
            }

            // 对于 k ≤ i < n 的每个下标 i，执行如下操作。
            for (int i = k; i < n; i++)
            {
                // 滑動視窗(移除左邊界, 新增右邊界)
                // 1. 将子数组元素和减少 nums[i - k] 并增加 nums[i]。
                int prev = nums[i - k];
                int curr = nums[i];
                sum = sum - prev + curr;
                // 2. 在哈希表中将 nums[i - k] 的出现次数减少 1，将 nums[i] 的出现次数增加 1，并将出现次数变成 0 的元素从哈希表中移除。
                counts[prev]--;
                
                if (counts[prev] == 0)
                {
                    // prev 數量為 0, 就移出
                    counts.Remove(prev);
                }

                #region TryAdd用法
                // 預設是 0, 有值就 + 1
                //counts.TryAdd(curr, 0);
                //counts[curr]++;
                #endregion

                if (counts.ContainsKey(curr))
                {
                    counts[curr]++;
                }
                else
                {
                    counts.Add(curr, 1);
                }

                // 如果哈希表中的不同元素个数等于 k，则使用子数组元素和更新最大子数组和。
                if (counts.Count == k)
                {
                    maxSum = Math.Max(maxSum, sum);
                }
            }

            return maxSum;
        }
    }
}
