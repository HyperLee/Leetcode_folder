# 645. Set Mismatch｜錯誤的集合

## 題目連結

- English: [LeetCode 645. Set Mismatch](https://leetcode.com/problems/set-mismatch/)
- 中文：[LeetCode 645. 錯誤的集合](https://leetcode.cn/problems/set-mismatch/)

## 題意與限制

給定原本應包含 `1..n` 每個整數一次的陣列 `nums`。實際輸入中恰有一個數字重複出現兩次，並恰有一個數字完全缺失；回傳順序固定為 `[duplicate, missing]`。

- `2 <= nums.Length <= 10000`
- `1 <= nums[i] <= nums.Length`
- 每個輸入都符合 LeetCode 有效契約：一個值重複、另一個值遺失。

本實作依題意處理有效輸入，不額外加入無效輸入的例外或行為。

## 解法：計數陣列

建立長度為 `n + 1` 的 `counts` 陣列，索引 `0` 不使用。走訪 `nums` 時遞增 `counts[num]`；接著掃描所有合法值 `1..n`：計數為 `2` 的值是重複值，計數為 `0` 的值是遺失值。

核心不變量是：完成第一輪走訪後，對每個 `value`（`1..n`），`counts[value]` 精確等於 `value` 在輸入陣列中的出現次數。因此第二輪掃描只要解讀 `2` 與 `0`，就能同時找出答案。方法只讀取輸入，不會修改呼叫端傳入的 `nums`，也不會輸出主控台訊息。

## 複雜度

| 項目 | 複雜度 | 原因 |
| --- | --- | --- |
| 時間 | `O(n)` | 計數與掃描各走訪一次。 |
| 結果／空間 | `O(n)` | 長度為 `n + 1` 的計數陣列主導整體額外空間；實際回傳的兩元素陣列為 `O(1)`。 |
| 輔助空間 | `O(n)` | `counts` 為輸入以外唯一隨 `n` 成長的資料結構。 |

## 逐步範例

以 `nums = [1, 2, 2, 4]` 為例，`n = 4`：

```plaintext
counts[1..4] = [1, 2, 0, 1]
value = 2 的計數為 2 → duplicate = 2
value = 3 的計數為 0 → missing = 3
result = [2, 3]
```

## Acceptance harness

本題沒有正式測試專案；`Program.Main` 是確定性的 acceptance harness。每個案例都會列印 `Case`、`Input`、`Expected`、`Actual` 與 `PASS`／`FAIL`，若任一案例失敗則設定非零結束碼。

| 案例名稱 | 輸入 | 預期結果 |
| --- | --- | --- |
| Official example and input remains unchanged | `[1, 2, 2, 4]` | `[2, 3]`，且輸入保持不變 |
| Minimum valid input | `[1, 1]` | `[1, 2]` |
| Missing first value | `[2, 2, 3, 4]` | `[2, 1]` |
| Missing last value | `[1, 2, 3, 3]` | `[3, 4]` |
| Duplicate first value | `[1, 1, 3, 4]` | `[1, 2]` |
| Duplicate last value | `[1, 2, 4, 4]` | `[4, 3]` |
| Unordered regression | `[4, 3, 2, 2]` | `[2, 1]` |
| Upper-bound spot check | `1..10000`，最後一項改為 `9999` | `[9999, 10000]` |

## 建置與執行

在 `leetcode_645` 題目根目錄執行：

```bash
dotnet build leetcode_645/leetcode_645.csproj --nologo
dotnet run --no-build --project leetcode_645/leetcode_645.csproj
```

## 實際執行輸出

以下為本 README 撰寫前，以目前專案執行所取得的完整輸出：

```text
Case: Official example and input remains unchanged
Input: nums = [1, 2, 2, 4]
Expected: result = [2, 3]; nums remains [1, 2, 2, 4]
Actual: result = [2, 3]; nums is [1, 2, 2, 4]
PASS
Case: Minimum valid input
Input: nums = [1, 1]
Expected: [1, 2]
Actual: [1, 2]
PASS
Case: Missing first value
Input: nums = [2, 2, 3, 4]
Expected: [2, 1]
Actual: [2, 1]
PASS
Case: Missing last value
Input: nums = [1, 2, 3, 3]
Expected: [3, 4]
Actual: [3, 4]
PASS
Case: Duplicate first value
Input: nums = [1, 1, 3, 4]
Expected: [1, 2]
Actual: [1, 2]
PASS
Case: Duplicate last value
Input: nums = [1, 2, 4, 4]
Expected: [4, 3]
Actual: [4, 3]
PASS
Case: Unordered regression
Input: nums = [4, 3, 2, 2]
Expected: [2, 1]
Actual: [2, 1]
PASS
Case: Upper-bound spot check
Input: nums = [1, 2, ..., 9999, 9999] (n = 10000)
Expected: [9999, 10000]
Actual: [9999, 10000]
PASS
Summary: 8/8 checks passed.
```

## 最終專案結構

```plaintext
leetcode_645/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_645/
    ├── Program.cs
    └── leetcode_645.csproj
```
