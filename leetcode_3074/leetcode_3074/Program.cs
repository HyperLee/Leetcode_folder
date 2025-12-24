namespace leetcode_3074
{
    internal class Program
    {
        /// <summary>
        /// https://leetcode.com/problems/apple-redistribution-into-boxes/description/?envType=daily-question&envId=2025-12-24
        /// 3074. Apple Redistribution into Boxes
        /// 3074. 重新分装苹果
        /// https://leetcode.cn/problems/apple-redistribution-into-boxes/description/?envType=daily-question&envId=2025-12-24
        ///
        /// Problem:
        /// You are given an array `apple` of size `n` and an array `capacity` of size `m`.
        /// There are `n` packs where the ith pack contains `apple[i]` apples. There are `m` boxes as well, and the ith box has a capacity of `capacity[i]` apples.
        /// Return the minimum number of boxes you need to select to redistribute these `n` packs of apples into boxes.
        /// Note that apples from the same pack can be distributed into different boxes.
        ///
        /// 繁體中文說明：
        /// 給定大小為 `n` 的陣列 `apple` 與大小為 `m` 的陣列 `capacity`。
        /// 有 `n` 包蘋果，第 i 包含有 `apple[i]` 顆蘋果。也有 `m` 個箱子，第 i 個箱子最多可裝 `capacity[i]` 顆蘋果。
        /// 請回傳為將這 `n` 包蘋果重新分裝到箱子中，最少需要選擇的箱子數量。
        /// 注意：同一包蘋果中的蘋果可分裝到不同的箱子中。
        /// </summary>
        /// <param name="args">命令列參數</param>
        static void Main(string[] args)
        {
            var program = new Program();

            // 測試案例 1
            int[] apple1 = { 1, 3, 2 };
            int[] capacity1 = { 4, 3, 1, 5, 2 };
            int result1 = program.MinimumBoxes(apple1, capacity1);
            Console.WriteLine($"測試案例 1:");
            Console.WriteLine($"蘋果包: [{string.Join(", ", apple1)}]");
            Console.WriteLine($"箱子容量: [{string.Join(", ", capacity1)}]");
            Console.WriteLine($"最少箱子數: {result1}");
            Console.WriteLine($"預期結果: 2\n");

            // 測試案例 2
            int[] apple2 = { 5, 5, 5 };
            int[] capacity2 = { 2, 4, 2, 7 };
            int result2 = program.MinimumBoxes(apple2, capacity2);
            Console.WriteLine($"測試案例 2:");
            Console.WriteLine($"蘋果包: [{string.Join(", ", apple2)}]");
            Console.WriteLine($"箱子容量: [{string.Join(", ", capacity2)}]");
            Console.WriteLine($"最少箱子數: {result2}");
            Console.WriteLine($"預期結果: 3\n");

            // 測試案例 3
            int[] apple3 = { 9 };
            int[] capacity3 = { 2, 2, 2, 2, 2 };
            int result3 = program.MinimumBoxes(apple3, capacity3);
            Console.WriteLine($"測試案例 3:");
            Console.WriteLine($"蘋果包: [{string.Join(", ", apple3)}]");
            Console.WriteLine($"箱子容量: [{string.Join(", ", capacity3)}]");
            Console.WriteLine($"最少箱子數: {result3}");
            Console.WriteLine($"預期結果: 5\n");
        }

        /// <summary>
        /// 計算將所有蘋果重新分裝所需的最少箱子數量
        /// 
        /// 解題思路：
        /// 1. 既然同一個包裹中的蘋果可以分裝到不同的箱子中，那就先把所有蘋果堆在一起
        /// 2. 為了少用箱子，要先裝大箱子，再裝小箱子（貪心策略）
        /// 3. 將箱子容量由大到小排序，依序裝入直到所有蘋果都被裝完
        /// 
        /// 時間複雜度：O(n + m log m)，其中 n 是蘋果包數，m 是箱子數
        /// 空間複雜度：O(1)
        /// </summary>
        /// <param name="apple">蘋果陣列，每個元素代表一包蘋果的數量</param>
        /// <param name="capacity">箱子容量陣列，每個元素代表一個箱子的容量</param>
        /// <returns>最少需要的箱子數量</returns>
        public int MinimumBoxes(int[] apple, int[] capacity)
        {
            // 步驟 1: 計算所有蘋果的總數量
            // 因為可以任意分裝，所以只需要知道總數即可
            int sum = apple.Sum();
            
            // 步驟 2: 將箱子容量由大到小排序
            // 先排序（由小到大）
            Array.Sort(capacity);
            // 再反轉（變成由大到小）
            Array.Reverse(capacity);

            // 步驟 3: 貪心策略 - 依序使用最大的箱子裝蘋果
            int res = 0;
            while (sum > 0)
            {
                // 使用當前最大的箱子裝蘋果
                sum -= capacity[res];
                // 箱子數量加 1
                res++;
            }
            
            return res;
        }
    }
}
