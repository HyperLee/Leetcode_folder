namespace leetcode_2948;

class Program
{
    /// <summary>
    /// 2948. Make Lexicographically Smallest Array by Swapping Elements
    /// https://leetcode.com/problems/make-lexicographically-smallest-array-by-swapping-elements/description
    /// 2948. 交換得到字典序最小的陣列
    /// https://leetcode.cn/problems/make-lexicographically-smallest-array-by-swapping-elements
    /// <para>English problem statement:</para>
    /// <para>Given a 0-indexed array of positive integers <c>nums</c> and a positive integer <c>limit</c>.</para>
    /// <para>In one operation, you can choose any two indices <c>i</c> and <c>j</c> and swap <c>nums[i]</c> and <c>nums[j]</c> if |<c>nums[i]</c> - <c>nums[j]</c>| &lt;= <c>limit</c>.</para>
    /// <para>Return the lexicographically smallest array that can be obtained by performing the operation any number of times.</para>
    /// <para>An array <c>a</c> is lexicographically smaller than an array <c>b</c> if in the first position where they differ, <c>a</c> has an element that is less than the corresponding element in <c>b</c>. For example, the array <c>[2,10,3]</c> is lexicographically smaller than <c>[10,2,3]</c> because they differ at index 0 and <c>2 &lt; 10</c>.</para>
    /// <para>繁體中文題目描述：</para>
    /// <para>給定一個以 0 為起始索引的正整數陣列 <c>nums</c>，以及一個正整數 <c>limit</c>。</para>
    /// <para>在一次操作中，你可以選擇任意兩個索引 <c>i</c> 和 <c>j</c>；如果 |<c>nums[i]</c> - <c>nums[j]</c>| &lt;= <c>limit</c>，就交換 <c>nums[i]</c> 與 <c>nums[j]</c>。</para>
    /// <para>請回傳經過任意次操作後可以得到的字典序最小陣列。</para>
    /// <para>若陣列 <c>a</c> 與陣列 <c>b</c> 在第一個不同的位置上，<c>a</c> 的元素小於 <c>b</c> 對應位置的元素，則稱 <c>a</c> 的字典序小於 <c>b</c>。例如，陣列 <c>[2,10,3]</c> 的字典序小於 <c>[10,2,3]</c>，因為它們在索引 0 的位置不同，且 <c>2 &lt; 10</c>。</para>
    /// </summary>
    /// <param name="args">Command-line arguments (unused).</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}