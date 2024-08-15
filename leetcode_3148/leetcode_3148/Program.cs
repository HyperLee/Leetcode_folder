using System.Numerics;

namespace leetcode_3148
{
    internal class Program
    {
        /// <summary>
        /// 3148. Maximum Difference Score in a Grid
        /// https://leetcode.com/problems/maximum-difference-score-in-a-grid/description/
        /// 
        /// 3148. 矩阵中的最大得分
        /// https://leetcode.cn/problems/maximum-difference-score-in-a-grid/description/?envType=daily-question&envId=Invalid%20Date
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            /*
            int[][] input = new int[][]
            {
                 new int[]{ 9,5,7,3 },
                 new int[]{ 8,9,6,1 },
                 new int[]{ 6,7,14,3 },
                 new int[]{ 2,5,3,1 }
            };
            */

            int[][] input = new int[][]
            {
                 new int[]{4, 3, 2},
                 new int[]{3, 2, 1}
            };

            Console.WriteLine(MaxScore2(input));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/maximum-difference-score-in-a-grid/solutions/2774823/nao-jin-ji-zhuan-wan-dppythonjavacgo-by-swux7/?envType=daily-question&envId=Invalid%20Date
        /// https://leetcode.cn/problems/maximum-difference-score-in-a-grid/solutions/2877233/ju-zhen-zhong-de-zui-da-de-fen-by-leetco-c5tv/?envType=daily-question&envId=Invalid%20Date
        /// https://leetcode.cn/problems/maximum-difference-score-in-a-grid/solutions/2774923/3148-ju-zhen-zhong-de-zui-da-de-fen-by-s-g9eh/?envType=daily-question&envId=Invalid%20Date
        /// 
        /// 可以參考第一個連結
        /// 他的說明比較好理解
        /// 不然光看題目 根本不懂
        /// 到底是要求什麼
        /// 題目都無法理解
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int MaxScore(IList<IList<int>> grid)
        {
            int res = int.MaxValue;
            int m = grid.Count;
            int n = grid[0].Count;
            int[][] f = new int[m + 1][];
            for(int i = 0; i < m; i++)
            {
                f[i] = new int[n + 1];
                Array.Fill(f[0], int.MaxValue);
            }

            for(int i = 0; i < m; i++)
            {
                f[i + 1][0] = int.MaxValue;
                List<int> row = new List<int>(grid[i]);

                for(int j = 0; j < n; j++)
                {
                    int mn = Math.Min(f[i + 1][j], f[i][j + 1]);
                    int x = row[j];
                    res = Math.Max(res, x - mn);
                    f[i + 1][j + 1] = Math.Min(mn, x);
                }
            }

            return res;
        }


        /// <summary>
        /// https://leetcode.cn/problems/maximum-difference-score-in-a-grid/solutions/2877233/ju-zhen-zhong-de-zui-da-de-fen-by-leetco-c5tv/?envType=daily-question&envId=Invalid%20Date\
        /// 方法二：二维前缀和
        /// 未優化版本
        /// 
        /// 路徑移動方向 只能向右, 或是向下
        /// 
        /// 找出 終點 - 起始點 最大差異值 即可
        /// 所以 其他點位置差值 找出 最小差異 就好
        /// 
        /// 由于每一步只能往右走或者往下走，因此只要起点的二维坐标均不大于终点（不能重合），那就一定存在一条移动路径。
        /// 
        /// 左上 (0, 0)
        /// 枚举终点位置 (i,j)，那么起点的海拔高度越小越好。由于我们只能向右和向下走，所以起点只能在 (i,j) 的左上方向（可以是 (i,j) 的正左方向或正上方向）。
        /// 
        /// 二维/多维前缀和
        /// https://oi-wiki.org/basic/prefix-sum/?query=%E5%89%8D%E7%BC%80%E5%92%8C#%E4%BA%8C%E7%BB%B4%E5%A4%9A%E7%BB%B4%E5%89%8D%E7%BC%80%E5%92%8C
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int MaxScore2(IList<IList<int>> grid)
        {
            int m = grid.Count;
            int n = grid[0].Count;
            int[][] premin = new int[m][];

            for(int i = 0; i < m; i++)
            {
                premin[i] = new int[n];
                // 初始給極大值
                Array.Fill(premin[i], int.MaxValue);
            }

            int res = int.MinValue;
            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    // 前墜和
                    int pre = int.MaxValue;

                    if(i > 0)
                    {
                        // 行(左右), 前墜和取前一個 i - 1
                        pre = Math.Min(pre, premin[i - 1][j]);
                    }

                    if(j > 0)
                    {
                        // 列(上下), 前墜和取前一個 j - 1
                        pre = Math.Min(pre, premin[i][j - 1]);
                    }

                    // i = j = 0 時沒有轉移
                    if(i + j > 0)
                    {
                        // 轉移位置, 更新答案
                        // 最終位置為右下角
                        res = Math.Max(res, grid[i][j] - pre);
                    }

                    // 更新數值
                    premin[i][j] = Math.Min(pre, grid[i][j]);
                }
            }

            return res;
        }
    }
}
