namespace leetcode_2210;

class Program
{
    /// <summary>
    /// 2210. Count Hills and Valleys in an Array
    /// https://leetcode.com/problems/count-hills-and-valleys-in-an-array/description/?envType=daily-question&envId=2025-07-27
    /// 2210. 統計數組中峰和谷的數量
    /// https://leetcode.cn/problems/count-hills-and-valleys-in-an-array/description/?envType=daily-question&envId=2025-07-27
    /// 
    /// 題目描述（繁體中文）：
    /// 給定一個 0-indexed 的整數陣列 nums。
    /// 當索引 i 的最近且不相等的左右鄰居都比 nums[i] 小時，i 屬於「峰」；
    /// 當索引 i 的最近且不相等的左右鄰居都比 nums[i] 大時，i 屬於「谷」。
    /// 若相鄰的索引 i 和 j 滿足 nums[i] == nums[j]，則 i 和 j 屬於同一個峰或谷。
    /// 注意：一個索引要被視為峰或谷，必須左右都有不相等的鄰居。
    /// 請回傳 nums 中峰和谷的總數。
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試範例
        int[] nums = {2, 4, 1, 1, 6, 5};
        var program = new Program();
        int result = program.CountHillValley(nums);
        Console.WriteLine($"Hills and Valleys count: {result}"); // 預期輸出 3
    }


    /// <summary>
    /// 統計陣列中的峰與谷數量。
    /// 解題思路：
    /// 1. 先去除連續重複元素，避免重複計算（同一段相同元素只保留一個）。
    /// 2. 依序判斷每個元素是否為「峰」或「谷」：
    ///    - 峰：filtered[i-1] < filtered[i] > filtered[i+1]
    ///    - 谷：filtered[i-1] > filtered[i] < filtered[i+1]
    /// 只統計左右都有不相等鄰居的元素。
    /// 
    /// 注意:
    /// 輸入陣列要去除頭尾因為他們沒有左右鄰居
    /// nums[i] 的左右鄰居必須不相等才能被視為峰或谷。
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <returns>峰與谷的總數</returns>
    public int CountHillValley(int[] nums)
    {
        // 去除連續重複元素，僅保留每段的第一個
        var filtered = new List<int>();
        filtered.Add(nums[0]); // 保留第一個元素
        for (int i = 1; i < nums.Length; i++)
        {
            // 若與前一個元素不同，則保留
            if (nums[i] != nums[i - 1])
            {
                filtered.Add(nums[i]);
            }
        }

        int res = 0;
        // 從第二個到倒數第二個，判斷是否為峰或谷
        for (int i = 1; i < filtered.Count - 1; i++)
        {
            // 判斷「峰」：左鄰居 < 當前 > 右鄰居
            if (filtered[i] > filtered[i - 1] && filtered[i] > filtered[i + 1])
            {
                res++;
            }
            // 判斷「谷」：左鄰居 > 當前 < 右鄰居
            else if (filtered[i] < filtered[i - 1] && filtered[i] < filtered[i + 1])
            {
                res++;
            }
        }
        // 回傳峰與谷的總數
        return res;
    }
}
