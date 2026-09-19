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
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一：分区域讨论
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="xCenter"></param>
    /// <param name="yCenter"></param>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <returns></returns>
    public bool CheckOverlap(int radius, int xCenter, int yCenter, int x1, int y1, int x2, int y2) {
        /* 圆心在矩形内部 */
        if (x1 <= xCenter && xCenter <= x2 && y1 <= yCenter && yCenter <= y2) {
            return true;
        }
        /* 圆心在矩形上部 */
        if (x1 <= xCenter && xCenter <= x2 && y2 <= yCenter && yCenter <= y2 + radius) {
            return true;
        }
        /* 圆心在矩形下部 */
        if (x1 <= xCenter && xCenter <= x2 && y1 - radius <= yCenter && yCenter <= y1) {
            return true;
        }
         /* 圆心在矩形左部 */
        if (x1 - radius <= xCenter && xCenter <= x1 && y1 <= yCenter && yCenter <= y2) {
            return true;
        }
         /* 圆心在矩形右部 */
        if (x2 <= xCenter && xCenter <= x2 + radius && y1 <= yCenter && yCenter <= y2) {
            return true;
        }
        /* 矩形左上角 */
        if (Distance(xCenter, yCenter, x1, y2) <= radius * radius)  {
            return true;
        }
        /* 矩形左下角 */
        if (Distance(xCenter, yCenter, x1, y1) <= radius * radius) {
            return true;
        }
        /* 矩形右上角 */
        if (Distance(xCenter, yCenter, x2, y2) <= radius * radius) {
            return true;
        }
        /* 矩形右下角 */
        if (Distance(xCenter, yCenter, x1, y2) <= radius * radius) {
            return true;
        }
        /* 无交点 */
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ux"></param>
    /// <param name="uy"></param>
    /// <param name="vx"></param>
    /// <param name="vy"></param>
    /// <returns></returns>
    public long Distance(int ux, int uy, int vx, int vy) {
        return (long)Math.Pow(ux - vx, 2) + (long)Math.Pow(uy - vy, 2);
    }

    /// <summary>
    /// 方法二：求圆心到矩形区域的最短距离
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="xCenter"></param>
    /// <param name="yCenter"></param>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <returns></returns>
    public bool CheckOverlap2(int radius, int xCenter, int yCenter, int x1, int y1, int x2, int y2)
    {
        double dist = 0;
        if (xCenter < x1 || xCenter > x2) {
            dist += Math.Min(Math.Pow(x1 - xCenter, 2), Math.Pow(x2 - xCenter, 2));
        }
        if (yCenter < y1 || yCenter > y2) {
            dist += Math.Min(Math.Pow(y1 - yCenter, 2), Math.Pow(y2 - yCenter, 2));
        }
        return dist <= radius * radius;     
    }

}