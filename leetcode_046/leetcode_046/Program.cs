namespace leetcode_046
{
    internal class Program
    {
        /// <summary>
        /// 46. Permutations
        /// https://leetcode.com/problems/permutations/
        /// 46. 全排列
        /// https://leetcode.cn/problems/permutations/
        /// 
        /// 给定一个不含重复数字的数组 nums ，返回其 所有可能的全排列 。你可以 按任意顺序 返回答案。
        /// 
        /// 本題目是求出"所有可能的排列"，因此需要使用回溯法。
        /// 
        /// 39. Combination Sum 是求出不重複的題型(順序不同視為相同組合), 有必要對輸入的陣列先進行排序 
        /// 46. Permutations是求出"所有可能的排列"(全部列舉出來), 不需要排序
        /// 所以兩題有差異
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 1, 2, 3 };

            var result = Permute(nums);
            foreach (var item in result)
            {
                Console.WriteLine(string.Join(",", item));
            }
            Console.WriteLine();
        }


        /// <summary>
        /// Backtrack 需要先對 nums 排序嗎?
        /// 在 LeetCode 46: Permutations 這題中，排序 nums 不會影響最終的結果，也不會提升效能，因為：
        /// 1. 不影響遞迴樹的結構：
        ///  排列（Permutations）問題是要找出所有可能的順序，而無論 nums 是否排序，所有排列的數量和遞迴樹的結構都相同。
        ///  例如，nums = {3,1,2} 和 nums = {1,2,3} 都會產生 6 種排列。
        /// 2. 不影響回溯過程的剪枝：
        ///  如果這題是 組合（Combinations），我們可以透過排序來減少不必要的遞迴（如 Combination Sum 類題）。
        ///  但 排列問題不涉及「選擇過的數不能再選」的情況，所以排序不會讓 Backtrack() 早點返回。
        /// 3. 程式運行時間相同：
        ///  排列問題的時間複雜度是 O(N * N!)，遠大於排序的 O(N log N)，所以排序的影響可以忽略不計。
        ///  排序後，遞迴仍然會嘗試所有可能的排列，所以效能沒有實質提升。
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> Permute(int[] nums)
        {
            // 初始化 result 來存儲所有排列
            IList<IList<int>> result = new List<IList<int>>();
            // 建立 list（用來存放當前排列）
            List<int> list = new List<int>();
            Backtrack(nums, list, result);
            return result;
        }


        /// <summary>
        /// 回溯法：透過 選擇 → 遞迴 → 回溯 產生排列
        /// 避免重複：使用 list.Contains(nums[i])
        /// 時間複雜度 O(N * N!)
        /// 
        /// 回溯步驟:
        /// 選擇 數字
        /// 遞迴 探索
        /// 回溯 撤銷選擇，嘗試其他可能性
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="list"></param>
        /// <param name="result"></param>
        private static void Backtrack(int[] nums, List<int> list, IList<IList<int>> result)
        {
            // 若 list 長度等於 nums.Length，表示找到一組排列，加入 result
            if (list.Count == nums.Length)
            {
                // 存入結果（要建立新 List 避免引用問題, 新的一組）
                result.Add(new List<int>(list));
                return;
            }

            // 遍歷所有數字
            for (int i = 0; i < nums.Length; i++)
            {
                // 若 list 已經包含該數字，則跳過（避免重複）
                if (list.Contains(nums[i]))
                {
                    continue; // 剪枝：避免重复选择
                }

                // 選擇當前數字
                list.Add(nums[i]);
                // 遞迴繼續
                Backtrack(nums, list, result);
                // 回溯：移除最後一個數字
                list.RemoveAt(list.Count - 1);
            }
        }

    }
}
