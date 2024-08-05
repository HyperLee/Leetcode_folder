using System.Text;

namespace leetcode_2053
{
    internal class Program
    {
        /// <summary>
        /// 2053. Kth Distinct String in an Array
        /// https://leetcode.com/problems/kth-distinct-string-in-an-array/description/?envType=daily-question&envId=2024-08-05
        /// 
        /// 2053. 数组中第 K 个独一无二的字符串
        /// https://leetcode.cn/problems/kth-distinct-string-in-an-array/description/
        /// 
        /// 題目中 獨一無二就是 在 陣列中只出現一次的 char
        /// 回傳第 k 個 獨一無二的 char
        /// 輸入順序不能異動
        /// 要是找不到 第 k 個
        /// 就回傳 空
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] input = { "d", "b", "c", "b", "c", "a" };
            int k = 2;

            Console.WriteLine(KthDistinct(input, k));
            Console.ReadKey();
        }


        /// <summary>
        /// 方法:
        /// 利用 Dictionary 來統計每個 char 出現的次數 以及 輸入順序
        /// 統計結束之後, 用一個迴圈去判斷
        /// count 累計 第幾個 k
        /// 找到之後就加入 sb 裡面
        /// 如果找不到就回傳空的
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static string KthDistinct(string[] arr, int k)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            StringBuilder sb = new StringBuilder();
            
            // 統計輸入 char 與 次數 
            foreach (string s in arr) 
            {
                if(!dic.ContainsKey(s))
                {
                    dic.Add(s, 1);
                }
                else
                {
                    dic[s]++;
                }
            }

            int count = 0;
            foreach(KeyValuePair<string, int> kvp in dic)
            {
                // 只出現一次
                if(kvp.Value == 1 )
                {
                    count++;
                    if(count == k)
                    {
                        // 第 k 個 獨一無二
                        sb.Append(kvp.Key);
                    }
                }
            }

            if(count < k)
            {
                // 題目要求 小於 k
                // 就回傳空 empty string ""
                sb.Length = 0;
            }

            return sb.ToString();
        }
    }
}
