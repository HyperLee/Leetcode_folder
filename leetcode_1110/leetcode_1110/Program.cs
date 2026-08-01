namespace leetcode_1110
{
    internal class Program
    {
        /// <summary>
        /// 表示刪點成林題目的二元樹節點，保存整數值及可為空的左右子節點。
        /// 建立節點時可只提供節點值，也可同時指定左右子樹；輸出物件可作為輸入樹或森林根節點。
        /// </summary>
        public class TreeNode
        {
            public int val;
            public TreeNode? left;
            public TreeNode? right;

            /// <summary>
            /// 建立具有指定值及左右子節點的二元樹節點。
            /// 左右子節點可為 <see langword="null"/>；輸出為可再連接其他節點的新節點。
            /// </summary>
            /// <param name="val">節點保存的整數值。</param>
            /// <param name="left">左子節點；沒有左子節點時為 <see langword="null"/>。</param>
            /// <param name="right">右子節點；沒有右子節點時為 <see langword="null"/>。</param>
            public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }

        /// <summary>
        /// 1110. Delete Nodes And Return Forest
        /// https://leetcode.com/problems/delete-nodes-and-return-forest/description/?envType=daily-question&envId=2024-07-17
        /// 1110. 删点成林
        /// https://leetcode.cn/problems/delete-nodes-and-return-forest/description/
        /// </summary>
        /// <param name="args"></param>
        /// <remarks>
        /// 執行九組固定案例，分別驗證三種刪點成林解法，並以結束碼表示所有檢查是否通過。
        /// </remarks>
        static void Main(string[] args)
        {
            Environment.ExitCode = RunSamples() ? 0 : 1;
        }

        /// <summary>
        /// 建立並執行固定的二元樹案例，逐一驗證三種刪點成林解法。
        /// 此方法不接受外部輸入；輸出每個案例的預期森林、實際森林與 PASS/FAIL，
        /// 並回傳全部解法是否通過所有案例。
        /// </summary>
        /// <returns>全部 27 項檢查通過時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
        private static bool RunSamples()
        {
            (string Name, Func<TreeNode?> BuildRoot, int[] ToDelete, string[] Expected, string InputDisplay)[] cases =
            [
                (
                    "官方範例一",
                    () => BuildTree([1, 2, 3, 4, 5, 6, 7]),
                    [3, 5],
                    ["[1,2,null,4]", "[6]", "[7]"],
                    "root = [1,2,3,4,5,6,7], to_delete = [3,5]"),
                (
                    "官方範例二",
                    () => BuildTree([1, 2, 4, null, 3]),
                    [3],
                    ["[1,2,4]"],
                    "root = [1,2,4,null,3], to_delete = [3]"),
                (
                    "刪除原始根節點",
                    () => BuildTree([1, 2, 3]),
                    [1],
                    ["[2]", "[3]"],
                    "root = [1,2,3], to_delete = [1]"),
                (
                    "不刪除任何節點",
                    () => BuildTree([1, 2, 3]),
                    [],
                    ["[1,2,3]"],
                    "root = [1,2,3], to_delete = []"),
                (
                    "刪除全部節點",
                    () => BuildTree([1, 2, 3]),
                    [1, 2, 3],
                    [],
                    "root = [1,2,3], to_delete = [1,2,3]"),
                (
                    "單節點保留",
                    () => BuildTree([1]),
                    [],
                    ["[1]"],
                    "root = [1], to_delete = []"),
                (
                    "單節點刪除",
                    () => BuildTree([1]),
                    [1],
                    [],
                    "root = [1], to_delete = [1]"),
                (
                    "空樹",
                    () => null,
                    [],
                    [],
                    "root = [], to_delete = []"),
                (
                    "1000 節點右斜樹上界",
                    () => BuildRightSkewedTree(1000),
                    CreateDeleteValues(999),
                    ["[1000]"],
                    "root = 1..1000 的右斜樹, to_delete = [1..999]")
            ];

            int passedChecks = 0;
            int totalChecks = 0;

            foreach ((string name, Func<TreeNode?> buildRoot, int[] toDelete, string[] expected, string inputDisplay) in cases)
            {
                (int passed, int total) = RunTestCase(name, buildRoot, toDelete, expected, inputDisplay);
                passedChecks += passed;
                totalChecks += total;
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過。");
            return passedChecks == totalChecks;
        }

        /// <summary>
        /// 對同一案例建立三棵互不共用節點的樹，執行三種解法後比較正規化森林。
        /// 輸入須提供可重複建立相同合法二元樹的 factory；輸出三種實際結果與通過狀態，
        /// 並回傳本案例的通過數及檢查總數。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="buildRoot">每次呼叫都建立全新二元樹的 factory。</param>
        /// <param name="toDelete">符合題目限制且值不重複的刪除清單。</param>
        /// <param name="expected">已正規化且不依賴森林順序的預期樹序列。</param>
        /// <param name="inputDisplay">適合顯示於主控台的輸入摘要。</param>
        /// <returns>本案例通過的解法數與固定檢查總數 3。</returns>
        private static (int Passed, int Total) RunTestCase(
            string name,
            Func<TreeNode?> buildRoot,
            int[] toDelete,
            string[] expected,
            string inputDisplay)
        {
            string expectedDisplay = FormatForest(expected);
            string actual1 = SerializeForest(DelNodes(buildRoot(), toDelete));
            string actual2 = SerializeForest(DelNodes2(buildRoot(), toDelete));
            string actual3 = SerializeForest(DelNodes3(buildRoot(), toDelete));
            bool passed1 = actual1 == expectedDisplay;
            bool passed2 = actual2 == expectedDisplay;
            bool passed3 = actual3 == expectedDisplay;

            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"輸入：{inputDisplay}");
            Console.WriteLine($"預期：{expectedDisplay}");
            Console.WriteLine($"解法一（後序遞迴 DFS）實際：{actual1} => {(passed1 ? "PASS" : "FAIL")}");
            Console.WriteLine($"解法二（前序遞迴 DFS）實際：{actual2} => {(passed2 ? "PASS" : "FAIL")}");
            Console.WriteLine($"解法三（迭代 DFS）實際：{actual3} => {(passed3 ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return ((passed1 ? 1 : 0) + (passed2 ? 1 : 0) + (passed3 ? 1 : 0), 3);
        }

        /// <summary>
        /// 依 LeetCode 的層序格式建立二元樹，陣列中的 <see langword="null"/> 代表缺少子節點。
        /// 輸入可為空陣列或首項為 <see langword="null"/>；輸出對應的樹根，空樹則輸出
        /// <see langword="null"/>。
        /// </summary>
        /// <param name="values">以層序排列的 nullable 節點值。</param>
        /// <returns>依輸入建立的二元樹根節點；輸入代表空樹時為 <see langword="null"/>。</returns>
        private static TreeNode? BuildTree(int?[] values)
        {
            if (values.Length == 0 || values[0] is null)
            {
                return null;
            }

            TreeNode root = new TreeNode(values[0]!.Value);
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            int index = 1;

            while (queue.Count > 0 && index < values.Length)
            {
                TreeNode node = queue.Dequeue();

                if (values[index] is int leftValue)
                {
                    node.left = new TreeNode(leftValue);
                    queue.Enqueue(node.left);
                }

                index++;
                if (index < values.Length && values[index] is int rightValue)
                {
                    node.right = new TreeNode(rightValue);
                    queue.Enqueue(node.right);
                }

                index++;
            }

            return root;
        }

        /// <summary>
        /// 建立節點值從 1 遞增且每個節點只有右子節點的鏈狀二元樹。
        /// 輸入節點數須為正整數；輸出包含指定數量節點的樹根，用於驗證題目數量上界。
        /// </summary>
        /// <param name="nodeCount">要建立的節點數量。</param>
        /// <returns>具有 <paramref name="nodeCount"/> 個節點的右斜樹根節點。</returns>
        private static TreeNode BuildRightSkewedTree(int nodeCount)
        {
            TreeNode root = new TreeNode(1);
            TreeNode current = root;

            for (int value = 2; value <= nodeCount; value++)
            {
                current.right = new TreeNode(value);
                current = current.right;
            }

            return root;
        }

        /// <summary>
        /// 建立從 1 到指定上限的連續刪除值。
        /// 輸入須為非負整數；輸出長度等於上限且所有值皆不重複的整數陣列。
        /// </summary>
        /// <param name="maximumValue">要包含的最大刪除值。</param>
        /// <returns>依序包含 1 到 <paramref name="maximumValue"/> 的陣列。</returns>
        private static int[] CreateDeleteValues(int maximumValue)
        {
            int[] values = new int[maximumValue];

            for (int index = 0; index < values.Length; index++)
            {
                values[index] = index + 1;
            }

            return values;
        }

        /// <summary>
        /// 將森林中的每棵樹轉為層序字串後排序，消除題目允許的森林回傳順序差異。
        /// 輸入可為空森林；輸出保留每棵樹的節點值與結構，可直接與預期結果比較。
        /// </summary>
        /// <param name="forest">要正規化的森林根節點集合。</param>
        /// <returns>排序後以外層中括號包住的森林字串。</returns>
        private static string SerializeForest(IEnumerable<TreeNode> forest)
        {
            string[] trees = forest.Select(SerializeTree).OrderBy(value => value, StringComparer.Ordinal).ToArray();
            return FormatForest(trees);
        }

        /// <summary>
        /// 使用層序走訪序列化單棵二元樹，並移除不影響結構判讀的尾端 <c>null</c>。
        /// 輸入須為森林中的非空根節點；輸出為 LeetCode 風格的中括號字串。
        /// </summary>
        /// <param name="root">要序列化的非空樹根。</param>
        /// <returns>包含節點值及必要 <c>null</c> 位置的層序字串。</returns>
        private static string SerializeTree(TreeNode root)
        {
            List<string> values = new List<string>();
            Queue<TreeNode?> queue = new Queue<TreeNode?>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                TreeNode? node = queue.Dequeue();
                if (node == null)
                {
                    values.Add("null");
                    continue;
                }

                values.Add(node.val.ToString());
                queue.Enqueue(node.left);
                queue.Enqueue(node.right);
            }

            while (values.Count > 0 && values[^1] == "null")
            {
                values.RemoveAt(values.Count - 1);
            }

            return $"[{string.Join(',', values)}]";
        }

        /// <summary>
        /// 將已正規化並排序的樹序列組合成森林顯示格式。
        /// 輸入可為空集合；輸出為雙層中括號格式，空集合輸出 <c>[]</c>。
        /// </summary>
        /// <param name="trees">每一項代表一棵樹的層序字串。</param>
        /// <returns>可供主控台顯示及相等比較的森林字串。</returns>
        private static string FormatForest(IEnumerable<string> trees)
        {
            return $"[{string.Join(',', trees.OrderBy(value => value, StringComparer.Ordinal))}]";
        }


        /// <summary>
        /// 使用後序遞迴 DFS 刪除指定節點並回傳剩餘森林。
        /// 解法先處理左右子樹，再以遞迴回傳值重新接回未刪除的子樹；若父節點被刪除，
        /// 其未刪除子節點會以候選根身分加入森林。輸入可為空樹，刪除值須符合題目限制。
        /// </summary>
        /// <param name="root">要原地斷開節點的二元樹根；空樹可為 <see langword="null"/>。</param>
        /// <param name="toDelete">值不重複的刪除清單。</param>
        /// <returns>刪除完成後所有剩餘樹的根節點，順序不具語意。</returns>
        /// <remarks>
        /// 時間複雜度為 O(n + d)，額外空間為 O(d + h)；n 是節點數、d 是刪除值數量、h 是樹高。
        /// 此方法會原地修改輸入樹的左右連結。
        /// 參考：https://leetcode.cn/problems/delete-nodes-and-return-forest/solutions/2286145/shan-dian-cheng-lin-by-leetcode-solution-gy95/
        /// </remarks>
        public static IList<TreeNode> DelNodes(TreeNode? root, int[] toDelete)
        {
            ISet<int> toDeleteSet = new HashSet<int>(toDelete);
            IList<TreeNode> roots = new List<TreeNode>();
            DFS(root, true, toDeleteSet, roots);
            return roots;
        }

        /// <summary>
        /// 後序處理指定子樹，回傳刪除完成後應接回父節點的子樹根。
        /// 輸入節點可為空，<paramref name="isRoot"/> 表示此節點沒有保留中的父節點；
        /// 輸出未刪除的目前節點，若目前節點應刪除則輸出 <see langword="null"/>。
        /// </summary>
        /// <param name="node">目前處理的子樹根節點。</param>
        /// <param name="isRoot">目前節點是否為原始根或被刪除節點的直接子節點。</param>
        /// <param name="toDeleteSet">用於常數平均時間查詢的刪除值集合。</param>
        /// <param name="roots">累積所有未刪除森林根節點的集合。</param>
        /// <returns>父節點應保留的子樹根；目前節點被刪除或為空時為 <see langword="null"/>。</returns>
        public static TreeNode? DFS(
            TreeNode? node,
            bool isRoot,
            ISet<int> toDeleteSet,
            IList<TreeNode> roots)
        {
            if (node is null)
            {
                return null;
            }

            bool deleted = toDeleteSet.Contains(node.val);

            // 後序回傳值直接取代左右連結，使被刪除的子節點從保留中的父節點斷開。
            node.left = DFS(node.left, deleted, toDeleteSet, roots);
            node.right = DFS(node.right, deleted, toDeleteSet, roots);

            if (isRoot && !deleted)
            {
                roots.Add(node);
            }

            return deleted ? null : node;
        }

        /// <summary>
        /// 使用帶父節點資訊的前序遞迴 DFS 刪除指定節點並回傳剩餘森林。
        /// 解法在進入節點時立即決定是否加入森林及是否從父節點斷開，再將被刪除節點的
        /// 左右子節點視為新的候選根。輸入可為空樹，刪除值須符合題目限制。
        /// </summary>
        /// <param name="root">要原地斷開節點的二元樹根；空樹可為 <see langword="null"/>。</param>
        /// <param name="toDelete">值不重複的刪除清單。</param>
        /// <returns>刪除完成後所有剩餘樹的根節點，順序不具語意。</returns>
        /// <remarks>
        /// 時間複雜度為 O(n + d)，額外空間為 O(d + h)，且會原地修改輸入樹的左右連結。
        /// </remarks>
        public static IList<TreeNode> DelNodes2(TreeNode? root, int[] toDelete)
        {
            ISet<int> toDeleteSet = new HashSet<int>(toDelete);
            IList<TreeNode> roots = new List<TreeNode>();
            DeletePreOrder(root, null, false, true, toDeleteSet, roots);
            return roots;
        }

        /// <summary>
        /// 前序走訪目前節點，依刪除狀態更新父節點連結並繼續處理原本的左右子樹。
        /// 輸入節點與父節點皆可為空；輸出直接累積於森林集合，沒有獨立回傳值。
        /// </summary>
        /// <param name="node">目前走訪的節點。</param>
        /// <param name="parent">目前節點在原樹中的父節點；沒有父節點時為 <see langword="null"/>。</param>
        /// <param name="isLeftChild">目前節點是否為父節點的左子節點。</param>
        /// <param name="isRoot">目前節點是否沒有保留中的父節點。</param>
        /// <param name="toDeleteSet">用於查詢刪除狀態的集合。</param>
        /// <param name="roots">累積所有未刪除森林根節點的集合。</param>
        private static void DeletePreOrder(
            TreeNode? node,
            TreeNode? parent,
            bool isLeftChild,
            bool isRoot,
            ISet<int> toDeleteSet,
            IList<TreeNode> roots)
        {
            if (node is null)
            {
                return;
            }

            bool deleted = toDeleteSet.Contains(node.val);
            TreeNode? left = node.left;
            TreeNode? right = node.right;

            if (isRoot && !deleted)
            {
                roots.Add(node);
            }

            if (deleted)
            {
                // 前序版本在遞迴子樹前斷鏈，因此必須先保存原本的左右子節點。
                DisconnectFromParent(parent, node, isLeftChild);
            }

            DeletePreOrder(left, deleted ? null : node, true, deleted, toDeleteSet, roots);
            DeletePreOrder(right, deleted ? null : node, false, deleted, toDeleteSet, roots);
        }

        /// <summary>
        /// 使用顯式堆疊的迭代 DFS 刪除指定節點並回傳剩餘森林。
        /// 每個堆疊項目保存節點、父節點、左右位置與候選根狀態，模擬前序遞迴但不使用呼叫堆疊。
        /// 輸入可為空樹，刪除值須符合題目限制。
        /// </summary>
        /// <param name="root">要原地斷開節點的二元樹根；空樹可為 <see langword="null"/>。</param>
        /// <param name="toDelete">值不重複的刪除清單。</param>
        /// <returns>刪除完成後所有剩餘樹的根節點，順序不具語意。</returns>
        /// <remarks>
        /// 時間複雜度為 O(n + d)，額外空間為 O(n + d)，且會原地修改輸入樹的左右連結。
        /// </remarks>
        public static IList<TreeNode> DelNodes3(TreeNode? root, int[] toDelete)
        {
            IList<TreeNode> roots = new List<TreeNode>();
            if (root is null)
            {
                return roots;
            }

            ISet<int> toDeleteSet = new HashSet<int>(toDelete);
            Stack<(TreeNode Node, TreeNode? Parent, bool IsLeftChild, bool IsRoot)> stack = new();
            stack.Push((root, null, false, true));

            while (stack.Count > 0)
            {
                (TreeNode node, TreeNode? parent, bool isLeftChild, bool isRoot) = stack.Pop();
                bool deleted = toDeleteSet.Contains(node.val);
                TreeNode? left = node.left;
                TreeNode? right = node.right;

                if (isRoot && !deleted)
                {
                    roots.Add(node);
                }

                if (deleted)
                {
                    DisconnectFromParent(parent, node, isLeftChild);
                }

                // 先壓右再壓左，讓走訪順序與一般前序 DFS 一致；答案比較不依賴森林順序。
                if (right is not null)
                {
                    stack.Push((right, deleted ? null : node, false, deleted));
                }

                if (left is not null)
                {
                    stack.Push((left, deleted ? null : node, true, deleted));
                }
            }

            return roots;
        }

        /// <summary>
        /// 將目前節點從父節點斷開；若目前節點沒有父節點，則清空自身的左右連結。
        /// 輸入節點須為確定刪除的節點；此方法沒有回傳值，結果直接反映在相關節點連結上。
        /// </summary>
        /// <param name="parent">被刪除節點的保留中父節點；原始根或父節點已刪除時為 <see langword="null"/>。</param>
        /// <param name="node">要斷開的節點。</param>
        /// <param name="isLeftChild">目前節點是否位於父節點的左側。</param>
        private static void DisconnectFromParent(TreeNode? parent, TreeNode node, bool isLeftChild)
        {
            if (parent is not null)
            {
                if (isLeftChild)
                {
                    parent.left = null;
                }
                else
                {
                    parent.right = null;
                }
            }

            node.left = null;
            node.right = null;
        }

    }
}