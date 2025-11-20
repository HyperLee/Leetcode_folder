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
        
        Console.WriteLine("\n=== 解法二：堆疊 + 二分搜尋法 ===");
        
        // 測試案例 1 (解法二)
        int result1_v2 = solution.IntersectionSizeTwoStack(test1);
        Console.WriteLine($"\n測試案例 1: [[1,3],[1,4],[2,5],[3,5]]");
        Console.WriteLine($"結果: {result1_v2}");
        Console.WriteLine($"預期: 3");
        
        // 測試案例 2 (解法二)
        int result2_v2 = solution.IntersectionSizeTwoStack(test2);
        Console.WriteLine($"\n測試案例 2: [[1,2],[2,3],[2,4],[4,5]]");
        Console.WriteLine($"結果: {result2_v2}");
        Console.WriteLine($"預期: 5");
        
        // 測試案例 3 (解法二)
        int result3_v2 = solution.IntersectionSizeTwoStack(test3);
        Console.WriteLine($"\n測試案例 3: [[1,3],[3,7],[8,9]]");
        Console.WriteLine($"結果: {result3_v2}");
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

    /// <summary>
    /// 解法二：使用堆疊與二分搜尋解決集合交集大小至少為 2 的問題
    /// 
    /// 核心思路：
    /// 1. 排序策略：按區間右端點升序排列
    ///    - 優先處理右端點較小的區間，確保貪心選擇的正確性
    ///    - 右端點小的區間限制更強，應該先滿足
    /// 
    /// 2. 堆疊維護已選擇的區間段：
    ///    - 堆疊中每個元素包含 [left, right, sum]
    ///      * left: 區間段的左端點
    ///      * right: 區間段的右端點
    ///      * sum: 從堆疊底到該元素的累積元素總數
    ///    - 使用哨兵 [-2, -2, 0] 避免邊界判斷
    /// 
    /// 3. 貪心選擇與區間合併：
    ///    - 對每個新區間，計算需要額外添加的元素數量 d
    ///    - 使用二分搜尋快速找到與當前區間重疊的堆疊位置
    ///    - 從區間右端點往左填充 d 個元素（貪心策略）
    ///    - 如果新元素會覆蓋堆疊頂部的區間，則合併它們
    /// 
    /// 4. 時間複雜度：O(n log n)
    ///    - 排序：O(n log n)
    ///    - 每個區間處理：O(log n) 二分搜尋 + O(1) 平攤堆疊操作
    ///    - 總體：O(n log n)
    /// 
    /// 5. 空間複雜度：O(n)
    ///    - 堆疊最多儲存 O(n) 個區間段
    /// </summary>
    /// <param name="intervals">二維整數陣列，每個元素代表一個區間 [start, end]</param>
    /// <returns>滿足條件的最小交集集合大小</returns>
    public int IntersectionSizeTwoStack(int[][] intervals)
    {
        // 按右端點升序排序
        // 右端點小的區間限制更強，應優先處理
        Array.Sort(intervals, (a, b) => a[1] - b[1]);
        
        // 堆疊保存已選擇的閉區間 [left, right] 及累積總和
        // 每個元素格式：[left, right, sum]
        // sum 表示從堆疊底到當前元素的所有區間長度總和
        List<int[]> st = new List<int[]>();
        
        // 哨兵：保證不與任何區間相交
        // 使用 -2 是因為區間端點最小為 0，-2 確保不會重疊
        st.Add(new int[] { -2, -2, 0 });
        
        // 遍歷每個區間
        foreach (int[] t in intervals)
        {
            int start = t[0];
            int end = t[1];
            
            // 使用二分搜尋找到第一個 left >= start 的位置
            // 然後取前一個元素 e，表示與當前區間可能重疊的最後一個堆疊元素
            int[] e = st[LowerBound(st, start) - 1];
            
            // 計算需要添加的元素數量 d
            // 2 是每個區間需要的最小交集大小
            // st[^1][2] - e[2] 是堆疊頂部到 e 之間的累積元素數（已在當前區間右側的元素）
            int d = 2 - (st[st.Count - 1][2] - e[2]);
            
            // 如果 start 落在區間 e 內，需要扣除重疊部分
            // e[1] - start + 1 表示重疊的元素個數
            if (start <= e[1])
            {
                d -= e[1] - start + 1;
            }
            
            // 如果 d <= 0，表示當前區間已經被滿足，無需添加新元素
            if (d <= 0)
            {
                continue;
            }
            
            // 貪心策略：從區間右端點往左填充 d 個元素
            // 如果新添加的區間會覆蓋堆疊頂部的區間，則合併它們
            // end - st[^1][1] <= d 表示新區間的左邊界會覆蓋或超過堆疊頂部的右邊界
            while (end - st[st.Count - 1][1] <= d)
            {
                // 彈出堆疊頂部元素並合併
                e = st[st.Count - 1];
                st.RemoveAt(st.Count - 1);
                
                // 將被覆蓋的區間長度加回 d
                // 這樣可以將多個小區間合併成一個大區間
                d += e[1] - e[0] + 1;
            }
            
            // 將新區間段加入堆疊
            // [end - d + 1, end] 是新添加的區間段
            // st[^1][2] + d 是累積總和
            st.Add(new int[] { end - d + 1, end, st[st.Count - 1][2] + d });
        }
        
        // 返回堆疊頂部的累積總和，即交集集合的大小
        return st[st.Count - 1][2];
    }

    /// <summary>
    /// 開區間二分搜尋：在堆疊中尋找第一個左端點 >= target 的位置
    /// 
    /// 使用開區間二分搜尋模板：
    /// - 搜尋範圍：(left, right) 開區間
    /// - 循環不變量：
    ///   * st[left][0] < target
    ///   * st[right][0] >= target
    /// - 最終返回 right，即第一個滿足條件的位置
    /// 
    /// 開區間二分搜尋的優勢：
    /// - 不需要處理邊界情況（left = 0 或 right = n-1）
    /// - 循環條件簡單：left + 1 < right
    /// - 不會出現死循環
    /// 
    /// 參考資料：https://www.bilibili.com/video/BV1AP41137w7/
    /// </summary>
    /// <param name="st">堆疊（包含區間資訊的列表）</param>
    /// <param name="target">目標值</param>
    /// <returns>第一個左端點 >= target 的位置索引</returns>
    private int LowerBound(List<int[]> st, int target)
    {
        // 開區間 (left, right)
        int left = -1;
        int right = st.Count;
        
        // 當區間不為空時繼續搜尋
        while (left + 1 < right)
        {
            // 使用無符號右移避免溢位
            // 等同於 (left + right) / 2
            int mid = (left + right) >> 1;
            
            // 循環不變量：
            // st[left][0] < target
            // st[right][0] >= target
            if (st[mid][0] < target)
            {
                // 範圍縮小到 (mid, right)
                left = mid;
            }
            else
            {
                // 範圍縮小到 (left, mid)
                right = mid;
            }
        }
        
        // 返回第一個 >= target 的位置
        return right;
    }
}
