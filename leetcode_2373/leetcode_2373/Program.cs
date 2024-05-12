namespace leetcode_2373
{
    internal class Program
    {
        /// <summary>
        /// 2373. Largest Local Values in a Matrix
        /// https://leetcode.com/problems/largest-local-values-in-a-matrix/description/?envType=daily-question&envId=2024-05-12
        /// 2373. 矩阵中的局部最大值
        /// https://leetcode.cn/problems/largest-local-values-in-a-matrix/description/
        /// 
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/arrays/jagged-arrays
        /// 不規則陣列
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
           
            int[][] input = new int[][]
            {
                 new int[]{ 9, 9 , 8, 1 },
                 new int[]{ 5, 6, 2, 6 },
                 new int[]{ 8, 6, 2, 4 },
                 new int[]{ 6, 2, 2, 2 }
            };

            //Console.WriteLine(LargestLocal(input));
            var res = LargestLocal(input);
            for (int i = 0; i < res.Length; i++)
            {
                System.Console.Write("Element({0}): ", i);

                for (int j = 0; j < res[i].Length; j++)
                {
                    System.Console.Write("{0}{1}", res[i][j], j == (res[i].Length - 1) ? "" : " ");
                }
                System.Console.WriteLine();
            }

            Console.ReadKey();
        }



        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/largest-local-values-in-a-matrix/solutions/2138032/ju-zhen-zhong-de-ju-bu-zui-da-zhi-by-lee-o703/
        /// https://leetcode.cn/problems/largest-local-values-in-a-matrix/solutions/1746845/yuan-di-xiu-gai-by-endlesscheng-m1k3/
        /// https://leetcode.cn/problems/largest-local-values-in-a-matrix/solutions/2576863/2373-ju-zhen-zhong-de-ju-bu-zui-da-zhi-b-kkc6/
        /// 
        /// 輸入的矩陣grid大小為 n * n 
        /// 要輸出 n - 2 * n - 2 範圍大小的矩陣res.
        /// 然後根據題目意思要在原先輸入的矩陣內
        /// 用輸入的 grid 範圍為 3 * 3 的大小去找出這範圍內最大數值
        /// 塞入要輸出的 n - 2 * n - 2 的矩陣內
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int[][] LargestLocal(int[][] grid)
        {
            int n = grid.Length;
            int[][] res = new int[n - 2][]; 

            // n - 2 的 x軸
            for(int i = 0; i < n - 2; i++)
            {
                res[i] = new int[n - 2];

                // n - 2 的 y軸
                for(int j = 0; j < n - 2; j++)
                {
                    // 3 * 3 範圍的 x 軸
                    for(int x = i; x < i + 3; x++)
                    {
                        // 3 * 3 範圍的 y 軸
                        for(int y = j; y < j + 3; y++)
                        {
                            // 找出 3 * 3 範圍內最大數值
                            res[i][j] = Math.Max(res[i][j], grid[x][y]);
                        }
                    }
                }
            }

            return res;
        }
    }
}
