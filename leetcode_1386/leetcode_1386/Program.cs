namespace leetcode_1386;

class Program
{
    /// <summary>
    /// 1386. Cinema Seat Allocation
    /// https://leetcode.com/problems/cinema-seat-allocation/description
    /// 1386. 安排电影院座位
    /// https://leetcode.cn/problems/cinema-seat-allocation/description/?envType=daily-question&amp;envId=2026-08-19
    ///
    /// English:
    /// A cinema has n rows of seats, numbered from 1 to n. Each row has 10 seats, numbered from 1 to 10.
    ///
    /// You are given a 2D integer array reservedSeats, where reservedSeats[i] = [rowi, seati] means that seat seati in row rowi is already reserved.
    ///
    /// A four-person group must be assigned to four seats in the same row. The group can be seated in one of the following seat blocks:
    /// seats 2, 3, 4, 5
    /// seats 4, 5, 6, 7
    /// seats 6, 7, 8, 9
    ///
    /// A block can be used only if none of its seats are reserved. Each seat can be assigned to at most one group.
    ///
    /// Return an integer denoting the maximum number of four-person groups that can be assigned.
    ///
    /// 繁體中文：
    /// 電影院有 n 排座位，編號從 1 到 n。每一排有 10 個座位，編號從 1 到 10。
    ///
    /// 給定一個二維整數陣列 reservedSeats，其中 reservedSeats[i] = [rowi, seati] 表示第 rowi 排的第 seati 個座位已被預訂。
    ///
    /// 一個四人團體必須被安排在同一排的四個座位上。團體可以坐在下列其中一組座位：
    /// 座位 2、3、4、5
    /// 座位 4、5、6、7
    /// 座位 6、7、8、9
    ///
    /// 只有當該座位區塊中的所有座位都未被預訂時，才能使用該區塊。每個座位最多只能分配給一個團體。
    ///
    /// 請回傳一個整數，表示最多可以安排的四人團體數量。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
