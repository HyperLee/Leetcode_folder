namespace leetcode_2037
{
    internal class Program
    {
        /// <summary>
        /// 2037. Minimum Number of Moves to Seat Everyone
        /// https://leetcode.com/problems/minimum-number-of-moves-to-seat-everyone/description/?envType=daily-question&envId=2024-06-13
        /// 2037. 使每位学生都有座位的最少移动次数
        /// https://leetcode.cn/problems/minimum-number-of-moves-to-seat-everyone/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] seats = { 3, 1, 5 };
            int[] students = { 2, 7, 4 };

            Console.WriteLine(MinMovesToSeat(seats, students));
            Console.ReadKey();
        }



        /// <summary>
        /// https://leetcode.cn/problems/minimum-number-of-moves-to-seat-everyone/solutions/2037615/shi-mei-wei-xue-sheng-du-you-zuo-wei-de-oll4i/
        /// https://leetcode.cn/problems/minimum-number-of-moves-to-seat-everyone/solutions/2625721/2037-shi-mei-wei-xue-sheng-du-you-zuo-we-g2ib/
        /// 
        /// </summary>
        /// <param name="seats"></param>
        /// <param name="students"></param>
        /// <returns></returns>
        public static int MinMovesToSeat(int[] seats, int[] students)
        {
            Array.Sort(seats);
            Array.Sort(students);

            int res = 0;
            for(int i = 0; i < seats.Length; i++)
            {
                res += Math.Abs(seats[i] - students[i]);
            }

            return res;
        }
    }
}
