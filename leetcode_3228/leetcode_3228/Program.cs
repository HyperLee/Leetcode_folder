namespace leetcode_3228;

class Program
{
    /// <summary>
    /// 3228. Maximum Number of Operations to Move Ones to the End
    /// https://leetcode.com/problems/maximum-number-of-operations-to-move-ones-to-the-end/description/?envType=daily-question&envId=2025-11-13
    /// 3228. 将 1 移动到末尾的最大操作次数
    /// https://leetcode.cn/problems/maximum-number-of-operations-to-move-ones-to-the-end/description/?envType=daily-question&envId=2025-11-13
    /// 
    /// 中文題目描述：
    /// 給定一個二元字串 s。
    /// 你可以對字串執行下列操作任意次數：
    /// 選擇索引 i（滿足 i + 1 < s.length），且 s[i] == '1' 且 s[i + 1] == '0'。
    /// 將字元 s[i] 向右移動，直到到達字串末尾或遇到另一個 '1' 為止。
    /// 例如，對 s = "010010" 若選擇 i = 1，得到的字串會是 s = "000110"。
    /// 回傳你最多可以執行的操作次數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
