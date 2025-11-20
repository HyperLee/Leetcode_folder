namespace leetcode_757;

class Program
{
    /// <summary>
    /// 757. Set Intersection Size At Least Two
    /// https://leetcode.com/problems/set-intersection-size-at-least-two/description/?envType=daily-question&envId=2025-11-20
    /// 757. 设置交集大小至少为2
    /// https://leetcode.cn/problems/set-intersection-size-at-least-two/description/?envType=daily-question&envId=2025-11-20
    ///
    /// Problem statement:
    /// You are given a 2D integer array intervals where intervals[i] = [starti, endi]
    /// represents all the integers from starti to endi inclusively.
    ///
    /// A containing set is an array nums where each interval from intervals
    /// has at least two integers in nums. For example, if intervals = [[1,3], [3,7], [8,9]],
    /// then [1,2,4,7,8,9] and [2,3,4,8,9] are containing sets.
    /// Return the minimum possible size of a containing set.
    ///
    /// 繁體中文翻譯：
    /// 給定一個二維整數陣列 intervals，其中 intervals[i] = [starti, endi] 表示從 starti 到 endi（包含端點）
    /// 的所有整數。
    ///
    /// 一個包含集合（containing set）是數組 nums，對於 intervals 中的每個區間，都至少有兩個整數屬於 nums 並且落在該區間內。
    /// 例如，若 intervals = [[1,3], [3,7], [8,9]]，則 [1,2,4,7,8,9] 與 [2,3,4,8,9] 都是包含集合。
    /// 回傳一個包含集合的最小可能大小（元素個數）。
    /// </summary>
    /// <param name="args">命令列參數（未使用）</param>
    static void Main(string[] args)
    {
        var solution = new Program();
        
        // 測試案例 1: [[1,3],[1,4],[2,5],[3,5]]
        // 預期輸出: 3
        int[][] test1 = new int[][]
        {
            new int[] {1, 3},
            new int[] {1, 4},
            new int[] {2, 5},
            new int[] {3, 5}
        };
        int result1 = solution.IntersectionSizeTwo(test1);
        Console.WriteLine($"測試案例 1: [[1,3],[1,4],[2,5],[3,5]]");
        Console.WriteLine($"結果: {result1}");
        Console.WriteLine($"預期: 3\n");
        
        // 測試案例 2: [[1,2],[2,3],[2,4],[4,5]]
        // 預期輸出: 5
        int[][] test2 = new int[][]
        {
            new int[] {1, 2},
            new int[] {2, 3},
            new int[] {2, 4},
            new int[] {4, 5}
        };
        int result2 = solution.IntersectionSizeTwo(test2);
        Console.WriteLine($"測試案例 2: [[1,2],[2,3],[2,4],[4,5]]");
        Console.WriteLine($"結果: {result2}");
        Console.WriteLine($"預期: 5\n");
        
        // 測試案例 3: [[1,3],[3,7],[8,9]]
        // 預期輸出: 5
        int[][] test3 = new int[][]
        {
            new int[] {1, 3},
            new int[] {3, 7},
            new int[] {8, 9}
        };
        int result3 = solution.IntersectionSizeTwo(test3);
        Console.WriteLine($"測試案例 3: [[1,3],[3,7],[8,9]]");
        Console.WriteLine($"結果: {result3}");
        Console.WriteLine($"預期: 5");
    }

    /// <summary>
    /// 使用貪心演算法解決集合交集大小至少為 2 的問題
    /// 
    /// 核心思路:
    /// 1. 排序策略: 按區間左端點升序排列,當左端點相同時按右端點降序排列
    ///    - 這確保處理時,較短的區間會先被考慮
    ///    - 從區間開始位置添加元素時能最大化覆蓋效果
    /// 
    /// 2. 從後往前處理: 從最後一個區間開始往前遍歷
    ///    - 對於每個區間,檢查它與交集集合已有多少個相交元素
    ///    - 如果不足 2 個,從該區間的左邊界開始添加元素
    ///    - 每添加一個元素,更新所有受影響的前面區間的相交計數
    /// 
    /// 3. 貪心選擇: 總是從區間的最左邊開始選擇元素
    ///    - 因為經過排序,左邊的元素更有可能覆蓋到前面的區間
    ///    - 這樣能最小化最終交集集合的大小
    /// </summary>
    /// <param name="intervals">二維整數陣列,每個元素代表一個區間 [start, end]</param>
    /// <returns>滿足條件的最小交集集合大小</returns>
    public int IntersectionSizeTwo(int[][] intervals)
    {
        int n = intervals.Length;
        int res = 0; // 記錄交集集合的最終大小
        int m = 2; // 每個區間需要與交集集合至少有 2 個相交元素
        
        // 排序: 左端點升序,左端點相同時右端點降序
        // 這樣能確保在處理時,區間長度較短的會優先處理
        Array.Sort(intervals, (a, b) =>
        {
            if(a[0] == b[0])
            {
                return b[1] - a[1]; // 右端點降序
            }
            return a[0] - b[0]; // 左端點升序
        });

        // temp[i] 記錄第 i 個區間已經與交集集合有哪些相交的元素
        IList<int>[] temp = new IList<int>[n];
        for(int i = 0; i < n; i++)
        {
            temp[i] = new List<int>();
        }

        // 從後往前處理每個區間
        for(int i = n - 1; i >= 0; i--)
        {
            // k 是目前第 i 個區間已有的相交元素個數
            // 如果 k < m,需要從區間左邊界開始添加元素
            for(int j = intervals[i][0], k = temp[i].Count; k < m; j++, k++)
            {
                res++; // 交集集合大小增加 1
                Help(intervals, temp, i, j); // 更新所有受影響的區間
            }
        }
        return res;
    }

    /// <summary>
    /// 輔助方法: 當向交集集合添加新元素時,更新所有受影響區間的相交元素記錄
    /// 
    /// 工作流程:
    /// 1. 從當前位置 pos 開始往前檢查所有區間
    /// 2. 如果某個區間的右端點小於新添加的元素 num,表示該元素不在此區間內
    ///    - 由於區間已按左端點升序排列,更前面的區間右端點只會更小
    ///    - 因此可以直接中斷,不需要繼續檢查
    /// 3. 如果元素 num 在區間 [start, end] 內,將其加入該區間的相交元素列表
    /// 
    /// 這個方法確保了每次添加元素到交集集合時,所有包含該元素的區間都能正確更新其相交計數
    /// </summary>
    /// <param name="intervals">排序後的區間陣列</param>
    /// <param name="temp">記錄每個區間與交集集合相交元素的陣列</param>
    /// <param name="pos">當前處理的區間位置</param>
    /// <param name="num">新添加到交集集合的元素值</param>
    public void Help(int[][] intervals, IList<int>[] temp, int pos, int num)
    {
        // 從當前位置往前檢查所有區間
        for(int i = pos; i >= 0; i--)
        {
            // 如果區間的右端點小於 num,表示 num 不在此區間內
            // 由於排序的特性,更前面的區間也不會包含 num,可以直接中斷
            if(intervals[i][1] < num)
            {
                break;
            }
            // num 在區間 [intervals[i][0], intervals[i][1]] 內,記錄此相交元素
            temp[i].Add(num);
        }
    }
}
