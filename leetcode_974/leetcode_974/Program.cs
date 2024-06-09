using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_974
{
    internal class Program
    {
        /// <summary>
        /// 974. Subarray Sums Divisible by K
        /// https://leetcode.com/problems/subarray-sums-divisible-by-k/description/?envType=daily-question&envId=2024-06-09
        /// 974. 和可被 K 整除的子数组
        /// https://leetcode.cn/problems/subarray-sums-divisible-by-k/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] num = { 4, 5, 0, -2, -3, 1 };
            int k = 5;

            Console.WriteLine(SubarraysDivByK(num, k));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:前綴和, 陣列
        /// https://leetcode.cn/problems/subarray-sums-divisible-by-k/solution/by-stormsunshine-37mq/
        /// https://leetcode.cn/problems/subarray-sums-divisible-by-k/solutions/263141/you-jian-qian-zhui-he-na-jiu-zai-ci-dai-ni-da-tong/
        /// https://leetcode.cn/problems/subarray-sums-divisible-by-k/solutions/187947/he-ke-bei-k-zheng-chu-de-zi-shu-zu-by-leetcode-sol/
        /// 
        /// 題目求 子序列總和 可以被k整除
        /// sum of sunarray / k = 0
        /// 
        /// 空的前綴和, 不包含任何元素, 此時和為0 以及 mod餘數也為0
        /// 所以初始化時候 先將餘數0 對應累計次數為1 先存入 hash table
        /// 
        /// sum: 前綴和 mod k 的餘數, 初始化為0
        /// k >= 2
        /// 餘數範圍[0, k - 2]
        /// 長度k數組代替hash table
        /// 
        /// 關注 mod數值與次數
        /// 
        /// hash
        /// key:前綴和 mod k => 餘數
        /// value: 出現次數
        /// 
        /// 前綴和 mod k : 0 ~ k - 1
        /// 剛好和索引對應, 用這去儲存
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int SubarraysDivByK(int[] nums, int k)
        {
            // 總共有幾組答案
            int res = 0;
            // hash table, mod k 餘數 出現次數
            int[] counts = new int[k];
            // 0 初始化為1
            counts[0] = 1;
            int sum = 0;
            int length = nums.Length;
            
            for (int i = 0; i < length; i++)
            {
                //前綴和; 遍歷 nums取餘數
                sum = (sum + nums[i]) % k;
                if (sum < 0)
                {
                    // 確保 0 <= sum < k
                    sum += k;
                }

                // 把对应的次数累加给count
                int count = counts[sum];
                // 索引对应模的结果，值对应出现次数
                res += count;
                // 并且更新出现次数，次数+1
                counts[sum]++;
            }
            return res;
        }

    }
}
