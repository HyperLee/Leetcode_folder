# 643. Maximum Average Subarray I｜子陣列最大平均數 I

## 題目連結

- English: [LeetCode 643. Maximum Average Subarray I](https://leetcode.com/problems/maximum-average-subarray-i/)
- 中文：[LeetCode 643. 子陣列最大平均數 I](https://leetcode.cn/problems/maximum-average-subarray-i/)

## 題意與限制

給定整數陣列 `nums` 與整數 `k`，找出所有長度恰為 `k` 的連續子陣列，並回傳其中最大的平均值。

- `1 <= k <= n <= 100000`
- 每個 `nums[i]` 的值域為 `-10000..10000`

題目保證輸入有效；本實作不為題目契約以外的無效輸入新增行為。

## 解法：固定長度滑動視窗

先計算前 `k` 個元素的總和，作為第一個視窗。之後每次將視窗向右移動一格時，只需加入新進入右端的元素，並減去離開左端的元素；同時更新最大的視窗總和。最後將最大總和除以 `k`，即可得到最大平均值。

## 核心不變量與容易出錯處

- 第一個視窗必須先完整累加前 `k` 個值，才能作為後續比較的基準。
- 每次右移後，`currentSum` 恆等於目前長度為 `k` 的連續視窗總和：加入 entering value，減去 leaving value。
- 右移時要移除 `nums[right - k]`；少減或錯減索引會讓視窗長度不再固定。
- 最大值要比較的是視窗總和，最後再做一次浮點數除法，避免在每個步驟重複處理平均值。
- 演算法只讀取 `nums`，不會改動呼叫端傳入的陣列。

## 複雜度

- 時間複雜度：`O(n)`。第一個視窗與後續每次右移合計只走訪陣列一次。
- 額外空間複雜度：`O(1)`。只維護目前總和與最大總和等固定數量的變數。

## 逐步範例

以 `nums = [1, 12, -5, -6, 50, 3]`、`k = 4` 為例：

```plaintext
first window [1, 12, -5, -6] = 2
move right: 2 + 50 - 1 = 51
move right: 51 + 3 - 12 = 42
maxSum = 51; 51 / 4 = 12.75
```

三個長度為 4 的視窗總和依序為 `2`、`51`、`42`，因此最大平均值為 `12.75`。

## Acceptance harness

本題沒有正式測試專案；`Program.Main` 是確定性的 acceptance harness，會列印每個案例的輸入、預期值、實際值與 PASS/FAIL，並在任一案例失敗時設定非零結束碼。

| 案例名稱 | 輸入 | 預期結果 |
| --- | --- | --- |
| Official example | `nums = [1, 12, -5, -6, 50, 3], k = 4` | `12.75` |
| Minimum valid input | `nums = [5], k = 1` | `5` |
| k = 1 regression | `nums = [-1, -2], k = 1` | `-1` |
| All negative values | `nums = [-8, -6, -7], k = 2` | `-6.5` |
| k equals n | `nums = [7, -3, 10], k = 3` | `4.666667` |
| Last window wins | `nums = [0, 4, 0, 3, 2], k = 2` | `2.5` |
| Upper-bound spot check | `nums = [10000 x 100000], k = 100000` | `10000` |
| Input remains unchanged | `nums = [1, 12, -5, -6, 50, 3], k = 4` | `average = 12.75; nums = [1, 12, -5, -6, 50, 3]` |

## 建置與執行

在 `leetcode_643` 題目根目錄執行：

```bash
dotnet build leetcode_643/leetcode_643.csproj --nologo
dotnet run --no-build --project leetcode_643/leetcode_643.csproj
```

## 實際執行輸出

以下為本 README 撰寫前，以目前專案執行所取得的完整輸出：

```text
Case: Official example
Input: nums = [1, 12, -5, -6, 50, 3], k = 4
Expected: 12.75
Actual: 12.75
PASS
Case: Minimum valid input
Input: nums = [5], k = 1
Expected: 5
Actual: 5
PASS
Case: k = 1 regression
Input: nums = [-1, -2], k = 1
Expected: -1
Actual: -1
PASS
Case: All negative values
Input: nums = [-8, -6, -7], k = 2
Expected: -6.5
Actual: -6.5
PASS
Case: k equals n
Input: nums = [7, -3, 10], k = 3
Expected: 4.666667
Actual: 4.666667
PASS
Case: Last window wins
Input: nums = [0, 4, 0, 3, 2], k = 2
Expected: 2.5
Actual: 2.5
PASS
Case: Upper-bound spot check
Input: nums = [10000 x 100000], k = 100000
Expected: 10000
Actual: 10000
PASS
Case: Input remains unchanged
Input: nums = [1, 12, -5, -6, 50, 3], k = 4
Expected: average = 12.75; nums = [1, 12, -5, -6, 50, 3]
Actual: average = 12.75; nums = [1, 12, -5, -6, 50, 3]
PASS
Summary: 8/8 checks passed.
```

## 最終專案結構

```plaintext
leetcode_643/
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
│       │   └── 2026-07-13-leetcode-643-net10-migration-implementation-plan.md
│       └── specs/
│           └── 2026-07-13-leetcode-643-net10-migration-design.md
└── leetcode_643/
    ├── Program.cs
    └── leetcode_643.csproj
```
