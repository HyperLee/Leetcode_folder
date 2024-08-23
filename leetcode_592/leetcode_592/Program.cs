using System.Diagnostics.CodeAnalysis;

namespace leetcode_592
{
    internal class Program
    {
        /// <summary>
        /// 592. Fraction Addition and Subtraction
        /// https://leetcode.com/problems/fraction-addition-and-subtraction/description/?envType=daily-question&envId=2024-08-23
        /// 
        /// 592. 分数加减运算
        /// https://leetcode.cn/problems/fraction-addition-and-subtraction/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "1/3-1/2";

            Console.WriteLine(FractionAddition(input));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/fraction-addition-and-subtraction/solutions/1699131/fen-shu-jia-jian-yun-suan-by-leetcode-so-2mto/
        /// https://leetcode.cn/problems/fraction-addition-and-subtraction/solutions/1701526/by-ac_oier-rmpy/
        /// https://leetcode.cn/problems/fraction-addition-and-subtraction/solutions/1818029/by-stormsunshine-pdbi/
        /// 
        /// 
        /// 基本知識
        /// 兩個分數
        /// x1 / y1 與 x2 / y2 相加結果為 x1 * y2 + x2 * y1 / y1 * y2
        /// 
        /// 初始化 x = 0, y = 1
        /// 從輸入端讀取每一個 分子為 x1, 分母為 y1
        /// 利用上述公式 計算 每一個讀取進來的 分數
        /// x = x * y1 + x1 * y
        /// y = y * y1
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string FractionAddition(string expression)
        {
            // 分子分母, 分母不為 0
            long x = 0, y = 1;
            int index = 0, n = expression.Length;

            while (index < n) 
            {
                // 讀取分子
                long x1 = 0, sign = 1;
                if (expression[index] == '-' || expression[index] == '+')
                {
                    // 先讀取判斷 正負
                    // '-' = -1, 否則 1
                    sign = expression[index] == '-' ? -1 : 1;
                    index++;
                }

                while(index < n && char.IsDigit(expression[index]))
                {
                    // *10 是考慮分子為兩位數（如10）要讀取
                    // expression[index] - '0' 實際就是减去‘0’的 ASCII 碼值 48 ，是一个整数，即將字符数字類型串轉化為整數數字類型。
                    x1 = x1 * 10 + expression[index] - '0';
                    index++;
                }

                x1 = sign * x1;
                index++;

                // 讀取分母
                long y1 = 0;
                while(index < n && char.IsDigit(expression[index]))
                {
                    // *10 是考慮分子為兩位數（如10）要讀取
                    y1 = y1 * 10 + expression[index] - '0';
                    index++;
                }

                // 兩個分數相乘
                x = x * y1 + x1 * y;
                y *= y1;

            }

            if(x == 0)
            {
                // 若分子為 0, 回傳 0/1
                return "0/1";
            }

            // 獲取最大公約數
            long g = GCD(Math.Abs(x), y);

            return (x / g).ToString() + "/" + (y / g).ToString();
        }


        /// <summary>
        /// 最大公約數 GCD 迭代实现
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long GCD(long a, long b)
        {
            while(b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }


        /// <summary>
        /// 最大公約數 GCD 遞迴
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long GCD2(long a, long b)
        {
            if(b == 0)
            {
                return a;
            }

            return GCD2(b, a % b);
        }

    }
}
