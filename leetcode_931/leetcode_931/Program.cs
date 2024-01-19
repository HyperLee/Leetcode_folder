using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_931
{
    internal class Program
    {
        /// <summary>
        /// 931. Minimum Falling Path Sum
        /// https://leetcode.com/problems/minimum-falling-path-sum/description/?envType=daily-question&envId=2024-01-19
        /// 931. 下降路径最小和
        /// https://leetcode.cn/problems/minimum-falling-path-sum/description/
        /// 
        /// 不規則陣列
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/arrays#jagged-arrays
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // [][]:不規則陣列, 
            int[][] input =
            {
                new int[] { 2, 1, 3 },
                new int[] { 6, 5, 4 },
                new int[] { 7, 8, 9 }
            };

            Console.WriteLine("結果1: " + MinFallingPathSum(input));
            Console.ReadKey();

        }


        /// <summary>
        /// 官方:
        /// https://leetcode.cn/problems/minimum-falling-path-sum/solutions/2338060/xia-jiang-lu-jing-zui-xiao-he-by-leetcod-vyww/
        /// 
        /// 參考2:
        /// https://leetcode.cn/problems/minimum-falling-path-sum/solutions/2253947/931-xia-jiang-lu-jing-zui-xiao-he-by-sto-5z05/
        /// 
        /// 建議畫圖比較好理解
        /// 
        /// 
        /// 輸出結果1 可以研究一下. 第一次看到這種寫法
        /// 輸出結果2 比較常見的用法
        /// 
        /// 上方往下找,  找出最小路徑總和
        /// 間格只能跨一行
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static int MinFallingPathSum(int[][] matrix)
        {
            int n = matrix.Length;
            int[][] dp = new int[n][];
            // 初始化陣列大小
            for(int i = 0; i < n; i++)
            {
                dp[i] = new int[n];
            }

            // 複製 方法1:  matrix 第0列 複製 至 dp 第0列; 最上方起始
            Array.Copy(matrix[0], 0, dp[0], 0, n);

            // 複製 方法2: matrix 第0列 複製 至 dp 第0列; 最上方起始
            //for (int j = 0; j < n; j++)
            //{
            //    dp[0][j] = matrix[0][j];
            //}

            // 行
            for (int i = 1; i < n; i++)
            {
                // 列
                for(int j = 0; j < n; j++)
                {
                    // 從最上方第0行開始找出最小值,再來依靠下方判斷式
                    // 沿著路徑 找出每一行列加總後最小值
                    // 第一列,最後一列有邊界問題.
                    // j在最上方(j小)只能往下找(j大), j在下方(j大)就要往上找(j小)
                    
                    int mn = dp[i - 1][j];

                    if(j > 0)
                    {
                        mn = Math.Min(mn, dp[i - 1][j - 1]);
                    }

                    if(j < n - 1)
                    {
                        mn = Math.Min(mn, dp[i - 1][j + 1]);
                    }

                    // 挑選出路徑之後, 將數值加總進dp[][]裡面
                    // 當前最小值 + 下一行位置加總 塞入 dp
                    dp[i][j] = mn + matrix[i][j];
                }
            }

            // 找出最小和 
            // 方法1. dp[n - 1].Min();
            // 方法2.
            /*
            int min = int.MaxValue;
            foreach(int sum in dp[n - 1])
            {
                min = Math.Min(min, sum);
            }

            int result = min;
            Console.WriteLine("結果2: " + result);
            */

            return dp[n - 1].Min();

        }

    }
}
