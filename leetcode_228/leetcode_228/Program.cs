using System.Text;

namespace leetcode_228;

class Program
{
    /// <summary>
    /// 228. Summary Ranges
    /// https://leetcode.com/problems/summary-ranges/description/
    /// 228. 彙總區間
    /// https://leetcode.cn/problems/summary-ranges/description/
    ///
    /// English original:
    /// You are given a sorted unique integer array nums.
    ///
    /// A range [a,b] is the set of all integers from a to b (inclusive).
    ///
    /// Return the smallest sorted list of ranges that cover all the numbers in the array exactly.
    /// That is, each element of nums is covered by exactly one of the ranges, and there is no integer x
    /// such that x is in one of the ranges but not in nums.
    ///
    /// Each range [a,b] in the list should be output as:
    ///
    /// "a->b" if a != b
    /// "a" if a == b
    ///
    /// 繁體中文：
    /// 給定一個已排序且所有元素皆唯一的整數陣列 nums。
    ///
    /// 範圍 [a,b] 是從 a 到 b 的所有整數集合（包含 a 與 b）。
    ///
    /// 請回傳最小的已排序範圍列表，使其能精確涵蓋陣列中的所有數字。
    /// 也就是說，nums 中每個元素都只會被一個範圍涵蓋一次，且不存在任何整數 x
    /// 屬於某個範圍但不屬於 nums。
    ///
    /// 列表中的每個範圍 [a,b] 應輸出為：
    ///
    /// 若 a != b，輸出 "a->b"
    /// 若 a == b，輸出 "a"
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法: 雙指標
    /// 題目大概意思:
    /// 題目輸入的陣列已經過排序, 遞增 <可能是連續也有不連續的數字段落>
    /// 要找出連續區間(數字要連續, 不能斷), 將之輸出
    /// 
    /// 使用雙指針解題目
    /// i 指向區間起始位置, j 向後遍歷直到不滿足區連續遞增的則當作區間結束
    /// 下一個區間計算 i 指向 j + 1 為區間起始位置(i ~ j 位置已經跑過, 所以從後一個開始), j 則維持上述方式找出 斷掉的位置結束
    /// 如此循環
    /// 
    /// 區間 [a, b] -> a 到 b 為連續遞增數字區間, 不能有斷開的跳過的
    /// 
    /// (j + 1 == nums.Length): 右邊界結束區間, 陣列從 0 開始所以 j + 1 是 nums 的右邊界  
    /// (nums[j] + 1 != nums[j + 1]): 區間內數字是連續遞增的所以 nums[j] + 1 要等同 nums[j + 1]  
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public IList<string> SummaryRanges(int[] nums)
    {
        List<string> res = new List<string>();
        // i 初始設定 第一個區間的起始位置
        int i = 0;

        for(int j = 0; j <  nums.Length; j++)
        {
            // i 固定之後, j 在後面遍歷. 直到遇到不連續的遞增 nums[j] + 1 != nums[j + 1]
            // 或是 j 走道輸入資料的最右邊邊界 則當前的區間範圍就是[i, j] 寫入 StringBuilder
            if ((j + 1 == nums.Length) || (nums[j] + 1 != nums[j + 1]))
            {
                // 將當前區間 [i, j] 寫入結果
                StringBuilder sb = new StringBuilder();
                // 區間起始(左邊界) i
                sb.Append(nums[i]);

                // 區間起始與結束不同位置需要加上 箭號指示
                // 區間位置相同就不需要. 題目要求
                if(i != j)
                {
                    // 區間結束(右邊界) j
                    sb.Append("->").Append(nums[j]);
                }

                // 寫入結果
                res.Add(sb.ToString());
                // 將 i 指向下一個區間的起始點 j + 1 當作新的區間左邊界起始位置 (因 i ~ j 已經跑過)
                i = j + 1;
            }            
        }
        return res;
    }
}
