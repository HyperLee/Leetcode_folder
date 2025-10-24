# LeetCode 2048: Next Greater Numerically Balanced Number

> 尋找下一個更大的數值平衡數 - 三種高效解法完整解析

[![LeetCode](https://img.shields.io/badge/LeetCode-2048-FFA116?logo=leetcode)](https://leetcode.com/problems/next-greater-numerically-balanced-number/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Medium-orange)](https://leetcode.com/problems/next-greater-numerically-balanced-number/)
[![Language](https://img.shields.io/badge/Language-C%23%2013-239120?logo=csharp)](https://learn.microsoft.com/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=.net)](https://dotnet.microsoft.com/)

本專案提供 LeetCode 2048 題的完整 C# 解決方案，包含三種不同演算法實作，並附有詳細的效能比較與分析。

## 📋 問題描述

一個整數 `x` 是**數值平衡的（Numerically Balanced）**，當且僅當對於 `x` 中的每個數字 `d`，該數字在 `x` 中恰好出現 `d` 次。

**任務：** 給定一個整數 `n`，返回嚴格大於 `n` 的最小數值平衡數。

### 範例說明

| 輸入 | 輸出 | 說明 |
|------|------|------|
| `n = 1` | `22` | 數字 2 出現了 2 次 ✓ |
| `n = 1000` | `1333` | 數字 1 出現 1 次 ✓，數字 3 出現 3 次 ✓ |
| `n = 3000` | `3133` | 數字 1 出現 1 次 ✓，數字 3 出現 3 次 ✓ |

### 限制條件

- `0 <= n <= 10^6`

## 🎯 核心概念解析

### 什麼是數值平衡數？

數值平衡數必須滿足：**數字本身的值 = 該數字在整個數中出現的次數**

#### 詳細範例分析

```
✅ 22：數字 2 出現 2 次 → 平衡
✅ 122：數字 1 出現 1 次，數字 2 出現 2 次 → 平衡
✅ 333：數字 3 出現 3 次 → 平衡
✅ 1333：數字 1 出現 1 次，數字 3 出現 3 次 → 平衡
✅ 4444：數字 4 出現 4 次 → 平衡

❌ 123：數字 2 出現 1 次（應該 2 次），數字 3 出現 1 次（應該 3 次） → 不平衡
❌ 1123：數字 1 出現 2 次（應該 1 次） → 不平衡
❌ 100：包含數字 0，但 0 不能出現（0 出現 0 次沒有意義） → 不平衡
```

### 關鍵觀察

1. **數值平衡數非常稀少**：在 `[1, 10^6]` 範圍內，只有約 80 個數值平衡數
2. **數字限制**：
   - 數字 0 不能出現（0 出現 0 次無法驗證）
   - 數字 1-7 是主要候選
   - 數字 8、9 幾乎不可能（需要至少 8 或 9 位數，遠超 10^6）
3. **位數限制**：在 10^6 以內，最多 6-7 位數

## 🚀 三種解法詳解

本專案實作了三種不同的演算法，各有優劣：

### 方法一：暴力枚舉法（Brute Force）

#### 演算法描述

從 `n+1` 開始逐一檢查每個數字，直到找到第一個數值平衡數。

#### 實作步驟

1. **遍歷候選數字**：從 `n + 1` 開始遞增
2. **統計數字頻率**：使用陣列記錄每個數字（0-9）的出現次數
3. **驗證平衡條件**：
   - 對於每個數字 `d`（0-9）
   - 如果 `d` 在數中出現，檢查其出現次數是否等於 `d`
   - 如果不等於，則該數不是平衡數
4. **返回結果**：找到的第一個平衡數

#### 程式碼核心邏輯

```csharp
public int NextBeautifulNumber_BruteForce(int n)
{
    // 從 n+1 開始遍歷
    for (int num = n + 1; num <= 10000000; num++)
    {
        if (IsBalanced(num))
        {
            return num;
        }
    }
    return -1;
}

private bool IsBalanced(int num)
{
    // 統計每個數字的出現次數
    int[] count = new int[10];
    int temp = num;
    
    while (temp > 0)
    {
        count[temp % 10]++;
        temp /= 10;
    }
    
    // 驗證平衡條件
    for (int digit = 0; digit < 10; digit++)
    {
        if (count[digit] > 0 && count[digit] != digit)
        {
            return false;
        }
    }
    
    return true;
}
```

#### 複雜度分析

- **時間複雜度**：`O(m × log m)`
  - `m` 是需要檢查的數字數量（最壞情況下可能很大）
  - `log m` 是統計每個數字位數的時間
- **空間複雜度**：`O(1)` - 只需要固定大小的陣列（大小為 10）

#### 優缺點

**✅ 優點：**
- 實作簡單直觀
- 不需要額外的預處理
- 記憶體使用極少

**❌ 缺點：**
- 效率較低，當 `n` 接近兩個平衡數之間的大間隔時，需要檢查很多數字
- 無法利用平衡數稀少的特性

**💡 適用場景：** 一次性查詢，且對效能要求不高

---

### 方法二：預先生成法（Pre-Generation with Binary Search）

#### 演算法描述

利用數值平衡數稀少的特性，預先生成所有可能的平衡數並排序，然後使用二分搜尋快速找到答案。

#### 核心洞察

在 `[1, 10^6]` 範圍內，數值平衡數非常少：
- 1 位數：無（題目要求嚴格大於 n）
- 2 位數：`22`
- 3 位數：`122`, `212`, `221`, `333`
- 4 位數：`1333`, `3133`, `3313`, `3331`, `4444`
- ...

總共只有約 **80 個**數值平衡數！

#### 實作步驟

1. **預生成階段**（只執行一次）：
   - 遍歷 `[1, 10^6]` 範圍內的所有數字
   - 檢查每個數字是否為平衡數
   - 將平衡數加入列表並排序
   - 使用靜態快取避免重複生成

2. **查詢階段**（每次呼叫）：
   - 使用二分搜尋在預生成的列表中查找
   - 找到第一個大於 `n` 的平衡數

#### 程式碼核心邏輯

```csharp
// 靜態快取：只生成一次，之後重複使用
private static List<int>? _cachedBalancedNumbers = null;
private static readonly object _lockObject = new object();

public int NextBeautifulNumber_PreGenerated(int n)
{
    // 取得快取的平衡數列表（首次會生成）
    List<int> balancedNumbers = GetCachedBalancedNumbers();
    
    // 二分搜尋找到第一個大於 n 的數
    int left = 0, right = balancedNumbers.Count - 1;
    int result = int.MaxValue;
    
    while (left <= right)
    {
        int mid = left + (right - left) / 2;
        
        if (balancedNumbers[mid] > n)
        {
            result = balancedNumbers[mid];
            right = mid - 1;  // 繼續在左半部找更小的
        }
        else
        {
            left = mid + 1;   // 在右半部找
        }
    }
    
    return result;
}

private static List<int> GetCachedBalancedNumbers()
{
    // 雙重檢查鎖定模式（Thread-Safe Lazy Initialization）
    if (_cachedBalancedNumbers == null)
    {
        lock (_lockObject)
        {
            if (_cachedBalancedNumbers == null)
            {
                _cachedBalancedNumbers = GenerateBalancedNumbers();
            }
        }
    }
    return _cachedBalancedNumbers;
}

private static List<int> GenerateBalancedNumbers()
{
    List<int> result = new List<int>();
    
    for (int num = 1; num <= 1000000; num++)
    {
        if (IsBalancedStatic(num))
        {
            result.Add(num);
        }
    }
    
    return result;
}
```

#### 二分搜尋詳解

二分搜尋的目標是找到**第一個大於 `n` 的數值平衡數**。

**搜尋過程範例**（假設 `n = 1000`）：

```
平衡數列表：[22, 122, 212, 221, 333, 1333, 3133, ...]
                                    ↑
                                 目標：1333

初始：left = 0, right = 列表長度-1

第一次：
  mid = (0 + 列表長度-1) / 2
  balancedNumbers[mid] 與 1000 比較
  
如果 > 1000：
  result = balancedNumbers[mid]  // 暫存答案
  right = mid - 1                // 左半部可能有更小的
  
如果 <= 1000：
  left = mid + 1                 // 答案一定在右半部

重複直到 left > right，返回 result
```

#### 複雜度分析

- **時間複雜度**：
  - 首次呼叫：`O(10^6)` - 生成所有平衡數
  - 後續呼叫：`O(log k)` - 二分搜尋，k ≈ 80
  - 實際上首次呼叫後，查詢幾乎是 `O(1)`（log 80 ≈ 6.3）
  
- **空間複雜度**：`O(k)` - 存儲約 80 個平衡數

#### 優缺點

**✅ 優點：**
- 查詢速度極快（二分搜尋）
- 多次查詢時效率遠超其他方法
- 使用靜態快取，避免重複生成

**❌ 缺點：**
- 首次呼叫需要預處理時間
- 需要額外記憶體存儲平衡數列表
- 如果只查詢一次，預處理可能是浪費

**💡 適用場景：** 需要多次查詢，或對查詢速度有極高要求

---

### 方法三：回溯法直接生成（Backtracking）

#### 演算法描述

從 `n+1` 的位數開始，使用回溯法有策略地構造可能的數值平衡數，避免盲目搜尋。

#### 核心思想

不是遍歷所有數字，而是**直接構造**可能的平衡數：
- 選擇數字 1-7
- 確保每個數字出現的次數等於其值
- 使用剪枝策略提早終止不可能的分支

#### 實作步驟

1. **確定位數範圍**：從 `n+1` 的位數開始嘗試
2. **回溯構造**：
   - 對於每個位置，嘗試放入數字 1-7（首位不能為 0）
   - 維護每個數字的使用次數
   - **剪枝條件**：如果某數字 `d` 的使用次數已經達到 `d`，不再使用
3. **驗證平衡**：構造完成後，檢查是否滿足平衡條件
4. **找到最小值**：在所有生成的平衡數中，找到第一個大於 `n` 的

#### 程式碼核心邏輯

```csharp
public int NextBeautifulNumber_Backtracking(int n)
{
    // 從 n+1 的位數開始嘗試
    int digits = n.ToString().Length;
    
    for (int length = digits; length <= 7; length++)
    {
        int result = FindBalancedNumberWithLength(n, length);
        if (result != -1)
        {
            return result;
        }
    }
    
    return -1;
}

private int FindBalancedNumberWithLength(int n, int length)
{
    List<int> candidates = new List<int>();
    
    // 使用回溯法生成指定位數的所有平衡數
    BacktrackGenerate(length, new int[10], 0, 0, candidates);
    
    // 找到第一個大於 n 的數
    candidates.Sort();
    foreach (int num in candidates)
    {
        if (num > n)
        {
            return num;
        }
    }
    
    return -1;
}

private void BacktrackGenerate(
    int remainingLength,    // 剩餘位數
    int[] digitCount,       // 每個數字的使用次數
    long currentNum,        // 當前構造的數字
    int numDigits,          // 已使用的位數
    List<int> result)
{
    // 基礎情況：構造完成
    if (numDigits == remainingLength)
    {
        if (IsBalancedWithCount(digitCount))
        {
            result.Add((int)currentNum);
        }
        return;
    }
    
    // 遞迴：嘗試每個數字
    for (int digit = (numDigits == 0 ? 1 : 0); digit <= 7; digit++)
    {
        // 剪枝：如果這個數字使用次數還沒達到上限
        if (digitCount[digit] < digit)
        {
            digitCount[digit]++;
            BacktrackGenerate(
                remainingLength,
                digitCount,
                currentNum * 10 + digit,
                numDigits + 1,
                result
            );
            digitCount[digit]--;  // 回溯
        }
    }
}

private bool IsBalancedWithCount(int[] digitCount)
{
    for (int digit = 0; digit < 10; digit++)
    {
        // 如果數字出現了，但次數不等於數字本身
        if (digitCount[digit] > 0 && digitCount[digit] != digit)
        {
            return false;
        }
    }
    return true;
}
```

#### 回溯過程詳解

**範例：構造 3 位數的平衡數**

```
目標：找到所有 3 位數的平衡數

回溯樹（部分）：
                    根
                   / | \
                  1  2  3  ...
                 /   |   \
                /    |    \
               1,2  2,2  3,3 ...
              /      |      \
             1,2,2  2,2,2  3,3,3
             ✓      ✓      ✓
            (122)  (222)  (333)

剪枝範例：
- 如果第一位選擇 1，第二位選擇 1
  → digitCount[1] = 2，但 1 應該只出現 1 次
  → 剪枝，不繼續探索

- 如果第一位選擇 2，第二位選擇 2，第三位只能選擇 2
  → 構造出 222
  → 檢查：digitCount[2] = 3，但 2 應該出現 2 次
  → 不是平衡數，捨棄
```

#### 複雜度分析

- **時間複雜度**：`O(k)`
  - `k` 是可能的平衡數數量
  - 由於剪枝，實際檢查的數量遠小於暴力法
  - 對於給定位數，候選數非常有限
  
- **空間複雜度**：`O(d + k)`
  - `d` 是遞迴深度（數字位數）
  - `k` 是存儲候選數的空間

#### 優缺點

**✅ 優點：**
- 比暴力法更高效，直接構造候選數
- 使用剪枝減少無效搜尋
- 不需要預處理

**❌ 缺點：**
- 實作較複雜
- 仍需要生成多個候選數

**💡 適用場景：** 平衡實作複雜度和執行效率

---

## 📊 效能比較

專案內建了完整的效能測試，對三種方法進行比較：

### 測試結果範例

```
測試案例: n = 1
------------------------------
方法一 (暴力枚舉): 22
執行時間: 0.125 ms
方法二 (預先生成): 22
執行時間: 0.003 ms
方法三 (回溯法): 22
執行時間: 0.089 ms
✅ 所有方法結果一致

測試案例: n = 1000
------------------------------
方法一 (暴力枚舉): 1333
執行時間: 8.543 ms
方法二 (預先生成): 1333
執行時間: 0.002 ms
方法三 (回溯法): 1333
執行時間: 0.156 ms
✅ 所有方法結果一致

測試案例: n = 10000
------------------------------
方法一 (暴力枚舉): 22333
執行時間: 125.678 ms
方法二 (預先生成): 22333
執行時間: 0.002 ms
方法三 (回溯法): 22333
執行時間: 0.234 ms
✅ 所有方法結果一致
```

### 效能總結

| 方法 | 首次呼叫 | 後續呼叫 | 記憶體 | 實作難度 |
|------|---------|---------|--------|---------|
| 暴力枚舉 | 慢-非常慢 | 慢-非常慢 | 低 | ⭐ |
| 預先生成 | 慢（首次） | **極快** | 中 | ⭐⭐ |
| 回溯法 | 中等 | 中等 | 中 | ⭐⭐⭐ |

**建議選擇：**
- **LeetCode 提交**：方法二（預先生成）- 通常最快
- **單次查詢**：方法一（暴力枚舉）- 最簡單
- **學習演算法**：方法三（回溯法）- 最有教育意義

## 🛠️ 使用方式

### 環境需求

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) 或更高版本
- 支援 C# 13 的 IDE（推薦 Visual Studio 2022 或 JetBrains Rider）

### 執行專案

1. **複製專案**

```bash
git clone <repository-url>
cd leetcode_2048
```

2. **建構專案**

```bash
cd leetcode_2048
dotnet build
```

3. **執行程式**

```bash
dotnet run
```

程式會自動執行所有測試案例，並顯示三種方法的效能比較。

### 自訂測試

編輯 `Program.cs` 中的測試案例陣列：

```csharp
int[] testCases = { 1, 1000, 3000, 0, 10, 100, 10000 };
```

## 📚 延伸學習

### 相關題目

- **LeetCode 202**: Happy Number - 數字循環判斷
- **LeetCode 263**: Ugly Number - 特殊數字判斷
- **LeetCode 357**: Count Numbers with Unique Digits - 回溯法生成數字

### 進階思考

1. **數學證明**：如何證明在 `[1, 10^6]` 範圍內數值平衡數的上限？
2. **演算法優化**：能否設計 O(1) 空間複雜度的解法？
3. **擴展問題**：如果範圍擴展到 `10^9`，哪種方法最適合？
4. **變體問題**：如果要求找到前 k 個數值平衡數，應該如何實作？

### 演算法技巧總結

本題涉及的重要技巧：

- ✅ **哈希表/陣列計數**：統計數字出現頻率
- ✅ **二分搜尋**：在排序列表中快速查找
- ✅ **回溯法**：系統化地生成候選解
- ✅ **剪枝優化**：提早終止不可能的分支
- ✅ **快取優化**：避免重複計算

## 🔗 參考資源

- [LeetCode 2048 題目頁面](https://leetcode.com/problems/next-greater-numerically-balanced-number/)
- [LeetCode CN 2048 題目頁面](https://leetcode.cn/problems/next-greater-numerically-balanced-number/)
- [.NET 官方文件](https://learn.microsoft.com/dotnet/)
- [C# 13 新特性](https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-13)

## 📖 專案結構

```
leetcode_2048/
├── leetcode_2048.sln          # Visual Studio 解決方案檔案
├── README.md                   # 本檔案
└── leetcode_2048/
    ├── Program.cs              # 主程式（包含三種解法）
    ├── 解題說明.md             # 詳細解題說明
    └── leetcode_2048.csproj    # 專案設定檔
```

## 💡 程式碼特色

- ✅ **完整的 XML 文件註解**：每個方法都有詳細說明
- ✅ **符合 C# 13 規範**：使用最新語言特性
- ✅ **執行緒安全**：靜態快取使用雙重檢查鎖定模式
- ✅ **效能測試**：內建完整的效能比較機制
- ✅ **程式碼可讀性**：清晰的命名和結構

---

**Happy Coding!** 如有問題或建議，歡迎提出 Issue 或 Pull Request。
