namespace leetcode_1550
{
    internal class Program
    {
        /// <summary>
        /// 1550. Three Consecutive Odds
        /// https://leetcode.com/problems/three-consecutive-odds/description/?envType=daily-question&envId=2024-07-01
        /// 1550. 存在连续三个奇数的数组
        /// https://leetcode.cn/problems/three-consecutive-odds/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 3, 5 };
            ThreeConsecutiveOdds(input);
            ThreeConsecutiveOdds2(input);
            Console.ReadKey();
        }



        /// <summary>
        /// https://leetcode.cn/problems/three-consecutive-odds/solutions/382537/cun-zai-lian-xu-san-ge-qi-shu-de-shu-zu-by-leetcod/
        /// https://leetcode.cn/problems/three-consecutive-odds/solutions/860041/1550-cun-zai-lian-xu-san-ge-qi-shu-de-sh-tt3w/
        /// 
        /// 本來想用雙指針
        /// 按順序判斷
        /// 但是看起來不需要
        /// 既然是連續三個
        /// for迴圈 直接跑三個值
        /// 下去測試
        /// 
        /// ------------------
        /// arr[right] % 2 == 1
        /// 也可以替換成
        /// (arr[right] & 1) != 0
        /// 用 bit operation
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static bool ThreeConsecutiveOdds(int[] arr)
        {
            int n = arr.Length;

            // 連續3個數字, 陣列從0開始, 所以i要從2開始跑
            for (int i = 2; i < n; i++)
            {
                // 數字 除 2 為1 代表是奇數
                if (arr[i - 2] % 2 == 1 && arr[i - 1] % 2 == 1 && arr[i] % 2 == 1)
                {
                    Console.WriteLine("Method1: [" + arr[i - 2] + ", " +  arr[i - 1] + ", " +  arr[i] + "]");
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 滑動窗口
        /// 
        /// left為視窗左邊界
        /// right為視窗右邊界
        /// 
        /// right持續往右走
        /// left用來判斷視窗最左邊的奇數位置
        /// 
        /// 
        /// ------------------
        /// arr[right] % 2 == 0
        /// 也可以替換成
        /// (arr[right] & 1) == 0
        /// 用 bit operation
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static bool ThreeConsecutiveOdds2(int[] arr)
        {
            int n = arr.Length;

            if(n < 3)
            {
                return false;
            }

            int left = 0, right = 0;

            while(right < n)
            {
                // 如果為偶數, 左邊界需要移動往右走
                if (arr[right] % 2 == 0)
                {
                    left = right + 1;
                }

                // 連續3個奇數
                if(right - left + 1 == 3)
                {
                    // 位置
                    //Console.WriteLine("left: " + left + ", right: " + right);
                    // 數值
                    Console.WriteLine("Method2: [" + arr[left] + ", " + arr[left + 1] + ", " + arr[right] + "]");
                    return true;
                }

                // 持續往右走
                right++;
            }

            return false;
        }
    }
}
