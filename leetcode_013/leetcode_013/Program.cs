namespace leetcode_013;

class Program
{
    /// <summary>
    /// 13. Roman to Integer
    /// English:
    /// Roman numerals are represented by seven different symbols: I, V, X, L, C, D and M.
    /// Symbol       Value
    /// I            1
    /// V            5
    /// X            10
    /// L            50
    /// C            100
    /// D            500
    /// M            1000
    /// 
    /// For example, 2 is written as II in Roman numeral, just two ones added together.
    /// 12 is written as XII, which is simply X + II.
    /// The number 27 is written as XXVII, which is XX + V + II.
    /// 
    /// Roman numerals are usually written largest to smallest from left to right.
    /// However, the numeral for four is not IIII. Instead, the number four is written as IV.
    /// Because the one is before the five we subtract it making four.
    /// The same principle applies to the number nine, which is written as IX.
    /// There are six instances where subtraction is used:
    /// I can be placed before V (5) and X (10) to make 4 and 9.
    /// X can be placed before L (50) and C (100) to make 40 and 90.
    /// C can be placed before D (500) and M (1000) to make 400 and 900.
    /// 
    /// Given a roman numeral, convert it to an integer.
    /// 
    /// Traditional Chinese:
    /// 羅馬數字由七種不同的符號表示：I、V、X、L、C、D 和 M。
    /// 符號       數值
    /// I          1
    /// V          5
    /// X          10
    /// L          50
    /// C          100
    /// D          500
    /// M          1000
    /// 
    /// 例如，2 寫作 II，也就是兩個 1 相加。
    /// 12 寫作 XII，也就是 X + II。
    /// 27 寫作 XXVII，也就是 XX + V + II。
    /// 
    /// 羅馬數字通常依照由大到小的順序，從左到右書寫。
    /// 不過，4 並不是寫成 IIII，而是寫成 IV。
    /// 因為 1 放在 5 的前面，表示要用 5 減去 1，結果為 4。
    /// 相同原理也適用於 9，因此 9 寫成 IX。
    /// 共有六種使用減法表示的情況：
    /// I 可以放在 V (5) 和 X (10) 前面，分別表示 4 和 9。
    /// X 可以放在 L (50) 和 C (100) 前面，分別表示 40 和 90。
    /// C 可以放在 D (500) 和 M (1000) 前面，分別表示 400 和 900。
    /// 
    /// 給定一個羅馬數字字串，請將它轉換成整數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一: 使用字典儲存羅馬數字與對應的整數，遍歷羅馬數字字串，根據前後字元的大小關係決定是加還是減。
    /// 
    /// 首先，分別將單個羅馬數和其所對應的整數存入字典中。
    /// 其次，對於輸入的羅馬數，將其看作字串。設定目前數為0，開始遍歷，根據規律，
    /// 從第一個字元到倒數第二個字元，每個字元在字典中的值與後一個字元比較
    /// ，若前者小於後者，說明是類似於IV一樣的，需要用目前的數減去這個值。
    /// 否則，用目前的數加上這個值。
    /// 若迴圈到最後一個字元，則其在字典中的值直接相加，直到迴圈結束。
    /// 最後，返回結果。
    /// 時間複雜度：O(n)
    /// 
    /// 要小心羅馬數字有前後問題
    /// 如下:
    /// IV: 4  => -=
    ///  V: 5
    /// VI: 6  => +=
    /// 並非像是阿拉伯數字一致性讀取即可, 會出錯
    /// 
    /// 羅馬數字前後判讀方式:
    /// 前面 < 後面 => 減法
    /// 前面 > 後面 => 加法
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int RomanToInt(string s)
    {
        int res = 0;
        // 儲存 羅馬字 總共七種符號 與其對應的數值
        Dictionary<char, int> dic = new Dictionary<char, int>{{'I', 1}, {'V', 5}, {'X', 10}, {'L', 50}, {'C', 100}, {'D', 500}, {'M', 1000}};

        for(int i = 0; i < s.Length; i++)
        {
            int value = dic[s[i]];

            // 每個字元在字典中的值與後一個字元比較
            if(i == s.Length - 1 || dic[s[i + 1]] <= dic[s[i]])
            {
                res += value;
            }
            else
            {
                // 後面比前面大，說明是類似於IV一樣的，需要用目前的數減去這個值
                res -= value;
            }
        }
        return res;
    }
}
