namespace leetcode_1861;

class Program
{
    /// <summary>
    /// 1861. Rotating the Box
    /// https://leetcode.com/problems/rotating-the-box/description/?envType=daily-question&envId=2026-05-06
    /// 1861. 旋轉盒子
    /// https://leetcode.cn/problems/rotating-the-box/description/?envType=daily-question&envId=2026-05-06
    ///
    /// You are given an m x n matrix of characters boxGrid representing a side-view of a box.
    /// Each cell of the box is one of the following:
    ///   A stone '#'
    ///   A stationary obstacle '*'
    ///   Empty '.'
    ///
    /// The box is rotated 90 degrees clockwise, causing some of the stones to fall due to gravity.
    /// Each stone falls down until it lands on an obstacle, another stone, or the bottom of the box.
    /// Gravity does not affect the obstacles' positions, and the inertia from the box's rotation
    /// does not affect the stones' horizontal positions.
    ///
    /// It is guaranteed that each stone in boxGrid rests on an obstacle, another stone, or the bottom of the box.
    ///
    /// Return an n x m matrix representing the box after the rotation described above.
    ///
    /// 給定一個 m x n 的字元矩陣 boxGrid，代表一個盒子的側視圖。
    /// 盒子中的每個格子是以下之一：
    ///   石頭 '#'
    ///   固定障礙物 '*'
    ///   空格 '.'
    ///
    /// 盒子順時針旋轉 90 度，由於重力作用，部分石頭會向下掉落。
    /// 每顆石頭會持續往下掉，直到碰到障礙物、另一顆石頭，或到達盒子底部為止。
    /// 重力不影響障礙物的位置，且旋轉的慣性不影響石頭的水平位置。
    ///
    /// 保證 boxGrid 中的每顆石頭都靠在障礙物、另一顆石頭或盒子底部上。
    ///
    /// 回傳旋轉後盒子的 n x m 矩陣。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
