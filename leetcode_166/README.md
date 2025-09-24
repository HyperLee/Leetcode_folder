# LeetCode 166: Fraction to Recurring Decimal

> **分數到小數** - 使用長除法模擬計算分數的小數表示，並自動偵測循環節

## 問題描述

給定兩個整數，分別表示一個分數的分子和分母，請將該分數以字串形式返回。

- 如果小數部分有循環重複，請將重複的部分用括號括起來
- 如果存在多種可能的答案，返回任意一個即可
- 保證對於所有給定的輸入，答案字串的長度小於 10^4

### 範例

| 輸入 | 輸出 | 說明 |
|------|------|------|
| numerator = 1, denominator = 2 | "0.5" | 有限小數 |
| numerator = 2, denominator = 1 | "2" | 整數 |
| numerator = 4, denominator = 333 | "0.(012)" | 循環小數 |
| numerator = 1, denominator = 3 | "0.(3)" | 單一數字循環 |

## 解法說明

### 什麼是長除法？

長除法（Long Division）是一種手工計算除法的方法，特別適用於計算分數的小數表示。基本原理：

1. **整數部分計算**：先計算 `分子 ÷ 分母` 的整數商
2. **餘數處理**：取得餘數 `分子 % 分母`
3. **小數計算**：
   - 將餘數乘以 10，繼續除以分母
   - 得到小數點後一位數字
   - 重複此過程直到餘數為 0 或出現循環

### 長除法範例：3 ÷ 14

```text
       0.2142857...
   ____________
14 | 3.0000000
     2.8       ← 14 × 0.2 = 2.8
     ---
     0.20      ← 餘數 2，乘以 10 = 20
     0.14      ← 14 × 1 = 14
     ----
     0.060     ← 餘數 6，乘以 10 = 60
     0.056     ← 14 × 4 = 56
     -----
     0.004...  ← 持續計算...
```

當餘數 `2` 再次出現時，說明開始循環，結果為 `0.2(142857)`

### 演算法核心思路

#### 1. 符號處理

```csharp
string sign = longNumerator * longDenominator < 0 ? "-" : "";
```

判斷分子分母符號是否相同，決定結果的正負性。

#### 2. 整數部分計算

```csharp
long quotient = longNumerator / longDenominator;
long remainder = longNumerator % longDenominator;
```

#### 3. 循環檢測的關鍵

使用哈希表記錄每個餘數第一次出現的位置：

```csharp
Dictionary<long, int> remainderToPosition = new Dictionary<long, int>();

while (remainder > 0)
{
    remainder *= 10;  // 模擬長除法：餘數乘以 10
    quotient = remainder / longDenominator;
    remainder %= longDenominator;
    
    result.Append(quotient);
    
    // 檢測循環：如果餘數重複出現
    if (remainderToPosition.ContainsKey(remainder))
    {
        int cycleStartPosition = remainderToPosition[remainder];
        return result.ToString().Substring(0, cycleStartPosition) + 
               "(" + result.ToString().Substring(cycleStartPosition) + ")";
    }
    
    remainderToPosition[remainder] = result.Length;
}
```

#### 4. 為什麼會有循環？

在長除法中，餘數的可能值範圍是 `[0, denominator-1]`。當餘數重複出現時：

- 後續的計算過程會完全重複
- 形成循環節
- 用哈希表記錄餘數首次出現位置，即可確定循環節的範圍

### 時間與空間複雜度

- **時間複雜度**：O(denominator)
  - 最壞情況下，所有可能的餘數都會出現一次
- **空間複雜度**：O(denominator)
  - 哈希表最多存儲 denominator 個不同的餘數

### 邊界情況處理

1. **整數情況**：餘數為 0，直接返回整數部分
2. **負數處理**：使用 `long` 型別避免 `int` 溢出，統一處理符號
3. **循環節偵測**：確保正確找到循環開始位置

## 執行結果

```bash
=== LeetCode 166: Fraction to Recurring Decimal ===

測試案例 1: 9/8
結果: 1.125
預期: 1.125

測試案例 2: 3/14
結果: 0.2(142857)
預期: 0.2(142857)

測試案例 3: 4/2
結果: 2
預期: 2

測試案例 4: -1/2
結果: -0.5
預期: -0.5

測試案例 5: 1/3
結果: 0.(3)
預期: 0.(3)
```

## 快速開始

### 環境需求

- .NET 8.0 或更高版本
- C# 12

### 執行程式

```bash
# 複製專案
git clone <repository-url>
cd leetcode_166

# 執行程式
dotnet run

# 或者先建構再執行
dotnet build
dotnet run --project leetcode_166/leetcode_166.csproj
```

### 專案結構

```text
leetcode_166/
├── leetcode_166/
│   ├── Program.cs              # 主程式，包含解法和測試案例
│   └── leetcode_166.csproj     # 專案檔案
├── leetcode_166.sln           # 解決方案檔案
└── README.md                  # 說明文件
```

## 核心程式碼

```csharp
public string FractionToDecimal(int numerator, int denominator)
{
    // 使用 long 避免 int 溢出問題
    long longNumerator = numerator;
    long longDenominator = denominator;
    
    // 判斷結果符號：分子分母符號不同時為負數
    string sign = longNumerator * longDenominator < 0 ? "-" : "";
    
    // 保證後續計算過程不產生負數
    longNumerator = Math.Abs(longNumerator);
    longDenominator = Math.Abs(longDenominator);

    // 計算整數部分和初始餘數
    long quotient = longNumerator / longDenominator;
    long remainder = longNumerator % longDenominator;
    
    // 如果沒有餘數，直接返回整數部分
    if (remainder == 0)
    {
        return sign + quotient.ToString();
    }

    // 建構小數結果：符號 + 整數部分 + 小數點
    StringBuilder result = new StringBuilder();
    result.Append(sign);
    result.Append(quotient);
    result.Append(".");
    
    // 用哈希表記錄餘數對應的小數位置，檢測循環節
    Dictionary<long, int> remainderToPosition = new Dictionary<long, int>();
    
    // 長除法計算小數部分
    while (remainder > 0)
    {
        // 餘數乘以10，準備計算下一位小數
        remainder *= 10;
        quotient = remainder / longDenominator;
        remainder %= longDenominator;
        
        // 將當前位數字加入結果
        result.Append(quotient);
        
        // 檢查是否進入循環節
        if (remainderToPosition.ContainsKey(remainder))
        {
            // 找到循環節開始位置
            int cycleStartPosition = remainderToPosition[remainder];
            // 將循環部分用括號括起來
            return result.ToString().Substring(0, cycleStartPosition) + 
                   "(" + result.ToString().Substring(cycleStartPosition) + ")";
        }
        
        // 記錄當前餘數對應的位置
        remainderToPosition[remainder] = result.Length;
    }
    
    // 有限小數，直接返回結果
    return result.ToString();
}
```

## 相關連結

- [LeetCode 原題（英文）](https://leetcode.com/problems/fraction-to-recurring-decimal/description/?envType=daily-question&envId=2025-09-24)
- [LeetCode 原題（中文）](https://leetcode.cn/problems/fraction-to-recurring-decimal/description/?envType=daily-question&envId=2025-09-24)

### 參考解法與討論

- [官方題解 - 分數到小數](https://leetcode.cn/problems/fraction-to-recurring-decimal/solutions/1028368/fen-shu-dao-xiao-shu-by-leetcode-solutio-tqdw/?envType=daily-question&envId=2025-09-24)
- [模擬長除法 - 多語言實現](https://leetcode.cn/problems/fraction-to-recurring-decimal/solutions/3790535/mo-ni-chang-chu-fa-pythonjavacgojsrust-b-di8h/?envType=daily-question&envId=2025-09-24)
- [宮水三葉 - 模擬數式計算](https://leetcode.cn/problems/fraction-to-recurring-decimal/solutions/1028784/gong-shui-san-xie-mo-ni-shu-shi-ji-suan-kq8c4/?envType=daily-question&envId=2025-09-24)

---

> 💡 **提示**：這個問題的核心在於理解長除法的計算過程，以及如何有效地檢測循環節。哈希表的使用是解決循環檢測的關鍵技巧。
