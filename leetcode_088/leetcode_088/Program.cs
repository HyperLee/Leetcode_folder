namespace leetcode_088;

class Program
{
    /// <summary>
    /// 88. Merge Sorted Array
    /// https://leetcode.com/problems/merge-sorted-array/description/
    /// 
    /// English:
    /// You are given two integer arrays nums1 and nums2, sorted in non-decreasing order, and two integers
    /// m and n, representing the number of elements in nums1 and nums2 respectively. Merge nums1 and nums2
    /// into a single array sorted in non-decreasing order. The final sorted array should not be returned by
    /// the function, but instead be stored inside the array nums1. To accommodate this, nums1 has a length
    /// of m + n, where the first m elements denote the elements that should be merged, and the last n
    /// elements are set to 0 and should be ignored. nums2 has a length of n.
    /// 
    /// 繁體中文:
    /// 給定兩個以非遞減順序排序的整數陣列 nums1 和 nums2，以及兩個整數 m 和 n，分別代表 nums1 和
    /// nums2 中有效元素的數量。請將 nums1 和 nums2 合併成一個以非遞減順序排序的陣列。最終排序後的
    /// 陣列不應由函式回傳，而是必須儲存在 nums1 內。為了容納合併後的結果，nums1 的長度為 m + n，
    /// 其中前 m 個元素代表需要合併的元素，最後 n 個元素設為 0 且應忽略。nums2 的長度為 n。
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一：逆向双指针
    /// 優點: 不需要項傳統正像雙指針需要一個暫存的空間變數
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="m"></param>
    /// <param name="nums2"></param>
    /// <param name="n"></param>
    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        // 陣列 index 從 0 開始
        int p1 = m - 1;
        int p2 = n - 1;
        int tail = m + n - 1;
        int cur;

        while(p1 >= 0 || p2 >= 0)
        {
            if(p1 == -1)
            {
                // p1 結束 取 p2
                cur = nums2[p2--];
            }
            else if (p2 == -1)
            {
                // p2 結束 取 p1
                cur = nums1[p1--];
            }
            else if (nums1[p1] > nums2[p2])
            {
                // 因為是逆向, 所以取大得出來.
                cur = nums1[p1--];
            }
            else
            {
                cur = nums2[p2--];
            }

            // 先放到 陣列最尾端, 從尾端往前放
            nums1[tail--] = cur;
        }
    }

    /// <summary>
    /// 方法二:雙指針(正向)
    /// 
    /// 依序去 nums1 與 nums2 中取出 element
    /// 然後比對 哪邊 小
    /// 小的取出來 然後放到 合併後的 新陣列裡面
    /// 新陣列長度 = m + n
    /// 陣列起始 index 從 0 開始
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="m"></param>
    /// <param name="nums2"></param>
    /// <param name="n"></param>
    public void Merge2(int[] nums1, int m, int[] nums2, int n)
    {
        int p1 = 0;
        int p2 = 0;
        // 暫存
        int[] sorted = new int[m + n];
        int cur;

        // 取出 cur, 放入 sorted 裡面
        while(p1 <= m || p2 <= n)
        {
            if(p1 == m)
            {
                // p1 結束取 p2
                cur = nums2[p2++];
            }
            else if(p2 == n)
            {
                // p2 結束取出 p1
                cur = nums1[p1++];
            }
            else if(nums1[p1] < nums2[p2])
            {
                // 取小的出來, 先放到陣列裡面
                cur = nums1[p1++];
            }
            else
            {
                cur = nums1[p1++];
            }

            // 陣列 index 0 開始, 扣 1
            sorted[p1 + p2 - 1] = cur;
        }

        for(int i = 0; i < m + n; i++)
        {
            nums1[i] = sorted[i];
        }
    }

    /// <summary>
    /// 方法三: 直接合併在排序
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="m"></param>
    /// <param name="nums2"></param>
    /// <param name="n"></param>
    public void Merge3(int[] nums1, int m, int[] nums2, int n)
    {
        // 合併後長度 = nums1 長度 + nums2 長度
        for(int i = 0; i < n; i++)
        {
            // nums1 結尾加上 nums2 數值
            nums1[m + i] = nums2[i];
        }

        Array.Sort(nums1);
    }
}
