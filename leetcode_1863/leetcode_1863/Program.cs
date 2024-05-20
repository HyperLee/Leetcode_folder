namespace leetcode_1863
{
    internal class Program
    {
        /// <summary>
        /// 1863. Sum of All Subset XOR Totals
        /// https://leetcode.com/problems/sum-of-all-subset-xor-totals/description/?envType=daily-question&envId=2024-05-20
        /// 1863. 找出所有子集的异或总和再求和
        /// https://leetcode.cn/problems/sum-of-all-subset-xor-totals/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 3 };
            Console.WriteLine(SubsetXORSum(input));
            Console.ReadKey();
        }


        /// <summary>
        /// ref: 方法二
        /// https://leetcode.cn/problems/sum-of-all-subset-xor-totals/solutions/784306/sum-of-all-subset-xor-totals-by-leetcode-o5aa/
        /// https://leetcode.cn/problems/sum-of-all-subset-xor-totals/solutions/2773282/1863-zhao-chu-suo-you-zi-ji-de-yi-huo-zo-omtk/
        /// https://zh.wikipedia.org/zh-tw/%E9%80%BB%E8%BE%91%E5%BC%82%E6%88%96
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int SubsetXORSum(int[] nums)
        {
            int sum = 0;
            int n = nums.Length;
            int total = 1 << n;

            // 遍歷所有子集合
            for (int mask = 0; mask < total; mask++)
            {
                int value = 0;
                //遍歷每個元素
                for(int i = 0; i < n; i++)
                {
                    if ((mask & (1 << i)) != 0)
                    {
                        value ^= nums[i];
                    }
                }
                sum += value;
            }
            return sum;
        }
    }
}
