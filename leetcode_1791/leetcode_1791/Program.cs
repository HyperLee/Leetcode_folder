namespace leetcode_1791
{
    internal class Program
    {
        /// <summary>
        /// 1791. Find Center of Star Graph
        /// https://leetcode.com/problems/find-center-of-star-graph/description/?envType=daily-question&envId=2024-06-27
        /// 1791. 找出星型图的中心节点
        /// https://leetcode.cn/problems/find-center-of-star-graph/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 1, 2 },
                 new int[]{ 2, 3 },
                 new int[]{ 4, 2 }
            };

            Console.WriteLine(FindCenter2(input));
            Console.ReadKey();

        }



        /// <summary>
        /// 中心點就是在每一條邊上都會出現的數字
        /// 假設有3條邊, 輸入的陣列中 找出一個出現3次的數字 就是中心點
        /// 
        /// 1.利用Dic來統計陣列中的每一個數字
        /// 2.找出 出現的次數與 edges陣列 長度一致的 就是中心點
        /// 
        /// 這方法比較慢, 因為全部跑過一輪
        /// </summary>
        /// <param name="edges"></param>
        /// <returns></returns>
        public static int FindCenter(int[][] edges)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();

            // 兩組foreach統計 輸入陣列裡面每個數值
            foreach (var edge in edges) 
            {
                foreach (var node in edge)
                {
                    if(!dic.ContainsKey(node))
                    {
                        dic.Add(node, 1);
                    }
                    else
                    {
                        dic[node]++;
                    }
                }
            }

            // 找出dic裡面累計的次數與edges數量一致者
            foreach (var edge in dic)
            {
                if(edge.Value == edges.Length)
                {
                    return edge.Key;
                }
            }

            return -1;
        }


        /// <summary>
        /// https://leetcode.cn/problems/find-center-of-star-graph/solutions/1264727/zhao-chu-xing-xing-tu-de-zhong-xin-jie-d-1xzm/
        /// https://leetcode.cn/problems/find-center-of-star-graph/solutions/1273588/gong-shui-san-xie-jian-dan-mo-ni-ti-by-a-qoix/
        /// https://leetcode.cn/problems/find-center-of-star-graph/solutions/1273639/fu-xue-ming-zhu-xue-hui-ti-mu-bei-hou-de-5b52/
        /// https://leetcode.cn/problems/find-center-of-star-graph/solutions/915242/c-ha-xi-ji-shu-by-bloodborne-6g4g/
        /// 
        /// 
        /// 優化解法, 不需要全部的邊都走過一次
        /// 只需要統計兩條邊即可
        /// 兩條邊都出現同樣的數字, 就是中心點
        /// 
        /// 假設有以下兩條邊
        /// (a, b) 與 (c, d)
        /// 用a去跟第二組對比
        /// 假如 a = c or a = d 那 中心點就是a
        /// 否則就是 b
        /// </summary>
        /// <param name="edges"></param>
        /// <returns></returns>
        public static int FindCenter2(int[][] edges)
        {
            return edges[0][0] == edges[1][0] || edges[0][0] == edges[1][1] ? edges[0][0] : edges[0][1];

        }
    }
}
