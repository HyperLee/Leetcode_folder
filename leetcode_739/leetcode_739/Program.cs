using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace leetcode_739
{
    internal class Program
    {
        /// <summary>
        /// 739. Daily Temperatures
        /// https://leetcode.com/problems/daily-temperatures/description/?envType=daily-question&envId=2024-01-31
        /// 739. 每日温度
        /// https://leetcode.cn/problems/daily-temperatures/description/
        /// 
        /// 簡單說題目
        /// 輸入一個陣列, 找出 i 位置後面第幾個比i大
        /// 回傳 該位置 or 距離 (距離i 多少位置)
        /// 要是都沒比他大 救回傳 0
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 73, 74, 75, 71, 69, 72, 76, 73 };
            //Console.WriteLine(DailyTemperatures(input));
            var answer = DailyTemperatures2(input);
            foreach (int i in answer) 
            {
                Console.Write(i + ", ");
            }

            Console.ReadKey();

        }


        /// <summary>
        /// 方法1
        /// 這方法會 超時
        /// 單純用雙迴圈判斷 硬跑判斷前後數值大小
        /// 
        /// </summary>
        /// <param name="temperatures"></param>
        /// <returns></returns>
        public static int[] DailyTemperatures(int[] temperatures)
        {
            int n = temperatures.Length;
            List<int> result = new List<int>();

            // 雙迴圈 i 位置往後找出誰比他大
            for(int i = 0; i < n; i++)
            {
                for(int j = i + 1; j < n; j++)
                {
                    int count = 0;

                    if (temperatures[j] > temperatures[i])
                    {
                        result.Add(j - i);
                        count++;
                        break;
                    }

                    if(j == n - 1 && count == 0)
                    {
                        // 找到最後都沒比他大就塞 0
                        result.Add(0);
                    }
                    
                }
            }

            // 陣列最後一個位置(n), 後面沒有人(不會出現 n + 1 位置)能比他大, 所以給 0
            result.Add(0);

            return result.ToArray();

        }



        /// <summary>
        /// 方法2
        /// https://leetcode.cn/problems/daily-temperatures/solutions/11967/jie-ti-si-lu-by-pulsaryu/
        /// 跟上面相比 是從右往左找
        /// 然後用差距(每人差距位置) 值來跳躍
        /// 不再是每一個都下去找
        /// 省下時間
        /// 
        /// 
        /// 很不错的思路，看到了动态规划的影子？ 
        /// 1.自上而下的递归思路：当前位置，询问下一个位置，你比我大，差值
        /// 是1，你比我小，比你大的位置是谁，我去比较。这种思路很明显会出现重复子问题； 
        /// 
        /// 2.自下而上的DP思路：最
        /// 后一个位置，知道自己是0，接着倒数第二个，依次类推
        /// 
        /// 
        /// 還有一個方法3
        /// 這可能比較是題目當初想考的方法原理 利用 stack
        /// https://leetcode.cn/problems/daily-temperatures/solutions/1459174/by-stormsunshine-vvw0/
        /// 想看可以參考這邊
        /// 
        /// 不過方法2太優質了
        /// 
        /// </summary>
        /// <param name="temperatures"></param>
        /// <returns></returns>
        public static int[] DailyTemperatures2(int[] temperatures)
        {
            int n = temperatures.Length;
            int[] result = new int[n];

            // 反方向 => 右往左方向找
            // i = n - 2 => 陣列從0開始所以扣1, 
            // 再來從倒數第二個開始找在扣1, 因為 後面還有一個迴圈 j 他是 i + 1 位置 開始計算
            for (int i = n - 2; i >= 0; i--)
            {
                // j += result[j] 利用已知結果來跳躍, 如果當前是1就找下一個,
                // 如果當前是2 那就直接跳2 畢竟這中間差值都2沒必要每一個都去比對
                for (int j = i + 1; j < n; j += result[j])
                {
                    if (temperatures[j] > temperatures[i])
                    {
                        result[i] = j - i;
                        break;
                    }
                    else if (result[j] == 0)
                    {
                        // 遇到 0 表示後面不會有更大值, 那當然目前也是給0
                        // 一開始都會是0, temperatures陣列最尾端,後面沒人(不會出現 n + 1 位置)肯定給 0
                        result[i] = 0;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
