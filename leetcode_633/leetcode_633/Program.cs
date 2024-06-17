namespace leetcode_633
{
    internal class Program
    {
        /// <summary>
        /// 633. Sum of Square Numbers
        /// https://leetcode.com/problems/sum-of-square-numbers/?envType=daily-question&envId=2024-06-17
        /// 633. 平方数之和
        /// https://leetcode.cn/problems/sum-of-square-numbers/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int c = 5;
            Console.WriteLine(JudgeSquareSum(c));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/sum-of-square-numbers/solutions/747079/ping-fang-shu-zhi-he-by-leetcode-solutio-8ydl/
        /// 雙指針方式找出答案
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool JudgeSquareSum(int c)
        {
            long left = 0;
            long right = (long) Math.Sqrt(c);

            while(left <= right)
            {
                long sum = left * left + right * right;
                if (sum == c)
                {
                    return true;
                }
                else if(sum > c)
                {
                    // 總和太大, 右邊要縮小
                    right--;
                }
                else
                {
                    // 總和太小, 左邊拉大
                    left++;
                }
            }
            return false;
        }
    }
}
