namespace leetcode_040
{
    internal class Program
    {
        /// <summary>
        /// 40. Combination Sum II
        /// https://leetcode.com/problems/combination-sum-ii/description/?envType=daily-question&envId=2024-08-13
        /// 
        /// 40. 组合总和 II
        /// https://leetcode.cn/problems/combination-sum-ii/description/
        /// 
        /// 本題目是 回溯 題型
        /// 但不是標準回溯
        /// 因為有扣 重覆元素 要求
        /// 
        /// 陣列 candidates 中可能存在重覆元素, 因此可能得到有重覆組合答案
        /// 需要執行去重覆問題
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 10, 1, 2, 7, 6, 1, 5 };
            int target = 8;

            //Console.WriteLine(CombinationSum2(input, target));

            Console.ReadKey();
        }


        IList<IList<int>> res = new List<IList<int>>();
        // 儲存當前組合
        IList<int> temp = new List<int>();
        int[] candidates;
        int n;

        /// <summary>
        /// ref: 遞迴 + 回溯
        /// https://leetcode.cn/problems/combination-sum-ii/solutions/407850/zu-he-zong-he-ii-by-leetcode-solution/
        /// https://leetcode.cn/problems/combination-sum-ii/solutions/2344725/40-zu-he-zong-he-ii-by-stormsunshine-mq52/
        /// </summary>
        /// <param name="candidates"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            // 遞增排序
            Array.Sort(candidates);
            this.candidates = candidates;
            this.n = candidates.Length;
            Backtrack(0, target);

            return res;
        }


        /// <summary>
        /// 回溯
        ///
        /// 為了避免重覆計算, 順序為先將資料排序,
        /// 然後由左至右考慮將每個元素加入 temp
        /// 對於 0 <= i < j < n
        /// candidates[i] 不可能在 candidates[j] 之後才加入 temp
        /// 
        /// C#中List集合使用RemoveAt方法移除指定索引位置的元素
        /// https://www.cnblogs.com/xu-yi/p/11026463.html
        /// </summary>
        /// <param name="index">下標 index</param>
        /// <param name="remain">剩餘數字總和</param>
        public void Backtrack(int index, int remain)
        {
            if (remain == 0)
            {
                // 符合 target, 加入答案列表
                res.Add(new List<int>(temp));
            }
            else
            {
                // 因為已排序, 如果遇到 candidates[i] > remain 則
                // 陣列中 index 裡面的元素資料也會大於 remain. 不需要加入
                for (int i = index; i < n && candidates[i] <= remain; i++)
                {
                    if(i > index && candidates[i] == candidates[i - 1])
                    {
                        // 排除相同元素, 避免產生重覆組合
                        continue;
                    }

                    // 開始回溯, 加入
                    temp.Add(candidates[i]);
                    // 開始往下找
                    Backtrack(i + 1, remain - candidates[i]);
                    // 結束回溯, 移除特定位置, 恢復添加前狀態
                    temp.RemoveAt(temp.Count - 1);
                }
            }
        }


    }
}
