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
}
