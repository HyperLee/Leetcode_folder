using System.Text;

namespace leetcode_402
{
    internal class Program
    {
        /// <summary>
        /// 402. Remove K Digits
        /// https://leetcode.com/problems/remove-k-digits/description/?envType=daily-question&envId=2024-04-11
        /// 402. 移掉 K 位数字
        /// https://leetcode.cn/problems/remove-k-digits/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "001";
            int k = 1;
            Console.WriteLine(RemoveKdigits(input, k));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/remove-k-digits/solutions/1456933/by-stormsunshine-4s4a/
        /// https://leetcode.cn/problems/remove-k-digits/solutions/290203/yi-zhao-chi-bian-li-kou-si-dao-ti-ma-ma-zai-ye-b-5/
        /// 
        /// 要使得輸出結果值最小,那就要移除比較大的element
        /// 開頭越大代表總和越大.
        /// 所以高位要移除element大的
        /// 
        /// 1. num[i] > num[i + 1] 要把 nums[i] 移除
        /// 2. 開頭不能為 0
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static string RemoveKdigits(string num, int k)
        {
            int length = num.Length;
            StringBuilder sb = new StringBuilder();
            int top = -1;

            for(int i = 0; i < length; i++)
            {
                char c = num[i];

                // 前面位置數字不能比後面大
                while(sb.Length > 0 && sb[top] > c && k > 0)
                {
                    // 如果 前面比後面大, 那前面會被移除
                    sb.Length = top;
                    top--;
                    k--;
                }
                sb.Append(c);
                top++;
            }

            // 計算 輸出答案 要擷取位置(長度)
            while(k > 0)
            {
                sb.Length = top;
                top--; 
                k--;
            }

            int remainlength = sb.Length;
            int startindex = 0;

            // 開頭不能有0; ex: 0200 => 200
            while(startindex < remainlength && sb[startindex] == '0')
            {
                startindex++;
            }

            return startindex == remainlength ? "0" : sb.ToString().Substring(startindex);
        }
    }
}
