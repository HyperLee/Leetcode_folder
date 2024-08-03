namespace leetcode_1460
{
    internal class Program
    {
        /// <summary>
        /// 1460. Make Two Arrays Equal by Reversing Subarrays
        /// https://leetcode.com/problems/make-two-arrays-equal-by-reversing-subarrays/description/?envType=daily-question&envId=2024-08-03
        /// 
        /// 1460. 通过翻转子数组使两个数组相等
        /// https://leetcode.cn/problems/make-two-arrays-equal-by-reversing-subarrays/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input11 = { 1, 2, 3, 4 };
            int[] input2 = { 2, 4, 1, 3 };

            Console.WriteLine("方法1: " + CanBeEqual(input11, input2));
            Console.WriteLine("方法2: " + CanBeEqual2(input11, input2));
            Console.ReadKey();
        }


        /// <summary>
        /// 題目說明
        /// 給兩個陣列 target 與 arr
        /// 可以無限制翻轉,只要讓兩個陣列相同即可
        /// 
        /// 排序方法
        /// 
        /// 實際上不需要翻轉
        /// 1. 先把兩陣列排序
        /// 2. 比對陣列資料是否相同
        /// 3. 上述兩步驟即可
        /// 
        /// 都不限制翻轉比對次數了, 直接對比資料就好
        /// </summary>
        /// <param name="target"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static bool CanBeEqual(int[] target, int[] arr)
        {
            Array.Sort(target);
            Array.Sort(arr);

            bool result = true;
            for(int i = 0; i < target.Length; i++)
            {
                // 比對兩個 陣列是否相同
                if (target[i] != arr[i])
                {
                    return false;
                }
            }


            return result;
        }


        /// <summary>
        /// Hash table 方法
        /// 
        /// 利用 Dictionary<> 去比對
        /// 
        /// 也可以使用兩個 dic 最後在去比對
        /// 但是這邊只使用一個
        /// 一開始新增資料
        /// 最後去比對資料
        /// 
        /// 時間複雜度: O(n)
        /// 空間複雜度: O(n)
        /// </summary>
        /// <param name="target"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static bool CanBeEqual2(int[] target, int[] arr)
        {
            Dictionary<int, int> dic1 = new Dictionary<int, int>();
            bool res = true;

            // 把 target 資料 放到 dic1 裡面
            foreach (var item in target)
            {
                if(!dic1.ContainsKey(item))
                {
                    dic1.Add(item, 1);
                }
                else
                {
                    dic1[item]++;
                }
            }


            // arr 與 dic1 比對
            // 資料與次數都要比對
            // 沒有資料 或是 次數不同 都是錯誤
            foreach(var item2 in arr)
            {
                if(!dic1.ContainsKey(item2))
                {
                    return false;
                }
                else
                {
                    dic1[item2]--;
                }

                if (dic1[item2] < 0)
                {
                    return false;
                }
            }

            return res;

        }

    }
}
