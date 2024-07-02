namespace leetcode_350
{
    internal class Program
    {
        /// <summary>
        /// 350. Intersection of Two Arrays II
        /// https://leetcode.com/problems/intersection-of-two-arrays-ii/description/?envType=daily-question&envId=2024-07-02
        /// 350. 两个数组的交集 II
        /// https://leetcode.cn/problems/intersection-of-two-arrays-ii/description/
        /// 
        /// 本題目為 349. Intersection of Two Arrays
        /// 進階衍生題目
        /// 
        /// 本題目如果遇到重覆數字,需要輸出相同數量的數字
        /// 不能只輸出一個數字當作代表
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input1 = { 1, 2, 2, 1 };
            int[] input2 = { 2, 2 };

            var res = Intersect(input1, input2);

            foreach(var value in res)
            {
                Console.Write(value + ", ");
            }

            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/intersection-of-two-arrays-ii/solutions/327356/liang-ge-shu-zu-de-jiao-ji-ii-by-leetcode-solution/
        /// https://leetcode.cn/problems/intersection-of-two-arrays-ii/solutions/48971/jin-jie-san-wen-by-user5707f/
        /// https://leetcode.cn/problems/intersection-of-two-arrays-ii/solutions/683821/ha-xi-biao-liang-ge-shu-zu-de-jiao-ji-ii-fkwo/
        /// https://leetcode.cn/problems/intersection-of-two-arrays-ii/solutions/1773063/by-stormsunshine-373a/
        /// 
        /// 找出兩個陣列有交集的元素
        /// 但是需要小心
        /// 輸入兩陣列有可能 長度不同
        /// 所以判斷時候要小心
        /// 大的要包含小的才比較完整
        /// 相反的話可能會導致element遺漏
        /// 沒有被比較到
        /// 
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public static int[] Intersect(int[] nums1, int[] nums2)
        {
            int n1 = nums1.Length;
            int n2 = nums2.Length;

            if(n1 > n2)
            {
                return GetIntersection(nums2, nums1);
            }
            else
            {
                return GetIntersection(nums1, nums2);
            }
        }


        /// <summary>
        /// 輸入的兩個陣列, 有長度不一致問題
        /// 所以呼叫function時候 要先做判斷
        /// 
        /// 1. List 用來儲存結果資料
        /// 2. Dictionary 用來統計每個element以及個數
        /// 3. 先將小陣列資料儲存進Dictionary
        /// 4. 再將上述儲存資料, 拿去與長陣列對比,
        ///    查看是否有交集
        /// 5. 有交集,就把資料加入 List
        /// 6. 然後扣減Dictionary數量
        /// 7. 當Dictionary數量扣減至0, 需要移出Dictionary
        /// 8. 回傳結果要轉陣列格式
        /// 
        /// 為什麼需要使用 Dictionary
        /// 因為題目有說明, 取兩陣列的交集element
        /// element 數量需要一致, 不能只回傳一次
        /// 所以 需要使用 Dictionary 來統計 element 數量
        /// 6, 7步驟也是因此才產生的行為
        /// 
        /// </summary>
        /// <param name="nums">輸入短陣列</param>
        /// <param name="Lnums">輸入長陣列</param>
        /// <returns></returns>
        public static int[] GetIntersection(int[] Snums, int[] Lnums)
        {
            // 宣告List用來儲存結果, 回傳時候要轉陣列格式
            IList<int> res = new List<int>();
            // 暫存, 用來儲存element以及個數
            IDictionary<int, int> dic = new Dictionary<int, int>();

            // 統計短陣列內每個element和個數
            foreach (int num in Snums)
            {
                if(!dic.ContainsKey(num))
                {
                    dic.Add(num, 1);
                }
                else
                {
                    dic[num]++;
                }
            }

            // 去長陣列比較找出交集element
            foreach(int num in Lnums)
            {
                // 暫存已經存在該element
                if(dic.ContainsKey(num))
                {
                    // 加入結果
                    res.Add(num);
                    // 當加入結果, 就要扣減暫存數量
                    dic[num]--;

                    // 數量扣減至0, 就要移出暫存
                    if (dic[num] == 0)
                    {
                        dic.Remove(num);
                    }
                }
            }

            // 回傳陣列格式
            return res.ToArray();
        }
    }
}
