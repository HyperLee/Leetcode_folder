namespace leetcode_540;

class Program
{
    /// <summary>
    /// 540. Single Element in a Sorted Array
    /// https://leetcode.com/problems/single-element-in-a-sorted-array/description/
    /// 540. 有序數組中的單一元素
    /// https://leetcode.cn/problems/single-element-in-a-sorted-array/description/
    /// 
    /// 題目（繁體中文）：
    /// 給你一個已排序的整數陣列，陣列中每個元素恰好出現兩次，只有一個元素恰好出現一次。
    /// 回傳那個只出現一次的元素。
    /// 要求：時間複雜度必須為 O(log n)，且額外空間為 O(1)。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        Console.WriteLine("=== 解法一：全陣列的二分查找 ===");
        // 測試案例 1: [1,1,2,3,3,4,4,8,8] => 2
        int[] nums1 = [1, 1, 2, 3, 3, 4, 4, 8, 8];
        Console.WriteLine($"測試 1: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate(nums1)}"); // 預期: 2

        // 測試案例 2: [3,3,7,7,10,11,11] => 10
        int[] nums2 = [3, 3, 7, 7, 10, 11, 11];
        Console.WriteLine($"\n測試 2: [{string.Join(", ", nums2)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate(nums2)}"); // 預期: 10

        // 測試案例 3: [1] => 1 (邊界情況：只有一個元素)
        int[] nums3 = [1];
        Console.WriteLine($"\n測試 3: [{string.Join(", ", nums3)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate(nums3)}"); // 預期: 1

        // 測試案例 4: [1,1,2] => 2 (單一元素在最後)
        int[] nums4 = [1, 1, 2];
        Console.WriteLine($"\n測試 4: [{string.Join(", ", nums4)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate(nums4)}"); // 預期: 2

        // 測試案例 5: [1,2,2] => 1 (單一元素在最前)
        int[] nums5 = [1, 2, 2];
        Console.WriteLine($"\n測試 5: [{string.Join(", ", nums5)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate(nums5)}"); // 預期: 1

        Console.WriteLine("\n\n=== 解法二：Dictionary 雜湊表計數法 ===");
        Console.WriteLine($"測試 1: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate2(nums1)}"); // 預期: 2

        Console.WriteLine($"\n測試 2: [{string.Join(", ", nums2)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate2(nums2)}"); // 預期: 10

        Console.WriteLine($"\n測試 3: [{string.Join(", ", nums3)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate2(nums3)}"); // 預期: 1

        Console.WriteLine($"\n測試 4: [{string.Join(", ", nums4)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate2(nums4)}"); // 預期: 2

        Console.WriteLine($"\n測試 5: [{string.Join(", ", nums5)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate2(nums5)}"); // 預期: 1

        Console.WriteLine("\n\n=== 解法三：XOR 異或運算法 ===");
        Console.WriteLine($"測試 1: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate3(nums1)}"); // 預期: 2

        Console.WriteLine($"\n測試 2: [{string.Join(", ", nums2)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate3(nums2)}"); // 預期: 10

        Console.WriteLine($"\n測試 3: [{string.Join(", ", nums3)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate3(nums3)}"); // 預期: 1

        Console.WriteLine($"\n測試 4: [{string.Join(", ", nums4)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate3(nums4)}"); // 預期: 2

        Console.WriteLine($"\n測試 5: [{string.Join(", ", nums5)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate3(nums5)}"); // 預期: 1

        Console.WriteLine("\n\n=== 解法四：步長為 2 的線性遍歷法 ===");
        Console.WriteLine($"測試 1: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate4(nums1)}"); // 預期: 2

        Console.WriteLine($"\n測試 2: [{string.Join(", ", nums2)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate4(nums2)}"); // 預期: 10

        Console.WriteLine($"\n測試 3: [{string.Join(", ", nums3)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate4(nums3)}"); // 預期: 1

        Console.WriteLine($"\n測試 4: [{string.Join(", ", nums4)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate4(nums4)}"); // 預期: 2

        Console.WriteLine($"\n測試 5: [{string.Join(", ", nums5)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate4(nums5)}"); // 預期: 1

        Console.WriteLine("\n\n=== 解法五：偶數下標二分查找法 ===");
        Console.WriteLine($"測試 1: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate5(nums1)}"); // 預期: 2

        Console.WriteLine($"\n測試 2: [{string.Join(", ", nums2)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate5(nums2)}"); // 預期: 10

        Console.WriteLine($"\n測試 3: [{string.Join(", ", nums3)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate5(nums3)}"); // 預期: 1

        Console.WriteLine($"\n測試 4: [{string.Join(", ", nums4)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate5(nums4)}"); // 預期: 2

        Console.WriteLine($"\n測試 5: [{string.Join(", ", nums5)}]");
        Console.WriteLine($"結果: {solution.SingleNonDuplicate5(nums5)}"); // 預期: 1
    }

    /// <summary>
    /// 解法一：全陣列的二分查找
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 假設只出現一次的元素位於下標 x，由於其餘每個元素都出現兩次，
    /// 因此下標 x 的左邊和右邊都有偶數個元素，陣列的長度是奇數。
    /// </para>
    /// 
    /// <para><b>關鍵觀察：</b></para>
    /// <para>
    /// 由於陣列是有序的，相同的元素一定相鄰。對於下標 x 左邊的下標 y，
    /// 如果 nums[y] == nums[y+1]，則 y 一定是偶數；對於下標 x 右邊的下標 z，
    /// 如果 nums[z] == nums[z+1]，則 z 一定是奇數。
    /// </para>
    /// 
    /// <para><b>二分查找策略：</b></para>
    /// <para>
    /// 下標 x 是相同元素開始下標奇偶性的分界點，因此可以使用二分查找。
    /// 利用按位異或 (XOR) 的特性：mid ^ 1 可以找到 mid 的配對下標
    /// （偶數變奇數，奇數變偶數）。
    /// </para>
    /// 
    /// <para><b>時間複雜度：</b>O(log n)</para>
    /// <para><b>空間複雜度：</b>O(1)</para>
    /// </summary>
    /// <param name="nums">已排序的整數陣列，每個元素出現兩次，僅有一個元素出現一次</param>
    /// <returns>只出現一次的元素</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int result = solution.SingleNonDuplicate([1, 1, 2, 3, 3]); // 回傳 2
    /// </code>
    /// </example>
    public int SingleNonDuplicate(int[] nums)
    {
        // 初始化二分查找的左右邊界
        int low = 0;
        int high = nums.Length - 1;

        // 二分查找：當 low < high 時持續搜尋
        while (low < high)
        {
            // 計算中間位置，使用 low + (high - low) / 2 避免整數溢位
            int mid = low + (high - low) / 2;

            // 利用 XOR 運算找到配對下標：
            // - 當 mid 是偶數時，mid ^ 1 = mid + 1
            // - 當 mid 是奇數時，mid ^ 1 = mid - 1
            // 如果 nums[mid] == nums[mid ^ 1]，表示單一元素在 mid 右邊
            if (nums[mid] == nums[mid ^ 1])
            {
                // 單一元素在右半部，縮小左邊界
                low = mid + 1;
            }
            else
            {
                // 單一元素在左半部（包含 mid），縮小右邊界
                high = mid;
            }
        }

        // 當 low == high 時，找到單一元素
        return nums[low];
    }

    /// <summary>
    /// 解法二：Dictionary 雜湊表計數法
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 使用 Dictionary 統計每個元素出現的次數，
    /// 找出出現次數為 1 的元素即為答案。
    /// </para>
    /// 
    /// <para><b>優點：</b></para>
    /// <para>簡單直觀，容易理解和實現。</para>
    /// 
    /// <para><b>缺點：</b></para>
    /// <para>
    /// 不符合題目要求的 O(log n) 時間複雜度和 O(1) 空間複雜度。
    /// 並且沒有利用「陣列已排序」這個重要條件。
    /// </para>
    /// 
    /// <para><b>時間複雜度：</b>O(n) - 需要遍歷整個陣列</para>
    /// <para><b>空間複雜度：</b>O(n) - 需要額外的 Dictionary 儲存元素計數</para>
    /// </summary>
    /// <param name="nums">已排序的整數陣列</param>
    /// <returns>只出現一次的元素</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int result = solution.SingleNonDuplicate2([1, 1, 2, 3, 3]); // 回傳 2
    /// </code>
    /// </example>
    public int SingleNonDuplicate2(int[] nums)
    {
        // 建立 Dictionary 用於記錄每個元素出現的次數
        Dictionary<int, int> dict = new Dictionary<int, int>();

        // 第一次遍歷：統計每個元素的出現次數
        for (int i = 0; i < nums.Length; i++)
        {
            if (dict.ContainsKey(nums[i]))
            {
                // 元素已存在，計數加 1
                dict[nums[i]]++;
            }
            else
            {
                // 元素第一次出現，初始化計數為 1
                dict[nums[i]] = 1;
            }
        }

        // 第二次遍歷：找出出現次數為 1 的元素
        foreach (var kvp in dict)
        {
            if (kvp.Value == 1)
            {
                return kvp.Key;
            }
        }

        // 理論上不會執行到這裡，回傳 -1 表示未找到
        return -1;
    }

    /// <summary>
    /// 解法三：XOR 異或運算法
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 利用 XOR 的數學特性：
    /// 1. a ^ a = 0（相同數字 XOR 結果為 0）
    /// 2. a ^ 0 = a（任何數字與 0 XOR 結果為本身）
    /// 3. XOR 具有交換律和結合律，運算順序不影響結果
    /// </para>
    /// 
    /// <para><b>核心概念：</b></para>
    /// <para>
    /// 將所有元素逐一進行 XOR 運算，成對出現的元素會互相抵消變成 0，
    /// 最後留下的就是那個只出現一次的元素。
    /// </para>
    /// 
    /// <para><b>缺點：</b></para>
    /// <para>
    /// 不符合題目要求的 O(log n) 時間複雜度。
    /// 並且沒有利用「陣列已排序」這個重要條件。
    /// </para>
    /// 
    /// <para><b>時間複雜度：</b>O(n) - 需要遍歷整個陣列</para>
    /// <para><b>空間複雜度：</b>O(1) - 只使用常數額外空間</para>
    /// </summary>
    /// <param name="nums">已排序的整數陣列</param>
    /// <returns>只出現一次的元素</returns>
    /// <example>
    /// <code>
    ///  運算過程範例：[1, 1, 2, 3, 3]
    ///  0 ^ 1 = 1
    ///  1 ^ 1 = 0
    ///  0 ^ 2 = 2
    ///  2 ^ 3 = 1
    ///  1 ^ 3 = 2 → 結果為 2
    /// var solution = new Program();
    /// int result = solution.SingleNonDuplicate3([1, 1, 2, 3, 3]); // 回傳 2
    /// </code>
    /// </example>
    public int SingleNonDuplicate3(int[] nums)
    {
        // 初始化結果為 0（任何數 XOR 0 都等於該數本身）
        int res = 0;

        // 將所有元素逐一進行 XOR 運算
        // 成對的元素會互相抵消（a ^ a = 0）
        // 最後留下的就是單一元素
        for (int i = 0; i < nums.Length; i++)
        {
            res = res ^ nums[i];
        }

        return res;
    }

    /// <summary>
    /// 解法四：步長為 2 的線性遍歷法
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 在單一元素之前，每對相同元素的第一個都在偶數下標。
    /// 因此以步長 2 遍歷偶數下標，檢查 nums[i] 是否等於 nums[i+1]。
    /// 第一個不符合條件的位置就是單一元素的位置。
    /// </para>
    /// 
    /// <para><b>優點：</b></para>
    /// <para>簡單直觀，利用了配對元素的奇偶性規律。</para>
    /// 
    /// <para><b>缺點：</b></para>
    /// <para>
    /// 不符合題目要求的 O(log n) 時間複雜度。
    /// 最壞情況下（單一元素在最後）需要遍歷整個陣列。
    /// </para>
    /// 
    /// <para><b>時間複雜度：</b>O(n) - 最壞情況遍歷整個陣列</para>
    /// <para><b>空間複雜度：</b>O(1) - 只使用常數額外空間</para>
    /// </summary>
    /// <param name="nums">已排序的整數陣列</param>
    /// <returns>只出現一次的元素</returns>
    /// <example>
    /// <code>
    ///  遍歷過程範例：[1, 1, 2, 3, 3]
    ///  i=0: nums[0]=1, nums[1]=1 → 相等，繼續
    ///  i=2: nums[2]=2, nums[3]=3 → 不等，回傳 nums[2]=2
    /// var solution = new Program();
    /// int result = solution.SingleNonDuplicate4([1, 1, 2, 3, 3]); // 回傳 2
    /// </code>
    /// </example>
    public int SingleNonDuplicate4(int[] nums)
    {
        int n = nums.Length;

        // 以步長 2 遍歷所有偶數下標
        // 在單一元素之前，每對元素的第一個都在偶數位置
        for (int i = 0; i < n - 1; i += 2)
        {
            // 檢查當前元素是否與下一個元素相等
            if (nums[i] != nums[i + 1])
            {
                // 不相等表示 nums[i] 就是單一元素
                return nums[i];
            }
        }

        // 如果遍歷完整都沒找到，單一元素在最後一個位置
        return nums[n - 1];
    }

    /// <summary>
    /// 解法五：偶數下標二分查找法
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 將陣列以「配對」為單位進行二分查找。
    /// 假設有 n 個元素，則有 (n-1)/2 個完整配對和 1 個單一元素。
    /// 將配對編號從 0 到 (n-1)/2，二分查找第一個不完整的配對。
    /// </para>
    /// 
    /// <para><b>關鍵觀察：</b></para>
    /// <para>
    /// 對於配對編號 mid，對應的元素下標為 2*mid 和 2*mid+1。
    /// 如果 nums[2*mid] == nums[2*mid+1]，表示這個配對是完整的，
    /// 單一元素在 mid 右邊；否則在 mid 或其左邊。
    /// </para>
    /// 
    /// <para><b>優點：</b></para>
    /// <para>符合題目要求的 O(log n) 時間複雜度和 O(1) 空間複雜度。</para>
    /// 
    /// <para><b>時間複雜度：</b>O(log n) - 每次縮小一半搜尋範圍</para>
    /// <para><b>空間複雜度：</b>O(1) - 只使用常數額外空間</para>
    /// </summary>
    /// <param name="nums">已排序的整數陣列</param>
    /// <returns>只出現一次的元素</returns>
    /// <example>
    /// <code>
    ///  範例：[1, 1, 2, 3, 3]，長度 5，配對數 = 5/2 = 2
    ///  配對 0: (nums[0], nums[1]) = (1, 1) ✓
    ///  配對 1: (nums[2], nums[3]) = (2, 3) ✗ → 單一元素在這裡
    /// var solution = new Program();
    /// int result = solution.SingleNonDuplicate5([1, 1, 2, 3, 3]); // 回傳 2
    /// </code>
    /// </example>
    public int SingleNonDuplicate5(int[] nums)
    {
        // low 初始化為 -1，表示「無效」的配對編號
        // high 初始化為配對數量（包含單一元素所在的「不完整配對」）
        int low = -1;
        int high = nums.Length / 2;

        // 二分查找：找到第一個不完整的配對
        while (low + 1 < high)
        {
            int mid = (low + high) / 2;

            // 檢查配對 mid 是否完整（兩個元素相等）
            if (nums[2 * mid] == nums[2 * mid + 1])
            {
                // 配對完整，單一元素在右邊
                low = mid;
            }
            else
            {
                // 配對不完整，單一元素在這裡或左邊
                high = mid;
            }
        }

        // high 指向第一個不完整配對，其第一個元素就是單一元素
        return nums[2 * high];
    }
}
