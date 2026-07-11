namespace leetcode_421;

internal static class Program
{
    private const int HighestBit = 30;

    private static int s_checks;
    private static int s_passed;

    /// <summary>
    /// 421. Maximum XOR of Two Numbers in an Array
    /// https://leetcode.com/problems/maximum-xor-of-two-numbers-in-an-array/
    /// 421. 陣列中兩個數的最大 XOR 值
    /// https://leetcode.cn/problems/maximum-xor-of-two-numbers-in-an-array/
    /// Given an integer array nums, return the maximum result of nums[i] XOR nums[j], where 0 &lt;= i &lt;= j &lt; n.
    /// 給定整數陣列 nums，回傳 nums[i] XOR nums[j] 的最大結果，其中 0 &lt;= i &lt;= j &lt; n。
    /// </summary>
    /// <remarks>
    /// <para>
    /// XOR 邏輯基本知識複習如下：兩個位元相同為 0，只有其中一個為 1 時才為 1。
    /// </para>
    /// <code>
    /// A  B  A XOR B
    /// 0  0     0
    /// 0  1     1
    /// 1  0     1
    /// 1  1     0
    /// </code>
    /// https://teatime28.pixnet.net/blog/post/338679642-and%E3%80%81or%E3%80%81nand%E3%80%81nor%E3%80%81xor%E3%80%81xnor-%E7%9C%9F%E5%80%BC%E8%A1%A8(true-table)%E6%95%B4
    /// <para>
    /// XOR 具有反運算性：若 a XOR b = c，則 a XOR c = b，且 b XOR c = a。
    /// 因此，若某個候選 XOR 前綴為 x，且 pre_k(a_i) XOR pre_k(a_j) = x，便可推得
    /// pre_k(a_j) = x XOR pre_k(a_i)。程式先把所有 pre_k(a_j) 放入 HashSet，再枚舉
    /// pre_k(a_i) 並檢查是否存在 x XOR pre_k(a_i)，便能判定候選前綴是否可由兩個數達成。
    /// </para>
    /// <para>
    /// 對目前處理的位元 k，pre_k(a) = a &gt;&gt; k；右移會捨去較低的 0 到 k - 1 位，只保留
    /// 第 30 位到第 k 位的高位前綴。這正是 HashSet 中的 prefixes 值，而不是完整的原始數字。
    /// </para>
    /// https://leetcode.cn/problems/maximum-xor-of-two-numbers-in-an-array/?envType=daily-question&amp;envId=Invalid%20Date
    /// https://leetcode.cn/problems/maximum-xor-of-two-numbers-in-an-array/solutions/778291/shu-zu-zhong-liang-ge-shu-de-zui-da-yi-h-n9m9/?envType=daily-question&amp;envId=Invalid+Date
    /// https://leetcode.cn/problems/maximum-xor-of-two-numbers-in-an-array/solutions/9289/li-yong-yi-huo-yun-suan-de-xing-zhi-tan-xin-suan-f/?envType=daily-question&amp;envId=Invalid+Date
    /// <para>
    /// 有效輸入是非負 int，因此只需考慮第 30 位到第 0 位的 31 個二進位位元。演算法從第 30 位
    /// 往下走到第 0 位：已確定較高位的 maximumXor 後，先假設目前位元可為 1，形成
    /// candidate = (maximumXor &lt;&lt; 1) | 1。若 HashSet 找得到可配對的高位前綴，這個 1 就可保留；
    /// 否則該位只能為 0。高位比任何後續低位更能決定數值大小，因此一旦可行就優先保留候選位元 1，
    /// 可在不回溯的情況下得到最大 XOR。
    /// </para>
    /// <para>
    /// 右移運算可取得數值的高位前綴：
    /// </para>
    /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#right-shift-operator-
    /// </remarks>
    private static void Main()
    {
        Console.WriteLine("LeetCode 421 acceptance harness");
        Console.WriteLine();

        RunCase("Official example 1", new int[] { 3, 10, 5, 25, 2, 8 }, 28);
        RunCase("Official example 2", new int[] { 14, 70, 53, 83, 49, 91, 36, 80, 92, 51, 66, 70 }, 127);
        RunCase("Minimum valid input", new int[] { 0 }, 0);
        RunCase("Duplicate values", new int[] { 7, 7 }, 0);
        RunCase("Bit 30 boundary", new int[] { 0, 1 << 30 }, 1 << 30);
        RunCase("All legal value bits", new int[] { 0, int.MaxValue }, int.MaxValue);
        RunCase("Greedy-prefix regression", new int[] { 8, 1, 2 }, 10);

        int[] upperBoundInput = new int[200_000];
        upperBoundInput[1] = int.MaxValue;
        RunCase(
            "Upper-bound spot check",
            upperBoundInput,
            int.MaxValue,
            "generated length = 200000; values include 0 and 2147483647");

        Console.WriteLine($"Summary: {s_passed}/{s_checks} checks passed.");

        if (s_passed != s_checks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 計算陣列中任兩個元素 XOR 後的最大值。
    /// </summary>
    /// <param name="nums">依 LeetCode 有效輸入契約提供的非負整數陣列；方法不另外定義無效輸入行為。</param>
    /// <returns>可由陣列中兩個元素 XOR 取得的最大 <see langword="int"/> 結果。</returns>
    /// <remarks>
    /// 從第 30 位往第 0 位逐步貪婪決定答案：每輪以 <c>HashSet&lt;int&gt;</c> 收集各數右移後的高位前綴，
    /// 並驗證假設目前位元為 1 的候選前綴是否能由兩個前綴配對形成。
    /// </remarks>
    public static int FindMaximumXOR(int[] nums)
    {
        int maximumXor = 0;

        for (int bit = HighestBit; bit >= 0; bit--)
        {
            HashSet<int> prefixes = new HashSet<int>();

            foreach (int num in nums)
            {
                // 右移捨去尚未決定的低位，只保留本輪位元判斷所需的高位前綴。
                prefixes.Add(num >> bit);
            }

            // 先貪婪地把目前位元設為 1；若兩個已存在的前綴可達成候選值，就保留它。
            int candidate = (maximumXor << 1) | 1;
            bool candidateExists = false;

            foreach (int prefix in prefixes)
            {
                // XOR 反運算可由 candidate ^ prefix 推得互補前綴；集合中存在它即證明候選值可行。
                if (prefixes.Contains(candidate ^ prefix))
                {
                    candidateExists = true;
                    break;
                }
            }

            maximumXor = candidateExists ? candidate : candidate - 1;
        }

        return maximumXor;
    }

    /// <summary>
    /// 執行一筆固定測資，輸出測資與比對結果，並更新驗收 harness 的檢查統計。
    /// </summary>
    /// <param name="name">顯示於輸出的測資名稱。</param>
    /// <param name="nums">傳入 <see cref="FindMaximumXOR(int[])"/> 的測試陣列。</param>
    /// <param name="expected">預期的最大 XOR 結果。</param>
    /// <param name="inputDescription">選擇性的輸入說明；未提供時會格式化 <paramref name="nums"/>。</param>
    private static void RunCase(string name, int[] nums, int expected, string? inputDescription = null)
    {
        int actual = FindMaximumXOR(nums);
        bool passed = expected == actual;
        string input = inputDescription ?? FormatInput(nums);

        Console.WriteLine($"Case {s_checks + 1}: {name}");
        Console.WriteLine($"Input: {input}");
        RecordCheck(passed);
        Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | Expected: {expected} | Actual: {actual}");
        Console.WriteLine();
    }

    /// <summary>
    /// 記錄單一驗收結果，遞增總檢查數，並在通過時遞增通過數。
    /// </summary>
    /// <param name="passed">此筆測資是否通過預期值比對。</param>
    private static void RecordCheck(bool passed)
    {
        s_checks++;

        if (passed)
        {
            s_passed++;
        }
    }

    /// <summary>
    /// 將整數序列格式化為驗收 harness 輸出使用的中括號清單字串。
    /// </summary>
    /// <param name="values">要顯示的整數序列。</param>
    /// <returns>以逗號分隔且以中括號包覆的輸入表示字串。</returns>
    private static string FormatInput(IEnumerable<int> values)
    {
        return $"[{string.Join(", ", values)}]";
    }
}
