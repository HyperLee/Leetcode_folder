namespace leetcode_1207;

class Program
{
    /// <summary>
    /// 1207. Unique Number of Occurrences
    /// https://leetcode.com/problems/unique-number-of-occurrences/description/
    /// 1207. 獨一無二的出現次數
    /// https://leetcode.cn/problems/unique-number-of-occurrences/description/
    ///
    /// English:
    /// Given an array of integers arr, return true if the number of occurrences of each value in the array is unique or false otherwise.
    ///
    /// 繁體中文：
    /// 給定一個整數陣列 arr，若陣列中每個數值的出現次數皆為唯一，則回傳 true；否則回傳 false。
    /// </summary>
    /// <param name="args">Command-line arguments (unused).</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// Dictionary 統計數量
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    public bool UniqueOccurrences(int[] arr)
    {
        // 計算每個文字個數(出現次數)
        Dictionary<int, int> dic = new Dictionary<int, int>();
        foreach(var i in arr)
        {
            if(!dic.ContainsKey(i))
            {
                // 第一次出現給初始值0, 下一個步驟在累計次數
                dic.Add(i, 1);
            }
            else
            {
                // 累加已經出現者次數
                dic[i]++;
            }
        }
        // 統計有沒有重覆
        HashSet<int> hashset = new HashSet<int>();
        foreach(KeyValuePair<int, int> pair in dic)
        {
            if(hashset.Contains(pair.Value))
            {
                return false;
            }

            hashset.Add(pair.Value);
        }
        return true;
    }
}
