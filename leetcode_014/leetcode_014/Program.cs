namespace leetcode_014;

class Program
{
    /// <summary>
    /// 14. Longest Common Prefix
    /// https://leetcode.com/problems/longest-common-prefix/description/
    /// 14. 最長共同前綴
    /// https://leetcode.cn/problems/longest-common-prefix/description/
    /// 
    /// English:
    /// Write a function to find the longest common prefix string amongst an array of strings.
    /// If there is no common prefix, return an empty string "".
    ///
    /// 繁體中文：
    /// 撰寫一個函式，用來找出字串陣列中最長的共同前綴字串。
    /// 如果沒有共同前綴，請回傳空字串 ""。
    ///
    /// Main 用途：執行內建測試資料，示範兩種最長共同前綴解法。
    /// 輸入條件：本範例不使用命令列參數，測試資料直接定義於程式中。
    /// 輸出結果：在主控台列出每筆測資的預期值與兩種解法的回傳結果。
    /// </summary>
    /// <param name="args">命令列參數；本範例未使用。</param>
    static void Main(string[] args)
    {
        string[][] testCases =
        [
            ["flower", "flow", "flight"],
            ["dog", "racecar", "car"],
            ["interspecies", "interstellar", "interstate"],
            [""],
            ["prefix", "prefix", "prefix"]
        ];

        string[] expectedResults = ["fl", "", "inters", "", "prefix"];

        for(int i = 0; i < testCases.Length; i++)
        {
            string shortestStringResult = LongestCommonPrefixByShortestString(testCases[i]);
            string verticalScanningResult = LongestCommonPrefixByVerticalScanning(testCases[i]);
            string formattedInput = string.Join(", ", testCases[i].Select(s => "\"" + s + "\""));

            Console.WriteLine($"Case {i + 1}: [{formattedInput}]");
            Console.WriteLine($"Expected: \"{expectedResults[i]}\"");
            Console.WriteLine($"Shortest string: \"{shortestStringResult}\"");
            Console.WriteLine($"Vertical scanning: \"{verticalScanningResult}\"");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 解法一：以最短字串為比較上限尋找最長共同前綴。
    /// 解題概念：先找出 <paramref name="strs"/> 中長度最短的字串，避免比較時超出任一字串範圍；
    /// 再逐一檢查最短字串的每個字元是否出現在所有字串的相同位置。
    /// 輸入條件：<paramref name="strs"/> 至少包含一個字串，且每個元素皆為非 null 字串。
    /// 輸出結果：回傳所有字串共同擁有的最長起始片段；若沒有共同前綴則回傳空字串。
    /// </summary>
    /// <param name="strs">要比對共同前綴的字串陣列。</param>
    /// <returns>所有字串的最長共同前綴，若不存在則為空字串。</returns>
    public static string LongestCommonPrefixByShortestString(string[] strs)
    {
        int shortestLength = int.MaxValue;
        string shortestString = string.Empty;
        string result = string.Empty;

        // 共同前綴不可能比最短字串更長，因此先找出比較上限。
        for(int i = 0; i < strs.Length; i++)
        {
            if(strs[i].Length < shortestLength)
            {
                shortestLength = strs[i].Length;
                shortestString = strs[i];
            }
        }

        for(int i = 0; i < shortestLength; i++)
        {
            for(int j = 0; j < strs.Length; j++)
            {
                if(shortestString[i] != strs[j][i])
                {
                    // 任一字串在此位置不同，代表目前累積結果就是最長共同前綴。
                    return result;
                }
            }

            result += shortestString[i];
        }

        return result;
    }

    /// <summary>
    /// 解法二：使用縱向掃描（Vertical Scanning）尋找最長共同前綴。
    /// 解題概念：以第一個字串為基準，由左到右逐欄比較所有字串在同一索引位置的字元；
    /// 若某個字串長度不足或字元不同，即可立即回傳基準字串在目前索引之前的子字串。
    /// 輸入條件：<paramref name="strs"/> 至少包含一個字串，且每個元素皆為非 null 字串。
    /// 輸出結果：回傳所有字串共同擁有的最長起始片段；若第一欄即不一致則回傳空字串。
    /// </summary>
    /// <param name="strs">要比對共同前綴的字串陣列。</param>
    /// <returns>所有字串的最長共同前綴，若不存在則為空字串。</returns>
    public static string LongestCommonPrefixByVerticalScanning(string[] strs)
    {
        string firstString = strs[0];

        // 從左至右
        for(int column = 0; column < firstString.Length; column++)
        {
            char expectedChar = firstString[column];

            // 逐欄確認所有字串是否仍維持相同前綴。 從上至下
            for(int row = 1; row < strs.Length; row++)
            {
                // 有缺失或是字母不同
                if(column == strs[row].Length || strs[row][column] != expectedChar)
                {
                    // 0 ~ column 是公共前綴
                    return firstString.Substring(0, column);
                }
            }
        }

        return firstString;
    }
}
