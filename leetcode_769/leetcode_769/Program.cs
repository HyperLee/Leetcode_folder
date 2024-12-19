namespace leetcode_769
{
    internal class Program
    {
        /// <summary>
        /// 769. Max Chunks To Make Sorted
        /// https://leetcode.com/problems/max-chunks-to-make-sorted/description/?envType=daily-question&envId=2024-12-19
        /// 
        /// 769. 最多能完成排序的块
        /// https://leetcode.cn/problems/max-chunks-to-make-sorted/description/
        /// 
        /// input資料是 0 ~ n - 1
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 4, 3, 2, 1, 0 };

            Console.WriteLine("method1: " + MaxChunksToSorted(input));
            Console.WriteLine("method2: " + MaxChunksToSorted2(input));
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/max-chunks-to-make-sorted/solutions/1886333/zui-duo-neng-wan-cheng-pai-xu-de-kuai-by-gc4k/
        /// https://leetcode.cn/problems/max-chunks-to-make-sorted/solutions/1888391/by-ac_oier-4uny/
        /// https://leetcode.cn/problems/max-chunks-to-make-sorted/solutions/2019566/by-stormsunshine-chzk/
        /// 
        /// input資料是 0 ~ n - 1
        /// 所以 if 用 index == i 似乎可以理解
        /// 能對上是最好
        /// 代表 A = B + C
        /// B 與 C 排序後 可以對應上 A
        /// 
        /// 如果前 i + 1 个数的最大值为 i ，那么前 i + 1 个数排序后一定是 [0,1,2,...,i]
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int MaxChunksToSorted(int[] arr)
        {
            int m = 0;
            int res = 0;

            for(int i = 0; i < arr.Length; i++)
            {
                m = Math.Max(m, arr[i]);
                if(m == i)
                {
                    res++;
                }
            }

            return res;
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/max-chunks-to-make-sorted/solutions/1888391/by-ac_oier-4uny/
        /// 
        /// i: 右邊界 index
        /// j: 左邊界 index, 初始值為 0.
        /// 
        /// min: 當前劃分區塊中的 element 最小值, 初始為 arr[0] or n (取 Min 所以初始給大值)
        /// max: 當前劃分區塊中的 element 最大值, 初始為 arr[1] or -1 (取 Max 所以初始給小值)
        /// 
        /// j == min && i == max => [j, i] 排序後 [min, max]
        /// 上述條件成立 res++.
        /// 然後重新開始初始化變數, 繼續循環找下一組答案
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int MaxChunksToSorted2(int[] arr)
        {
            int n = arr.Length;
            int res = 0;

            for(int i = 0, j = 0, min = n, max = -1; i < n; i++)
            {
                min = Math.Min(min, arr[i]);
                max = Math.Max(max, arr[i]);

                if(j == min && i == max)
                {
                    res++;
                    // 左邊界右移
                    j = i + 1;
                    min = n;
                    max = -1;
                }
            }

            return res;
        }
    }
}
