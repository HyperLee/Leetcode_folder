# LeetCode 2029 — 石子遊戲 IX（Stone Game IX）

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![LeetCode Medium](https://img.shields.io/badge/LeetCode-Medium-F89F1B?logo=leetcode)

這是一個以 .NET 10 Console App 實作的教學專案。程式保留三種 O(n) 寫法，並透過可直接執行的固定案例比較三者結果。

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [三種解法](#三種解法)
- [方法比較](#方法比較)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

- 英文題目：[2029. Stone Game IX](https://leetcode.com/problems/stone-game-ix/description/)
- 中文題目：[2029. 石子遊戲 IX](https://leetcode.cn/problems/stone-game-ix/description/)

Alice 與 Bob 面前有一排石子，每顆石子都有一個正整數數值。兩人輪流移除一顆石子，由 Alice 先手。

- 若某位玩家移除石子後，所有已移除石子的數值總和可以被 3 整除，該玩家立刻輸掉。
- 如果移除後沒有剩餘石子，而且沒有觸發上述整除規則，則 Bob 直接獲勝，即使最後一步是 Alice 取走的。
- 兩位玩家都採取最佳策略；目標是判斷 Alice 是否有必勝策略。

## 限制條件

- `1 <= stones.Length <= 100000`
- `1 <= stones[i] <= 10000`
- 輸入陣列不會被三個解法修改。

## 解題概念與出發點

### 1. 原始數值可以縮成三種餘數

勝負只與累加和是否為 3 的倍數有關，因此每顆石子的實際數值並不重要，只需統計：

- `cnt[0]`：除以 3 餘 0 的石子數量。
- `cnt[1]`：除以 3 餘 1 的石子數量。
- `cnt[2]`：除以 3 餘 2 的石子數量。

如此便把最多 100000 顆石子的博弈，化簡成三個計數值的關係。

### 2. 餘數 0 只改變回合歸屬

當目前累加和的餘數是 1 或 2 時，拿走餘數 0 的石子不會改變累加和餘數，但會消耗一個回合。因此：

- `cnt[0]` 是偶數時，餘數 0 的石子可以成對抵銷，不改變原本的先後手優勢。
- `cnt[0]` 是奇數時，多出的一顆餘數 0 石子會交換先後手優勢。

### 3. 最終勝負條件

設三種餘數數量分別為 `cnt0`、`cnt1`、`cnt2`：

```text
cnt0 為偶數：cnt1 > 0 且 cnt2 > 0
cnt0 為奇數：|cnt1 - cnt2| > 2
```

偶數情況下，Alice 必須能從一種非零餘數開始，並利用另一種非零餘數迫使 Bob 先組成 3 的倍數；缺少任何一類都無法完成這個安排。

奇數情況下，額外的餘數 0 石子會交換回合優勢，因此其中一種非零餘數必須比另一種至少多 3 顆，Alice 才能維持安全取法並將必敗回合留給 Bob。

## 三種解法

三個公開方法都使用上述同一個數學結論；差異在於統計方式、條件拆解方式，以及程式碼強調的教學重點。

### 解法一：三個獨立計數器 — `StoneGameIX`

#### 設計

1. 使用 `cnt0`、`cnt1`、`cnt2` 三個變數逐顆統計餘數。
2. 如果 `cnt0` 為偶數，檢查 `cnt1` 與 `cnt2` 是否都至少為 1。
3. 如果 `cnt0` 為奇數，分別檢查 `cnt1 - cnt2 > 2` 與 `cnt2 - cnt1 > 2`。

這種寫法將兩個可能的優勢方向完整展開，適合第一次閱讀勝負條件時逐項對照。

#### 範例演示

輸入：`[3,1,1,1,1,2]`

1. 餘數分類為 `cnt0 = 1`、`cnt1 = 4`、`cnt2 = 1`。
2. `cnt0` 是奇數，進入數量差判斷。
3. `cnt1 - cnt2 = 4 - 1 = 3`，符合大於 2。
4. Alice 有必勝策略，回傳 `true`。

#### 複雜度

- 時間複雜度：O(n)。
- 額外空間複雜度：O(1)。

### 解法二：分類判斷與命名輔助方法 — `StoneGameIX2`

#### 設計

1. 使用長度固定為 3 的 `cnt` 陣列統計餘數。
2. `cnt[0]` 為偶數時，直接檢查另外兩類是否都存在。
3. `cnt[0]` 為奇數時，分別呼叫 `HasWinningImbalance(cnt[1], cnt[2])` 與反向版本。
4. `HasWinningImbalance` 將「候選餘數是否比另一類多至少 3 顆」表達為一個具名判斷，使數學條件的意圖比裸運算式更明確。

> [!NOTE]
> 此方法原先以最大回合數近似博弈流程，會在 `[1,1,2,2]` 等案例產生錯誤結果；目前已改為完整且可由回歸案例驗證的勝負條件。

#### 範例演示

輸入：`[1,1,2,2]`

1. 餘數分類為 `cnt[0] = 0`、`cnt[1] = 2`、`cnt[2] = 2`。
2. `cnt[0]` 是偶數。
3. 餘數 1 與餘數 2 都存在，因此 Alice 可以選擇安全開局，迫使 Bob 先取出令總和可被 3 整除的石子。
4. 回傳 `true`。這也是防止舊版錯誤再次出現的回歸案例。

#### 複雜度

- 時間複雜度：O(n)。
- 額外空間複雜度：O(1)，因為計數陣列長度固定為 3。

### 解法三：陣列計數與絕對值公式 — `StoneGameIX3`

#### 設計

1. 同樣使用長度為 3 的陣列統計餘數。
2. 偶數情況沿用「兩種非零餘數都必須存在」的條件。
3. 奇數情況用 `Math.Abs(cnt[1] - cnt[2]) > 2` 同時涵蓋兩個優勢方向。

這是三種寫法中最精簡的版本，適合已理解數學推導後使用。

#### 範例演示

輸入：`[5,1,2,4,3]`

1. 各元素餘數依序為 `2, 1, 2, 1, 0`。
2. 得到 `cnt[0] = 1`、`cnt[1] = 2`、`cnt[2] = 2`。
3. `cnt[0]` 是奇數，計算 `|2 - 2| = 0`。
4. 數量差沒有大於 2，Alice 無法建立必勝優勢，回傳 `false`。

#### 複雜度

- 時間複雜度：O(n)。
- 額外空間複雜度：O(1)。

## 方法比較

| 方法 | 計數方式 | 勝負條件寫法 | 教學重點 | 時間 | 額外空間 |
| --- | --- | --- | --- | --- | --- |
| `StoneGameIX` | 三個獨立整數 | 展開兩個差值方向 | 最直接呈現所有分支 | O(n) | O(1) |
| `StoneGameIX2` | 固定長度陣列 | 具名輔助方法拆解奇數情況 | 強調條件意圖與回歸修正 | O(n) | O(1) |
| `StoneGameIX3` | 固定長度陣列 | `Math.Abs` 合併方向 | 最精簡的公式實作 | O(n) | O(1) |

## 專案結構

```text
leetcode_2029/
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_2029/
    ├── leetcode_2029.csproj
    └── Program.cs
```

## 建置與執行

需求：安裝支援 `net10.0` 的 .NET 10 SDK。

從本 README 所在目錄執行：

```bash
dotnet restore leetcode_2029/leetcode_2029.csproj
dotnet build leetcode_2029/leetcode_2029.csproj --no-restore
dotnet run --project leetcode_2029/leetcode_2029.csproj --no-build
```

Console harness 會執行 7 組案例與 3 個方法，共 21 項檢查。任何結果不符時，程式會設定非零結束碼，方便在終端機或 CI 中偵測失敗。

## 實際執行結果

以下內容來自上述 `dotnet run --project leetcode_2029/leetcode_2029.csproj --no-build` 的實際輸出：

```text
案例：官方範例 1：Alice 使 Bob 取到總和 3
Input: [2, 1]
  StoneGameIX: Expected=true, Actual=true, PASS
  StoneGameIX2: Expected=true, Actual=true, PASS
  StoneGameIX3: Expected=true, Actual=true, PASS

案例：官方範例 2：只有一顆石子
Input: [2]
  StoneGameIX: Expected=false, Actual=false, PASS
  StoneGameIX2: Expected=false, Actual=false, PASS
  StoneGameIX3: Expected=false, Actual=false, PASS

案例：官方範例 3：餘數數量平衡
Input: [5, 1, 2, 4, 3]
  StoneGameIX: Expected=false, Actual=false, PASS
  StoneGameIX2: Expected=false, Actual=false, PASS
  StoneGameIX3: Expected=false, Actual=false, PASS

案例：回歸案例：偶數個餘數 0 且兩類非零餘數都存在
Input: [1, 1, 2, 2]
  StoneGameIX: Expected=true, Actual=true, PASS
  StoneGameIX2: Expected=true, Actual=true, PASS
  StoneGameIX3: Expected=true, Actual=true, PASS

案例：奇數個餘數 0 且餘數 1 多三顆
Input: [3, 1, 1, 1, 1, 2]
  StoneGameIX: Expected=true, Actual=true, PASS
  StoneGameIX2: Expected=true, Actual=true, PASS
  StoneGameIX3: Expected=true, Actual=true, PASS

案例：奇數個餘數 0 但數量差不足
Input: [3, 1, 1, 2]
  StoneGameIX: Expected=false, Actual=false, PASS
  StoneGameIX2: Expected=false, Actual=false, PASS
  StoneGameIX3: Expected=false, Actual=false, PASS

案例：邊界案例：只有餘數 0
Input: [3, 6, 9]
  StoneGameIX: Expected=false, Actual=false, PASS
  StoneGameIX2: Expected=false, Actual=false, PASS
  StoneGameIX3: Expected=false, Actual=false, PASS

總結：21/21 項測試通過
```