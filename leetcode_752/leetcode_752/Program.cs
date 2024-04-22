namespace leetcode_752
{
    internal class Program
    {
        /// <summary>
        /// 752. Open the Lock
        /// https://leetcode.com/problems/open-the-lock/description/?envType=daily-question&envId=2024-04-22
        /// 752. 打开转盘锁
        /// https://leetcode.cn/problems/open-the-lock/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] deadends = { "8888" };
            string target = "0009";

            Console.WriteLine(OpenLock(deadends, target));
            Console.ReadKey();
        }



        /// <summary>
        /// ref:
        /// 1. https://leetcode.cn/problems/open-the-lock/solutions/843687/da-kai-zhuan-pan-suo-by-leetcode-solutio-l0xo/
        /// 2. https://leetcode.cn/problems/open-the-lock/solutions/843986/gong-shui-san-xie-yi-ti-shuang-jie-shuan-wyr9/
        /// 3. https://leetcode.cn/problems/open-the-lock/solutions/1871305/by-stormsunshine-t4j1/
        /// 
        /// 
        /// 先說結論, 題目我就看不是很懂
        /// 應該是要旋轉出解答, 但是要最小次數
        /// 然後不能踩到dead數字, 否則會卡住 不能旋轉
        /// Queue 用法,不熟. 很少用
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.queue?view=net-7.0
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.generic.queue-1?view=net-8.0
        /// https://medium.com/@ehowming/%E8%A4%87%E7%BF%92%E6%95%B4%E7%90%86-%E5%9F%BA%E7%A4%8E%E8%B3%87%E6%96%99%E7%B5%90%E6%A7%8B-c%E8%AA%9E%E8%A8%80-%E4%BD%87%E5%88%97-queue-1e707b525d8f
        ///  Enqueue(data)：從佇列尾端存入資料
        ///  Dequeue()：從佇列前端移除資料
        ///  
        /// </summary>
        /// <param name="deadends"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int OpenLock(string[] deadends, string target)
        {
            // 如果target初始為 0000 ,直接回傳答案 0
            if("0000".Equals(target))
            {
                return 0;
            }

            // 儲存 deadends 數字
            ISet<string> dead = new HashSet<string>();
            foreach(string deadend in deadends)
            {
                dead.Add(deadend);
            }

            // 找不到答案 回傳 -1
            if(dead.Contains("0000"))
            {
                return -1;
            }

            // 旋转的次数为 step
            int step = 0;
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("0000");
            ISet<string> seen = new HashSet<string>();
            seen.Add("0000");
            
            while(queue.Count > 0) 
            {
                step++;
                int size = queue.Count;
                
                for(int i = 0; i < size; i++)
                {
                    // 设当前搜索到的数字为 status
                    string status = queue.Dequeue();
                    // 设其中的某个数字为 nextstatus
                    foreach (string nextstatus in Get(status))
                    {
                        // 沒有被找過
                        if(!seen.Contains(nextstatus) && !dead.Contains(nextstatus))
                        {
                            if(nextstatus.Equals(target))
                            {
                                // 找到 target 回傳次數
                                return step;
                            }
                            // 沒找到 加入 queue
                            queue.Enqueue(nextstatus);
                            seen.Add(nextstatus);
                        }
                    }
                }
            }

            return -1;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static char NumtPre(char x)
        {
            return x == '0' ? '9' : (char)(x - 1);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static char NumSucc(char x)
        {
            return x == '9' ? '0' : (char)(x + 1);
        }


        /// <summary>
        /// 枚举 status 通过一次旋转得到的数字
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static IList<string> Get(string status)
        {
            IList<string> ret = new List<string>();
            char[] array = status.ToCharArray();
            for(int i = 0; i < 4; i++)
            {
                char num = array[i];
                array[i] = NumtPre(num);
                ret.Add(new string(array));
                array[i] = NumSucc(num);
                ret.Add(new string(array));
                array[i] = num;
            }

            return ret;
        }

    }
}
