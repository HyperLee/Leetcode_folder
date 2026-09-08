namespace leetcode_3870;

class Program
{
    /// <summary>
    /// Problem: Total Number of Commas
    ///
    /// English:
    /// You are given an integer n.
    ///
    /// Return the total number of commas used when writing all integers from [1, n] (inclusive) in standard number formatting.
    ///
    /// In standard formatting:
    ///
    /// - A comma is inserted after every three digits from the right.
    /// - Numbers with fewer than 4 digits contain no commas.
    ///
    /// 繁體中文：
    /// 給定一個整數 n。
    ///
    /// 請回傳以標準數字格式書寫從 [1, n]（包含頭尾）的所有整數時，所使用的逗號總數。
    ///
    /// 在標準格式中：
    ///
    /// - 從右側開始，每三位數字插入一個逗號。
    /// - 少於 4 位數的數字不包含逗號。
    /// </summary>
    /// <remarks>
    /// This entry point does not require command-line input and keeps the starter output
    /// so the project can be launched directly from the debugger.
    /// 此程式進入點不需要命令列輸入，並保留範例起始輸出，方便直接由偵錯器執行。
    /// </remarks>
    /// <param name="args">Command-line arguments; not used by this documentation-only example. 命令列參數；此摘要範例不使用。</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一:遍歷計數
    /// 思路与算法
    /// 这个方法通过直接遍历从 1 到 n 的所有整数，来统计符合条件的数字个数。根据题意，在 [1,n] 的范围内（n≤105）
    /// ，只有大于 999 的数才会在标准格式下包含一个逗号（例如 1,000）。因此，每次遇到循环变量大于 999 时，
    /// 就将结果增加 1，最后返回总数即可。
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int CountCommas(int n)
    {
        int count = 0;

        for(int i = 0; i < n; i++)
        {
            if(i > 999)
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// 方法二：直接计算
    /// 思路与算法
    /// 这个方法通过数学计算在 O(1) 的时间内得出答案。
    /// 根据题目限制，在 [1,n] 的范围内（n≤105），
    /// 只有大于等于 1000 的数才会包含且仅包含一个逗号（例如 1,000 到 100,000）。
    /// 因此，如果 n≥1000，包含逗号的数字个数就是 n−999；
    /// 如果 n<1000，则个数为 0。我们可以直接取 n−999 和 0 之间的最大值作为最终结果，避免了不必要的循环过程。
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int CountCommas2(int n)
    {
        return Math.Max(n - 999, 0);
    }
}