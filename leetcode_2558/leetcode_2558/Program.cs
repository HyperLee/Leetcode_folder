namespace leetcode_2558;

class Program
{
    /// <summary>
    /// 2558. Take Gifts From the Richest Pile
    /// https://leetcode.com/problems/take-gifts-from-the-richest-pile/description/
    ///
    /// You are given an integer array gifts denoting the number of gifts in various piles.
    /// Every second, you do the following:
    ///
    /// Choose the pile with the maximum number of gifts.
    /// If there is more than one pile with the maximum number of gifts, choose any.
    /// Reduce the number of gifts in the pile to the floor of the square root of the
    /// original number of gifts in the pile.
    ///
    /// Return the number of gifts remaining after k seconds.
    ///
    /// 給定一個整數陣列 gifts，表示各個禮物堆中的禮物數量。
    /// 每一秒，你會執行以下操作：
    ///
    /// 選擇禮物數量最多的一堆。
    /// 如果有多堆的禮物數量並列最多，可以任選其中一堆。
    /// 將該堆的禮物數量減少為原禮物數量平方根的向下取整值。
    ///
    /// 回傳經過 k 秒後剩餘的禮物總數。
    ///
    /// 2558. 從禮物最多的堆中拿取禮物
    /// https://leetcode.cn/problems/take-gifts-from-the-richest-pile/description/
    /// </summary>
    /// <remarks>
    /// 程式進入點會執行固定案例，比較三種解法的結果並輸出 PASS/FAIL。
    /// </remarks>
    /// <param name="args">命令列參數；本範例不使用此參數。</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        (string Name, int[] Gifts, int K, long Expected)[] testCases =
        [
            ("Official example 1", [25, 64, 9, 4, 100], 4, 29),
            ("Official example 2", [1, 1, 1, 1], 4, 4),
            ("Single pile", [100], 1, 10),
            ("Repeated maximums", [16, 16], 2, 8),
            ("Zero operations", [9, 4], 0, 13)
        ];

        foreach ((string name, int[] gifts, int k, long expected) in testCases)
        {
            RunTestCase(solution, name, gifts, k, expected);
        }
    }

    /// <summary>
    /// 執行單一固定案例，分別呼叫三種取禮物解法，並比較實際結果與預期結果。
    /// 每次呼叫前都複製輸入陣列，避免會修改陣列的解法影響後續解法。
    /// 輸入應符合題目的禮物堆數量與數值限制；此方法將比較結果輸出至主控台，
    /// 不回傳資料。
    /// </summary>
    /// <param name="solution">包含三種解法的 <see cref="Program"/> 實例。</param>
    /// <param name="name">顯示於主控台的案例名稱。</param>
    /// <param name="gifts">各堆禮物數量；方法不會修改此原始陣列。</param>
    /// <param name="k">執行選取最大禮物堆的次數。</param>
    /// <param name="expected">執行 <paramref name="k"/> 次後預期的禮物總數。</param>
    private static void RunTestCase(
        Program solution,
        string name,
        int[] gifts,
        int k,
        long expected)
    {
        long scanResult = solution.PickGifts(gifts.ToArray(), k);
        long sortingResult = solution.PickGifts2(gifts.ToArray(), k);
        long priorityQueueResult = solution.PickGifts3(gifts.ToArray(), k);

        Console.WriteLine($"{name}: gifts = [{string.Join(", ", gifts)}], k = {k}, Expected = {expected}");
        Console.WriteLine($"  PickGifts:  Actual = {scanResult} ({(scanResult == expected ? "PASS" : "FAIL")})");
        Console.WriteLine($"  PickGifts2: Actual = {sortingResult} ({(sortingResult == expected ? "PASS" : "FAIL")})");
        Console.WriteLine($"  PickGifts3: Actual = {priorityQueueResult} ({(priorityQueueResult == expected ? "PASS" : "FAIL")})");
        Console.WriteLine();
    }

    /// <summary>
    /// 以線性掃描模擬每一秒的取禮物操作。每輪先用 <see cref="Enumerable.Max(IEnumerable{int})"/>
    /// 找出最大禮物數，再用 <see cref="Array.IndexOf{T}(T[], T)"/> 找到其中一堆並替換為
    /// 平方根向下取整值。輸入陣列必須至少包含一堆正整數禮物，<paramref name="k"/>
    /// 應為非負整數；方法會直接修改 <paramref name="gifts"/>，最後回傳剩餘禮物總數。
    /// 時間複雜度為 O(k × n)，額外空間複雜度為 O(1)。
    /// </summary>
    /// <param name="gifts">各堆禮物數量；方法執行後內容會反映縮減結果。</param>
    /// <param name="k">選取並縮減最大禮物堆的次數。</param>
    /// <returns>執行 <paramref name="k"/> 次操作後，所有禮物堆的數量總和。</returns>
    public long PickGifts(int[] gifts, int k)
    {
        int index = 0;
        long res = 0;

        for (int i = 0; i < k; i++)
        {
            // 題目允許最大值並列時任選一堆，因此取第一個最大值的位置即可。
            index = Array.IndexOf(gifts, gifts.Max());

            // 正數轉成 int 會截去小數部分，等同題目要求的平方根向下取整。
            gifts[index] = (int)Math.Sqrt(gifts[index]);
        }

        for (int i = 0; i < gifts.Length; i++)
        {
            res += gifts[i];
        }

        return res;
    }

    /// <summary>
    /// 以完整排序模擬每一秒的取禮物操作。每輪將陣列由大到小排列，使最大禮物堆
    /// 位於索引 0，再將該值替換為平方根向下取整值。輸入陣列必須至少包含一堆
    /// 正整數禮物，<paramref name="k"/> 應為非負整數；方法會排序並直接修改
    /// <paramref name="gifts"/>，最後回傳剩餘禮物總數。時間複雜度為
    /// O(k × n log n)，排序所需的額外呼叫堆疊空間為 O(log n)。
    /// </summary>
    /// <param name="gifts">各堆禮物數量；方法執行後順序與內容都可能改變。</param>
    /// <param name="k">選取並縮減最大禮物堆的次數。</param>
    /// <returns>執行 <paramref name="k"/> 次操作後，所有禮物堆的數量總和。</returns>
    public long PickGifts2(int[] gifts, int k)
    {
        for (; 0 < k; k--)
        {
            // Array.Sort 預設由小到大，再反轉後即可直接從索引 0 取得最大值。
            Array.Sort(gifts);
            Array.Reverse(gifts);

            gifts[0] = (int)Math.Floor(Math.Sqrt(gifts[0]));
        }

        long sum = 0;
        foreach (int gift in gifts)
        {
            sum += gift;
        }

        return sum;
    }

    /// <summary>
    /// 以優先佇列維護目前最大的禮物堆。由於 .NET 的 <see cref="PriorityQueue{TElement, TPriority}"/>
    /// 會先移除最小優先權，因此使用禮物數量的負值作為優先權，模擬最大堆；每輪取出
    /// 最大值、替換成平方根向下取整值後再放回。輸入陣列必須至少包含一堆正整數禮物，
    /// <paramref name="k"/> 應為非負整數；方法不會修改 <paramref name="gifts"/>。
    /// 時間複雜度為 O((n + k) log n)，額外空間複雜度為 O(n)。
    /// </summary>
    /// <param name="gifts">各堆禮物數量；方法只讀取此陣列。</param>
    /// <param name="k">選取並縮減最大禮物堆的次數。</param>
    /// <returns>執行 <paramref name="k"/> 次操作後，所有禮物堆的數量總和。</returns>
    public long PickGifts3(int[] gifts, int k)
    {
        PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
        foreach (int gift in gifts)
        {
            // 負值越小代表原始禮物數越大，Dequeue 因而會先取出最大禮物堆。
            pq.Enqueue(gift, -gift);
        }

        while (k > 0)
        {
            k--;
            int x = pq.Dequeue();
            pq.Enqueue((int)Math.Sqrt(x), -(int)Math.Sqrt(x));
        }

        long res = 0;
        while (pq.Count > 0)
        {
            res += pq.Dequeue();
        }

        return res;
    }
}