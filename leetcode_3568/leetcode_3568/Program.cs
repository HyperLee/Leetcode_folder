namespace leetcode_3568;

class Program
{
    /// <summary>
    /// 3568. Minimum Moves to Clean the Classroom
    /// https://leetcode.com/problems/minimum-moves-to-clean-the-classroom/description/
    /// You are given an m x n grid classroom where a student volunteer is tasked with cleaning up litter scattered around the room. Each cell in the grid is one of the following:
    /// - 'S': Starting position of the student
    /// - 'L': Litter that must be collected (once collected, the cell becomes empty)
    /// - 'R': Reset area that restores the student's energy to full capacity, regardless of their current energy level (can be used multiple times)
    /// - 'X': Obstacle the student cannot pass through
    /// - '.': Empty space
    /// You are also given an integer energy, representing the student's maximum energy capacity. The student starts with this energy from the starting position 'S'.
    /// Each move to an adjacent cell (up, down, left, or right) costs 1 unit of energy. If the energy reaches 0, the student can only continue if they are on a reset area 'R', which resets the energy to its maximum capacity energy.
    /// Return the minimum number of moves required to collect all litter items, or -1 if it's impossible.
    /// 3568. 清理教室的最少移動
    /// https://leetcode.cn/problems/minimum-moves-to-clean-the-classroom/description/
    /// 給定一個 m x n 的網格 classroom，一名學生志工負責清理散落在教室中的垃圾。網格中的每個儲存格都是以下其中一種：
    /// - 'S'：學生的起始位置
    /// - 'L'：必須收集的垃圾（收集後，該儲存格會變成空白）
    /// - 'R'：重置區域，無論學生目前的能量為何，都會將能量恢復為最大值（可以重複使用）
    /// - 'X'：學生無法通過的障礙物
    /// - '.'：空白空間
    /// 另外給定一個整數 energy，表示學生的最大能量容量。學生從起始位置 'S' 出發時擁有此能量。
    /// 每次移動到相鄰儲存格（上、下、左或右）都會消耗 1 單位能量。若能量降至 0，學生只有在重置區域 'R' 上時才能繼續移動；該區域會將能量重置為最大容量 energy。
    /// 請返回收集所有垃圾所需的最少移動次數；如果無法完成，請返回 -1。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
