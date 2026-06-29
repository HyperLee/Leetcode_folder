using System.Text;

namespace leetcode_228;

class Program
{
    /// <summary>
    /// 228. Summary Ranges
    /// https://leetcode.com/problems/summary-ranges/description/
    /// 228. 彙總區間
    /// https://leetcode.cn/problems/summary-ranges/description/
    ///
    /// English original:
    /// You are given a sorted unique integer array nums.
    ///
    /// A range [a,b] is the set of all integers from a to b (inclusive).
    ///
    /// Return the smallest sorted list of ranges that cover all the numbers in the array exactly.
    /// That is, each element of nums is covered by exactly one of the ranges, and there is no integer x
    /// such that x is in one of the ranges but not in nums.
    ///
    /// Each range [a,b] in the list should be output as:
    ///
    /// "a->b" if a != b
    /// "a" if a == b
    ///
    /// 繁體中文：
    /// 給定一個已排序且所有元素皆唯一的整數陣列 nums。
    ///
    /// 範圍 [a,b] 是從 a 到 b 的所有整數集合（包含 a 與 b）。
    ///
    /// 請回傳最小的已排序範圍列表，使其能精確涵蓋陣列中的所有數字。
    /// 也就是說，nums 中每個元素都只會被一個範圍涵蓋一次，且不存在任何整數 x
    /// 屬於某個範圍但不屬於 nums。
    ///
    /// 列表中的每個範圍 [a,b] 應輸出為：
    ///
    /// 若 a != b，輸出 "a->b"
    /// 若 a == b，輸出 "a"
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 使用固定範例執行 Summary Ranges 解法，涵蓋題目範例、空陣列、單一元素、負數區間與整數邊界。
    /// 每筆輸入都會列出預期與實際輸出，方便從主程式直接驗證目前解法。
    /// </summary>
    private static void RunSamples()
    {
        SampleCase[] samples = new SampleCase[]
        {
            new SampleCase(
                "LeetCode 範例 1：多段連續區間",
                new int[] { 0, 1, 2, 4, 5, 7 },
                new string[] { "0->2", "4->5", "7" }),
            new SampleCase(
                "LeetCode 範例 2：單點與短區間交錯",
                new int[] { 0, 2, 3, 4, 6, 8, 9 },
                new string[] { "0", "2->4", "6", "8->9" }),
            new SampleCase(
                "空陣列：沒有任何範圍",
                Array.Empty<int>(),
                Array.Empty<string>()),
            new SampleCase(
                "單一元素：輸出單點",
                new int[] { 5 },
                new string[] { "5" }),
            new SampleCase(
                "負數區間：跨越 0 的連續段",
                new int[] { -3, -2, -1, 1, 2, 4 },
                new string[] { "-3->-1", "1->2", "4" }),
            new SampleCase(
                "int.MinValue 邊界：仍可正確串接連續值",
                new int[] { int.MinValue, int.MinValue + 1, -1, 0, 2 },
                new string[] { "-2147483648->-2147483647", "-1->0", "2" }),
        };

        Program solution = new Program();
        int passedCount = 0;

        Console.WriteLine("Summary Ranges sample verification");
        Console.WriteLine();

        for (int index = 0; index < samples.Length; index++)
        {
            SampleCase sample = samples[index];
            IList<string> actual = solution.SummaryRanges(sample.Input);
            bool passed = actual.SequenceEqual(sample.Expected);

            if (passed)
            {
                passedCount++;
            }

            Console.WriteLine($"Case {index + 1}: {sample.Name}");
            Console.WriteLine($"Input: {FormatIntArray(sample.Input)}");
            Console.WriteLine($"Expected: {FormatStringList(sample.Expected)}");
            Console.WriteLine($"Actual: {FormatStringList(actual)}");
            Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"Passed {passedCount}/{samples.Length} cases.");
    }

    /// <summary>
    /// 將已排序且不重複的整數陣列整理成最小範圍列表。
    /// 解題概念是以雙指標固定區間左端，再往右延伸到連續數字中斷為止；
    /// 輸入需符合題目條件，輸出會以 "a" 或 "a-&gt;b" 表示每個範圍。
    /// </summary>
    /// <param name="nums">已排序且所有元素唯一的整數陣列。</param>
    /// <returns>能精確涵蓋所有輸入數字的最小排序範圍列表。</returns>
    public IList<string> SummaryRanges(int[] nums)
    {
        List<string> result = new List<string>();
        int start = 0;

        while (start < nums.Length)
        {
            int end = start;

            // 右指標持續延伸目前區間，直到下一個數字不再剛好大 1。
            while (end + 1 < nums.Length && (long)nums[end] + 1L == nums[end + 1])
            {
                end++;
            }

            // 完成一段最長連續區間後立即輸出，再從下一個位置開始。
            result.Add(BuildRange(nums[start], nums[end]));
            start = end + 1;
        }

        return result;
    }

    /// <summary>
    /// 依題目格式建立單一範圍字串；起終點相同時輸出單一數字，否則輸出 "start-&gt;end"。
    /// </summary>
    /// <param name="start">範圍起點。</param>
    /// <param name="end">範圍終點。</param>
    /// <returns>符合題目要求的範圍表示字串。</returns>
    private static string BuildRange(int start, int end)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(start);

        if (start != end)
        {
            builder.Append("->").Append(end);
        }

        return builder.ToString();
    }

    /// <summary>
    /// 將整數陣列格式化為 README 與主程式範例使用的顯示字串。
    /// </summary>
    /// <param name="values">要顯示的整數集合。</param>
    /// <returns>例如 [0, 1, 2] 的字串。</returns>
    private static string FormatIntArray(IEnumerable<int> values)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append('[');

        bool isFirst = true;
        foreach (int value in values)
        {
            if (!isFirst)
            {
                builder.Append(", ");
            }

            builder.Append(value);
            isFirst = false;
        }

        builder.Append(']');
        return builder.ToString();
    }

    /// <summary>
    /// 將範圍字串集合格式化為 README 與主程式範例使用的顯示字串。
    /// </summary>
    /// <param name="values">要顯示的範圍字串集合。</param>
    /// <returns>例如 ["0-&gt;2", "4"] 的字串。</returns>
    private static string FormatStringList(IEnumerable<string> values)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append('[');

        bool isFirst = true;
        foreach (string value in values)
        {
            if (!isFirst)
            {
                builder.Append(", ");
            }

            builder.Append('"').Append(value).Append('"');
            isFirst = false;
        }

        builder.Append(']');
        return builder.ToString();
    }

    /// <summary>
    /// 表示一筆主程式範例，包含案例名稱、輸入陣列與預期範圍列表。
    /// </summary>
    /// <param name="Name">案例名稱與測試重點。</param>
    /// <param name="Input">符合題目條件的已排序不重複整數陣列。</param>
    /// <param name="Expected">預期輸出的範圍字串列表。</param>
    private sealed record SampleCase(string Name, int[] Input, string[] Expected);
}
