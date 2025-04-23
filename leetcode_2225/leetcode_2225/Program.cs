namespace leetcode_2225;

class Program
{
    /// <summary>
    /// 2225. Find Players With Zero or One Losses
    /// https://leetcode.com/problems/find-players-with-zero-or-one-losses/description/?envType=daily-question&envId=2024-01-15
    /// 2225. 找出输掉零场或一场比赛的玩家
    /// https://leetcode.cn/problems/find-players-with-zero-or-one-losses/description/
    /// 
    /// 不規則陣列
    /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/arrays#jagged-arrays
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // [][]:不規則陣列, 
        int[][] input = 
        { 
            new int[] { 1, 3 }, 
            new int[] { 2, 3 }, 
            new int[] { 3, 6 },
            new int[] { 5, 6 },
            new int[] { 5, 7 },
            new int[] { 4, 5 },
            new int[] { 4, 8 },
            new int[] { 4, 9 },
            new int[] { 10, 4 },
            new int[] { 10, 9 }
        };

        //Console.WriteLine(FindWinners(input));
        //FindWinners(input);
        
        // 呼叫 Dictionary 解法
        var res2 = FindWinners2(input);
        for (int i = 0; i < res2.Count; i++)
        {
            Console.Write("結果({0}): ", i);
            
            for (int j = 0; j < res2[i].Count; j++)
            {
                Console.Write("{0}{1}", res2[i][j], j == (res2[i].Count - 1) ? "" : " ");
            }
            
            Console.WriteLine();
        }
    }


    /// <summary>
    /// https://leetcode.com/problems/find-players-with-zero-or-one-losses/solutions/4567354/c-solution-for-find-players-with-zero-or-one-losses-problem/
    /// 
    /// 不規則陣列
    /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/arrays#jagged-arrays
    /// 
    /// 題目要回傳 [贏家, 只輸一次] 這種規格資料
    /// Hash 不會儲存重複資料, 一筆資料只會有一次
    /// </summary>
    /// <param name="matches">matches[i] = [winner_i, loser_i] </param>
    /// <returns></returns>

    public static IList<IList<int>> FindWinners(int[][] matches)
    {
        // 贏家, 沒輸過
        HashSet<int> nolosers = new HashSet<int>();
        // 輸家, 只輸一次
        HashSet<int> onelosers = new HashSet<int>();
        // 輸家, 輸很多次
        HashSet<int> manylosers = new HashSet<int>();

        foreach (int[] match in matches) 
        {
            int winner = match[0];
            int loser = match[1];

            // 贏家紀錄, 不能有輸過的紀錄 
            if (!onelosers.Contains(winner) && !manylosers.Contains(winner))
            {
                nolosers.Add(winner);
            }

            // 輸家, 輸過就算非贏家
            if(nolosers.Contains(loser))
            {
                // 輸了就要移出贏家紀錄
                nolosers.Remove(loser);
                // 加入 輸家的紀錄
                onelosers.Add(loser);
            }
            else if(onelosers.Contains(loser))
            {
                // 輸超過一次就要歸類為輸很多次, 且移出輸一次的紀錄
                onelosers.Remove(loser);
                manylosers.Add(loser);
            }
            else if(manylosers.Contains(loser))
            {
                // 輸很多次就繼續歸類為同一類紀錄
                continue;
            }
            else
            {
                onelosers.Add(loser);
            }

        }

        // 輸出規格, 
        int[][] result = new int[2][];
        result[0] = nolosers.OrderBy(x => x).ToArray();
        result[1] = onelosers.OrderBy(x => x).ToArray();

        // 輸出顯示 不規則陣列
        for (int i = 0; i < result.Length; i++)
        {
            Console.Write("Element({0}): ", i);

            for (int j = 0; j < result[i].Length; j++)
            {
                Console.Write("{0}{1}", result[i][j], j == (result[i].Length - 1) ? "" : " ");
            }

            Console.WriteLine();
        }

        return result;
    }

    /// <summary>
    /// 推薦使用這方法, 易讀性高
    /// 使用 Dictionary 解法實作
    /// 
    /// 解題概念：
    /// 1. 建立一個字典來記錄每個玩家輸球和參與比賽的情況
    /// 2. 輸球次數為 0 的玩家代表從未輸過
    /// 3. 輸球次數為 1 的玩家代表只輸一次
    /// 
    /// 我們用 Dictionary<int, int> 來追蹤每個玩家的輸球次數
    /// 贏家如果不在字典中，會被設為 0（代表從未輸過）
    /// 輸家則是計算輸球次數
    /// 最後依據輸球次數 0 和 1 分別建立兩個列表
    /// 
    /// 記憶體複雜度：O(n)，其中 n 是參與比賽的玩家總數
    /// 時間複雜度：O(n log n)，主要受制於排序操作
    /// </summary>
    /// <param name="matches">比賽記錄陣列 matches[i] = [贏家_i, 輸家_i]</param>
    /// <returns>二維陣列 [從未輸過的玩家, 只輸一次的玩家]</returns>
    public static IList<IList<int>> FindWinners2(int[][] matches)
    {
        // 使用 Dictionary 來追蹤每個玩家的輸球次數
        // 對贏家設定為 0（從未輸過）；對輸家累加輸球次數
        // key: 玩家 ID, value: 輸球次數
        Dictionary<int, int> playerStats = new Dictionary<int, int>();
        
        // 步驟 1: 遍歷所有比賽，記錄所有玩家並計算輸球次數
        foreach (int[] match in matches)
        {
            int winner = match[0];
            int loser = match[1];
            
            // 如果贏家還不存在於字典，加入並設定輸球次數為 0
            if (!playerStats.ContainsKey(winner))
            {
                playerStats[winner] = 0;
            }
            
            // 如果輸家不存在於字典，加入並設定輸球次數為 1，否則增加輸球次數
            if (!playerStats.ContainsKey(loser))
            {
                playerStats[loser] = 1;
            }
            else
            {
                playerStats[loser]++;
            }
        }
        
        // 步驟 2: 準備結果列表
        List<int> zeroLoss = new List<int>(); // 從未輸過的玩家
        List<int> oneLoss = new List<int>();  // 只輸一次的玩家
        
        // 步驟 3: 遍歷所有玩家，找出從未輸過和只輸一次的玩家;其餘不紀錄
        foreach (var player in playerStats)
        {
            if (player.Value == 0)
            {
                // 玩家輸球次數為 0，表示從未輸過
                zeroLoss.Add(player.Key);
            }
            else if (player.Value == 1)
            {
                // 玩家輸球次數為 1，表示只輸一次
                oneLoss.Add(player.Key);
            }
            // 輸超過一次的玩家不需要記錄
        }
        
        // 步驟 4: 排序結果
        // 題目要求 遞增排序輸出
        zeroLoss.Sort();
        oneLoss.Sort();
        
        // 步驟 5: 建立結果陣列
        IList<IList<int>> res = new List<IList<int>>
        {
            zeroLoss,
            oneLoss
        };
        
        return res;
    }
}
