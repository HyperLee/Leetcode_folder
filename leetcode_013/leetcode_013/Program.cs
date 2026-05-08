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
}
