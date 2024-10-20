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
        /// 502. IPO
        /// https://leetcode.com/problems/ipo/
        /// 502. IPO
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
        /// 有 tryadd 方法 無法執行
        /// 
        /// https://leetcode.cn/problems/ipo/solution/wo-ye-bu-zhi-dao-shi-yao-suan-fa-dan-shi-xq38/
        /// 目前使用之方法; 無註解與說明
        /// 靠自己中斷 測試出來 得知
        /// 
        /// profits 與 capital 題目初始已由小至大排序
        /// 但是要找出 k 個最大獲利
        /// 所以當然是要反向排序
        /// 由大至小, 高額度才高獲利
        /// </summary>
        /// <param name="k">最多能完成之項目</param>
        /// <param name="w">目前資金</param>
        /// <param name="profits">純利潤</param>
        /// <param name="capital">專案資本額度(啟動資金/低消)</param>
        /// <returns></returns>
        public static int FindMaximizedCapital(int k, int w, int[] profits, int[] capital)
        {
            // 手上當前資金不能比 最小專案資本額還低, 這樣專案一個都不能跑
            // 專案資本 已經排序 小至大 最小都不能跑 那就可以結案了
            if (w < capital.Min())
            {
                return 0;
            }

            int n = profits.Length;
            // Key: index, Value: array value
            Dictionary<int, int> dicP = new Dictionary<int, int>();
            Dictionary<int, int> dicC = new Dictionary<int, int>();
            
            for (int i = 0; i < n; i++)
            {
                dicP.Add(i, profits[i]);
                dicC.Add(i, capital[i]);
            }

            // 排序方式是 逆向 反轉 排序;
            // 看題目範例 這兩個排序似乎是 小到大, 所以反轉就可以直接從大的開始拿
            dicP = dicP.OrderBy(d => d.Value).Reverse().ToDictionary(d => d.Key, d => d.Value);
            dicC = dicC.OrderBy(d => d.Value).Reverse().ToDictionary(d => d.Key, d => d.Value);

            // 當沒有項目或是沒有獲利 就終止條件
            while (k > 0 && dicP.Count() > 0)
            {
                // 手上當前資金不能比最小的 專案資本還要小, 這樣無法啟動專案
                if (w < dicC.Last().Value)
                {
                    break;
                }

                // 手上當前資金 大於 專案資本最大那一個, 也就是說 全部都可以選
                // 手上當前資金 > 專案資本
                // 基本上 一開始都是先跑下面那個 foreach 加入初始的 item.key => 0 那一個之後
                // 就有基本 value 就會來跑這邊了
                if (w >= dicC.Max(c => c.Value))
                {
                    // 找到最大利潤, 項目就減少一項
                    // 把利潤加入 當前資金裡面
                    // 利潤排序是大至小, 故會是最大獲利
                    foreach (var item in dicP)
                    {
                        // 賺到獲利
                        w += item.Value;
                        // 移除項目
                        --k;

                        if (k <= 0)
                        {
                            // 項目不能為空, 為空就跳出
                            break;
                        }
                    }
                    break;
                }


                // 找出 可以使用的 專案資本
                foreach (var item in dicP)
                {
                    // 找出 手上當前資金 >  可使用專案資本, 就把 value 加入 然後 從 dictionary 剔除
                    // 一開始 w 基本上都是 0 所以 item.Key 基本上 都要找到最小那一個 才能加入
                    // 除非一開始 題目就給 w > 0 了, item.Key => 0 那一個 加入他所蘊含的 value;
                    if (w >= dicC[item.Key])
                    {
                        // 賺到獲利
                        w += item.Value;
                        // 移除項目
                        --k;
                        // 移除項目
                        dicC.Remove(item.Key);
                        // 移除項目
                        dicP.Remove(item.Key);

                        break;
                    }

                }
            }

            return w;
        }


    }
}
