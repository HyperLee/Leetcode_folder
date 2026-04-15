namespace leetcode_2515;

class Program
{
    /// <summary>
    /// 2515. Shortest Distance to Target String in a Circular Array
    ///
    /// English:
    /// You are given a 0-indexed circular string array words and a string target. A circular array means that the array's end connects to the array's beginning.
    /// Formally, the next element of words[i] is words[(i + 1) % n] and the previous element of words[i] is words[(i - 1 + n) % n], where n is the length of words.
    /// Starting from startIndex, you can move to either the next word or the previous word with 1 step at a time.
    /// Return the shortest distance needed to reach the string target. If the string target does not exist in words, return -1.
    ///
    /// 繁體中文：
    /// 給定一個 0-indexed 的環狀字串陣列 words 與一個字串 target。環狀陣列表示陣列的結尾會連接回陣列的開頭。
    /// 更正式地說，words[i] 的下一個元素是 words[(i + 1) % n]，前一個元素是 words[(i - 1 + n) % n]，其中 n 為 words 的長度。
    /// 從 startIndex 開始，你每一步都可以移動到下一個單字或前一個單字。
    /// 回傳到達字串 target 所需的最短距離。如果 words 中不存在 target，則回傳 -1。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
