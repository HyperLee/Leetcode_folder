namespace leetcode_1700
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1700. Number of Students Unable to Eat Lunch
        /// https://leetcode.com/problems/number-of-students-unable-to-eat-lunch/description/
        ///
        /// The cafeteria offers circular and square sandwiches, represented by 0 and 1. Students form a queue, each preferring
        /// one type. There are equally many sandwiches, placed in a stack. If the front student prefers the top sandwich, they
        /// take it and leave; otherwise they move to the queue's end. Continue until no queued student wants the top sandwich.
        /// Given students and sandwiches, where index 0 is the queue front and stack top, return how many students cannot eat.
        ///
        /// Example 1:
        /// Input: students = [1,1,0,0], sandwiches = [0,1,0,1]
        /// Output: 0
        /// Explanation: The queue changes [1,1,0,0] -&gt; [1,0,0,1] -&gt; [0,0,1,1]. A student takes 0, leaving
        /// students = [0,1,1], sandwiches = [1,0,1]. After further rotations and matches, both arrays become empty, so all eat.
        ///
        /// Example 2:
        /// Input: students = [1,1,1,0,0,1], sandwiches = [1,0,0,0,1,1]
        /// Output: 3
        ///
        /// Constraints:
        /// - 1 &lt;= students.length, sandwiches.length &lt;= 100
        /// - students.length == sandwiches.length
        /// - sandwiches[i] is 0 or 1.
        /// - students[i] is 0 or 1.
        /// </para>
        /// <para>
        /// 1700. 無法吃午餐的學生數量
        /// https://leetcode.cn/problems/number-of-students-unable-to-eat-lunch/description/
        ///
        /// 餐廳提供以 0、1 表示的圓形與方形三明治。學生排成佇列，每人偏好一種類型；三明治數量與學生相同，
        /// 並疊成堆疊。若隊首學生偏好頂端三明治，就取走並離開；否則移到隊尾。持續到佇列中無人想要頂端
        /// 三明治。給定 students 與 sandwiches，索引 0 分別是隊首與堆疊頂端，回傳無法用餐的學生數量。
        ///
        /// 範例 1：
        /// 輸入：students = [1,1,0,0]，sandwiches = [0,1,0,1]
        /// 輸出：0
        /// 解釋：佇列依序變成 [1,1,0,0] -&gt; [1,0,0,1] -&gt; [0,0,1,1]。一名學生取走 0 後，
        /// students = [0,1,1]、sandwiches = [1,0,1]。繼續輪轉與配對後兩個陣列皆為空，所有學生都能用餐。
        ///
        /// 範例 2：
        /// 輸入：students = [1,1,1,0,0,1]，sandwiches = [1,0,0,0,1,1]
        /// 輸出：3
        ///
        /// 限制條件：
        /// - 1 &lt;= students.length, sandwiches.length &lt;= 100
        /// - students.length == sandwiches.length
        /// - sandwiches[i] 為 0 或 1。
        /// - students[i] 為 0 或 1。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 執行六組固定案例，比較計數法與佇列模擬法，並驗證兩種解法皆不修改輸入陣列。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        private static void Main(string[] args)
        {
            int passedChecks = 0;
            int totalChecks = 0;

            AddResult(RunCase(
                "官方範例一",
                new[] { 1, 1, 0, 0 },
                new[] { 0, 1, 0, 1 },
                0));
            AddResult(RunCase(
                "官方範例二",
                new[] { 1, 1, 1, 0, 0, 1 },
                new[] { 1, 0, 0, 0, 1, 1 },
                3));
            AddResult(RunCase(
                "最小成功",
                new[] { 0 },
                new[] { 0 },
                0));
            AddResult(RunCase(
                "最小阻塞",
                new[] { 0 },
                new[] { 1 },
                1));
            AddResult(RunCase(
                "全部相同",
                new[] { 1, 1, 1 },
                new[] { 1, 1, 1 },
                0));
            AddResult(RunCase(
                "重複值且中途阻塞",
                new[] { 0, 1, 0, 1 },
                new[] { 1, 1, 1, 0 },
                2));

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
            Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;

            void AddResult((int Passed, int Total) result)
            {
                passedChecks += result.Passed;
                totalChecks += result.Total;
            }
        }

        /// <summary>
        /// 執行一組測試資料，分別驗證兩種主要解法的回傳值與輸入保持不變契約。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="students">只包含 0 或 1 的學生偏好陣列。</param>
        /// <param name="sandwiches">與學生人數等長、只包含 0 或 1 的三明治陣列。</param>
        /// <param name="expected">預期無法用餐的學生人數。</param>
        /// <returns>本案例通過的檢查數與總檢查數。</returns>
        private static (int Passed, int Total) RunCase(
            string name,
            int[] students,
            int[] sandwiches,
            int expected)
        {
            int[] countStudentsStudents = (int[])students.Clone();
            int[] countStudentsSandwiches = (int[])sandwiches.Clone();
            int countStudentsActual = CountStudents(countStudentsStudents, countStudentsSandwiches);
            bool countStudentsResultPassed = countStudentsActual == expected;
            bool countStudentsInputPreserved =
                countStudentsStudents.SequenceEqual(students)
                && countStudentsSandwiches.SequenceEqual(sandwiches);

            int[] countStudents2Students = (int[])students.Clone();
            int[] countStudents2Sandwiches = (int[])sandwiches.Clone();
            int countStudents2Actual = CountStudents2(countStudents2Students, countStudents2Sandwiches);
            bool countStudents2ResultPassed = countStudents2Actual == expected;
            bool countStudents2InputPreserved =
                countStudents2Students.SequenceEqual(students)
                && countStudents2Sandwiches.SequenceEqual(sandwiches);

            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Students: {FormatArray(students)}");
            Console.WriteLine($"Sandwiches: {FormatArray(sandwiches)}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine(
                $"CountStudents  Actual: {countStudentsActual} | Input preserved: {countStudentsInputPreserved} | "
                + (countStudentsResultPassed && countStudentsInputPreserved ? "PASS" : "FAIL"));
            Console.WriteLine(
                $"CountStudents2 Actual: {countStudents2Actual} | Input preserved: {countStudents2InputPreserved} | "
                + (countStudents2ResultPassed && countStudents2InputPreserved ? "PASS" : "FAIL"));
            Console.WriteLine();

            int passedChecks = 0;
            passedChecks += countStudentsResultPassed ? 1 : 0;
            passedChecks += countStudentsInputPreserved ? 1 : 0;
            passedChecks += countStudents2ResultPassed ? 1 : 0;
            passedChecks += countStudents2InputPreserved ? 1 : 0;

            return (passedChecks, 4);
        }

        /// <summary>
        /// 將整數陣列格式化為容易閱讀且可重複比對的方括號表示法。
        /// </summary>
        /// <param name="values">要顯示的整數陣列。</param>
        /// <returns>以逗號與空格分隔元素的字串，例如 <c>[1, 0, 1]</c>。</returns>
        private static string FormatArray(int[] values)
        {
            return $"[{string.Join(", ", values)}]";
        }

        /// <summary>
        /// 計算最後無法取得午餐的學生人數。先統計偏好圓形與方形三明治的人數，再依固定的
        /// 三明治順序扣除對應人數；輸入陣列必須等長且元素只能是 0 或 1，方法不會修改輸入，
        /// 並回傳第一個無人偏好的三明治出現時仍留在隊伍中的學生數量。
        /// </summary>
        /// <remarks>
        /// 學生可以持續移到隊尾，因此只要仍有人偏好目前的三明治，就一定能把該學生移到隊首。
        /// 時間複雜度為 O(n)，額外空間複雜度為 O(1)。
        /// </remarks>
        /// <param name="students">學生的初始偏好；0 代表圓形，1 代表方形。</param>
        /// <param name="sandwiches">由索引 0 開始取用的三明治種類順序。</param>
        /// <returns>無法取得午餐的學生人數。</returns>
        public static int CountStudents(int[] students, int[] sandwiches)
        {
            int remainingSquarePreferences = students.Sum();
            int remainingCircularPreferences = students.Length - remainingSquarePreferences;

            // 學生可以任意輪轉到隊尾，所以能否繼續只取決於是否還有人偏好目前的三明治。
            foreach (int sandwich in sandwiches)
            {
                if (sandwich == 0)
                {
                    if (remainingCircularPreferences == 0)
                    {
                        break;
                    }

                    remainingCircularPreferences--;
                }
                else
                {
                    if (remainingSquarePreferences == 0)
                    {
                        break;
                    }

                    remainingSquarePreferences--;
                }
            }

            return remainingCircularPreferences + remainingSquarePreferences;
        }

        /// <summary>
        /// 以佇列逐步模擬學生取餐，計算最後無法取得午餐的人數。輸入陣列必須等長且元素只能
        /// 是 0 或 1；每位不偏好頂端三明治的學生會移到隊尾，方法不會修改輸入，並回傳連續
        /// 一整輪都無人取餐時仍在佇列中的學生數量。
        /// </summary>
        /// <remarks>
        /// 最壞情況下，每取走一份三明治前都可能輪轉整個佇列，因此時間複雜度為 O(n²)，
        /// 佇列所需的額外空間複雜度為 O(n)。
        /// </remarks>
        /// <param name="students">學生的初始偏好；0 代表圓形，1 代表方形。</param>
        /// <param name="sandwiches">由索引 0 開始取用的三明治種類順序。</param>
        /// <returns>無法取得午餐的學生人數。</returns>
        public static int CountStudents2(int[] students, int[] sandwiches)
        {
            Queue<int> waitingStudents = new(students);
            int sandwichIndex = 0;
            int consecutiveRejections = 0;

            while (waitingStudents.Count > 0 && consecutiveRejections < waitingStudents.Count)
            {
                int preference = waitingStudents.Dequeue();

                if (preference == sandwiches[sandwichIndex])
                {
                    sandwichIndex++;
                    // 有人取餐代表佇列狀態已改變，必須重新觀察新一輪學生。
                    consecutiveRejections = 0;
                }
                else
                {
                    waitingStudents.Enqueue(preference);
                    consecutiveRejections++;
                }
            }

            // 連續拒絕人數等於佇列長度時，代表無人偏好目前頂端的三明治。
            return waitingStudents.Count;
        }
    }
}