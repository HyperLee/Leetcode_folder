using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1642
{
    internal class Program
    {
        /// <summary>
        /// 1642. Furthest Building You Can Reach
        /// https://leetcode.com/problems/furthest-building-you-can-reach/description/?envType=daily-question&envId=2024-02-17
        /// 1642. 可以到达的最远建筑
        /// https://leetcode.cn/problems/furthest-building-you-can-reach/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 14, 3, 19, 3 };
            int bricks = 17;
            int ladders = 0;

            Console.WriteLine(FurthestBuilding(input, bricks, ladders));
            Console.ReadKey();
        }



        /// <summary>
        /// https://leetcode.cn/problems/furthest-building-you-can-reach/solutions/2020841/by-stormsunshine-1xqe/
        /// https://leetcode.cn/problems/furthest-building-you-can-reach/solutions/468904/tan-xin-by-huang-ge-ru-meng-shi/
        /// 
        /// 
        /// 本方法leetcode 會 error
        /// 需要再改進
        /// 
        /// 先計算 i - 1 到 i 之間大樓高度的差異值 difference
        /// 一開始都先假設用 磚塊去填補差異值高度 讓他可以往下走
        /// 直到沒有磚塊或是走不下去為止
        /// 統計完一輪之後, 再用 梯子去取代最大差異值(使用最多磚塊的地方). 
        /// 這樣比較划算
        /// 
        /// 梯子取代之後就可以釋放出磚塊,
        /// 此時再繼續往下走看看 能不能填補 高度差
        /// 
        /// </summary>
        /// <param name="heights"></param>
        /// <param name="bricks"></param>
        /// <param name="ladders"></param>
        /// <returns></returns>
        public static int FurthestBuilding(int[] heights, int bricks, int ladders)
        {
            int maxbricks = 0;
            int maxBricksPosition = - 1;

            for(int i = 1; i < heights.Length; i++)
            {
                if (heights[i] > heights[i - 1])
                {
                    if (heights[i] - heights[i - 1] <= bricks)
                    {
                        bricks -= (heights[i] - heights[i - 1]);
                        // 紀錄 單一筆 消耗最多磚塊的 數量
                        maxbricks = Math.Max(maxbricks, heights[i] - heights[i - 1]);
                        // 紀錄 消耗最多磚塊的位置
                        maxBricksPosition = i - 1;
                    }
                    else if (heights[i] - heights[i - 1] > bricks && ladders > 0)
                    {
                        // 樓層高度差異值 比 現有磚塊數量還多, 且梯子還保留未使用之狀態

                        // 現在要用多少磚塊
                        int temp = heights[i] - heights[i - 1];
                        if(temp > maxbricks)
                        {
                            // 現在要用的 比 之前 最大消耗還多
                            // 直接用梯子取代
                            // 再把 磚塊 差異值補上.  現在消耗 扣除 之前最大堆
                            // 也可以說省下多少磚塊
                            ladders--;
                            bricks += temp - maxbricks;
                        }
                        else
                        {
                            // 直接用梯子補上
                            ladders--;
                        }
                    }
                    else
                    {
                        // 爬不動了
                        return i - 1;
                    }
                }
            }

            // 下標(index)從0開始 故 - 1
            return heights.Length - 1;
        }


        /// <summary>
        /// https://leetcode.cn/problems/furthest-building-you-can-reach/solutions/2020841/by-stormsunshine-1xqe/
        /// 此方法 vs 不能跑 因缺少 PriorityQueue
        /// 但是leetcode 可以過
        /// </summary>
        /// <param name="heights"></param>
        /// <param name="bricks"></param>
        /// <param name="ladders"></param>
        /// <returns></returns>
        public int FurthestBuilding2(int[] heights, int bricks, int ladders)
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            int furthest = 0;
            int length = heights.Length;

            for (int i = 1; i < length && bricks >= 0; i++)
            {
                int difference = heights[i] - heights[i - 1];

                if (difference > 0)
                {
                    pq.Enqueue(difference, -difference);
                    bricks -= difference;

                    while (bricks < 0 && ladders > 0 && pq.Count > 0)
                    {
                        bricks += pq.Dequeue();
                        ladders--;
                    }
                }

                if (bricks >= 0)
                {
                    furthest = i;
                }

            }

            return furthest;

        }
    }


}
