namespace leetcode_506
{
    internal class Program
    {
        /// <summary>
        /// 506. Relative Ranks
        /// https://leetcode.com/problems/relative-ranks/?envType=daily-question&envId=2024-05-08
        /// 506. 相对名次
        /// https://leetcode.cn/problems/relative-ranks/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = {1, 2, 3, 4, 5 };
            //Console.WriteLine(FindRelativeRanks(input));
            var res = FindRelativeRanks(input);
            foreach ( var i in res ) 
            {
                Console.WriteLine(i);
            }

            Console.ReadKey();
        }



        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/relative-ranks/solutions/1131693/xiang-dui-ming-ci-by-leetcode-solution-5sua/
        /// https://leetcode.cn/problems/relative-ranks/solutions/1133453/gong-shui-san-xie-jian-dan-pai-xu-mo-ni-cmuzj/
        /// https://leetcode.cn/problems/relative-ranks/solutions/1509971/506-xiang-dui-ming-ci-by-stormsunshine-7gyl/
        /// 
        /// 題目要求, 依據選手 得分 來做排名輸出
        /// 原始順序不能動
        /// 前三名要改成 "Gold Medal", "Silver Medal", "Bronze Medal"
        /// 其餘的輸出是 阿拉伯數字
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public static string[] FindRelativeRanks(int[] score)
        {
            int n = score.Length;
            // 前三名要用文字取代數字
            string[] rank = {"Gold Medal", "Silver Medal", "Bronze Medal" };
            int[][] arr = new int[n][];

            for(int i = 0; i < n; i++)
            {
                // 紀錄 原始輸入 兩種資料
                arr[i] = new int[2];
                // 1. 該index 選手得分
                arr[i][0] = score[i];
                // 2. 該index 位置
                arr[i][1] = i;
            }

            // 依據選手得分來排序 大至小
            Array.Sort(arr, (a, b) => b[0] - a[0]);
            string[] ans = new string[n];

            for(int i = 0; i < n; i++)
            {
                if(i >= 3)
                {
                    // 第四名開始, 因選手排名是從1開始. 故要 i + 1. (array是從0開始)
                    ans[arr[i][1]] = (i + 1).ToString();
                }
                else
                {
                    // 前三名 文字取代index
                    ans[arr[i][1]] = rank[i];
                }
            }

            return ans;
        }
    }
}
