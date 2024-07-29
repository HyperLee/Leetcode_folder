using System.Runtime.ExceptionServices;

namespace leetcode_1395
{
    internal class Program
    {
        /// <summary>
        /// 1395. Count Number of Teams
        /// https://leetcode.com/problems/count-number-of-teams/description/?envType=daily-question&envId=2024-07-29
        /// 1395. 统计作战单位数
        /// https://leetcode.cn/problems/count-number-of-teams/description/
        /// 
        /// 簡單說就是找出3個數字(評分)是 遞增 或是 遞減的組合 
        /// 3個index分別是 i, j, k,  0 <= i < j < k < n
        /// 3個index不需要連續的index
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 2, 5, 3, 4, 1 };
            Console.WriteLine("枚舉方式: " + NumTeams(input));
            Console.WriteLine("枚舉中間點: " + NumTeams(input));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/count-number-of-teams/solutions/186425/tong-ji-zuo-zhan-dan-wei-shu-by-leetcode-solution/
        /// https://leetcode.cn/problems/count-number-of-teams/solutions/1609413/by-ac_oier-qm3a/
        /// https://leetcode.cn/problems/count-number-of-teams/solutions/1460260/by-flix-3lu9/
        /// 
        /// 枚舉方式  將可能組合 都列出來
        /// 3個index為一個組合
        /// 數字組合順序為 遞增 || 遞減
        /// 符合就 res++
        /// i, j, k 可為非連續 index.
        /// 0 <= i < j < k < n
        /// 
        /// 时间复杂度：O(N^3), 3個for迴圈。
        /// 空间复杂度：O(1)。
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        public static int NumTeams(int[] rating)
        {
            int n = rating.Length;
            int res = 0;

            for(int i = 0; i < n; i++)
            {
                for(int j = i + 1; j < n; j++)
                {
                    for(int k = j + 1; k < n; k++)
                    {
                        // 數字組合為 遞增 || 遞減 組合, i, j, k 可為非連續
                        if ((rating[i] < rating[j] && rating[j] < rating[k]) || (rating[i] > rating[j] && rating[j] > rating[k]))
                        {
                            res++;
                        }
                    }
                }
            }

            return res;
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/count-number-of-teams/solutions/186425/tong-ji-zuo-zhan-dan-wei-shu-by-leetcode-solution/
        /// https://leetcode.cn/problems/count-number-of-teams/solutions/1609413/by-ac_oier-qm3a/
        /// https://leetcode.cn/problems/count-number-of-teams/solutions/1460260/by-flix-3lu9/
        /// 
        /// 枚舉中間點,  以位置 j 來當作中間點(固定不動, 找出會變動的左右兩邊)
        /// 固定好 j 之後, 接下來有幾種情況. 能找出 遞增 || 遞減
        /// 1. 位置 j 左方, 出現 評分比 j 還要低的 i_low
        /// 2. 位置 j 左方, 出現 評分比 j 還要高得 i_height
        /// 3. 位置 j 右方, 出現 評分比 j 還要低的 k_low
        /// 4. 位置 j 右方, 出現 評分比 j 還要高的 k_height
        /// 
        /// 上述四情況可組合出
        /// (i_low, j, k_height) => 遞增
        /// (i_height, j, k_low) => 遞減
        /// 
        /// 时间复杂度：O(N^2), 2個for迴圈
        /// 空间复杂度：O(1)。
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        public static int NumTeams2(int[] rating)
        {
            int n = rating.Length;
            int res = 0;

            // i 在 j 前面, 故從 1 開始
            // j 後面還有k, 故 n - 1
            for(int j = 1; j < n - 1; j++)
            {
                int i_low = 0, i_high = 0;
                int k_low = 0, k_high = 0;

                // 找出 i 的 遞增與遞減, i 在 j 前面
                for(int i = 0; i < j; i++)
                {
                    if (rating[i] < rating[j])
                    {
                        // 遞減
                        i_low++;
                    }
                    else if (rating[i] > rating[j])
                    {
                        // 遞增
                        i_high++;
                    }
                }

                // 找出 k 的 遞增與遞減, k 在 j 後面
                for (int k = j + 1; k < n; k++)
                {
                    if (rating[k] < rating[j])
                    {
                        // 遞減
                        k_low++;
                    }
                    else if (rating[k] > rating[j])
                    {
                        // 遞增
                        k_high++;
                    }
                }

                // 計算組合數量 先乘法在相加
                res += i_low * k_high + i_high * k_low;
            }

            return res;
        }

    }
}
