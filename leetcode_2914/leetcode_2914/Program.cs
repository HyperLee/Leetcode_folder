namespace leetcode_2914;

using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

class Program
{
    /// <summary>
    /// 2914. Minimum Number of Changes to Make Binary String Beautiful
    /// https://leetcode.com/problems/minimum-number-of-changes-to-make-binary-string-beautiful/description/
    /// 2914. 使二進位字串變美麗的最少修改次數
    /// https://leetcode.cn/problems/minimum-number-of-changes-to-make-binary-string-beautiful/description/
    ///
    /// English
    /// You are given a 0-indexed binary string s having an even length.
    /// A string is beautiful if it's possible to partition it into one or more substrings such that:
    /// Each substring has an even length.
    /// Each substring contains only 1's or only 0's.
    /// You can change any character in s to 0 or 1.
    /// Return the minimum number of changes required to make the string s beautiful.
    ///
    /// 繁體中文
    /// 給定一個從索引 0 開始、長度為偶數的二進位字串 s。
    /// 如果能將一個字串分割成一個或多個符合下列條件的子字串，這個字串就是美麗的。
    /// 每個子字串的長度都是偶數。
    /// 每個子字串都只包含 1，或只包含 0。
    /// 你可以將 s 中的任意字元改為 0 或 1。
    /// 請回傳使字串 s 成為美麗字串所需的最少修改次數。
    /// </summary>
    /// <param name="args"></param>
    /// <remarks>
    /// 執行內建的二進位字串範例，核對最少修改次數。無需傳入命令列引數。
    /// 逐筆輸出三種解法的結果、預期值和通過狀態。任一結果不符時，程序以非零狀態碼結束。
    /// </remarks>
    static void Main(string[] args)
    {
        (string Input, int Expected, string? Name)[] examples =
        [
            ("1001", 2, null),
            ("10", 1, null),
            ("0000", 0, null),
            ("1100", 0, null),
            ("00100111", 2, null),
            ("0010011110", 3, null),
            (string.Concat(Enumerable.Repeat("01", 50_000)), 50_000, "重複 01")
        ];

        Program solution = new Program();

        foreach ((string input, int expected, string? name) in examples)
        {
            int greedy = solution.MinChanges(input);
            int xor = solution.MinChangesXor(input);
            int simd = solution.MinChangesSimd(input);
            bool passed = greedy == expected && xor == expected && simd == expected;
            string status = passed ? "PASS" : "FAIL";
            string label = name is null ? $"s = \"{input}\"" : $"{name}（長度 {input.Length}）";
            Console.WriteLine($"{label} | greedy = {greedy} | xor = {xor} | simd = {simd} | expected = {expected} | {status}");

            if (!passed)
            {
                Environment.ExitCode = 1;
            }
        }
    }

    /// <summary>
    /// 計算將偶數長度的二進位字串變成美麗字串所需的最少修改次數。
    /// 以貪心法從索引 0 起每兩個字元配成一組，每組字元不同時累計一次修改。
    /// </summary>
    /// <param name="s">長度介於 2 與 100,000 之間且為偶數，只包含 0 和 1 的字串。</param>
    /// <returns>使所有固定配對內的字元相同所需的最少修改次數。</returns>
    public int MinChanges(string s)
    {
        int changes = 0;

        // 每段長度是偶數，因此分段邊界都在偶數索引，固定配對不會跨段。
        for (int i = 0; i < s.Length; i += 2)
        {
            if (s[i] != s[i + 1])
            {
                changes++;
            }
        }

        return changes;
    }

    /// <summary>
    /// 逐組對兩個二進位字元做 XOR，相同得 0，不同得 1。
    /// </summary>
    public int MinChangesXor(string s)
    {
        int changes = 0;

        for (int i = 0; i < s.Length; i += 2)
        {
            changes += s[i] ^ s[i + 1];
        }

        return changes;
    }

    /// <summary>
    /// 使用 128 位元向量每批比較四組配對，剩餘配對以逐組 XOR 處理。
    /// </summary>
    public int MinChangesSimd(string s)
    {
        if (!Vector128.IsHardwareAccelerated)
        {
            return MinChangesXor(s);
        }

        // 每個 uint 包含兩個 UTF-16 字元。只重新解讀記憶體，無須複製。
        ReadOnlySpan<uint> pairs = MemoryMarshal.Cast<char, uint>(s.AsSpan());
        int pairsPerVector = Vector128<uint>.Count;
        Vector128<uint> lowBitMask = Vector128.Create(1u);
        Vector128<uint> totals = Vector128<uint>.Zero;
        int pairIndex = 0;

        for (; pairIndex <= pairs.Length - pairsPerVector; pairIndex += pairsPerVector)
        {
            Vector128<uint> packedPairs = Vector128.Create(pairs.Slice(pairIndex, pairsPerVector));
            Vector128<uint> upperCharacters = Vector128.ShiftRightLogical(packedPairs, 16);
            Vector128<uint> differences = Vector128.Xor(packedPairs, upperCharacters);
            totals += Vector128.BitwiseAnd(differences, lowBitMask);
        }

        int changes = (int)Vector128.Sum(totals);

        for (int i = pairIndex * 2; i < s.Length; i += 2)
        {
            changes += s[i] ^ s[i + 1];
        }

        return changes;
    }
}
