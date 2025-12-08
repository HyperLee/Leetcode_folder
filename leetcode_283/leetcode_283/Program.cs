namespace leetcode_283;

class Program
{
    /// <summary>
    /// 283. Move Zeroes
    /// https://leetcode.com/problems/move-zeroes/description/?envType=study-plan-v2&envId=leetcode-75
    /// 283. 移動零
    /// https://leetcode.cn/problems/move-zeroes/description/
    /// 
    /// 繁體中文題目描述：
    /// 給定一個整數陣列 nums，請將所有的 0 移動到陣列的末尾，同時保持非零元素的相對順序。
    /// 注意：你必須在原地完成操作，不能複製該陣列的副本。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 將陣列中的所有 0 移至末尾，同時保持非零元素的相對順序（在原地完成）。
    /// </summary>
    /// <param name="nums">要在原地重新排列的整數陣列（請參考上方繁體中文題目描述）。</param>
    public void MoveZeroes(int[] nums)
    {
        int pointer = 0;
        for(int i = 0; i < nums.Length; i++)
        {
            if(nums[i] != 0)
            {
                if(pointer != i)
                {
                    nums[pointer] = nums[i];
                    nums[i] = 0;
                }
                pointer++;
            }
        }
    }
}
