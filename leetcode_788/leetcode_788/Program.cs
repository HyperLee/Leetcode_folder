namespace leetcode_788;

class Program
{
    /// <summary>
    /// 788. Rotated Digits
    /// https://leetcode.com/problems/rotated-digits/description/?envType=daily-question&envId=2026-05-02
    /// 788. 旋转数字
    /// https://leetcode.cn/problems/rotated-digits/description/?envType=daily-question&envId=2026-05-02
    /// 
    /// English Original:
    /// An integer x is a good if after rotating each digit individually by 180 degrees,
    /// we get a valid number that is different from x. Each digit must be rotated -
    /// we cannot choose to leave it alone.
    ///
    /// A number is valid if each digit remains a digit after rotation.
    /// For example:
    /// 0, 1, and 8 rotate to themselves,
    /// 2 and 5 rotate to each other (in this case they are rotated in a different direction,
    /// in other words, 2 or 5 gets mirrored),
    /// 6 and 9 rotate to each other, and
    /// the rest of the numbers do not rotate to any other number and become invalid.
    ///
    /// Given an integer n, return the number of good integers in the range [1, n].
    ///
    /// 繁體中文版本：
    /// 如果整數 x 的每個數字各自旋轉 180 度後，能得到一個合法且與 x 不同的數字，
    /// 則 x 是 good。
    /// 每個數字都必須旋轉，不能選擇不旋轉。
    ///
    /// 一個數字若在旋轉後每個位數仍能對應成數字，就稱為合法數字。
    /// 例如：
    /// 0、1、8 旋轉後仍會是自己，
    /// 2 和 5 會互相對應（這裡是以不同方向旋轉，也就是 2 或 5 會被鏡射）,
    /// 6 和 9 會互相對應，
    /// 其餘數字旋轉後都無法對應成其他數字，因此屬於無效。
    ///
    /// 給定整數 n，回傳區間 [1, n] 中 good integers 的數量。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        int[] testCases = { 1, 2, 10, 20, 30, 100 };

        Console.WriteLine("LeetCode 788. Rotated Digits");
        Console.WriteLine();

        foreach (int n in testCases)
        {
            int result = solution.RotatedDigits(n);
            Console.WriteLine($"n = {n}, good numbers count = {result}");
        }
    }

    // 陣列索引代表數字 0 ~ 9，值的意義如下：
    // 0  => 旋轉後仍是自己，例如 0、1、8
    // 1  => 旋轉後會變成其他合法數字，例如 2、5、6、9
    // -1 => 旋轉後不是合法數字，例如 3、4、7
    static readonly int[] check = { 0, 0, 1, -1, -1, 1, 1, -1, 0, 1 };

    /// <summary>
    /// 方法一：枚舉 1 到 n 的每一個整數，逐位判斷它是否為 good number。
    ///
    /// 解題觀念：
    /// 1. 若數字中出現 3、4、7，旋轉後會失效，因此該數字直接淘汰。
    /// 2. 若數字至少出現一次 2、5、6、9，代表旋轉後一定會與原數不同。
    /// 3. 若數字只由 0、1、8 組成，雖然旋轉後仍合法，但結果與原數相同，不算 good number。
    ///
    /// 因此只要同時滿足「整個數字合法」以及「旋轉後至少有一位不同」，
    /// 就可以將它計入答案。
    /// </summary>
    /// <param name="n">要統計的上限，範圍為 [1, n]。</param>
    /// <returns>區間 [1, n] 中 good numbers 的數量。</returns>
    public int RotatedDigits(int n)
    {
        int count = 0;

        for (int i = 1; i <= n; i++)
        {
            string num = i.ToString();
            bool valid = true;
            bool different = false;

            foreach (char ch in num)
            {
                int rotationResult = check[ch - '0'];

                // 只要某一位旋轉後不合法，整個數字就不是 good number。
                if (rotationResult == -1)
                {
                    valid = false;
                    break;
                }

                // 只要有任一位旋轉後改變，最終結果就會與原數不同。
                if (rotationResult == 1)
                {
                    different = true;
                }
            }

            if (valid && different)
            {
                count++;
            }
        }

        return count;
    }
}
