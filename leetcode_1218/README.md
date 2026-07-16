# LeetCode 1218：Longest Arithmetic Subsequence of Given Difference／最長定差子序列

這個 .NET 10 主控台專案以兩種動態規劃策略解題，並由 `Main` 統一負責所有主控台輸出與驗收結果。公開方法 `LongestSubsequence(int[] arr, int difference)` 與 `LongestSubsequence2(int[] arr, int difference)` 都依輸入順序處理數值、不修改傳入陣列，也不寫入主控台。

## 題目連結

- [LeetCode（英文）](https://leetcode.com/problems/longest-arithmetic-subsequence-of-given-difference/)
- [LeetCode（中文）](https://leetcode.cn/problems/longest-arithmetic-subsequence-of-given-difference/)

## 題目定義與限制

給定整數陣列 `arr` 與整數 `difference`，找出最長子序列，使子序列中每一對相鄰元素的差都等於 `difference`。子序列可以刪除部分元素，但保留下來的元素不可改變原本順序。

- `1 <= arr.Length <= 10^5`
- `-10^4 <= arr[i], difference <= 10^4`

## 共用 DP 不變量

掃描到目前位置時，狀態 `dp[x]` 表示「在已掃描前綴中，以數值 `x` 結尾的合法子序列最佳長度」。目前值為 `x` 時，能銜接的前驅值必須是 `x - difference`，所以轉移式為：

`dp[x] = dp[x - difference] + 1`

若前驅狀態不存在，其長度視為 `0`，目前元素便形成長度 `1` 的新子序列。

## 解法一：字典動態規劃

`LongestSubsequence` 使用 `Dictionary<int, int>` 保存已掃描數值的最佳結尾長度。每讀到一個 `x`，就查詢 `x - difference` 的既有長度、加一後寫回 `x`，並同步更新全域最佳答案。

狀態只來自目前元素之前已掃描的前綴，因此每次延伸都保留原始索引順序；這正是子序列語意，而不是將數值重新排序後拼接。字典不依賴固定值域，也只為實際出現的不同數值建立狀態。

## 解法二：固定值域陣列動態規劃

`LongestSubsequence2` 利用題目保證的 `[-10,000, 10,000]` 值域，以 `10,000` 作為 offset，將值 `x` 對應到索引 `x + 10,000`。因此陣列共有 `20,001` 個槽位，完整涵蓋索引 `0..20,000`。

前驅值 `x - difference` 若仍在題目值域內，就讀取其 offset 索引；若超出範圍，代表不可能有合法的已掃描前驅狀態，因此貢獻長度為 `0`。這個界線判斷也避免存取陣列範圍之外。

## 複雜度與取捨

兩個方法皆回傳單一整數，因此結果空間都是 `O(1)`。

| 方法 | 時間複雜度 | 輔助空間 | 適用特性 |
| --- | --- | --- | --- |
| `LongestSubsequence` | `O(n)` | `O(u)`，`u` 為掃描期間出現的不同數值數量 | 值域不固定時仍可直接使用 |
| `LongestSubsequence2` | `O(n)` | 固定 `O(20,001)` | 利用本題已知值域，避免雜湊查詢 |

## 負公差範例逐步推演

官方輸入 `arr = [1, 5, 7, 8, 5, 3, 4, 2, 1]`、`difference = -2`。此時前驅值為 `x - (-2) = x + 2`：

| 掃描到的 `x` | 尋找的前驅 `x + 2` | 更新後長度 | 說明 |
| ---: | ---: | ---: | --- |
| `7` | `9` | `1` | 尚無前驅，以 `7` 開始 |
| 第二個 `5` | `7` | `2` | 依原順序形成 `[7, 5]` |
| `3` | `5` | `3` | 延伸為 `[7, 5, 3]` |
| 最後一個 `1` | `3` | `4` | 延伸為 `[7, 5, 3, 1]` |

這四個元素在原陣列中的索引依序遞增，因此 `[7, 5, 3, 1]` 是合法子序列，最佳長度為 `4`。

## 驗收案例

專案沒有正式測試專案或獨立測試框架；可執行檔內的確定性 harness 是驗證機制。下列 8 個案例會分別檢查兩個公開 API，共 16 項檢查：

| 案例 | 輸入摘要 | 預期 | 驗證重點 |
| --- | --- | ---: | --- |
| 1 | `[1, 2, 3, 4]`, `difference = 1` | `4` | 官方遞增範例 |
| 2 | `[1, 3, 5, 7]`, `difference = 1` | `1` | 官方無可延伸鏈範例 |
| 3 | `[1, 5, 7, 8, 5, 3, 4, 2, 1]`, `difference = -2` | `4` | 官方負公差範例 |
| 4 | `[5]`, `difference = 7` | `1` | 最小輸入 |
| 5 | `[7, 7, 7, 7]`, `difference = 0` | `4` | 零公差與重複值 |
| 6 | `[4, 1, 2, 3]`, `difference = 1` | `3` | 順序回歸，確認不可重排 |
| 7 | `[-10,000, 0, 10,000]`, `difference = 10,000` | `3` | 同時涵蓋值域上下界 |
| 8 | `100,000` 個 `7`, `difference = 0` | `100,000` | 100,000 元素上限 spot check |

## 建置與執行

從題目根目錄 `leetcode_1218/` 執行：

```bash
dotnet build leetcode_1218/leetcode_1218.csproj --nologo
dotnet run --no-build --project leetcode_1218/leetcode_1218.csproj
```

使用 `--no-build` 前請先完成建置。不要執行裸的 `dotnet build` 或 `dotnet test`，因為題目根目錄沒有專案、solution 或正式測試專案。

## 實際驗證輸出

以下內容直接取自成功執行結果：

```text
LeetCode 1218 acceptance harness

Case 1: Official increasing example
Input: arr = [1, 2, 3, 4], difference = 1
PASS | Dictionary DP | Expected: 4 | Actual: 4
PASS | Value-range array DP | Expected: 4 | Actual: 4

Case 2: Official no-chain example
Input: arr = [1, 3, 5, 7], difference = 1
PASS | Dictionary DP | Expected: 1 | Actual: 1
PASS | Value-range array DP | Expected: 1 | Actual: 1

Case 3: Official negative-difference example
Input: arr = [1, 5, 7, 8, 5, 3, 4, 2, 1], difference = -2
PASS | Dictionary DP | Expected: 4 | Actual: 4
PASS | Value-range array DP | Expected: 4 | Actual: 4

Case 4: Minimum valid input
Input: arr = [5], difference = 7
PASS | Dictionary DP | Expected: 1 | Actual: 1
PASS | Value-range array DP | Expected: 1 | Actual: 1

Case 5: Zero difference with duplicates
Input: arr = [7, 7, 7, 7], difference = 0
PASS | Dictionary DP | Expected: 4 | Actual: 4
PASS | Value-range array DP | Expected: 4 | Actual: 4

Case 6: Subsequence order regression
Input: arr = [4, 1, 2, 3], difference = 1
PASS | Dictionary DP | Expected: 3 | Actual: 3
PASS | Value-range array DP | Expected: 3 | Actual: 3

Case 7: Value-range boundaries
Input: arr = [-10000, 0, 10000], difference = 10000
PASS | Dictionary DP | Expected: 3 | Actual: 3
PASS | Value-range array DP | Expected: 3 | Actual: 3

Case 8: Maximum-length spot check
Input: arr = 100,000 × 7, difference = 0
PASS | Dictionary DP | Expected: 100000 | Actual: 100000
PASS | Value-range array DP | Expected: 100000 | Actual: 100000

Summary: 16/16 checks passed.
```

## 專案結構

```plaintext
leetcode_1218/
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
│       │   └── 2026-07-16-leetcode-1218-net10-migration.md
│       └── specs/
│           └── 2026-07-16-leetcode-1218-net10-migration-design.md
└── leetcode_1218/
    ├── Program.cs
    └── leetcode_1218.csproj
```
