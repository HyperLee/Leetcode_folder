namespace leetcode_476
{
    internal class Program
    {
        /// <summary>
        /// 476. Number Complement
        /// https://leetcode.com/problems/number-complement/description/?envType=daily-question&envId=2024-08-22
        /// 
        /// 476. 数字的补数
        /// https://leetcode.cn/problems/number-complement/description/
        /// 
        /// 本題目比較推薦 方法一 解法
        /// 位元運算
        /// 但是需要理解一下
        /// 不然不是很好懂
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int num = 5;
            Console.WriteLine("方法1: " + FindComplement(num));
            //Console.WriteLine("方法2: " + FindComplement2(num));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/number-complement/solutions/1052788/tong-ge-lai-shua-ti-la-jian-dan-gao-xiao-k0p9/
        /// https://leetcode.cn/problems/number-complement/solutions/1050060/shu-zi-de-bu-shu-by-leetcode-solution-xtn8/
        /// https://leetcode.cn/problems/number-complement/solutions/1052783/gong-shui-san-xie-yi-ti-shuang-jie-bian-wjl0y/
        /// https://leetcode.cn/problems/number-complement/solutions/1631259/by-stormsunshine-onze/
        /// 
        /// 
        /// 補數介紹:
        /// 二的補數(two’s complement)就是將位元反置後再加一
        /// https://tim.diary.tw/2009/08/24/twos-complement/
        /// 
        ///     2(d) =   00000010(b)
        ///  1. 反置     11111101
        ///  2. 再加1得  11111110
        ///  3. 所以得   11111110(b) = -2(d)
        ///  
        /// 也可以參考 wiki 說明, 有範例
        /// https://zh.wikipedia.org/zh-tw/%E4%BA%8C%E8%A3%9C%E6%95%B8
        /// 
        /// ^ => xor
        /// 相同為 0
        /// 不同為 1
        /// 
        /// 本解法重點:
        /// 只需要找到最高位的 1, 將最高位的 1 所在位置以及比他低的位置全變成 1, 在做 xor 即可
        /// 
        ///  highbit 左移 1 位在減 1 
        ///  => 就可以得到最高位为 1 的位置及比它低的位置全是 1 的数字了。
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int FindComplement(int num)
        {
            int highbit = 1;
            int x = num;

            // 找出最高位的 1
            while(x != 0)
            {
                // 找最低位(最右邊)的 1
                highbit = x & (-x);
                // 打掉最低位(最右邊)的 1
                x = x & (x - 1);
            }

            // 原始輸入的 num xor ( highbit 左移 1 位在減 1 (反置))
            return num ^ ((highbit << 1) - 1);
        }


        /// <summary>
        /// https://leetcode.cn/problems/number-complement/solutions/1631259/by-stormsunshine-onze/
        /// 這個方法 比較特殊
        /// 
        /// sum: num 與 補數 之和
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int FindComplement2(int num)
        {
            int sum = 1;
            while(sum < num)
            {
                // 二進位 所以每個位數都 * 2
                sum = sum * 2 + 1;
            }

            // 最後再把 sum - num = 補數
            return sum - num;
        }
    }
}
