using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_067
{
    internal class Program
    {
        /// <summary>
        /// 67. Add Binary
        /// https://leetcode.com/problems/add-binary/description/
        /// 
        /// 67. 二进制求和
        /// https://leetcode.cn/problems/add-binary/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string a = "11";
            string b = "01";

            Console.WriteLine(AddBinary(a, b));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/add-binary/solution/er-jin-zhi-qiu-he-by-yicheng2020/
        /// 從兩個 string 的尾端開始往前計算
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string AddBinary(string a, string b)
        {
            // 宣告長度為 a.Length + 1 (例如："11" + "1") , 避免溢位/進位問題
            List<char> result = new List<char>(a.Length + 1);

            // carry 紀錄相加之後是否需要進位
            int carry = 0;

            for (int i = a.Length - 1, j = b.Length - 1; i >= 0 || j >= 0; i--, j--)
            {
                // i--, j-- 後往前  右往左
                int aElement = i >= 0 ? int.Parse(a[i].ToString()) : 0;
                int bElement = j >= 0 ? int.Parse(b[j].ToString()) : 0;
                int tempResult = carry + aElement + bElement;

                // 計算進位，將當前答案更新至 result
                carry = GetCarryAndUpdateResult(result, tempResult);
            }

            // 如果最後運算有進位, 需要再多進位一次
            if (carry == 1)
            {
                result.Add('1');
            }

            // 因當初是反向計算, 所以答案輸出要反轉
            result.Reverse();
            return new string(result.ToArray<char>());
        }

        /// <summary>
        /// 副程式
        /// 管理 是否進位
        /// 以及相加之後
        /// 數值是多少
        /// 
        /// case 0: 答案 0, 不需進位
        /// case 1: 答案 1, 不需進位
        /// case 2: 答案 0 (1 + 1 要進位), 進位
        /// case 3: 答案 1 (1 + 1 + 1), 進位
        /// 
        /// 二進位相加最多就上述幾種 case
        /// 
        /// </summary>
        /// <param name="result">儲存答案</param>
        /// <param name="tempResult">加總後數值</param>
        /// <returns></returns>
        private static int GetCarryAndUpdateResult(List<char> result, int tempResult)
        {
            // carry为进位
            int carry = 0;

            switch(tempResult)
            {
                case 0:
                    carry = 0;
                    result.Add('0');
                    break;
                case 1:
                    carry = 0;
                    result.Add('1');
                    break;
                case 2:
                    carry = 1;
                    result.Add('0');
                    break;
                case 3:
                    carry = 1;
                    result.Add('1');
                    break;
            }

            return carry;
        }

    }
}
