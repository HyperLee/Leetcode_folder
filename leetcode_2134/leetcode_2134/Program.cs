namespace leetcode_2134
{
    internal class Program
    {
        /// <summary>
        /// 2134. Minimum Swaps to Group All 1's Together II
        /// https://leetcode.com/problems/minimum-swaps-to-group-all-1s-together-ii/description/?envType=daily-question&envId=2024-08-02
        /// 
        /// 2134. 最少交换次数来组合所有的 1 II
        /// https://leetcode.cn/problems/minimum-swaps-to-group-all-1s-together-ii/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 0, 1, 0, 1, 1, 0, 0 };
            Console.WriteLine(MinSwaps(input));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/minimum-swaps-to-group-all-1s-together-ii/solutions/1202043/zui-shao-jiao-huan-ci-shu-lai-zu-he-suo-iaghf/
        /// https://leetcode.cn/problems/minimum-swaps-to-group-all-1s-together-ii/solutions/2591173/2134-zui-shao-jiao-huan-ci-shu-lai-zu-he-u617/
        /// 
        /// nums 陣列是binary, 也就是說只有 0, 1
        /// 
        /// 滑動視窗統計 1 數量, 再用總數去撿
        /// 
        /// 改變思考模式, 統計區間內 0 的個數
        /// </summary>
        /// <param name="nums">binary circular array </param>
        /// <returns></returns>
        public static int MinSwaps(int[] nums)
        {
            int n = nums.Length;
            int cnt = 0;

            for(int i = 0; i < n; i++)
            {
                // 統計陣列中 1 的個數
                cnt += nums[i];
            }

            if(cnt == 0)
            {
                return 0;
            }

            int cur = 0;
            for(int i = 0; i < cnt; i++)
            {
                // 統計 [0, cnt) 區間內 0 個數
                cur += (1 - nums[i]);
            }

            int ans = cur;
            // 固定長度滑動視窗, i = 1 開始
            // 區間內 0 與區間外要互換
            // 先跑區間1, 離開後進區間2
            for(int i = 1; i < n; i++)
            {
                // 區間1
                if (nums[i - 1] == 0)
                {
                    cur--;
                }

                // 區間2
                if (nums[(i + cnt - 1) % n] == 0)
                {
                    cur++;
                }

                ans = Math.Min(ans, cur);
            }

            return ans;
        }
    }
}
