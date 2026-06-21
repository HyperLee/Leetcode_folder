namespace leetcode_215;

class Program
{
    /// <summary>
    /// 215. Kth Largest Element in an Array
    /// https://leetcode.com/problems/kth-largest-element-in-an-array/description/
    /// 215. 数组中的第K个最大元素
    /// https://leetcode.cn/problems/kth-largest-element-in-an-array/description/
    /// Given an integer array nums and an integer k, return the kth largest element in the array.
    /// Note that it is the kth largest element in the sorted order, not the kth distinct element.
    /// Can you solve it without sorting?
    /// 給定整數陣列 nums 與整數 k，請回傳陣列中第 k 大的元素。
    /// 請注意，這是排序後的第 k 大元素，而不是第 k 個相異元素。
    /// 你能否在不排序的情況下解決此問題？
    /// </summary>
    /// <remarks>
    /// 主控台入口會執行固定案例，印出三種解法的預期值、實際值與驗證結果。
    /// </remarks>
    /// <param name="args">未使用；主控台入口固定執行內建的範例驗證。</param>
    static void Main(string[] args)
    {
        Program program = new Program();
        program.RunSamples();
    }

    /// <summary>
    /// 執行一般、重複值與單元素邊界案例，對照兩種解法是否都回傳預期的第 k 大元素。
    /// </summary>
    private void RunSamples()
    {
        RunSample("一般案例", [3, 2, 1, 5, 6, 4], 2, 5);
        RunSample("重複值案例", [3, 2, 3, 1, 2, 4, 5, 5, 6], 4, 4);
        RunSample("單元素邊界案例", [42], 1, 42);
    }

    /// <summary>
    /// 以同一份輸入的複本執行三種解法，輸出預期值、實際值與 PASS 或 FAIL 的對照結果。
    /// </summary>
    /// <param name="caseName">顯示於主控台的案例名稱。</param>
    /// <param name="nums">符合題目限制的非空整數陣列；原始陣列會保留供輸出使用。</param>
    /// <param name="k">介於 1 與 <paramref name="nums"/> 長度之間的排名。</param>
    /// <param name="expected">此案例應回傳的第 k 大元素。</param>
    private void RunSample(string caseName, int[] nums, int k, int expected)
    {
        int sortingResult = FindKthLargest((int[])nums.Clone(), k);
        int quickselectResult = FindKthLargest2((int[])nums.Clone(), k);
        int randomQuickselectResult = FindKthLargest3((int[])nums.Clone(), k);
        string sortingStatus = sortingResult == expected ? "PASS" : "FAIL";
        string quickselectStatus = quickselectResult == expected ? "PASS" : "FAIL";
        string randomQuickselectStatus = randomQuickselectResult == expected ? "PASS" : "FAIL";

        Console.WriteLine($"案例：{caseName}");
        Console.WriteLine($"輸入：nums = [{string.Join(", ", nums)}], k = {k}");
        Console.WriteLine($"預期：{expected}");
        Console.WriteLine($"解法一（排序）：{sortingResult}（{sortingStatus}）");
        Console.WriteLine($"解法二（Quickselect）：{quickselectResult}（{quickselectStatus}）");
        Console.WriteLine($"解法三（隨機 Pivot Quickselect）：{randomQuickselectResult}（{randomQuickselectStatus}）");
        Console.WriteLine();
    }

    /// <summary>
    /// 使用完整排序尋找第 k 大元素：將 <paramref name="nums"/> 升冪排列後，回傳索引 <c>nums.Length - k</c> 的值。
    /// 輸入必須為非空陣列且 <paramref name="k"/> 介於 1 與陣列長度之間；此方法會原地重排陣列。
    /// </summary>
    /// <param name="nums">符合題目限制、且允許被升冪排序的整數陣列。</param>
    /// <param name="k">欲取得的排名，1 代表最大元素。</param>
    /// <returns>排序後第 k 大的元素；重複值會分別計入排名。</returns>
    public int FindKthLargest(int[] nums, int k)
    {
        // 升冪排序後，第 k 大元素的索引正好是 nums.Length - k。
        Array.Sort(nums);
        return nums[nums.Length - k];
    }

    /// <summary>
    /// 使用 Quickselect 尋找第 k 大元素：每輪固定以候選區間最左元素作為 pivot，
    /// 將 <paramref name="k"/> 轉為升冪排序後的目標索引，再透過 Hoare 分割只縮小包含該索引的一側，
    /// 因此無須完成整個陣列的排序。
    /// </summary>
    /// <remarks>
    /// 此實作固定取區間最左元素作為 pivot，並以 Hoare 分割取得區間邊界；在隨機排列輸入上平均為 O(n)，最壞為 O(n²)。
    /// 輸入必須為非空陣列且 <paramref name="k"/> 介於 1 與陣列長度之間；方法會原地重排陣列。
    /// </remarks>
    /// <param name="nums">符合題目限制、且允許被分割與交換元素的整數陣列。</param>
    /// <param name="k">欲取得的排名，1 代表最大元素。</param>
    /// <returns>第 k 大的元素；不會忽略重複值。</returns>
    public int FindKthLargest2(int[] nums, int k)
    {
        int targetIndex = nums.Length - k;

        // 第 k 大元素等於升冪排序後索引 targetIndex 的元素。
        return Quickselect(nums, 0, nums.Length - 1, targetIndex);
    }

    /// <summary>
    /// 在 <paramref name="nums"/> 的閉區間 <paramref name="left"/> 到 <paramref name="right"/> 中，
    /// 以 Hoare 分割持續縮小範圍，直到找出全陣列索引 <paramref name="targetIndex"/> 的元素。
    /// </summary>
    /// <param name="nums">正在原地分割的整數陣列。</param>
    /// <param name="left">目前候選區間的左界（含）。</param>
    /// <param name="right">目前候選區間的右界（含）。</param>
    /// <param name="targetIndex">升冪排序後欲取得元素的零起始索引，且必須落在目前候選區間內。</param>
    /// <returns>升冪排序後位於 <paramref name="targetIndex"/> 的元素。</returns>
    private int Quickselect(int[] nums, int left, int right, int targetIndex)
    {
        // 候選區間只剩一個元素時，它就是目標位置的答案。
        if (left == right)
        {
            return nums[targetIndex];
        }

        int pivot = nums[left];
        int i = left - 1;
        int j = right + 1;

        while (i < j)
        {
            i++;
            while (nums[i] < pivot)
            {
                i++;
            }

            j--;
            while (nums[j] > pivot)
            {
                j--;
            }

            if (i < j)
            {
                int temp = nums[i];
                nums[i] = nums[j];
                nums[j] = temp;
            }
        }

        // j 是 Hoare 分割的邊界，不是 pivot 的最終索引；目標只會位於其中一側。
        if (targetIndex <= j)
        {
            return Quickselect(nums, left, j, targetIndex);
        }

        return Quickselect(nums, j + 1, right, targetIndex);
    }

    /// <summary>
    /// 解法三：使用隨機 pivot 的快速選擇方法。
    /// </summary>
    /// <remarks>
    /// 每輪從目前區間隨機選出 pivot，交換至左端後以與解法二相同的 Hoare 分割縮小候選範圍。
    /// 輸入必須為非空陣列且 <paramref name="k"/> 介於 1 與陣列長度之間；方法會原地重排陣列。
    /// </remarks>
    /// <param name="nums">符合題目限制、且允許被分割與交換元素的整數陣列。</param>
    /// <param name="k">欲取得的排名，1 代表最大元素。</param>
    /// <returns>第 k 大的元素；不會忽略重複值。</returns>
    public int FindKthLargest3(int[] nums, int k)
    {
        int targetIndex = nums.Length - k;

        // 第 k 大元素等於升冪排序後索引 targetIndex 的元素。
        return QuickselectWithRandomPivot(nums, 0, nums.Length - 1, targetIndex);
    }

    /// <summary>
    /// 在 <paramref name="nums"/> 的閉區間 <paramref name="left"/> 到 <paramref name="right"/> 中，
    /// 先隨機選取 pivot 並交換至左端，再以 Hoare 分割持續縮小範圍，直到找出目標元素。
    /// </summary>
    /// <param name="nums">正在原地分割的整數陣列。</param>
    /// <param name="left">目前候選區間的左界（含）。</param>
    /// <param name="right">目前候選區間的右界（含）。</param>
    /// <param name="targetIndex">升冪排序後欲取得元素的零起始索引，且必須落在目前候選區間內。</param>
    /// <returns>升冪排序後位於 <paramref name="targetIndex"/> 的元素。</returns>
    private int QuickselectWithRandomPivot(int[] nums, int left, int right, int targetIndex)
    {
        // 候選區間只剩一個元素時，它就是目標位置的答案。
        if (left == right)
        {
            return nums[targetIndex];
        }

        int pivotIndex = Random.Shared.Next(left, right + 1);
        (nums[left], nums[pivotIndex]) = (nums[pivotIndex], nums[left]);

        int pivot = nums[left];
        int i = left - 1;
        int j = right + 1;

        while (i < j)
        {
            i++;
            while (nums[i] < pivot)
            {
                i++;
            }

            j--;
            while (nums[j] > pivot)
            {
                j--;
            }

            if (i < j)
            {
                int temp = nums[i];
                nums[i] = nums[j];
                nums[j] = temp;
            }
        }

        // j 是 Hoare 分割的邊界，不是 pivot 的最終索引；目標只會位於其中一側。
        if (targetIndex <= j)
        {
            return QuickselectWithRandomPivot(nums, left, j, targetIndex);
        }

        return QuickselectWithRandomPivot(nums, j + 1, right, targetIndex);
    }
}
