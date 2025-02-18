namespace leetcode_039
{
    internal class Program
    {
        /// <summary>
        /// 39. Combination Sum
        /// https://leetcode.com/problems/combination-sum/description/
        /// 39. 组合总和
        /// https://leetcode.cn/problems/combination-sum/description/
        /// 
        /// 給定一個無重複的正整數數組 candidates 和一個整數 target，找出 candidates 中所有可以使數字和為 target 的組合。每個數字可以在組合中使用多次。
        /// 
        /// Q:
        /// 一組無重複的正整數 candidates
        /// 一個目標數 target
        /// 無限次使用 candidates 中的數字
        /// 找出所有和等於 target 的不重複組合（順序不同視為相同組合）
        /// 
        /// result裡面好幾組陣列, 所以要用 foreach 取出來
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = {2, 3, 5};
            int target = 8;

            var result = CombinationSum(input, target);
            foreach (var item in result)
            {
                Console.WriteLine(string.Join(",", item));
            }
            Console.WriteLine();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/combination-sum/solutions/406516/zu-he-zong-he-by-leetcode-solution/
        /// https://leetcode.cn/problems/combination-sum/solutions/2747858/liang-chong-fang-fa-xuan-huo-bu-xuan-mei-mhf9/
        /// https://leetcode.cn/problems/combination-sum/solutions/2344722/39-zu-he-zong-he-by-stormsunshine-hhd2/
        /// 
        /// 解題思路：
        /// 這是一個典型的**回溯（Backtracking）**問題：
        /// 1.遞歸遍歷所有可能的數字組合。
        /// 2.剪枝：如果當前總和超過 target，則提前結束。
        /// 3.避免重複組合：使用索引 start 來控制數字的選取範圍，確保相同數字不會出現在不同位置的組合中（如 [2, 3, 3] 和 [3, 2, 3] 視為相同組合）。
        /// 4.可重複選擇數字：遞歸時不移動索引 start，允許當前數字多次選取。
        /// 
        /// 這種 回溯解法 適用於 組合類問題（如子集、排列、N 皇后等）。💡🚀
        /// </summary>
        /// <param name="candidates">無重複的正整數數組</param>
        /// <param name="target">整數目標值</param>
        /// <returns></returns>
        public static IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            // 創建結果列表 result，用來存儲所有符合條件的組合
            IList<IList<int>> result = new List<IList<int>>();
            // 創建 combination 列表，存儲當前組合，在回溯過程中使用。
            List<int> combination = new List<int>();
            // 排序 candidates，有助於剪枝（當數字大於 target 時可提前跳過），以便在後續處理中可以跳過不必要的計算。
            Array.Sort(candidates);
            // 呼叫回溯函式開始搜尋，初始索引為 0
            Backtrack(candidates, target, result, combination, 0);
            return result;
        }


        /// <summary>
        /// Backtracking_回溯法
        /// 
        /// 這個回溯方法通過添加數字、檢查它們是否和為目標值，並在不匹配時回溯來系統地構建組合。
        /// 對 candidates 進行排序確保了生成的組合沒有重複，因為在同一位置上無法用較小的數字替換較大的數字。
        /// 回溯法是一種深度優先搜索的策略，通過不斷地嘗試、回退、再嘗試的方式，探索所有可能的解。
        /// </summary>
        /// <param name="candidates">接受候選數組 </param>
        /// <param name="target">目標值</param>
        /// <param name="result">結果列表</param>
        /// <param name="combination">當前組合</param>
        /// <param name="start">起始索引</param>
        public static void Backtrack(int[] candidates, int target, IList<IList<int>> result, List<int> combination, int start)
        {
            // 如果目標值 target 為 0，表示找到了一個有效組合。
            if (target == 0)
            {
                // 存入結果（要建立新 List 避免引用問題, 新的一組）
                result.Add(new List<int>(combination));
                return;
            }

            // 迴圈遍歷 candidates，從 start 索引開始，避免重複組合
            for (int i = start; i < candidates.Length; i++)
            {
                // 剪枝：如果當前數字超過目標值，則跳過
                // 跳過此次迭代，因為加入這個數字會超過目標。
                if (target < candidates[i]) 
                {
                    continue;
                }

                // 選擇當前數字，加入 combination
                combination.Add(candidates[i]);
                // 遞歸繼續搜尋，target 減去當前數字，且索引不變（允許重複使用相同數字）
                // 調用回溯方法，更新目標值 target 為 target - candidates[i]，並從當前索引 i 開始繼續遞歸。
                Backtrack(candidates, target - candidates[i], result, combination, i);
                // 回溯，撤銷選擇;
                // 從當前組合 combination 中移除最後一個添加的候選數，進行回溯。
                // 從 combination 中移除最後添加的數字，以便探索其他可能的組合。
                combination.RemoveAt(combination.Count - 1);
            }
        }
    }
}
