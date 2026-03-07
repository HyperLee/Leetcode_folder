namespace leetcode_886;

class Program
{
    /// <summary>
    /// 886. Possible Bipartition
    /// https://leetcode.com/problems/possible-bipartition/description/
    /// 886. 可能的二分法
    /// https://leetcode.cn/problems/possible-bipartition/description/
    /// 
    /// We want to split a group of n people (labeled from 1 to n) into two groups of any size.
    /// Each person may dislike some other people, and they should not go into the same group.
    /// Given the integer n and the array dislikes where dislikes[i] = [ai, bi] indicates that 
    /// the person labeled ai does not like the person labeled bi, return true if it is possible
    /// to split everyone into two groups in this way.
    /// 
    /// 我們希望將一群 n 個人（標記為 1 到 n）分成任意大小的兩組。
    /// 每個人可能不喜歡某些其他人，他們不應該在同一組。
    /// 給定整數 n 和陣列 dislikes，其中 dislikes[i] = [ai, bi] 表示標記為 ai 的人不喜歡
    /// 標記為 bi 的人，如果可以按照這種方式將每個人拆分成兩組，則返回 true。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        // 測試案例 1: n=4, dislikes=[[1,2],[1,3],[2,4]]
        // 可將所有人分成兩組：[1,4] 和 [2,3]
        // 1 不喜歡 2 → 分開，1 不喜歡 3 → 分開，2 不喜歡 4 → 分開，合法
        // 預期輸出：True
        int[][] dislikes1 = [[1, 2], [1, 3], [2, 4]];
        Console.WriteLine($"Test 1 (n=4, dislikes=[[1,2],[1,3],[2,4]]): {solution.PossibleBipartition(4, dislikes1)}");

        // 測試案例 2: n=3, dislikes=[[1,2],[1,3],[2,3]]
        // 三人彼此互相不喜歡，形成奇數環，無法完成二分
        // 預期輸出：False
        int[][] dislikes2 = [[1, 2], [1, 3], [2, 3]];
        Console.WriteLine($"Test 2 (n=3, dislikes=[[1,2],[1,3],[2,3]]): {solution.PossibleBipartition(3, dislikes2)}");

        // 測試案例 3: n=5, dislikes=[[1,2],[2,3],[3,4],[4,5],[1,5]]
        // 1-2-3-4-5-1 形成長度為 5 的奇數環，無法完成二分
        // 預期輸出：False
        int[][] dislikes3 = [[1, 2], [2, 3], [3, 4], [4, 5], [1, 5]];
        Console.WriteLine($"Test 3 (n=5, dislikes=[[1,2],[2,3],[3,4],[4,5],[1,5]]): {solution.PossibleBipartition(5, dislikes3)}");
    }

    /// <summary>
    /// 判斷 n 個人是否可依「不喜歡」關係分成兩組（二分圖判定）。
    ///
    /// 解題思路（染色法 / 二分圖著色）：
    /// 將每個人視為圖中的節點，若 a 不喜歡 b，則在 a 與 b 之間建立一條無向邊。
    /// 問題轉化為：此無向圖是否為「二分圖」？
    ///
    /// 二分圖的充要條件：圖中不存在奇數長度的環。
    /// 驗證方式：使用染色法，將節點依序染成顏色 1（第一組）或顏色 2（第二組）。
    /// 若相鄰兩節點（即互相不喜歡的兩人）被染成同色，則代表衝突，無法二分，回傳 false。
    ///
    /// 演算法步驟：
    /// 1. 建立鄰接表 g，記錄每位使用者的「不喜歡」名單（無向邊）。
    /// 2. 依序走訪每個節點，若該節點尚未染色，從該節點啟動深度優先搜尋（DFS）嘗試染色。
    /// 3. 若任一節點的 DFS 回傳衝突（false），整體回傳 false；全部完成則回傳 true。
    ///
    /// 時間複雜度：O(n + m)，其中 m 為 dislikes 的長度（邊數）。
    /// 空間複雜度：O(n + m)，用於鄰接表與遞迴堆疊。
    /// </summary>
    /// <param name="n">人數，編號範圍 1 到 n</param>
    /// <param name="dislikes">不喜歡關係陣列，dislikes[i] = [a, b] 表示 a 不喜歡 b</param>
    /// <returns>若可分成兩組回傳 true，否則回傳 false</returns>
    public bool PossibleBipartition(int n, int[][] dislikes)
    {
        // color[i] 記錄節點 i 的分組顏色：0 = 未分組，1 = 第一組，2 = 第二組
        int[] color = new int[n + 1];

        // g[i] 為鄰接表，儲存節點 i 的所有「不喜歡」對象（無向邊，雙向記錄）
        IList<int>[] g = new IList<int>[n + 1];
        for(int i = 0; i <= n; i++)
        {
            g[i] = new List<int>();
        }

        // 建立無向圖：若 a 不喜歡 b，則 a→b 與 b→a 皆加入鄰接表
        foreach(int[] p in dislikes)
        {
            g[p[0]].Add(p[1]);
            g[p[1]].Add(p[0]);
        }

        // 依序走訪每個節點；若尚未染色，從該節點啟動 DFS 嘗試染色
        // 圖可能不連通，因此需對每個未染色節點分別啟動 DFS
        for(int i = 1; i <= n; i++)
        {
            if(color[i] == 0 && !DFS(i, 1, color, g))
            {
                // 此節點所在的連通分量存在奇數環，無論如何都無法二分
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 深度優先搜尋（DFS）對節點進行染色，驗證以 curnode 為起點的連通分量是否為二分圖。
    ///
    /// 染色規則：
    /// - 顏色 0：尚未分組
    /// - 顏色 1：第一組（紅色）
    /// - 顏色 2：第二組（藍色）
    ///
    /// 反色公式：使用位元互斥或（XOR）切換顏色。
    ///   3（二進位 11）XOR 1（二進位 01）= 2（二進位 10）
    ///   3（二進位 11）XOR 2（二進位 10）= 1（二進位 01）
    /// 因此 3 ^ nowcolor 可在 1 與 2 之間切換，非常簡潔。
    ///
    /// DFS 任務：
    /// 1. 將 curnode 染成 nowcolor。
    /// 2. 走訪 curnode 的每一位「仇人」（相鄰節點）：
    ///    - 若仇人已被染色且與 curnode 同色 → 衝突，回傳 false。
    ///    - 若仇人尚未染色 → 遞迴地將仇人染成反色（3 ^ nowcolor），若遞迴失敗也回傳 false。
    /// 3. 若所有仇人皆處理完畢且無衝突，回傳 true。
    ///
    /// <example>
    /// <code>
    /// 節點 1 染為顏色 1，其仇人 2、3 將被嘗試染為顏色 2（3^1=2）
    /// DFS(1, 1, color, g);
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="curnode">目前正在處理的節點編號</param>
    /// <param name="nowcolor">要賦予 curnode 的顏色（1 或 2）</param>
    /// <param name="color">所有節點的染色狀態陣列，0 表示尚未染色</param>
    /// <param name="g">鄰接表，g[i] 儲存節點 i 的所有相鄰節點（仇人）</param>
    /// <returns>若染色方案合法（無衝突）回傳 true，否則回傳 false</returns>
    public bool DFS(int curnode, int nowcolor, int[] color, IList<int>[] g)
    {
        // 將當前節點染上指定顏色，完成分組歸屬
        color[curnode] = nowcolor;

        // g[curnode] 為 curnode 的所有相鄰節點（即不喜歡的對象）
        foreach(int nextnode in g[curnode])
        {
            if(color[nextnode] != 0 && color[nextnode] == color[curnode])
            {
                // 相鄰節點已被染色，且與 curnode 顏色相同 → 產生衝突，無法二分
                return false;
            }

            if(color[nextnode] == 0 && !DFS(nextnode, 3 ^ nowcolor, color, g))
            {
                // 相鄰節點尚未染色，嘗試染反色（3 ^ nowcolor：1↔2 互換）
                // 若遞迴 DFS 也回傳衝突，則整條路徑均無法二分
                return false;
            }
        }
        return true;
    }
}
