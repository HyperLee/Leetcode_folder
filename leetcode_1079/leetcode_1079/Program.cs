namespace leetcode_1079;

class Program
{
    /// <summary>
    /// 1079. Letter Tile Possibilities
    /// https://leetcode.com/problems/letter-tile-possibilities/description/
    /// <br/>
    /// You have n tiles, where each tile has one letter tiles[i] printed on it.
    /// Return the number of possible non-empty sequences of letters you can make
    /// using the letters printed on those tiles.
    /// <br/>
    /// 1079. 活字印刷
    /// https://leetcode.cn/problems/letter-tile-possibilities/description/
    /// <br/>
    /// 你有 n 個磁磚，每個磁磚上都印有一個字母 tiles[i]。
    /// 返回你可以使用這些磁磚上的字母所組成的非空字母序列的數量。
    /// </summary>
    /// <param name="args">命令列引數</param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        (string Tiles, int Expected)[] testCases =
        [
            ("AAB", 8),
            ("AAABBC", 188),
            ("V", 1),
        ];

        Console.WriteLine($"Command line args count: {args.Length}");
        foreach((string tiles, int expected) in testCases)
        {
            int actualSolution1 = solver.NumTilePossibilities(tiles);
            int actualSolution2 = solver.NumTilePossibilities2(tiles);
            Console.WriteLine(
                $"tiles = {tiles}, expected = {expected}, solution1 = {actualSolution1}, solution2 = {actualSolution2}, " +
                $"pass1 = {actualSolution1 == expected}, pass2 = {actualSolution2 == expected}");
        }
    }

    /// <summary>
    /// 方法一：回溯。
    /// 先統計每個字元出現的次數，再在每一層遞迴中依序挑選所有仍可使用的字元。
    /// 每次選到一個字元後，就將其剩餘次數減一、往下一層搜尋，待遞迴結束後再恢復原本次數。
    /// 由於遞迴會把目前路徑本身也視為一種可行狀態，因此最終答案需要扣掉空字串。
    /// </summary>
    /// <param name="tiles">磁磚上的字母集合。</param>
    /// <returns>可組成的所有非空字母序列數量。</returns>
    /// <example>
    /// <code>
    /// new Program().NumTilePossibilities("AAB"); // 8
    /// </code>
    /// </example>
    public int NumTilePossibilities(string tiles)
    {
        ArgumentNullException.ThrowIfNull(tiles);

        IDictionary<char, int> count = new Dictionary<char, int>();
        foreach(char currentTile in tiles)
        {
            // 先壓縮成字元次數表，避免直接排列原字串造成大量重複分支。
            if(count.ContainsKey(currentTile))
            {
                count[currentTile]++;
            }
            else
            {
                count.Add(currentTile, 1);
            }
        }

        ISet<char> uniqueTiles = new HashSet<char>(count.Keys);

        // DFS 會把「尚未選任何字元」的空字串算進去，因此這裡扣掉 1。
        return DFS(tiles.Length, count, uniqueTiles) - 1;
    }

    /// <summary>
    /// 深度優先搜尋目前剩餘字元可以形成的所有序列。
    /// 每一層先把目前已形成的前綴視為一種結果，再嘗試所有剩餘次數大於 0 的字元。
    /// 當遞迴深度用完時，代表目前路徑已完整展開，回傳 1 交由上一層累加。
    /// </summary>
    /// <param name="remainingLength">目前最多還能放入多少個字元。</param>
    /// <param name="count">每個字元剩餘可用的次數。</param>
    /// <param name="uniqueTiles">所有不重複字元，用來展開回溯分支。</param>
    /// <returns>包含目前前綴在內，可延伸出的序列總數。</returns>
    private int DFS(int remainingLength, IDictionary<char, int> count, ISet<char> uniqueTiles)
    {
        if(remainingLength == 0)
        {
            return 1;
        }

        int result = 1;
        foreach(char currentTile in uniqueTiles)
        {
            if(count[currentTile] == 0)
            {
                continue;
            }

            // 選擇目前字元後往下一層搜尋。
            count[currentTile]--;
            result += DFS(remainingLength - 1, count, uniqueTiles);

            // 回復現場，讓後續分支看到正確的剩餘數量。
            count[currentTile]++;
        }

        return result;
    }

    private int letters;
    private int[] counts = [];

    /// <summary>
    /// 方法二：以次數陣列搭配回溯計算所有非空字母序列。
    /// 先統計每個不同字元的出現次數，再把這些次數放入陣列中。
    /// 遞迴時只需要關注每個字元還剩多少次可用，因此不必真的建立字串，
    /// 也不會因為相同字元來自不同位置而重複計算。
    /// </summary>
    /// <param name="tiles">磁磚上的字母集合。</param>
    /// <returns>可組成的所有非空字母序列數量。</returns>
    /// <example>
    /// <code>
    /// new Program().NumTilePossibilities2("AAB"); // 8
    /// </code>
    /// </example>
    public int NumTilePossibilities2(string tiles)
    {
        ArgumentNullException.ThrowIfNull(tiles);

        IDictionary<char, int> count = new Dictionary<char, int>();
        foreach(char currentTile in tiles)
        {
            // 先統計每個字元的可用次數，讓回溯只需管理剩餘數量。
            if(count.ContainsKey(currentTile))
            {
                count[currentTile]++;
            }
            else
            {
                count.Add(currentTile, 1);
            }
        }

        letters = count.Count;
        counts = new int[letters];
        int index = 0;

        foreach(KeyValuePair<char, int> kvp in count)
        {
            // counts 中每個位置代表一種不同字元目前還剩多少次可選。
            counts[index] = kvp.Value;
            index++;
        }

        return Backtrack();
    }

    /// <summary>
    /// 依照每種字元的剩餘次數展開回溯，累加所有可形成的非空序列。
    /// 只要某個位置的次數大於 0，就代表可以再加入一個字元，
    /// 因此先將答案加 1，再遞迴搜尋更長的序列，最後回復次數供其他分支使用。
    /// </summary>
    /// <returns>目前剩餘狀態可以形成的非空字母序列數量。</returns>
    private int Backtrack()
    {
        int sequences = 0;
        for(int i = 0; i < letters; i++)
        {
            if(counts[i] > 0)
            {
                // 選擇一個仍可使用的字元，這一步本身就形成一個新的非空序列。
                counts[i]--;
                sequences++;

                // 繼續在更新後的剩餘次數上往下搜尋更長的序列。
                sequences += Backtrack();

                // 回復現場，讓下一個分支看到原本的次數狀態。
                counts[i]++;
            }
        }

        return sequences;
    }
}
