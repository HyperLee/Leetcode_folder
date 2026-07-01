namespace leetcode_260;

class Program
{
    /// <summary>
    /// 260. Single Number III
    /// https://leetcode.com/problems/single-number-iii/description/
    /// 260. 只出现一次的数字 III
    /// https://leetcode.cn/problems/single-number-iii/description/
    ///
    /// English:
    /// Given an integer array nums, in which exactly two elements appear only once and all the other elements appear exactly twice.
    /// Find the two elements that appear only once. You can return the answer in any order.
    /// You must write an algorithm that runs in linear runtime complexity and uses only constant extra space.
    ///
    /// 繁體中文:
    /// 給定一個整數陣列 nums，其中恰好有兩個元素只出現一次，其餘所有元素都恰好出現兩次。
    /// 找出這兩個只出現一次的元素。答案可以任意順序回傳。
    /// 你必須撰寫一個執行時間複雜度為線性，且只使用常數額外空間的演算法。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一：哈希表
    /// 我们可以使用一个哈希映射统计数组中每一个元素出现的次数。
    /// 在统计完成后，我们对哈希映射进行遍历，将所有只出现了一次的数放入答案中。
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int[] SingleNumber(int[] nums)
    {
        List<int> list = new List<int>();
        Dictionary<int, int> dic = new Dictionary<int, int>();

        for(int i = 0; i < nums.Length; i++)
        {
            if(dic.ContainsKey(nums[i]))
            {
                dic[nums[i]]++;
            }
            else
            {
                dic.Add(nums[i], 1);
            }
        }

        foreach(var item in dic)
        {
            if(item.Value == 1)
            {
                list.Add(item.Key);
            }
        }
        return list.ToArray();
    }

    /// <summary>
    /// 方法二：位运算
    /// 在理解如何使用位运算解决本题前，读者需要首先掌握「136. 只出现一次的数字」中的位运算做法。
    /// 假设数组 nums 中只出现一次的元素分别是 x1 和 x2。如果把 nums 中的所有元素全部异或起来，得到结果 x，那么一定有：
    /// x = x1 ⊕ x2
    /// 其中 ⊕ 表示异或运算。这是因为 nums 中出现两次的元素都会因为异或运算的性质 a⊕b⊕b=a 抵消掉，那么最终的结果就只剩
    /// 下 x1 和 x2 的异或和。
    /// x 显然不会等于 0，因为如果 x=0，那么说明 x1 = x2 这样 x1 和 x2  就不是只出现一次的数字了。因此，我们可以使用位运算
    /// x & -x 取出 x 的二进制表示中最低位那个 1，设其为第 l 位，那么 x1 和 x2 中的某一个数的二进制表示的第 l 位为 0，另一个数
    /// 的二进制表示的第 l 位为 1。在这种情况下，x1 ⊕ x2 的二进制表示的第 l 位才能为 1。
    /// 这样一来，我们就可以把 nums 中的所有元素分成两类，其中一类包含所有二进制表示的第 l 位为 0 的数，另一类包含所有二进制
    /// 表示的第 l 位为 1 的数。可以发现：
    /// - 对于任意一个在数组 nums 中出现两次的元素，该元素的两次出现会被包含在同一类中；
    /// - 对于任意一个在数组 nums 中只出现了一次的元素，即 x1和 x2，它们会被包含在不同类中。
    /// 因此，如果我们将每一类的元素全部异或起来，那么其中一类会得到 x1另一类会得到 x2 
    /// 这样我们就找出了这两个只出现一次的元素
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int[] SingleNumber2(int[] nums)
    {
        int xorsum = 0;
        foreach(int num in nums)
        {
            xorsum ^= num;
        }

        // 防止溢出
        int lsb = (xorsum == int.MinValue ? xorsum : xorsum & (-xorsum));
        int type1 = 0;
        int type2 = 0;
        foreach(int num in nums)
        {
            if((num & lsb) != 0)
            {
                type1 ^= num;
            }
            else
            {
                type2 ^= num;
            }
        }
        return new int[] { type1, type2 };
    }
}
