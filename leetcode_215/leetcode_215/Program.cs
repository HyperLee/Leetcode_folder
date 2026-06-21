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
    /// 主控台入口會執行固定案例，印出兩種解法的預期值、實際值與驗證結果。
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
    /// 以同一份輸入的複本執行兩種解法，輸出預期值、實際值與 PASS 或 FAIL 的對照結果。
    /// </summary>
    /// <param name="caseName">顯示於主控台的案例名稱。</param>
    /// <param name="nums">符合題目限制的非空整數陣列；原始陣列會保留供輸出使用。</param>
    /// <param name="k">介於 1 與 <paramref name="nums"/> 長度之間的排名。</param>
    /// <param name="expected">此案例應回傳的第 k 大元素。</param>
    private void RunSample(string caseName, int[] nums, int k, int expected)
    {
        int sortingResult = FindKthLargest((int[])nums.Clone(), k);
        int quickselectResult = FindKthLargest2((int[])nums.Clone(), k);
        string sortingStatus = sortingResult == expected ? "PASS" : "FAIL";
        string quickselectStatus = quickselectResult == expected ? "PASS" : "FAIL";

        Console.WriteLine($"案例：{caseName}");
        Console.WriteLine($"輸入：nums = [{string.Join(", ", nums)}], k = {k}");
        Console.WriteLine($"預期：{expected}");
        Console.WriteLine($"解法一（排序）：{sortingResult}（{sortingStatus}）");
        Console.WriteLine($"解法二（Quickselect）：{quickselectResult}（{quickselectStatus}）");
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
    /// 解法二: 基于快速排序的选择方法
    /// 思路和算法:
    /// 我们可以用快速排序来解决这个问题，先对原数组排序，再返回倒数第 k 个位置，这样平均时间复杂度是 O(nlogn)，但其实我们
    /// 可以做的更快。
    /// 首先我们来回顾一下快速排序，这是一个典型的分治算法。我们对数组 a[l⋯r] 做快速排序的过程是（参考《算法导论》）：
    /// 分解： 将数组 a[l⋯r] 「划分」成两个子数组 a[l⋯q−1]、a[q+1⋯r]，使得 a[l⋯q−1] 中的每个元素小于等于 a[q]，且
    /// a[q] 小于等于 a[q+1⋯r] 中的每个元素。其中，计算下标 q 也是「划分」过程的一部分。
    /// 解决： 通过递归调用快速排序，对子数组 a[l⋯q−1] 和 a[q+1⋯r] 进行排序。
    /// 合并： 因为子数组都是原址排序的，所以不需要进行合并操作，a[l⋯r] 已经有序。
    /// 上文中提到的 「划分」 过程是：从子数组 a[l⋯r] 中选择任意一个元素 x 作为主元，调整子数组的元素使得左边的元素都小
    /// 于等于它，右边的元素都大于等于它， x 的最终位置就是 q。
    ///
    /// 由此可以发现每次经过「划分」操作后，我们一定可以确定一个元素的最终位置，即 x 的最终位置为 q，并且保证 a[l⋯q−1] 中
    /// 的每个元素小于等于 a[q]，且 a[q] 小于等于 a[q+1⋯r] 中的每个元素。所以只要某次划分的 q 为倒数第 k 个下标的时候，我们就
    /// 已经找到了答案。 我们只关心这一点，至于 a[l⋯q−1] 和 a[q+1⋯r] 是否是有序的，我们不关心。
    ///
    /// 因此我们可以改进快速排序算法来解决这个问题：在分解的过程当中，我们会对子数组进行划分，如果划分得到的 q 正好就是我们
    /// 需要的下标，就直接返回 a[q]；否则，如果 q 比目标下标小，就递归右子区间，否则递归左子区间。这样就可以把原来递归两个区
    /// 间变成只递归一个区间，提高了时间效率。这就是「快速选择」算法。
    ///
    /// 我们知道快速排序的性能和「划分」出的子数组的长度密切相关。直观地理解如果每次规模为 n 的问题我们都划分成 1 和 n−1
    /// 每次递归的时候又向 n−1 的集合中递归，这种情况是最坏的，时间代价是 O(n )。我们可以引入随机化来加速这个过程，它的时
    /// 间代价的期望是 O(n)，证明过程可以参考「《算法导论》9.2：期望为线性的选择算法」。需要注意的是，这个时间复杂度只有在 随
    /// 机数据 下才成立，而对于精心构造的数据则可能表现不佳。因此我们这里并没有真正地使用随机数，而是使用双指针的方法，这种
    /// 方法能够较好地应对各种数据。
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
}
