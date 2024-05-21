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
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = {1, 2};
            var res =  Subsets2(input);
            foreach(var i in res)
            {
                for(int j = 0; j < i.Count; j++)
                {
                    Console.Write(i[j] + ", ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }


        static IList<IList<int>> res = new List<IList<int>>();
        static IList<int> temp = new List<int>();


        /// <summary>
        /// ref:
        /// 思路三
        /// https://leetcode.cn/problems/subsets/solutions/7683/er-jin-zhi-wei-zhu-ge-mei-ju-dfssan-chong-si-lu-9c/
        /// https://leetcode.cn/problems/subsets/solutions/1918156/by-stormsunshine-qhdj/
        /// 方法二：递归法实现子集枚举
        /// https://leetcode.cn/problems/subsets/solutions/420294/zi-ji-by-leetcode-solution/
        /// 
        /// 採用類似完整二元樹 思維
        /// 左子樹為不選
        /// 右子樹為選
        /// 然後採用中序去找整棵樹
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> Subsets(int[] nums)
        {
            DFS(0, nums);

            return res;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public static void DFS(int index, int[] nums)
        {
            if(index == nums.Length)
            {
                // 紀錄答案
                res.Add(new List<int>(temp));
                return;
            }

            // 考慮選擇當前位置
            temp.Add(nums[index]);
            DFS(index + 1, nums);
            temp.RemoveAt(temp.Count - 1);
            // 考慮不選當前位置
            DFS(index + 1, nums);

        }


        /// <summary>
        /// ref:解法二
        /// https://leetcode.cn/problems/subsets/solutions/1918156/by-stormsunshine-qhdj/
        /// 思路二：
        /// https://leetcode.cn/problems/subsets/solutions/7683/er-jin-zhi-wei-zhu-ge-mei-ju-dfssan-chong-si-lu-9c/
        /// 
        /// 空集合是所有集合的 子集合的一種
        /// 
        /// 一開始先創建一個 List 裡面只加入 一個空集合
        /// 再來去遍歷輸入的nums
        /// 開一個新的暫存 List temp
        /// 複製 res[i] 給 temp
        /// temp加入新的元素,
        /// 然後 res再加入temp
        /// 
        /// 簡單說就是在原先的集合中, 每輪加入一個新的num
        /// 就會把原先集合多增加一個元素 擴大
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> Subsets2(int[] nums)
        {
            IList<IList<int>> res = new List<IList<int>>();
            // 先加入空集合
            res.Add(new List<int>());

            foreach(int num in nums)
            {
                int size = res.Count;
                // 每一輪都添加一個新元素 num
                for(int i = 0; i < size; i++)
                {
                    // 暫存集合; 複製res[i]
                    IList<int> temp = new List<int>(res[i]);
                    // 暫存加入新元素 num
                    temp.Add(num);
                    // 再把暫存集合 加入res, 先前List加入一個新的元素
                    res.Add(temp);
                }
            }

            return res;
        }
    }
}
