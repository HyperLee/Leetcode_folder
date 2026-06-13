using System.ComponentModel.DataAnnotations;

namespace leetcode_189;

class Program
{
    /// <summary>
    /// 189. Rotate Array
    /// https://leetcode.com/problems/rotate-array/description/?envType=study-plan-v2&envId=top-interview-150
    /// 189. 轮转数组
    /// https://leetcode.cn/problems/rotate-array/description/
    ///
    /// Problem (English):
    /// Given an integer array nums, rotate the array to the right by k steps, where k is non-negative.
    ///
    /// 題目描述（繁體中文）:
    /// 給定一個整數陣列 nums，將陣列向右輪轉 k 個步驟，其中 k 為非負整數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一：数组翻转
    /// 该方法基于如下的事实：当我们将数组的元素向右移动 k 次后，尾部 k mod n
    /// 个元素会移动至数组头部，其余元素向后移动 k mod n 个位置。
    /// 
    /// 该方法为数组的翻转：我们可以先将所有元素翻转，这样尾部的 k mod n 个元素就被移至数组头部，然后我们再翻转 [0,k mod n − 1] 区间的元素和 [k mod n, n − 1] 区间的元素即能得到最后的答案。
    /// 
    /// 我们以 n=7，k=3 为例进行如下展示：
    /// 操作	结果
    /// 原始数组	1 2 3 4 5 6 7
    /// 翻转所有元素	7 6 5 4 3 2 1
    /// 翻转 [0,k mod n − 1] 区间的元素	5 6 7 4 3 2 1
    /// 翻转 [k mod n,n−1] 区间的元素	5 6 7 1 2 3 4
    /// 
    /// 1.將原始輸入陣列反轉
    /// 2.翻转 [0,k  mod  n − 1] 区间的元素 =>   前k個元素 反轉回去成原始輸入順序
    /// 3. [k  mod  n, n − 1] 区间的元素 => 後n - k 元素 反轉回去成原始輸入順序
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    public void Rotate(int[] nums, int k)
    {
        // 計算移動k之後, 原始陣列中的結尾位置
        int distance = k % nums.Length;

        // 輸入陣列資料全部反轉
        reverse(nums, 0, nums.Length - 1);

        // 前k個元素反轉為 輸入順序
        reverse(nums, 0, distance - 1);

        // n - k 之後(k之後的)的元素反轉回去輸入順序
        reverse(nums, distance, nums.Length - 1);
    }

    /// <summary>
    /// 資料反轉
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    private void reverse(int[] nums, int start, int end)
    {
        while(start < end)
        {
            int temp = nums[start];
            nums[start] = nums[end];
            nums[end] = temp;

            start++;
            end--;
        }
    }

    /// <summary>
    /// 解法二: 使用額外數組
    /// 我们可以使用额外的数组来将每个元素放至正确的位置。用 n 表示数组的长度，我们遍历原数组，将原数组下标为 i 的元素放至新
    /// 数组下标为 (i+k) mod n 的位置，最后将新数组拷贝至原数组即可。
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    public void Rotate2(int[] nums, int k)
    {
        int n = nums.Length;
        int[] newArr = new int[n];
        for(int i = 0; i < n; i++)
        {
            newArr[(i + k) % n] = nums[i];
        }

        // 從 newArr 的索引 0 開始
        // 複製到 nums 的索引 0 開始
        // 總共複製 n 個元素
        Array.Copy(newArr, 0, nums, 0, n);
    }
}
