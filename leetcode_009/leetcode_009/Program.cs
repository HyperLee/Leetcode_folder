namespace leetcode_009;

class Program
{
    /// <summary>
    /// 9. Palindrome Number
    /// https://leetcode.com/problems/palindrome-number/description/
    /// 
    /// Given an integer x, return true if x is a palindrome, and false otherwise.
    /// A palindrome is a number that reads the same forward and backward.
    /// For example, 121 is a palindrome while 123 is not.
    /// Note: Negative numbers are never palindromes (e.g. -121 is not a palindrome).
    /// 
    /// 9. 回文數
    /// https://leetcode.cn/problems/palindrome-number/description/
    /// 
    /// 給你一個整數 x，如果 x 是一個回文整數，返回 true；否則，返回 false。
    /// 回文數是指正序（從左向右）和倒序（從右向左）讀都是一樣的整數。
    /// 例如，121 是回文，而 123 不是。
    /// 注意：負數不是回文（例如 -121 不是回文）。
    /// </summary>
    /// <param name="args">命令列引數</param>
    static void Main(string[] args)
    {
        Program solver = new();
        int[] testCases = [121, -121, 10, 0, 12321, 1234321, 123];

        Console.WriteLine("Palindrome Number 測試開始");

        foreach(int testCase in testCases)
        {
            bool result1 = solver.IsPalindrome(testCase);
            bool result2 = solver.IsPalindrome2(testCase);
            bool result3 = solver.IsPalindrome3(testCase);

            Console.WriteLine($"輸入: {testCase}");
            Console.WriteLine($"  IsPalindrome  => {result1}");
            Console.WriteLine($"  IsPalindrome2 => {result2}");
            Console.WriteLine($"  IsPalindrome3 => {result3}");
        }
    }

    /// <summary>
    /// 解法一：轉成字串後反轉整個內容，再與原字串比對。
    /// 這個方法直觀好理解，適合先快速驗證回文的基本概念。
    /// 核心出發點是：如果正著讀與倒著讀完全一致，那就是回文數。
    /// </summary>
    /// <param name="x">要判斷的整數。</param>
    /// <returns>如果 x 是回文數則回傳 true，否則回傳 false。</returns>
    public bool IsPalindrome(int x)
    {
        // 先把整數轉成字串，方便從尾端往前組出反轉結果。
        string inputString = x.ToString();
        string reversedString = "";

        // 從最後一個字元開始往前走，逐步組出反轉後的字串。
        for(int i = inputString.Length - 1; i >= 0; i--)
        {
            reversedString += inputString[i];
        }

        // 反轉字串與原始字串完全一致時，代表這個數字是回文。
        if(reversedString == inputString)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// 解法二：只反轉後半段數字，再與前半段比對。
    /// 這是效率較好的數學解法，不需要把整數轉成字串，也不用完整反轉全部位數。
    /// 核心出發點是：回文的前半段與後半段鏡像對稱，因此只要處理一半就足夠完成判斷。
    /// </summary>
    /// <param name="x">要判斷的整數。</param>
    /// <returns>如果 x 是回文數則回傳 true，否則回傳 false。</returns>
    public bool IsPalindrome2(int x)
    {
        // 負數一定不是回文。
        // 若尾數是 0，開頭也必須是 0 才可能回文，而整數只有 0 自己符合這個條件。
        if(x < 0 || (x % 10 == 0 && x != 0))
        {
            return false;
        }

        int revertedNumber = 0;
        while(x > revertedNumber)
        {
            // 取出目前最低位，接到 revertedNumber 尾端，形成後半段的反轉數字。
            revertedNumber = revertedNumber * 10 + x % 10;

            // 原始數字去掉最低位，讓未處理的前半段繼續往中間靠攏。
            x /= 10;
        }

        // 位數是偶數時，前半段會直接等於後半段反轉後的結果。
        // 位數是奇數時，中間那一位不用比，因此把 revertedNumber / 10 後再比較即可。
        return x == revertedNumber || x == revertedNumber / 10;
    }

    /// <summary>
    /// 解法三：轉成字串後使用雙指針，從左右兩端往中間比對。
    /// 這個方法保留了字串解法的直觀性，同時避免額外建立完整反轉字串。
    /// 核心出發點是：只要任一組對稱位置的字元不同，就可以立刻判定不是回文。
    /// </summary>
    /// <param name="x">要判斷的整數。</param>
    /// <returns>如果 x 是回文數則回傳 true，否則回傳 false。</returns>
    public bool IsPalindrome3(int x)
    {
        // 負數開頭有負號，但結尾不會有負號，因此不可能是回文。
        if(x < 0)
        {
            return false;
        }

        // 先轉成字串，之後才可以透過索引從兩端往中間檢查。
        string inputString = x.ToString();
        int left = 0;
        int right = inputString.Length - 1;

        while(left < right)
        {
            // 只要左右兩端任一組字元不同，就可以直接判定不是回文。
            if(inputString[left] != inputString[right])
            {
                return false;
            }

            // 左右相同則繼續往中間縮小範圍。
            left++;
            right--;
        }

        return true;
    }
}
