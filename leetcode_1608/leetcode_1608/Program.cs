namespace leetcode_1608
{
    internal class Program
    {
        /// <summary>
        /// 1608. Special Array With X Elements Greater Than or Equal X
        /// https://leetcode.com/problems/special-array-with-x-elements-greater-than-or-equal-x/description/?envType=daily-question&envId=2024-05-27
        /// 1608. 特殊数组的特征值
        /// https://leetcode.cn/problems/special-array-with-x-elements-greater-than-or-equal-x/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 3, 5 };
            Console.WriteLine(SpecialArray(input));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/special-array-with-x-elements-greater-than-or-equal-x/solutions/1816575/te-shu-shu-zu-de-te-zheng-zhi-by-leetcod-9wfo/
        /// https://leetcode.cn/problems/special-array-with-x-elements-greater-than-or-equal-x/solutions/1817958/by-ac_oier-z525/
        /// https://leetcode.cn/problems/special-array-with-x-elements-greater-than-or-equal-x/solutions/2747855/1608-te-shu-shu-zu-de-te-zheng-zhi-by-st-t1rq/
        /// 
        /// 這是funtion重點
        /// 若 i 为特征值，那么 nums中恰好有 i个元素大于等于 i。由于数组已经降序排序，说明 nums[i−1]
        /// 必须大于等于 i，并且 nums[i]（如果存在）必须小于 i。
        /// 
        /// 需要多加思考幾次 才能想清楚
        /// 
        /// 因為是desc排序
        /// 所以找到一個 特偵 i
        /// 代表 前面幾個 都比 i  還要大
        /// 符合題目要求
        /// 再來要判斷 i == n
        /// 這條件
        /// 才符合題目完整要求
        /// 
        /// 目前還不是很理解 nums[i] < i 用意
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int SpecialArray(int[] nums)
        {
            // 排序 小至大 asc
            Array.Sort(nums);
            int n = nums.Length;

            // array 反轉, 大至小 desc
            for(int i = 0, j = n - 1; i < j; i++, j--)
            {
                int temp = nums[i];
                nums[i] = nums[j];
                nums[j] = temp;
            }

            // range [1, n]
            for(int i = 1; i <= n; i++)
            {
                // nums 中恰好有 x 个元素 大于或者等于 x
                if (nums[i - 1] >= i && (i == n || nums[i] < i))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
