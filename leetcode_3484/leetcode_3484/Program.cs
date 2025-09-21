namespace leetcode_3484;

class Program
{
    /// <summary>
    /// 3484. Design Spreadsheet
    /// https://leetcode.com/problems/design-spreadsheet/description/?envType=daily-question&envId=2025-09-19
    /// 3484. 设计电子表格
    /// https://leetcode.cn/problems/design-spreadsheet/description/?envType=daily-question&envId=2025-09-19
    ///
    /// 題目（中文）:
    /// 一個試算表有 26 欄（標示從 'A' 到 'Z'）與給定的列數。每個儲存格可儲存 0 到 10^5 的整數。
    /// 實作 Spreadsheet 類別：
    /// - Spreadsheet(int rows): 建立包含 26 欄與指定列數的試算表，初始值為 0。
    /// - void SetCell(string cell, int value): 設定指定儲存格的值，儲存格格式如 "A1"。
    /// - void ResetCell(string cell): 將指定儲存格重設為 0。
    /// - int GetValue(string formula): 評估形式為 "=X+Y" 的公式，X 與 Y 為儲存格或非負整數，回傳結果。
    ///
    /// 注意：若引用未被 SetCell 的儲存格，其值視為 0。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 簡單示範與 smoke test
        var sheet = new Spreadsheet(5); // 5 rows, columns A-Z
        sheet.SetCell("A1", 5);
        sheet.SetCell("B2", 7);
        Console.WriteLine(sheet.GetValue("=A1+B2")); // 12
        sheet.ResetCell("A1");
        Console.WriteLine(sheet.GetValue("=A1+3")); // 3
    }

    /// <summary>
    /// Spreadsheet 類別實作
    /// - 使用固定大小的二維陣列儲存值：rows x 26
    /// - 提供 SetCell, ResetCell, GetValue
    /// </summary>
    public class Spreadsheet
    {
        private readonly int rows;
        private readonly int cols = 26;
        private readonly int[,] data;

        /// <summary>
        /// 建構子：建立 rows x 26 的表格，初始值為 0
        /// </summary>
        public Spreadsheet(int rows)
        {
            if (rows <= 0) throw new ArgumentException("rows must be positive", nameof(rows));
            this.rows = rows;
            data = new int[rows, cols];
        }

        /// <summary>
        /// 將儲存格設為指定值。cell 格式如 "A1"。
        /// </summary>
        public void SetCell(string cell, int value)
        {
            var (r, c) = ParseCell(cell);
            data[r, c] = value;
        }

        /// <summary>
        /// 重設儲存格為 0
        /// </summary>
        public void ResetCell(string cell)
        {
            var (r, c) = ParseCell(cell);
            data[r, c] = 0;
        }

        /// <summary>
        /// 評估公式 "=X+Y"，X 與 Y 為儲存格或非負整數
        /// </summary>
        public int GetValue(string formula)
        {
            if (string.IsNullOrEmpty(formula)) throw new ArgumentException("formula is null or empty", nameof(formula));
            formula = formula.Trim();
            if (!formula.StartsWith("=")) throw new ArgumentException("formula must start with '='", nameof(formula));
            var body = formula.Substring(1);
            var parts = body.Split('+');
            if (parts.Length != 2) throw new ArgumentException("formula must be in form '=X+Y'", nameof(formula));

            int left = EvalOperand(parts[0].Trim());
            int right = EvalOperand(parts[1].Trim());
            return left + right;
        }

        private int EvalOperand(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            // 如果是數字
            if (int.TryParse(s, out var val)) return val;
            // 否則視為儲存格引用
            var (r, c) = ParseCell(s);
            return data[r, c];
        }

        /// <summary>
        /// 解析儲存格字串如 "A1" 回傳 0-based (row, col)
        /// </summary>
        private (int row, int col) ParseCell(string cell)
        {
            if (string.IsNullOrEmpty(cell)) throw new ArgumentException("cell is null or empty", nameof(cell));
            cell = cell.Trim().ToUpperInvariant();
            // 只考慮單一字母 A-Z
            if (cell.Length < 2) throw new ArgumentException("invalid cell format", nameof(cell));
            char colChar = cell[0];
            if (colChar < 'A' || colChar > 'Z') throw new ArgumentException("column must be A-Z", nameof(cell));
            if (!int.TryParse(cell.Substring(1), out var row1)) throw new ArgumentException("invalid row number", nameof(cell));
            if (row1 < 1 || row1 > rows) throw new ArgumentOutOfRangeException(nameof(cell), $"row out of range: {row1}");
            int col = colChar - 'A';
            int row = row1 - 1; // 0-based
            return (row, col);
        }
    }
}
