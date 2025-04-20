namespace leetcode_001;

class Program
{
    /// <summary>
    ///  LeetCode 1. Two Sum
    ///  https://leetcode.com/problems/two-sum/
    ///  1. 两数之和
    /// https://leetcode.cn/problems/two-sum/description/
    /// 
    ///  Given nums = [2, 7, 11, 15], target = 9,
    ///  Because nums[0] + nums[1] = 2 + 7 = 9,
    ///  return [0, 1].
    ///  回傳 index 位置
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        int[] nums = { 2, 7, 11, 15 };
        int target = 9;
        var res = TwoSum(nums, target);
        Console.WriteLine($"ref: [{res[0]},{res[1]}]");
    }


    /// <summary>
    /// 解題思路：
    /// 1. 使用 Dictionary 作為 Hash Table，儲存已遍歷過的數字及其索引
    /// 2. 對於每個數字 nums[i]，計算目標值與當前數字的差值 (target - nums[i])
    /// 3. 如果差值存在於 Dictionary 中，表示找到配對，返回兩個數字的索引
    /// 4. 如果沒找到，將當前數字和索引加入 Dictionary
    /// 
    /// 時間複雜度：O(n) - 只需遍歷一次數組
    /// 空間複雜度：O(n) - 需要額外的 Dictionary 儲存已遍歷的數字
    /// 
    /// 優化重點：
    /// - 使用 Dictionary 將查找時間從 O(n) 降至 O(1)
    /// - 一邊遍歷一邊存儲，避免重複遍歷
    /// - 使用索引器替代 Add 方法，程式碼更簡潔
    /// 
    /// https://www.itread01.com/content/1543410439.html
    /// https://ithelp.ithome.com.tw/articles/10217042
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <param name="target">目標和</param>
    /// <returns>符合目標和的兩個數字的索引陣列</returns>
    public static int[] TwoSum(int[] nums, int target)
    {
        // 建立 Dictionary 作為 Hash Table，key 為數字值，value 為索引位置
        Dictionary<int, int> dic = new Dictionary<int, int>();
        
        // 遍歷整個數組
        for(int i = 0; i < nums.Length; i++)
        {
            // 計算需要尋找的配對數字 (目標值減去當前數字)
            int left = target - nums[i];
            
            // 檢查是否已經存在配對的數字
            if(dic.ContainsKey(left))
            {
                // 找到配對！返回兩個數字的索引
                // dic[left] 是之前存入的索引，i 是當前索引
                return new int[] { dic[left], i };
            }

            // 若當前數字不在 Dictionary 中，將其加入
            // 這裡使用判斷是為了避免重複值覆蓋原有的索引
            // 只存儲第一次出現的索引
            if (!dic.ContainsKey(nums[i]))
            {
                // dic.Add(nums[i], i);
                dic[nums[i]] = i; // 使用索引器語法，較 Add 方法更簡潔
            }
        }

        // 若找不到符合的配對，返回空陣列
        // return null;
        return Array.Empty<int>(); // 使用 Empty<int>() 替代 null，更安全
    }
}
