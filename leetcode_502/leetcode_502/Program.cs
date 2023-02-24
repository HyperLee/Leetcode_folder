using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace leetcode_502
{
    internal class Program
    {
        /// <summary>
        /// leetcode 502 IPO
        /// https://leetcode.com/problems/ipo/
        /// 中文題目
        /// https://leetcode.cn/problems/ipo/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int k = 2, w = 0;
            int[] profits = new int[] {1, 2, 3 };
            int[] captial = new int[] { 0, 1, 1 };

            Console.WriteLine(FindMaximizedCapital(k, w, profits, captial));
            Console.ReadKey();
        }



        /// <summary>
        /// https://leetcode.com/problems/ipo/solutions/131379/c-beats-100/?q=c%23&orderBy=most_relevant
        /// 
        /// 有tryadd方法 無法執行
        /// 
        /// https://leetcode.cn/problems/ipo/solution/wo-ye-bu-zhi-dao-shi-yao-suan-fa-dan-shi-xq38/
        /// 目前使用之方法; 無註解與說明
        /// 靠自己中斷 測試出來 得知
        /// </summary>
        /// <param name="k">最多能完成之項目</param>
        /// <param name="w">目前資本</param>
        /// <param name="profits">純利潤</param>
        /// <param name="capital">最小資本(啟動資金)</param>
        /// <returns></returns>
        public static int FindMaximizedCapital(int k, int w, int[] profits, int[] capital)
        {
            // 資本不能比 最小啟動資金還低, 這樣專案一個都不能跑
            // 啟動資本 已經排序 大至小 最小都不能跑 那就可以結案了
            if (w < capital.Min())
            {
                return 0;
            }

            int n = profits.Length;
            Dictionary<int, int> dicP = new Dictionary<int, int>();
            Dictionary<int, int> dicC = new Dictionary<int, int>();
            
            for (int i = 0; i < n; i++)
            {
                dicP.Add(i, profits[i]);
                dicC.Add(i, capital[i]);
            }

            // 排序方式是 逆向 反轉 排序; 目前還不清楚反向 用意
            // 看題目範例 這兩個排序似乎是 小到大, 所以反轉就可以直接從大的開始拿
            dicP = dicP.OrderBy(d => d.Value).Reverse().ToDictionary(d => d.Key, d => d.Value);
            dicC = dicC.OrderBy(d => d.Value).Reverse().ToDictionary(d => d.Key, d => d.Value);

            //項目與利潤 都要 > 0
            while (k > 0 && dicP.Count() > 0)
            {
                // 資本不能比 啟動資金小
                if (w < dicC.Last().Value)
                {
                    break;
                }

                // 資本大於 啟動資本最大那一個, 也就是說 全部都可以選
                // 資本 > 啟動資本
                // 基本上 一開始都是先跑下面那個foreach 加入初始的 item.key => 0 那一個之後
                // 就有基本 value 就會來跑這邊了
                if (w >= dicC.Max(c => c.Value))
                {
                    // 找到最大利潤, 項目就減少一項
                    // 把利潤加入 資本裡面
                    // 利潤排序是大至小, 故會是最大獲利
                    foreach (var item in dicP)
                    {
                        w += item.Value;
                        --k;
                        if (k <= 0)
                        {
                            // 項目不能為空, 為空就跳出
                            break;
                        }
                    }
                    break;
                }


                // 找出 可以使用的 啟動資本
                foreach (var item in dicP)
                {
                    // 一開始 w基本上都是0 所以 item.Key基本上 都要找到最小那一個 才能加入
                    // 除非一開始 題目就給 w > 0 了, item.Key => 0那一個 加入他所蘊含的value;
                    // 找出 資本 >  可使用啟動資金, 就把value加入 然後 從dictionary 剔除
                    if (w >= dicC[item.Key])
                    {
                        w += item.Value;
                        --k;
                        dicC.Remove(item.Key);
                        dicP.Remove(item.Key);
                        break;
                    }

                }
            }

            return w;
        }


    }
}
