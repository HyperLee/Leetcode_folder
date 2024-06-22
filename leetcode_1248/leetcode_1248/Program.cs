namespace leetcode_1248
{
    internal class Program
    {
        /// <summary>
        /// 1248. Count Number of Nice Subarrays
        /// https://leetcode.com/problems/count-number-of-nice-subarrays/description/?envType=daily-question&envId=2024-06-22
        /// 1248. 统计「优美子数组」
        /// https://leetcode.cn/problems/count-number-of-nice-subarrays/description/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 1, 1, 2, 1, 1 };
            int k = 3;

            Console.WriteLine(NumberOfSubarrays2(nums, k));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/count-number-of-nice-subarrays/solutions/211268/tong-ji-you-mei-zi-shu-zu-by-leetcode-solution/
        /// https://leetcode.cn/problems/count-number-of-nice-subarrays/solutions/1735354/by-stormsunshine-b714/
        /// 滑動視窗解法
        /// https://leetcode.cn/problems/count-number-of-nice-subarrays/solutions/213352/hua-dong-chuang-kou-qian-zhui-he-bi-xu-miao-dong-b/
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int NumberOfSubarrays(int[] nums, int k)
        {
            int left = 0, right = 0, oddCount = 0, res = 0;

            while(right < nums.Length)
            {
                // 右指針往右走, 遇到奇數就把 oddcount + 1
                if ((nums[right++] & 1) == 1)
                {
                    oddCount++;
                }

                //  若当前滑动窗口 [left, right) 中有 k 个奇数了，进入此分支统计当前滑动窗口中的优美子数组个数。
                if (oddCount == k)
                {
                    // 先将滑动窗口的右边界向右拓展，直到遇到下一个奇数（或出界）
                    // rightEvenCnt 即为第 k 个奇数右边的偶数的个数
                    int tmp = right;
                    while (right < nums.Length && (nums[right] & 1) == 0)
                    {
                        right++;
                    }
                    int rightEvenCnt = right - tmp;

                    // leftEvenCnt 即为第 1 个奇数左边的偶数的个数
                    int leftEvenCnt = 0;
                    while ((nums[left] & 1) == 0)
                    {
                        leftEvenCnt++;
                        left++;
                    }

                    // 第 1 个奇数左边的 leftEvenCnt 个偶数都可以作为优美子数组的起点
                    // (因为第1个奇数左边可以1个偶数都不取，所以起点的选择有 leftEvenCnt + 1 种）
                    // 第 k 个奇数右边的 rightEvenCnt 个偶数都可以作为优美子数组的终点
                    // (因为第k个奇数右边可以1个偶数都不取，所以终点的选择有 rightEvenCnt + 1 种）
                    // 所以该滑动窗口中，优美子数组左右起点的选择组合数为 (leftEvenCnt + 1) * (rightEvenCnt + 1)
                    res += (leftEvenCnt + 1) * (rightEvenCnt + 1);

                    // 此时 left 指向的是第 1 个奇数，因为该区间已经统计完了，因此 left 右移一位，oddCnt--
                    left++;
                    oddCount--;
                }

            }

            return res;

        }


        /// <summary>
        /// https://leetcode.cn/problems/count-number-of-nice-subarrays/solutions/211268/tong-ji-you-mei-zi-shu-zu-by-leetcode-solution/
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int NumberOfSubarrays2(int[] nums, int k)
        {
            int n = nums.Length;
            // 紀錄奇數index位置, +2分別是頭尾擴大陣列 防止出界
            int[] odd = new int[n + 2];
            int ans = 0, cnt = 0;

            for(int i = 0; i < n; i++)
            {
                // 判斷是不是 奇數
                if ((nums[i] & 1) != 0)
                {
                    // 紀錄 每一個奇數下標(index)位置
                    odd[++cnt] = i;
                }
            }

            // 頭 給值, 邊界運算
            odd[0] = -1;
            // 尾 給值
            odd[++cnt] = n;

            for (int i = 1; i + k <= n; i++)
            {
                // 左右兩邊[l,r]  計算個數
                // 對於每個奇數, 計算以他為第一個起始位置能構成的優美子數組 列入答案加總
                ans += (odd[i] - odd[i - 1]) * (odd[i + k] - odd[i + k - 1]);
            }

            return ans;
        }
    }
}
