namespace leetcode_1437;

class Program
{
    /// <summary>
    /// 1437. Check If All 1's Are at Least Length K Places Away
    /// https://leetcode.com/problems/check-if-all-1s-are-at-least-length-k-places-away/description/?envType=daily-question&envId=2025-11-17
    /// 1437. 是否所有 1 都至少相隔 k 個元素
    /// https://leetcode.cn/problems/check-if-all-1s-are-at-least-length-k-places-away/description/?envType=daily-question&envId=2025-11-17
    /// 題目描述(繁體中文): 給定一個二進位陣列 nums 和整數 k，若所有值為 1 的元素彼此間至少相隔 k 個元素，回傳 true；否則回傳 false。
    /// 
    /// 
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 簡單測試範例
        var program = new Program();
        int[] nums1 = new int[] { 1, 0, 0, 0, 1, 0, 0, 1 };
        Console.WriteLine($"nums1 -> k=2 : {program.KLengthApart(nums1, 2)} (expected True)");
        int[] nums2 = new int[] { 1, 0, 0, 1, 0, 1 };
        Console.WriteLine($"nums2 -> k=2 : {program.KLengthApart(nums2, 2)} (expected False)");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public bool KLengthApart(int[] nums, int k)
    {
        int prev = -1;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 1)
            {
                if (prev != -1)
                {
                    if (i - prev - 1 < k) return false;
                }
                prev = i;
            }
        }

        return true;
    }
}
