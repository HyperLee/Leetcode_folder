namespace leetcode_542
{
    internal class Program
    {
        /// <summary>
        /// 542. 01 Matrix
        /// https://leetcode.com/problems/01-matrix/description/
        /// 
        /// 542. 01 矩阵
        /// https://leetcode.cn/problems/01-matrix/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{0, 0, 0},
                 new int[]{0, 1, 0},
                 new int[]{0, 0, 0}
            };

            var res = UpdateMatrix(input);

            // 不規則陣列輸出
            for (int i = 0; i < res.Length; i++)
            {
                Console.Write("Element({0}): ", i);

                for (int j = 0; j < res[i].Length; j++)
                {
                    Console.Write("{0}{1}", res[i][j], j == (res[i].Length - 1) ? "" : ", ");
                }

                Console.WriteLine();
            }
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/01-matrix/solutions/202012/01ju-zhen-by-leetcode-solution/
        /// https://leetcode.cn/problems/01-matrix/solutions/203486/2chong-bfs-xiang-jie-dp-bi-xu-miao-dong-by-sweetie/ <-- 有詳細解說
        /// https://leetcode.cn/problems/01-matrix/solutions/1862396/by-stormsunshine-4sg6/
        /// 
        /// 直接透過原數組 matrix 修改
        /// 沒有額外新增空間儲存
        /// 
        /// ref 連結說明要多看幾次, 才比較好理解解法
        /// 对于 「Tree 的 BFS」 （典型的「单源 BFS」） 大家都已经轻车熟路了：
        /// -> 首先把 root 节点入队，再一层一层无脑遍历就行了。
        /// 
        /// 对于 「图 的 BFS」 （「多源 BFS」） 做法其实也是一样滴～，与 「Tree 的 BFS」的区别注意以下两条就 ok 辣～
        /// -> Tree 只有 1 个 root，而图可以有多个源点，所以首先需要把多个源点都入队；
        /// -> Tree 是有向的因此不需要标识是否访问过，而对于无向图来说，必须得标志是否访问过哦！并且为了防止某个节点多次入
        ///    队，需要在其入队之前就将其设置成已访问！【 看见很多人说自己的 BFS 超时了，坑就在这里哈哈哈
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static int[][] UpdateMatrix(int[][] mat)
        {
            // 首先將所有的 0 都入隊, 並且將 1 的位置設置成 -1, 
            // 表示該位置是 未被訪問過的 1
            Queue<int[]> queue = new Queue<int[]>();
            // 行(上下)
            int m = mat.Length;
            // 列(左右)
            int n = mat[0].Length;
            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if (mat[i][j] == 0)
                    {
                        // value == 0: index 加入 queue, 後續要訪問使用
                        queue.Enqueue(new int[] { i, j});
                    }
                    else
                    {
                        // value == 1: 設定成未訪問狀態, 避免重複訪問
                        mat[i][j] = -1;
                    }
                }
            }

            // 移動至四個鄰近(上下左右)
            int[] dx = new int[] {-1, 1, 0, 0 };
            int[] dy = new int[] {0, 0, -1, 1 };

            while(queue.Count > 0)
            {
                // queue 取出 index 位置來遍歷
                int[] point = queue.Dequeue();
                // x, y 軸
                int x = point[0], y = point[1];
                // 如果四個鄰近(上下左右)的點是 -1, 表示這個點是未被訪問過的 1
                // 所以這個點到 0 的距離就可以更新成 mat[x][y] + 1
                for (int i = 0; i < 4; i++)
                {
                    int newX = x + dx[i];
                    int newY = y + dy[i];
                    if (newX >= 0 && newX < m && newY >= 0 && newY < n && mat[newX][newY] == -1)
                    {
                        // 計算距離
                        mat[newX][newY] = mat[x][y] + 1;
                        queue.Enqueue(new int[] { newX, newY });
                    }
                }
            }

            return mat;
        }
    }
}
