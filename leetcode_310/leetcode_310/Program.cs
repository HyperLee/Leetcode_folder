namespace leetcode_310
{
    internal class Program
    {
        /// <summary>
        /// 310. Minimum Height Trees
        /// https://leetcode.com/problems/minimum-height-trees/description/?envType=daily-question&envId=2024-04-23
        /// 310. 最小高度树
        /// https://leetcode.cn/problems/minimum-height-trees/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 測試案例 1: 四個節點的樹
            Console.WriteLine("測試案例 1:");
            int n1 = 4;
            int[][] input1 = new int[][]
            {
                 new int[]{ 1, 0 },
                 new int[]{ 1, 2 },
                 new int[]{ 1, 3 }
            };
            var res1 = FindMinHeightTrees(n1, input1);
            Console.WriteLine("預期結果: 1");
            Console.WriteLine("實際結果:");
            foreach (int i in res1) 
            {
                Console.Write(i + ", ");
            }
            
            // 測試案例 2: 六個節點的樹
            Console.WriteLine("\n測試案例 2:");
            int n2 = 6;
            int[][] input2 = new int[][]
            {
                 new int[]{ 3, 0 },
                 new int[]{ 3, 1 },
                 new int[]{ 3, 2 },
                 new int[]{ 3, 4 },
                 new int[]{ 5, 4 }
            };
            var res2 = FindMinHeightTrees(n2, input2);
            Console.WriteLine("預期結果: 3, 4");
            Console.WriteLine("實際結果:");
            foreach (int i in res2)
            {
                Console.Write(i + ", ");
            }

            // 測試案例 3: 單個節點
            Console.WriteLine("\n測試案例 3:");
            int n3 = 1;
            int[][] input3 = new int[][] { };
            var res3 = FindMinHeightTrees(n3, input3);
            Console.WriteLine("預期結果: 0");
            Console.WriteLine("實際結果:");
            foreach (int i in res3)
            {
                Console.Write(i + ", ");
            }
            
        }

        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/minimum-height-trees/solutions/1395249/zui-xiao-gao-du-shu-by-leetcode-solution-6v6f/
        /// https://leetcode.cn/problems/minimum-height-trees/solutions/1397905/by-ac_oier-7xio/
        /// https://leetcode.cn/problems/minimum-height-trees/solutions/1961777/by-stormsunshine-fv74/
        /// 
        /// 解題思路：
        /// 1. 找到圖中的最長路徑（樹的直徑）：
        ///    - 從任意節點開始進行DFS找到最遠的節點x
        ///    - 從節點x開始再次DFS找到最遠的節點y
        ///    - x和y之間的路徑就是圖中的最長路徑
        /// 
        /// 2. 最小高度樹的根節點必定在最長路徑的中間位置：
        ///    - 如果路徑長度為奇數，中間節點即為答案
        ///    - 如果路徑長度為偶數，中間的兩個節點都是答案
        /// 
        /// 時間複雜度：O(n)，其中n為節點數量
        /// 空間複雜度：O(n)，用於存儲鄰接表和訪問數組
        /// 
        /// 最小高度樹的特性：
        /// 最小高度樹的根節點必定位於整棵樹的「中心位置」
        /// 這些根節點最多只有兩個
        /// 它們位於樹的最長路徑（直徑）的中點
        /// </summary>
        public static IList<int> FindMinHeightTrees(int n, int[][] edges)
        {
            IList<int> res = new List<int>();
            if(n == 1)
            {
                // 特殊情況：只有一個節點，該節點即為根節點
                res.Add(0);
                return res;
            }

            // 建立鄰接表來表示無向圖
            // 每個節點儲存與其相連的所有節點
            IList<int>[] adj = new List<int>[n];
            for(int i = 0; i < n; i++)
            {
                adj[i] = new List<int>();
            }

            // 建立無向圖的邊關係
            // 因為是無向圖，所以每條邊都需要雙向添加
            foreach (int[] edge in edges)
            {
                adj[edge[0]].Add(edge[1]); // 添加 a->b 的邊
                adj[edge[1]].Add(edge[0]); // 添加 b->a 的邊
            }

            // parent數組用於記錄DFS過程中的父節點關係
            // 用於記錄在 DFS 遍歷過程中每個節點的父節點
            // 幫助我們能夠從任意節點回溯到起始節點
            int[] parent = new int[n];
            // -1 表示該節點尚未被訪問; 非 -1 的值表示該節點已被訪問，並記錄其父節點
            Array.Fill(parent, -1);
            
            // 第一次DFS：從節點0開始，找到最遠的節點x
            int x = FindLongestNode(0, parent, adj);
            
            // 第二次DFS：從節點x開始，找到最遠的節點y
            // x和y之間的路徑即為圖中的最長路徑
            int y = FindLongestNode(x, parent, adj);
            
            // 找出從x到y的路徑
            IList<int> path = new List<int>();
            parent[x] = -1; // 重置起點x的父節點為-1
            
            // 通過parent數組回溯，記錄從y到x的完整路徑
            while (y != -1)
            {
                // 將當前節點加入路徑
                path.Add(y);
                // 移動到父節點
                y = parent[y];
            }
            
            // 根據路徑長度判斷根節點
            int m = path.Count;
            // 判斷路徑長度是否為偶數
            if (m % 2 == 0)
            {
                // 如果路徑長度為偶數，取中間兩個節點
                // 偶數長度：加入第一個中間節點
                res.Add(path[m / 2 - 1]);
            }

            // 無論奇偶，都要加入中間節點
            // 注意,偶數長度中間節點有兩個.這裡是加入第二個
            // 反之長度為奇數只有這行會加入中間節點
            res.Add(path[m / 2]);

            return res;
        }

        /// <summary>
        /// FindLongestNode 方法用於找出距離給定起始節點最遠的節點
        /// 找出距離給定起始節點最遠的節點，這是找到樹直徑的關鍵步驟。
        /// 這個函數是找到樹直徑的核心步驟，通過兩次呼叫可以確定樹中最長的路徑。
        /// [0, x]: 第一次, 從任意節點開始，找到的最遠點必定是直徑的一個端點
        /// [x, y]: 第二次, 從直徑端點出發，找到的最遠點必定是直徑的另一個端點
        /// 
        /// 為什麼需要兩次 DFS：
        /// 第一次 DFS 找到的最遠點一定是直徑的一個端點
        /// 但從起點到這個最遠點的路徑不一定是直徑
        /// 
        /// 直徑的定義：
        /// 樹中任意兩點間的最長路徑
        /// 必須從確定的直徑端點開始尋找另一個端點
        /// 
        /// 解題思路：
        /// 1. 使用DFS遍歷整個圖，記錄每個節點到起始節點的距離
        /// 2. 同時記錄每個節點的父節點，用於之後重建路徑
        /// 3. 在所有節點中找出距離最遠的節點
        /// 
        /// 參數說明：
        /// - u: 起始節點
        /// - parent: 用於記錄每個節點的父節點
        /// - adj: 鄰接表表示的圖結構
        /// 
        /// 返回值：距離起始節點最遠的節點編號
        /// </summary>
        public static int FindLongestNode(int u, int[] parent, IList<int>[] adj)
        {
            // 獲取圖中節點總數
            int n = adj.Length;
            
            // 創建距離數組
            int[] dist = new int[n];
            // 初始化距離數組，-1表示未訪問
            Array.Fill(dist, -1);
            
            // 設置起始節點距離為0
            dist[u] = 0;
            
            // 開始DFS遍歷
            DFS(u, dist, parent, adj);
            
            // 尋找最大距離的節點
            int maxdist = 0;
            // 記錄最遠節點
            int node = -1;

            // 遍歷所有節點找出距離最遠的節點
            // 遍歷所有節點
            for(int i = 0; i < n; i++)
            {
                // 如果找到更遠的節點
                if (dist[i] > maxdist)
                {
                    // 更新最大距離
                    maxdist = dist[i];
                    // 更新最遠節點
                    node = i;
                }
            }

            return node;
        }

        /// <summary>
        /// DFS（深度優先搜索）方法用於遍歷圖並記錄節點間的距離
        /// 
        /// 解題思路：
        /// 1. 針對當前節點的每個相鄰節點進行遍歷
        /// 2. 如果相鄰節點未被訪問過，則更新其距離和父節點
        /// 3. 遞歸處理每個相鄰節點
        /// 
        /// 參數說明：
        /// - u: 當前正在處理的節點
        /// - dist: 記錄每個節點到起始節點的距離
        /// - parent: 記錄每個節點的父節點
        /// - adj: 圖的鄰接表表示
        /// </summary>
        public static void DFS(int u, int[] dist, int[] parent, IList<int>[] adj)
        {
            // 遍歷當前節點的所有相鄰節點
            foreach(int v in adj[u])
            {
                // 如果相鄰節點未被訪問過（距離為-1）
                if (dist[v] < 0)
                {
                    // 更新相鄰節點的距離（當前節點距離+1）
                    // 每次遞歸時距離+1; 確保正確計算到起始節點的距離
                    // 每多一層距離就 +1
                    dist[v] = dist[u] + 1;
                    // 記錄父節點關係
                    parent[v] = u;
                    // 遞歸處理相鄰節點
                    DFS(v, dist, parent, adj);
                }
            }
        }
    }
}
