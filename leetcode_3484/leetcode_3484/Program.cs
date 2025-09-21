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
    /// 
    /// 解題思路與演算法分析：
    /// 1. 資料結構選擇：使用二維陣列 int[rows, 26] 儲存所有儲存格值
    ///    - 時間複雜度：SetCell/ResetCell/GetValue 都是 O(1)（不考慮字串解析）
    ///    - 空間複雜度：O(rows * 26) = O(rows)，因為列數是 26 的常數倍
    /// 
    /// 2. 儲存格地址解析：將 "A1" 格式轉換為陣列索引
    ///    - 欄位：'A'-'Z' 對應到 0-25
    ///    - 列號：1-based 輸入轉換為 0-based 陣列索引
    ///    - 錯誤處理：檢查格式正確性、範圍邊界
    /// 
    /// 3. 公式評估：解析 "=X+Y" 格式的簡單加法運算
    ///    - 分割字串取得左右運算元
    ///    - 判斷是數字常數或儲存格參考
    ///    - 未設定的儲存格自動回傳 0（陣列預設值）
    /// 
    /// 4. 設計優勢：
    ///    - 簡潔高效：直接陣列存取，無需額外查找
    ///    - 記憶體預分配：避免動態調整大小的開銷
    ///    - 類型安全：C# 強型別系統確保資料正確性
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // ===== 示範程式與測試案例 =====
        Console.WriteLine("=== LeetCode 3484: Design Spreadsheet 解法示範 ===\n");
        
        // 建立 5 列 × 26 欄的試算表
        var sheet = new Spreadsheet(5);
        Console.WriteLine("建立 5×26 試算表完成");
        
        // 測試案例 1：基本設定與加法
        sheet.SetCell("A1", 5);
        sheet.SetCell("B2", 7);
        Console.WriteLine($"設定 A1 = 5, B2 = 7");
        Console.WriteLine($"計算 =A1+B2 = {sheet.GetValue("=A1+B2")}"); // 應為 12
        
        // 測試案例 2：重設儲存格與混合運算
        sheet.ResetCell("A1");
        Console.WriteLine($"重設 A1 後，計算 =A1+3 = {sheet.GetValue("=A1+3")}"); // 應為 3
        
        // 測試案例 3：未設定儲存格的預設值
        Console.WriteLine($"未設定的 C3，計算 =C3+10 = {sheet.GetValue("=C3+10")}"); // 應為 10
        
        // 測試案例 4：數字與數字相加
        Console.WriteLine($"常數計算 =15+25 = {sheet.GetValue("=15+25")}"); // 應為 40
        
        Console.WriteLine("\n=== 所有測試案例執行完成 ===");
        
        // ===== 方法二：哈希表實作測試 =====
        Console.WriteLine("\n=== 方法二：哈希表實作測試 ===");
        var sheet2 = new SpreadsheetHashMap(10);
        sheet2.SetCell("A1", 15);
        sheet2.SetCell("B3", 25);
        Console.WriteLine($"方法二：設定 A1 = 15, B3 = 25");
        Console.WriteLine($"方法二：計算 =A1+B3 = {sheet2.GetValue("=A1+B3")}"); // 應為 40
        sheet2.ResetCell("A1");
        Console.WriteLine($"方法二：重設 A1 後，計算 =A1+10 = {sheet2.GetValue("=A1+10")}"); // 應為 10
        Console.WriteLine("=== 方法二測試完成 ===");
    }

    /// <summary>
    /// 方法一：二維陣列實作 - LeetCode 3484 解法
    /// 
    /// 核心設計理念：
    /// 1. 使用二維陣列 data[rows, 26] 作為主要儲存結構
    /// 2. 所有操作都在 O(1) 時間內完成（除字串解析外）
    /// 3. 利用 C# 陣列的預設初始化（0）來滿足題目需求
    /// 
    /// 資料結構說明：
    /// - rows: 試算表的列數（1 到 rows）
    /// - cols: 固定為 26，對應 A-Z 欄位
    /// - data: 二維陣列，data[i,j] 代表第 i+1 列、第 j+'A' 欄的值
    /// </summary>
    public class Spreadsheet
    {
        private readonly int rows;       // 試算表列數
        private readonly int cols = 26;  // 固定 26 欄 (A-Z)
        private readonly int[,] data;    // 主要儲存陣列 [行][列]

        /// <summary>
        /// 建構子：初始化指定大小的試算表
        /// 
        /// 演算法：
        /// 1. 驗證輸入參數（rows > 0）
        /// 2. 建立 rows × 26 的二維陣列
        /// 3. C# 陣列自動初始化所有元素為 0
        /// 
        /// 時間複雜度：O(rows * 26) = O(rows)
        /// 空間複雜度：O(rows * 26) = O(rows)
        /// </summary>
        /// <param name="rows">試算表列數，必須 > 0</param>
        /// <exception cref="ArgumentException">當 rows <= 0 時拋出</exception>
        public Spreadsheet(int rows)
        {
            if (rows <= 0)
            {
                throw new ArgumentException("rows must be positive", nameof(rows));
            }
            
            this.rows = rows;
            data = new int[rows, cols]; // 自動初始化為 0
        }

        /// <summary>
        /// 設定指定儲存格的值
        /// 
        /// 演算法：
        /// 1. 解析儲存格地址（如 "A1" → (0,0)）
        /// 2. 直接在陣列中設定值
        /// 
        /// 時間複雜度：O(1)（不考慮字串解析）
        /// 空間複雜度：O(1)
        /// </summary>
        /// <param name="cell">儲存格地址，格式如 "A1", "Z5"</param>
        /// <param name="value">要設定的值 (0 ≤ value ≤ 10^5)</param>
        public void SetCell(string cell, int value)
        {
            var (r, c) = ParseCell(cell);
            data[r, c] = value;
        }

        /// <summary>
        /// 重設指定儲存格為 0
        /// 
        /// 演算法：本質上就是 SetCell(cell, 0) 的特殊版本
        /// 
        /// 時間複雜度：O(1)
        /// 空間複雜度：O(1)
        /// </summary>
        /// <param name="cell">要重設的儲存格地址</param>
        public void ResetCell(string cell)
        {
            var (r, c) = ParseCell(cell);
            data[r, c] = 0;
        }

        /// <summary>
        /// 評估公式並回傳計算結果
        /// 
        /// 演算法：
        /// 1. 驗證公式格式（必須以 '=' 開頭）
        /// 2. 移除 '=' 並以 '+' 分割成兩個運算元
        /// 3. 分別評估左右運算元（數字或儲存格參考）
        /// 4. 回傳兩個運算元的和
        /// 
        /// 支援的運算元類型：
        /// - 非負整數：如 "5", "100"
        /// - 儲存格參考：如 "A1", "Z26"
        /// 
        /// 時間複雜度：O(1)（字串操作為常數時間）
        /// 空間複雜度：O(1)
        /// </summary>
        /// <param name="formula">公式字串，格式為 "=X+Y"</param>
        /// <returns>計算結果</returns>
        /// <exception cref="ArgumentException">當公式格式不正確時拋出</exception>
        public int GetValue(string formula)
        {
            // 輸入驗證
            if (string.IsNullOrEmpty(formula))
            {
                throw new ArgumentException("formula is null or empty", nameof(formula));
            }
            
            formula = formula.Trim();
            if (!formula.StartsWith("="))
            {
                throw new ArgumentException("formula must start with '='", nameof(formula));
            }

            // 解析公式主體（移除 '=' 前綴）
            var body = formula.Substring(1);
            var parts = body.Split('+');
            if (parts.Length != 2)
            {   
                throw new ArgumentException("formula must be in form '=X+Y'", nameof(formula));
            }

            // 評估左右運算元並計算和
            int left = EvalOperand(parts[0].Trim());
            int right = EvalOperand(parts[1].Trim());
            return left + right;
        }

        /// <summary>
        /// 評估單一運算元（數字或儲存格參考）
        /// 
        /// 演算法：
        /// 1. 嘗試解析為整數
        /// 2. 如果失敗，視為儲存格參考並取得其值
        /// 3. 未設定的儲存格回傳 0（陣列預設值）
        /// 
        /// 時間複雜度：O(1)
        /// 空間複雜度：O(1)
        /// </summary>
        /// <param name="s">運算元字串</param>
        /// <returns>運算元的數值</returns>
        private int EvalOperand(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            
            // 嘗試解析為數字常數
            if (int.TryParse(s, out var val))
            {
                return val;
            }

            // 否則視為儲存格參考，取得儲存格值
            var (r, c) = ParseCell(s);
            return data[r, c]; // 未設定的儲存格自動為 0
        }

        /// <summary>
        /// 解析儲存格地址字串為陣列索引
        /// 
        /// 演算法詳解：
        /// 1. 輸入格式驗證：非空、長度 ≥ 2
        /// 2. 欄位解析：第一個字元必須是 A-Z
        /// 3. 列號解析：剩餘字元必須是有效正整數
        /// 4. 範圍檢查：列號必須在 1 到 rows 之間
        /// 5. 座標轉換：
        ///    - 欄位：'A' → 0, 'B' → 1, ..., 'Z' → 25
        ///    - 列號：1-based → 0-based (減 1)
        /// 
        /// 範例轉換：
        /// - "A1" → (row=0, col=0)
        /// - "B5" → (row=4, col=1)
        /// - "Z26" → (row=25, col=25) [如果 rows ≥ 26]
        /// 
        /// 注意:
        /// 輸入資料是 1-based 的列號（如 "A1" 是第一列），但是這陣列是 0-based 的索引。
        /// 所以要轉換時要將列號減 1。
        /// row 是使用者輸入的列號，col 是字母對應的欄位索引。
        /// 
        /// 時間複雜度：O(k)，其中 k 是列號的位數（通常是常數）
        /// 空間複雜度：O(1)
        /// </summary>
        /// <param name="cell">儲存格地址字串，如 "A1", "B10", "Z999"</param>
        /// <returns>0-based 的 (row, col) 索引對</returns>
        /// <exception cref="ArgumentException">當格式不正確時拋出</exception>
        /// <exception cref="ArgumentOutOfRangeException">當列號超出範圍時拋出</exception>
        private (int row, int col) ParseCell(string cell)
        {
            // 基本輸入驗證
            if (string.IsNullOrEmpty(cell))
            {
                throw new ArgumentException("cell is null or empty", nameof(cell));
            }

            cell = cell.Trim().ToUpperInvariant(); // 正規化為大寫

            // 格式檢查：至少要有一個字母 + 一個數字
            if (cell.Length < 2)
            {
                throw new ArgumentException("invalid cell format", nameof(cell));
            }

            // 解析欄位字母（第一個字元）
            char colChar = cell[0];
            if (colChar < 'A' || colChar > 'Z')
            {
                throw new ArgumentException("column must be A-Z", nameof(cell));
            }

            // 解析列號（剩餘字元）
            if (!int.TryParse(cell.Substring(1), out var row1))
            {
                throw new ArgumentException("invalid row number", nameof(cell));
            }

            // 範圍檢查：列號必須在有效範圍內
            if (row1 < 1 || row1 > rows)
            {
                throw new ArgumentOutOfRangeException(nameof(cell),
                    $"row out of range: {row1} (valid range: 1-{rows})");
            }

            // 座標轉換：1-based → 0-based
            int col = colChar - 'A';  // A=0, B=1, ..., Z=25
            int row = row1 - 1;       // 1-based → 0-based
            return (row, col);
        }
    }

    /// <summary>
    /// 方法二：哈希表實作 - LeetCode 3484 解法
    /// 
    /// 核心設計理念：
    /// 1. 使用 Dictionary&lt;string, int&gt; 作為主要儲存結構
    /// 2. 直接以儲存格地址（如 "A1"）作為 key，值作為 value
    /// 3. 利用 Dictionary 的 GetValueOrDefault 方法處理未設定儲存格
    /// 
    /// 優勢：
    /// - 節省記憶體：只儲存有值的儲存格
    /// - 彈性：不受列數限制，可動態擴展
    /// - 簡潔：無需複雜的座標轉換
    /// 
    /// 適用場景：
    /// - 稀疏資料（大部分儲存格為空）
    /// - 不確定試算表大小的情況
    /// - 需要動態擴展的應用
    /// </summary>
    public class SpreadsheetHashMap
    {
        private readonly Dictionary<string, int> cellValues = new Dictionary<string, int>();

        /// <summary>
        /// 建構子：初始化哈希表
        /// 
        /// 演算法：
        /// 1. 初始化空的 Dictionary
        /// 2. 不需預分配記憶體，動態增長
        /// 
        /// 時間複雜度：O(1)
        /// 空間複雜度：O(1) - 初始狀態
        /// </summary>
        /// <param name="rows">試算表列數（在此實作中僅作為參數，實際不限制大小）</param>
        public SpreadsheetHashMap(int rows) 
        {
            // 哈希表實作不需要預先知道大小，此參數保持介面一致性
        }

        /// <summary>
        /// 設定指定儲存格的值
        /// 
        /// 演算法：
        /// 1. 直接將 cell 作為 key，value 作為值存入 Dictionary
        /// 2. 如果 key 已存在，會覆蓋原值
        /// 
        /// 時間複雜度：O(1) 平均情況
        /// 空間複雜度：O(1)
        /// </summary>
        /// <param name="cell">儲存格地址，格式如 "A1", "Z5"</param>
        /// <param name="value">要設定的值</param>
        public void SetCell(string cell, int value) 
        {
            cellValues[cell] = value;
        }

        /// <summary>
        /// 重設指定儲存格為 0
        /// 
        /// 演算法：
        /// 1. 從 Dictionary 中移除對應的 key
        /// 2. 後續查詢時會回傳預設值 0
        /// 
        /// 時間複雜度：O(1) 平均情況
        /// 空間複雜度：O(1)
        /// </summary>
        /// <param name="cell">要重設的儲存格地址</param>
        public void ResetCell(string cell) 
        {
            cellValues.Remove(cell);
        }

        /// <summary>
        /// 評估公式並回傳計算結果
        /// 
        /// 演算法：
        /// 1. 尋找 '+' 的位置來分割公式
        /// 2. 提取左右兩個運算元
        /// 3. 判斷運算元類型：
        ///    - 首字母為字母：儲存格參考，從 Dictionary 取值
        ///    - 首字母為數字：數字常數，直接解析
        /// 4. 回傳兩個運算元的和
        /// 
        /// 時間複雜度：O(1) 平均情況
        /// 空間複雜度：O(1)
        /// </summary>
        /// <param name="formula">公式字串，格式為 "=X+Y"</param>
        /// <returns>計算結果</returns>
        public int GetValue(string formula) 
        {
            // 尋找 '+' 符號的位置
            int i = formula.IndexOf('+');
            
            // 提取左右運算元（移除前綴 '='）
            string cell1 = formula.Substring(1, i - 1);
            string cell2 = formula.Substring(i + 1);
            
            // 評估左運算元
            int val1 = char.IsLetter(cell1[0]) 
                ? cellValues.GetValueOrDefault(cell1) // 儲存格參考
                : int.Parse(cell1); // 數字常數
            
            // 評估右運算元
            int val2 = char.IsLetter(cell2[0]) 
                ? cellValues.GetValueOrDefault(cell2) // 儲存格參考
                : int.Parse(cell2); // 數字常數
            
            return val1 + val2;
        }
    }
}
