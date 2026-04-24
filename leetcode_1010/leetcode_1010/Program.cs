namespace leetcode_1010;

class Program
{
    /// <summary>
    /// 1010. Pairs of Songs With Total Durations Divisible by 60
    /// https://leetcode.com/problems/pairs-of-songs-with-total-durations-divisible-by-60/description/
    /// 1010. 總持續時間可被 60 整除的歌曲
    /// https://leetcode.cn/problems/pairs-of-songs-with-total-durations-divisible-by-60/description/
    ///
    /// English:
    /// You are given a list of songs where the i-th song has a duration of time[i] seconds.
    /// Return the number of pairs of songs for which their total duration in seconds is divisible by 60.
    /// Formally, we want the number of indices i, j such that i &lt; j with (time[i] + time[j]) % 60 == 0.
    ///
    /// 繁體中文：
    /// 給定一個歌曲陣列，其中第 i 首歌的長度為 time[i] 秒。
    /// 請回傳所有歌曲配對中，總秒數可被 60 整除的配對數量。
    /// 形式化地說，找出所有 i, j 且 i &lt; j，並且 (time[i] + time[j]) % 60 == 0 的索引對數量。
    /// </summary>
    /// <param name="args">命令列參數。</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
