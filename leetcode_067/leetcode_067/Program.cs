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
        /// leetcode 067 二進位相加
        /// https://leetcode.com/problems/add-binary/description/
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
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string AddBinary(string a, string b)
        {
            //宣告一个长度位a.Length+1长度的list
            //因为结果的长度最大就是a.Length+1(例如："11" + "1")
            List<char> result = new List<char>(a.Length + 1);

            //carry为每次位相加运算的进位
            int carry = 0;

            for (int i = a.Length - 1, j = b.Length - 1; i >= 0 || j >= 0; i--, j--)
            {
                // i--, j-- 後往前  右往左
                int aElement = i >= 0 ? int.Parse(a[i].ToString()) : 0;
                int bElement = j >= 0 ? int.Parse(b[j].ToString()) : 0;
                int tempResult = carry + aElement + bElement;

                //计算进位，并将当前位计算结果更新到result中
                carry = GetCarryAndUpdateResult(result, tempResult);
            }
            //如果最后一次运算有进位，需要添加一个"1"
            if (carry == 1)
            {
                result.Add('1');
            }

            //因为list是从尾数倒着添加的，需要反转list元素
            result.Reverse();
            return new string(result.ToArray<char>());
        }

        /// <summary>
        /// 副程式
        /// 管理 是否進位
        /// 以及相加之後
        /// 數值是多少
        /// </summary>
        /// <param name="result"></param>
        /// <param name="tempResult"></param>
        /// <returns></returns>
        private static int GetCarryAndUpdateResult(List<char> result, int tempResult)
        {
            //carry为进位
            int carry = 0;

            switch (tempResult)
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
