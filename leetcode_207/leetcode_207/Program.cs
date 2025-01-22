using System.Runtime.ConstrainedExecution;

namespace leetcode_207
{
    internal class Program
    {
        /// <summary>
        /// 207. Course Schedule
        /// https://leetcode.com/problems/course-schedule/
        /// 207. 课程表
        /// https://leetcode.cn/problems/course-schedule/
        /// 
        /// 拓扑排序 基本概念介紹
        /// 
        /// 拓扑排序（Topological Sorting）是一種針對 有向無環圖（DAG, Directed Acyclic Graph）的排序方法，用於將圖中的所有節點排列成線性順序，並且滿足以下條件：
        /// 1. 如果在圖中有一條從節點 A 指向節點 B 的有向邊 (A → B)，那麼在排序中節點 A 必須排在節點 B 之前。
        /// 2. 拓扑排序僅適用於無環圖，因為有環的圖無法確定先後順序。
        /// 
        /// 典型應用場景
        /// 課程安排：解決依賴關係的問題，例如某些課程需要先完成其他課程。
        /// 任務調度：確定任務執行順序，保證依賴的任務先執行。
        /// 構建順序：如軟體編譯順序，其中某些模組需要依賴其他模組。
        /// 
        /// 常用的拓扑排序算法
        /// 1. Kahn 算法
        /// 2. DFS（深度優先搜索）算法
        /// 核心思想：基於 逆後序遍歷。
        /// 遍歷每個節點，對於尚未訪問的節點執行深度優先搜索。
        /// 在遞歸返回時，將該節點加入結果棧中。
        /// 最終將棧中的節點順序輸出，得到拓扑排序。 時間複雜度：O(V + E)。
        /// 
        /// 注意
        /// 如果圖中存在環，則拓扑排序無法完成，需要先檢測環的存在。
        /// Kahn 算法和 DFS 的結果可能不唯一，但均為合法的拓扑排序。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] prerequisites = new int[][]
            {
                 new int[]{ 1, 0 }
            };

            int numCourses = 2;

            Console.WriteLine("res: " + CanFinish(numCourses, prerequisites));
        }

        // 建立一個有向圖 
        static IList<int>[] graph;
        // 每個課程拜訪狀態
        static int[] states;

        /// <summary>
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
        /// <param name="numCourses">總共多少課程</param>
        /// <param name="prerequisites">上[0]課程 要先上過[1]課程; prerequisites[a, b] 看成一條有向邊 b -> a; 0 開始, 所以總共 n - 1 條邊</param>
        /// <returns></returns>
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
                // 塞入題目給予的資料; 要先上[1]的課程, 才可以上[0]的課程
                // 建立圖: 把每個 prerequisites[0, 1] 看成一條有向邊 1 -> 0.
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
        /// 深度優先, 找尋每個課程狀態
        /// </summary>
        /// <param name="curr"></param>
        /// <returns></returns>
        public static bool DFS(int curr)
        {
            if (states[curr] == 1)
            {
                // 搜索中狀態: 1, 有環就錯誤
                return false;
            }

            if (states[curr] == 2)
            {
                // 已完成狀態: 2, 已經訪問過沒問題. 不影響答案
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
            // 都成立就把狀態改為 已完成狀態
            states[curr] = 2;

            return true;
        }
    }
}
