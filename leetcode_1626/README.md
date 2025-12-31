# LeetCode 1626. Best Team With No Conflicts

> 無衝突的最佳球隊 - 使用排序與動態規劃解決

## 題目描述

你是籃球隊的經理。為了即將到來的比賽，你想挑選一支**總分最高**的隊伍。隊伍的分數為隊內所有球員分數之和。

但隊伍中不得出現**衝突**：當**年輕球員的分數嚴格高於年長球員**時，視為衝突。同齡球員之間不算衝突。

給定兩個陣列 `scores` 和 `ages`，其中 `scores[i]` 與 `ages[i]` 分別表示第 i 位球員的分數與年齡，請回傳所有可能隊伍中能取得的**最高總分**。

### 範例

**範例 1：**
```
輸入：scores = [1,3,5,10,15], ages = [1,2,3,4,5]
輸出：34
解釋：可以選擇所有球員組成隊伍，總分 = 1 + 3 + 5 + 10 + 15 = 34
```

**範例 2：**
```
輸入：scores = [4,5,6,5], ages = [2,1,2,1]
輸出：16
解釋：選擇最後 3 位球員最佳，總分 = 5 + 6 + 5 = 16
```

**範例 3：**
```
輸入：scores = [1,2,3,5], ages = [8,9,10,1]
輸出：6
解釋：選擇年齡為 1 的球員，分數為 5；或選擇 1+2+3=6 的組合
```

### 限制條件

- `1 <= scores.length, ages.length <= 1000`
- `scores.length == ages.length`
- `1 <= scores[i] <= 10^6`
- `1 <= ages[i] <= 1000`

## 解題思路

### 關鍵觀察

1. **衝突的本質**：若球員 A 比球員 B 年輕，但 A 的分數卻嚴格高於 B，則產生衝突
2. **排序轉化**：若將球員依年齡排序，問題轉化為「選擇一個**分數非遞減**的子序列，使其總和最大」
3. **類似 LIS**：這是「最長遞增子序列 (Longest Increasing Subsequence)」的變形，但目標是**最大總和**而非最大長度

### 為什麼排序後只需考慮分數？

排序後，對於任意兩位被選中的球員 `players[i]` 和 `players[j]`（其中 `j < i`）：
- 已知 `age[j] <= age[i]`（排序保證）
- 若要無衝突，需滿足：若 `age[j] < age[i]`，則 `score[j] <= score[i]`
- 因此只需確保選出的子序列分數**非遞減**即可

### 演算法步驟

```
步驟 1：將球員依 (年齡, 分數) 升序排序
步驟 2：建立 DP 陣列，dp[i] = 以 players[i] 結尾的最大分數總和
步驟 3：對每位球員 i，檢查所有 j < i，若 score[j] <= score[i]，則可延伸
步驟 4：回傳 dp 陣列中的最大值
```

## 詳細演示

以 `scores = [4,5,6,5], ages = [2,1,2,1]` 為例：

### Step 1: 建立球員資料並排序

| 原始索引 | 年齡 | 分數 |
|---------|------|------|
| 0 | 2 | 4 |
| 1 | 1 | 5 |
| 2 | 2 | 6 |
| 3 | 1 | 5 |

**排序後**（先依年齡，再依分數）：

| 排序索引 | 年齡 | 分數 |
|---------|------|------|
| 0 | 1 | 5 |
| 1 | 1 | 5 |
| 2 | 2 | 4 |
| 3 | 2 | 6 |

### Step 2: 動態規劃過程

**初始化：** `dp[0] = 5`（第一位球員單獨成隊）

**i = 1：** 檢查 j = 0
- `score[0]=5 <= score[1]=5` ✓ 可延伸
- `dp[1] = max(0, dp[0]) + score[1] = 5 + 5 = 10`

**i = 2：** 檢查 j = 0, 1
- `score[0]=5 > score[2]=4` ✗ 不可延伸（會衝突）
- `score[1]=5 > score[2]=4` ✗ 不可延伸
- `dp[2] = 0 + 4 = 4`（只能自己成隊）

**i = 3：** 檢查 j = 0, 1, 2
- `score[0]=5 <= score[3]=6` ✓ 可延伸，maxPrevSum = 5
- `score[1]=5 <= score[3]=6` ✓ 可延伸，maxPrevSum = max(5, 10) = 10
- `score[2]=4 <= score[3]=6` ✓ 可延伸，maxPrevSum = max(10, 4) = 10
- `dp[3] = 10 + 6 = 16`

### Step 3: 取得結果

```
dp = [5, 10, 4, 16]
答案 = max(dp) = 16
```

## 複雜度分析

| 項目 | 複雜度 |
|------|--------|
| 時間複雜度 | O(n²) |
| 空間複雜度 | O(n) |

> [!NOTE]
> 此題可使用 Binary Indexed Tree (BIT) 或線段樹優化至 O(n log n)，但 O(n²) 解法在題目限制下已足夠。

## 執行方式

### 前置需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### 建構與執行

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_1626/leetcode_1626.csproj
```

### 預期輸出

```
測試案例 1: 34
測試案例 2: 16
測試案例 3: 6
```

## 相關連結

- [LeetCode 題目連結 (英文)](https://leetcode.com/problems/best-team-with-no-conflicts/)
- [LeetCode 題目連結 (中文)](https://leetcode.cn/problems/best-team-with-no-conflicts/)

## 相關題目

- [300. Longest Increasing Subsequence](https://leetcode.com/problems/longest-increasing-subsequence/)
- [354. Russian Doll Envelopes](https://leetcode.com/problems/russian-doll-envelopes/)
