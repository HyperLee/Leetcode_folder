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
        Console.WriteLine("Hello, World!");
    }

    static int[] check = {0, 0, 1, -1, -1, 1, 1, -1, 0, 1};

    /// <summary>
    /// 方法一：枚舉每個數字，檢查是否為 good 數字
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int RotatedDigits(int n)
    {
        int count = 0;
        for(int i = 1; i <= n; i++)
        {
            string num = i.ToString();
            bool valid = true;
            bool different = false;

            foreach(char ch in num)
            {
                if(check[ch - '0'] == -1)
                {
                    valid = false;

                }
                else if(check[ch - '0'] == 1)
                {
                    different = true;
                }
            }
            if(valid && different)
            {
                count++;
            }
        }
        return count;
    }
}
