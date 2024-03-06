namespace leetcode_2917
{
    internal class Program
    {
        /// <summary>
        /// 2917. Find the K-or of an Array
        /// https://leetcode.com/problems/find-the-k-or-of-an-array/description/
        /// 2917. 找出数组中的 K-or 值
        /// https://leetcode.cn/problems/find-the-k-or-of-an-array/description/?envType=daily-question&envId=Invalid%20Date
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 7, 12, 9, 8, 9, 15 };
            int k = 4;

            Console.WriteLine(FindKOr(input, k));
            Console.ReadKey();

            //Console.WriteLine("Hello, World!");
        }


        /// <summary>
        /// 建議看影片說明, 比較好理解 題目意思
        /// 位運算題型
        /// https://leetcode.cn/problems/find-the-k-or-of-an-array/solutions/2503182/an-ti-yi-mo-ni-by-endlesscheng-34t1/?envType=daily-question&envId=Invalid%20Date
        /// 文件參考
        /// https://leetcode.cn/circle/discuss/CaOJ45/
        /// 属于     (s >> i) & 1=1
        /// 添加元素 s ∣ (1 << i)
        /// 
        /// 其中 << 表示左移，>> 表示右移。
        /// 注：左移 i 位相当于乘 2^i，右移 i 位相当于除 2^i。
        /// 
        /// 
        /// 官方
        /// https://leetcode.cn/problems/find-the-k-or-of-an-array/solutions/2666472/zhao-chu-shu-zu-zhong-de-k-or-zhi-by-lee-kxjc/?envType=daily-question&envId=Invalid%20Date
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int FindKOr(int[] nums, int k)
        {
            int ans = 0;

            // 題目說明小於 2^31次方; 故迴圈要小於31
            for(int i = 0; i < 31; i++)
            {
                int cnt = 0;

                // 要用二進位去看每一個bit值
                foreach(int x in nums) 
                {
                    // 屬於
                    // 判斷 x 的每個 bit 位數是 0 還是 1
                    // 都累加到 cnt1 
                    cnt += x >> i & 1;
                }

                if(cnt >= k)
                {
                    // 添加元素
                    ans |= 1 << i;
                }
            }

            return ans;
        }

    }
}
