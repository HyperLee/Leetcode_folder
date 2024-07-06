namespace leetcode_2582
{
    internal class Program
    {
        /// <summary>
        /// 2582. Pass the Pillow
        /// https://leetcode.com/problems/pass-the-pillow/description/?envType=daily-question&envId=2024-07-06
        /// 2582. 递枕头
        /// https://leetcode.cn/problems/pass-the-pillow/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 4;
            int time = 5;

            Console.WriteLine(PassThePillow(n, time));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/pass-the-pillow/solutions/2451117/di-zhen-tou-by-leetcode-solution-kl5e/
        /// https://leetcode.cn/problems/pass-the-pillow/solutions/2148332/o1-gong-shi-by-endlesscheng-z4xz/
        /// https://leetcode.cn/problems/pass-the-pillow/solutions/2606914/2582-di-zhen-tou-by-stormsunshine-t5fl/
        /// 
        /// 純數學推理問題
        /// 解法公式 推理
        ///  看ref 比較好懂
        /// </summary>
        /// <param name="n"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int PassThePillow(int n, int time)
        {
            // 傳遞方式: 頭 -> 尾 -> 頭
            // 來回一圈所需時間是 2 * n
            // 但是編號是從1開始, 所以要修改為
            // (n - 1) * 2
            time %= (n - 1) * 2;

            var temp0 = time;
            var temp1 = time + 1;
            var temp2 = n - (time - (n - 1));

            return time < n ? time + 1 : n - (time - (n - 1));
        }
    }
}
