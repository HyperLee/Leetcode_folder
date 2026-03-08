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
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public string FindDifferentBinaryString(string[] nums)
    {
        string res = "";
        int n = nums.Length;

        for(int i = 0; i < n; i++)
        {
            string tmp = nums[i][i].ToString();
            if(nums[i][i] == '0')
            {
                tmp += '1';
            }
            else
            {
                tmp += '0';
            }
        }
        return res;
    }

    /// <summary>
    /// 方法二: 暴力枚舉    
    /// 暴力枚舉轉化為整數，從 0 到 2^n - 1，將每個整數轉化為二進位字串，檢查是否在 nums 中出現過。
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public string FindDifferentBinaryString2(string[] nums)
    {
        int n = nums.Length;
        // 预处理对应整数的哈希集合
        HashSet<int> set = new HashSet<int>();
        foreach(string num in nums)
        {
            // 将二进位字串转化为整数
            set.Add(Convert.ToInt32(num, 2));
        }

        // 寻找第一个不在哈希集合中的整数
        int val = 0;
        while(set.Contains(val))
        {
            val++;
        }

        // 将整数转化为二进制字符串返回
        string binary = Convert.ToString(val, 2);
        // 将二进位字串补齐到 n 位
        while(binary.Length < n)
        {
            binary = '0' + binary;
        }
        return binary;
    }

    /// <summary>
    /// 方法3: 康托对角线
    /// 这个方法灵感来自数学家康托关于「实数是不可数无限」的证明。
    /// 例如 nums=[111,011,000]。我们可以构造一个字符串 ans，满足：
    /// ans[0]=0 != nums[0][0]。
    /// ans[1]=0 != nums[1][1]。
    /// ans[2]=1 != nums[2][2]。
    /// ans=001 和每个 nums[i] 都至少有一个字符不同，满足题目要求。
    /// 一般地，令 ans[i]=nums[i][i]⊕1，即可满足要求。其中 ⊕ 是异或运算。
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
