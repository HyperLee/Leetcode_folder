namespace leetcode_655;

class Program
{
    /// <summary>
    /// 655. Print Binary Tree
    /// https://leetcode.com/problems/print-binary-tree/description/
    ///
    /// English:
    /// Given the root of a binary tree, construct a 0-indexed m x n string matrix res that represents a formatted layout of the tree.
    /// The formatted layout matrix should be constructed using the following rules:
    /// 1. The height of the tree is height and the number of rows m should be equal to height + 1.
    /// 2. The number of columns n should be equal to 2^(height + 1) - 1.
    /// 3. Place the root node in the middle of the top row (more formally, at location res[0][(n - 1) / 2]).
    /// 4. For each node that has been placed in the matrix at position res[r][c], place its left child at
    ///    res[r + 1][c - 2^(height - r - 1)] and its right child at res[r + 1][c + 2^(height - r - 1)].
    /// 5. Continue this process until all the nodes in the tree have been placed.
    /// 6. Any empty cells should contain the empty string "".
    /// Return the constructed matrix res.
    ///
    /// 655. 输出二叉树
    /// https://leetcode.cn/problems/print-binary-tree/description/
    ///
    /// 繁體中文：
    /// 給定一棵二元樹的根節點，請建構一個 0-indexed 的 m x n 字串矩陣 res，用來表示該二元樹的格式化版面配置。
    /// 這個格式化矩陣需要遵守以下規則：
    /// 1. 樹的高度為 height，列數 m 應等於 height + 1。
    /// 2. 欄數 n 應等於 2^(height + 1) - 1。
    /// 3. 將根節點放在第一列正中央，也就是 res[0][(n - 1) / 2]。
    /// 4. 若某節點位於 res[r][c]，則其左子節點放在
    ///    res[r + 1][c - 2^(height - r - 1)]，右子節點放在 res[r + 1][c + 2^(height - r - 1)]。
    /// 5. 持續此流程，直到所有節點都被放入矩陣中。
    /// 6. 其餘空白位置都填入空字串 ""。
    /// 回傳建構完成的矩陣 res。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
