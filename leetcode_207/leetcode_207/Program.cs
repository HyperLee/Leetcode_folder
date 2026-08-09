namespace leetcode_207
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 207. Course Schedule
        /// https://leetcode.com/problems/course-schedule/description/
        ///
        /// There are numCourses courses labeled 0 through numCourses - 1. prerequisites[i] = [a_i,b_i] means course b_i must be completed before course a_i. For example, [0,1] means course 1 is required before course 0. Return true if all courses can be finished; otherwise return false.
        ///
        /// Example 1:
        /// Input: numCourses = 2, prerequisites = [[1,0]]
        /// Output: true
        /// Explanation: Complete course 0 before course 1, so all courses can be finished.
        ///
        /// Example 2:
        /// Input: numCourses = 2, prerequisites = [[1,0],[0,1]]
        /// Output: false
        /// Explanation: Course 1 requires 0 and course 0 requires 1, so completion is impossible.
        ///
        /// Constraints:
        /// - 1 &lt;= numCourses &lt;= 2000
        /// - 0 &lt;= prerequisites.length &lt;= 5000
        /// - prerequisites[i].length == 2
        /// - 0 &lt;= a_i, b_i &lt; numCourses
        /// - All prerequisite pairs are unique.
        /// </para>
        /// <para>
        /// 207. 課程表
        /// https://leetcode.cn/problems/course-schedule/description/
        ///
        /// 共有 numCourses 門課程，編號從 0 到 numCourses - 1。prerequisites[i] = [a_i,b_i] 表示修讀 a_i 前必須先完成 b_i。例如，[0,1] 表示修讀課程 0 前必須先完成課程 1。若能完成所有課程回傳 true，否則回傳 false。
        ///
        /// 範例 1：
        /// 輸入：numCourses = 2, prerequisites = [[1,0]]
        /// 輸出：true
        /// 說明：先完成課程 0 再修讀課程 1，因此能完成所有課程。
        ///
        /// 範例 2：
        /// 輸入：numCourses = 2, prerequisites = [[1,0],[0,1]]
        /// 輸出：false
        /// 說明：課程 1 需要先完成 0，而課程 0 又需要先完成 1，因此不可能完成。
        ///
        /// 限制條件：
        /// - 1 &lt;= numCourses &lt;= 2000
        /// - 0 &lt;= prerequisites.length &lt;= 5000
        /// - prerequisites[i].length == 2
        /// - 0 &lt;= a_i, b_i &lt; numCourses
        /// - 所有先修課程配對皆不重複。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行七組課程依賴案例，對照驗證 DFS 三色標記與 Kahn BFS 兩種解法。
        /// 測試資料均符合題目限制，最後輸出通過案例數與演算法驗證數。
        /// </summary>
        private static void RunSamples()
        {
            SampleCase[] sampleCases =
            {
                new("單一課程，沒有先修條件", 1, [], true),
                new("單向依賴", 2, [[1, 0]], true),
                new("兩門課互相依賴", 2, [[1, 0], [0, 1]], false),
                new("菱形依賴且沒有環", 4, [[1, 0], [2, 0], [3, 1], [3, 2]], true),
                new("三門課形成環", 3, [[1, 0], [2, 1], [0, 2]], false),
                new("非連通且各部分都沒有環", 5, [[1, 0], [3, 2], [4, 3]], true),
                new("非連通圖的其中一部分有環", 6, [[1, 0], [2, 1], [1, 2], [4, 3]], false)
            };

            int passedCases = 0;
            int passedChecks = 0;

            for (int index = 0; index < sampleCases.Length; index++)
            {
                (bool casePassed, int checkCount) = RunSample(index + 1, sampleCases[index]);
                passedChecks += checkCount;

                if (casePassed)
                {
                    passedCases++;
                }
            }

            int totalChecks = sampleCases.Length * 2;
            Console.WriteLine(
                $"總結：{passedCases}/{sampleCases.Length} 筆案例通過（{passedChecks}/{totalChecks} 項演算法驗證通過）。");
        }

        /// <summary>
        /// 執行單一案例，分別呼叫兩種解法並與預期答案比較。
        /// 輸入案例會為每種解法建立獨立副本，避免任一實作修改資料而影響另一個結果。
        /// </summary>
        /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
        /// <param name="sampleCase">包含案例名稱、課程數、先修關係與預期結果的測試資料。</param>
        /// <returns>案例是否整體通過，以及兩項演算法檢查中的通過數。</returns>
        private static (bool CasePassed, int PassedChecks) RunSample(
            int caseNumber,
            SampleCase sampleCase)
        {
            bool dfsResult = CanFinish(
                sampleCase.NumCourses,
                ClonePrerequisites(sampleCase.Prerequisites));
            bool kahnResult = CanFinish2(
                sampleCase.NumCourses,
                ClonePrerequisites(sampleCase.Prerequisites));
            bool dfsPassed = dfsResult == sampleCase.Expected;
            bool kahnPassed = kahnResult == sampleCase.Expected;

            Console.WriteLine($"案例 {caseNumber}：{sampleCase.Name}");
            Console.WriteLine($"  numCourses：{sampleCase.NumCourses}");
            Console.WriteLine($"  prerequisites：{FormatPrerequisites(sampleCase.Prerequisites)}");
            Console.WriteLine($"  預期：{sampleCase.Expected}");
            Console.WriteLine($"  DFS 三色標記：{dfsResult} => {(dfsPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"  Kahn BFS：{kahnResult} => {(kahnPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (dfsPassed && kahnPassed, Convert.ToInt32(dfsPassed) + Convert.ToInt32(kahnPassed));
        }

        /// <summary>
        /// 深層複製先修課程二維陣列，讓不同解法使用互不共享的測試輸入。
        /// </summary>
        /// <param name="prerequisites">每筆元素格式為 [課程, 先修課程] 的原始陣列。</param>
        /// <returns>內容相同且每個內層陣列皆為新實例的副本。</returns>
        private static int[][] ClonePrerequisites(int[][] prerequisites)
        {
            return prerequisites.Select(static prerequisite => prerequisite.ToArray()).ToArray();
        }

        /// <summary>
        /// 將先修課程資料格式化為穩定且適合 console 與 README 顯示的文字。
        /// </summary>
        /// <param name="prerequisites">每筆元素格式為 [課程, 先修課程] 的陣列。</param>
        /// <returns>例如 [[1,0], [2,0]]；空陣列則回傳 []。</returns>
        private static string FormatPrerequisites(int[][] prerequisites)
        {
            return $"[{string.Join(", ", prerequisites.Select(
                static prerequisite => $"[{prerequisite[0]},{prerequisite[1]}]"))}]";
        }

        private sealed record SampleCase(
            string Name,
            int NumCourses,
            int[][] Prerequisites,
            bool Expected);

        // DFS 使用的有向圖；每次呼叫 CanFinish 都會重新建立。
        private static IList<int>[] graph = Array.Empty<IList<int>>();

        // 0 表示未搜尋、1 表示搜尋中、2 表示已完成。
        private static int[] states = Array.Empty<int>();

        /// <summary>
        /// 判斷指定的課程與先修關係是否能完成全部課程。
        /// 解法將先修關係建立為有向圖，並以 DFS 三色標記偵測搜尋路徑中是否出現回邊；
        /// 輸入需符合課程編號介於 0 到 numCourses - 1、每筆先修關係包含兩個編號的題目條件，
        /// 若圖中沒有環則回傳 true，否則回傳 false。
        ///
        /// ref: 建議先看連結說明 比較好理解
        /// https://leetcode.cn/problems/course-schedule/solutions/359392/ke-cheng-biao-by-leetcode-solution/
        /// https://leetcode.cn/problems/course-schedule/solutions/2992884/san-se-biao-ji-fa-pythonjavacgojsrust-by-pll7/
        /// https://leetcode.cn/problems/course-schedule/solutions/2347937/207-ke-cheng-biao-by-stormsunshine-u8lq/
        /// 
        /// 拓樸排序 題型
        /// 本解法使用深度優先搜尋
        /// 
        /// 解題概念
        /// 對於圖中的任意一個節點，它在搜索的過程中有三種狀態，即：
        /// 「未搜索」：我們還沒有搜索到這個節點。
        /// 「搜索中」：我們搜索過這個節點，但還沒有回溯到該節點，即該節點還沒有入棧，還有相鄰的節點沒有搜索完成。
        /// 「已完成」：我們搜索過並且回溯過這個節點，即該節點已經入棧，並且所有該節點的相鄰節點都出現在棧的更底部的位置，滿足拓撲排序的要求。
        /// 通過上述的三種狀態，我們就可以給出使用深度優先搜索得到拓撲排序的演算法流程，在每一輪的搜索搜索開始時，我們任取一個「未搜索」的節點開始進行深度優先搜索。
        /// 
        ///  我們將當前搜索的節點 u 標記為「搜索中」，遍曆該節點的每一個相鄰節點 v：
        ///  如果 v 為「未搜索」，那麼我們開始搜索 v，待搜索完成回溯到 u。
        ///  如果 v 為「搜索中」，那麼我們就找到了圖中的一個環，因此是不存在拓撲排序的。
        ///  如果 v 為「已完成」，那麼說明 v 已經在棧中了，而 u 還不在棧中，因此 u 無論何時入棧都不會影響到 （u，v） 之前的拓撲關係，以及不用進行任何操作。 
        ///  當 u 的所有相鄰節點都為「已完成」時，我們將 u 放入棧中，並將其標記為「已完成」。
        ///  在整個深度優先搜索的過程結束后，如果我們沒有找到圖中的環，那麼棧中存儲這所有的 n 個節點，從棧頂到棧底的順序即為一種拓撲排序。 
        ///  
        /// prerequisites[i] = [ai, bi]
        /// 表示如果要學習課程 ai, 則必須先學習課程 bi
        /// ex: [0, 1]: 要上課程 0, 需要先完成課程 1 才可以
        /// 把 bi 當成 index, 然後 ai 當成 value.
        /// 
        /// 每個 node 拜訪狀態
        /// 未搜索狀態: 0
        /// 搜索中狀態: 1
        /// 已完成狀態: 2
        /// </summary>
        /// <param name="numCourses">課程總數，課程編號範圍為 0 到 numCourses - 1。</param>
        /// <param name="prerequisites">先修關係；[a, b] 代表修習課程 a 前必須先完成課程 b。</param>
        /// <returns>能完成所有課程時回傳 true；存在循環依賴時回傳 false。</returns>
        public static bool CanFinish(int numCourses, int[][] prerequisites)
        {
            // 有向圖課程數量
            graph = new List<int>[numCourses];
            // 每個課程拜訪狀態; 有三種狀態
            states = new int[numCourses];
            // 初始設定都是 未搜索狀態: 0
            Array.Fill(states, 0);
            
            for(int i = 0; i < numCourses; i++)
            {
                // 宣告
                graph[i] = new List<int>();
            }

            foreach (int[] prerequisite in prerequisites)
            {
                // [a, b] 代表 b 必須先於 a，因此建立 b -> a 的有向邊。
                graph[prerequisite[1]].Add(prerequisite[0]);
            }

            for(int i = 0; i < numCourses; i++)
            {
                // 往下找其他尚未搜尋的課程
                bool valid = DFS(i);
                if(!valid)
                {
                    // 不能完成搜尋
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 判斷指定的課程與先修關係是否能完成全部課程。
        /// 解法使用 Kahn 拓樸排序：統計每門課的入度，從入度為零的課程開始逐層移除依賴；
        /// 輸入需符合課程編號介於 0 到 numCourses - 1、每筆先修關係包含兩個編號的題目條件，
        /// 若最後處理的課程數等於 numCourses 則回傳 true，否則代表圖中有環並回傳 false。
        /// </summary>
        /// <param name="numCourses">課程總數，課程編號範圍為 0 到 numCourses - 1。</param>
        /// <param name="prerequisites">先修關係；[a, b] 代表修習課程 a 前必須先完成課程 b。</param>
        /// <returns>能完成所有課程時回傳 true；存在循環依賴時回傳 false。</returns>
        public static bool CanFinish2(int numCourses, int[][] prerequisites)
        {
            IList<int>[] adjacencyList = new List<int>[numCourses];
            int[] indegrees = new int[numCourses];

            for (int course = 0; course < numCourses; course++)
            {
                adjacencyList[course] = new List<int>();
            }

            foreach (int[] prerequisite in prerequisites)
            {
                int course = prerequisite[0];
                int prerequisiteCourse = prerequisite[1];

                // 建立 prerequisiteCourse -> course，並記錄 course 尚有幾個先修條件。
                adjacencyList[prerequisiteCourse].Add(course);
                indegrees[course]++;
            }

            Queue<int> availableCourses = new Queue<int>();
            for (int course = 0; course < numCourses; course++)
            {
                if (indegrees[course] == 0)
                {
                    availableCourses.Enqueue(course);
                }
            }

            int completedCourses = 0;
            while (availableCourses.Count > 0)
            {
                int currentCourse = availableCourses.Dequeue();
                completedCourses++;

                foreach (int nextCourse in adjacencyList[currentCourse])
                {
                    indegrees[nextCourse]--;

                    // 入度歸零代表該課程的所有先修條件都已被處理。
                    if (indegrees[nextCourse] == 0)
                    {
                        availableCourses.Enqueue(nextCourse);
                    }
                }
            }

            return completedCourses == numCourses;
        }

        /// <summary>
        /// 從指定課程開始進行深度優先搜尋，以三色狀態判斷目前搜尋路徑是否形成環。
        /// curr 必須是 graph 與 states 中有效的課程索引；
        /// 若此路徑沒有循環依賴則回傳 true，遇到仍在搜尋中的節點時回傳 false。
        /// </summary>
        /// <param name="curr">目前正在檢查的課程編號。</param>
        /// <returns>目前路徑無環時回傳 true；偵測到回邊時回傳 false。</returns>
        public static bool DFS(int curr)
        {
            if (states[curr] == 1)
            {
                // 搜尋路徑再次遇到「搜尋中」節點，表示找到回邊並形成環。
                return false;
            }

            if (states[curr] == 2)
            {
                // 已完成節點先前已證明其後續路徑無環，不必重複搜尋。
                return true;
            }

            // 當前課程未搜索過, 先改成 搜索中
            states[curr] = 1;
            IList<int> adj = graph[curr];
            foreach(int next in adj)
            {
                // 找出該課程相鄰節點繼續往下找
                bool valid = DFS(next);
                if(!valid)
                {
                    // 都必須有效為 true 才成立
                    return false;
                }
            }

            // 所有相鄰節點皆無環，離開遞迴路徑前標記為已完成。
            states[curr] = 2;

            return true;
        }
    }
}
