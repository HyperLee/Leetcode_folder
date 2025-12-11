namespace leetcode_3531;

class Program
{
    /// <summary>
    /// 3531. Count Covered Buildings
    /// https://leetcode.com/problems/count-covered-buildings/description/?envType=daily-question&envId=2025-12-11
    /// 3531. 统计被覆盖的建筑
    /// https://leetcode.cn/problems/count-covered-buildings/description/?envType=daily-question&envId=2025-12-11
    ///
    /// 題目描述（繁體中文）：
    /// 給定一個正整數 n，代表 n x n 的城市。再給定一個二維陣列 buildings，其中 buildings[i] = [x, y]
    /// 表示位於座標 [x, y] 的唯一建築物。
    /// 如果某一棟建築物在四個方向（左、右、上、下）上至少各有一棟建築物，則該建築物被視為「被覆蓋」。
    /// 回傳被覆蓋的建築物數量。
    /// 
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();
        
        // 測試案例 1: n = 3, buildings = [[0,0],[1,1],[2,2]]
        // 預期結果: 1 (只有 [1,1] 被覆蓋)
        int[][] buildings1 = [[0,0], [1,1], [2,2]];
        int result1 = solution.CountCoveredBuildings(3, buildings1);
        Console.WriteLine($"測試案例 1: n=3, buildings=[[0,0],[1,1],[2,2]] => 結果: {result1}");
        
        // 測試案例 2: n = 4, buildings = [[1,1],[1,2],[2,1],[2,2]]
        // 預期結果: 0 (沒有建築被覆蓋)
        int[][] buildings2 = [[1,1], [1,2], [2,1], [2,2]];
        int result2 = solution.CountCoveredBuildings(4, buildings2);
        Console.WriteLine($"測試案例 2: n=4, buildings=[[1,1],[1,2],[2,1],[2,2]] => 結果: {result2}");
        
        // 測試案例 3: n = 5, buildings = [[0,2],[1,1],[1,3],[2,0],[2,2],[2,4],[3,1],[3,3],[4,2]]
        // 預期結果: 3
        int[][] buildings3 = [[0,2], [1,1], [1,3], [2,0], [2,2], [2,4], [3,1], [3,3], [4,2]];
        int result3 = solution.CountCoveredBuildings(5, buildings3);
        Console.WriteLine($"測試案例 3: n=5, buildings=[[0,2],[1,1],[1,3],[2,0],[2,2],[2,4],[3,1],[3,3],[4,2]] => 結果: {result3}");
    }

    /// <summary>
    /// 統計被覆蓋的建築數量
    /// 
    /// 解題思路：
    /// 1. 一個建築被覆蓋，代表它在四個方向（左、右、上、下）都至少有一個其他建築
    /// 2. 換句話說，這個建築不能處於任何一行或一列的邊界位置
    /// 3. 我們統計每一行的最小值和最大值，以及每一列的最小值和最大值
    /// 4. 如果一個建築的 x 座標既不是該行的最小值也不是最大值，
    ///    且 y 座標既不是該列的最小值也不是最大值，則該建築被覆蓋
    /// 
    /// 時間複雜度：O(buildings.length)
    /// 空間複雜度：O(n)
    /// </summary>
    /// <param name="n">城市大小 (n x n)</param>
    /// <param name="buildings">建築物座標陣列，buildings[i] = [x, y]</param>
    /// <returns>被覆蓋的建築物數量</returns>
    public int CountCoveredBuildings(int n, int[][] buildings)
    {
        // 建立陣列來記錄每一行和每一列的最大值和最小值
        int[] maxRow = new int[n + 1];  // 每一行的最大 x 座標
        int[] minRow = new int[n + 1];  // 每一行的最小 x 座標
        int[] maxCol = new int[n + 1];  // 每一列的最大 y 座標
        int[] minCol = new int[n + 1];  // 每一列的最小 y 座標

        // 初始化最小值為最大可能值
        Array.Fill(minRow, n + 1);
        Array.Fill(minCol, n + 1);
        
        // 第一次遍歷：統計每一行和每一列的邊界值
        foreach(var building in buildings)
        {
            int x = building[0];  // 列座標
            int y = building[1];  // 行座標
            
            // 更新第 y 行的 x 座標範圍
            maxRow[y] = Math.Max(maxRow[y], x);
            minRow[y] = Math.Min(minRow[y], x);
            
            // 更新第 x 列的 y 座標範圍
            maxCol[x] = Math.Max(maxCol[x], y);
            minCol[x] = Math.Min(minCol[x], y);
        }

        // 第二次遍歷：檢查每個建築是否被覆蓋
        int res = 0;
        foreach(var building in buildings)
        {
            int x = building[0];
            int y = building[1];
            
            // 檢查建築是否被覆蓋：
            // 1. x > minRow[y] && x < maxRow[y]：在該行中不是最左也不是最右
            // 2. y > minCol[x] && y < maxCol[x]：在該列中不是最上也不是最下
            if (x > minRow[y] && x < maxRow[y] && y > minCol[x] && y < maxCol[x])
            {
                res++;  // 該建築被覆蓋
            }
        }
        return res;
    }
}
