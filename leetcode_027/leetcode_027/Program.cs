namespace leetcode_027;

class Program
{
    /// <summary>
    /// 27. Remove Element
    /// https://leetcode.com/problems/remove-element/description/
    /// 27. 移除元素
    /// https://leetcode.cn/problems/remove-element/description/
    ///
    /// English:
    /// Given an integer array nums and an integer val, remove all occurrences of val in nums in-place.
    /// The order of the elements may be changed. Then return the number of elements in nums which are
    /// not equal to val.
    ///
    /// Consider the number of elements in nums which are not equal to val be k, to get accepted,
    /// you need to do the following things:
    /// 1. Change the array nums such that the first k elements of nums contain the elements which are
    /// not equal to val. The remaining elements of nums are not important as well as the size of nums.
    /// 2. Return k.
    ///
    /// 繁體中文:
    /// 給定一個整數陣列 nums 與一個整數 val，請你原地移除 nums 中所有等於 val 的元素。
    /// 元素的順序可以改變。接著回傳 nums 中不等於 val 的元素數量。
    ///
    /// 設 nums 中不等於 val 的元素數量為 k，若要通過題目，你需要完成以下事項：
    /// 1. 調整陣列 nums，使前 k 個元素皆為不等於 val 的元素。其餘元素與陣列長度不重要。
    /// 2. 回傳 k。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 输入：nums = [3,2,2,3], val = 3
    /// 输出：2, nums = [2,2]
    /// 解释：函数应该返回新的长度 2, 并且 nums 中的前两个元素均为 2。
    /// 你不需要考虑数组中超出新长度后面的元素。
    /// 例如，函数返回的新长度为 2 
    /// ，而 nums = [2,2,3,3] 或 nums = [2,2,0,0]，也会被视作正确答案。
    ///
    /// 右指針指向下一個要比對的位置
    /// 左指針指向下一個要替換的位置
    /// 
    /// 原地替換
    /// 把 != val 的 element 往前移動
    /// = val 往後
    /// 最終 回傳 前 k 個 數量
    /// 也是新的 nums[k] 的 index 位置
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    public int RemoveElement(int[] nums, int val)
    {
        int n = nums.Length;
        if(n == 0)
        {
            return 0;
        }

        int left = 0;
        for(int right = 0; right < n; right++)
        {
            // 不等於 val 的 element 往前放
            if(nums[right] != val)
            {
                nums[left] = nums[right];
                left++;
            }
        }
        return left;
    }
}
