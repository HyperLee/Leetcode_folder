namespace leetcode_2962;

class Program
{
    /// <summary>
    /// 2962. Count Subarrays Where Max Element Appears at Least K Times
    /// https://leetcode.com/problems/count-subarrays-where-max-element-appears-at-least-k-times/description/?envType=daily-question&envId=2024-03-29
    /// 2962. 统计最大元素出现至少 K 次的子数组
    /// https://leetcode.cn/problems/count-subarrays-where-max-element-appears-at-least-k-times/description/
    /// 
    /// 題目描述：
    /// 給定一個整數數組 nums 和一個整數 k，請找出子數組的數量，這些子數組的最大元素至少出現 k 次。
    /// 解題出發點建議：
    /// 1. 使用滑動視窗技術來處理子數組的範圍。
    /// 2. 需要統計最大元素的出現次數，並根據條件調整視窗的起點。
    /// 3. 注意結果的數據類型為 long，避免溢出。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料 1
        int[] input1 = { 1, 3, 2, 3, 3 };
        int k1 = 2;
        Console.WriteLine("Test Case 1 - CountSubarrays: " + CountSubarrays(input1, k1));
        Console.WriteLine("Test Case 1 - CountSubarrays2: " + CountSubarrays2(input1, k1));

        // 測試資料 2
        int[] input2 = { 1, 1, 1, 1 };
        int k2 = 3;
        Console.WriteLine("Test Case 2 - CountSubarrays: " + CountSubarrays(input2, k2));
        Console.WriteLine("Test Case 2 - CountSubarrays2: " + CountSubarrays2(input2, k2));

        // 測試資料 3
        int[] input3 = { 5, 5, 5, 5, 5 };
        int k3 = 1;
        Console.WriteLine("Test Case 3 - CountSubarrays: " + CountSubarrays(input3, k3));
        Console.WriteLine("Test Case 3 - CountSubarrays2: " + CountSubarrays2(input3, k3));

        // 測試資料 4
        int[] input4 = { 1, 2, 3, 4, 5 };
        int k4 = 1;
        Console.WriteLine("Test Case 4 - CountSubarrays: " + CountSubarrays(input4, k4));
        Console.WriteLine("Test Case 4 - CountSubarrays2: " + CountSubarrays2(input4, k4));

        // 測試資料 5
        int[] input5 = { 10, 10, 10, 10, 10, 10 };
        int k5 = 6;
        Console.WriteLine("Test Case 5 - CountSubarrays: " + CountSubarrays(input5, k5));
        Console.WriteLine("Test Case 5 - CountSubarrays2: " + CountSubarrays2(input5, k5));

        // 測試資料 6
        int[] input6 = { 1, 2, 3, 2, 1 };
        int k6 = 2;
        Console.WriteLine("Test Case 6 - CountSubarrays: " + CountSubarrays(input6, k6));
        Console.WriteLine("Test Case 6 - CountSubarrays2: " + CountSubarrays2(input6, k6));
    }


    /// <summary>
    /// https://leetcode.cn/problems/count-subarrays-where-max-element-appears-at-least-k-times/solutions/2561054/2962-tong-ji-zui-da-yuan-su-chu-xian-zhi-t910/
    /// https://leetcode.cn/problems/count-subarrays-where-max-element-appears-at-least-k-times/solutions/2560940/hua-dong-chuang-kou-fu-ti-dan-pythonjava-xvwg/
    /// 
    /// 
    /// 滑動視窗概念題型
    /// [start, end] 整體視窗往右滑動
    /// 
    /// end 先往右, 如果 end 元素為 maxnum 就累加其次數
    /// 當 次數達到 k 時候
    /// 此時需要考慮把 最左邊 start 的元素移除
    /// 這樣才能納入新的子陣列組合
    /// 每次剔除組合 就可以加入新的組合
    /// 故結果只要統計 start 次數 即可知道有多少種組合
    /// 
    /// 請注意 題目 function 回傳是 long 不是 int
    /// 
    /// 時間複雜度: O(n)，其中 n 是陣列長度
    /// 空間複雜度: O(1)，只使用了有限的變數
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static long CountSubarrays(int[] nums, int k)
    {
        int maxnum = 0;
        foreach (int i in nums) 
        {
            // 找出 nums 中最大的 element
            maxnum = Math.Max(maxnum, i);
        }

        long res = 0;
        int start = 0, end = 0;
        int length = nums.Length;
        // 統計 element 出現次數
        int maxcount = 0;

        while(end < length)
        {
            if (nums[end] == maxnum) 
            {
                // element 數值為 最大元素,
                // 累加 出現次數
                maxcount++;
            }

            while(maxcount == k)
            {
                // 當最大元素 出現次數達到 k
                // 要讓視窗 start 往右滑動
                // 如果 start 符合 maxnum 就要扣除 次數累加
                if (nums[start] == maxnum) 
                {
                    maxcount--;
                }
                start++;
            }

            res += start;
            end++;
        }

        return res;
    }


    /// <summary>
    /// 此解法是解決「最大元素出現至少 k 次的子陣列計數」問題的優化版本
    /// 其實這兩方法差異不大(forach 換成 for)，但是這個方法更簡化了邏輯可讀性比較好
    /// 
    /// 解題思路：
    /// 1. 使用優化的滑動視窗技術，透過一次遍歷解決問題
    /// 2. 使用 LINQ 的 Max() 方法直接找出陣列中的最大值
    /// 3. 維護一個滑動視窗，追蹤視窗內最大元素的出現次數
    /// 4. 當最大元素出現次數恰好等於 k 時，縮小視窗左邊界，直到條件不再滿足
    /// 5. 利用 start 位置來計算符合條件的子陣列數量
    /// 
    /// 解法特點：
    /// - 簡化邏輯：使用 for 迴圈控制視窗右邊界，程式碼結構更清晰
    /// - 精準條件判斷：使用 maxcount == k 作為條件，避免不必要的視窗調整
    /// - 直接計數：每次處理完視窗後，直接累加 start 值得到結果
    /// 
    /// 時間複雜度: O(n)，其中 n 是陣列長度，每個元素最多被處理兩次
    /// 空間複雜度: O(1)，只使用了有限的變數
    /// 
    /// 請注意 題目 function 回傳是 long 不是 int
    /// </summary>
    /// <param name="nums">輸入整數陣列</param>
    /// <param name="k">最大元素需要出現的最小次數</param>
    /// <returns>符合條件的子陣列數量</returns>
    public static long CountSubarrays2(int[] nums, int k)
    {
        int maxnum = nums.Max(); // 找出陣列中的最大值
        long res = 0;
        int start = 0, maxcount = 0;

        for (int end = 0; end < nums.Length; end++)
        {
            // 當遇到最大值時，增加其計數
            if (nums[end] == maxnum) 
            {
                maxcount++;
            }

            // 當最大值出現次數恰好等於 k 時
            // 移動左邊界以維護視窗條件
            while (maxcount == k) 
            {
                // 如果移除的元素是最大值，則減少計數
                if (nums[start] == maxnum) 
                {
                    maxcount--;
                }
                start++;
            }

            // 累加以 start 為起點的所有可能子陣列數量
            // 這代表從 start 到 end 的所有子陣列中，最大值出現次數都小於 k
            res += start; 
        }

        return res;
    }

}
