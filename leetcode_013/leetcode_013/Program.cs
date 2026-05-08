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
        Program solution = new();

        // 建立題目常見案例，涵蓋一般加法與六種減法規則中的代表情境。
        (string Roman, int Expected)[] testCases =
        [
            ("III", 3),
            ("IV", 4),
            ("IX", 9),
            ("LVIII", 58),
            ("MCMXCIV", 1994)
        ];

        Console.WriteLine("LeetCode 13 - Roman to Integer");
        Console.WriteLine();

        // 逐筆執行兩種解法，方便直接從主控台確認輸出是否符合預期。
        foreach ((string roman, int expected) in testCases)
        {
            int result1 = solution.RomanToInt(roman);
            int result2 = solution.RomanToInt2(roman);

            Console.WriteLine($"Input: {roman}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"RomanToInt : {result1}");
            Console.WriteLine($"RomanToInt2: {result2}");
            Console.WriteLine($"Pass: {result1 == expected && result2 == expected}");
            Console.WriteLine(new string('-', 32));
        }
    }

    /// <summary>
    /// 解法一：在方法內建立羅馬符號對應表，從左到右掃描字串並累加答案。
    /// 
    /// 解題說明：
    /// 羅馬數字通常由大到小排列，因此大多數符號可以直接相加。
    /// 但若目前符號的值小於右邊下一個符號，代表遇到 IV、IX、XL、XC、CD、CM
    /// 這類減法組合，此時目前符號要改成扣除。
    /// 
    /// 判斷規則：
    /// 目前值 < 下一個值：減去目前值。
    /// 目前值 >= 下一個值，或目前符號已是最後一位：加上目前值。
    ///
    /// 時間複雜度：O(n)，其中 n 是字串長度。
    /// 空間複雜度：O(1)，羅馬符號種類固定為 7 種。
    /// </summary>
    /// <param name="s">有效的羅馬數字字串。</param>
    /// <returns>羅馬數字轉換後的整數值。</returns>
    public int RomanToInt(string s)
    {
        int res = 0;

        // 儲存七種羅馬符號與對應的整數值。
        Dictionary<char, int> dic = new Dictionary<char, int>{{'I', 1}, {'V', 5}, {'X', 10}, {'L', 50}, {'C', 100}, {'D', 500}, {'M', 1000}};

        for(int i = 0; i < s.Length; i++)
        {
            // 先取得目前符號的數值，避免後續判斷重複查表。
            int value = dic[s[i]];

            // 最後一個符號一定直接相加；若下一個值不比目前值大，也屬於一般加法。
            if(i == s.Length - 1 || dic[s[i + 1]] <= dic[s[i]])
            {
                res += value;
            }
            else
            {
                // 下一個值比目前值大，表示遇到減法組合，例如 IV = 5 - 1。
                res -= value;
            }
        }

        return res;
    }

    /// <summary>
    /// 解法二使用的共用羅馬符號對應表。
    /// 
    /// 解題說明：
    /// 羅馬符號的映射固定不變，將字典放在欄位中可以讓多次呼叫重複使用，
    /// 不需要每次執行方法時都重新建立同一份資料。
    /// </summary>
    /// <value>七種羅馬符號與整數值的固定映射。</value>
    Dictionary<char, int> symbolValues = new Dictionary<char, int> {
        {'I', 1},
        {'V', 5},
        {'X', 10},
        {'L', 50},
        {'C', 100},
        {'D', 500},
        {'M', 1000},
    };

    /// <summary>
    /// 解法二：使用共用字典欄位查表，從左到右掃描羅馬數字並計算總和。
    /// 
    /// 解題說明：
    /// 每次讀取目前符號後，先比較它與下一個符號的大小。
    /// 若目前值小於下一個值，表示目前符號是減法組合的左側符號，因此扣除；
    /// 否則將目前值加進答案。
    ///
    /// 此解法和解法一的核心邏輯相同，差異在於對應表放在欄位中共用。
    /// 時間複雜度：O(n)，其中 n 是字串長度。
    /// 空間複雜度：O(1)，只使用固定大小的符號對應表。
    /// </summary>
    /// <param name="s">有效的羅馬數字字串。</param>
    /// <returns>羅馬數字轉換後的整數值。</returns>
    public int RomanToInt2(string s)
    {
        int res = 0;
        int n = s.Length;

        for(int i = 0; i < n; i++)
        {
            // 使用共用字典取得目前符號的數值。
            int value = symbolValues[s[i]];

            // 目前值小於下一個值時，代表要套用羅馬數字的減法規則。
            if(i < n - 1 && value < symbolValues[s[i + 1]])
            {
                res -= value;
            }
            else
            {
                // 一般情況直接把目前符號的值加入總和。
                res += value;
            }
        }

        return res;
    }
}
