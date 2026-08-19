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

    /// <summary>
    /// 方法一：位运算
    /// </summary>
    /// <param name="n"></param>
    /// <param name="reservedSeats"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="reservedSeats"></param>
    /// <returns></returns>
    public int MaxNumberOfFamilies(int n, int[][] reservedSeats)
    {
        int left = 0b11110000;
        int middle = 0b11000011;
        int right = 0b00001111;

        Dictionary<int, int> occupied = new Dictionary<int, int>();
        foreach(int[] seat in reservedSeats)
        {
            if(seat[1] >= 2 && seat[1] <= 9)
            {
                int row = seat[0];
                if(!occupied.ContainsKey(row))
                {
                    occupied[row] = 0;
                }
                occupied[row] |= (1 << (seat[1] - 2));
            }
        }

        int ans = (n - occupied.Count) * 2;
        foreach(var kvp in occupied)
        {
            int bitmask = kvp.Value;
            if ((bitmask | left) == left || 
                (bitmask | middle) == middle || 
                (bitmask | right) == right) {
                ans++;
            }      
        }

        return ans;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="reservedSeats"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="reservedSeats"></param>
    /// <returns></returns>
    public int MaxNumberOfFamilies2(int n, int[][] reservedSeats)
    {
        // key = 第幾排
        // value = 第 2~9 號座位的預約狀態，用二進位表示
        Dictionary<int, int> seats = new Dictionary<int, int>();

        foreach (int[] r in reservedSeats)
        {
            int row = r[0];
            int seat = r[1];

            // 只需要考慮 2~9 號座位
            // 1 和 10 不會影響四人家庭的座位安排
            if (2 <= seat && seat <= 9)
            {
                int mask = 1 << (seat - 2);

                if (seats.TryGetValue(row, out int value))
                {
                    // 把對應座位的 bit 設成 1
                    seats[row] = value | mask;
                }
                else
                {
                    seats[row] = mask;
                }
            }
        }

        // 如果某一排只有 1 或 10 被預約，
        // 那麼這排不會存在 Dictionary 中，相當於整排 2~9 都是空的。
        int emptyRows = n - seats.Count;

        // 完全空的排可以安排 2 組四人家庭
        int ans = emptyRows * 2;

        foreach (int x in seats.Values)
        {
            // 可安排四人家庭的三種區域：
            //
            // 2 3 4 5     -> 00001111
            // 4 5 6 7     -> 00111100
            // 6 7 8 9     -> 11110000
            //
            // 只要其中一個區域完全沒有被預約，
            // 這一排就可以再安排 1 組四人家庭。
            if ((x & 0b00001111) == 0 ||
                (x & 0b00111100) == 0 ||
                (x & 0b11110000) == 0)
            {
                ans++;
            }
        }

        return ans;        
    }
}
