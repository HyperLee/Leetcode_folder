using System.Runtime.InteropServices;

namespace leetcode_2799;

class Program
{
    /// <summary>
    /// 2799. Count Complete Subarrays in an Array
    /// https://leetcode.com/problems/count-complete-subarrays-in-an-array/description/?envType=daily-question&envId=2025-04-24
    /// 2799. 统计完全子数组的数目
    /// https://leetcode.cn/problems/count-complete-subarrays-in-an-array/description/?envType=daily-question&envId=2025-04-24
    /// 
    /// Array, Sliding Window, Hash Table
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[] nums1 = { 1, 3, 1, 2, 2 };
        int[] nums2 = { 1, 2, 3, 4 };
        int[] nums3 = { 1, 1, 1, 1 };

        // 建立 Program 物件以呼叫 CountCompleteSubarrays 函式
        Program program = new Program();

        // 驗證與測試
        Console.WriteLine("測試資料 1: [1, 3, 1, 2, 2]");
        Console.WriteLine("完整子陣列數量: " + program.CountCompleteSubarrays(nums1)); // 預期輸出: 4

        Console.WriteLine("測試資料 2: [1, 2, 3, 4]");
        Console.WriteLine("完整子陣列數量: " + program.CountCompleteSubarrays(nums2)); // 預期輸出: 10

        Console.WriteLine("測試資料 3: [1, 1, 1, 1]");
        Console.WriteLine("完整子陣列數量: " + program.CountCompleteSubarrays(nums3)); // 預期輸出: 10
    }

    /// <summary>
    /// 2799. Count Complete Subarrays in an Array 統計完整子陣列的數目
    /// 
    /// 題目說明：
    /// 給定一個正整數陣列 nums，「完整子陣列」定義為具有和原始陣列相同數量不同元素的子陣列。
    /// 例如，如果原始陣列有 3 個不同的元素，那麼任何包含這 3 個不同元素的子陣列都是「完整子陣列」。
    /// 
    /// 簡單說假如原始輸入的 nums 陣列中有 k 個不同的元素，那麼任何包含這 k 個不同元素的子陣列都是完整子陣列。
    /// 注意只要包涵了這 k 個不同元素的子陣列都是完整子陣列，元素的數量不需要等於 k。
    /// 
    /// 解題思路：
    /// 使用滑動窗口（Sliding Window）技術求解。先計算原始陣列中不同元素的數量，
    /// 然後遍歷每個可能的起始位置，對每個起始位置，找出最短的窗口使其包含所有不同元素，
    /// 然後計算以該起始位置開始的有效子陣列數量。
    /// 
    /// 固定左邊界，然後右邊界不斷向右移動，直到窗口中包含所有不同元素。
    /// 當不同元素的數量等於原始陣列中的不同元素數量時（此時已經符合題目描述的完整子陣列）。
    /// 再者繼續計算右邊界到陣列結尾的所有子陣列數量，這些子陣列都是完整的子陣列。
    /// 
    /// 時間複雜度：O(n)，其中 n 為陣列長度
    /// 空間複雜度：O(k)，其中 k 為不同元素的數量
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <returns>完整子陣列的數量</returns>
    public int CountCompleteSubarrays(int[] nums)
    {
        int res = 0; 
        // 記錄當前窗口中每個元素的出現次數
        // key 為元素，value 為該元素在當前窗口中的出現次數
        Dictionary<int, int> count = new Dictionary<int, int>();
        int n = nums.Length; 
        // 右指針初始位置
        int right = 0; 
        // 計算原始陣列中不同元素的數量
        int distinct = new HashSet<int>(nums).Count; 
        
        // 遍歷每個可能的起始位置（左指針）
        for(int left = 0; left < n; left++)
        {
            // 當左指針移動時，需要從窗口中移除左側元素
            if(left > 0)
            {
                // 需要移除的元素
                int remove = nums[left - 1]; 
                // 減少該元素的計數
                count[remove]--; 
                // 如果該元素計數為 0，則從窗口中完全移除
                if(count[remove] == 0) 
                {
                    count.Remove(remove);
                }
            }

            // 擴展右指針，直到窗口中包含所有不同的元素
            while(right < n && count.Count < distinct)
            {
                // 需要添加的元素
                int add = nums[right]; 
                // 如果該元素尚未在窗口中
                if(!count.ContainsKey(add)) 
                {
                    // 初始化計數
                    count[add] = 1; 
                }
                else
                {
                    // 如果該元素已經在窗口中，則增加計數
                    count[add]++; 
                }
                // 右指針右移
                right++; 
            }

            // 如果窗口中包含所有不同的元素（即形成了一個完整子陣列）
            if(count.Count == distinct)
            {
                // 對於當前左指針，可以形成的完整子陣列數量等於從 right 到數組末尾的元素數量 + 1
                // 這是因為任何包含 [left, right-1] 且向右延伸的子陣列都是完整的
                res += n - right + 1;
            }
        }
        return res;
    }
}
