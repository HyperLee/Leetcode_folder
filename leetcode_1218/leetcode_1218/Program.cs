using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1218
{
    internal class Program
    {
        /// <summary>
        /// 1218. Longest Arithmetic Subsequence of Given Difference
        /// https://leetcode.com/problems/longest-arithmetic-subsequence-of-given-difference/
        /// 1218. 最长定差子序列
        /// https://leetcode.cn/problems/longest-arithmetic-subsequence-of-given-difference/
        /// 
        /// 找出 arr 中 等差為 difference  的 最常子序列
        /// 可以刪除 element 但是不能 變更順序
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 1, 1, 3, 4 };
            int difference = 1;

            Console.WriteLine(LongestSubsequence2(input, difference));
            Console.ReadKey();

        }

        /// <summary>
        /// 官方
        /// https://leetcode.cn/problems/longest-arithmetic-subsequence-of-given-difference/solution/zui-chang-ding-chai-zi-xu-lie-by-leetcod-xkua/
        /// 
        /// https://leetcode.cn/problems/longest-arithmetic-subsequence-of-given-difference/solution/1218-zui-chang-ding-chai-zi-xu-lie-by-st-zb7g/
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="difference"></param>
        /// <returns></returns>
        public static int LongestSubsequence(int[] arr, int difference)
        {
            int ans = 0;
            Dictionary<int, int> dic = new Dictionary<int, int>();

            foreach (int i in arr) 
            {
                int prev = dic.ContainsKey(i - difference) ? dic[i - difference] : 0;

                if(dic.ContainsKey(i))
                {
                    dic[i] = prev + 1;
                }
                else
                {
                    dic.Add(i, prev + 1);
                }

                ans = Math.Max(ans, dic[i]);
            }

            return ans;
        }

        /// <summary>
        /// 跟官方寫法差不多邏輯
        /// 但是第一次看 可能比較好理解
        /// https://leetcode.cn/problems/longest-arithmetic-subsequence-of-given-difference/solution/1218-zui-chang-ding-chai-zi-xu-lie-by-st-zb7g/
        /// 
        /// 当 i=0 时，以下标 i 结尾的子序列只有一个，长度是 1，公差可以是任意值，因此动态规划的边界
        /// 情况是 dp[0]=1。
        /// 
        /// 当 i>0 时，只有当下标 i 的前面存在元素 arr[i]−difference 时
        /// ，才可能有 dp[i]>1，否则 dp[i]=1
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="difference"></param>
        /// <returns></returns>
        public static int LongestSubsequence2(int[] arr, int difference)
        {
            int longest = 0;
            Dictionary<int, int> maxLengths = new Dictionary<int, int>();
            int n = arr.Length;

            for (int i = 0; i < n; i++)
            {
                int num = arr[i];
                int prev = num - difference;
                // 在哈希表中寻找以 arr[i] - difference结尾的公差为 difference 的最长子序列长度
                // 该长度加 1 之后的结果即为 dp[i]
                // 如果 存在 maxLengths.ContainsKey(prev) 代表 前後順序等差 長度累加,否則就是從頭開始 也就是1
                // 利用 prev 計算出 i的前一個 等差後 應該是多少 arr[j]才對. 存在就代表是預計中合理等差
                int currLength = (maxLengths.ContainsKey(prev) ? maxLengths[prev] : 0) + 1;

                if (maxLengths.ContainsKey(num))
                {
                    maxLengths[num] = currLength;
                }
                else
                {
                    maxLengths.Add(num, currLength);
                }

                longest = Math.Max(longest, currLength);
            }

            return longest;

        }


    }
}
