namespace leetcode_1980;

class Program
{
    /// <summary>
    /// 1980. Find Unique Binary String
    /// https://leetcode.com/problems/find-unique-binary-string/description/?envType=daily-question&envId=2026-03-08
    ///
    /// Given an array of strings nums containing n unique binary strings each of length n,
    /// return a binary string of length n that does not appear in nums.
    /// If there are multiple answers, you may return any of them.
    ///
    /// 1980. 找出不同的二進位字串
    /// https://leetcode.cn/problems/find-unique-binary-string/description/?envType=daily-question&envId=2026-03-08
    /// 給定一個包含 n 個 唯一二進位字串 的陣列 nums，每個字串長度為 n，
    /// 回傳一個長度為 n 的二進位字串，該字串不出現在 nums 中。
    /// 如果有多個答案，可以回傳任何一個。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program p = new Program();

        // 測試資料一：n=2
        string[] nums1 = ["01", "10"];
        Console.WriteLine("=== Test 1: [\"01\", \"10\"] ===");
        Console.WriteLine($"方法一: {p.FindDifferentBinaryString(nums1)}");  // 期望: 00 或 11
        Console.WriteLine($"方法二: {p.FindDifferentBinaryString2(nums1)}"); // 期望: 00 或 11
        Console.WriteLine($"方法三: {p.FindDifferentBinaryString3(nums1)}"); // 期望: 00 或 11

        // 測試資料二：n=2，有連續數字
        string[] nums2 = ["00", "01"];
        Console.WriteLine("=== Test 2: [\"00\", \"01\"] ===");
        Console.WriteLine($"方法一: {p.FindDifferentBinaryString(nums2)}");  // 期望: 10 或 11
        Console.WriteLine($"方法二: {p.FindDifferentBinaryString2(nums2)}"); // 期望: 10 或 11
        Console.WriteLine($"方法三: {p.FindDifferentBinaryString3(nums2)}"); // 期望: 10 或 11

        // 測試資料三：n=3
        string[] nums3 = ["111", "011", "001"];
        Console.WriteLine("=== Test 3: [\"111\", \"011\", \"001\"] ===");
        Console.WriteLine($"方法一: {p.FindDifferentBinaryString(nums3)}");  // 期望: 不在 nums3 中的長度為 3 的字串
        Console.WriteLine($"方法二: {p.FindDifferentBinaryString2(nums3)}"); // 期望: 不在 nums3 中的長度為 3 的字串
        Console.WriteLine($"方法三: {p.FindDifferentBinaryString3(nums3)}"); // 期望: 不在 nums3 中的長度為 3 的字串
    }

    /// <summary>
    /// 方法一：康托對角線（條件翻轉）
    ///
    /// 解題說明：
    /// 利用「康托對角線」原理建構結果字串。
    /// 對每個位置 i，取 nums[i] 的第 i 個字元（即對角線元素），
    /// 並將其翻轉（'0' → '1'，'1' → '0'），逐字元拼接成 res。
    ///
    /// 正確性證明：
    /// 對任意 nums[i]，res 在第 i 個字元與 nums[i][i] 不同，
    /// 故 res 至少在第 i 位與 nums[i] 相異，確保 res 不等於任何 nums[i]。
    ///
    /// 時間複雜度：O(n)
    /// 空間複雜度：O(n)（結果字串）
    /// </summary>
    /// <param name="nums">長度為 n 的 n 個唯一二進位字串陣列</param>
    /// <returns>一個不存在於 nums 中的長度為 n 的二進位字串</returns>
    public string FindDifferentBinaryString(string[] nums)
    {
        string res = "";
        int n = nums.Length;

        for(int i = 0; i < n; i++)
        {
            // 取對角線位置的字元，並翻轉後直接追加至結果字串
            // nums[i][i] 是第 i 個字串的第 i 個字元（對角線元素）
            if(nums[i][i] == '0')
            {
                res += '1'; // '0' 翻轉為 '1'
            }
            else
            {
                res += '0'; // '1' 翻轉為 '0'
            }
        }
        return res;
    }

    /// <summary>
    /// 方法二：暴力枚舉（HashSet 整數映射）
    ///
    /// 解題說明：
    /// 由於 nums 中每個字串長度為 n、共 n 個字串，長度為 n 的二進位字串共有 2^n 種，
    /// 而 nums 僅佔用 n 個，必定存在至少一個缺漏值。
    /// 先將所有二進位字串轉為整數存入 HashSet，
    /// 接著從 0 開始逐一枚舉，找到第一個不在 HashSet 中的整數，
    /// 再將其轉回補齊至 n 位的二進位字串作為答案。
    ///
    /// 時間複雜度：O(n^2)（最壞情況需枚舉 n+1 個值，每次轉換為 O(n)）
    /// 空間複雜度：O(n)（HashSet 儲存 n 個整數）
    /// </summary>
    /// <param name="nums">長度為 n 的 n 個唯一二進位字串陣列</param>
    /// <returns>一個不存在於 nums 中的長度為 n 的二進位字串</returns>
    public string FindDifferentBinaryString2(string[] nums)
    {
        int n = nums.Length;

        // 將所有二進位字串轉為整數，存入 HashSet 以便 O(1) 查詢
        HashSet<int> set = new HashSet<int>();
        foreach(string num in nums)
        {
            // Convert.ToInt32(num, 2) 將二進位字串解析為十進位整數
            set.Add(Convert.ToInt32(num, 2));
        }

        // 從 0 開始往上枚舉，找到第一個不在集合中的整數
        // 由於集合中最多 n 個元素，最多枚舉到 n 即可找到答案
        int val = 0;
        while(set.Contains(val))
        {
            val++;
        }

        // 將整數轉回二進位字串
        string binary = Convert.ToString(val, 2);

        // 在左側補 '0'，確保長度恰好為 n
        while(binary.Length < n)
        {
            binary = '0' + binary;
        }
        return binary;
    }

    /// <summary>
    /// 方法3: 康托對角線
    /// 這個方法靈感來自數學家康托關於「實數是不可數無限」的證明。
    /// 例如 nums=[111,011,000]。我們可以構造一個字串 ans，滿足：
    /// ans[0]=0 != nums[0][0]。
    /// ans[1]=0 != nums[1][1]。
    /// ans[2]=1 != nums[2][2]。
    /// ans=001 和每個 nums[i] 都至少有一個字元不同，滿足題目要求。
    /// 一般地，令 ans[i]=nums[i][i]⊕1，即可滿足要求。其中 ⊕ 是異或運算。
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public string FindDifferentBinaryString3(string[] nums)
    {
        int n = nums.Length;
        char[] ans = new char[n];
        for(int i = 0; i < n; i++)
        {
            ans[i] = (char)(nums[i][i] ^ 1);
        }
        return new string(ans);
    }
}
