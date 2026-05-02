namespace leetcode_655;

class Program
{
    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;

        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
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
    /// <param name="args">命令列參數。</param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試資料 1：
        //   1
        //  /
        // 2
        var sample1 = new TreeNode(1, new TreeNode(2));

        // 測試資料 2：
        //     1
        //    / \
        //   2   3
        //    \
        //     4
        var sample2 = new TreeNode(
            1,
            new TreeNode(2, null, new TreeNode(4)),
            new TreeNode(3));

        // 測試資料 3：
        //       1
        //      / \
        //     2   3
        //    /   / \
        //   4   5   6
        var sample3 = new TreeNode(
            1,
            new TreeNode(2, new TreeNode(4)),
            new TreeNode(3, new TreeNode(5), new TreeNode(6)));

        program.RunComparisonTestCase("Sample 1", sample1);
        program.RunComparisonTestCase("Sample 2", sample2);
        program.RunComparisonTestCase("Sample 3", sample3);
    }

    /// <summary>
    /// 使用 DFS 解出題目要求的字串矩陣。
    /// 解題核心分成三步：
    /// 1. 先遞迴求出樹高，注意本題高度從 0 開始，因此葉節點高度是 0。
    /// 2. 依照公式建立 rows = height + 1、cols = 2^(height + 1) - 1 的答案矩陣。
    /// 3. 再以 DFS 將每個節點放到目前區間的正中央，並把左右剩餘空間遞迴分配給左右子樹。
    /// </summary>
    /// <param name="root">二元樹根節點。</param>
    /// <returns>符合題意的二維字串矩陣。</returns>
    public IList<IList<string>> PrintTree(TreeNode? root)
    {
        if (root is null)
        {
            return new List<IList<string>>();
        }

        // 本題高度從 0 開始，葉節點高度為 0。
        int height = GetHeight(root);
        int rows = height + 1;
        int cols = (1 << (height + 1)) - 1;

        var result = new List<IList<string>>(rows);

        // 先建立一個 filled with "" 的 m x n 矩陣。
        for (int row = 0; row < rows; row++)
        {
            var currentRow = new List<string>(cols);
            for (int col = 0; col < cols; col++)
            {
                currentRow.Add(string.Empty);
            }

            result.Add(currentRow);
        }

        // 從整個欄位區間開始，把根節點放在第一列正中央。
        FillMatrix(result, root, row: 0, left: 0, right: cols - 1);
        return result;
    }

    /// <summary>
    /// 使用 BFS 解出題目要求的字串矩陣。
    /// 做法分成兩個階段：
    /// 1. 先以 BFS 計算整棵樹的高度。
    /// 2. 再以 BFS 逐層放置節點，並記錄每個節點應該落在哪一列與哪一欄。
    /// </summary>
    /// <param name="root">二元樹根節點。</param>
    /// <returns>符合題意的二維字串矩陣。</returns>
    public IList<IList<string>> PrintTreeBfs(TreeNode? root)
    {
        if (root is null)
        {
            return new List<IList<string>>();
        }

        // 先以 BFS 算出高度，後續才能依公式建立答案矩陣。
        int height = GetHeightBfs(root);
        int rows = height + 1;
        int cols = (1 << (height + 1)) - 1;

        var result = new List<IList<string>>(rows);

        // 先建立一個全部填入空字串的 m x n 矩陣。
        for (int row = 0; row < rows; row++)
        {
            var currentRow = new List<string>(cols);
            for (int col = 0; col < cols; col++)
            {
                currentRow.Add(string.Empty);
            }

            result.Add(currentRow);
        }

        // 佇列中同時保存節點本身，以及它在答案矩陣中的座標。
        var queue = new Queue<(TreeNode node, int row, int col)>();
        queue.Enqueue((root, 0, (cols - 1) / 2));

        while (queue.Count > 0)
        {
            (TreeNode node, int row, int col) = queue.Dequeue();

            // 取出節點後，直接把值放入對應位置。
            result[row][col] = node.val.ToString();

            // 左右子節點都在下一列，欄位位移量依題目公式為 2^(height - row - 1)。
            if (node.left is not null || node.right is not null)
            {
                int offset = 1 << (height - row - 1);

                // 左子節點放到目前欄位往左 offset 的位置。
                if (node.left is not null)
                {
                    queue.Enqueue((node.left, row + 1, col - offset));
                }

                // 右子節點放到目前欄位往右 offset 的位置。
                if (node.right is not null)
                {
                    queue.Enqueue((node.right, row + 1, col + offset));
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 計算二元樹高度，並以「邊數」作為高度定義。
    /// 也就是說：
    /// 1. 空節點高度視為 -1。
    /// 2. 葉節點高度為 0。
    /// 這樣可直接對應題目的 rows = height + 1 與 cols = 2^(height + 1) - 1。
    /// </summary>
    /// <param name="node">目前遞迴到的節點。</param>
    /// <returns>目前子樹的高度。</returns>
    public int GetHeight(TreeNode? node)
    {
        if (node is null)
        {
            return -1;
        }

        // 左右子樹各自求高，取較大值再加上目前這一層。
        int leftHeight = GetHeight(node.left);
        int rightHeight = GetHeight(node.right);
        return Math.Max(leftHeight, rightHeight) + 1;
    }

    /// <summary>
    /// 使用 BFS 逐層計算二元樹高度。
    /// 每完成一層就把高度加一，因此空樹高度為 -1，葉節點高度為 0。
    /// </summary>
    /// <param name="root">二元樹根節點。</param>
    /// <returns>目前二元樹的高度。</returns>
    public int GetHeightBfs(TreeNode? root)
    {
        if (root is null)
        {
            return -1;
        }

        int height = -1;
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            int levelCount = queue.Count;
            height++;

            // 每次迴圈完整處理同一層的所有節點。
            for (int index = 0; index < levelCount; index++)
            {
                TreeNode node = queue.Dequeue();

                // 把下一層存在的節點加入佇列，供下一輪處理。
                if (node.left is not null)
                {
                    queue.Enqueue(node.left);
                }

                if (node.right is not null)
                {
                    queue.Enqueue(node.right);
                }
            }
        }

        return height;
    }

    /// <summary>
    /// 以 DFS 將節點值填入答案矩陣。
    /// 每次遞迴都把目前節點放在 [left, right] 區間的中點，
    /// 之後把左半部區間交給左子樹、右半部區間交給右子樹。
    /// </summary>
    /// <param name="matrix">答案矩陣。</param>
    /// <param name="node">目前要放入矩陣的節點。</param>
    /// <param name="row">目前所在列。</param>
    /// <param name="left">目前可用區間的左邊界。</param>
    /// <param name="right">目前可用區間的右邊界。</param>
    public void FillMatrix(IList<IList<string>> matrix, TreeNode? node, int row, int left, int right)
    {
        if (node is null || left > right)
        {
            return;
        }

        // 目前區間的中點，就是這個節點應該放置的位置。
        int mid = left + (right - left) / 2;
        matrix[row][mid] = node.val.ToString();

        // 左子樹使用左半部空間 [left, mid - 1]。
        FillMatrix(matrix, node.left, row + 1, left, mid - 1);

        // 右子樹使用右半部空間 [mid + 1, right]。
        FillMatrix(matrix, node.right, row + 1, mid + 1, right);
    }

    /// <summary>
    /// 同時執行 DFS 解法一與 BFS 解法二，並輸出兩者結果供比對。
    /// </summary>
    /// <param name="title">測試案例名稱。</param>
    /// <param name="root">測試用二元樹根節點。</param>
    public void RunComparisonTestCase(string title, TreeNode? root)
    {
        Console.WriteLine(title);

        Console.WriteLine("DFS:");
        IList<IList<string>> dfsResult = PrintTree(root);
        PrintMatrix(dfsResult);

        Console.WriteLine("BFS:");
        IList<IList<string>> bfsResult = PrintTreeBfs(root);
        PrintMatrix(bfsResult);

        Console.WriteLine($"Same Result: {AreMatricesEqual(dfsResult, bfsResult)}");
        Console.WriteLine();
    }

    /// <summary>
    /// 比對兩個二維字串矩陣是否完全相同，方便驗證兩種解法輸出一致。
    /// </summary>
    /// <param name="first">第一個矩陣。</param>
    /// <param name="second">第二個矩陣。</param>
    /// <returns>若矩陣內容一致則回傳 true，否則回傳 false。</returns>
    public bool AreMatricesEqual(IList<IList<string>> first, IList<IList<string>> second)
    {
        if (first.Count != second.Count)
        {
            return false;
        }

        for (int row = 0; row < first.Count; row++)
        {
            if (first[row].Count != second[row].Count)
            {
                return false;
            }

            for (int col = 0; col < first[row].Count; col++)
            {
                if (!string.Equals(first[row][col], second[row][col], StringComparison.Ordinal))
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// 執行單筆測試資料，並把題目要求的矩陣格式印出來，方便在 main 中快速驗證。
    /// </summary>
    /// <param name="title">測試案例名稱。</param>
    /// <param name="root">測試用二元樹根節點。</param>
    public void RunTestCase(string title, TreeNode? root)
    {
        Console.WriteLine(title);
        PrintMatrix(PrintTree(root));
        Console.WriteLine();
    }

    /// <summary>
    /// 將二維字串矩陣輸出成容易閱讀的形式。
    /// </summary>
    /// <param name="matrix">要輸出的矩陣。</param>
    public void PrintMatrix(IList<IList<string>> matrix)
    {
        foreach (IList<string> row in matrix)
        {
            Console.WriteLine($"[{string.Join(", ", row.Select(value => $"\"{value}\""))}]");
        }
    }
}
