namespace leetcode_1051
{
    internal class Program
    {
        /// <summary>
        /// 1051. Height Checker
        /// https://leetcode.com/problems/height-checker/description/?envType=daily-question&envId=2024-06-10
        /// 1051. 高度检查器
        /// https://leetcode.cn/problems/height-checker/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 1, 4, 2, 1, 3 };
            Console.WriteLine(HeightChecker(input));
            Console.ReadKey();

        }



        /// <summary>
        /// https://leetcode.cn/problems/height-checker/solutions/1593917/gao-du-jian-cha-qi-by-leetcode-solution-jeb0/
        /// https://leetcode.cn/problems/height-checker/solutions/10411/onjie-fa-yong-shi-yu-nei-cun-ji-bai-100-javayong-h/
        /// https://leetcode.cn/problems/height-checker/solutions/1597915/by-ac_oier-8xoj/
        /// https://leetcode.cn/problems/height-checker/solutions/1496110/1051-gao-du-jian-cha-qi-by-stormsunshine-c5gd/
        /// 
        /// 先把 heights 複製一組新的
        /// 然後進行排序為 expected
        /// 最後再與原先的 heights
        /// 進行比對 每個下標element 是否相同
        /// 
        /// </summary>
        /// <param name="heights"></param>
        /// <returns></returns>
        public static int HeightChecker(int[] heights)
        {
            int n = heights.Length;
            int res = 0;
            int[] expected = new int[n];
            // 將 heights 複製至 expected
            Array.Copy(heights, 0, expected, 0, n);
            // 遞增排序
            Array.Sort(expected);

            for(int i = 0; i < n; i++)
            {
                if (heights[i] != expected[i])
                {
                    res++;
                }
            }

            return res;
        }
    }
}
