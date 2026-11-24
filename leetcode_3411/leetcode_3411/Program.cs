namespace leetcode_3411;

class Program
{
    /// <summary>
    /// 3411. Maximum Subarray With Equal Products
    /// https://leetcode.com/problems/maximum-subarray-with-equal-products/description/
    /// English
    /// You are given an array of positive integers nums.
    /// An array arr is called product equivalent if
    /// prod(arr) == lcm(arr) * gcd(arr), where:
    /// 
    /// prod(arr) is the product of all elements of arr.
    /// gcd(arr) is the GCD of all elements of arr.
    /// lcm(arr) is the LCM of all elements of arr.
    /// 
    /// Return the length of the longest product equivalent subarray of nums.
    /// 
    /// 3411. 最長乘積等價子陣列
    /// https://leetcode.cn/problems/maximum-subarray-with-equal-products/description/
    /// 繁體中文
    /// 給定一個由正整數組成的陣列 nums。
    /// 若陣列 arr 滿足 prod(arr) == lcm(arr) * gcd(arr)，
    /// 則稱它為乘積等價陣列。各函式的定義如下。
    /// 
    /// prod(arr) 是 arr 所有元素的乘積。
    /// gcd(arr) 是 arr 所有元素的最大公因數。
    /// lcm(arr) 是 arr 所有元素的最小公倍數。
    /// 
    /// 回傳 nums 中最長的乘積等價子陣列的長度。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 枚舉
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MaxLength(int[] nums)
    {
        int res = 0;
        for(int i = 0; i < nums.Length; i++)
        {
            long prodVal = nums[i];
            long gdcVal = nums[i];
            long lcmVal = nums[i];

            for(int j = i + 1; j < nums.Length; j++)
            {
                prodVal = prodVal * nums[j];
                gdcVal = GCD(gdcVal, nums[j]);
                lcmVal = lcmVal * nums[j] / GCD(lcmVal, nums[j]);

                if(prodVal > gdcVal * lcmVal)
                {
                    res = Math.Max(res, j - i + 1);
                }
            }
        }
        return res;
    }

    private long GCD(long a, long b)
    {
        while(b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    /// <summary>
    /// 滑動視窗
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MaxLength2(int[] nums)
    {
        int res = 2;
        int prodVal = 1;
        int n = nums.Length;
        int start = 0;
        int end = 0;

        while(end < n)
        {
            while(GCD2(prodVal, nums[end]) > 1)
            {
                prodVal /= nums[start];
                start++;
            }

            prodVal *= nums[end];
            res = Math.Max(res, end - start + 1);
            end++;
        }
        return res;
    }

    private int GCD2(int nums1, int nums2)
    {
        while(nums2 != 0)
        {
            int temp = nums1;
            nums1 = nums2;
            nums2 = temp % nums2;
        }
        return nums1;
    }
}
