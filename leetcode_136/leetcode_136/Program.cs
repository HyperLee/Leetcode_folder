namespace leetcode_136;

class Program
{
    /// <summary>
    /// 136. Single Number
    /// https://leetcode.com/problems/single-number/description/
    /// 136. 只出现一次的数字
    /// https://leetcode.cn/problems/single-number/description/
    /// 
    /// English:
    /// Given a non-empty array of integers nums, every element appears twice except for one. Find that single one.
    /// You must implement a solution with a linear runtime complexity and use only constant extra space.
    /// 
    /// 繁體中文：
    /// 給定一個非空的整數陣列 nums，除了某個元素只出現一次以外，其餘每個元素都會出現兩次。請找出那個只出現一次的元素。
    /// 你必須實作一個具備線性時間複雜度，且只使用常數額外空間的解法。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }


    /// <summary>
    /// Dictionary 解法
    /// 本方法是透過 function 去找出 次數包含 1 者(可能會有多個, 但是本題目輸入只會有一次而已)
    /// 然後再輸出第一個 當作答案
    /// return dic.FirstOrDefault(x => x.Value == 1).Key;
    /// 
    /// 題目有說明 出現一次者 只有一個
    /// 所以取 第一個 沒問題
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int SingleNumber(int[] nums)
    {
        // key: num, Value: times
        Dictionary<int, int> dict = new Dictionary<int, int>();
        foreach(int num in nums)
        {
            if(dict.ContainsKey(num))
            {
                dict[num]++;
            }
            else
            {
                dict.Add(num, 1);
            }
        }

        // 找出 dic 中 Value 有為 1 者
        if(dict.ContainsValue(1))
        {
            // 取出第一個 Value 為 1 的 Key
            return dict.FirstOrDefault(x => x.Value == 1).Key;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// 方法2:
    /// 差別在於 找尋次數為 1者 不同而已
    /// 這邊是透過 遍歷 dic 
    /// 去找出  num.Value == 1
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int SingleNumber2(int[] nums)
    {
        // key: num, Value: times
        Dictionary<int, int> dict = new Dictionary<int, int>();

        for(int i = 0; i < nums.Length; i++)
        {
            if(dict.ContainsKey(nums[i]))
            {
                dict[nums[i]]++;
            }
            else
            {
                dict.Add(nums[i], 1);
            }
        }

        // 從 dict 中找出 Value 為 1 者
        foreach(var num in dict)
        {
            if(num.Value == 1)
            {
                return num.Key;
            }
        }
        return 0;
    }

    /// <summary>
    /// 用邏輯運算 xor
    /// 1 ⊕ 0 = 1
    /// 0 ⊕ 0 = 0
    /// 註記:
    /// 1. 任何数和 0 做异或运算，结果仍然是原来的数，即 a⊕0=a。
    /// 2. 任何数和其自身做异或运算，结果是 0，即 a⊕a=0。
    /// 3. 异或运算满足交换律和结合律，即 a⊕b⊕a=b⊕a⊕a=b⊕(a⊕a)=b⊕0=b。
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int SingleNumber3(int[] nums)
    {
        int res = 0;
        foreach(var num in nums)
        {
            res ^= num;
        }
        return res;
    }
}
