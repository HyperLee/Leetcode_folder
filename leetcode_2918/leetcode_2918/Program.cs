namespace leetcode_2918;

class Program
{   
    /// <summary>
    /// 2918. Minimum Equal Sum of Two Arrays After Replacing Zeros
    /// https://leetcode.com/problems/minimum-equal-sum-of-two-arrays-after-replacing-zeros/description/?envType=daily-question&envId=2025-05-10
    /// 2918. 数组的最小相等和
    /// https://leetcode.cn/problems/minimum-equal-sum-of-two-arrays-after-replacing-zeros/description/
    /// 
    /// 題目概述:
    /// 給定兩個整數陣列 nums1 和 nums2，我們需要將兩個陣列中所有的 0 替換為任意正整數，
    /// 使得兩個陣列的和相等，並找出可能的最小相等和。如果無法達成，則回傳 -1。
    /// 
    /// 解題概念:
    /// 1. 因為 0 只能替換為正整數，所以我們需要將每個 0 至少替換為 1
    /// 2. 計算出兩個陣列的初始和（將 0 替換為 1 後）
    /// 3. 若某個陣列無 0 且和較小，就無法達成相等和
    /// 4. 若有解，最小相等和即為兩陣列替換後和值的較大者
    /// </summary>
    /// <param name="args"></param>    
    static void Main(string[] args)
    {        // 測試資料（有解）
        int[] numsA = { 3, 2, 0, 1, 0 };
        int[] numsB = { 6, 5, 0 };
        Program program = new Program();
        long minSumResultA = program.MinSum(numsA, numsB);
        long minimumSumResultA = program.MinimumSum(numsA, numsB);
        Console.WriteLine($"[有解] MinSum 結果: {minSumResultA}"); // 預期 13
        Console.WriteLine($"[有解] MinimumSum 結果: {minimumSumResultA}"); // 預期 13 或 7（依題目定義）

        // 測試資料（無解）
        int[] numsC = { 2, 3, 4 };
        int[] numsD = { 10, 0 };
        long minSumResultB = program.MinSum(numsC, numsD);
        long minimumSumResultB = program.MinimumSum(numsC, numsD);
        Console.WriteLine($"[無解] MinSum 結果: {minSumResultB}"); // 預期 -1
        Console.WriteLine($"[無解] MinimumSum 結果: {minimumSumResultB}"); // 預期 -1
    }


    /// <summary>
    /// 計算兩個陣列經過替換 0 後可能達到的最小相等和
    /// 
    /// 解題思路:
    /// 1. 計算兩個陣列的總和，同時將每個 0 視為 1 (因為 0 必須替換為至少為 1 的正整數)
    /// 2. 記錄每個陣列中 0 的數量，這代表該陣列可以增加的彈性
    /// 3. 判斷是否有可能使兩陣列相等:
    ///    - 如果有一個陣列沒有 0 (無法增加和值)，而另一個陣列的和值更大，則無解
    /// 4. 如果有解，則最小相等和為兩陣列替換後和值的較大者
    /// 
    /// 最小相等和就是兩個陣列「初始總和」的較大值。
    /// 這是因為總和較小的陣列可以透過將其中的 0 替換為較大的值來達到與另一個陣列相同的總和。
    /// 
    /// 時間複雜度：兩者都是 O(n + m)，只需遍歷兩個陣列各一次。
    /// 空間複雜度：兩者都是 O(1)，只用到固定數量變數。
    /// </summary>
    /// <param name="nums1"> 第一個整數陣列 </param>
    /// <param name="nums2"> 第二個整數陣列 </param>
    /// <returns > 兩個陣列可能達到的最小相等和，如果無解則回傳 -1</returns>
    public long MinSum(int[] nums1, int[] nums2)
    {
        // 初始化兩個陣列的總和與 0 的計數
        long sum1 = 0, sum2 = 0;
        long zeroCount1 = 0, zeroCount2 = 0;

        // 計算第一個陣列的總和，並將每個 0 替換為 1
        foreach(int i in nums1)
        {
            sum1 += i;
            if (i == 0)
            {
                sum1++; // 將 0 替換為 1 (增加總和)
                zeroCount1++; // 紀錄 0 的數量
            }
        }

        // 計算第二個陣列的總和，並將每個0替換為1
        foreach(int i in nums2)
        {
            sum2 += i;
            if (i == 0)
            {
                sum2++; // 將 0 替換為 1 (增加總和)
                zeroCount2++; // 紀錄 0 的數量
            }
        }

        // 判斷是否有解:
        // 1. 如果第一個陣列沒有 0 (無法增加) 且 第二個陣列的和大於第一個陣列，則無解
        // 2. 如果第二個陣列沒有 0 (無法增加) 且 第一個陣列的和大於第二個陣列，則無解
        if((zeroCount1 == 0 && sum2 > sum1) || (zeroCount2 == 0 && sum1 > sum2))
        {
            return -1;
        }

        // 有解的情況下，最小相等和就是兩個陣列替換後和的較大值
        return Math.Max(sum1, sum2);
    }    
    
    
    /// <summary>
    /// 這方法比較易讀
    /// 這個解法與前面的 MinSum 相比，處理邊界情況更加細緻，
    /// 特別是當只有一個陣列有零時進行了更嚴格的範圍檢查。
    /// 
    /// 計算兩個陣列經過替換 0 後可能達到的最小相等和 (另一種實作方式)
    /// 
    /// 解題概念:
    /// 1. 計算兩個陣列非零元素的總和，同時統計零的數量
    /// 2. 考慮三種情況:
    ///    a. 兩個陣列都沒有零 - 只有當兩陣列總和已經相等時才有解
    ///    b. 兩個陣列都有零 - 最小相等和為將零替換為 1 後兩陣列總和的較大值
    ///    c. 只有一個陣列有零:
    ///       - 如果有零的陣列可以通過替換零使總和達到另一陣列，則有解
    ///       - 零的最小值是 1，最大值可以是 int.MaxValue
    /// 
    /// 時間複雜度：兩者都是 O(n + m)，只需遍歷兩個陣列各一次。
    /// 空間複雜度：兩者都是 O(1)，只用到固定數量變數。
    /// </summary>
    /// <param name="nums1">第一個整數陣列</param>
    /// <param name="nums2">第二個整數陣列</param>    
    /// <returns>兩個陣列可能達到的最小相等和，如果無解則回傳 -1</returns>
    public long MinimumSum(int[] nums1, int[] nums2) 
    {
        long sum1 = 0, sum2 = 0;
        int zero1 = 0, zero2 = 0;

        // 計算第一個陣列的非零元素總和及零的數量
        foreach (var num in nums1) 
        {
            if (num == 0) 
            {
                zero1++; // 統計零的數量
            }
            else 
            {
                sum1 += num;      // 累加非零元素
            }
        }

        // 計算第二個陣列的非零元素總和及零的數量
        foreach (var num in nums2) 
        {
            if (num == 0) 
            {
                zero2++; // 統計零的數量
            }
            else 
            {
                sum2 += num;      // 累加非零元素
            }
        }  
          // 情況 a: 兩個陣列都沒有零，只有當原本總和相等時才有解
        if (zero1 == 0 && zero2 == 0) 
        {
            return sum1 == sum2 ? sum1 : -1; // 若總和已相等則回傳總和，否則無解
        }

        // 計算兩個陣列將零替換為 1 後的最小可能總和
        long minSum1 = sum1 + zero1; // 第一個陣列的最小可能總和
        long minSum2 = sum2 + zero2; // 第二個陣列的最小可能總和

        // 情況 b: 兩個陣列都有零，可以彈性調整到相同總和
        if (zero1 > 0 && zero2 > 0) 
        {
            return Math.Max(minSum1, minSum2); // 回傳最小可能總和的較大值
        }

        // 情況 c-1: 只有第一個陣列有零
        if (zero1 > 0) 
        {
            // 檢查範圍：sum1 + zero1 (最小可能值) <= sum2 <= sum1 + zero1*int.MaxValue (最大可能值)
            return (sum1 + zero1 <= sum2 && sum2 <= sum1 + (long)zero1 * int.MaxValue) ? sum2 : -1;
        }

        // 情況 c-2: 只有第二個陣列有零 (zero2 > 0)
        // 檢查範圍：sum2 + zero2 (最小可能值) <= sum1 <= sum2 + zero2*int.MaxValue (最大可能值)
        return (sum2 + zero2 <= sum1 && sum1 <= sum2 + (long)zero2 * int.MaxValue) ? sum1 : -1;
    }
}
