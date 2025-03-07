namespace leetcode_078
{
    internal class Program
    {
        /// <summary>
        /// 78. Subsets
        /// https://leetcode.com/problems/subsets/description/?envType=daily-question&envId=2024-05-21
        /// 78. 子集
        /// https://leetcode.cn/problems/subsets/description/
        /// 冪集 wiki
        /// https://zh.wikipedia.org/zh-tw/%E5%86%AA%E9%9B%86
        /// 
        /// 給定一個不含重複數字的整數陣列 nums，找出所有可能的子集（包含空集合與完整集合）。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = {1, 2};
            var res =  Subsets2(input);

            //foreach(var i in res)
            //{
            //    for(int j = 0; j < i.Count; j++)
            //    {
            //        Console.Write(i[j] + ", ");
            //    }
            //    Console.WriteLine();
            //}

            // 寫法2; string.Join, 比較簡潔
            foreach (var item in res)
            {
                Console.WriteLine(string.Join(",", item));
            }

        }


        /// <summary>
        /// ref:解法二 使用迭代的方法生成所有子集
        /// https://leetcode.cn/problems/subsets/solutions/1918156/by-stormsunshine-qhdj/
        /// 思路二：
        /// https://leetcode.cn/problems/subsets/solutions/7683/er-jin-zhi-wei-zhu-ge-mei-ju-dfssan-chong-si-lu-9c/
        /// 
        /// 務必注意: 空集合是所有集合的 子集合的一種
        /// 
        /// 1.從空集開始。
        /// 2.對於每個新元素，複製現有子集並加入該元素，生成新的子集。
        /// 3.重複此過程，直到處理完所有元素。
        /// 
        /// 簡單說就是在原先的子集合中, 每輪加入一個新的 num
        /// 就會把原先子集合多增加一個元素 擴大
        /// 
        /// 時間複雜度：O(2^n)
        /// 空間複雜度：O(2^n * n)（考慮輸出結果），O(1)（不考慮輸出）
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> Subsets2(int[] nums)
        {
            IList<IList<int>> res = new List<IList<int>>();
            // 1️. 先加入空集合 []
            res.Add(new List<int>());

            // 2️. 遍歷 nums 陣列的每個數字
            foreach (int num in nums)
            {
                // 目前 res 內已有的子集數量
                int size = res.Count;
                // 3️. 對 res 內的所有子集，新增一個新的子集，將 num 加入
                for (int i = 0; i < size; i++)
                {
                    // 4️. 複製當前子集 res[i]
                    IList<int> temp = new List<int>(res[i]);
                    // 5️. 在複製的子集加入 num;
                    // 注意這裡是加入 num 不是加入 res
                    temp.Add(num);
                    // 6️. 將新子集加入 res
                    res.Add(temp);
                }
            }

            return res;
        }
    }
}
