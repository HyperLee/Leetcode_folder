namespace leetcode_274;

class Program
{
    /// <summary>
    /// 274. H-Index
    /// https://leetcode.com/problems/h-index/description/
    ///
    /// English:
    /// Given an array of integers citations where citations[i] is the number of citations a researcher received for their ith paper, return the researcher's h-index.
    /// According to the definition of h-index on Wikipedia: The h-index is defined as the maximum value of h such that the given researcher has published at least h papers that have each been cited at least h times.
    ///
    /// 274. H 指数
    /// https://leetcode.cn/problems/h-index/description/
    ///
    /// 繁體中文:
    /// 給定一個整數陣列 citations，其中 citations[i] 表示研究者第 i 篇論文被引用的次數，請回傳這位研究者的 h-index。
    /// 根據 Wikipedia 上對 h-index 的定義：h-index 是滿足以下條件的最大值 h：該研究者至少發表了 h 篇論文，而且這 h 篇論文中的每一篇都至少被引用了 h 次。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solver = new Program();

        Console.WriteLine("LeetCode 274 - H-Index");
        Console.WriteLine("================================");
        Console.WriteLine();

        RunSampleCase(solver, 1, new int[] { 3, 0, 6, 1, 5 }, 3);
        RunSampleCase(solver, 2, new int[] { 1, 3, 1 }, 1);
        RunSampleCase(solver, 3, new int[] { 0 }, 0);
        RunSampleCase(solver, 4, new int[] { 100 }, 1);
        RunSampleCase(solver, 5, new int[] { 0, 1, 3, 5, 6 }, 3);
    }

    /// <summary>
    /// 使用排序法計算 h-index。
    /// 先將引用次數由小到大排序，再從最大值往前檢查目前是否仍有至少 h 篇論文的引用次數大於 h；
    /// 只要條件成立，就代表 h 還可以再增加。輸入必須是非 null 的整數陣列，元素可為 0 或更大的引用次數。
    /// 這個方法會原地排序輸入陣列，因此可能改動呼叫端資料；回傳值為該研究者可成立的最大 h-index。
    /// 時間複雜度為 O(n log n)，空間複雜度依排序實作而定。
    /// </summary>
    /// <param name="citations">每篇論文的引用次數陣列，長度可為 0，值需為非負整數。</param>
    /// <returns>滿足至少有 h 篇論文且各自引用次數至少為 h 的最大整數 h。</returns>
    public int HIndex(int[] citations)
    {
        Array.Sort(citations);
        int h = 0;
        int i = citations.Length - 1;

        // 從最大引用次數往回檢查，只要目前論文的引用數仍大於 h，就能再多支撐一篇進入 h-index。
        while (i >= 0 && citations[i] > h)
        {
            h++;
            i--;
        }

        return h;
    }

    /// <summary>
    /// 使用二分搜尋計算 h-index。
    /// 針對答案 h 的範圍 [0, n] 做搜尋，每次檢查是否至少有 mid 篇論文的引用次數大於等於 mid，
    /// 若成立就代表答案至少可以達到 mid，否則必須往更小的區間收斂。輸入必須是非 null 的整數陣列，
    /// 元素可為 0 或更大的引用次數；回傳值為可成立的最大 h-index。時間複雜度為 O(n log n)，空間複雜度為 O(1)。
    /// </summary>
    /// <param name="citations">每篇論文的引用次數陣列，長度可為 0，值需為非負整數。</param>
    /// <returns>滿足至少有 h 篇論文且各自引用次數至少為 h 的最大整數 h。</returns>
    public int HIndex2(int[] citations)
    {
        int left = 0;
        int right = citations.Length;
        int mid = 0;
        int count = 0;

        while (left < right)
        {
            // 取上中位數，避免 left 與 right 相鄰時因為偏左取整而卡住。
            mid = (left + right + 1) >> 1;
            count = 0;

            for (int i = 0; i < citations.Length; i++)
            {
                if (citations[i] >= mid)
                {
                    count++;
                }
            }

            // 如果已有至少 mid 篇論文達標，答案就在右半邊；否則必須把上界縮小。
            if (count >= mid)
            {
                left = mid;
            }
            else
            {
                right = mid - 1;
            }
        }

        return left;
    }

    /// <summary>
    /// 使用計數桶計算 h-index。
    /// 因為 h-index 不可能超過論文總數 n，所以先把所有引用次數壓縮到 [0, n] 的桶子內，
    /// 再從高到低累加「至少被引用 i 次的論文數」，第一個滿足累計篇數大於等於 i 的位置就是答案。
    /// 輸入必須是非 null 的整數陣列，元素可為 0 或更大的引用次數；回傳值為可成立的最大 h-index。
    /// 時間複雜度為 O(n)，空間複雜度為 O(n)。
    /// </summary>
    /// <param name="citations">每篇論文的引用次數陣列，長度可為 0，值需為非負整數。</param>
    /// <returns>滿足至少有 h 篇論文且各自引用次數至少為 h 的最大整數 h。</returns>
    public int HIndex3(int[] citations)
    {
        int n = citations.Length;
        int totalPapers = 0;
        int[] counter = new int[n + 1];

        for (int i = 0; i < n; i++)
        {
            // 超過 n 的引用次數對 h-index 只等同於「至少 n 次」，所以全部壓到最後一個桶即可。
            if (citations[i] >= n)
            {
                counter[n]++;
            }
            else
            {
                counter[citations[i]]++;
            }
        }

        for (int i = n; i >= 0; i--)
        {
            // 反向累加後，totalPapers 代表「至少被引用 i 次」的論文總數，第一次達標就是最大 h。
            totalPapers += counter[i];
            if (totalPapers >= i)
            {
                return i;
            }
        }

        return 0;
    }

    /// <summary>
    /// 執行單筆範例資料，分別呼叫三種 h-index 解法並輸出結果。
    /// 為避免排序解法改動原始輸入，三個方法都會使用複製後的陣列。輸入需提供案例編號、引用次數陣列與預期答案，
    /// 輸出為主控台上的案例摘要與每種解法的 PASS/FAIL 驗證結果，方便直接對照 README 範例。
    /// </summary>
    /// <param name="solver">包含三種 h-index 解法的程式實例。</param>
    /// <param name="caseNumber">目前輸出的案例編號。</param>
    /// <param name="citations">要驗證的引用次數陣列。</param>
    /// <param name="expected">此案例預期的 h-index。</param>
    private static void RunSampleCase(Program solver, int caseNumber, int[] citations, int expected)
    {
        int sortingResult = solver.HIndex((int[])citations.Clone());
        int binarySearchResult = solver.HIndex2((int[])citations.Clone());
        int countingResult = solver.HIndex3((int[])citations.Clone());

        Console.WriteLine($"Case {caseNumber}");
        Console.WriteLine($"Input: [{string.Join(", ", citations)}]");
        Console.WriteLine($"Expected: {expected}");
        Console.WriteLine($"HIndex  (Sort)           : {sortingResult} {(sortingResult == expected ? "PASS" : "FAIL")}");
        Console.WriteLine($"HIndex2 (Binary Search)  : {binarySearchResult} {(binarySearchResult == expected ? "PASS" : "FAIL")}");
        Console.WriteLine($"HIndex3 (Counting)       : {countingResult} {(countingResult == expected ? "PASS" : "FAIL")}");
        Console.WriteLine();
    }
}
