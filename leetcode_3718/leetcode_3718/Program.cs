namespace leetcode_3718;

class Program
{
    /// <summary>
    /// 3718. Smallest Missing Multiple of K
    /// https://leetcode.com/problems/smallest-missing-multiple-of-k/description
    /// 3718. 缺失的最小倍数
    /// https://leetcode.cn/problems/smallest-missing-multiple-of-k/description
    /// 
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一：枚举 + 哈希表
    /// 思路与算法
    /// 我们需要找到不在数组 nums 中出现的，最小的正整数 k 的倍数。
    /// 首先将 nums 中的所有元素放入哈希集合 seen 中，以便 O(1) 判断某个数是否出现过。然后从 k 的第一个正倍数 k 开始，依次
    /// 枚举 k,2k,3k,…，直到找到第一个不在 seen 中的数，即为答案。
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public int MissingMultiple(int[] nums, int k)
    {
        HashSet<int> seen = new HashSet<int>(nums);
        int multiple  = k;

        while(seen.Contains(multiple ))
        {
            multiple += k;
        }
        return multiple ;
    }

    /// <summary>
    /// 解法二: 不使用 hash, 改用 bool 來判斷
    /// 因為題目限制 nums[i] <= 100，可以用 bool[]：
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public int MissingMultiple2(int[] nums, int k)
    {
        bool[] exists = new bool[101];

        foreach(int num in nums)
        {
            exists[num] = true;
        }

        int multiple = k;

        while(multiple <= 100 && exists[multiple])
        {
            multiple += k;
        }
        return multiple;
    }

    /// <summary>
    /// 解法三: 上述兩種解法調整而已
    /// 原先是使用加法 `multiple += k;` 改為 乘法來處理
    /// k, 2k, 3k, ... 依此類推
    /// 其實道理差不多
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public int MissingMultiple3(int[] nums, int k)
    {
        HashSet<int> seen = new HashSet<int>(nums);

        int multiplier = 1; // 乘法要從1倍開始

        while(seen.Contains(k * multiplier))
        {
            // 倍數遞增
            multiplier++;
        }

        return k * multiplier;
    }
}
