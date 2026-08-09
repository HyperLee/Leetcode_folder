using System.Linq;

namespace leetcode_2322;

class Program
{
    /// <summary>
    /// <para>
    /// 2322. Minimum Score After Removals on a Tree
    /// https://leetcode.com/problems/minimum-score-after-removals-on-a-tree/description/
    ///
    /// An undirected connected tree has n nodes labeled 0 through n - 1 and n - 1 edges. nums[i] is node i's value and edges[i] = [a_i,b_i] is an edge. Remove two distinct edges to create three connected components. XOR the values within each component; the score is the largest of these three XOR values minus the smallest. For example, components [4,5,7], [1,9], and [3,3,3] have XOR values 4 ^ 5 ^ 7 = 6, 1 ^ 9 = 8, and 3 ^ 3 ^ 3 = 3, so the score is 8 - 3 = 5. Return the minimum score over all pairs of removed edges.
    ///
    /// Images: https://assets.leetcode.com/uploads/2022/05/03/ex1drawio.png and https://assets.leetcode.com/uploads/2022/05/03/ex2drawio.png
    ///
    /// Example 1:
    /// Input: nums = [1,5,5,4,11], edges = [[0,1],[1,2],[1,3],[3,4]]
    /// Output: 9
    /// Explanation: Remove edges to form components with nodes [1,3,4], [0], and [2]. Their values are [5,4,11], [1], and [5], and their XOR values are 5 ^ 4 ^ 11 = 10, 1 = 1, and 5 = 5. The score is 10 - 1 = 9, the minimum possible.
    ///
    /// Example 2:
    /// Input: nums = [5,5,2,4,4,2], edges = [[0,1],[1,2],[5,2],[4,3],[1,3]]
    /// Output: 0
    /// Explanation: Form components with nodes [3,4], [1,0], and [2,5]. Their values are [4,4], [5,5], and [2,2], so every XOR is 0. The score is 0 - 0 = 0, which cannot be improved.
    ///
    /// Constraints:
    /// - n == nums.length
    /// - 3 &lt;= n &lt;= 1000
    /// - 1 &lt;= nums[i] &lt;= 10^8
    /// - edges.length == n - 1
    /// - edges[i].length == 2
    /// - 0 &lt;= a_i, b_i &lt; n
    /// - a_i != b_i
    /// - edges represents a valid tree.
    /// </para>
    /// <para>
    /// 2322. 從樹中刪除邊的最小分數
    /// https://leetcode.cn/problems/minimum-score-after-removals-on-a-tree/description/
    ///
    /// 一棵無向連通樹有 n 個編號 0 到 n - 1 的節點與 n - 1 條邊。nums[i] 是節點 i 的值，edges[i] = [a_i,b_i] 表示一條邊。刪除兩條不同的邊以產生三個連通分量；分別計算各分量節點值的 XOR，分數是三個 XOR 值中的最大值減最小值。例如分量 [4,5,7]、[1,9]、[3,3,3] 的 XOR 分別為 4 ^ 5 ^ 7 = 6、1 ^ 9 = 8、3 ^ 3 ^ 3 = 3，因此分數為 8 - 3 = 5。回傳所有刪邊組合中的最小分數。
    ///
    /// 圖片：https://assets.leetcode.com/uploads/2022/05/03/ex1drawio.png 與 https://assets.leetcode.com/uploads/2022/05/03/ex2drawio.png
    ///
    /// 範例 1：
    /// 輸入：nums = [1,5,5,4,11], edges = [[0,1],[1,2],[1,3],[3,4]]
    /// 輸出：9
    /// 說明：刪邊後形成節點為 [1,3,4]、[0]、[2] 的分量，其值為 [5,4,11]、[1]、[5]，XOR 值為 5 ^ 4 ^ 11 = 10、1 = 1、5 = 5。分數是 10 - 1 = 9，且已是最小值。
    ///
    /// 範例 2：
    /// 輸入：nums = [5,5,2,4,4,2], edges = [[0,1],[1,2],[5,2],[4,3],[1,3]]
    /// 輸出：0
    /// 說明：形成節點為 [3,4]、[1,0]、[2,5] 的分量，其值為 [4,4]、[5,5]、[2,2]，每個 XOR 都是 0。分數為 0 - 0 = 0，不可能更小。
    ///
    /// 限制條件：
    /// - n == nums.length
    /// - 3 &lt;= n &lt;= 1000
    /// - 1 &lt;= nums[i] &lt;= 10^8
    /// - edges.length == n - 1
    /// - edges[i].length == 2
    /// - 0 &lt;= a_i, b_i &lt; n
    /// - a_i != b_i
    /// - edges 表示一棵有效的樹。
    /// </para>
    /// </summary>
    /// <param name="args"></param> <summary>
    /// <summary>
    /// 主程式進入點，包含測試範例
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        Program program = new Program();
        
        // 測試範例 1
        Console.WriteLine("=== 測試範例 1 ===");
        int[] nums1 = { 1, 5, 5, 4, 11 };
        int[][] edges1 = { 
            new int[] { 0, 1 }, 
            new int[] { 1, 2 }, 
            new int[] { 1, 3 }, 
            new int[] { 3, 4 } 
        };
        int result1 = program.MinimumScore(nums1, edges1);
        Console.WriteLine($"節點值: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"邊: [{string.Join("], [", edges1.Select(e => string.Join(", ", e)))}]");
        Console.WriteLine($"最小分數: {result1}");
        Console.WriteLine();
        
        // 測試範例 2  
        Console.WriteLine("=== 測試範例 2 ===");
        int[] nums2 = { 5, 5, 2, 4, 4, 2 };
        int[][] edges2 = {
            new int[] { 0, 1 },
            new int[] { 1, 2 },
            new int[] { 5, 2 },
            new int[] { 4, 3 },
            new int[] { 1, 3 }
        };
        int result2 = program.MinimumScore(nums2, edges2);
        Console.WriteLine($"節點值: [{string.Join(", ", nums2)}]");
        Console.WriteLine($"邊: [{string.Join("], [", edges2.Select(e => string.Join(", ", e)))}]");
        Console.WriteLine($"最小分數: {result2}");
    }

    /// <summary>
    /// 計算從樹中刪除兩條邊後的最小分數
    /// 
    /// 解題思路：
    /// 1. 使用 DFS 序來處理樹的遍歷，通過 in[x] 和 out[x] 記錄每個節點子樹的遍歷範圍
    /// 2. 如果 in[x] < in[y] < out[x]，則 x 是 y 的祖先節點
    /// 3. 透過一次 DFS 計算每個子樹的異或和，然後枚舉所有可能的邊對
    /// 4. 根據兩個節點的祖先關係，計算刪除兩條邊後形成的三個部分的異或值
    /// 
    /// 時間複雜度：O(n²) - 需要枚舉所有可能的節點對
    /// 空間複雜度：O(n) - 存儲鄰接表、DFS序和子樹異或和
    /// 
    /// ref:https://leetcode.cn/problems/minimum-score-after-removals-on-a-tree/solutions/3726042/cong-shu-zhong-shan-chu-bian-de-zui-xiao-mrrc/?envType=daily-question&envId=2025-07-24
    /// 
    /// memo:
    /// 祖先節點的定義確實就是：
    /// 比較早被訪問 (in_[ancestor] < in_[descendant])
    /// 比較晚離開 (out_[ancestor] > out_[descendant])
    /// 有可以理解為: 越小就是越早被訪問，越大就是越晚離開
    /// </summary>
    /// <param name="nums">每個節點的值陣列</param>
    /// <param name="edges">樹的邊集合</param>
    /// <returns>所有可能刪除邊對的最小分數</returns>
    public int MinimumScore(int[] nums, int[][] edges)
    {
        int n = nums.Length;
        
        // 建立鄰接表來表示樹的結構
        List<List<int>> adj = new List<List<int>>();
        for (int i = 0; i < n; i++)
        {
            adj.Add(new List<int>());
        }

        // 將邊加入鄰接表，因為是無向樹，所以雙向建立連接
        foreach (var e in edges)
        {
            adj[e[0]].Add(e[1]);
            adj[e[1]].Add(e[0]);
        }

        // 初始化陣列
        int[] sum = new int[n];    // 每個子樹的異或和
        int[] in_ = new int[n];    // DFS序中每個節點開始遍歷的時間戳
        int[] out_ = new int[n];   // DFS序中每個節點結束遍歷的時間戳
        int cnt = 0;               // DFS遍歷的計數器
        
        // 從根節點0開始進行DFS，計算子樹異或和和DFS序
        Dfs(0, -1, nums, adj, sum, in_, out_, ref cnt);

        int res = int.MaxValue;
        
        // 枚舉所有可能的節點對(u,v)，排除根節點0
        for (int u = 1; u < n; u++)
        {
            for (int v = u + 1; v < n; v++)
            { 
                // 檢查u和v的祖先關係，並計算對應的三個部分異或值
                if (in_[v] > in_[u] && in_[v] < out_[u]) 
                {
                    // u是v的祖先：三個部分為 (全部^u的子樹), (u的子樹^v的子樹), (v的子樹)
                    res = Math.Min(res, Calc(sum[0] ^ sum[u], sum[u] ^ sum[v], sum[v]));
                } 
                else if (in_[u] > in_[v] && in_[u] < out_[v]) 
                {
                    // v是u的祖先：三個部分為 (全部^v的子樹), (v的子樹^u的子樹), (u的子樹)
                    res = Math.Min(res, Calc(sum[0] ^ sum[v], sum[v] ^ sum[u], sum[u]));
                } 
                else 
                {
                    // u和v不互為祖先：三個部分為 (全部^u的子樹^v的子樹), (u的子樹), (v的子樹)
                    res = Math.Min(res, Calc(sum[0] ^ sum[u] ^ sum[v], sum[u], sum[v]));
                }
            }
        }
        return res;
    }

    /// <summary>
    /// 計算三個部分異或值的分數（最大值與最小值的差）
    /// 
    /// 根據題目要求，分數定義為三個連通元件異或值中的最大值減去最小值
    /// 這個方法計算給定三個異或值的分數
    /// 
    /// 注意: Math.Max or Math.Min 比較方式一次只能輸入兩組數字, 但是有三組數字需要比對
    ///       所以這邊拆分兩次比較，先找出最大值和最小值，再計算差值
    /// </summary>
    /// <param name="part1">第一個部分的異或值</param>
    /// <param name="part2">第二個部分的異或值</param>
    /// <param name="part3">第三個部分的異或值</param>
    /// <returns>三個異或值中最大值與最小值的差</returns>
    private int Calc(int part1, int part2, int part3)
    {
        // 計算三個異或值中的最大值和最小值，然後返回它們的差
        return Math.Max(part1, Math.Max(part2, part3)) - Math.Min(part1, Math.Min(part2, part3));
    }

    /// <summary>
    /// 深度優先搜尋，計算每個子樹的異或和以及DFS序
    /// 
    /// 這個方法同時完成三個任務：
    /// 1. 記錄DFS序：in_[x]記錄節點x開始被訪問的時間，out_[x]記錄節點x訪問結束的時間
    /// 2. 計算子樹異或和：sum[x]儲存以節點x為根的子樹中所有節點值的異或結果
    /// 3. 建立祖先關係判斷基礎：透過DFS序可以判斷節點間的祖先關係
    /// 
    /// DFS序的特性：如果節點x是節點y的祖先，則 in_[x] < in_[y] < out_[x]
    /// </summary>
    /// <param name="x">當前遍歷的節點</param>
    /// <param name="fa">當前節點的父節點</param>
    /// <param name="nums">節點值陣列</param>
    /// <param name="adj">樹的鄰接表表示</param>
    /// <param name="sum">儲存每個子樹異或和的陣列</param>
    /// <param name="in_">儲存每個節點DFS開始時間的陣列</param>
    /// <param name="out_">儲存每個節點DFS結束時間的陣列</param>
    /// <param name="cnt">DFS遍歷的時間計數器</param>
    private void Dfs(int x, int fa, int[] nums, List<List<int>> adj, int[] sum, int[] in_, int[] out_, ref int cnt)
    {
        // 記錄節點x開始被訪問的時間（DFS序的開始時間）
        in_[x] = cnt++;
        
        // 初始化當前節點的異或和為節點本身的值
        sum[x] = nums[x];
        
        // 遍歷當前節點的所有鄰居節點
        foreach (int y in adj[x])
        {
            // 跳過父節點，避免回到已經訪問過的節點
            if (y == fa)
            {
                continue;
            }
            
            // 遞迴訪問子節點
            Dfs(y, x, nums, adj, sum, in_, out_, ref cnt);
            
            // 將子樹的異或和合併到當前節點
            // 這裡使用異或運算的性質：A ^ A = 0, A ^ 0 = A
            sum[x] ^= sum[y];
        }
        
        // 記錄節點x訪問結束的時間（DFS序的結束時間）
        out_[x] = cnt;
    }
}
