namespace leetcode_1380
{
    internal class Program
    {
        /// <summary>
        /// 1380. Lucky Numbers in a Matrix
        /// https://leetcode.com/problems/lucky-numbers-in-a-matrix/description/?envType=daily-question&envId=2024-07-19
        /// 1380. 矩阵中的幸运数
        /// https://leetcode.cn/problems/lucky-numbers-in-a-matrix/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 3, 7, 8 },
                 new int[]{ 9, 11, 13 },
                 new int[]{ 15, 16, 17 }
            };

            var res = LuckyNumbers(input);

            foreach (int i in res) 
            {
                Console.WriteLine("res: " + i);
            }

            Console.ReadKey();
        }



        /// <summary>
        /// Lucky Numbers:
        /// 1. 同一行(左右)中, 數字最小
        /// 2. 同一列(上下)中, 數字最大
        /// 
        /// https://leetcode.cn/problems/lucky-numbers-in-a-matrix/solutions/241888/ju-zhen-zhong-de-xing-yun-shu-by-leetcode-solution/
        /// https://leetcode.cn/problems/lucky-numbers-in-a-matrix/solutions/1266828/gong-shui-san-xie-jian-dan-mo-ni-ti-by-a-9xwg/
        /// https://leetcode.cn/problems/lucky-numbers-in-a-matrix/solutions/2637045/1380-ju-zhen-zhong-de-xing-yun-shu-by-st-96ir/
        /// 
        /// continue 陳述式會啟動最接近封閉式反覆項目陳述式的新反覆項目 (即 for、foreach、while 或 do 迴圈)
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/statements/jump-statements#the-continue-statement
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static IList<int> LuckyNumbers(int[][] matrix)
        {
            // 列
            int m = matrix.Length;
            // 行
            int n = matrix[0].Length;
            IList<int> result = new List<int>();

            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    bool isMin = true;
                    bool isMax = true;

                    // 找出同一行 最小者
                    for(int k = 0; k < n; k++)
                    {
                        //int tempa = matrix[i][k];
                        //int tempb = matrix[i][j];
                        // j 用 k 取代
                        if (matrix[i][k] < matrix[i][j])
                        {
                            // 比當前位置[i][j] 還要小
                            // 就代表 目前位置不是最小, 
                            // 要找出最小者
                            isMin = false;
                            break;
                        }
                    }

                    if(!isMin)
                    {
                        // false, 不往下走.
                        // 跳回去第二層迴圈
                        // 這判斷可寫,可不寫. 效能差異不是很明顯
                        continue;
                    }

                    // 找出同一列 最大者
                    for(int k = 0; k < m; k++)
                    {
                        //int tempa = matrix[k][j];
                        //int tempb = matrix[i][j];
                        // i 用 k 取代
                        if (matrix[k][j] > matrix[i][j])
                        {
                            // 比當前位置[i][j] 還要大
                            // 就代表 目前位置不是最大,
                            // 要找出最大者
                            isMax = false;
                            break;
                        }
                    }

                    if(isMax == true && isMin == true)
                    {
                        result.Add(matrix[i][j]);
                    }
                }
            }

            return result;
        }
    }
}
