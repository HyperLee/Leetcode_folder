namespace leetcode_3551;

/// <summary>
/// 提供依數位和排序所需最少交換次數的兩種解法，以及無須輸入的固定測試入口。
/// </summary>
class Program
{
    /// <summary>
    /// 3551. Minimum Swaps to Sort by Digit Sum
    /// https://leetcode.com/problems/minimum-swaps-to-sort-by-digit-sum/description/
    /// 3551. 数位和排序需要的最小交换次数
    /// https://leetcode.cn/problems/minimum-swaps-to-sort-by-digit-sum/description/
    /// <para>English:</para>
    /// <para>You are given an array <c>nums</c> of distinct positive integers. You need to sort the array in increasing order based on the sum of the digits of each number. If two numbers have the same digit sum, the smaller number appears first in the sorted order.</para>
    /// <para>Return the minimum number of swaps required to rearrange <c>nums</c> into this sorted order.</para>
    /// <para>A swap is defined as exchanging the values at two distinct positions in the array.</para>
    ///
    /// <para>繁體中文：</para>
    /// <para>給定一個由互不相同正整數組成的陣列 <c>nums</c>。你需要依照每個數字的數位總和，以遞增順序排序陣列。如果兩個數字的數位總和相同，較小的數字排在前面。</para>
    /// <para>請回傳將 <c>nums</c> 重新排列為上述排序結果所需的最少交換次數。</para>
    /// <para>交換的定義是：交換陣列中兩個不同位置上的值。</para>
    /// </summary>
    /// <remarks>
    /// 不需互動輸入；執行十二組固定案例，分別驗證並查集與置換環解法的答案及輸入不變性。
    /// 每次呼叫使用獨立陣列副本，輸出 Expected、Actual 與 PASS／FAIL；任一檢查失敗時結束碼為 1。
    /// </remarks>
    /// <param name="args">命令列參數；本測試入口不使用這些參數。</param>
    static void Main(string[] args)
    {
        Environment.ExitCode = RunSamples() ? 0 : 1;
    }

    /// <summary>
    /// 建立符合題目條件的十二組固定資料，對兩種解法逐一比對已知答案與呼叫前後的輸入內容。
    /// 無須外部輸入；大型案例以長度呈現，最後輸出通過數量。
    /// </summary>
    /// <returns>全部二十四次檢查皆取得預期答案且未修改輸入時為 true，否則為 false。</returns>
    private static bool RunSamples()
    {
        // 測試資料用十進位字元加總建立排序基準，獨立於兩個解法的取餘數實作。
        int[] sortedLargeInput = Enumerable.Range(1, 100_000)
            .OrderBy(value => value.ToString(System.Globalization.CultureInfo.InvariantCulture).Sum(digit => digit - '0'))
            .ThenBy(value => value)
            .ToArray();

        // 左旋一格得到 targetPos = [1, 2, ..., n - 1, 0]，形成長度 n 的單一置換環。
        int[] longCycleInput = new int[sortedLargeInput.Length];
        Array.Copy(sortedLargeInput, 1, longCycleInput, 0, sortedLargeInput.Length - 1);
        longCycleInput[^1] = sortedLargeInput[0];

        (string Name, int[] Input, int Expected)[] testCases =
        [
            ("Official example 1", [37, 100], 1),
            ("Official example 2", [22, 14, 33, 7], 0),
            ("Official example 3", [18, 43, 34, 16], 2),
            ("Single element", [1], 0),
            ("Equal digit sums", [21, 12], 1),
            ("Digit order differs from numeric order", [19, 20], 1),
            ("Three-element cycle", [2, 3, 1], 2),
            ("Two disjoint cycles", [2, 1, 4, 3], 2),
            ("Fixed point and cycle", [2, 3, 1, 4], 2),
            ("Maximum value", [999_999_999, 1_000_000_000, 1], 1),
            ("Maximum length sorted", sortedLargeInput, 0),
            ("Maximum length single cycle", longCycleInput, 99_999)
        ];

        var solution = new Program();
        (string Name, Func<int[], int> Solve)[] solutions =
        [
            (nameof(MinSwaps), solution.MinSwaps),
            (nameof(MinSwaps2), solution.MinSwaps2)
        ];

        int passed = 0;
        int total = testCases.Length * solutions.Length;
        Console.WriteLine("LeetCode 3551 - Minimum Swaps to Sort by Digit Sum");
        foreach (var testCase in testCases)
        {
            string inputDescription = testCase.Input.Length <= 10
                ? $"nums=[{string.Join(", ", testCase.Input)}]"
                : $"length={testCase.Input.Length}";
            Console.WriteLine($"{testCase.Name}: {inputDescription}");

            foreach (var method in solutions)
            {
                int[] input = (int[])testCase.Input.Clone();
                int actual = method.Solve(input);
                bool inputUnchanged = input.SequenceEqual(testCase.Input);
                bool isPassed = actual == testCase.Expected && inputUnchanged;
                if (isPassed)
                {
                    passed++;
                }

                Console.WriteLine($"  {method.Name}: Expected={testCase.Expected}, Actual={actual}, InputUnchanged={inputUnchanged} => {(isPassed ? "PASS" : "FAIL")}");
            }
        }

        Console.WriteLine($"Summary: {passed}/{total} checks passed.");
        return passed == total;
    }

    /// <summary>
    /// 使用並查集求出依「數位和、數值」遞增排序所需的最少交換次數。
    /// 先建立原位置到目標位置的置換，再合併每條置換邊；連通分量數即為置換環數，答案為元素數減去環數。
    /// </summary>
    /// <param name="nums">長度為 1 至 100000 的互異正整數陣列，每個值不超過 1000000000；方法不修改陣列內容。</param>
    /// <returns>交換任意兩個不同位置，使陣列符合目標排序所需的最少次數。</returns>
    public int MinSwaps(int[] nums)
    {
        int n = nums.Length;
        int[] digitSum = new int[n];
        for (int i = 0; i < n; i++)
        {
            int x = nums[i], sum = 0;
            while (x > 0)
            {
                sum += x % 10;
                x /= 10;
            }
            digitSum[i] = sum;
        }

        int[] indices = new int[n];
        for (int i = 0; i < n; i++)
        {
            indices[i] = i;
        }

        // 排序原始下標以保留 nums；數位和相同時必須以數值決定唯一的目標順序。
        Array.Sort(indices, (a, b) =>
        {
            if (digitSum[a] != digitSum[b])
            {
                return digitSum[a].CompareTo(digitSum[b]);
            }
            return nums[a].CompareTo(nums[b]);
        });

        int[] targetPos = new int[n];
        for (int pos = 0; pos < n; pos++)
        {
            // indices[pos] 是目標位置 pos 所放元素的原位置；反向填出每個原位置應前往哪裡。
            targetPos[indices[pos]] = pos;
        }

        int[] parent = new int[n];
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
        }

        int components = n;
        for (int i = 0; i < n; i++)
        {
            int ri = Find(parent, i);
            int rj = Find(parent, targetPos[i]);
            // 同一環的最後一條邊不會新增合併；長度 k 的環恰好成功合併 k - 1 次。
            if (ri != rj)
            {
                parent[ri] = rj;
                components--;
            }
        }

        return n - components;
    }

    /// <summary>
    /// 以兩段迴圈尋找並查集根節點，並將查找路徑直接連到根節點。
    /// parent 必須表示合法的並查集森林，x 必須為其中的有效下標；迭代方式避免長鏈造成過深遞迴。
    /// </summary>
    /// <param name="parent">父節點陣列；根節點的父節點為自己，方法會就地壓縮查找路徑。</param>
    /// <param name="x">要查找所屬集合的節點下標。</param>
    /// <returns>節點 x 所屬集合的根節點下標。</returns>
    private int Find(int[] parent, int x)
    {
        int root = x;
        while (parent[root] != root)
        {
            root = parent[root];
        }

        while (parent[x] != x)
        {
            // 先保留原父節點，才能在改接根節點後繼續沿原路徑向上壓縮。
            int next = parent[x];
            parent[x] = root;
            x = next;
        }

        return root;
    }

    /// <summary>
    /// 使用置換環計數求出依「數位和、數值」遞增排序所需的最少交換次數。
    /// 建立原位置到目標位置的置換後，沿 targetPos 走訪每個環；包含固定點的環數為 cycles，答案為 n - cycles。
    /// </summary>
    /// <param name="nums">長度為 1 至 100000 的互異正整數陣列，每個值不超過 1000000000；方法不修改陣列內容。</param>
    /// <returns>交換任意兩個不同位置，使陣列符合目標排序所需的最少次數。</returns>
    public int MinSwaps2(int[] nums)
    {
        int n = nums.Length;
        int[] digitSum = new int[n];
        for (int i = 0; i < n; i++)
        {
            int x = nums[i], sum = 0;
            while (x > 0)
            {
                sum += x % 10;
                x /= 10;
            }
            digitSum[i] = sum;
        }

        int[] indices = new int[n];
        for (int i = 0; i < n; i++)
        {
            indices[i] = i;
        }
        // 與並查集解法使用相同的雙鍵排序，差異在後續如何計算置換環數。
        Array.Sort(indices, (a, b) =>
        {
            if (digitSum[a] != digitSum[b])
            {
                return digitSum[a].CompareTo(digitSum[b]);
            }
            return nums[a].CompareTo(nums[b]);
        });

        int[] targetPos = new int[n];
        for (int pos = 0; pos < n; pos++)
        {
            // targetPos[i] 記錄原位置 i 的元素應移往的目標位置。
            targetPos[indices[pos]] = pos;
        }

        bool[] visited = new bool[n];
        int cycles = 0;
        for (int i = 0; i < n; i++)
        {
            if (!visited[i])
            {
                // 置換中每個位置恰有一條入邊與出邊；未訪問的位置一定屬於一個新環。
                cycles++;
                int j = i;
                while (!visited[j])
                {
                    visited[j] = true;
                    j = targetPos[j];
                }
            }
        }

        // 固定點也是長度 1 的環，貢獻 1 - 1 = 0 次交換。
        return n - cycles;
    }
}