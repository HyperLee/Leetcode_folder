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
    /// <remarks>
    /// 執行五組固定範例，並比較兩種 PriorityQueue 貪心解法的實際結果與預期結果。
    /// </remarks>
    /// <param name="args">命令列參數；此範例程式不使用。</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        (int[] Heights, int Bricks, int Ladders, int Expected)[] examples =
        [
            ([4, 2, 7, 6, 9, 14, 12], 5, 1, 4),
            ([4, 12, 2, 7, 3, 18, 20, 3, 19], 10, 2, 7),
            ([14, 3, 19, 3], 17, 0, 3),
            ([9, 7, 5, 3], 0, 0, 3),
            ([1, 2], 0, 0, 0)
        ];

        for (int i = 0; i < examples.Length; i++)
        {
            (int[] heights, int bricks, int ladders, int expected) = examples[i];
            int maxHeapResult = solution.FurthestBuilding(heights, bricks, ladders);
            int minHeapResult = solution.FurthestBuilding2(heights, bricks, ladders);

            Console.WriteLine($"Example {i + 1}: heights = [{string.Join(", ", heights)}], bricks = {bricks}, ladders = {ladders}, expected = {expected}");
            Console.WriteLine($"  FurthestBuilding:  {maxHeapResult} ({(maxHeapResult == expected ? "PASS" : "FAIL")})");
            Console.WriteLine($"  FurthestBuilding2: {minHeapResult} ({(minHeapResult == expected ? "PASS" : "FAIL")})");
        }
    }

    /// <summary>
    /// 使用最大堆貪心配置磚塊與梯子，以找出能到達的最遠建築索引。
    /// 每次遇到向上的高度差時先使用磚塊；若磚塊不足，就從最大堆取出截至目前最大的高度差，
    /// 將該次磚塊支出改由梯子負擔，藉此取回最多磚塊並延伸可行距離。
    /// 輸入須符合題目限制：<paramref name="heights"/> 至少包含一棟建築，
    /// <paramref name="bricks"/> 與 <paramref name="ladders"/> 皆不可為負數。
    /// </summary>
    /// <param name="heights">依行進順序排列的建築高度。</param>
    /// <param name="bricks">可使用的磚塊數量。</param>
    /// <param name="ladders">可使用的梯子數量。</param>
    /// <returns>以最佳方式配置資源後能到達的最遠建築索引。</returns>
    public int FurthestBuilding(int[] heights, int bricks, int ladders)
    {
        PriorityQueue<int, int> brickClimbs = new PriorityQueue<int, int>();
        int furthest = 0;

        for (int i = 1; i < heights.Length && bricks >= 0; i++)
        {
            int difference = heights[i] - heights[i - 1];
            if (difference > 0)
            {
                // PriorityQueue 預設是最小堆；以負數作 priority，讓最大高度差優先出隊。
                brickClimbs.Enqueue(difference, -difference);
                bricks -= difference;

                // 磚塊不足時，用梯子替換最大筆磚塊支出，能取回最多可用磚塊。
                while (bricks < 0 && ladders > 0 && brickClimbs.Count > 0)
                {
                    bricks += brickClimbs.Dequeue();
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

    /// <summary>
    /// 使用最小堆貪心配置磚塊與梯子，以找出能到達的最遠建築索引。
    /// 每次遇到向上的高度差時，先暫定使用梯子；當高度差數量超過梯子數量，
    /// 就從最小堆取出目前最小的高度差改用磚塊，讓梯子保留給較大的高度差。
    /// 輸入須符合題目限制：<paramref name="heights"/> 至少包含一棟建築，
    /// <paramref name="bricks"/> 與 <paramref name="ladders"/> 皆不可為負數。
    /// </summary>
    /// <param name="heights">依行進順序排列的建築高度。</param>
    /// <param name="bricks">可使用的磚塊數量。</param>
    /// <param name="ladders">可使用的梯子數量。</param>
    /// <returns>以最佳方式配置資源後能到達的最遠建築索引。</returns>
    public int FurthestBuilding2(int[] heights, int bricks, int ladders)
    {
        PriorityQueue<int, int> ladderClimbs = new PriorityQueue<int, int>();

        for (int i = 1; i < heights.Length; i++)
        {
            int difference = heights[i] - heights[i - 1];
            if (difference <= 0)
            {
                continue;
            }

            // 先暫定所有正高度差都使用梯子，最小堆可快速找出其中最小的一次攀升。
            ladderClimbs.Enqueue(difference, difference);

            if (ladderClimbs.Count > ladders)
            {
                // 梯子名額超過上限時，將目前最小高度差改用磚塊。
                bricks -= ladderClimbs.Dequeue();
            }

            if (bricks < 0)
            {
                return i - 1;
            }
        }

        return heights.Length - 1;
    }
}