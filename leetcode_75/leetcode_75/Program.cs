namespace leetcode_75;

class Program
{
    /// <summary>
    /// LeetCode 75. 颜色分類（Sort Colors）
    /// 題目描述：
    /// 給定一個包含 0、1 和 2 的整數陣列 nums，請你原地對陣列進行排序，使得相同顏色的元素相鄰，並按照 0、1、2 的順序排列。
    /// 你必須在不使用內建 sort 函式的情況下完成這個問題。
    /// 
    /// 解題提示：
    /// 1. 可以使用氣泡排序、計數排序或雙指針（荷蘭國旗問題）等方法。
    /// 2. 本範例採用氣泡排序，雖然效率較低，但實作簡單。
    /// 3. 若需最佳化，建議參考一次遍歷的雙指針法。
    /// 
    /// 題目連結：
    /// https://leetcode.com/problems/sort-colors/description/?envType=daily-question&envId=2025-05-17
    /// https://leetcode.cn/problems/sort-colors/description/?envType=daily-question&envId=2025-05-17
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[] nums = { 2, 0, 2, 1, 1, 0 };
        Console.WriteLine($"原始陣列: {string.Join(", ", nums)}");

        // 建立 Program 物件並呼叫 SortColors 進行排序
        Program p = new Program();
        p.SortColors(nums);
        Console.WriteLine($"SortColors 排序後陣列: {string.Join(", ", nums)}");
        int[] expected = { 0, 0, 1, 1, 2, 2 };
        bool isCorrect = nums.SequenceEqual(expected);
        Console.WriteLine($"SortColors 排序正確: {isCorrect}");

        // 測試 SortColors2
        int[] nums2 = { 2, 0, 2, 1, 1, 0 };
        Console.WriteLine($"\n原始陣列: {string.Join(", ", nums2)}");
        p.SortColors2(nums2);
        Console.WriteLine($"SortColors2 排序後陣列: {string.Join(", ", nums2)}");
        bool isCorrect2 = nums2.SequenceEqual(expected);
        Console.WriteLine($"SortColors2 排序正確: {isCorrect2}");

        // 測試 SortColors3
        int[] nums3 = { 2, 0, 2, 1, 1, 0 };
        Console.WriteLine($"\n原始陣列: {string.Join(", ", nums3)}");
        p.SortColors3(nums3);
        Console.WriteLine($"SortColors3 排序後陣列: {string.Join(", ", nums3)}");
        bool isCorrect3 = nums3.SequenceEqual(expected);
        Console.WriteLine($"SortColors3 排序正確: {isCorrect3}");
    }


    /// <summary>
    /// 無腦氣泡排序
    /// 這個解法是最簡單的，但效率不高，時間複雜度是 O(n^2)，空間複雜度是 O(1)
    /// 這個解法的思路是，從頭到尾遍歷數組，對於每一對相鄰的元素，如果前一個元素大於後一個元素，就交換它們的位置。這樣，每次遍歷都能把最大的元素放到最後面。
    /// 這樣重複 n-1 次，就能把所有的元素都排好序了。
    /// </summary>
    /// <param name="nums">待排序的整數陣列，僅包含 0、1、2</param>
    public void SortColors(int[] nums)
    {
        int n = nums.Length;
        // 外層 for 迴圈控制排序輪數
        for (int i = 0; i < n - 1; i++)
        {
            bool swap = false; // 標記本輪是否有交換

            // 內層 for 迴圈進行相鄰元素比較與交換
            for (int j = 0; j < n - i - 1; j++)
            {
                if (nums[j] > nums[j + 1]) // 若前一個元素大於後一個，則交換
                {
                    int temp = nums[j];
                    nums[j] = nums[j + 1];
                    nums[j + 1] = temp;
                    swap = true; // 有交換則設為 true
                }
            }

            if (!swap)
            {
                // 若本輪無交換，代表已排序完成，可提前結束
                break;
            }
        }
    }


    /// <summary>
    /// 計數排序    
    /// 這個解法的時間複雜度是 O(n)，空間複雜度是 O(1)
    /// 這個解法的思路是，先遍歷數組，計算每個顏色的個數，然後根據計數結果重新填充原陣列。
    /// 這樣就能在 O(n) 的時間內完成排序。
    /// 這個解法的優點是簡單易懂，且效率較高，適合用於數量較少的顏色分類問題。
    /// 這個解法的缺點是需要額外的空間來存儲計數結果，對於顏色數量較多的情況，可能會浪費空間。
    /// 這個解法的適用場景是數量較少的顏色分類問題，例如 0、1、2 的情況。
    /// </summary>
    /// <param name="nums"></param>
    public void SortColors2(int[] nums)
    {
        int[] count = new int[3]; // 計數陣列，分別計算 0、1、2 的個數

        // 計算每個顏色的個數
        for (int i = 0; i < nums.Length; i++)
        {
            count[nums[i]]++;
        }

        // 根據計數結果重新填充原陣列
        int index = 0;
        for (int i = 0; i < count.Length; i++)
        {
            for (int j = 0; j < count[i]; j++)
            {
                nums[index++] = i;
            }
        }
    }


    /// <summary>
    /// 雙指針法（荷蘭國旗問題）
    /// 解題方法說明：
    /// 本方法利用三個指標：p0、p1 以及當前索引 i。
    /// - p0：指向下一個應該放 0 的位置。
    /// - p1：指向下一個應該放 1 的位置。
    /// - i：遍歷整個陣列。
    /// 當 nums[i] == 0 時，將其與 p0 位置交換，若 p0 < p1，還需與 p1 位置交換，確保 1 的順序。
    /// 當 nums[i] == 1 時，將其與 p1 位置交換。
    /// 這樣一次遍歷即可完成排序，時間複雜度 O(n)，空間複雜度 O(1)。
    /// </summary>
    /// <param name="nums">待排序的整數陣列，僅包含 0、1、2</param>
    public void SortColors3(int[] nums)
    {
        int n = nums.Length;
        int p0 = 0; // 指向當前要放置 0 的位置
        int p1 = 0; // 指向當前要放置 1 的位置

        for (int i = 0; i < n; i++)
        {
            if (nums[i] == 0)
            {
                // 將 0 放到 p0 位置
                int temp = nums[i];
                nums[i] = nums[p0];
                nums[p0] = temp;
                // 若 p0 < p1，需再將 1 放到 p1 位置，確保 1 的順序
                if (p0 < p1)
                {
                    temp = nums[i];
                    nums[i] = nums[p1];
                    nums[p1] = temp;
                }
                p0++;
                p1++;
            }
            else if (nums[i] == 1)
            {
                // 將 1 放到 p1 位置
                int temp = nums[i];
                nums[i] = nums[p1];
                nums[p1] = temp;
                p1++;
            }
            // 若為 2，直接跳過，因為 2 會自然留在最後
        }
    }
}
