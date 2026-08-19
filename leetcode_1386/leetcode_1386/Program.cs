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
    /// <remarks>
    /// Main 會建立固定測試資料，依序執行三種解法，輸出 Expected、Actual 與 PASS/FAIL，
    /// 並在任一驗證失敗時以非零結束碼結束程式。
    /// </remarks>
    /// <param name="args">命令列參數；本專案的固定驗證 harness 不使用此參數。</param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        (string Name, int N, int[][] ReservedSeats, int Expected)[] testCases = new[]
        {
            (
                "官方範例 1",
                3,
                new int[][]
                {
                    new int[] { 1, 2 },
                    new int[] { 1, 3 },
                    new int[] { 1, 8 },
                    new int[] { 2, 6 },
                    new int[] { 3, 1 },
                    new int[] { 3, 10 }
                },
                4
            ),
            (
                "官方範例 2",
                2,
                new int[][]
                {
                    new int[] { 2, 1 },
                    new int[] { 1, 8 },
                    new int[] { 2, 6 }
                },
                2
            ),
            (
                "官方範例 3",
                4,
                new int[][]
                {
                    new int[] { 4, 3 },
                    new int[] { 1, 4 },
                    new int[] { 4, 6 },
                    new int[] { 1, 7 }
                },
                4
            ),
            (
                "只預約第 1、10 號座位",
                1,
                new int[][]
                {
                    new int[] { 1, 1 },
                    new int[] { 1, 10 }
                },
                2
            ),
            (
                "單一座位區塊受阻",
                1,
                new int[][]
                {
                    new int[] { 1, 4 }
                },
                1
            ),
            (
                "所有候選區塊受阻",
                1,
                new int[][]
                {
                    new int[] { 1, 2 },
                    new int[] { 1, 3 },
                    new int[] { 1, 4 },
                    new int[] { 1, 5 },
                    new int[] { 1, 6 },
                    new int[] { 1, 7 },
                    new int[] { 1, 8 },
                    new int[] { 1, 9 }
                },
                0
            ),
            (
                "十億排的稀疏資料",
                1_000_000_000,
                new int[][]
                {
                    new int[] { 1, 2 }
                },
                1_999_999_999
            )
        };

        // 每一組資料都同時交給三種解法，讓結果可以互相比對並直接產生 README transcript。
        int passedChecks = 0;
        foreach ((string Name, int N, int[][] ReservedSeats, int Expected) testCase in testCases)
        {
            passedChecks += RunCase(
                solver,
                testCase.Name,
                testCase.N,
                testCase.ReservedSeats,
                testCase.Expected);
        }

        int totalChecks = testCases.Length * 3;
        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
        Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
    }

    /// <summary>
    /// 使用位元運算壓縮每一排第 2 至第 9 號座位的預約狀態，
    /// 再以候選區塊的補集遮罩判斷是否能安排四人團體。
    /// 輸入必須符合題目限制，回傳所有排最多可安排的團體數量。
    /// </summary>
    /// <param name="n">電影院的排數，範圍為 1 至 10^9。</param>
    /// <param name="reservedSeats">已預約的 [排號, 座位號]，座位號範圍為 1 至 10。</param>
    /// <returns>最多可以安排的四人團體數量。</returns>
    public int MaxNumberOfFamilies(int n, int[][] reservedSeats)
    {
        // 座位 2~9 對應 bit 0~7；這三個遮罩是候選區塊以外的座位。
        int outsideMaskForLeftBlock = 0b11110000;
        int outsideMaskForMiddleBlock = 0b11000011;
        int outsideMaskForRightBlock = 0b00001111;

        Dictionary<int, int> occupied = new Dictionary<int, int>();
        foreach (int[] reservedSeat in reservedSeats)
        {
            int row = reservedSeat[0];
            int seat = reservedSeat[1];

            // 第 1 與第 10 號座位不會出現在任何候選區塊中，因此可以忽略。
            if (seat >= 2 && seat <= 9)
            {
                if (!occupied.ContainsKey(row))
                {
                    occupied[row] = 0;
                }

                occupied[row] |= 1 << (seat - 2);
            }
        }

        // Dictionary 沒有記錄的排，其 2~9 號座位全部可用，可以直接安排 2 組。
        int answer = (n - occupied.Count) * 2;
        foreach (int occupiedMask in occupied.Values)
        {
            // occupiedMask OR 補集遮罩仍等於補集遮罩，代表該候選區塊沒有被占用。
            if ((occupiedMask | outsideMaskForLeftBlock) == outsideMaskForLeftBlock ||
                (occupiedMask | outsideMaskForMiddleBlock) == outsideMaskForMiddleBlock ||
                (occupiedMask | outsideMaskForRightBlock) == outsideMaskForRightBlock)
            {
                answer++;
            }
        }

        return answer;
    }

    /// <summary>
    /// 使用直接對應候選區塊的位元遮罩判斷每一排是否存在可用區塊。
    /// 輸入為排數與預約座位資料，輸出為最多可安排的四人團體數量。
    /// </summary>
    /// <param name="n">電影院的排數，範圍為 1 至 10^9。</param>
    /// <param name="reservedSeats">已預約的 [排號, 座位號]，座位號範圍為 1 至 10。</param>
    /// <returns>最多可以安排的四人團體數量。</returns>
    public int MaxNumberOfFamilies2(int n, int[][] reservedSeats)
    {
        Dictionary<int, int> occupiedByRow = new Dictionary<int, int>();

        foreach (int[] reservedSeat in reservedSeats)
        {
            int row = reservedSeat[0];
            int seat = reservedSeat[1];

            if (2 <= seat && seat <= 9)
            {
                int mask = 1 << (seat - 2);

                if (occupiedByRow.TryGetValue(row, out int occupiedMask))
                {
                    occupiedByRow[row] = occupiedMask | mask;
                }
                else
                {
                    occupiedByRow[row] = mask;
                }
            }
        }

        // 只保留真正影響候選區塊的排，其餘排都能安排 2 組。
        int answer = (n - occupiedByRow.Count) * 2;

        foreach (int occupiedMask in occupiedByRow.Values)
        {
            // 2 3 4 5 -> 00001111；4 5 6 7 -> 00111100；6 7 8 9 -> 11110000。
            // 與候選區塊遮罩 AND 為 0，表示該區塊的四個座位都未預約。
            if ((occupiedMask & 0b00001111) == 0 ||
                (occupiedMask & 0b00111100) == 0 ||
                (occupiedMask & 0b11110000) == 0)
            {
                answer++;
            }
        }

        return answer;
    }

    /// <summary>
    /// 以每排的 HashSet 保存實際預約座位，直接檢查三個候選座位區塊。
    /// 這個版本不使用位元運算，輸入與輸出契約和前兩種解法相同。
    /// </summary>
    /// <param name="n">電影院的排數，範圍為 1 至 10^9。</param>
    /// <param name="reservedSeats">已預約的 [排號, 座位號]，座位號範圍為 1 至 10。</param>
    /// <returns>最多可以安排的四人團體數量。</returns>
    public int MaxNumberOfFamilies3(int n, int[][] reservedSeats)
    {
        Dictionary<int, HashSet<int>> reservedByRow = new Dictionary<int, HashSet<int>>();

        foreach (int[] reservedSeat in reservedSeats)
        {
            int row = reservedSeat[0];
            int seat = reservedSeat[1];

            // 1 和 10 號座位不會阻擋任何候選區塊，不必建立該排的集合。
            if (seat < 2 || seat > 9)
            {
                continue;
            }

            if (!reservedByRow.TryGetValue(row, out HashSet<int>? rowSeats))
            {
                rowSeats = new HashSet<int>();
                reservedByRow[row] = rowSeats;
            }

            rowSeats.Add(seat);
        }

        // 未出現在 Dictionary 的排，其候選座位全部可用，可以安排 2 組。
        int answer = (n - reservedByRow.Count) * 2;

        foreach (HashSet<int> rowSeats in reservedByRow.Values)
        {
            bool canUseLeftBlock = IsBlockAvailable(rowSeats, 2);
            bool canUseMiddleBlock = IsBlockAvailable(rowSeats, 4);
            bool canUseRightBlock = IsBlockAvailable(rowSeats, 6);

            // 左右區塊互不重疊，因此兩者都可用時能安排 2 組；其餘情況最多安排 1 組。
            if (canUseLeftBlock && canUseRightBlock)
            {
                answer += 2;
            }
            else if (canUseLeftBlock || canUseMiddleBlock || canUseRightBlock)
            {
                answer++;
            }
        }

        return answer;
    }

    /// <summary>
    /// 檢查從 startSeat 開始的連續四個座位是否都不在指定排的預約集合中。
    /// </summary>
    /// <param name="reservedSeats">同一排已預約的座位號集合。</param>
    /// <param name="startSeat">候選區塊起點，只會是 2、4 或 6。</param>
    /// <returns>四個座位全部可用時回傳 true，否則回傳 false。</returns>
    private static bool IsBlockAvailable(HashSet<int> reservedSeats, int startSeat)
    {
        for (int seat = startSeat; seat < startSeat + 4; seat++)
        {
            if (reservedSeats.Contains(seat))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 執行單一固定案例的三種解法，輸出預期值、實際值與 PASS/FAIL，並回傳通過的檢查數。
    /// </summary>
    /// <param name="solver">提供三種解法的 Program 實例。</param>
    /// <param name="caseName">測試案例名稱。</param>
    /// <param name="n">電影院的排數。</param>
    /// <param name="reservedSeats">測試案例的預約座位資料。</param>
    /// <param name="expected">三種解法都應回傳的結果。</param>
    /// <returns>三種解法中回傳預期值的檢查數，範圍為 0 至 3。</returns>
    private static int RunCase(
        Program solver,
        string caseName,
        int n,
        int[][] reservedSeats,
        int expected)
    {
        int actual1 = solver.MaxNumberOfFamilies(n, reservedSeats);
        int actual2 = solver.MaxNumberOfFamilies2(n, reservedSeats);
        int actual3 = solver.MaxNumberOfFamilies3(n, reservedSeats);

        int passedChecks = 0;
        if (actual1 == expected)
        {
            passedChecks++;
        }

        if (actual2 == expected)
        {
            passedChecks++;
        }

        if (actual3 == expected)
        {
            passedChecks++;
        }

        string status = passedChecks == 3 ? "PASS" : "FAIL";
        Console.WriteLine(
            $"[{caseName}] Expected: {expected} | Actual: M1={actual1}, M2={actual2}, M3={actual3} | {status}");

        return passedChecks;
    }
}