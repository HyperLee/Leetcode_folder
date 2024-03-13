namespace leetcode_2485
{
    internal class Program
    {
        /// <summary>
        /// 2485. Find the Pivot Integer
        /// https://leetcode.com/problems/find-the-pivot-integer/description/?envType=daily-question&envId=2024-03-13
        /// 2485. 找出中枢整数
        /// https://leetcode.cn/problems/find-the-pivot-integer/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 8;
            Console.WriteLine(PivotInteger(n));
            Console.ReadKey();
        }



        /// <summary>
        /// 題目意思是
        /// 前x個總和 要與 x之後到n 之間的總和 要相等
        /// 前x 與 後 n - x 個 總和相同
        /// 
        /// 本方法採用
        /// 利用雙指標來計算
        /// 找出 x 在哪裡
        /// 
        /// n範圍: [1, n]
        /// 
        /// 有其他解法 利用等差公式 數學算法
        /// https://leetcode.cn/problems/find-the-pivot-integer/solutions/2306030/zhao-chu-zhong-shu-zheng-shu-by-leetcode-t7gn/
        /// https://leetcode.cn/problems/find-the-pivot-integer/solutions/1993442/o1-zuo-fa-by-endlesscheng-571j/
        /// https://leetcode.cn/problems/find-the-pivot-integer/solutions/2602536/2485-zhao-chu-zhong-shu-zheng-shu-by-sto-7aja/
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int PivotInteger(int n)
        {
            // 找不到回傳 -1, 題目預設
            int pivot = -1;

            for (int x = 1; x <= n && pivot < 0; x++)
            {
                int leftsum = 0;
                int rightsum = 0;

                // 前 x 個總和
                for(int i = 1; i <= x; i++)
                {
                    leftsum += i;
                }
                // 後 n - x 個總和
                for(int i = x; i <= n; i++)
                {
                    rightsum += i;
                }
                // 相同 即是題目所求
                if(leftsum == rightsum)
                {
                    pivot = x;
                }
            }

            return pivot;
        }
    }
}
