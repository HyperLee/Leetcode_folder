namespace leetcode_3075;

class Program
{
    /// <summary>
    /// 3075. Maximize Happiness of Selected Children
    /// https://leetcode.com/problems/maximize-happiness-of-selected-children/description/
    /// <para>
    /// You are given an array happiness of length n and a positive integer k.
    ///
    /// There are n children standing in a queue, where the i-th child has happiness value happiness[i]. You want to select k children from these n children in k turns.
    ///
    /// In each turn, when you select a child, the happiness value of every child not selected so far decreases by 1. A happiness value cannot become negative and is decremented only when it is positive.
    ///
    /// Return the maximum sum of the happiness values of the selected children that you can achieve by selecting k children.
    ///
    /// Example 1:
    /// Input: happiness = [1,2,3], k = 2
    /// Output: 4
    /// Explanation: Pick the child with happiness 3, leaving values [0,1]. Then pick the child with happiness 1, leaving [0]. Happiness cannot fall below 0. The selected sum is 3 + 1 = 4.
    ///
    /// Example 2:
    /// Input: happiness = [1,1,1,1], k = 2
    /// Output: 1
    /// Explanation: Pick any child with happiness 1, leaving [0,0,0]. Then pick a child with happiness 0, leaving [0,0]. The selected sum is 1 + 0 = 1.
    ///
    /// Example 3:
    /// Input: happiness = [2,3,4,5], k = 1
    /// Output: 5
    /// Explanation: Pick the child with happiness 5, leaving [1,2,3]. The selected sum is 5.
    ///
    /// Constraints:
    /// - 1 &lt;= n == happiness.length &lt;= 2 * 10^5
    /// - 1 &lt;= happiness[i] &lt;= 10^8
    /// - 1 &lt;= k &lt;= n
    /// </para>
    /// <para>
    /// 3075. 使選取孩子的快樂值最大化
    /// https://leetcode.cn/problems/maximize-happiness-of-selected-children/description/
    ///
    /// 給定一個長度為 n 的陣列 happiness 和正整數 k。
    ///
    /// 有 n 個孩子站成一列，第 i 個孩子的快樂值為 happiness[i]。你要在 k 個回合中，從這 n 個孩子裡選出 k 個孩子。
    ///
    /// 每一回合選取一個孩子時，所有尚未被選取孩子的快樂值都會減少 1。快樂值不能變成負數，而且只有在為正數時才會減少。
    ///
    /// 回傳選取 k 個孩子後，所能得到的被選孩子快樂值總和最大值。
    ///
    /// 範例 1：
    /// 輸入：happiness = [1,2,3], k = 2
    /// 輸出：4
    /// 解釋：先選取快樂值為 3 的孩子，剩餘快樂值變為 [0,1]；再選取快樂值為 1 的孩子，剩下 [0]。快樂值不能低於 0。所選快樂值總和為 3 + 1 = 4。
    ///
    /// 範例 2：
    /// 輸入：happiness = [1,1,1,1], k = 2
    /// 輸出：1
    /// 解釋：先選任意一個快樂值為 1 的孩子，剩餘值變為 [0,0,0]；再選一個快樂值為 0 的孩子，剩下 [0,0]。所選快樂值總和為 1 + 0 = 1。
    ///
    /// 範例 3：
    /// 輸入：happiness = [2,3,4,5], k = 1
    /// 輸出：5
    /// 解釋：選取快樂值為 5 的孩子，剩餘值變為 [1,2,3]。所選快樂值總和為 5。
    ///
    /// 限制條件：
    /// - 1 &lt;= n == happiness.length &lt;= 2 * 10^5
    /// - 1 &lt;= happiness[i] &lt;= 10^8
    /// - 1 &lt;= k &lt;= n
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solver = new Program();
        var tests = new (int[] happiness, int k, long expected)[]
        {
            (new int[]{5,2,2}, 2, 6),
            (new int[]{1,1,1}, 3, 1),
            (new int[]{5,0,0}, 2, 5),
            (new int[]{4,3,3,2}, 3, 7),
            (new int[]{10,8,6,4,2}, 5, 22),
        };

        for (int i = 0; i < tests.Length; i++)
        {
            var (h, k, exp) = tests[i];
            long ans = solver.MaximumHappinessSum(h, k);
            Console.WriteLine($"Test {i + 1}: happiness=[{string.Join(',', h)}], k={k}, expected={exp}, actual={ans}");
        }
    }

    /// <summary>
    /// 計算在 k 回合中可以取得的快樂值總和最大值。策略：對 `happiness` 由大到小排序，依序選擇最大的值，
    /// 第 i 次選擇的實得值為 max(0, sorted[i] - i)。
    /// 
    /// long val = (long)happiness[i] - i;
    /// 為什麼是減 i？因為每次選擇後，其他未被選中的小孩快樂值都會減少 1。
    /// 因為沒有減一且把數值塞回去所以直接用減 i 來計算實得值。
    /// i 是遞增的，每次都 + 1 而已。
    /// </summary>
    /// <param name="happiness">各小孩的快樂值陣列</param>
    /// <param name="k">要選擇的次數</param>
    /// <returns>可取得的最大快樂值總和（long）</returns>
    public long MaximumHappinessSum(int[] happiness, int k)
    {
        if (happiness == null || happiness.Length == 0 || k <= 0)
            return 0L;

        int n = happiness.Length;
        // sort descending
        Array.Sort(happiness);
        Array.Reverse(happiness);
        long total = 0L;
        for (int i = 0; i < k && i < n; i++)
        {
            long val = (long)happiness[i] - i;
            if (val <= 0)
            {
                break;
            }

            total += val;
        }
        return total;
    }
}
