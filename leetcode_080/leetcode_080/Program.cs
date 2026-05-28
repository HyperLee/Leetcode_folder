namespace leetcode_080;

class Program
{
    /// <summary>
    /// 80. Remove Duplicates from Sorted Array II
    /// https://leetcode.com/problems/remove-duplicates-from-sorted-array-ii/description/
    /// 80. 刪除排序陣列中的重複項 II
    /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array-ii/description/
    ///
    /// English:
    /// Given an integer array nums sorted in non-decreasing order, remove some duplicates in-place
    /// such that each unique element appears at most twice. The relative order of the elements
    /// should be kept the same.
    ///
    /// Since it is impossible to change the length of the array in some languages, place the result
    /// in the first part of nums. If there are k elements after removing duplicates, then the first
    /// k elements of nums should hold the final result. It does not matter what remains beyond the
    /// first k elements. Return k after placing the final result in the first k slots of nums.
    ///
    /// Do not allocate extra space for another array. Modify the input array in-place with O(1)
    /// extra memory.
    ///
    /// Custom Judge:
    /// int[] nums = [...];
    /// int[] expectedNums = [...];
    /// int k = removeDuplicates(nums);
    /// assert k == expectedNums.length;
    /// for (int i = 0; i &lt; k; i++) {
    ///     assert nums[i] == expectedNums[i];
    /// }
    ///
    /// 繁體中文:
    /// 給定一個以非遞減順序排序的整數陣列 nums，請原地移除部分重複元素，使每個不同元素最多
    /// 出現兩次。元素的相對順序必須保持不變。
    ///
    /// 因為在某些語言中無法改變陣列長度，所以必須把結果放在 nums 的前半部。若移除重複項後
    /// 有 k 個元素，則 nums 的前 k 個位置應保存最終結果；第 k 個位置之後留下什麼內容都不重要。
    /// 在把最終結果放入 nums 的前 k 個位置後，回傳 k。
    ///
    /// 不要為另一個陣列配置額外空間。你必須使用 O(1) 額外記憶體，直接原地修改輸入陣列。
    ///
    /// 自訂判題:
    /// int[] nums = [...];
    /// int[] expectedNums = [...];
    /// int k = removeDuplicates(nums);
    /// assert k == expectedNums.length;
    /// for (int i = 0; i &lt; k; i++) {
    ///     assert nums[i] == expectedNums[i];
    /// }
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        (string Name, Func<int[], int> Solver)[] solvers =
        {
            ("RemoveDuplicates", solution.RemoveDuplicates),
            ("RemoveDuplicates2", solution.RemoveDuplicates2),
            ("RemoveDuplicates3", solution.RemoveDuplicates3),
        };
        int[][] samples =
        {
            new[] { 1, 1, 1, 2, 2, 3 },
            new[] { 0, 0, 1, 1, 1, 1, 2, 3, 3 },
            new[] { 1 },
        };
        int[][] expectedResults =
        {
            new[] { 1, 1, 2, 2, 3 },
            new[] { 0, 0, 1, 1, 2, 3, 3 },
            new[] { 1 },
        };

        Console.WriteLine("LeetCode 80 - Remove Duplicates from Sorted Array II");
        Console.WriteLine();

        foreach ((string name, Func<int[], int> solver) in solvers)
        {
            RunSamples(name, solver, samples, expectedResults);
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 使用通用「最多保留 k 次」的寫入指標解法處理 LeetCode 80。
    /// 輸入必須是非遞減排序的整數陣列，方法會原地覆寫陣列前段，使每個數字最多出現兩次。
    /// 核心概念是比較目前候選值與已寫入結果中倒數第 2 個位置；若不同，代表寫入後不會形成第三個相同值。
    /// 回傳值是有效結果長度，結果內容位於 nums 的前回傳值個位置。
    ///
    /// 解法一:
    /// 網路上神奇解法
    /// 參考
    /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array-ii/solutions/702970/gong-shui-san-xie-guan-yu-shan-chu-you-x-glnq/
    /// 通用解法 for 保留 k 位數
    ///
    /// 由于是保留 k 个相同数字，对于前 k 个数字，我们可以直接保留
    /// 对于后面的任意数字，能够保留的前提是：
    /// 与当前写入的位置前面的第 k 个元素进行比较，不相同则保留
    ///
    /// 举个例子，我们令 k=2，假设有如下样例
    /// [1,1,1,1,1,1,2,2,2,2,2,2,3]
    /// 1. 首先我们先让前 2 位直接保留，得到 1,1
    /// 2. 对后面的每一位进行继续遍历，能够保留的前提是与当前位置的前面 k 个元素不同（答案中的第一个 1），因此我们会跳过剩
    ///    余的 1，将第一个 2 追加，得到 1,1,2
    /// 3. 继续这个过程，这时候是和答案中的第 2 个 1 进行对比，因此可以得到 1,1,2,2
    /// 4. 这时候和答案中的第 1 个 2 比较，只有与其不同的元素能追加到答案，因此剩余的 2 被跳过，3 被追加到答案：1,1,2,2,3
    /// </summary>
    /// <param name="nums">非遞減排序的整數陣列，會被原地修改。</param>
    /// <returns>移除多餘重複項後的有效長度。</returns>
    public int RemoveDuplicates(int[] nums)
    {
        return Process(nums, 2);
    }

    /// <summary>
    /// 將已排序陣列原地壓縮成每個不同數字最多保留指定次數的結果。
    /// 輸入陣列必須是非遞減排序，maxOccurrences 必須大於 0；若 maxOccurrences 小於等於 0，會回傳 0。
    /// 解題概念是維護寫入位置，前 maxOccurrences 個元素直接保留，後續元素只要不同於結果中往前 maxOccurrences 格的位置即可保留。
    /// 回傳值是壓縮後的有效長度，陣列前段保存有效結果。
    /// </summary>
    /// <param name="nums">非遞減排序的整數陣列，會被原地修改。</param>
    /// <param name="maxOccurrences">每個不同數字最多允許保留的次數。</param>
    /// <returns>壓縮後的有效長度。</returns>
    private static int Process(int[] nums, int maxOccurrences)
    {
        if (maxOccurrences <= 0)
        {
            return 0;
        }

        int writeIndex = 0;
        foreach (int value in nums)
        {
            // 只要候選值和結果中倒數第 maxOccurrences 個值不同，寫入後就不會超過保留上限。
            if (writeIndex < maxOccurrences || nums[writeIndex - maxOccurrences] != value)
            {
                nums[writeIndex] = value;
                writeIndex++;
            }
        }
        return writeIndex;
    }

    /// <summary>
    /// 使用官方雙指針解法處理 LeetCode 80。
    /// 輸入必須是非遞減排序的整數陣列，方法會原地覆寫陣列前段，使每個數字最多出現兩次。
    /// fast 負責掃描每個候選值，slow 表示下一個可寫入位置；當 nums[fast] 不等於 nums[slow - 2] 時才寫入。
    /// 回傳值是有效結果長度，結果內容位於 nums 的前回傳值個位置。
    ///
    /// 解法二: 雙指針
    /// 官方解法
    /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array-ii/solutions/702644/shan-chu-pai-xu-shu-zu-zhong-de-zhong-fu-yec2/
    /// https://leetcode.cn/problems/remove-duplicates-from-sorted-array-ii/solutions/2855042/bao-mu-ji-de-bian-se-tu-shi-mo-ni-c-pyth-zn6z/
    ///
    /// 同樣的數字最多保留兩個,
    /// 輸入的序列是有排序過的
    /// 不需要考虑数组中超出新长度后面的元素。
    ///
    /// 我们定义两个指针 slow 和 fast 分别为慢指针和快指针，
    /// 其中慢指针表示处理出的数组的长度，快指针表示已经检查过的数组的长度
    /// ，即 nums[fast] 表示待检查的第一个元素
    /// ， nums[slow - 2] 为上一个应该被保留的元素所移动到的指定位置。
    ///
    ///  快指針: 簡單說就是 遍歷 陣列 nums
    ///  慢指針: 用於將不同 element 填入結果 array
    ///
    /// 請參考類似題目
    /// Leetcode_026
    /// Leetcode_027
    ///
    /// 因為相同 element 只能出現兩次
    /// 所以當出現第三次時候, 要進行替換
    /// 把第三次的 index 與 下一個不同 element value index 交換
    ///
    /// 輸入的 nums 已經排序過遞增, 所以相同元素.
    /// 一定在隔壁而已
    /// 只需要處理相同 > 2 即可
    /// 從第三個相同的 element 開始處理
    /// </summary>
    /// <param name="nums">非遞減排序的整數陣列，會被原地修改。</param>
    /// <returns>移除多餘重複項後的有效長度。</returns>
    public int RemoveDuplicates2(int[] nums)
    {
        int n = nums.Length;
        if (n <= 2)
        {
            return n;
        }

        int slow = 2;
        for (int fast = 2; fast < n; fast++)
        {
            // nums[slow - 2] 是目前結果中倒數第 2 個值；若候選值相同，寫入後會變成第三個重複值。
            if (nums[slow - 2] != nums[fast])
            {
                nums[slow] = nums[fast];
                slow++;
            }
        }

        return slow;
    }

    /// <summary>
    /// 使用原地陣列當作棧的解法處理 LeetCode 80。
    /// 輸入必須是非遞減排序的整數陣列，方法會把 nums 前段視為棧，使每個數字最多入棧兩次。
    /// 掃描新候選值時，若它不同於棧頂下方第二格 nums[stackSize - 2]，代表入棧後仍符合最多兩次的限制。
    /// 回傳值是棧大小，也就是有效結果長度，結果內容位於 nums 的前回傳值個位置。
    ///
    /// 解法三: 棧
    /// 核心思路：用一个栈记录去重后的元素，如果当前元素等于栈顶下方那个数（倒数第二个数），那么不能入栈（否则会有三个一样的
    /// 数），反之可以入栈。
    ///
    /// 看示例 1，nums=[1,1,1,2,2,3]。由于数组是有序的，前两个数一定满足要求，直接入栈。现在栈中元素为 [1,1]，我们从 nums[2] 开始思考：
    /// i	栈（入栈前）	栈顶下方元素	nums[i]	nums[i] 是否入栈
    /// 2	[1,1]	1	1	否
    /// 3	[1,1]	1	2	是
    /// 4	[1,1,2]	1	2	是
    /// 5	[1,1,2,2]	2	3	是
    ///
    /// 最终栈中元素为 [1,1,2,2,3]。
    /// 为了做到 O(1) 空间复杂度，直接把 nums 当作栈，用一个变量 stackSize 表示栈的大小，初始值为 2。
    /// 那么 nums[stackSize−2] 就是栈顶下方那个数。
    /// 入栈就是把 nums[stackSize] 置为 nums[i]，同时把 stackSize 加一。
    /// 最后返回栈的大小。由于本题数组长度 n 最小是 1，最终返回值为 min(stackSize,n)，这样可以兼容 n=1 的情况。（实际上，力
    /// 扣没有验证返回值是否超过数组长度，不计算 min 也能通过，但建议还是把 min 写上。）
    /// </summary>
    /// <param name="nums">非遞減排序的整數陣列，會被原地修改。</param>
    /// <returns>移除多餘重複項後的有效長度。</returns>
    public int RemoveDuplicates3(int[] nums)
    {
        if (nums.Length <= 2)
        {
            return nums.Length;
        }

        int stackSize = 2;
        for (int i = 2; i < nums.Length; i++)
        {
            // 和棧頂下方第二格比較，可以判斷目前值是否已經保留兩次。
            if (nums[i] != nums[stackSize - 2])
            {
                nums[stackSize] = nums[i];
                stackSize++;
            }
        }
        return stackSize;
    }

    /// <summary>
    /// 執行 Main 使用的固定範例，確認指定解法回傳的有效長度與陣列前段內容是否符合預期。
    /// 輸入 samples 與 expectedResults 必須一一對應，solver 會原地修改樣本副本。
    /// 輸出每個案例的 PASS 或 FAIL、回傳長度與有效結果，方便直接用 dotnet run 驗證目前實作。
    /// </summary>
    /// <param name="solutionName">顯示在輸出中的解法名稱。</param>
    /// <param name="solver">要驗證的解法委派。</param>
    /// <param name="samples">非遞減排序的測試資料。</param>
    /// <param name="expectedResults">每筆測試資料預期的有效前綴結果。</param>
    private static void RunSamples(
        string solutionName,
        Func<int[], int> solver,
        int[][] samples,
        int[][] expectedResults)
    {
        Console.WriteLine($"{solutionName}:");
        for (int i = 0; i < samples.Length; i++)
        {
            int[] nums = (int[])samples[i].Clone();
            int k = solver(nums);
            int prefixLength = Math.Clamp(k, 0, nums.Length);
            int[] actual = CopyPrefix(nums, prefixLength);
            bool passed = k == expectedResults[i].Length && actual.SequenceEqual(expectedResults[i]);
            string status = passed ? "PASS" : "FAIL";

            Console.WriteLine(
                $"  {status} case {i + 1}: input={FormatArray(samples[i])}, k={k}, result={FormatArray(actual)}, expected={FormatArray(expectedResults[i])}");
        }
    }

    /// <summary>
    /// 複製陣列前指定長度的元素，用於呈現 LeetCode 判題只會檢查的有效前綴。
    /// 輸入 length 必須介於 0 與 nums.Length 之間，輸出為新的整數陣列。
    /// </summary>
    /// <param name="nums">來源陣列。</param>
    /// <param name="length">要複製的前綴長度。</param>
    /// <returns>包含來源陣列前 length 個元素的新陣列。</returns>
    private static int[] CopyPrefix(int[] nums, int length)
    {
        int[] result = new int[length];
        Array.Copy(nums, result, length);
        return result;
    }

    /// <summary>
    /// 將整數陣列格式化成 README 與範例輸出容易比對的字串。
    /// 輸入可為任意整數陣列，輸出格式為 [a, b, c]。
    /// </summary>
    /// <param name="nums">要格式化的整數陣列。</param>
    /// <returns>陣列的可讀字串表示。</returns>
    private static string FormatArray(int[] nums)
    {
        return $"[{string.Join(", ", nums)}]";
    }
}
