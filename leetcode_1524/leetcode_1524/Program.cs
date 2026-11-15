namespace leetcode_1524;

class Program
{
    /// <summary>
    /// 1524. Number of Sub-arrays With Odd Sum
    /// https://leetcode.com/problems/number-of-sub-arrays-with-odd-sum/description/
    /// 1524. 總和為奇數的子陣列數目
    /// https://leetcode.cn/problems/number-of-sub-arrays-with-odd-sum/description/
    /// <para>
    /// Given an array of integers <c>arr</c>, return the number of subarrays with an <b>odd</b> sum.
    /// Since the answer can be very large, return it modulo <c>10^9 + 7</c>.
    /// </para>
    /// <para>
    /// 給定一個整數陣列 <c>arr</c>，回傳總和為<b>奇數</b>的子陣列數目。
    /// 由於答案可能非常大，請回傳答案除以 <c>10^9 + 7</c> 的餘數。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 前缀和 + 数学
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    public int NumOfSubarrays(int[] arr)
    {
        const int MODULO = 1_000_000_007;
        int odd = 0;
        int even = 1;
        int subarrays = 0;
        int total = 0;

        foreach(int x in arr)
        {
            total += x;
            if(total % 2 == 0)
            {
                subarrays = (subarrays + odd) % MODULO;
                even++;
            }
            else
            {
                subarrays  = (subarrays + even) % MODULO;
                odd++;
            }
        }
        return subarrays % MODULO;
    }

    /// <summary>
    /// 解法二：先建立前綴和的奇偶性陣列，再統計奇偶性不同的前綴配對。
    /// </summary>
    /// <remarks>
    /// 時間複雜度 O(n)，額外空間複雜度 O(n)。
    /// </remarks>
    public int NumOfSubarrays2(int[] arr)
    {
        const int MODULO = 1_000_000_007;
        // prefixParity[0] 代表空前綴，後續才能計入從索引 0 開始的子陣列。
        int[] prefixParity = new int[arr.Length + 1];
        for (int i = 0; i < arr.Length; i++)
        {
            prefixParity[i + 1] = (prefixParity[i] + arr[i]) % 2;
        }

        // counts[0] 記錄已掃過的偶數前綴，counts[1] 記錄奇數前綴。
        int[] counts = new int[2];
        long subarrays = 0;
        foreach (int parity in prefixParity)
        {
            // XOR 1 將 0 和 1 互換，找出奇偶性相反的前綴個數。
            subarrays += counts[parity ^ 1];
            counts[parity]++;
        }

        return (int)(subarrays % MODULO);
    }
}
