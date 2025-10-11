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

### 核心演算法流程

在詳細說明兩種解法之前，先理解共同的演算法核心：

#### 步驟 1：統計相同傷害值

```text
輸入：[1, 1, 3, 4]
處理：相同傷害值可以全部選擇（不會互相衝突）
結果：{1: 2, 3: 3, 4: 4}  // 1出現2次，總傷害2
```

#### 步驟 2：排序處理

```text
排序前：{1: 2, 3: 3, 4: 4}
排序後：[1, 3, 4]  // 按傷害值大小排序
```

**為什麼要排序？**

- 排序後，對於每個傷害值 `x`，所有小於 `x` 的值都在它左邊
- 這樣只需要**向前查找**（往左看）就能找到不衝突的位置
- 因為 `x` 的衝突範圍是 `[x-2, x-1, x+1, x+2]`，而排序後 `x+1` 和 `x+2` 還沒處理到

#### 步驟 3：動態規劃決策

對於每個位置 `i`，有兩種選擇：

選項 A：不選擇當前傷害值

```text
dp[i] = dp[i-1]  // 直接繼承前一個狀態的最大傷害
```

選項 B：選擇當前傷害值

```text
1. 向前遍歷，找到最後一個不衝突的位置 j
2. dp[i] = dp[j] + currentDamage
```

**為什麼要向前遍歷？關鍵理解！**

讓我們用範例說明：

```text
排序後的傷害值：[1, 3, 4, 7, 9]
              ↑  ↑  ↑  ↑  ↑
索引：         0  1  2  3  4

當處理 i=3 (傷害值7) 時：
- 7 的衝突範圍：[5, 6, 8, 9]
- 向前檢查：
  - j=2 (值4): 7-4=3 > 2 ✅ 不衝突！停止
  - 可以選擇 dp[2] + 7
  
當處理 i=4 (傷害值9) 時：
- 9 的衝突範圍：[7, 8, 10, 11]
- 向前檢查：
  - j=3 (值7): 9-7=2 ≤ 2 ❌ 衝突！繼續
  - j=2 (值4): 9-4=5 > 2 ✅ 不衝突！停止
  - 可以選擇 dp[2] + 9
```

**向前遍歷的核心原因：**

1. **已知性**：排序後，位置 `i` 左邊的所有值都已經被 DP 處理過
2. **遞減性**：從 `i-1` 向前看，傷害值遞減，差距越來越大
3. **提前終止**：一旦找到不衝突的位置，後面的位置差距更大，必定不衝突

#### 完整範例追蹤

```text
輸入：[1, 2, 3, 4, 5]
統計：{1:1, 2:2, 3:3, 4:4, 5:5}
排序：[1, 2, 3, 4, 5]

DP 過程：
i=0: 值1
  - dp[0] = 1
  
i=1: 值2
  - 不選：dp[1] = dp[0] = 1
  - 選：向前查找
    - j=0 (值1): 2-1=1 ≤ 2 ❌ 衝突
    - j=-1: 無可用位置，只能取當前值 = 2
  - dp[1] = max(1, 2) = 2
  
i=2: 值3
  - 不選：dp[2] = dp[1] = 2
  - 選：向前查找
    - j=1 (值2): 3-2=1 ≤ 2 ❌ 衝突
    - j=0 (值1): 3-1=2 ≤ 2 ❌ 衝突
    - j=-1: 無可用位置，只能取當前值 = 3
  - dp[2] = max(2, 3) = 3
  
i=3: 值4
  - 不選：dp[3] = dp[2] = 3
  - 選：向前查找
    - j=2 (值3): 4-3=1 ≤ 2 ❌ 衝突
    - j=1 (值2): 4-2=2 ≤ 2 ❌ 衝突
    - j=0 (值1): 4-1=3 > 2 ✅ 不衝突！
  - 選：dp[0] + 4 = 1 + 4 = 5
  - dp[3] = max(3, 5) = 5
  
i=4: 值5
  - 不選：dp[4] = dp[3] = 5
  - 選：向前查找
    - j=3 (值4): 5-4=1 ≤ 2 ❌ 衝突
    - j=2 (值3): 5-3=2 ≤ 2 ❌ 衝突
    - j=1 (值2): 5-2=3 > 2 ✅ 不衝突！
  - 選：dp[1] + 5 = 2 + 5 = 7
  - dp[4] = max(5, 7) = 7
  
最終答案：7 (選擇 2 和 5)
```

### 解法 1：使用輔助函式

這個解法將「尋找不衝突位置」的邏輯抽取成獨立函式，提高程式碼的可讀性和可維護性。

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

// 輔助函式：向前遍歷尋找不衝突的位置
private int FindLastValidIndex(int[] sortedDamages, int index)
{
    int currentDamage = sortedDamages[index];
    
    // 從 index-1 開始向前遍歷
    for (int i = index - 1; i >= 0; i--)
    {
        // 如果差距 > 2，表示不衝突
        if (currentDamage - sortedDamages[i] > 2)
        {
            return i;  // 找到了！返回這個位置
        }
        // 否則繼續向前找
    }
    
    return -1;  // 所有位置都衝突
}
```

#### 解法 1 的設計優勢

##### 職責分離

- 主函式負責 DP 邏輯
- `FindLastValidIndex` 專門處理衝突判斷
- 符合單一職責原則

##### 易於理解

- 函式名稱清楚表達意圖
- 向前遍歷的邏輯獨立封裝
- 降低主函式的複雜度

##### 方便測試

- 可以單獨測試 `FindLastValidIndex`
- 便於驗證邊界情況
- 修改不影響主邏輯

##### 向前遍歷的實作細節

```text
範例：sortedDamages = [1, 3, 4, 7, 9], index = 4 (值9)

FindLastValidIndex(sortedDamages, 4):
  currentDamage = 9
  
  迴圈 i=3: sortedDamages[3] = 7
    9 - 7 = 2 ≤ 2  ❌ 衝突，繼續
    
  迴圈 i=2: sortedDamages[2] = 4
    9 - 4 = 5 > 2  ✅ 不衝突！
    return 2
    
這表示選擇傷害值 9 時，可以加上 dp[2] 的最大傷害
```

**特點總結**：

- ✅ 使用獨立的 `FindLastValidIndex` 函式尋找不衝突位置
- ✅ 程式碼結構清晰，職責分離
- ✅ 易於測試和維護
- ✅ 向前遍歷邏輯清晰易懂
- ✅ 適合團隊協作和大型專案

### 解法 2：內嵌式實作

這個解法將向前遍歷的邏輯直接嵌入 DP 迴圈中，程式碼更緊湊，執行效率更高。

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
        
        // 內嵌尋找不衝突位置：向前遍歷
        int j = i - 1;
        while (j >= 0 && damages[i].Damage - damages[j].Damage <= 2)
        {
            j--;  // 衝突，繼續向前找
        }
        
        long chooseCurrentDamage = (j >= 0 ? dp[j] : 0) + damages[i].Total;
        dp[i] = Math.Max(dp[i], chooseCurrentDamage);
    }
    
    return dp[n - 1];
}
```

#### 解法 2 的設計優勢

##### 程式碼緊湊

- 邏輯集中在一個函式內
- 減少函式呼叫的開銷
- 適合競賽和快速開發

##### 使用匿名型別

```csharp
var damages = damageMap.Select(kv => new { Damage = kv.Key, Total = kv.Value })
```

- 更清晰的資料結構
- `damages[i].Damage` 和 `damages[i].Total` 語意明確
- 避免使用多個陣列或複雜的索引

##### 內嵌向前遍歷的邏輯

```text
範例：damages = [{1,1}, {2,2}, {3,3}, {4,4}, {5,5}], i = 4 (值5)

內嵌的 while 迴圈：
  j = 3: damages[4].Damage - damages[3].Damage = 5 - 4 = 1 ≤ 2  ❌ 衝突
    j--  (j = 2)
  
  j = 2: damages[4].Damage - damages[2].Damage = 5 - 3 = 2 ≤ 2  ❌ 衝突
    j--  (j = 1)
  
  j = 1: damages[4].Damage - damages[1].Damage = 5 - 2 = 3 > 2  ✅ 不衝突！
    跳出迴圈
  
  計算：chooseCurrentDamage = dp[1] + damages[4].Total = 2 + 5 = 7
  dp[4] = max(dp[3], 7) = max(5, 7) = 7
```

##### 為什麼這裡也是向前遍歷？

關鍵原因與解法 1 相同：

1. **排序保證**：`damages` 已按 `Damage` 由小到大排序
2. **DP 狀態**：`dp[j]` (j < i) 都已計算完成
3. **衝突檢查**：只需檢查左側（較小）的值
4. **提早終止**：找到第一個不衝突的位置就可以停止

**與解法 1 的差異**：

| 項目 | 解法 1 | 解法 2 |
|------|--------|--------|
| 向前遍歷位置 | 獨立函式 `FindLastValidIndex` | 內嵌 `while` 迴圈 |
| 程式碼行數 | 較多（函式分離） | 較少（邏輯集中） |
| 可讀性 | 函式名稱清楚 | 需理解整體邏輯 |
| 效能 | 有函式呼叫開銷 | 無額外呼叫 |
| 適用場景 | 團隊協作、大型專案 | 競賽、原型開發 |

**特點總結**：

- ✅ 邏輯更緊湊，直接在迴圈內處理
- ✅ 減少函式呼叫開銷，效能更好
- ✅ 使用匿名型別提高可讀性
- ✅ 向前遍歷邏輯清晰（while 迴圈）
- ✅ 適合追求極致效能的場景

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
