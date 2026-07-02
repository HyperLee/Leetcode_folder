namespace leetcode_268;

class Program
{
    /// <summary>
    /// 268. Missing Number
    /// https://leetcode.com/problems/missing-number/description/
    /// 268. 丢失的数字
    /// https://leetcode.cn/problems/missing-number/description/
    /// Given an array nums containing n distinct numbers in the range [0, n], 
    /// return the only number in the range that is missing from the array.
    /// 
    /// 給定一個陣列 nums，其中包含範圍 [0, n] 內 n 個互不相同的數字，
    /// 請回傳此範圍中唯一缺少的那個數字。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一:排序
    /// 利用排序下去找出 缺少的
    /// 題目有說 範圍: [0, n]
    /// 從0開始
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MissingNumber(int[] nums)
    {
        Array.Sort(nums);
        // 迴圈找出缺失的數字  0 <= i < n
        for(int i = 0;i < nums.Length; i++)
        {
            if(nums[i] != i)
            {
                return i;
            }
        }
        // 上述迴圈都存在那就是缺失n; 上述迴圈不包含 n
        return nums.Length;
    }

    /// <summary>
    /// 解法二: hashset
    /// 使用哈希集合，可以将时间复杂度降低到 O(n)。
    /// 首先遍历数组 nums，将数组中的每个元素加入哈希集合，
    /// 然后依次检查从 0 到 n 的每个整数是否在哈希集合中，不在哈希集合
    /// 中的数字即为丢失的数字。
    /// 由于哈希集合的每次添加元素和查找元素的时间复杂度都是 O(1)，因此总时间复杂度是 O(n)
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MissingNumber2(int[] nums)
    {
        ISet<int> numSet = new HashSet<int>(nums);
        int n = nums.Length;
        for(int i = 0; i < n; i++)
        {
            numSet.Add(nums[i]);
        }

        int missingNumber = -1;
        for(int i = 0; i <= n; i++)
        {
            if(!numSet.Contains(i))
            {
                missingNumber = i;
                break;
            }
        }
        return missingNumber;
    }

    /// <summary>
    /// 位元運算
    /// 数组 nums 中有 n 个数，在这 n 个数的后面添加从 0 到 n 的每个整数，则添加了 n+1 个整数，共有 2n+1 个整数。
    /// 在 2n+1 个整数中，丢失的数字只在后面 n+1 个整数中出现一次，其余的数字在前面 n 个整数中（即数组中）和后面 n+1
    /// 个整数中各出现一次，即其余的数字都出现了两次。
    /// 根据出现的次数的奇偶性，可以使用按位异或运算得到丢失的数字。按位异或运算 ⊕ 满足交换律和结合律，且对任意整数 x 都满
    /// 足 x⊕x=0 和 x⊕0=x。
    /// 由于上述 2n+1 个整数中，丢失的数字出现了一次，其余的数字都出现了两次，因此对上述 2n+1 个整数进行按位异或运算
    /// 结果即为丢失的数字。
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MissingNumber3(int[] nums)
    {
        int xor = 0;
        int n = nums.Length;
        for(int i = 0; i < n; i++)
        {
            xor ^= nums[i];
        }

        for(int i = 0; i <= n; i++)
        {
            xor ^= i;
        }
        return xor;
    }
}
