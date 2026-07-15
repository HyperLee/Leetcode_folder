# LeetCode 875：Koko Eating Bananas／愛吃香蕉的珂珂

[英文題目](https://leetcode.com/problems/koko-eating-bananas/) ·
[中文題目](https://leetcode.cn/problems/koko-eating-bananas/)

這個 .NET 10 主控台專案實作 LeetCode 875：找出珂珂能在 `h` 小時內吃完所有香蕉堆的
最小整數速度。公開方法 `MinEatingSpeed` 只負責計算；`Main` 集中所有 acceptance
harness 輸出。

## 題目說明

每小時珂珂會選一堆香蕉並吃掉最多 `k` 根；若該堆少於 `k` 根，她會吃完那一堆，
但這一小時不再吃其他堆。給定每堆香蕉數量 `piles` 與可用時數 `h`，回傳能準時
吃完的最小速度 `k`。

## 題目限制

- `1 <= piles.length <= 10^4`。
- `piles.length <= h <= 10^9`。
- `1 <= piles[i] <= 10^9`。

實作只處理 LeetCode 的有效輸入契約，不額外加入 invalid-input 行為。

## 解法：答案範圍的下界二分搜尋

速度至少為 `1`，而速度達到最大香蕉堆 `piles.Max()` 時，每堆最多只需一小時，
因此答案必定位於 `[1, piles.Max()]`。`MinEatingSpeed` 在這個範圍找第一個滿足
`CalculateRequiredHours(piles, speed) <= h` 的速度。

二分搜尋維持的下界不變量如下：

1. `high` 始終保留一個可行速度；當中點可行時，令 `high = speed`，繼續尋找更小答案。
2. 當中點不可行時，該速度及更慢速度都不可能是答案，因此令 `low = speed + 1`。
3. 迴圈結束時 `low == high`，即為最小可行速度。

每一堆需要的時數是 `ceil(pile / speed)`。實作使用
`((long)pile + speed - 1) / speed`，先轉成 `long` 再相加，避免
`pile + speed - 1` 或總時數在極端輸入下發生 `int` 溢位。

### 取捨

若從速度 `1` 逐一嘗試，最壞需要檢查十億個速度。二分搜尋只需對答案範圍做
`log M` 次檢查，每次線性掃描所有香蕉堆；它保留簡單的常數額外空間，同時能處理
題目上限。專案不保留同功能的 `MinEatingSpeed2`，避免重複解法與不同溢位行為。

## 複雜度

令 `n = piles.Length`，`M = piles.Max()`：

- 時間複雜度：`O(n log M)`。
- 輔助空間複雜度：`O(1)`。
- 結果空間複雜度：`O(1)`。

## 逐步走查

以 `piles = [3,6,7,11]`、`h = 8` 為例：

```plaintext
初始範圍：[1, 11]
speed = 6，需要 6 小時，可行，high = 6
speed = 3，需要 10 小時，不可行，low = 4
speed = 5，需要 8 小時，可行，high = 5
speed = 4，需要 8 小時，可行，high = 4
low == high == 4，因此最小速度為 4
```

## 可執行驗證

專案不建立正式測試專案；`Main` 的確定性驗收程式是目前的驗證機制。任一失敗都
會設定 `Environment.ExitCode = 1`。

| # | 輸入 | 預期 | 驗證重點 |
|---:|---|---:|---|
| 1 | `[3,6,7,11]`, `h=8` | `4` | 官方範例一 |
| 2 | `[30,11,23,4,20]`, `h=5` | `30` | 官方範例二，答案為最大堆 |
| 3 | `[30,11,23,4,20]`, `h=6` | `23` | 官方範例三 |
| 4 | `[1]`, `h=1` | `1` | 最小有效輸入 |
| 5 | `[1,1,1,1]`, `h=10^9` | `1` | 極大可用時數 |
| 6 | `[2,8,4]`, `h=3` | `8` | 每堆恰好只能使用一小時 |
| 7 | `[5,5]`, `h=3` | `5` | 整數上取整邊界 |
| 8 | `[1,1,1,10^9]`, `h=10^9` | `2` | 大數與溢位回歸 |
| 9 | 一萬堆、每堆 `10^9`，`h=10^9` | `10000` | 香蕉堆數與堆大小上限 |

## 建置與執行

請把外層 `leetcode_875/` 當成工作目錄：

```bash
dotnet build leetcode_875/leetcode_875.csproj --nologo
dotnet run --no-build --project leetcode_875/leetcode_875.csproj
```

## 最新驗證輸出

以下是執行第二個命令的完整輸出，也是本 README 唯一的 `text` fence：

```text
LeetCode 875 acceptance harness

Case 1: Official example 1
Input: piles = [3,6,7,11], h = 8
Expected: 4
Actual: 4
PASS

Case 2: Official example 2
Input: piles = [30,11,23,4,20], h = 5
Expected: 30
Actual: 30
PASS

Case 3: Official example 3
Input: piles = [30,11,23,4,20], h = 6
Expected: 23
Actual: 23
PASS

Case 4: Single pile
Input: piles = [1], h = 1
Expected: 1
Actual: 1
PASS

Case 5: Many available hours
Input: piles = [1,1,1,1], h = 1000000000
Expected: 1
Actual: 1
PASS

Case 6: One hour per pile
Input: piles = [2,8,4], h = 3
Expected: 8
Actual: 8
PASS

Case 7: Ceiling division boundary
Input: piles = [5,5], h = 3
Expected: 5
Actual: 5
PASS

Case 8: Overflow regression
Input: piles = [1,1,1,1000000000], h = 1000000000
Expected: 2
Actual: 2
PASS

Case 9: Maximum constraints
Input: 10,000 piles, each 1000000000; h = 1000000000
Expected: 10000
Actual: 10000
PASS

Summary: 9/9 checks passed.
```

## 專案結構

```plaintext
leetcode_875/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   ├── readme-template.md
│   └── superpowers/
│       ├── plans/
│       │   └── 2026-07-15-leetcode-875-net10-migration.md
│       └── specs/
│           └── 2026-07-15-leetcode-875-net10-migration-design.md
└── leetcode_875/
    ├── Program.cs
    └── leetcode_875.csproj
```
