namespace leetcode_657;

class Program
{
    /// <summary>
    /// 657. Robot Return to Origin
    /// https://leetcode.com/problems/robot-return-to-origin/description/?envType=daily-question&envId=2026-04-05
    /// 657. 机器人能否返回原点
    /// https://leetcode.cn/problems/robot-return-to-origin/description/?envType=daily-question&envId=2026-04-05
    /// <para>
    /// There is a robot starting at the position (0, 0), the origin, on a 2D plane.
    /// Given a sequence of its moves, judge if this robot ends up at (0, 0) after it completes its moves.
    /// You are given a string moves that represents the move sequence of the robot where moves[i] represents its ith move.
    /// Valid moves are 'R' (right), 'L' (left), 'U' (up), and 'D' (down).
    /// Return true if the robot returns to the origin after it finishes all of its moves, or false otherwise.
    /// Note: The way that the robot is "facing" is irrelevant. 'R' will always make the robot move to the right once,
    /// 'L' will always make it move left, etc. Also, assume that the magnitude of the robot's movement is the same for each move.
    /// </para>
    /// <para>
    /// 有一個機器人從二維平面的原點 (0, 0) 出發。
    /// 給定一段移動序列，判斷機器人在執行完所有移動後，是否回到原點 (0, 0)。
    /// 輸入字串 moves 代表機器人的移動序列，其中 moves[i] 表示第 i 次移動的方向。
    /// 合法的移動方向為 'R'（右）、'L'（左）、'U'（上）、'D'（下）。
    /// 若機器人完成所有移動後回到原點，則傳回 true，否則傳回 false。
    /// 注意：機器人的「朝向」不影響移動結果。'R' 永遠向右移動一格，'L' 永遠向左移動一格，以此類推。
    /// 同時假設每次移動的距離均相同。
    /// </para>
    /// </summary>
    /// <param name="args">命令列引數（未使用）。</param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1："UD" — 向上再向下，回到原點 → true
        Console.WriteLine($"Test 1 \"UD\"    : {solution.JudgeCircle("UD")}");     // Expected: True

        // 測試案例 2："LL" — 連續向左兩步，無法回到原點 → false
        Console.WriteLine($"Test 2 \"LL\"    : {solution.JudgeCircle("LL")}");     // Expected: False

        // 測試案例 3："UDLR" — 上下左右各一步，回到原點 → true
        Console.WriteLine($"Test 3 \"UDLR\" : {solution.JudgeCircle("UDLR")}");   // Expected: True

        // 測試案例 4："LDRRUUDD" — 組合移動，驗證最終座標是否為 (0,0) → false
        Console.WriteLine($"Test 4 \"LDRRUUDD\": {solution.JudgeCircle("LDRRUUDD")}"); // Expected: False
    }

    /// <summary>
    /// LeetCode 657 — Robot Return to Origin（機器人能否回到原點）。
    /// <para>
    /// 解題思路：模擬法。<br/>
    /// 以二維座標 (x, y) 追蹤機器人位置，初始為 (0, 0)。<br/>
    /// 依序執行每道指令，最後判斷是否回到原點 (0, 0)。
    /// </para>
    /// <para>
    /// 時間複雜度：O(n)，其中 n 為指令字串長度。<br/>
    /// 空間複雜度：O(1)，只使用常數額外空間。
    /// </para>
    /// <example>
    /// <code>
    /// var sol = new Program();
    /// sol.JudgeCircle("UD");   // true  — 上下各一步，回到原點
    /// sol.JudgeCircle("LL");   // false — 向左移動兩步，無法回到原點
    /// sol.JudgeCircle("UDLR"); // true  — 上下左右各一步，回到原點
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="moves">
    /// 由 'U'（上）、'D'（下）、'L'（左）、'R'（右）組成的指令字串。
    /// </param>
    /// <returns>
    /// 若機器人最終回到原點 (0, 0) 則傳回 <see langword="true"/>，否則傳回 <see langword="false"/>。
    /// </returns>
    public bool JudgeCircle(string moves)
    {
        // 使用 x、y 模擬機器人的二維座標，起始位置為原點 (0, 0)
        int x = 0;
        int y = 0;
        int length = moves.Length;

        for (int i = 0; i < length; i++)
        {
            char move = moves[i];

            // 依據指令更新座標：
            //   U（Up）   → y 減 1（向上）
            //   D（Down） → y 加 1（向下）
            //   L（Left） → x 減 1（向左）
            //   R（Right）→ x 加 1（向右）
            switch (move)
            {
                case 'U':
                    y--;
                    break;
                case 'D':
                    y++;
                    break;
                case 'L':
                    x--;
                    break;
                case 'R':
                    x++;
                    break;
            }
        }

        // 所有指令執行完畢後，若 x 與 y 皆為 0，代表機器人回到原點
        return x == 0 && y == 0;
    }
}
