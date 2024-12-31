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

            Console.WriteLine("res: " + AddBinary(a, b));
        }


        /// <summary>
        /// https://leetcode.cn/problems/add-binary/solution/er-jin-zhi-qiu-he-by-yicheng2020/
        /// 從兩個 string 的尾端開始往前計算
        /// 二進位從低位開始往高位做計算 <右邊往左邊計算>
        /// 低位遇到進位問題,要給高位來進位
        /// 
        /// result.ToArray()
        /// ToArray() 方法的作用是將集合（如 List<char> 或其他支持 IEnumerable 的集合）轉換為一個字符陣列（char[]）。
        /// result.ToArray() 會將 List<char> 轉換為 char[] 陣列
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string AddBinary(string a, string b)
        {
            List<char> result = new List<char>();

            // carry 紀錄相加之後是否需要進位
            int carry = 0;

            // i--, j-- 後往前  右往左 低位往高位
            for (int i = a.Length - 1, j = b.Length - 1; i >= 0 || j >= 0; i--, j--)
            {
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

            // 因當初是反向計算(右邊開始先計算, 由右至左加入答案), 所以答案輸出要反轉
            result.Reverse();

            return new string(result.ToArray());
        }


        /// <summary>
        /// 副程式
        /// 管理 是否進位
        /// 以及相加之後
        /// 數值是多少
        /// 
        /// *** 
        /// tempResult 加總後數值, 可區分下列幾種 case
        /// case 0: 答案 0, 不需進位
        /// case 1: 答案 1, 不需進位
        /// case 2: 答案 0 (1 + 1 要進位), 進位
        /// case 3: 答案 1 (1 + 1 + 1), 進位
        /// 
        /// 二進位相加最多就上述幾種 case
        /// 
        /// 二進位從低位開始往高位做計算 <右邊往左邊計算>
        /// 低位遇到進位問題,要給高位來進位
        /// </summary>
        /// <param name="result">儲存答案</param>
        /// <param name="tempResult">加總後數值</param>
        /// <returns></returns>
        private static int GetCarryAndUpdateResult(List<char> result, int tempResult)
        {
            // 進位
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
