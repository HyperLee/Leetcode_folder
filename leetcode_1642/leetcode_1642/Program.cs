namespace leetcode_1642;

class Program
{
    /// <summary>
    /// 1642. Furthest Building You Can Reach
    /// https://leetcode.com/problems/furthest-building-you-can-reach/description/
    /// 1642. 可以到达的最远建筑
    /// https://leetcode.cn/problems/furthest-building-you-can-reach/description/
    ///
    /// English:
    /// You are given an integer array heights representing the heights of buildings,
    /// some bricks, and some ladders.
    ///
    /// You start your journey from building 0 and move to the next building by possibly
    /// using bricks or ladders.
    ///
    /// While moving from building i to building i+1 (0-indexed):
    /// - If the current building's height is greater than or equal to the next building's
    ///   height, you do not need a ladder or bricks.
    /// - If the current building's height is less than the next building's height, you can
    ///   either use one ladder or (h[i+1] - h[i]) bricks.
    ///
    /// Return the furthest building index (0-indexed) you can reach if you use the given
    /// ladders and bricks optimally.
    ///
    /// 繁體中文：
    /// 給你一個整數陣列 heights，表示各棟建築物的高度，以及一些磚塊和梯子。
    ///
    /// 你從第 0 棟建築物開始旅程，並移動到下一棟建築物；移動時可能需要使用磚塊或梯子。
    ///
    /// 當你從第 i 棟建築物移動到第 i + 1 棟建築物時（索引從 0 開始）：
    /// - 如果目前建築物的高度大於或等於下一棟建築物的高度，則不需要使用梯子或磚塊。
    /// - 如果目前建築物的高度小於下一棟建築物的高度，你可以選擇使用一把梯子，或使用
    ///   (heights[i + 1] - heights[i]) 塊磚塊。
    ///
    /// 如果以最佳方式使用給定的梯子和磚塊，請回傳你能到達的最遠建築物索引（索引從 0 開始）。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    ///  貪心策略 PriorityQueue
    /// 
    /// 当 i>0 时，从建筑物 i−1 到达建筑物 i，可能有以下两种情况。
    /// - 如果 heights[i]≤heights[i−1]，则不需要梯子或砖块。
    /// - 如果 heights[i]>heights[i−1]，则需要一架梯子或 heights[i]−heights[i−1] 个砖块。
    /// 由于一架梯子可以取代任意数量的砖块，因此可以首先考虑不用梯子只用砖块的情况下可以到达的最远的建筑物，然后将砖块替换
    /// 成梯子，使可以到达的最远建筑物下标最大化。
    /// 
    /// 只用砖块的情况下，可以到达的最远建筑物下标与砖块数量正相关，即砖块数量越多，则可以到达的最远建筑物下标越大。为了到
    /// 达尽可能远的建筑物，在将砖块替换成梯子时，应将高度差最大位置的砖块替换成梯子，此时剩余的砖块数量最多，如果将高度差
    /// 较小位置的砖块替换成梯子，则剩余的砖块数量较少，能到达的最远建筑物下标不可能更大。
    /// 
    /// 根据上述分析，可以使用贪心策略计算可以到达的最远建筑物的下标。
    /// 
    /// 从建筑物 0 开始向后面的建筑物移动，移动过程中维护优先队列存储正高度差，优先队列的队首元素是最大元素，每次遇到正高
    /// 度差时优先考虑使用砖块，如果砖块数量不够则使用梯子替换最大高度差位置的砖块，直到砖块和梯子都使用完毕时，到达的建筑
    /// 物即为最远建筑物。
    /// 
    /// 具体做法是，从下标 1 开始从左到右遍历数组 heights，当遍历到下标 i 时，执行如下操作。
    /// 1. 计算高度差 difference=heights[i]−heights[i−1]。
    /// 2. 如果 difference≤0 则可以直接到达下标 i，如果 difference>0 则需要使用砖块或梯子，做法如下。
    /// - 将 difference 加入优先队列，将 bricks 减少 difference。
    /// - 如果 bricks<0，则砖块数量不够，需要将砖块替换成梯子。当 bricks<0、ladders>0 且优先队列不为空时，从优先
    ///   队列中取出最大值加到 bricks 并将 ladders 减 1，表示使用砖块的最大高度差位置替换成梯子，重复该操作直到 
    ///   bricks≥0、ladders=0 或优先队列为空，即当砖块数量足够、没有剩余梯子或没有更多的砖块可以替换成梯子时结束该操作
    /// - 如果此时 bricks≥0，则可以到达下标 i，将答案更新为 i。如果此时 bricks<0，则不能到达下标 i 和更大的下标，结束遍历。
    /// 遍历结束时，即可得到可以到达的最远建筑物的下标。
    /// </summary>
    /// <param name="heights"></param>
    /// <param name="bricks"></param>
    /// <param name="ladders"></param>
    /// <returns></returns>
    public int FurthestBuilding(int[] heights, int bricks, int ladders)
    {
        PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
        int furthest = 0;
        int length = heights.Length;

        for(int i = 1; i < length && bricks >= 0; i++)
        {
            int difference = heights[i] - heights[i - 1];
            if(difference > 0)
            {
                pq.Enqueue(difference, -difference);
                bricks -= difference;

                while(bricks < 0 && ladders > 0 && pq.Count > 0)
                {
                    bricks += pq.Dequeue();
                    ladders--;
                }
            }

            if(bricks >= 0)
            {
                furthest = i;
            }
        }

        return furthest;
    }
}
