# LeetCode 3484: Design Spreadsheet

一個高效的試算表實作，支援儲存格操作與簡單公式計算。

## 問題描述

設計一個試算表類別，具有 26 欄（A-Z）和指定的列數。每個儲存格可以儲存 0 到 10^5 的整數值。

### 需要實作的方法

- `Spreadsheet(int rows)` - 初始化具有指定列數的試算表
- `void SetCell(string cell, int value)` - 設定儲存格值
- `void ResetCell(string cell)` - 重設儲存格為 0
- `int GetValue(string formula)` - 評估 "=X+Y" 格式的公式

## 解題思路

本專案提供了兩種不同的實作方法，各有其優勢和適用場景：

### 方法一：二維陣列實作

#### 核心設計理念

1. **資料結構選擇**：使用二維陣列 `int[rows, 26]` 作為主要儲存結構
2. **高效能設計**：所有核心操作都在 O(1) 時間內完成
3. **記憶體最佳化**：預分配固定大小的陣列，避免動態調整開銷

#### 方法一適用場景

- 固定大小的試算表
- 需要最佳效能的應用
- 密集型數據（大部分儲存格都有值）

### 方法二：哈希表實作

#### 設計理念

1. **資料結構選擇**：使用 `Dictionary<string, int>` 作為主要儲存結構
2. **記憶體效率**：只儲存有值的儲存格，節省記憶體空間
3. **彈性設計**：不受試算表大小限制，可動態擴展

#### 方法二適用場景

- 稀疏資料（大部分儲存格為空）
- 不確定試算表大小的情況
- 需要動態擴展的應用

### 演算法分析

#### 方法一（二維陣列）時間複雜度

- `SetCell()`: O(1) - 直接陣列存取
- `ResetCell()`: O(1) - 直接陣列存取  
- `GetValue()`: O(1) - 簡單字串解析 + 陣列存取
- `Spreadsheet()`: O(rows) - 陣列初始化

#### 方法一（二維陣列）空間複雜度

- 總體：O(rows × 26) = O(rows) - 二維陣列儲存

#### 方法二（哈希表）時間複雜度

- `SetCell()`: O(1) 平均情況 - Dictionary 插入
- `ResetCell()`: O(1) 平均情況 - Dictionary 刪除
- `GetValue()`: O(1) 平均情況 - Dictionary 查找
- `SpreadsheetHashMap()`: O(1) - 初始化空 Dictionary

#### 方法二（哈希表）空間複雜度

- 總體：O(k) - 其中 k 是實際設定值的儲存格數量

### 關鍵技術點

#### 1. 儲存格地址解析（方法一）

```csharp
// "A1" → (row=0, col=0)
// "B5" → (row=4, col=1) 
// "Z26" → (row=25, col=25)
private (int row, int col) ParseCell(string cell)
```

#### 2. 公式評估引擎（兩種方法通用）

```csharp
// 支援格式：=X+Y
// X, Y 可以是：數字常數 或 儲存格參考
public int GetValue(string formula)
```

#### 3. 不同的儲存策略

##### 方法一：預分配陣列

- 利用 C# 陣列的預設初始化（0）
- 未設定的儲存格自動回傳 0，無需額外邏輯

##### 方法二：哈希表動態儲存

- 使用 `GetValueOrDefault()` 處理未設定的儲存格
- 只佔用實際使用的記憶體空間

## 實作特色

### ✅ 方法一優勢（二維陣列）

- **最佳效能**：所有操作都是 O(1) 時間複雜度
- **記憶體友善**：固定大小預分配，無記憶體碎片
- **簡單直觀**：直接陣列索引，易於理解和除錯

### ✅ 方法二優勢（哈希表）

- **記憶體效率**：只儲存有值的儲存格，適合稀疏資料
- **動態擴展**：不受固定大小限制，可隨需求成長
- **實作簡潔**：無需複雜的座標轉換邏輯

### 🆚 兩種方法比較

| 特性 | 方法一（二維陣列） | 方法二（哈希表） |
|------|------------------|------------------|
| 時間複雜度 | O(1) 保證 | O(1) 平均 |
| 空間複雜度 | O(rows × 26) | O(實際使用的儲存格數) |
| 記憶體使用 | 固定預分配 | 按需分配 |
| 適用場景 | 密集資料、固定大小 | 稀疏資料、動態大小 |
| 實作複雜度 | 需要座標轉換 | 直接字串索引 |

### 🎯 通用優勢

- **類型安全**：利用 C# 強型別系統確保資料正確性
- **錯誤處理**：完整的輸入驗證與例外處理
- **可讀性**：詳細註解與清晰的方法命名

### 🎯 適用場景

- 小到中等規模的試算表應用
- 需要快速存取的數據表格
- 簡單計算引擎的基礎建構

## 使用方式

### 方法一：二維陣列實作範例

```csharp
// 建立 5 列 × 26 欄的試算表
var sheet = new Spreadsheet(5);

// 設定儲存格值
sheet.SetCell("A1", 10);
sheet.SetCell("B2", 20);

// 計算公式
int result = sheet.GetValue("=A1+B2"); // 30

// 重設儲存格
sheet.ResetCell("A1");
int newResult = sheet.GetValue("=A1+5"); // 5
```

### 方法二：哈希表實作範例

```csharp
// 建立哈希表版本的試算表
var sheet = new SpreadsheetHashMap(100);

// 設定儲存格值
sheet.SetCell("A1", 10);
sheet.SetCell("B2", 20);

// 計算公式
int result = sheet.GetValue("=A1+B2"); // 30

// 重設儲存格（從哈希表中移除）
sheet.ResetCell("A1");
int newResult = sheet.GetValue("=A1+5"); // 5
```

### 進階使用（兩種方法通用）

```csharp
// 混合運算：儲存格參考 + 常數
sheet.SetCell("C3", 15);
int result1 = sheet.GetValue("=C3+25"); // 40

// 儲存格間運算
sheet.SetCell("D4", 8);
int result2 = sheet.GetValue("=C3+D4"); // 23

// 純常數運算
int result3 = sheet.GetValue("=100+200"); // 300
```

## 建置與執行

### 環境需求

- .NET 8.0 或更高版本
- C# 12 語言功能支援

### 編譯

```bash
dotnet build
```

### 執行示範

```bash
dotnet run
```

### 預期輸出

```text
=== LeetCode 3484: Design Spreadsheet 解法示範 ===

建立 5×26 試算表完成
設定 A1 = 5, B2 = 7
計算 =A1+B2 = 12
重設 A1 後，計算 =A1+3 = 3
未設定的 C3，計算 =C3+10 = 10
常數計算 =15+25 = 40

=== 所有測試案例執行完成 ===

=== 方法二：哈希表實作測試 ===
方法二：設定 A1 = 15, B3 = 25
方法二：計算 =A1+B3 = 40
方法二：重設 A1 後，計算 =A1+10 = 10
=== 方法二測試完成 ===
```

## 實作細節

### 檔案結構

```text
leetcode_3484/
├── Program.cs          # 主程式與兩種 Spreadsheet 實作
├── leetcode_3484.csproj # 專案設定檔
└── README.md           # 本說明文件
```

### 核心方法說明

#### 方法一：Spreadsheet 類別（二維陣列）

##### 建構子

- 驗證輸入參數（rows > 0）
- 建立二維陣列並自動初始化為 0

##### SetCell & ResetCell

- 解析儲存格地址
- 直接操作陣列元素

##### GetValue

- 解析公式字串（"=X+Y" 格式）
- 評估運算元（數字或儲存格參考）
- 回傳計算結果

##### ParseCell

- 儲存格地址轉換為陣列索引
- 支援完整的錯誤檢查與範圍驗證

#### 方法二：SpreadsheetHashMap 類別（哈希表）

##### 初始化

- 初始化空的 Dictionary

##### 儲存格操作

- 直接操作 Dictionary（設定或移除項目）

##### 公式計算

- 使用字串操作分割公式
- 利用 `char.IsLetter()` 判斷運算元類型
- 使用 `GetValueOrDefault()` 處理未設定的儲存格

## 錯誤處理

### 支援的例外類型

- `ArgumentException` - 無效的輸入格式
- `ArgumentOutOfRangeException` - 超出範圍的儲存格地址

### 範例錯誤情況

```csharp
// 無效的列數
new Spreadsheet(0); // ArgumentException

// 無效的儲存格格式
sheet.SetCell("A", 5); // ArgumentException
sheet.SetCell("1A", 5); // ArgumentException

// 超出範圍的列號
sheet.SetCell("A100", 5); // ArgumentOutOfRangeException (如果 rows < 100)

// 無效的公式格式
sheet.GetValue("A1+B2"); // ArgumentException (缺少 '=')
sheet.GetValue("=A1*B2"); // ArgumentException (不支援乘法)
```

## 擴展可能

### 🚀 潛在改進方向

1. **多運算子支援**：擴展至減法、乘法、除法
2. **多欄位支援**：支援 AA、AB 等多字母欄位
3. **函式支援**：SUM、AVERAGE、MAX、MIN 等內建函式
4. **循環參考檢測**：避免 A1=B1, B1=A1 的情況
5. **儲存格格式**：支援文字、日期等非數字格式
6. **公式快取**：快取計算結果以提升重複查詢效能

### 🎯 效能最佳化

- 使用稀疏矩陣處理大量空白儲存格
- 實作公式相依圖以支援智慧重算
- 加入多執行緒支援處理大型計算

---

## 相關連結

- [LeetCode 3484 題目連結](https://leetcode.com/problems/design-spreadsheet/)
- [LeetCode 3484 中文題目](https://leetcode.cn/problems/design-spreadsheet/)

> **注意**：本實作專注於滿足 LeetCode 題目要求，在實際生產環境中可能需要額外的功能與最佳化。
