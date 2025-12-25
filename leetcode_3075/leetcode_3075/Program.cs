namespace leetcode_3075;

class Program
{
    /// <summary>
    /// 3075. Maximize Happiness of Selected Children
    /// https://leetcode.com/problems/maximize-happiness-of-selected-children/description/?envType=daily-question&envId=2025-12-25
    /// 3075. 幸福值最大化的选择方案
    /// https://leetcode.cn/problems/maximize-happiness-of-selected-children/description/?envType=daily-question&envId=2025-12-25
    /// 
    /// 給定一個長度為 n 的整數陣列 `happiness` 與正整數 `k`。
    /// 有 n 位小孩排成一列，第 i 位小孩的快樂值為 `happiness[i]`。
    /// 你要在 k 回合內選出 k 位小孩；每當選擇一位小孩時，尚未被選中的每位小孩的快樂值會減少 1（但不會低於 0）。
    /// 回傳能取得的被選中小孩快樂值總和的最大值。
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
