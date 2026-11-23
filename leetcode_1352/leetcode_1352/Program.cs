namespace leetcode_1352;

class Program
{
    /// <summary>
    /// 1352. Product of the Last K Numbers
    /// https://leetcode.com/problems/product-of-the-last-k-numbers/description/
    /// 1352. 最後 K 個數的乘積
    /// https://leetcode.cn/problems/product-of-the-last-k-numbers/description/
    ///
    /// English:
    /// Design an algorithm that accepts a stream of integers and retrieves the product of the last k integers of the stream.
    ///
    /// Implement the ProductOfNumbers class:
    /// - ProductOfNumbers() Initializes the object with an empty stream.
    /// - void add(int num) Appends the integer num to the stream.
    /// - int getProduct(int k) Returns the product of the last k numbers in the current list.
    ///   You can assume that always the current list has at least k numbers.
    ///
    /// The test cases are generated so that, at any time, the product of any contiguous sequence of numbers
    /// will fit into a single 32-bit integer without overflowing.
    ///
    /// 繁體中文：
    /// 設計一個演算法，接收整數資料流，並取得資料流中最後 k 個整數的乘積。
    ///
    /// 實作 ProductOfNumbers 類別：
    /// - ProductOfNumbers()：以空的資料流初始化物件。
    /// - void add(int num)：將整數 num 加入資料流的尾端。
    /// - int getProduct(int k)：回傳目前串列中最後 k 個數字的乘積。
    ///   你可以假設目前串列中一定至少有 k 個數字。
    ///
    /// 測試案例保證，在任何時刻，任意連續數列的乘積都能以單一 32 位元整數表示，不會溢位。
    /// </summary>
    /// <remarks>
    /// 以相同案例驗證兩種解法。成功時印出通過筆數，結果不符時將差異寫入標準錯誤。
    /// </remarks>
    /// <param name="args">命令列引數，此程式不需要參數。</param>
    /// <returns>全部通過時回傳 0，任一檢查失敗時回傳 1。</returns>
    private static int Main(string[] args)
    {
        try
        {
            int passed = RunChecks(nameof(ProductOfNumbers), () =>
            {
                ProductOfNumbers stream = new ProductOfNumbers();
                return (stream.Add, stream.GetProduct);
            });
            passed += RunChecks(nameof(ProductOfNumbers2), () =>
            {
                ProductOfNumbers2 stream = new ProductOfNumbers2();
                return (stream.Add, stream.GetProduct);
            });

            Console.WriteLine($"Passed {passed} product checks.");
            return 0;
        }
        catch (InvalidOperationException exception)
        {
            Console.Error.WriteLine(exception.Message);
            return 1;
        }
    }

    /// <summary>
    /// 使用指定解法的新資料流執行官方範例及邊界情境，並回傳通過的查詢筆數。
    /// </summary>
    /// <param name="solution">用於錯誤訊息的解法名稱。</param>
    /// <param name="createStream">每次呼叫都建立空資料流，並提供加入及查詢方法。</param>
    /// <returns>乘積符合預期的查詢筆數。</returns>
    private static int RunChecks(
        string solution,
        Func<(Action<int> Add, Func<int, int> GetProduct)> createStream)
    {
        (Action<int> Add, Func<int, int> GetProduct) officialExample = createStream();
        int passed = CheckProducts(solution, "Official example", officialExample,
            [3, 0, 2, 5, 4], (2, 20), (3, 40), (4, 0));
        passed += CheckProducts(solution, "Official example after adding 8", officialExample,
            [8], (2, 32));

        passed += CheckProducts(solution, "Single nonzero value", createStream(),
            [6], (1, 6));
        passed += CheckProducts(solution, "Single zero", createStream(),
            [0], (1, 0));
        passed += CheckProducts(solution, "Consecutive zeros", createStream(),
            [7, 0, 0, 2], (1, 2), (2, 0), (3, 0), (4, 0));
        passed += CheckProducts(solution, "Skipping ones", createStream(),
            [2, 1, 1, 3, 1], (1, 1), (2, 3), (4, 3), (5, 6));

        return passed;
    }

    /// <summary>
    /// 將測試資料加入指定資料流，再逐筆比較尾段乘積與預期值。
    /// </summary>
    /// <param name="solution">用於錯誤訊息的解法名稱。</param>
    /// <param name="scenario">用於錯誤訊息的情境名稱。</param>
    /// <param name="stream">目前資料流的加入及查詢方法。</param>
    /// <param name="numbers">依序加入資料流且符合題目限制的數字。</param>
    /// <param name="queries">尾段長度與預期乘積，每筆長度須介於 1 與加入後的資料流長度。</param>
    /// <returns>通過的查詢筆數。</returns>
    /// <exception cref="InvalidOperationException">實際乘積與預期值不符。</exception>
    private static int CheckProducts(
        string solution,
        string scenario,
        (Action<int> Add, Func<int, int> GetProduct) stream,
        int[] numbers,
        params (int K, int Expected)[] queries)
    {
        foreach (int number in numbers)
        {
            stream.Add(number);
        }

        foreach ((int k, int expected) in queries)
        {
            int actual = stream.GetProduct(k);
            if (actual != expected)
            {
                throw new InvalidOperationException(
                    $"{solution} / {scenario}: k={k}, expected {expected}, actual {actual}.");
            }
        }

        return queries.Length;
    }
}

/// <summary>
/// 保存最後一個零之後的前綴乘積，利用除法取得不含零的尾段乘積。
/// </summary>
public class ProductOfNumbers
{
    private readonly List<int> prefixProduct;

    /// <summary>
    /// 建立空資料流，並以乘法單位元素 1 表示空前綴的乘積。
    /// </summary>
    public ProductOfNumbers()
    {
        prefixProduct = new List<int>();
        prefixProduct.Add(1);
    }

    /// <summary>
    /// 將數字加入資料流尾端。遇到零時重設前綴，其餘數字延伸累積乘積。
    /// </summary>
    /// <param name="num">要加入的整數，範圍為 0 到 100。</param>
    public void Add(int num)
    {
        if (num == 0)
        {
            // 跨過這個零的尾段乘積必為 0，之後的非零前綴可從 1 重新累積。
            prefixProduct.Clear();
            prefixProduct.Add(1);
        }
        else
        {
            prefixProduct.Add(num * prefixProduct[prefixProduct.Count - 1]);
        }
    }

    /// <summary>
    /// 查詢最後 k 個數字的乘積。若尾段跨過最後一個零，回傳零，否則相除兩個前綴。
    /// </summary>
    /// <param name="k">查詢長度，介於 1 與資料流目前長度之間。</param>
    /// <returns>最後 <paramref name="k"/> 個數字的乘積。</returns>
    public int GetProduct(int k)
    {
        int n = prefixProduct.Count;

        // 清單包含空前綴，因此目前只有 n - 1 個連續的非零數字。
        if (n <= k)
        {
            return 0;
        }

        // 用完整前綴除以查詢起點前的前綴，抵銷尾段左側的因數。
        return prefixProduct[n - 1] / prefixProduct[n - 1 - k];
    }
}

/// <summary>
/// 以累計零數判斷尾段是否含零，再用索引跳躍略過不影響乘積的 1。
/// </summary>
public class ProductOfNumbers2
{
    private readonly List<int> nums;
    private readonly List<int> zeroCount;
    private readonly List<int> prevNonOne;
    private int n;

    /// <summary>
    /// 建立空資料流及兩份索引資料，分別保存累計零數與前一個大於 1 的位置。
    /// </summary>
    public ProductOfNumbers2()
    {
        nums = new List<int>();
        zeroCount = new List<int>();
        prevNonOne = new List<int>();
        n = 0;
    }

    /// <summary>
    /// 將數字加入資料流尾端，同時更新該位置的累計零數與往前跳躍的索引。
    /// </summary>
    /// <param name="num">要加入的整數，範圍為 0 到 100。</param>
    public void Add(int num)
    {
        n++;
        nums.Add(num);

        // 前一格是 0 或 1 時，沿用它的跳躍索引。-1 表示左側沒有可跳往的位置。
        prevNonOne.Add(-1);
        if (n > 1)
        {
            if (nums[n - 2] != 1 && nums[n - 2] != 0)
            {
                prevNonOne[n - 1] = n - 2;
            }
            else
            {
                prevNonOne[n - 1] = prevNonOne[n - 2];
            }
        }

        zeroCount.Add(num == 0 ? 1 : 0);
        if (n > 1)
        {
            zeroCount[n - 1] += zeroCount[n - 2];
        }
    }

    /// <summary>
    /// 查詢最後 k 個數字的乘積。用累計零數判斷是否含零，再用跳躍索引乘上區間內的因數。
    /// </summary>
    /// <param name="k">查詢長度，介於 1 與資料流目前長度之間。</param>
    /// <returns>最後 <paramref name="k"/> 個數字的乘積，含零時回傳 0。</returns>
    public int GetProduct(int k)
    {
        // 整段的累計零數扣掉尾段左側的累計零數，就能得到尾段內的零數。
        int totalZeros = zeroCount[n - 1];
        if (n - k >= 1)
        {
            totalZeros -= zeroCount[n - 1 - k];
        }

        if (totalZeros > 0)
        {
            return 0;
        }

        // 已確認尾段無零，跳過的區間內只有 1。索引小於尾段起點時停止。
        int ans = 1;
        for (int i = n - 1; i >= n - k;)
        {
            ans *= nums[i];
            i = prevNonOne[i];
        }

        return ans;
    }
}