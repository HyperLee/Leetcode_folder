namespace leetcode_1700
{
    internal class Program
    {
        /// <summary>
        /// 1700. Number of Students Unable to Eat Lunch
        /// https://leetcode.com/problems/number-of-students-unable-to-eat-lunch/description/?envType=daily-question&envId=2024-04-08
        /// 1700. 无法吃午餐的学生数量
        /// https://leetcode.cn/problems/number-of-students-unable-to-eat-lunch/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] students = { 1, 1, 1, 0, 0, 1 };
            int[] sandwiches = { 1, 0, 0, 0, 1, 1 };

            Console.WriteLine(CountStudents(students, sandwiches));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/number-of-students-unable-to-eat-lunch/solutions/1900373/wu-fa-chi-wu-can-de-xue-sheng-shu-liang-fv3f5/
        /// https://leetcode.cn/problems/number-of-students-unable-to-eat-lunch/solutions/1456925/by-stormsunshine-w6yn/
        /// https://leetcode.cn/problems/number-of-students-unable-to-eat-lunch/solutions/1903169/by-lcbin-u0ar/
        /// 
        /// 1.迴圈走一輪
        /// 2.三明治順序不能變動
        /// 3.學生喜歡吃的,離開stack.
        /// 4.學生不喜歡吃, 移動到對伍尾端
        /// 
        /// 學生順序可變動, 三明治不能
        /// 
        /// 方形: 1
        /// 圓形: 0
        /// </summary>
        /// <param name="students"></param>
        /// <param name="sandwiches"></param>
        /// <returns></returns>
        public static int CountStudents(int[] students, int[] sandwiches)
        {
            // 喜歡吃方形數量; 總和即可知道有多少個1
            int s1 = students.Sum();
            // 喜歡吃圓形數量; 長度扣除總和,即為剩餘0數量
            int s0 = students.Length - s1;

            for(int i = 0; i < sandwiches.Length; i++)
            {
                if (sandwiches[i] == 0 && s0 > 0)
                {
                    // 找到喜歡, 就離開對列
                    s0--;
                }
                else if (sandwiches[i] == 1 && s1 > 0)
                {
                    // 找到喜歡, 就離開對列
                    s1--;
                }
                else
                {
                    break;
                }
            }

            return s0 + s1;
        }
    }
}
