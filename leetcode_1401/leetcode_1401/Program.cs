namespace leetcode_1401;

class Program
{
    /// <summary>
    /// 1401. Circle and Rectangle Overlapping
    /// https://leetcode.com/problems/circle-and-rectangle-overlapping/description/
    ///
    /// English:
    /// You are given a circle represented as (radius, xCenter, yCenter) and an
    /// axis-aligned rectangle represented as (x1, y1, x2, y2), where (x1, y1)
    /// are the coordinates of the bottom-left corner, and (x2, y2) are the
    /// coordinates of the top-right corner of the rectangle.
    ///
    /// Return true if the circle and rectangle are overlapped otherwise return
    /// false. In other words, check if there is any point (xi, yi) that belongs
    /// to the circle and the rectangle at the same time.
    ///
    /// 繁體中文：
    /// 給定一個由 (radius, xCenter, yCenter) 表示的圓，以及一個由
    /// (x1, y1, x2, y2) 表示的與座標軸對齊的矩形，其中 (x1, y1) 是矩形
    /// 左下角的座標，而 (x2, y2) 是矩形右上角的座標。
    ///
    /// 如果圓與矩形重疊，請回傳 true；否則回傳 false。換句話說，請檢查
    /// 是否存在任意一個同時屬於圓與矩形的點 (xi, yi)。
    ///
    /// https://leetcode.cn/problems/circle-and-rectangle-overlapping/description/
    ///
    /// </summary>
    /// <remarks>
    /// 主控台進入點使用固定案例驗證兩種解法，並逐筆輸出預期值、實際值與
    /// PASS 或 FAIL。若任何案例失敗，程式會以非零狀態碼結束。
    /// </remarks>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        Program solution = new();
        (string Name, int Radius, int XCenter, int YCenter, int X1, int Y1, int X2, int Y2, bool Expected)[] testCases =
        {
            ("官方範例 1：右側邊界相交", 1, 0, 0, 1, -1, 3, 1, true),
            ("官方範例 2：垂直距離超過半徑", 1, 1, 1, 1, -3, 2, -1, false),
            ("官方範例 3：矩形角落相交", 1, 0, 0, -1, 0, 0, 1, true),
            ("圓心位於矩形內", 2, 1, 1, 0, 0, 2, 2, true),
            ("上側邊界剛好相切", 1, 1, 3, 0, 0, 2, 2, true),
            ("右下角剛好落在半徑內", 3, 12, -2, 0, 0, 10, 10, true),
            ("左側完全分離", 1, -2, 5, 0, 0, 10, 10, false),
            ("右側邊界剛好相切", 1, 11, 5, 0, 0, 10, 10, true),
            ("上側完全分離", 1, 5, 12, 0, 0, 10, 10, false),
            ("負座標矩形包含圓心", 5, -5, -5, -8, -8, -2, -2, true)
        };

        int passed = 0;
        int total = 0;
        int caseNumber = 1;

        foreach ((string Name, int Radius, int XCenter, int YCenter, int X1, int Y1, int X2, int Y2, bool Expected) testCase in testCases)
        {
            bool actual1 = solution.CheckOverlap(
                testCase.Radius,
                testCase.XCenter,
                testCase.YCenter,
                testCase.X1,
                testCase.Y1,
                testCase.X2,
                testCase.Y2);
            bool firstPass = actual1 == testCase.Expected;
            Console.WriteLine($"Case {caseNumber:D2} - {testCase.Name}");
            Console.WriteLine($"  CheckOverlap  Expected: {testCase.Expected}, Actual: {actual1}, {(firstPass ? "PASS" : "FAIL")}");
            passed += firstPass ? 1 : 0;
            total++;

            bool actual2 = solution.CheckOverlap2(
                testCase.Radius,
                testCase.XCenter,
                testCase.YCenter,
                testCase.X1,
                testCase.Y1,
                testCase.X2,
                testCase.Y2);
            bool secondPass = actual2 == testCase.Expected;
            Console.WriteLine($"  CheckOverlap2 Expected: {testCase.Expected}, Actual: {actual2}, {(secondPass ? "PASS" : "FAIL")}");
            passed += secondPass ? 1 : 0;
            total++;

            caseNumber++;
        }

        Console.WriteLine($"Summary: {passed}/{total} checks passed.");
        Environment.ExitCode = passed == total ? 0 : 1;
    }

    /// <summary>
    /// 判斷圓與軸對齊矩形是否有共同點。方法一依照圓心位於矩形內部、
    /// 四個方向或四個角落分區討論；邊界相切也算重疊，回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="radius">圓的半徑，限制為正整數。</param>
    /// <param name="xCenter">圓心的 x 座標。</param>
    /// <param name="yCenter">圓心的 y 座標。</param>
    /// <param name="x1">矩形左下角的 x 座標。</param>
    /// <param name="y1">矩形左下角的 y 座標。</param>
    /// <param name="x2">矩形右上角的 x 座標。</param>
    /// <param name="y2">矩形右上角的 y 座標。</param>
    /// <returns>若圓與矩形重疊或相切則回傳 true，否則回傳 false。</returns>
    public bool CheckOverlap(int radius, int xCenter, int yCenter, int x1, int y1, int x2, int y2)
    {
        long radiusSquared = (long)radius * radius;

        // 圓心在矩形內部時，圓心本身就是兩者的共同點。
        if (x1 <= xCenter && xCenter <= x2 && y1 <= yCenter && yCenter <= y2)
        {
            return true;
        }

        // 圓心在矩形上下方且水平投影相交時，只需檢查到最近水平邊的距離。
        if (x1 <= xCenter && xCenter <= x2 && y2 <= yCenter && yCenter <= y2 + radius)
        {
            return true;
        }
        if (x1 <= xCenter && xCenter <= x2 && y1 - radius <= yCenter && yCenter <= y1)
        {
            return true;
        }

        // 圓心在矩形左右方且垂直投影相交時，同理只需檢查最近垂直邊。
        if (x1 - radius <= xCenter && xCenter <= x1 && y1 <= yCenter && yCenter <= y2)
        {
            return true;
        }
        if (x2 <= xCenter && xCenter <= x2 + radius && y1 <= yCenter && yCenter <= y2)
        {
            return true;
        }

        // 圓心落在矩形角落對角區域時，改用圓心到角落的平方距離判斷。
        if (Distance(xCenter, yCenter, x1, y2) <= radiusSquared)
        {
            return true;
        }
        if (Distance(xCenter, yCenter, x1, y1) <= radiusSquared)
        {
            return true;
        }
        if (Distance(xCenter, yCenter, x2, y2) <= radiusSquared)
        {
            return true;
        }
        if (Distance(xCenter, yCenter, x2, y1) <= radiusSquared)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 計算兩個整數座標點之間的歐幾里得距離平方，避免不必要的平方根與
    /// 浮點數誤差；輸入座標差值與平方結果以 long 保存。
    /// </summary>
    /// <param name="ux">第一個點的 x 座標。</param>
    /// <param name="uy">第一個點的 y 座標。</param>
    /// <param name="vx">第二個點的 x 座標。</param>
    /// <param name="vy">第二個點的 y 座標。</param>
    /// <returns>兩點距離的平方。</returns>
    public long Distance(int ux, int uy, int vx, int vy)
    {
        long deltaX = (long)ux - vx;
        long deltaY = (long)uy - vy;
        return deltaX * deltaX + deltaY * deltaY;
    }

    /// <summary>
    /// 判斷圓與軸對齊矩形是否有共同點。方法二分別計算圓心到矩形在
    /// x 軸與 y 軸投影的最短距離，將兩者平方和與半徑平方比較；相切時回傳 true。
    /// </summary>
    /// <param name="radius">圓的半徑，限制為正整數。</param>
    /// <param name="xCenter">圓心的 x 座標。</param>
    /// <param name="yCenter">圓心的 y 座標。</param>
    /// <param name="x1">矩形左下角的 x 座標。</param>
    /// <param name="y1">矩形左下角的 y 座標。</param>
    /// <param name="x2">矩形右上角的 x 座標。</param>
    /// <param name="y2">矩形右上角的 y 座標。</param>
    /// <returns>若圓與矩形重疊或相切則回傳 true，否則回傳 false。</returns>
    public bool CheckOverlap2(int radius, int xCenter, int yCenter, int x1, int y1, int x2, int y2)
    {
        long distanceSquared = 0;

        // 圓心在投影區間內時，該軸對最短距離的貢獻為零；在外部才計算到最近端點的距離。
        if (xCenter < x1)
        {
            long deltaX = (long)x1 - xCenter;
            distanceSquared += deltaX * deltaX;
        }
        else if (xCenter > x2)
        {
            long deltaX = (long)xCenter - x2;
            distanceSquared += deltaX * deltaX;
        }

        if (yCenter < y1)
        {
            long deltaY = (long)y1 - yCenter;
            distanceSquared += deltaY * deltaY;
        }
        else if (yCenter > y2)
        {
            long deltaY = (long)yCenter - y2;
            distanceSquared += deltaY * deltaY;
        }

        long radiusSquared = (long)radius * radius;
        return distanceSquared <= radiusSquared;
    }

}