namespace leetcode_1992
{
    internal class Program
    {
        /// <summary>
        /// 1992. Find All Groups of Farmland
        /// https://leetcode.com/problems/find-all-groups-of-farmland/description/?envType=daily-question&envId=2024-04-20
        /// 1992. 找到所有的农场组
        /// https://leetcode.cn/problems/find-all-groups-of-farmland/description/
        /// 
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/arrays/jagged-arrays
        /// 不規則陣列
        /// 宣告
        /// 取長度
        /// 以及輸出 方式
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 不規則陣列
            int[][] input = new int[][]
            {
                 new int[]{ 1, 0, 0, 0 },
                 new int[]{ 0, 1, 1, 0 },
                 new int[]{ 0, 1, 1, 0 }
            };

            // output answer
            var res = FindFarmland(input);
            for (int i = 0; i < res.Length; i++)
            {
                System.Console.Write("第({0})組: ", i);

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
        /// https://leetcode.cn/problems/find-all-groups-of-farmland/solutions/2635528/1992-zhao-dao-suo-you-de-nong-chang-zu-b-e7vv/
        /// 
        /// 1.要知道如何分辨 森林 與 農場
        /// 2.農場區域 不相鄰, 所以 農場旁邊都是 森林
        /// 3.要知道如何取 左上 與 右下
        /// 4.定位出農場位置之後, 即為左上[i][j] , 接下來用while去跑出 右下位置
        /// </summary>
        /// <param name="land"></param>
        /// <returns></returns>
        public static int[][] FindFarmland(int[][] land)
        {
            IList<int[]> farmlandgroups = new List<int[]>();
            // 行
            int m = land.Length;
            // 列
            int n = land[0].Length;

            for (int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    // 符合這條件要排除 因這是森林, 本身為0 且相鄰(上面與左邊)都是 農場; 
                    if (land[i][j] == 0 || (i > 0 && land[i - 1][j] == 1) || (j > 0 && land[i][j - 1] == 1))
                    {
                        continue;
                    }

                    // 排除森林之後, 接下來[i][j]為農場左上角
                    // 接著 往下 + 往右 => 找出 農場矩陣右下角位置
                    int endrow = i;
                    int endcol = j;

                    while (endrow + 1 < m && land[endrow + 1][endcol] == 1)
                    {
                        // 往下找
                        endrow++;
                    }

                    while (endcol + 1 < n && land[endrow][endcol + 1] == 1)
                    {
                        // 往右找
                        endcol++;
                    }

                    farmlandgroups.Add(new int[] {i, j, endrow, endcol});

                }
            }

            return farmlandgroups.ToArray();
        }

    }
}
