namespace leetcode_973
{
    internal class Program
    {
        /// <summary>
        /// 973. K Closest Points to Origin
        /// https://leetcode.com/problems/k-closest-points-to-origin/description/
        /// 
        /// 973. 最接近原点的 K 个点
        /// https://leetcode.cn/problems/k-closest-points-to-origin/description/
        /// 
        /// 題目:
        /// 給定一個點的陣列，其中 points[i] = [xi, yi] 表示 X-Y 平面上的一個點，以及一個整數 k，返回距離原點 (0, 0) 最近的 k 個點。
        /// 在 X-Y 平面上兩點之間的距離是歐幾里得距離  (i.e., √(x1 - x2)^2 + (y1 - y2)^2).
        /// 你可以以任意順序返回答案。答案保證是唯一的（除了順序之外）。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{1, 3},
                 new int[]{-2, 2}
            };

            int k = 1;

            var res = KClosest(input, k);

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
        /// https://leetcode.cn/problems/k-closest-points-to-origin/solutions/477916/zui-jie-jin-yuan-dian-de-k-ge-dian-by-leetcode-sol/
        /// https://leetcode.cn/problems/k-closest-points-to-origin/solutions/478516/kuai-lai-miao-dong-topkkuai-pai-bian-xing-da-gen-d/
        /// https://leetcode.cn/problems/k-closest-points-to-origin/solutions/2918707/973-zui-jie-jin-yuan-dian-de-k-ge-dian-b-i9ci/
        /// 
        /// 直接採用直觀方法
        /// 平方歐幾里得距離
        /// https://zh.wikipedia.org/zh-tw/%E6%AC%A7%E5%87%A0%E9%87%8C%E5%BE%97%E8%B7%9D%E7%A6%BB
        /// 
        /// 根據題目敘述, 歐基里德 計算距離
        /// 为什么是欧几里得距离的「平方」？这是因为欧几里得距离并不一定是个整数，在进行计算和比较时可能会引进误差；但它的平方一定是个整数，这样我们就无需考虑误差了。
        /// 
        /// 欧几里得距离的平方一定是整数，因此可以计算欧几里得距离的平方，计算与原点的欧几里得距离的平方最小的 k 个点。
        /// 
        /// 二維空間 (x1, y1), (x2, y2)
        /// 距離 d = (x1 - x2)^2 + (y1 - y2)^2
        /// 取平方就免開根號了, 
        /// 簡單說就是:
        /// 平方歐幾里得距離 計算
        /// 每個輸入座標跟原點(0, 0)比對計算距離, 後續在排序大小
        /// </summary>
        /// <param name="points"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int[][] KClosest(int[][] points, int k)
        {
            // 題目給予的 Euclidean distance 公式取平方(不做開根號)計算後排序大小
            // d^2 = (x1 - x2)^2 + (y1 - y2)^2
            Array.Sort(points, (a, b) => (a[0] * a[0] - b[0] * b[0]) + (a[1] * a[1] - b[1] * b[1]));

            int[][] closet = new int[k][];
            // 取出前 k 組資料回傳答案
            Array.Copy(points, 0, closet, 0, k);
            return closet;
        }
    }
}
