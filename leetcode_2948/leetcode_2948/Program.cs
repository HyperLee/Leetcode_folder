namespace leetcode_2948;

class Program
{
    /// <summary>
    /// 2948. Make Lexicographically Smallest Array by Swapping Elements
    /// https://leetcode.com/problems/make-lexicographically-smallest-array-by-swapping-elements/description
    /// 2948. 交換得到字典序最小的陣列
    /// https://leetcode.cn/problems/make-lexicographically-smallest-array-by-swapping-elements
    /// English problem statement:
    /// Given a 0-indexed array of positive integers nums and a positive integer limit.
    /// In one operation, you can choose any two indices i and j and swap nums[i] and nums[j] if |nums[i] - nums[j]| <= limit.
    /// Return the lexicographically smallest array that can be obtained by performing the operation any number of times.
    /// An array a is lexicographically smaller than an array b if in the first position where they differ, a has an element that is less than the corresponding element in b. For example, the array [2,10,3] is lexicographically smaller than [10,2,3] because they differ at index 0 and 2 &lt; 10.
    /// 繁體中文題目描述：
    /// 給定一個以 0 為起始索引的正整數陣列 nums，以及一個正整數 limit。
    /// 在一次操作中，你可以選擇任意兩個索引 i 和 j；如果 |nums[i] - nums[j]| <= limit，就交換 nums[i] 與 nums[j]。
    /// 請回傳經過任意次操作後可以得到的字典序最小陣列。
    /// 若陣列 a 與陣列 b 在第一個不同的位置上，a 的元素小於 b 對應位置的元素，則稱 a 的字典序小於 b。例如，陣列 [2,10,3] 的字典序小於 [10,2,3]，因為它們在索引 0 的位置不同，且 2 < 10。
    /// </summary>
    /// <param name="args">Command-line arguments (unused).</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一：排序
    /// 
    /// 在滿足交換條件的情況下，將 <paramref name="nums"/> 重新排列成字典序最小的陣列。
    ///
    /// 若兩個元素的值差不超過 <paramref name="limit"/>，則可以交換它們的位置。
    /// 由於交換次數與順序不限，只要多個元素之間能透過合法交換間接連接，
    /// 位於同一連通塊中的元素就可以任意重新排列。
    ///
    /// 為了得到字典序最小的結果，對每個連通塊：
    /// 1. 找出該連通塊所有元素的原始下標。
    /// 2. 將連通塊內的元素值依非遞減順序排列。
    /// 3. 將較小的元素依序放回較小的原始下標。
    /// </summary>
    /// <param name="nums">
    /// 原始整數陣列。
    /// </param>
    /// <param name="limit">
    /// 允許交換的最大差值。
    /// 當兩個元素滿足
    /// <c>|nums[i] - nums[j]| &lt;= limit</c>
    /// 時，可以交換它們的位置。
    /// </param>
    /// <returns>
    /// 經過任意次合法交換後，可以得到的字典序最小陣列。
    /// </returns>
    /// <remarks>
    /// <para>
    /// <b>核心概念：連通塊</b>
    /// </para>
    ///
    /// <para>
    /// 如果元素 x 可以與 y 交換，而 y 可以與 z 交換，
    /// 即使 x 與 z 無法直接交換，也可以透過 y 間接完成 x 與 z 的位置交換。
    /// 因此，可以把每個元素視為一個節點，符合交換條件的元素之間建立一條邊。
    /// 位於同一連通塊中的元素，可以透過若干次合法交換任意重新排列；
    /// 不同連通塊之間則無法交換。
    /// </para>
    ///
    /// <para>
    /// <b>如何有效找出連通塊</b>
    /// </para>
    ///
    /// <para>
    /// 若直接比較所有元素是否可以交換，需要檢查所有元素對，
    /// 時間複雜度為 <c>O(n²)</c>。
    /// </para>
    ///
    /// <para>
    /// 將元素按照值由小到大排序後，設排序結果為：
    /// </para>
    ///
    /// <code>
    /// v[0] &lt;= v[1] &lt;= ... &lt;= v[n - 1]
    /// </code>
    ///
    /// <para>
    /// 對於排序後相鄰的兩個元素：
    /// </para>
    ///
    /// <code>
    /// v[i] - v[i - 1] &lt;= limit
    /// </code>
    ///
    /// <para>
    /// 則兩者屬於同一個連通塊。
    /// </para>
    ///
    /// <para>
    /// 如果：
    /// </para>
    ///
    /// <code>
    /// v[i] - v[i - 1] &gt; limit
    /// </code>
    ///
    /// <para>
    /// 因為陣列已排序，所以對所有 <c>j &lt; i</c> 都有：
    /// </para>
    ///
    /// <code>
    /// v[i] - v[j] &gt; limit
    /// </code>
    ///
    /// <para>
    /// 因此 <c>v[i]</c> 不可能與左側任何元素建立交換關係，
    /// 連通塊一定會在這個位置分裂。
    /// </para>
    ///
    /// <para>
    /// 所以每個連通塊在排序後的陣列中一定是一段連續區間，
    /// 只需要比較相鄰元素的差值即可完成連通塊劃分，
    /// 不需要真的建立圖或執行 DFS / BFS。
    /// </para>
    ///
    /// <para>
    /// 排序時必須同時保留每個元素的原始下標，
    /// 因為找到連通塊後，仍需要把排序後的元素值放回對應的原始位置。
    /// </para>
    ///
    /// <para>
    /// <b>時間複雜度：</b>
    /// 排序需要 <c>O(n log n)</c>，掃描與重建答案需要 <c>O(n)</c>，
    /// 因此總時間複雜度為 <c>O(n log n)</c>。
    /// </para>
    ///
    /// <para>
    /// <b>空間複雜度：</b>
    /// 需要額外保存排序後的元素、原始下標以及答案，
    /// 空間複雜度為 <c>O(n)</c>。
    /// </para>
    /// </remarks>
    /// <param name="nums"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public int[] LexicographicallySmallestArray(int[] nums, int limit)
    {
        int n = nums.Length;
        int[] ans = new int[n];

        // 將元素值與原下標綁定
        List<(int value, int index)> arr = new ();
        for(int i = 0; i < n; i++)
        {
            arr.Add((nums[i], i));
        }

        // 按照元素升序排序
        arr.Sort((a, b) => a.value.CompareTo(b.value));

        List<int> values = new();
        List<int> indices = new();

        foreach(var p in arr)
        {
            values.Add(p.value);
            indices.Add(p.index);
        }

        int ptr = 0;
        while(ptr < n)
        {
            int start = ptr;

            //當前聯通塊中的原下標
            List<int> groupIndices = new();

            // 當前聯通塊中的元素值
            List<int> groupValues = new();

            while (ptr < n && (ptr == start || values[ptr] - values[ptr - 1] <= limit)) 
            {
                groupIndices.Add(indices[ptr]);
                groupValues.Add(values[ptr]);
                ptr++;
            }

            // 由於元素值數組已經有序. 這裡不需要在排序
            groupIndices.Sort();

            // 為得到字典序最小的結果, 將較小元素放到較小下標處
            for(int k = 0; k < groupIndices.Count; k++)
            {
                ans[groupIndices[k]] = groupValues[k];
            }
        }
        return ans;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="limit"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public int[] LexicographicallySmallestArray2(int[] nums, int limit)
    {
        int n = nums.Length;

        int[] idx = new int[n];

        for (int i = 0; i < n; i++)
        {
            idx[i] = i;
        }

        Array.Sort(idx, (i, j) => nums[i].CompareTo(nums[j]));

        int[] ans = new int[n];

        for (int i = 0; i < n;)
        {
            int j = i + 1;

            while (j < n &&
                   nums[idx[j]] - nums[idx[j - 1]] <= limit)
            {
                j++;
            }

            int[] t = idx[i..j];

            Array.Sort(t);

            for (int k = i; k < j; k++)
            {
                ans[t[k - i]] = nums[idx[k]];
            }

            i = j;
        }

        return ans;        
    }
}