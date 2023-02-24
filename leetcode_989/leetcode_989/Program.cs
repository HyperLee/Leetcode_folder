using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_989
{
    internal class Program
    {
        /// <summary>
        /// leetcode 989
        /// https://leetcode.com/problems/add-to-array-form-of-integer/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] num = new int[] { 1, 2, 0, 9 };
            int k = 34;

            //Console.WriteLine(AddToArrayForm(num, k));

            AddToArrayForm(num, k);

            /*
            int[] array = { 5, 6, 2, 4 };
            int finalScore = 0;
            for (int i = 0; i < array.Length; i++)
            {
                finalScore += array[i] * Convert.ToInt32(Math.Pow(10, array.Length - i - 1));
            }
            Console.WriteLine(finalScore);
            */

            Console.ReadKey();


        }


        /// <summary>
        /// https://leetcode.cn/problems/add-to-array-form-of-integer/solution/shu-zu-xing-shi-de-zheng-shu-jia-fa-by-l-jljp/
        /// 
        /// 從個位數開始往十數位,百位數,千位數 相加  (有進位問題個位數開始比較保險)
        /// 結果需要反轉,因從個位數開始做 要反過來才正常
        /// 
        ///    当前位 = 和 % 10;
        ///    进位 = 和 / 10;
        /// </summary>
        /// <param name="num"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static IList<int> AddToArrayForm(int[] num, int k)
        {
            // 初始化参数
            List<int> res = new List<int>();
            int n = num.Length;

            // 1. 从后往前逐位相加
            for (int i = n - 1; i >= 0; --i)
            {
                // 1.1 逐位相加
                int sum = num[i] + k % 10; // 每次重置sum,  每次取k最右邊的位數
                k /= 10; // 取玩最右邊之後 向左一個位數

                // 1.2 处理两位相加 进位的情况; 若加法的结果大于等于 10，把进位的 1 加入到下一位的计算中
                if (sum >= 10)
                {
                    k++; // 进位到K的末尾
                    sum -= 10; // 进位清掉
                }

                // 1.2 当前相加的结果 添加到结果集
                res.Add(sum);
            }

            // 2. K的数字长度大于数组的数字长度
            for (; k > 0; k /= 10) // 每次K左移一位
            {
                res.Add(k % 10); // 添加到结果集
            }

            // 3. 将结果集翻转即是所求答案
            // 因是從個位數開始做,所以答案要反過來 才是正常
            res.Reverse();

            foreach(var value in res)
            {
                Console.Write(value + ", ");
            }

            return res;

        }


        /// <summary>
        /// https://stackoverflow.com/questions/9564800/how-to-convert-int-array-to-int
        /// 原先構想 int[] num 直接轉 int 看來會有問題
        /// 沒辦法透過 api轉換
        /// 看來不能偷吃步 乖乖用 list
        /// 把陣列資料取出來 相加之後 塞回去
        /// 
        /// 此方法經過 int[] 轉 int 之後 數值是對的
        /// 但是錯在 每個位數之間都要有逗號區隔
        /// 還是回頭用 List 下去做比較快
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static IList<int> AddToArrayForm2(int[] num, int k)
        {
            List<int> res = new List<int>();

            int[] array = num;
            int finalScore = 0;
            for (int i = 0; i < array.Length; i++)
            {
                finalScore += array[i] * Convert.ToInt32(Math.Pow(10, array.Length - i - 1));
            }

            finalScore += k;

            res.Add(finalScore);

            foreach (var value in res)
            {
                Console.Write(value + ", ");
            }

            return res;

        }


    }
}
