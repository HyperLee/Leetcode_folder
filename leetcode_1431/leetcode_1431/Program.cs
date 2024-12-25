namespace leetcode_1431
{
    internal class Program
    {
        /// <summary>
        /// 1431. Kids With the Greatest Number of Candies
        /// https://leetcode.com/problems/kids-with-the-greatest-number-of-candies/
        /// 1431. 拥有最多糖果的孩子
        /// https://leetcode.cn/problems/kids-with-the-greatest-number-of-candies/
        /// 
        /// 方法1 與 方法 2 其實大同小異
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] candies = new int[] { 2, 3, 5, 1, 3 };
            int extraCandies = 3;

            KidsWithCandies(candies, extraCandies);
            KidsWithCandies2(candies, extraCandies);
        }


        /// <summary>
        /// 1. 先找出原始輸入資料中有最多糖果的
        /// 2. 第 i 個孩子 + 額外糖果 要 >= 持有最多糖果者(可多個)
        /// </summary>
        /// <param name="candies"></param>
        /// <param name="extraCandies"></param>
        /// <returns></returns>
        public static IList<bool> KidsWithCandies(int[] candies, int extraCandies)
        {
            int n = candies.Length;
            int maxcandies = 0;

            // 找出原始資料中有最多糖果者
            for(int i = 0; i < n; i++)
            {
                maxcandies = Math.Max(maxcandies, candies[i]);
            }

            // 找出 目前持有 + 額外給的 >= 原本最多者
            IList<bool> result = new List<bool>();
            for(int i = 0; i < n; i++)
            {
                result.Add(candies[i] + extraCandies >= maxcandies);
            }

            Console.Write("method1: [");
            foreach(var value in result)
            {
                Console.Write(value + ", ");
            }
            Console.WriteLine("]");

            return result;

        }


        /// <summary>
        /// 1. 先把 目前糖果 + 額外糖果 放到新陣列
        /// 2. 從新陣列找出比原先糖果最多者 >= 的孩子
        /// </summary>
        /// <param name="candies"></param>
        /// <param name="extraCandies"></param>
        /// <returns></returns>
        public static IList<bool> KidsWithCandies2(int[] candies, int extraCandies)
        {
            int n = candies.Length;
            int[] candies2 = new int[n];
            // 1. 先把 目前糖果 + 額外糖果 放到新陣列
            for (int i = 0; i < n; i++)
            {
                candies2[i] = candies[i] + extraCandies;
            }

            // 2. 從新陣列找出比原先糖果最多者 >= 的孩子
            IList<bool> result = new List<bool>();
            for(int i = 0; i < n; i++)
            {
                result.Add(candies2[i] >= candies.Max());
            }

            Console.Write("method2: [");
            foreach (var value in result)
            {
                Console.Write(value + ", ");
            }
            Console.WriteLine("]");

            return result;
        }
    }
}
