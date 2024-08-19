namespace leetcode_650
{
    internal class Program
    {
        /// <summary>
        /// 650. 2 Keys Keyboard
        /// https://leetcode.com/problems/2-keys-keyboard/description/?envType=daily-question&envId=2024-08-19
        /// 
        /// 650. 两个键的键盘
        /// https://leetcode.cn/problems/2-keys-keyboard/description/
        /// 
        /// 本題目, 比較屬於 數學題目
        /// 需要推理公式
        /// 看懂才比較好解題
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 3;

            Console.WriteLine(MinSteps(n));
            Console.ReadKey();
        }


        /// <summary>
        /// 數學題目, 需要看 ref 推理 比較好懂
        /// ref:
        /// https://leetcode.cn/problems/2-keys-keyboard/solutions/1004937/zhi-you-liang-ge-jian-de-jian-pan-by-lee-ussa/
        /// https://leetcode.cn/problems/2-keys-keyboard/solutions/1005636/gong-shui-san-xie-yi-ti-san-jie-dong-tai-f035/
        /// https://leetcode.cn/problems/2-keys-keyboard/solutions/2637752/650-liang-ge-jian-de-jian-pan-by-stormsu-k02p/
        /// 
        /// 
        /// 初始化 就有一個 A 了
        /// 所以 dp[1] = 0
        /// 從 dp[2] 開始才需要增加次數
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int MinSteps(int n)
        {
            int[] f = new int[n + 1];

            // 第二個 A 開始
            for(int i = 2; i <= n; i++)
            {
                f[i] = int.MaxValue;

                // 1 ~ 根號 i 範圍內找答案
                for(int j = 1; j * j <= i; j++)
                {
                    if(i % j == 0)
                    {
                        // j 可以整除 i, j 是 i  的因數
                        // 枚舉 j 進行狀態轉移
                        f[i] = Math.Min(f[i], f[j] + i / j);
                        //
                        f[i] = Math.Min(f[i], f[i / j] + j);
                    }
                }
            }

            return f[n];
        }
    }
}
