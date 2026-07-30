# LeetCode 650：只有兩個鍵的鍵盤

這是一個以 C# 與 .NET 10 撰寫的主控台教學專案，示範如何把 Copy All 與 Paste 的操作序列
轉換成因數拆分問題。專案保留三種互補解法：由小到大的因數動態規劃、自頂向下的記憶化
遞迴，以及直接累加質因數的數學解法。

- [LeetCode English：650. 2 Keys Keyboard](https://leetcode.com/problems/2-keys-keyboard/)
- [LeetCode 中文：650. 只有兩個鍵的鍵盤](https://leetcode.cn/problems/2-keys-keyboard/)

## 題目說明

畫面一開始只有一個字元 `A`，鍵盤只能執行兩種操作：

1. **Copy All**：複製畫面上目前所有的 `A`。
2. **Paste**：貼上剪貼簿中的內容。

給定整數 `n`，求出讓畫面上恰好出現 `n` 個 `A` 所需的最少操作數。

以 `n = 3` 為例，可以依序執行：

```text
初始：A
Copy All：A（剪貼簿為 A）
Paste：AA
Paste：AAA
```

總共需要 3 次操作，因此答案為 `3`。

## 限制條件與 API 前提

- `1 <= n <= 1000`
- 初始畫面已有一個 `A`，所以 `n = 1` 不需要任何操作。
- Copy All 必定複製畫面上的全部內容，不能只複製其中一部分。
- 三個公開方法只處理 LeetCode 定義的有效輸入，不另外定義範圍外輸入行為。
- `MinSteps`、`MinSteps2` 與 `MinSteps3` 都只回傳最少操作數，不讀寫主控台狀態。

## 解題概念與出發點

每次 Copy All 之後，若接著 Paste `k - 1` 次，畫面上的 `A` 數量就會變成原本的 `k` 倍；
這一段操作的成本是：

```text
1 次 Copy All + (k - 1) 次 Paste = k 次操作
```

因此，一連串操作可視為把初始數量 1 依序乘上若干因數。例如：

```text
1 × 2 × 2 × 3 = 12
```

對應的操作成本為 `2 + 2 + 3 = 7`。問題因而轉換成：如何將 `n` 拆成乘法因數，使因數總和
最小。

若某個操作因數是合數 `a × b`，其中 `a, b >= 2`，把它拆成兩段的成本不會更高：

```text
a + b <= a × b
```

所以最佳分組最終可以拆成質因數。三種解法分別從 DP、遞迴與數學角度使用這個觀察。

## 解法比較

| 方法 | 核心做法 | 時間複雜度 | 輔助空間 |
| --- | --- | --- | --- |
| `MinSteps` | Bottom-up DP，枚舉成對因數進行狀態轉移 | `O(n√n)` | `O(n)` |
| `MinSteps2` | Top-down 遞迴拆因數，以 memo 重用子問題 | 最寬鬆上界 `O(n√n)` | `O(n)` |
| `MinSteps3` | 逐一拆出質因數並累加 | `O(√n)` | `O(1)` |

`MinSteps2` 實際只會計算遞迴拆分時遇到的目標；表中的複雜度使用不超過所有 `1..n` 狀態
都被處理的寬鬆上界。三個方法都只回傳一個整數，因此結果空間皆為 `O(1)`。

## 解法一：因數動態規劃 `MinSteps`

### 設計說明

定義 `minimumSteps[i]` 為從初始的一個 `A` 得到恰好 `i` 個 `A` 的最少操作數。

- `minimumSteps[1] = 0`，因為初始畫面已經符合目標。
- 對每個 `current = 2..n`，先把答案設為尚未找到。
- 枚舉不超過 `√current` 的因數 `factor`。
- 若 `factor` 可以整除 `current`，另一個成對因數為
  `complementaryFactor = current / factor`。
- 可以先得到 `factor` 個 A，再用 `complementaryFactor` 次操作放大；也可以反向先得到
  `complementaryFactor` 個 A，再用 `factor` 次操作放大：

```text
minimumSteps[current] =
    min(
        minimumSteps[factor] + complementaryFactor,
        minimumSteps[complementaryFactor] + factor
    )
```

因數總是成對出現，只需掃描到平方根就能涵蓋兩個方向。由於狀態依數量由小到大計算，
轉移時需要的較小狀態都已經完成。

### `n = 12` 演示流程

計算到 `current = 12` 時，可用的因數拆分如下：

| 拆分 | 先完成的子問題 | 放大成本 | 總成本 |
| --- | ---: | ---: | ---: |
| `1 × 12` | `minimumSteps[1] = 0` | 12 | 12 |
| `2 × 6` | `minimumSteps[2] = 2` | 6 | 8 |
| `6 × 2` | `minimumSteps[6] = 5` | 2 | 7 |
| `3 × 4` | `minimumSteps[3] = 3` | 4 | 7 |
| `4 × 3` | `minimumSteps[4] = 4` | 3 | 7 |

最小值為 `7`。例如先用 5 次操作得到 6 個 A，再執行一次 Copy All 與一次 Paste，就能以
2 次操作把數量放大為 12。

## 解法二：記憶化遞迴 `MinSteps2`

### 設計說明

這個方法從目標 `n` 向下拆解，而不是先計算所有較小狀態：

1. `MinSteps2` 建立 memo，先記錄基底 `memo[1] = 0`。
2. `MinStepsMemo(target, memo)` 若找到快取，直接回傳。
3. 先以 `target` 作為安全上界；這代表從一個 A 執行一次 Copy All，再 Paste
   `target - 1` 次。
4. 枚舉 `target` 的成對因數，遞迴計算兩個拆分方向。
5. 保存最小結果，讓後續相同子問題不需重算。

質數沒有 1 與自身以外的因數，因此會保留初始上界 `target`；合數則可能透過因數拆分降低
成本。

### `n = 12` 演示流程

```text
MinStepsMemo(12)
├─ 預設上界：12
├─ 拆成 2 × 6
│  ├─ MinStepsMemo(2) + 6 = 2 + 6 = 8
│  └─ MinStepsMemo(6) + 2 = 5 + 2 = 7
└─ 拆成 3 × 4
   ├─ MinStepsMemo(3) + 4 = 3 + 4 = 7
   └─ MinStepsMemo(4) + 3 = 4 + 3 = 7
```

最後保存 `memo[12] = 7`。其中 `2`、`3`、`4`、`6` 的結果也會被快取，其他遞迴分支遇到
相同目標時可以直接重用。

## 解法三：質因數分解 `MinSteps3`

### 設計說明

最佳操作分組可以完全拆成質因數，因此不需要建立 DP 狀態：

1. 從因數 2 開始試除目前剩餘值。
2. 每成功除掉一次因數，就把該因數加入答案。
3. 同一因數可能重複出現，因此持續相除直到不能整除。
4. 當試除完成後，若剩餘值大於 1，它就是最後一個質因數，直接加入答案。

迴圈條件使用 `factor <= remaining / factor`，避免以 `factor * factor` 比較時可能發生的整數
乘法溢位。雖然本題上限只有 1000，這個寫法仍能清楚表達安全的平方根界線。

### `n = 12` 演示流程

```text
remaining = 12, answer = 0
12 ÷ 2 = 6  → answer = 0 + 2 = 2
 6 ÷ 2 = 3  → answer = 2 + 2 = 4
remaining = 3，是最後一個質因數
answer = 4 + 3 = 7
```

質因數分解為 `12 = 2 × 2 × 3`，對應操作成本 `2 + 2 + 3 = 7`。

## Acceptance Harness

專案目前沒有獨立測試專案，因此 `Main` 會執行固定的 acceptance harness。每個案例都會以
相同輸入呼叫三個公開方法，只有三個結果都等於人工推導的預期值時才顯示 `PASS`。任何案例
失敗都會將 process exit code 設為 1。

| 案例 | `n` | 預期 | 驗證重點 |
| --- | ---: | ---: | --- |
| 輸入下界 | 1 | 0 | 初始畫面已符合目標 |
| 最小 Copy/Paste 操作 | 2 | 2 | 最小非零答案 |
| 官方範例／質數 | 3 | 3 | 官方案例與質數行為 |
| 2 的冪次 | 4 | 4 | 重複因數 |
| 一般合數 | 6 | 5 | 不同質因數 |
| 完全平方數 | 9 | 6 | 平方根因數 |
| 多種因數拆分 | 12 | 7 | 多條最佳拆分 |
| 大質數 | 997 | 997 | 大型質數 |
| 輸入上界／重複質因數 | 1000 | 21 | 上界與重複質因數 |

## 建置與執行

請從此 README 所在的題目 repository root 執行：

```bash
dotnet restore leetcode_650/leetcode_650.csproj
dotnet build leetcode_650/leetcode_650.csproj --nologo --no-restore
dotnet run --no-build --project leetcode_650/leetcode_650.csproj
```

本次驗證的建置結果為成功，並顯示 `0 個警告`、`0 個錯誤`。以下是 fresh run 的完整輸出：

```text
案例：輸入下界
輸入 n = 1
預期：0
MinSteps：0
MinSteps2：0
MinSteps3：0
結果：PASS

案例：最小 Copy/Paste 操作
輸入 n = 2
預期：2
MinSteps：2
MinSteps2：2
MinSteps3：2
結果：PASS

案例：官方範例／質數
輸入 n = 3
預期：3
MinSteps：3
MinSteps2：3
MinSteps3：3
結果：PASS

案例：2 的冪次
輸入 n = 4
預期：4
MinSteps：4
MinSteps2：4
MinSteps3：4
結果：PASS

案例：一般合數
輸入 n = 6
預期：5
MinSteps：5
MinSteps2：5
MinSteps3：5
結果：PASS

案例：完全平方數
輸入 n = 9
預期：6
MinSteps：6
MinSteps2：6
MinSteps3：6
結果：PASS

案例：多種因數拆分
輸入 n = 12
預期：7
MinSteps：7
MinSteps2：7
MinSteps3：7
結果：PASS

案例：大質數
輸入 n = 997
預期：997
MinSteps：997
MinSteps2：997
MinSteps3：997
結果：PASS

案例：輸入上界／重複質因數
輸入 n = 1000
預期：21
MinSteps：21
MinSteps2：21
MinSteps3：21
結果：PASS

總結：9/9 組案例通過。
```

## 專案結構

```text
.
├── leetcode_650/
│   ├── Program.cs              # 題目敘述、三種解法與 acceptance harness
│   └── leetcode_650.csproj     # .NET 10 主控台專案設定
├── docs/
│   └── readme-template.md      # README 初始撰寫範本
├── AGENTS.md                   # 專案開發與安全規範
└── README.md
```
