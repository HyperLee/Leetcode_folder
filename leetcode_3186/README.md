# LeetCode 3186: Maximum Total Damage With Spell Casting

> **施咒的最大總傷害** - 動態規劃解法實作與分析

[![LeetCode](https://img.shields.io/badge/LeetCode-3186-orange.svg)](https://leetcode.com/problems/maximum-total-damage-with-spell-casting/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Medium-yellow.svg)](https://leetcode.com/problemset/all/)
[![Language](https://img.shields.io/badge/Language-C%23-239120.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4.svg)](https://dotnet.microsoft.com/)

## 題目描述

### English

A magician has various spells. You are given an array `power`, where each element represents the damage of a spell. Multiple spells can have the same damage value.

It is a known fact that if a magician decides to cast a spell with a damage of `power[i]`, they **cannot** cast any spell with a damage of `power[i] - 2`, `power[i] - 1`, `power[i] + 1`, or `power[i] + 2`.

Each spell can be cast **only once**.

Return the **maximum possible total damage** that a magician can cast.

### 中文

一位魔法師擁有多種法術。給定陣列 `power`，陣列中每個元素代表一個法術的傷害值。可能有多個法術具有相同的傷害值。

若魔法師選擇施放傷害為 `power[i]` 的法術，則他**不能**施放任何傷害為 `power[i] - 2`、`power[i] - 1`、`power[i] + 1` 或 `power[i] + 2` 的法術。

每個法術只能施放**一次**。

請回傳魔法師能施放的**最大總傷害**。

## 題目範例

### Example 1

```text
Input: power = [1,1,3,4]
Output: 6
Explanation: 
- 選擇所有傷害值為 1 的法術 (1+1=2) 和傷害值為 4 的法術 (4)
- 總傷害 = 2 + 4 = 6
- 或選擇兩個傷害值為 3 的法術（如果存在）
```

### Example 2

```text
Input: power = [7,1,6,6]
Output: 13
Explanation:
- 選擇傷害值為 7 的法術和所有傷害值為 6 的法術 (6+6=12)
- 總傷害 = 7 + 6 + 6 = 13（實際上是選 7 和一個 6，或兩個 6）
```

### Example 3

```text
Input: power = [1,2,3,4,5]
Output: 7
Explanation:
- 選擇傷害值 2 和 5（它們的差距為 3，不衝突）
- 總傷害 = 2 + 5 = 7
- 注意：不能選 1, 3, 5，因為 1 和 3 差距為 2（衝突）
```

## 解題思路

### 核心概念

這是一個**動態規劃**問題，類似於 **House Robber** 問題的變種，但有更嚴格的限制條件。

#### 為什麼不是 Two Pointers？

- **Two Pointers** 適用於在有序陣列中尋找配對或連續區間
- 本題的核心是**決策問題**：每個傷害值需要決定「選或不選」
- 需要考慮**最優子結構**，這是動態規劃的典型特徵

### 關鍵觀察

1. **相同傷害值可以全部選擇**：傷害值相同的法術不會互相衝突
2. **衝突範圍**：選擇傷害值 `x` 後，`[x-2, x-1, x+1, x+2]` 都不能選
3. **排序優化**：排序後只需考慮左側的衝突（`x-2` 和 `x-1`）

### 動態規劃狀態定義

- **狀態**：`dp[i]` = 考慮前 `i+1` 個不同傷害值時的最大總傷害
- **轉移方程**：

  ```text
  dp[i] = max(dp[i-1], dp[j] + currentDamage)
  ```

  其中 `j` 是最後一個不與 `i` 衝突的位置

- **初始狀態**：`dp[0] = 第一個傷害值的總和`
- **答案**：`dp[n-1]`

## 解法實作

本專案提供兩種動態規劃解法，核心思路相同但實作細節不同。

### 解法 1：使用輔助函式

```csharp
public long MaximumTotalDamage_DP(int[] power)
{
    // 1. 統計每個傷害值的總和
    var damageCount = new Dictionary<int, long>();
    foreach (int p in power)
    {
        damageCount[p] = damageCount.GetValueOrDefault(p, 0) + p;
    }
    
    // 2. 按傷害值排序
    var sortedDamages = damageCount.Keys.OrderBy(x => x).ToArray();
    int n = sortedDamages.Length;
    
    // 3. 動態規劃
    long[] dp = new long[n];
    dp[0] = damageCount[sortedDamages[0]];
    
    for (int i = 1; i < n; i++)
    {
        dp[i] = dp[i - 1]; // 不選
        
        int lastValidIndex = FindLastValidIndex(sortedDamages, i);
        long currentDamage = damageCount[sortedDamages[i]];
        
        if (lastValidIndex == -1)
            dp[i] = Math.Max(dp[i], currentDamage);
        else
            dp[i] = Math.Max(dp[i], dp[lastValidIndex] + currentDamage);
    }
    
    return dp[n - 1];
}
```

**特點**：

- 使用獨立的 `FindLastValidIndex` 函式尋找不衝突位置
- 程式碼結構清晰，職責分離
- 易於測試和維護

### 解法 2：內嵌式實作

```csharp
public long MaximumTotalDamage_Optimized(int[] power)
{
    // 1. 統計每種傷害值的總傷害
    var damageMap = new Dictionary<int, long>();
    foreach (int p in power)
    {
        damageMap[p] = damageMap.GetValueOrDefault(p, 0) + p;
    }
    
    // 2. 轉換為 (damage, totalDamage) 並排序
    var damages = damageMap.Select(kv => new { Damage = kv.Key, Total = kv.Value })
                            .OrderBy(x => x.Damage)
                            .ToArray();
    
    int n = damages.Length;
    if (n == 1) return damages[0].Total;
    
    // 3. 動態規劃
    long[] dp = new long[n];
    dp[0] = damages[0].Total;
    
    for (int i = 1; i < n; i++)
    {
        dp[i] = dp[i - 1]; // 不選
        
        // 內嵌尋找不衝突位置
        int j = i - 1;
        while (j >= 0 && damages[i].Damage - damages[j].Damage <= 2)
        {
            j--;
        }
        
        long chooseCurrentDamage = (j >= 0 ? dp[j] : 0) + damages[i].Total;
        dp[i] = Math.Max(dp[i], chooseCurrentDamage);
    }
    
    return dp[n - 1];
}
```

**特點**：

- 邏輯更緊湊，直接在迴圈內處理
- 減少函式呼叫開銷
- 使用匿名型別提高可讀性

## 複雜度分析

### 時間複雜度

| 解法 | 時間複雜度 | 說明 |
|------|-----------|------|
| **解法 1** | **O(n log n + n·k)** | 排序 O(n log n) + DP O(n·k)，k 為平均衝突檢查次數 |
| **解法 2** | **O(n log n + n·k)** | 與解法 1 相同 |

- **排序**：O(n log n)
- **統計傷害值**：O(n)
- **DP 迴圈**：O(n)，每次迴圈內尋找不衝突位置最壞 O(n)
- **實際表現**：通常 k << n，因為衝突範圍只有 ±2

### 空間複雜度

| 解法 | 空間複雜度 | 說明 |
|------|-----------|------|
| **解法 1** | **O(n)** | Dictionary + 排序陣列 + DP 陣列 |
| **解法 2** | **O(n)** | Dictionary + damages 陣列 + DP 陣列 |

## 兩種解法比較

| 比較項目 | 解法 1（輔助函式） | 解法 2（內嵌式） |
|---------|------------------|-----------------|
| **程式碼長度** | 較長（分離函式） | 較短（緊湊） |
| **可讀性** | ⭐⭐⭐⭐⭐ 職責清晰 | ⭐⭐⭐⭐ 邏輯直觀 |
| **維護性** | ⭐⭐⭐⭐⭐ 易於修改測試 | ⭐⭐⭐⭐ 需理解整體邏輯 |
| **執行效率** | ⭐⭐⭐⭐ 有函式呼叫開銷 | ⭐⭐⭐⭐⭐ 較少開銷 |
| **記憶體使用** | ⭐⭐⭐⭐ 額外函式堆疊 | ⭐⭐⭐⭐⭐ 無額外呼叫 |
| **適用場景** | 大型專案、團隊協作 | 競賽、快速開發 |

### 推薦使用

- **解法 1**：適合生產環境、需要良好可維護性的專案
- **解法 2**：適合競賽、原型開發、追求極致效能的場景

## 執行結果

```text
Test Case 1: [1, 1, 3, 4]
  DP Solution: 6
  Optimized Solution: 6

Test Case 2: [7, 1, 6, 6]
  DP Solution: 13
  Optimized Solution: 13

Test Case 3: [1, 2, 3, 4, 5]
  DP Solution: 7
  Optimized Solution: 7

Test Case 4: [5, 9, 4, 6]
  DP Solution: 15
  Optimized Solution: 15
```

## 如何執行

### 前置需求

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) 或更新版本
- Visual Studio Code（建議）或其他 C# IDE

### 建置與執行

```bash
# 複製專案
git clone <repository-url>
cd leetcode_3186

# 建置專案
dotnet build

# 執行程式
dotnet run --project leetcode_3186/leetcode_3186.csproj
```

### 偵錯

專案已包含 VS Code 偵錯設定檔（`.vscode/launch.json`），可直接按 `F5` 啟動偵錯。

## 演算法優化討論

### 可能的優化方向

1. **二分搜尋優化**
   - 使用二分搜尋找不衝突位置：O(log n)
   - 總時間複雜度降至 O(n log n)
   
2. **空間優化**
   - 使用滾動陣列：空間降至 O(1)
   - 但需要保留完整 DP 陣列來查找不衝突位置

3. **記憶化遞迴**
   - 使用 Top-down DP
   - 程式碼更直觀但可能有遞迴開銷

### 與相似問題的關聯

- **House Robber (LeetCode 198)**：不能選擇相鄰房屋
- **House Robber II (LeetCode 213)**：環狀陣列版本
- **Delete and Earn (LeetCode 740)**：選擇數字後刪除相鄰數字
- **本題特點**：衝突範圍擴大到 ±2，需要更複雜的狀態轉移

## 學習重點

1. ✅ 動態規劃的狀態定義與轉移方程
2. ✅ 排序預處理優化問題複雜度
3. ✅ Dictionary 的聚合操作技巧
4. ✅ 匿名型別在資料轉換中的應用
5. ✅ 函式分離 vs 內嵌實作的權衡

## 延伸思考

### 問題變化

1. **如果衝突範圍改為 ±k，演算法如何調整？**
   - 修改衝突判斷條件即可，核心邏輯不變

2. **如果相同傷害值只能選一次呢？**
   - 不需要 Dictionary 聚合，直接排序原陣列

3. **如果要求輸出選擇的法術序列？**
   - 需要額外陣列記錄選擇路徑，回溯構造答案

## 專案結構

```text
leetcode_3186/
├── .github/
│   ├── instructions/
│   │   └── csharp.instructions.md    # C# 開發規範
│   └── prompts/
│       └── create-readme.prompt.md   # README 生成提示
├── .vscode/
│   ├── launch.json                   # 偵錯設定
│   └── tasks.json                    # 建置任務
├── leetcode_3186/
│   ├── Program.cs                    # 主程式與解法實作
│   └── leetcode_3186.csproj          # 專案檔
├── .editorconfig                     # 編輯器設定
├── .gitignore                        # Git 忽略檔案
├── leetcode_3186.sln                 # 解決方案檔
└── README.md                         # 本檔案
```

## 相關連結

- [LeetCode 原題（英文）](https://leetcode.com/problems/maximum-total-damage-with-spell-casting/)
- [LeetCode 原題（中文）](https://leetcode.cn/problems/maximum-total-damage-with-spell-casting/)
- [動態規劃入門指南](https://leetcode.com/discuss/general-discussion/662866/dp-for-beginners-problems-patterns-sample-solutions)
- [House Robber 系列解析](https://leetcode.com/problems/house-robber/solutions/)

## 授權

本專案僅供學習交流使用。

---

> Happy Coding! 🚀
>
> 如有任何問題或建議，歡迎開 Issue 討論。
