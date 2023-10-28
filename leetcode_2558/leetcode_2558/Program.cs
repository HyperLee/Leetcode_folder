using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2558
{
    internal class Program
    {
        /// <summary>
        /// 2558. Take Gifts From the Richest Pile
        /// https://leetcode.com/problems/take-gifts-from-the-richest-pile/
        /// 2558. 从数量最多的堆取走礼物
        /// https://leetcode.cn/problems/take-gifts-from-the-richest-pile/?envType=daily-question&envId=Invalid%20Date
        /// 
        /// 從gifts[]裡面執行k次
        /// 每次都取出最大的 開平方根
        /// 最後取出總和
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 25, 64, 9, 4, 100 };
            int k = 4;
            Console.WriteLine(PickGifts(input, k));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/take-gifts-from-the-richest-pile/solutions/2477680/cong-shu-liang-zui-duo-de-dui-qu-zou-li-kt246/?envType=daily-question&envId=Invalid+Date
        /// https://leetcode.cn/problems/take-gifts-from-the-richest-pile/solutions/2094024/c-by-yu-gi-oh-3oks/?envType=daily-question&envId=Invalid+Date
        /// 
        /// </summary>
        /// <param name="gifts">禮物堆數量</param>
        /// <param name="k">執行次數</param>
        /// <returns></returns>
        public static long PickGifts(int[] gifts, int k)
        {
            int index = 0;
            long res = 0;

            // 執行 k 次
            for (int i = 0; i < k; i++)
            {
                // 找出 gifts[] 裡面最大數值的位置
                index = Array.IndexOf(gifts, gifts.Max());
                // 題目需求,找出最大 開平方根
                gifts[index] = (int)Math.Sqrt(gifts[index]);
            }

            // 執行k次之後, 將gifts[]裡面數值做總和
            for (int i = 0; i < gifts.Length; i++)
            {
                res += gifts[i];
            }

            return res;
        }


        /// <summary>
        /// https://leetcode.cn/problems/take-gifts-from-the-richest-pile/solutions/2502176/c-shi-yong-zui-jian-dan-de-pai-xu-he-xun-b9ko/?envType=daily-question&envId=Invalid+Date
        /// 這方法很有趣
        /// 都使用API來完成
        /// 1. 先排序 大至小
        /// 2. 取出最大開平方跟
        /// 3. 執行k次之後
        /// 4. gifts[] 取總和
        /// </summary>
        /// <param name="gifts"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public long PickGifts2(int[] gifts, int k)
        {
            for (; 0 < k; k--)
            {
                // 排序 這是小至大
                Array.Sort(gifts);
                // 取相反 就是要 大至小排序
                Array.Reverse(gifts);
                // 開平方跟
                gifts[0] = (int)Math.Floor(Math.Sqrt(gifts[0]));
            }

            // 取總和
            long sum = 0;
            foreach (int gift in gifts)
            {
                sum += gift;
            }
            
            return sum;
        }

    }
}
