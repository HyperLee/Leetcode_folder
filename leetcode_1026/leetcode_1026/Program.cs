namespace leetcode_1026;

class Program
{
    /// <summary>
    /// 1026. Maximum Difference Between Node and Ancestor
    /// https://leetcode.com/problems/maximum-difference-between-node-and-ancestor/description/?envType=daily-question&envId=2024-01-11
    /// Given the root of a binary tree, 
    /// find the maximum value v for which there exist different nodes a and b 
    /// where v = |a.val - b.val| and a is an ancestor of b.
    /// A node a is an ancestor of b if either: any child of a is equal to b 
    /// or any child of a is an ancestor of b.
    /// 1026. 节点与其祖先之间的最大差值
    /// https://leetcode.cn/problems/maximum-difference-between-node-and-ancestor/description/
    /// 給定一棵二元樹的根節點 root，找出最大值 v，
    /// 使得存在兩個不同的節點 a 與 b，且 v = |a.val - b.val|，其
    /// 中 a 是 b 的祖先節點。
    /// 若符合以下任一條件，則節點 a 視為節點 b 的祖先：
    /// a 的任一子節點等於 b，或 a 的任一子節點是 b 的祖先。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
