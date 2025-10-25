# LeetCode 1716: 計算力扣銀行的錢

> **LeetCode 題目 1716** - 2025-10-25 每日一題

使用 C# 實作的 LeetCode 題目「計算力扣銀行的錢」解法，提供兩種不同方法：數學優化與模擬。

## 題目描述

Hercy 想要存錢買他的第一輛車。他每天都會依照以下規則存錢到力扣銀行：

- **第 1 天（星期一）：** 存入 $1
- **第 2-7 天（星期二至星期日）：** 每天比前一天多存 $1
- **每個後續的星期一：** 比前一個星期一多存 $1

給定 `n` 天，回傳第 n 天結束時銀行的總金額。

### 範例

**輸入：** `n = 4`  
**輸出：** `10`  
**說明：** 4 天後：1 + 2 + 3 + 4 = 10

**輸入：** `n = 10`  
**輸出：** `37`  
**說明：**

- 第 1 週（星期一至星期日）：1 + 2 + 3 + 4 + 5 + 6 + 7 = 28
- 第 2 週（星期一至星期三）：2 + 3 + 4 = 9
- 總計：28 + 9 = 37

## 解法

本專案實作了兩種不同的方法來解決此題：

### 1. 數學優化解法（`TotalMoney`）

**時間複雜度：** O(w)，其中 w = ⌊n/7⌋  
**空間複雜度：** O(1)

#### 演算法概述

此解法將問題拆解為完整週數與剩餘天數：

```text
n = 7w + d
```

其中：

- `w` = 完整週數
- `d` = 剩餘天數（0 ≤ d < 7）

#### 數學推導

**完整週的計算：**

對於每個完整的第 `i` 週（從 0 開始計數），存款形成等差數列：

- 第 0 週：1, 2, 3, 4, 5, 6, 7 → 起始值為 1
- 第 1 週：2, 3, 4, 5, 6, 7, 8 → 起始值為 2
- 第 i 週：(1+i), (2+i), ..., (7+i) → 起始值為 (1+i)

第 `i` 週的總和為：

```text
Sum_i = Σ(k=0 to 6) [(1+i) + k]
      = 7(1+i) + Σ(k=0 to 6) k
      = 7(1+i) + 21
      = 7 + 7i + 21
      = 28 + 7i
```

所有完整週的總和：

```text
Total_weeks = Σ(i=0 to w-1) [28 + 7i]
            = 28w + 7 × Σ(i=0 to w-1) i
            = 28w + 7 × [w(w-1)/2]
            = 28w + 7w(w-1)/2
```

**剩餘天數的計算：**

剩餘的 `d` 天屬於第 `w` 週（從 0 開始計數），起始值為 `(1+w)`：

```text
Total_days = Σ(k=0 to d-1) [(1+w) + k]
           = d(1+w) + Σ(k=0 to d-1) k
           = d(1+w) + d(d-1)/2
```

**最終公式：**

```text
Total = 28w + 7w(w-1)/2 + d(1+w) + d(d-1)/2
```

#### 程式碼實作

```csharp
public int TotalMoney(int n)
{
    int weeks = n / 7;
    int days = n % 7;
    int total = 0;

    // 計算完整週的總金額
    for (int i = 0; i < weeks; i++)
    {
        int weekStart = 1 + i;
        total += 7 * weekStart + 21;  // 7 * weekStart + (0+1+2+...+6)
    }

    // 計算剩餘天數的總金額
    if (days > 0)
    {
        int weekStart = 1 + weeks;
        total += days * weekStart + (days * (days - 1)) / 2;
    }

    return total;
}
```

#### 逐步範例（n = 10）

1. **解析輸入：** n = 10
2. **計算：** weeks = 10 / 7 = 1, days = 10 % 7 = 3
3. **完整的第 0 週：**
   - weekStart = 1
   - sum = 7 × 1 + 21 = 28
4. **剩餘 3 天（第 1 週）：**
   - weekStart = 1 + 1 = 2
   - sum = 3 × 2 + (3 × 2) / 2 = 6 + 3 = 9
5. **總計：** 28 + 9 = **37** ✓

### 2. 模擬解法（`TotalMoney2`）

**時間複雜度：** O(n)  
**空間複雜度：** O(1)

#### 演算法概述

此方法完全按照題目描述模擬每日存款過程：

1. 追蹤當前週數與週內天數
2. 對每一天，計算存款金額為 `week + day - 1`
3. 超過第 7 天後，重置為第 1 天並增加週數

#### 程式碼實作

```csharp
public int TotalMoney2(int n)
{
    int week = 1;  // 當前週數（從 1 開始）
    int day = 1;   // 當前週內天數（1-7）
    int res = 0;   // 累計總金額

    for (int i = 0; i < n; i++)
    {
        // 每日存款 = 當前週的基準金額 + 週內天數的偏移量
        res += week + day - 1;
        
        day++;
        if (day > 7)
        {
            day = 1;    // 重置為星期一
            week++;     // 進入下一週
        }
    }
    
    return res;
}
```

#### 執行追蹤範例（n = 10）

| 天數 (i) | 週數 | 週內天數 | 存款金額 | 累計總額 |
|---------|------|---------|---------|---------|
| 0       | 1    | 1       | 1       | 1       |
| 1       | 1    | 2       | 2       | 3       |
| 2       | 1    | 3       | 3       | 6       |
| 3       | 1    | 4       | 4       | 10      |
| 4       | 1    | 5       | 5       | 15      |
| 5       | 1    | 6       | 6       | 21      |
| 6       | 1    | 7       | 7       | 28      |
| 7       | 2    | 1       | 2       | 30      |
| 8       | 2    | 2       | 3       | 33      |
| 9       | 2    | 3       | 4       | **37** ✓|

## 比較

| 面向            | TotalMoney（數學） | TotalMoney2（模擬） |
|-----------------|-------------------|---------------------|
| 時間複雜度      | O(w) ≈ O(n/7)     | O(n)                |
| 空間複雜度      | O(1)              | O(1)                |
| 程式碼複雜度    | 中等              | 簡單                |
| 可讀性          | 需要數學知識      | 非常直觀            |
| 大數值效能      | 較佳              | 較慢                |

## 快速開始

### 前置需求

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) 或更新版本

### 執行解法

1. **複製儲存庫：**

   ```bash
   git clone <repository-url>
   cd leetcode_1716
   ```

2. **建置專案：**

   ```bash
   dotnet build
   ```

3. **執行應用程式：**

   ```bash
   dotnet run --project leetcode_1716
   ```

   **預期輸出：**

   ```text
   n = 4, TotalMoney = 10, TotalMoney2 = 10
   n = 10, TotalMoney = 37, TotalMoney2 = 37
   n = 20, TotalMoney = 96, TotalMoney2 = 96
   n = 28, TotalMoney = 154, TotalMoney2 = 154
   n = 100, TotalMoney = 1060, TotalMoney2 = 1060
   ```

### 在 VS Code 中除錯

專案包含預先設定好的 VS Code 設定：

1. 在 VS Code 中開啟專案
2. 按 `F5` 開始除錯
3. 程式會自動建置並使用測試案例執行

## 專案結構

```text
leetcode_1716/
├── .vscode/
│   ├── launch.json      # 除錯設定
│   └── tasks.json       # 建置任務
├── leetcode_1716/
│   ├── Program.cs       # 主要解法檔案
│   └── *.csproj         # 專案設定
├── .editorconfig        # 程式碼風格設定
└── README.md            # 本檔案
```

## 連結

- **LeetCode 題目（英文）：** [1716. Calculate Money in Leetcode Bank](https://leetcode.com/problems/calculate-money-in-leetcode-bank/)
- **LeetCode 題目（中文）：** [1716. 计算力扣银行的钱](https://leetcode.cn/problems/calculate-money-in-leetcode-bank/)
- **每日一題：** 2025-10-25

## 注意事項

> [!TIP]
> 對於較小的 `n` 值（< 100），兩種解法的效能相近。對於較大的數值或重複計算，建議使用 `TotalMoney`，因為其時間複雜度為 O(w)。

> [!NOTE]
> 數學解法可以進一步優化為 O(1)，方法是將迴圈替換為封閉形式公式：
> ```text
> Total_weeks = 28w + 7w(w-1)/2
> ```
