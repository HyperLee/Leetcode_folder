namespace leetcode_621
{
    internal class Program
    {
        /// <summary>
        /// 621. Task Scheduler
        /// https://leetcode.com/problems/task-scheduler/description/?envType=daily-question&envId=2024-03-19
        /// 621. 任务调度器
        /// https://leetcode.cn/problems/task-scheduler/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            char[] chars = {'A', 'A', 'A', 'B', 'B', 'B' };
            int n = 2;
            Console.Write(LeastInterval(chars, n));
            Console.ReadKey();
        }



        /// <summary>
        /// 參考:
        /// https://leetcode.cn/problems/task-scheduler/solutions/2020840/by-stormsunshine-hxv6/
        /// https://leetcode.cn/problems/task-scheduler/solutions/510292/c-jie-fa-by-jian-wei-z-km3w/
        /// https://leetcode.cn/problems/task-scheduler/solutions/509687/ren-wu-diao-du-qi-by-leetcode-solution-ur9w/
        /// 
        /// 須注意
        /// case 1:
        /// 任務種類較少, 要思考輪流插入任務需要的時間
        /// 即為公式計算 (n + 1) * (maxcount - 1) + maxtasks;
        /// 
        /// (n + 1) * (maxcount - 1) 可計算出不包含冷卻時間光char輪流插入所需耗時
        /// 再加上 maxtasks(可視為冷卻時間 or 任務種類次數) 即為總需要時間
        /// 
        /// case 2:
        /// 任務種類較多, 不太會出現冷卻時間可以插入
        /// 就要改計算任務次數
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int LeastInterval(char[] tasks, int n)
        {
            int maxcount = 0;
            int[] counts = new int[26];

            foreach (char c in tasks) 
            {
                // 統計每個char次數
                counts[c - 'A']++;
                // 找出char 最大出現次數
                maxcount = Math.Max(maxcount, counts[c - 'A']);
            }

            int maxtasks = 0;
            foreach (int count in counts)
            {
                if(count == maxcount)
                {
                    // 統計 出現最多次數之任務種類數
                    maxtasks++;
                }
            }

            int total = (n + 1) * (maxcount - 1) + maxtasks;

            return Math.Max(total, tasks.Length);
        }
    }
}
