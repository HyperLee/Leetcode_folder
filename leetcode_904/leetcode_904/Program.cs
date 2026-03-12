namespace leetcode_904;

class Program
{
    /// <summary>
    /// 904. Fruit Into Baskets
    /// https://leetcode.com/problems/fruit-into-baskets/description/
    /// 904. 水果成籃
    /// https://leetcode.cn/problems/fruit-into-baskets/description/
    ///
    /// You are visiting a farm that has a single row of fruit trees arranged from left to right.
    /// The trees are represented by an integer array fruits where fruits[i] is the type of fruit the ith tree produces.
    ///
    /// You want to collect as much fruit as possible. However, the owner has some strict rules that you must follow:
    ///
    /// - You only have two baskets, and each basket can only hold a single type of fruit.
    ///   There is no limit on the amount of fruit each basket can hold.
    /// - Starting from any tree of your choice, you must pick exactly one fruit from every tree
    ///   (including the start tree) while moving to the right. The picked fruits must fit in one of your baskets.
    /// - Once you reach a tree with fruit that cannot fit in your baskets, you must stop.
    ///
    /// Given the integer array fruits, return the maximum number of fruits you can pick.
    ///
    /// ---
    ///
    /// 你正在探訪一家農場，農場從左到右種植了一排果樹。
    /// 這些樹用整數陣列 fruits 表示，其中 fruits[i] 是第 i 棵樹所產的水果種類。
    ///
    /// 你想要儘可能多地收集水果。然而，農場的主人設定了一些嚴格的規定，你必須遵守：
    ///
    /// - 你只有兩個籃子，每個籃子只能裝單一種類的水果。
    ///   每個籃子能裝的水果數量沒有限制。
    /// - 從你選擇的任意一棵樹開始，你必須從每棵樹（包含起始樹）中摘取恰好一個水果，
    ///   並向右移動。摘取的水果必須能放入其中一個籃子。
    /// - 一旦你到達一棵水果無法放入任何籃子的樹，你就必須停止。
    ///
    /// 給定整數陣列 fruits，請回傳你能摘取的最大水果數量。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試範例 1: fruits = [1,2,1] => 預期輸出: 3
        // 說明: 三棵樹只有兩種水果 (1, 2)，全部都能摘取
        int[] fruits1 = [1, 2, 1];
        Console.WriteLine($"測試 1: [{string.Join(", ", fruits1)}] => {solution.TotalFruit(fruits1)}"); // 3

        // 測試範例 2: fruits = [0,1,2,2] => 預期輸出: 3
        // 說明: 從第 2 棵樹開始摘取 [1, 2, 2]，共 3 個水果
        int[] fruits2 = [0, 1, 2, 2];
        Console.WriteLine($"測試 2: [{string.Join(", ", fruits2)}] => {solution.TotalFruit(fruits2)}"); // 3

        // 測試範例 3: fruits = [1,2,3,2,2] => 預期輸出: 4
        // 說明: 從第 2 棵樹開始摘取 [2, 3, 2, 2]，共 4 個水果
        int[] fruits3 = [1, 2, 3, 2, 2];
        Console.WriteLine($"測試 3: [{string.Join(", ", fruits3)}] => {solution.TotalFruit(fruits3)}"); // 4

        // 測試範例 4: 只有一種水果 => 預期輸出: 5
        int[] fruits4 = [3, 3, 3, 3, 3];
        Console.WriteLine($"測試 4: [{string.Join(", ", fruits4)}] => {solution.TotalFruit(fruits4)}"); // 5

        // 測試範例 5: 空陣列邊界情況 => 預期輸出: 0
        int[] fruits5 = [];
        Console.WriteLine($"測試 5: [] => {solution.TotalFruit(fruits5)}"); // 0

        // 測試範例 6: 單一元素 => 預期輸出: 1
        int[] fruits6 = [7];
        Console.WriteLine($"測試 6: [{string.Join(", ", fruits6)}] => {solution.TotalFruit(fruits6)}"); // 1
    }

    /// <summary>
    /// 方法一：滑動窗口 (Sliding Window)
    ///
    /// <para>
    /// 【解題思路】
    /// 本題本質上是：在整數陣列中，找到一個最長的連續子陣列，使得子陣列中最多只包含兩種不同的數字。
    /// 這是經典的「滑動窗口 + 雜湊表」問題。
    /// </para>
    ///
    /// <para>
    /// 【演算法】
    /// 1. 使用雙指標 left 和 right 維護一個滑動窗口 [left, right]。
    /// 2. 使用雜湊表 cnt 記錄窗口內每種水果的出現次數。
    /// 3. 每次將 right 向右擴展一格，將 fruits[right] 加入雜湊表。
    /// 4. 若雜湊表中的鍵值數量超過 2（代表窗口內超過兩種水果），
    ///    則不斷將 left 向右收縮，並從雜湊表中移除 fruits[left]，
    ///    直到窗口內只剩兩種水果為止。
    /// 5. 每次擴展後，更新最大長度 res = max(res, right - left + 1)。
    /// </para>
    ///
    /// <para>
    /// 【複雜度分析】
    /// - 時間複雜度：O(n)，其中 n 為陣列長度。left 和 right 各最多移動 n 次。
    /// - 空間複雜度：O(1)，雜湊表中最多存放 3 個鍵值對。
    /// </para>
    ///
    /// <example>
    /// 範例：fruits = [1, 2, 3, 2, 2]
    /// <code>
    /// var solution = new Program();
    /// int result = solution.TotalFruit(new[] { 1, 2, 3, 2, 2 }); // 回傳 4
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="fruits">代表每棵樹所產水果種類的整數陣列</param>
    /// <returns>最多能摘取的水果數量</returns>
    public int TotalFruit(int[] fruits)
    {
        int n = fruits.Length;

        // 雜湊表：記錄目前窗口內每種水果的出現次數
        IDictionary<int, int> cnt = new Dictionary<int, int>();

        // left 為窗口左邊界
        int left = 0;

        // res 記錄答案（最大窗口長度）
        int res = 0;

        // right 為窗口右邊界，逐步向右擴展
        for (int right = 0; right < n; right++)
        {
            // 將 fruits[right] 加入雜湊表，若不存在則初始化為 0
            if (!cnt.ContainsKey(fruits[right]))
            {
                cnt[fruits[right]] = 0;
            }
            cnt[fruits[right]]++;

            // 當窗口內水果種類超過 2 種時，收縮左邊界
            while (cnt.Count > 2)
            {
                cnt[fruits[left]]--;

                // 若某種水果次數降為 0，從雜湊表移除該鍵
                if (cnt[fruits[left]] == 0)
                {
                    cnt.Remove(fruits[left]);
                }
                left++;
            }

            // 更新最大窗口長度
            res = Math.Max(res, right - left + 1);
        }

        return res;
    }
}
