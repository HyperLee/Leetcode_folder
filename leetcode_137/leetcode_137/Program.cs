namespace leetcode_137;

class Program
{
    /// <summary>
    /// 137. Single Number II
    /// https://leetcode.com/problems/single-number-ii/description/
    ///
    /// English:
    /// Given an integer array nums where every element appears three times except for one,
    /// which appears exactly once. Find the single element and return it.
    ///
    /// You must implement a solution with a linear runtime complexity and use only constant extra space.
    ///
    /// 繁體中文：
    /// 給定一個整數陣列 nums，其中每個元素都出現三次，只有一個元素恰好只出現一次。
    /// 找出這個只出現一次的元素並回傳它。
    ///
    /// 你必須實作一個線性時間複雜度且只使用常數額外空間的解法。
    ///
    /// 137. 只出現一次的數字 II
    /// https://leetcode.cn/problems/single-number-ii/description/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        SampleCase[] samples =
        [
            new SampleCase([2, 2, 3, 2], 3),
            new SampleCase([0, 1, 0, 1, 0, 1, 99], 99),
            new SampleCase([-2, -2, 1, 1, 4, 1, -2], 4),
            new SampleCase([int.MinValue, 7, 7, 7], int.MinValue),
            new SampleCase([42], 42),
        ];

        bool allPassed = true;
        allPassed &= RunSamples("Dictionary", samples, solution.SingleNumber);
        allPassed &= RunSamples("Bit state", samples, solution.SingleNumber2);
        allPassed &= RunSamples("Bit count", samples, solution.SingleNumber3);

        Environment.ExitCode = allPassed ? 0 : 1;
    }

    /// <summary>
    /// Represents one executable sample case for the LeetCode 137 console runner.
    /// </summary>
    /// <param name="Nums">Input array that follows the problem rule: one value appears once and every other value appears three times.</param>
    /// <param name="Expected">The value that should be returned by a valid Single Number II solution.</param>
    private readonly record struct SampleCase(int[] Nums, int Expected);

    /// <summary>
    /// Runs all sample cases against one solution function and prints whether each result matches the expected answer.
    /// </summary>
    /// <param name="name">Display name of the solution being exercised.</param>
    /// <param name="samples">Sample inputs and expected outputs that satisfy the LeetCode 137 constraints.</param>
    /// <param name="solver">Function that receives a nums array and returns the single element.</param>
    /// <returns><c>true</c> when every sample passes; otherwise, <c>false</c>.</returns>
    private static bool RunSamples(string name, SampleCase[] samples, Func<int[], int> solver)
    {
        bool allPassed = true;
        Console.WriteLine($"[{name}]");

        foreach (SampleCase sample in samples)
        {
            int actual = solver(sample.Nums);
            bool passed = actual == sample.Expected;
            allPassed &= passed;

            string status = passed ? "PASS" : "FAIL";
            Console.WriteLine($"{status} nums=[{string.Join(", ", sample.Nums)}], expected={sample.Expected}, actual={actual}");
        }

        Console.WriteLine();
        return allPassed;
    }

    /// <summary>
    /// Uses a hash table to count every number and return the value whose count is one.
    /// 解題概念：先統計每個整數出現次數，再找出唯一出現一次的元素。
    /// 輸入條件：<paramref name="nums"/> 符合 LeetCode 137 規則，只有一個元素出現一次，其餘元素皆出現三次。
    /// 輸出結果：回傳只出現一次的整數；此解法時間複雜度為 O(n)，額外空間為 O(k)。
    /// </summary>
    /// <param name="nums">Input array that contains one single element and groups of elements that appear three times.</param>
    /// <returns>The element that appears exactly once.</returns>
    public int SingleNumber(int[] nums)
    {
        Dictionary<int, int> frequencyByNumber = new Dictionary<int, int>();

        // Baseline approach: record each value's frequency, then look for the only count that is not three.
        for (int i = 0; i < nums.Length; i++)
        {
            if (frequencyByNumber.ContainsKey(nums[i]))
            {
                frequencyByNumber[nums[i]]++;
            }
            else
            {
                frequencyByNumber.Add(nums[i], 1);
            }
        }

        foreach (KeyValuePair<int, int> frequency in frequencyByNumber)
        {
            if (frequency.Value == 1)
            {
                return frequency.Key;
            }
        }

        return 0;
    }

    /// <summary>
    /// Uses two bit masks as a finite-state counter to keep each bit's occurrence count modulo three.
    /// 解題概念：用 <c>ones</c> 表示某 bit 出現一次的狀態，用 <c>twos</c> 表示出現兩次的狀態；
    /// 當同一 bit 第三次出現時會從兩個狀態中清除，最後 <c>ones</c> 即為答案。
    /// 輸入條件：<paramref name="nums"/> 符合 LeetCode 137 規則，只有一個元素出現一次，其餘元素皆出現三次。
    /// 輸出結果：回傳只出現一次的整數；此解法時間複雜度為 O(n)，額外空間為 O(1)。
    /// </summary>
    /// <param name="nums">Input array that contains one single element and groups of elements that appear three times.</param>
    /// <returns>The element that appears exactly once.</returns>
    public int SingleNumber2(int[] nums)
    {
        int ones = 0;
        int twos = 0;

        foreach (int num in nums)
        {
            // Each bit cycles through 00 -> 01 -> 10 -> 00 as its count increases modulo three.
            ones = (ones ^ num) & ~twos;
            twos = (twos ^ num) & ~ones;
        }

        return ones;
    }

    /// <summary>
    /// Counts every bit position independently and reconstructs the answer from counts modulo three.
    /// 解題概念：對 32 個 bit 分別統計 1 的出現次數，出現三次的數字在每個 bit 上都會被 3 整除；
    /// 剩下餘數為 1 的 bit 就屬於唯一出現一次的整數。
    /// 輸入條件：<paramref name="nums"/> 符合 LeetCode 137 規則，只有一個元素出現一次，其餘元素皆出現三次。
    /// 輸出結果：回傳只出現一次的整數；此解法時間複雜度為 O(32n)，額外空間為 O(1)。
    /// </summary>
    /// <param name="nums">Input array that contains one single element and groups of elements that appear three times.</param>
    /// <returns>The element that appears exactly once.</returns>
    public int SingleNumber3(int[] nums)
    {
        const int IntBitCount = 32;
        int answer = 0;

        for (int bit = 0; bit < IntBitCount; bit++)
        {
            int bitCount = 0;

            foreach (int num in nums)
            {
                bitCount += (num >> bit) & 1;
            }

            if ((bitCount % 3) != 0)
            {
                answer |= 1 << bit;
            }
        }

        return answer;
    }
}
