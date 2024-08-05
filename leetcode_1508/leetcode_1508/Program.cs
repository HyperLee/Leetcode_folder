namespace leetcode_1508
{
    internal class Program
    {
        /// <summary>
        /// 1508. Range Sum of Sorted Subarray Sums
        /// https://leetcode.com/problems/range-sum-of-sorted-subarray-sums/description/?envType=daily-question&envId=2024-08-04
        /// 
        /// 1508. 子数组和排序后的区间和
        /// https://leetcode.cn/problems/range-sum-of-sorted-subarray-sums/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 2, 3, 4 };
            int n = 4;
            int left = 1;
            int right = 5;

            Console.WriteLine(RangeSum(input, n, left, right));
            Console.ReadKey();
        }


        /// <summary>
        /// 排序
        /// ref:
        /// https://leetcode.cn/problems/range-sum-of-sorted-subarray-sums/solutions/371273/zi-shu-zu-he-pai-xu-hou-de-qu-jian-he-by-leetcode-/
        /// https://leetcode.cn/problems/range-sum-of-sorted-subarray-sums/solutions/1817549/-by-mkdir700-l90o/
        /// https://leetcode.cn/problems/range-sum-of-sorted-subarray-sums/solutions/326456/onlogs-jie-fa-er-fen-shuang-zhi-zhen-by-newhar/
        /// 
        /// subarray 長度計算方式: n * (n + 1) / 2
        /// => 題目給的
        /// 
        /// 需告一個新的 array 用來存放 subarray 資料
        /// 枚舉 nums 資料 加總之後 放入 sums 裡面
        /// 再來要排序, 也是題目要求
        /// 最後 取出 要加總的 範圍
        /// 陣列 index 是從 0 開始
        /// 但是題目的 left, right 是從 1 開始
        /// 這邊計算時候要小心, 不然會計算錯誤
        /// => 題目有說 left, right 是從 1 開始
        /// 
        /// 最後計算的 解答要取 mod
        /// => modulo 10^9 + 7
        /// 這也是題目要求
        /// </summary>
        /// <param name="nums">輸入陣列</param>
        /// <param name="n">n 個正整數</param>
        /// <param name="left">左起始點, 題目是從 1 開始, 需要注意</param>
        /// <param name="right">右截止點, 題目是從 1 開始, 需要注意</param>
        /// <returns></returns>
        public static int RangeSum(int[] nums, int n, int left, int right)
        {
            const int mod = 1000000007;
            // 計算 subarray 長度
            int sumlength = n * (n + 1) / 2;
            // 列舉全部 subarray 長度
            int[] sums = new int[sumlength];
            // 0 開始
            int index = 0;

            // 將 array 枚舉, 計算 subarray 總和放入 sums
            for(int i = 0; i < n; i++)
            {
                // i 更換就重新, 要注意
                int sum = 0;
                for(int j = i; j < n; j++)
                {
                    sum += nums[j];
                    sums[index++] = sum;
                }
            }

            // 遞增排序
            Array.Sort(sums);

            int ans = 0;
            for(int i = left - 1; i < right; i++)
            {
                // 答案取 mod
                ans = (ans + sums[i]) % mod;
            }

            return ans;
        }
    }
}
