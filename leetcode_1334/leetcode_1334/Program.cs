namespace leetcode_1334
{
    internal class Program
    {
        /// <summary>
        /// 1334. Find the City With the Smallest Number of Neighbors at a Threshold Distance
        /// https://leetcode.com/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/?envType=daily-question&envId=2024-07-26
        /// 
        /// 1334. 阈值距离内邻居最少的城市
        /// https://leetcode.cn/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/description/
        /// 
        /// 關鍵點：
        /// 城市之間的距離：由連接兩個城市的邊的權重總和決定。
        /// 目標城市：找到可達城市數量最少的城市。
        /// 可達城市：距離該城市不超過指定閾值的城市。
        /// 處理重複情況：如果有多個城市符合條件，返回城市編號最大的那個。
        /// 
        /// 解題思路
        /// 1. 建立圖結構：使用鄰接矩陣或鄰接表表示城市之間的連接關係和距離。
        /// 2. 計算城市間距離：使用圖算法（如 Dijkstra、Floyd-Warshall）計算任意兩城市之間的最短距離。
        /// 3. 統計可達城市數量：對於每個城市，計算距離小於等於閾值的城市數量。
        /// 4. 找出最小可達城市：在所有城市中，找到可達城市數量最小的城市，並返回其編號。
        /// 
        /// 算法選擇
        /// 1. Dijkstra算法：適用於單源最短路徑問題，可以高效地計算一個城市到其他所有城市的距離。
        /// 2. Floyd-Warshall算法：適用於求解所有城市之間的最短距離，但時間複雜度較高，對於大型圖形可能不太適合。
        /// 
        /// 注意：
        /// 如果城市數量較大，鄰接矩陣可能會消耗大量記憶體，可以考慮使用鄰接表。
        /// 可以使用堆優化的 Dijkstra 算法來提高效率。
        /// 如果需要處理大型圖形，可以考慮使用分治或近似算法。
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 4;
            int[][] input = new int[][]
            {
                 new int[]{ 0, 1, 3 },
                 new int[]{ 1, 2, 1 },
                 new int[]{ 1, 3, 4 },
                 new int[]{ 2, 3, 1 }
            };
            int distanceThreshold = 4;

            Console.WriteLine(FindTheCity(n, input, distanceThreshold));
            Console.ReadKey();
        }


        /// <summary>
        /// Floyd-Warshall 算法是解決任意兩點間的最短路徑的一種演算法。
        /// https://zh.wikipedia.org/zh-tw/Floyd-Warshall%E7%AE%97%E6%B3%95
        /// https://hackmd.io/@fdhscpp110/shortest_path
        /// 
        /// k 為中間點, 考慮是否經過點 k 能夠縮短 i 和 j 之間的路徑。
        /// 當 (i, j) >= (i, k) + (k, j) 時候
        /// (i, j) = (i, k) + (k, j)
        /// 簡單說就是原本 i 走道 j
        /// 現在是透過第三方城市 k
        /// 使得 i 到 k + k 到 j
        /// 會比原先 i, j 直達距離還要短
        /// 
        /// 
        /// ref:
        /// https://leetcode.cn/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/solutions/2524814/yu-zhi-ju-chi-nei-lin-ju-zui-shao-de-che-i73c/
        /// https://leetcode.cn/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/solutions/2525946/dai-ni-fa-ming-floyd-suan-fa-cong-ji-yi-m8s51/
        /// https://leetcode.cn/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/solutions/2526052/gong-shui-san-xie-han-gai-suo-you-cun-tu-svq7/
        /// https://leetcode.cn/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/solutions/1966076/by-stormsunshine-ksol/
        /// 
        /// map 一開始會給 預設值 int.MaxValue / 2
        /// int.MaxValue / 2:  除法用意是 防止加法溢出
        /// 之後會再把 edges 填入 map 裡面
        /// 
        /// 題目要求是取 distanceThreshold 內 可以到達的最少城市, 
        /// 所以更新 ans 的 if 要取 小於判斷
        /// 一開始的 map 資料填入是雙向圖
        /// 所以取資料時候不用擔心, 可以取道最大的城市編號
        /// 
        /// 
        /// --------------------------------------------------------------------
        /// 還有 Dijkstra 也可以處理
        /// 不過兩者有些微差異
        /// 不能檢查 負環
        /// </summary>
        /// <param name="n">總共n個城市 (編號從0開始, 所以共n - 1編號)</param>
        /// <param name="edges">from, to, weight, 注意方向是雙向圖</param>
        /// <param name="distanceThreshold">距离阈值是一个整数 distanceThreshold。</param>
        /// <returns></returns>
        public static int FindTheCity(int n, int[][] edges, int distanceThreshold)
        {
            // 可到達城市數量, 城市編號
            int[] ans = {int.MaxValue / 2, -1 };
            // 使用一個二維陣列表示雙向圖的鄰接矩陣, 儲存edges資料
            int[][] map = new int[n][];
            for(int i = 0; i < n; i++)
            {
                // new 二維陣列
                map[i] = new int[n];
                // 初始化map距離矩陣, 這邊用一個足夠大的數來表示無限大
                Array.Fill(map[i], int.MaxValue / 2);
            }

            // 將edges資料填入 map 裡面. 注意是雙向圖
            // a城市到b城市權重 以及 b城市到a城市權重
            foreach (int[] eg in edges)
            {
                int from = eg[0], to = eg[1], weight = eg[2];
                // 填入雙向圖權重
                map[from][to] = map[to][from] = weight;
            }

            // 找出中間點 k, 考慮是否經過點 k 能夠縮短 i 和 j 之間的路徑。
            // 當 (i, j) >= (i, k) + (k, j) 時候
            // 可以更新為 (i, j) = (i, k) + (k, j)
            for (int k = 0; k < n; k++)
            {
                // 每個城市到自己距離為0
                map[k][k] = 0;
                for(int i = 0; i < n; i++)
                {
                    for(int j = 0; j < n; j++)
                    {
                        /*
                        int a1 = map[i][k];
                        int a2 = map[j][k];
                        int a3 = map[i][j];
                        */

                        map[i][j] = Math.Min(map[i][j], map[i][k] + map[k][j]);
                    }
                }
            }

            for(int i = 0; i < n; i++)
            {
                int cnt = 0;
                for(int j = 0; j < n; j++)
                {
                    if (map[i][j] <= distanceThreshold)
                    {
                        // 計算 i 出發的共有多少符合要求城市
                        // 累計 distanceThreshold 內可到達城市數量
                        cnt++;
                    }
                }

                // 更新 ans 資料
                // 題目要求是取 distanceThreshold 內
                // 可以到達的最少城市, 所以 if 要取 小於判斷
                if (cnt <= ans[0])
                {
                    ans[0] = cnt;
                    ans[1] = i;
                }
            }

            return ans[1];

        }
    }
}
