namespace leetcode_655;

class Program
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }    

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

    /// <summary>
    /// DFS 深度優先搜尋
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public IList<IList<string>> PrintTree(TreeNode root)
    {
        int height = CalDepth(root);
        // 行數高從 0 開始, 所以要 +1
        int m = height + 1;
        // 列數 n = 2^(height + 1) - 1
        int n = (1 << (height + 1)) - 1;

        IList<IList<string>> res = new List<IList<string>>();
        // 先創建一顆空樹, value 是空字串
        for (int i = 0; i < m; i++)
        {
            IList<string> row = new List<string>();
            for (int j = 0; j < n; j++)
            {
                row.Add("");
            }
            res.Add(row);
        }
        DFS(res, root, 0, (n - 1) / 2, height);
        return res;
    }

    /// <summary>
    /// Depth
    /// 一開始先透過DFS, 取出高度
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public int CalDepth(TreeNode root)
    {
        int h = 0;
        if(root.left is not null)
        {
            h = Math.Max(h, CalDepth(root.left));
        }
        if(root.right is not null)
        {
            h = Math.Max(h, CalDepth(root.right));
        }
        return h + 1;
    }

    /// <summary>
    /// DFS
    /// 中左右
    /// 透過 深度優先 來把資料塞進去
    /// 依據題目規則來塞入
    /// </summary>
    /// <param name="res"></param>
    /// <param name="root"></param>
    /// <param name="r"></param>
    /// <param name="c"></param>
    /// <param name="height"></param>
    public void DFS(IList<IList<string>> res, TreeNode root, int r, int c, int height)
    {
        // root 在高度為0(最上層)的正中央
        res[r][c] = root.val.ToString();

        if(root.left is not null)
        {
            // 左子樹公式   
            DFS(res, root.left, r + 1, c - (1 << (height - r - 1)), height);
        }

        if(root.right is not null)
        {
            // 右子樹公式
            DFS(res, root.right, r + 1, c + (1 << (height - r - 1)), height);
        }
    }
}
