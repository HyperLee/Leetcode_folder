namespace leetcode_167;

class Program
{
    /// <summary>
    /// 167. Two Sum II - Input Array Is Sorted
    /// https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/description/
    /// 167. 两数之和 II - 输入有序数组
    /// https://leetcode.cn/problems/two-sum-ii-input-array-is-sorted/description/
    ///
    /// <para>English</para>
    /// <para>Given a 1-indexed array of integers numbers that is already sorted in non-decreasing order, find two numbers such that they add up to a specific target number. Let these two numbers be numbers[index1] and numbers[index2] where 1 &lt;= index1 &lt; index2 &lt;= numbers.length.</para>
    /// <para>Return the indices of the two numbers index1 and index2, each incremented by one, as an integer array [index1, index2] of length 2.</para>
    /// <para>The tests are generated such that there is exactly one solution. You may not use the same element twice.</para>
    /// <para>Your solution must use only constant extra space.</para>
    ///
    /// <para>繁體中文</para>
    /// <para>給定一個 1-indexed 的整數陣列 numbers，該陣列已依非遞減順序排序。請找出兩個數字，使它們的總和等於指定的 target。設這兩個數字分別為 numbers[index1] 與 numbers[index2]，其中 1 &lt;= index1 &lt; index2 &lt;= numbers.length。</para>
    /// <para>請回傳這兩個數字的索引 index1 和 index2，並以長度為 2 的整數陣列 [index1, index2] 表示。</para>
    /// <para>測試資料保證恰好只有一組解。你不能重複使用同一個元素。</para>
    /// <para>你的解法必須只使用常數額外空間。</para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一: Dictionary
    /// 可能效率沒那麼好
    /// </summary>
    /// <param name="numbers"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public int[] TwoSum(int[] numbers, int target)
    {
        // key: 輸入數值, Value: index
        Dictionary<int, int> dic = new Dictionary<int, int>();
        for(int i = 0; i < numbers.Length; i++)
        {
            int left = target - numbers[i];
            if(dic.ContainsKey(left))
            {
                return new int[]{dic[left], i + 1};
            }

            if(!dic.ContainsKey(numbers[i]))
            {
                dic.Add(numbers[i], i + 1);
            }
        }
        return null;
    }

    /// <summary>
    /// 解法二: 二分法
    /// 在数组中找到两个数，使得它们的和等于目标值，可以首先固定第一个数，
    /// 然后寻找第二个数，第二个数等于目标值减去第一个数的差。
    /// 利用数组的有序性质，可以通过二分查找的方法寻找第二个数。为了避免重复寻找，在寻找第二个数时，只在第一个数的右侧寻
    /// 找
    /// </summary>
    /// <param name="numbers"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public int[] TwoSum2(int[] numbers, int target)
    {
        for(int i = 0; i < numbers.Length; i++)
        {
            int low = i + 1;
            int high = numbers.Length - 1;
            while(low <= high)
            {
                int mid = low + (high - low) / 2;
                if(numbers[mid] == target - numbers[i])
                {
                    return new int[]{i + 1, mid + 1};
                }
                else if(numbers[mid] > target - numbers[i])
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }
        }
        return new int[]{ -1, -1};
    }

    /// <summary>
    /// 方法三: 雙指針
    /// 初始时两个指针分别指向第一个元素位置和最后一个元素的位置。每次计算两个指针指向的两个元素之和，并和目标值比较。如果两
    /// 个元素之和等于目标值，则发现了唯一解。如果两个元素之和小于目标值，则将左侧指针右移一位。如果两个元素之和大于目标值，
    /// 则将右侧指针左移一位。移动指针之后，重复上述操作，直到找到答案。
    /// 
    /// 使用双指针的实质是缩小查找范围。那么会不会把可能的解过滤掉？答案是不会。假设 numbers[i]+numbers[j]=target 是唯一
    /// 解，其中 0≤i<j≤numbers.length−1。初始时两个指针分别指向下标 0 和下标 numbers.length−1，左指针指向的下标小于或
    /// 等于 i，右指针指向的下标大于或等于 j。除非初始时左指针和右指针已经位于下标 i 和 j，否则一定是左指针先到达下标 i 的位
    /// 置或者右指针先到达下标 j 的位置。
    /// 
    /// 如果左指针先到达下标 i 的位置，此时右指针还在下标 j 的右侧，sum>target，因此一定是右指针左移，左指针不可能移到 i 的
    /// 右侧。
    /// 
    /// 如果右指针先到达下标 j 的位置，此时左指针还在下标 i 的左侧，sum<target，因此一定是左指针右移，右指针不可能移到 j 的
    /// 左侧。
    /// 
    /// 由此可见，在整个移动过程中，左指针不可能移到 i 的右侧，右指针不可能移到 j 的左侧，因此不会把可能的解过滤掉。由于题目
    /// 确保有唯一的答案，因此使用双指针一定可以找到答案。
    /// </summary>
    /// <param name="numbers"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public int[] TwoSum3(int[] numbers, int target)
    {
        int low = 0;
        int high = numbers.Length - 1;
        while(low < high)
        {
            int sum = numbers[low] + numbers[high];
            if(sum == target)
            {
                return new int[]{low + 1, high + 1};
            }
            else if(sum < target)
            {
                low++;
            }
            else
            {
                high--;
            }
        }
        return new int[]{ -1, -1};
    }
}
