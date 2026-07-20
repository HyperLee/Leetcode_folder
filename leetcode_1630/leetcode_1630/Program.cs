namespace leetcode_1630;

class Program
{
    /// <summary>
    /// <para><b>1630. Arithmetic Subarrays</b></para>
    /// <para>https://leetcode.com/problems/arithmetic-subarrays/description/</para>
    /// <para><b>English</b></para>
    /// <para>
    /// A sequence of numbers is called arithmetic if it consists of at least two elements,
    /// and the difference between every two consecutive elements is the same. More formally,
    /// a sequence <c>s</c> is arithmetic if and only if
    /// <c>s[i+1] - s[i] == s[1] - s[0]</c> for all valid <c>i</c>.
    /// </para>
    /// <para>For example, these are arithmetic sequences:</para>
    /// <list type="bullet">
    /// <item><description><c>1, 3, 5, 7, 9</c></description></item>
    /// <item><description><c>7, 7, 7, 7</c></description></item>
    /// <item><description><c>3, -1, -5, -9</c></description></item>
    /// </list>
    /// <para>The following sequence is not arithmetic: <c>1, 1, 2, 5, 7</c>.</para>
    /// <para>
    /// You are given an array of <c>n</c> integers, <c>nums</c>, and two arrays of <c>m</c>
    /// integers each, <c>l</c> and <c>r</c>, representing the <c>m</c> range queries, where
    /// the <c>i</c>th query is the range <c>[l[i], r[i]]</c>. All the arrays are 0-indexed.
    /// </para>
    /// <para>
    /// Return a list of boolean elements <c>answer</c>, where <c>answer[i]</c> is <c>true</c>
    /// if the subarray <c>nums[l[i]], nums[l[i]+1], ... , nums[r[i]]</c> can be rearranged to
    /// form an arithmetic sequence, and <c>false</c> otherwise.
    /// </para>
    /// <para><b>1630. 等差子陣列</b></para>
    /// <para>https://leetcode.cn/problems/arithmetic-subarrays/description/</para>
    /// <para><b>繁體中文</b></para>
    /// <para>
    /// 如果一個數列至少包含兩個元素，且每兩個相鄰元素之間的差皆相同，則稱此數列為等差數列。
    /// 更正式地說，若且唯若對所有有效的 <c>i</c>，數列 <c>s</c> 都滿足
    /// <c>s[i+1] - s[i] == s[1] - s[0]</c>，則 <c>s</c> 為等差數列。
    /// </para>
    /// <para>例如，下列數列皆為等差數列：</para>
    /// <list type="bullet">
    /// <item><description><c>1, 3, 5, 7, 9</c></description></item>
    /// <item><description><c>7, 7, 7, 7</c></description></item>
    /// <item><description><c>3, -1, -5, -9</c></description></item>
    /// </list>
    /// <para>下列數列不是等差數列：<c>1, 1, 2, 5, 7</c>。</para>
    /// <para>
    /// 給定一個包含 <c>n</c> 個整數的陣列 <c>nums</c>，以及兩個各自包含 <c>m</c> 個整數的陣列
    /// <c>l</c> 和 <c>r</c>，用來表示 <c>m</c> 個範圍查詢；其中第 <c>i</c> 個查詢的範圍為
    /// <c>[l[i], r[i]]</c>。所有陣列皆採用從 0 開始的索引。
    /// </para>
    /// <para>
    /// 回傳一個布林值串列 <c>answer</c>。如果子陣列
    /// <c>nums[l[i]], nums[l[i]+1], ... , nums[r[i]]</c> 能重新排列成等差數列，則
    /// <c>answer[i]</c> 為 <c>true</c>；否則為 <c>false</c>。
    /// </para>
    /// </summary>
    /// <param name="args">Command-line arguments (not used).</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
