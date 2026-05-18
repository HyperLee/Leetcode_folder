namespace leetcode_066;

class Program
{
    /// <summary>
    /// 66. Plus One
    /// https://leetcode.com/problems/plus-one/description/
    /// 66. 加一
    /// https://leetcode.cn/problems/plus-one/description/
    /// 
    /// English:
    /// You are given a large integer represented as an integer array digits, where each digits[i] is the ith
    /// digit of the integer. The digits are ordered from most significant to least significant in left-to-right
    /// order. The large integer does not contain any leading 0's.
    ///
    /// Increment the large integer by one and return the resulting array of digits.
    ///
    /// Traditional Chinese:
    /// 給定一個以整數陣列 digits 表示的大整數，其中每個 digits[i] 是該整數的第 i 個數字。這些數字依照
    /// 從最高位到最低位的順序由左至右排列。這個大整數不包含任何前導 0。
    ///
    /// 將這個大整數加一，並回傳結果的數字陣列。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一：找出最长的后缀 9
    /// 当我们对数组 digits 加一时，我们只需要关注 digits 的末尾出现了多少个 9 即可。我们可以考虑如下的三种情况：
    /// - 如果 digits 的末尾没有 9，例如 [1,2,3]，那么我们直接将末尾的数加一，得到 [1,2,4] 并返回；
    /// - 如果 digits 的末尾有若干个 9，例如 [1,2,3,9,9]，那么我们只需要找出从末尾开始的第一个不为 9 的元素，即 3，将该元素加
    ///   一，得到 [1,2,4,9,9]。随后将末尾的 9 全部置零，得到 [1,2,4,0,0] 并返回。
    /// - 如果 digits 的所有元素都是 9，例如 [9,9,9,9,9]，那么答案为 [1,0,0,0,0,0]。我们只需要构造一个长度比 digits 多 1 的新数
    ///   组，将首元素置为 1，其余元素置为 0 即可。
    /// 
    /// 我们只需要对数组 digits 进行一次逆序遍历，找出第一个不为 9 的元素，将其加一并将后续所有元素置零即可。如果 digits 中所有
    /// 的元素均为 9，那么对应着「思路」部分的第三种情况，我们需要返回一个新的数组。
    /// </summary>
    /// <param name="digits"></param>
    /// <returns></returns>
    public int[] PlusOne(int[] digits)
    {
        int n = digits.Length;
        for(int i = n - 1; i >= 0; i--)
        {
            if(digits[i] != 9)
            {
                digits[i]++;

                for(int j = i + 1; j < n; j++)
                {
                    // 為 9 的進位之後後面都要給0
                    digits[j] = 0;
                }

                return digits;
            }
        }

        // digits 中所有的元素均为 9
        int[] res = new int[n + 1];
        res[0] = 1;
        return res;
    }

    /// <summary>
    /// 解法二:
    /// 加一得十进一位个位数为 0 加法运算如不出现进位就运算结束了且进位只会是一。
    /// 只需要判断有没有进位并模拟出它的进位方式，如十位数加 1 个位数置为 0，如此循环直到判断没有再进位就退出循环返回结果。
    /// 然后还有一些特殊情况就是当出现 99、999 之类的数字时，循环到最后也需要进位，出现这种情况时需要手动将它进一位。
    /// 
    /// 簡單說就是 模擬數學計算
    /// 1. 數字非 9 就直接 ++ 然後顯示
    /// 2. 數字為 9 就需要進位(高位進位), 然後原本 index 位置為 0
    /// 2.1 當輸入數字 開頭也為 9 , 此時需要宣告陣列為 digits.Length + 1
    ///     因為要再往上進位, 此時開頭數字要給 1
    /// </summary>
    /// <param name="digits"></param>
    /// <returns></returns>
    public int[] PlusOne2(int[] digits)
    {
            // 正常數學計算 由後面往前; 從低位往高位
            for(int i = digits.Length - 1; i >= 0; i--)
            {
                digits[i]++;
                // 數字 + 1 之後, 計算該數字個位數是否為 0
                digits[i] = digits[i] % 10;
                // 非 0 直接輸出, 若是為 0 就有進位問題
                if (digits[i] != 0)
                {
                    return digits;
                }
            }

            // 需要額外進位.  ex: 99, 999
            digits = new int[digits.Length + 1];
            // 進位開頭給 1
            digits[0] = 1;

            return digits;        
    }
}
