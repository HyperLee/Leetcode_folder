namespace leetcode_3354;

class Program
{
    /// <summary>
    /// 3354. Make Array Elements Equal to Zero
    /// https://leetcode.com/problems/make-array-elements-equal-to-zero/description/?envType=daily-question&envId=2025-10-28
    /// 3354. 使数组元素等于零
    /// https://leetcode.cn/problems/make-array-elements-equal-to-zero/description/?envType=daily-question&envId=2025-10-28
    /// 
    /// 題目描述：
    /// 給定一個整數陣列 nums。
    /// 
    /// 首先選擇一個起始位置 curr，使得 nums[curr] == 0，並選擇一個移動方向（向左或向右）。
    /// 
    /// 之後，重複執行以下過程：
    /// - 如果 curr 超出範圍 [0, n - 1]，此過程結束。
    /// - 如果 nums[curr] == 0，則按照當前方向移動，如果向右移動則 curr 加 1，如果向左移動則 curr 減 1。
    /// - 否則如果 nums[curr] > 0：
    ///   - 將 nums[curr] 減 1。
    ///   - 反轉移動方向（左變右，右變左）。
    ///   - 朝新方向移動一步。
    /// 
    /// 如果在過程結束時，nums 中的每個元素都變為 0，則初始位置 curr 和移動方向的選擇被視為有效。
    /// 
    /// 返回可能的有效選擇數量。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        
        // 測試案例 1：範例 1
        int[] nums1 = {1, 0, 2, 0, 3};
        int result1 = solution.CountValidSelections(nums1);
        Console.WriteLine($"測試案例 1: nums = [{string.Join(", ", nums1)}]");
        Console.WriteLine($"預期輸出: 2, 實際輸出: {result1}");
        Console.WriteLine($"結果: {(result1 == 2 ? "通過 ✓" : "失敗 ✗")}");
        Console.WriteLine();
        
        // 測試案例 2：範例 2
        int[] nums2 = {2, 3, 4, 0, 4, 1, 0};
        int result2 = solution.CountValidSelections(nums2);
        Console.WriteLine($"測試案例 2: nums = [{string.Join(", ", nums2)}]");
        Console.WriteLine($"預期輸出: 0, 實際輸出: {result2}");
        Console.WriteLine($"結果: {(result2 == 0 ? "通過 ✓" : "失敗 ✗")}");
        Console.WriteLine();
        
        // 測試案例 3：簡單案例
        int[] nums3 = {0, 1, 0};
        int result3 = solution.CountValidSelections(nums3);
        Console.WriteLine($"測試案例 3: nums = [{string.Join(", ", nums3)}]");
        Console.WriteLine($"預期輸出: 2, 實際輸出: {result3}");
        Console.WriteLine($"結果: {(result3 == 2 ? "通過 ✓" : "失敗 ✗")}");
        Console.WriteLine();
        
        // 測試案例 4：所有元素都是 0
        int[] nums4 = {0, 0, 0};
        int result4 = solution.CountValidSelections(nums4);
        Console.WriteLine($"測試案例 4: nums = [{string.Join(", ", nums4)}]");
        Console.WriteLine($"預期輸出: 6, 實際輸出: {result4}");
        Console.WriteLine($"結果: {(result4 == 6 ? "通過 ✓" : "失敗 ✗")}");
        Console.WriteLine();
        
        // 測試案例 5：只有一個 0
        int[] nums5 = {1, 0, 1};
        int result5 = solution.CountValidSelections(nums5);
        Console.WriteLine($"測試案例 5: nums = [{string.Join(", ", nums5)}]");
        Console.WriteLine($"預期輸出: 2, 實際輸出: {result5}");
        Console.WriteLine($"結果: {(result5 == 2 ? "通過 ✓" : "失敗 ✗")}");
        Console.WriteLine();

        // ====== 方法二：模擬法測試 ======
        Console.WriteLine("====== 方法二：模擬法測試 ======");
        Console.WriteLine();

        // 測試案例 1：範例 1
        int[] nums1_v2 = {1, 0, 2, 0, 3};
        int result1_v2 = solution.CountValidSelections2(nums1_v2);
        Console.WriteLine($"測試案例 1: nums = [{string.Join(", ", nums1_v2)}]");
        Console.WriteLine($"預期輸出: 2, 實際輸出: {result1_v2}");
        Console.WriteLine($"結果: {(result1_v2 == 2 ? "通過 ✓" : "失敗 ✗")}");
        Console.WriteLine();

        // 測試案例 2：範例 2
        int[] nums2_v2 = {2, 3, 4, 0, 4, 1, 0};
        int result2_v2 = solution.CountValidSelections2(nums2_v2);
        Console.WriteLine($"測試案例 2: nums = [{string.Join(", ", nums2_v2)}]");
        Console.WriteLine($"預期輸出: 0, 實際輸出: {result2_v2}");
        Console.WriteLine($"結果: {(result2_v2 == 0 ? "通過 ✓" : "失敗 ✗")}");
        Console.WriteLine();

        // 測試案例 3：簡單案例
        int[] nums3_v2 = {0, 1, 0};
        int result3_v2 = solution.CountValidSelections2(nums3_v2);
        Console.WriteLine($"測試案例 3: nums = [{string.Join(", ", nums3_v2)}]");
        Console.WriteLine($"預期輸出: 2, 實際輸出: {result3_v2}");
        Console.WriteLine($"結果: {(result3_v2 == 2 ? "通過 ✓" : "失敗 ✗")}");
        Console.WriteLine();

        // 測試案例 4：所有元素都是 0
        int[] nums4_v2 = {0, 0, 0};
        int result4_v2 = solution.CountValidSelections2(nums4_v2);
        Console.WriteLine($"測試案例 4: nums = [{string.Join(", ", nums4_v2)}]");
        Console.WriteLine($"預期輸出: 6, 實際輸出: {result4_v2}");
        Console.WriteLine($"結果: {(result4_v2 == 6 ? "通過 ✓" : "失敗 ✗")}");
        Console.WriteLine();

        // 測試案例 5：只有一個 0
        int[] nums5_v2 = {1, 0, 1};
        int result5_v2 = solution.CountValidSelections2(nums5_v2);
        Console.WriteLine($"測試案例 5: nums = [{string.Join(", ", nums5_v2)}]");
        Console.WriteLine($"預期輸出: 2, 實際輸出: {result5_v2}");
        Console.WriteLine($"結果: {(result5_v2 == 2 ? "通過 ✓" : "失敗 ✗")}");
    }

    /// <summary>
    /// 解題思路：前綴和
    /// 
    /// 核心概念：
    /// 把整個過程看成是「打磚塊」遊戲，對於每一個選取的初始位置，
    /// 有一個小球在左右方向上來回彈跳，每次遇到正數就反彈，同時將正數減少 1。
    /// 
    /// 算法說明：
    /// 1. 為了消除所有的正數，假設初始方向向右，那麼初始位置兩邊元素的和應該相等，
    ///    或者右邊元素和比左邊元素和大 1，此時球會在右邊完成最後一次反彈，並從左邊離開。
    /// 2. 初始方向向左時，情況是對稱的：左邊元素和應該等於或比右邊元素和大 1，
    ///    此時球會在左邊完成最後一次反彈，並從右邊離開。
    /// 3. 枚舉每一個為 0 的位置作為初始位置，並利用前綴和計算出左右兩邊的元素和，
    ///    判斷是否為有效選擇方案。
    /// 
    /// 時間複雜度：O(n)
    /// 空間複雜度：O(1)
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <returns>可能的有效選擇數量</returns>
    public int CountValidSelections(int[] nums)
    {
        int n = nums.Length;
        int res = 0;
        // 計算陣列總和
        int sum = nums.Sum();
        int left = 0;   // 左邊元素和
        int right = sum; // 右邊元素和

        // 枚舉每一個位置
        for (int i = 0; i < n; i++)
        {
            // 如果當前位置為 0，可以作為初始位置
            if (nums[i] == 0)
            {
                // 向右移動：如果右邊和 >= 左邊和，且差值 <= 1，則為有效選擇
                // right == left：兩邊相等，剛好消除所有元素
                // right == left + 1：右邊多 1，球會在右邊完成最後一次反彈，並從左邊離開
                if (right - left >= 0 && right - left <= 1)
                {
                    res++;
                }

                // 向左移動：如果左邊和 >= 右邊和，且差值 <= 1，則為有效選擇
                // left == right：兩邊相等，剛好消除所有元素
                // left == right + 1：左邊多 1，球會在左邊完成最後一次反彈，並從右邊離開
                if (left - right >= 0 && left - right <= 1)
                {
                    res++;
                }
            }
            else
            {
                // 更新前綴和：當前元素加入左邊，從右邊移除
                left += nums[i];
                right -= nums[i];
            }
        }
        return res;
    }

    /// <summary>
    /// 解題思路：模擬法
    /// 
    /// 核心概念：
    /// 由於資料量較小，我們可以直接模擬每種方案並判斷是否有效。
    /// 將陣列 nums 中每個為 0 元素的位置作為初始位置，分別向兩個方向進行模擬。
    /// 
    /// 算法說明：
    /// 1. 枚舉每個為 0 的位置作為初始位置
    /// 2. 對於每個初始位置，分別向左（-1）和向右（1）兩個方向進行模擬
    /// 3. 模擬過程：
    ///    - 判斷當前元素是否為 0，如果為 0 繼續朝原方向移動
    ///    - 否則將當前值減 1，並將方向反轉，移動到下一個位置
    /// 4. 當所有元素變為 0 或移動到陣列下標範圍外時模擬結束
    /// 5. 如果此時所有元素都變為 0 則是有效方案
    /// 
    /// 時間複雜度：O(n² × m)，其中 n 是陣列長度，m 是陣列元素的平均值
    /// 空間複雜度：O(n)，需要複製陣列進行模擬
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <returns>可能的有效選擇數量</returns>
    public int CountValidSelections2(int[] nums)
    {
        int count = 0;
        // 計算非零元素的數量，用於判斷是否所有元素都變為 0
        int nonZeros = nums.Count(x => x > 0);
        int n = nums.Length;

        // 枚舉每個為 0 的位置作為初始位置
        for (int i = 0; i < n; i++)
        {
            if (nums[i] == 0)
            {
                // 向左移動（方向 -1）
                if (IsValid(nums, nonZeros, i, -1))
                {
                    count++;
                }
                // 向右移動（方向 1）
                if (IsValid(nums, nonZeros, i, 1))
                {
                    count++;
                }
            }
        }
        return count;
    }

    /// <summary>
    /// 驗證給定的初始位置和方向是否為有效選擇
    /// </summary>
    /// <param name="nums">原始陣列</param>
    /// <param name="nonZeros">非零元素數量</param>
    /// <param name="start">起始位置</param>
    /// <param name="direction">移動方向（-1 向左，1 向右）</param>
    /// <returns>是否為有效選擇</returns>
    bool IsValid(int[] nums, int nonZeros, int start, int direction)
    {
        int n = nums.Length;
        // 複製陣列，避免修改原始資料
        int[] temp = (int[])nums.Clone();
        int curr = start;

        // 模擬移動過程，直到所有非零元素都變為 0 或移出陣列範圍
        while (nonZeros > 0 && curr >= 0 && curr < n)
        {
            // 如果當前位置的值大於 0
            if (temp[curr] > 0)
            {
                // 將當前值減 1
                temp[curr]--;
                // 反轉方向
                direction *= -1;
                // 如果當前值變為 0，非零元素數量減 1
                if (temp[curr] == 0)
                {
                    nonZeros--;
                }
            }
            // 移動到下一個位置
            curr += direction;
        }

        // 如果所有非零元素都變為 0，則為有效選擇
        return nonZeros == 0;
    }
}
