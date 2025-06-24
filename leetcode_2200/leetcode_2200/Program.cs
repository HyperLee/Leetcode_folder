namespace leetcode_2200;

class Program
{
    /// <summary>
    /// 2200. Find All K-Distant Indices in an Array
    /// https://leetcode.com/problems/find-all-k-distant-indices-in-an-array/description/?envType=daily-question&envId=2025-06-24
    /// 2200. 找出数组中的所有 K 近邻下标
    /// https://leetcode.cn/problems/find-all-k-distant-indices-in-an-array/description/?envType=daily-question&envId=2025-06-24
    /// 
    /// 給定一個 0 索引的整數陣列 nums 和兩個整數 key 及 k。
    /// 若存在至少一個索引 j 滿足 |i - j| <= k 且 nums[j] == key，則索引 i 為 k 近鄰下標。
    /// 請回傳所有 k 近鄰下標，並以遞增順序排序。 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[] nums = {3, 2, 1, 4, 2};
        int key = 2;
        int k = 1;

        var prog = new Program();
        var res1 = prog.FindKDistantIndices(nums, key, k);
        var res2 = prog.FindKDistantIndices2(nums, key, k);
        var res3 = prog.FindKDistantIndices3(nums, key, k);

        Console.WriteLine("方法一: " + string.Join(", ", res1));
        Console.WriteLine("方法二: " + string.Join(", ", res2));
        Console.WriteLine("方法三: " + string.Join(", ", res3));
    }


    /// <summary>
    /// 一遍遍歷法解題說明：
    /// 1. 先遍歷 nums，找出所有 nums[j]=key 的下標 j。
    /// 2. 對於每個符合條件的 j，計算區間 [max(0, j-k), min(n-1, j+k)]。
    /// 3. 將該區間內所有下標依序加入結果集合（用 HashSet 去重）。
    /// 4. 最後將結果集合轉成 List 並排序，得到所有 K 近鄰下標（遞增且無重複）。
    ///
    /// 本方法會重複加入下標，最終用 HashSet 去重並排序，簡單直觀但非最優化。
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="key"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public IList<int> FindKDistantIndices(int[] nums, int key, int k)
    {
        var result = new HashSet<int>(); // 使用 HashSet 防止重複
        int n = nums.Length;
        // 先找出所有 key 的索引
        for (int j = 0; j < n; j++)
        {
            if (nums[j] == key)
            {
                // 對於每個 key 的索引 j，計算範圍 [j-k, j+k]
                int left = Math.Max(0, j - k); // 左邊界不能小於 0
                int right = Math.Min(n - 1, j + k); // 右邊界不能超過陣列長度
                // 將範圍內的索引加入結果集合
                for (int i = left; i <= right; i++)
                {
                    result.Add(i); 
                }
            }
        }
        var ans = result.ToList(); // 轉成 List
        ans.Sort(); // 遞增排序
        return ans;
    }

    /// <summary>
    /// 枚舉法解題說明：
    /// 我們可以枚舉所有的下標對 (i, j)，判斷是否滿足 nums[j] = key 且 |i−j| <= k。
    /// 用 List res 維護所有 K 近鄰下標。若上述條件都滿足，則將 i 加入 res，並終止內層迴圈。
    /// 這樣可確保 res 不重複且遞增，最後回傳 res 即為答案。
    /// 
    /// ref:https://leetcode.cn/problems/find-all-k-distant-indices-in-an-array/solutions/1365061/zhao-chu-shu-zu-zhong-de-suo-you-k-jin-l-b3k2/?envType=daily-question&envId=2025-06-24
    /// 
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <param name="key">目標值</param>
    /// <param name="k">距離</param>
    /// <returns>所有 K 近鄰下標（遞增排序）</returns>
    public IList<int> FindKDistantIndices2(int[] nums, int key, int k)
    {
        List<int> res = new List<int>(); // 用來存放結果
        int n = nums.Length;
        for (int i = 0; i < n; i++) // 枚舉每個下標 i
        {
            for (int j = 0; j < n; j++) // 枚舉每個下標 j
            {
                // 若 nums[j] == key 且 |i-j| <= k，則 i 為 K 近鄰下標
                if (nums[j] == key && Math.Abs(i - j) <= k)
                {
                    res.Add(i); // 加入結果
                    break; // 為避免重複，找到一個就 break
                }
            }
        }
        return res;
    }

    /// <summary>
    /// 雙指針優化法解題說明：
    /// 1. 用 last 記錄目前視窗內最右側的 key 下標，初始設為 -k-1（保證一開始不會誤判）。
    /// 2. 先從 0 到 k-1 檢查是否有 key，若有則更新 last。
    /// 3. 依序遍歷每個下標 i：
    ///    a. 若 i+k 未超出陣列且 nums[i+k]=key，則 last 更新為 i+k。
    ///    b. 若 last >= i-k，代表 i 的視窗內有 key，將 i 加入結果。
    /// 4. 回傳所有 K 近鄰下標（遞增排序）。
    /// 
    /// ref:https://leetcode.cn/problems/find-all-k-distant-indices-in-an-array/solutions/1332985/mo-ni-by-endlesscheng-57j9/?envType=daily-question&envId=2025-06-24
    /// 
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <param name="key">目標值</param>
    /// <param name="k">距離</param>
    /// <returns>所有 K 近鄰下標（遞增排序）</returns>
    public IList<int> FindKDistantIndices3(int[] nums, int key, int k)
    {
        int last = -k - 1;
        int n = nums.Length;
        for (int i = k - 1; i >= 0; i--)
        {
            if (i < n && nums[i] == key)
            {
                last = i;
                break;
            }
        }
        List<int> ans = new List<int>();
        for (int i = 0; i < n; i++)
        {
            if (i + k < n && nums[i + k] == key)
            {
                last = i + k;
            }
            if (last >= i - k)
            {
                ans.Add(i);
            }
        }
        return ans;
    }
}
