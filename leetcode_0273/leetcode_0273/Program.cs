using System.Text;

namespace leetcode_0273
{
    internal class Program
    {
        /// <summary>
        /// 273. Integer to English Words
        /// https://leetcode.com/problems/integer-to-english-words/description/?envType=daily-question&envId=2024-08-07
        /// 
        /// 273. 整数转换英文表示
        /// https://leetcode.cn/problems/integer-to-english-words/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int num = 1234;

            Console.WriteLine(NumberToWords(num));
            Console.ReadKey();
        }

        static string[] singles = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        static string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        static string[] tens = { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        static string[] thousands = { "", "Thousand", "Million", "Billion" };

        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/integer-to-english-words/solutions/1040791/zheng-shu-zhuan-huan-ying-wen-biao-shi-b-ivik/
        /// https://leetcode.cn/problems/integer-to-english-words/solutions/1040978/gong-shui-san-xie-zi-fu-chuan-da-mo-ni-b-0my6/
        /// https://leetcode.cn/problems/integer-to-english-words/solutions/1844462/by-stormsunshine-6a2y/
        /// 
        /// 
        /// 非負整數 2^31 - 1 最多 10 位數
        /// 以三位數(想像成計算機顯示方式, 3個位數一個逗號) 唯一單位 來處理 數字問題
        /// 所以 i 從 3 開始
        /// 剛好夠用處理 10 位數
        /// 輸入的 num 從高位數(左邊), 開始轉換英文
        /// 由左至右 處理
        /// 3 位數單位 來慢慢拼接出 英文
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string NumberToWords(int num)
        {
            if(num == 0)
            {
                return "Zero";
            }
            
            StringBuilder sb = new StringBuilder();
            // 3位數唯一組單位, 所以每次都要 unit /= 1000
            // 輸入的 num 從高位數 開始 轉換
            for (int i = 3, unit = 1000000000; i >= 0; i--, unit /= 1000)
            {
                // 計算該輪數值
                int currnum = num / unit;
                if(currnum != 0)
                {
                    // 剩餘總數 要扣除 該輪數值
                    num -= currnum * unit;
                    StringBuilder curr = new StringBuilder();
                    Recursion(curr, currnum);
                    // 依據位數 拼接 thousands
                    curr.Append(thousands[i]).Append(" ");
                    sb.Append(curr);
                }
            }

            return sb.ToString().Trim();
        }


        /// <summary>
        /// 3 位數唯一個單位 去處理 數字轉英文
        /// 
        /// 文字之間有空白
        /// 
        /// if 區分條件 對應  英文 顯示(上方宣告)
        /// 
        /// 1. num < 20 直接顯示英文單字即可
        /// 2. 20 <= num < 100, 先處理十位數轉換,再來將 個位數 遞迴 處理
        /// 3. num >= 100, 先將百位數轉換,再來將 十位數, 個位數 遞迴 處理
        /// 3.1 如果百位不是 0, 字尾需要拼接上 Hundred
        /// </summary>
        /// <param name="curr"></param>
        /// <param name="num"></param>
        public static void Recursion(StringBuilder curr, int num)
        {
            if(num == 0)
            {
                return;
            }
            else if(num < 10)
            {
                curr.Append(singles[num]).Append(" ");
            }
            else if(num < 20)
            {
                // - 10 用意在於取出陣列中第幾個 index value
                // ex: 18: 18 - 10 = 8
                curr.Append(teens[num - 10]).Append(" ");
            }
            else if(num < 100)
            {
                // 先取 十位數 出來
                curr.Append(tens[num / 10]).Append(" ");
                // 再去遞迴處理 個位數
                Recursion(curr, num % 10);
            }
            else
            {
                // 先取 百位數
                // 百位數轉換是取 個位數 的英文單詞接上 Hundred
                curr.Append(singles[num / 100]).Append(" Hundred ");
                // 其餘 十位數與個位數, 遞迴處理
                Recursion(curr, num % 100);
            }
        }
    }
}
