# 1822. Sign of the Product of an Array／陣列元素乘積的符號

給定一個整數陣列，判斷所有元素乘積的符號：正數回傳 `1`、負數回傳 `-1`、零回傳
`0`。本專案保留兩個純函式解法；兩者都只判斷零與負數，不會真的相乘，因此不會遭遇
乘積溢位，也不會改動輸入陣列。

- [LeetCode English](https://leetcode.com/problems/sign-of-the-product-of-an-array/)
- [LeetCode 中文](https://leetcode.cn/problems/sign-of-the-product-of-an-array/)

## 題目說明與限制條件

- `1 <= nums.length <= 1000`
- `-100 <= nums[i] <= 100`
- `nums` 是整數陣列；乘積為正、負或零時，分別回傳 `1`、`-1` 或 `0`。
- 兩個 API 只處理官方保證的有效輸入，不額外定義 `null` 或空陣列行為。

## 兩個保留解法與取捨

### `ArraySign`：統計負數數量

單次掃描中，若出現零便立即回傳 `0`；否則只計數負數。非零乘積的符號完全由負數個數
奇偶決定：偶數個負數為正，奇數個負數為負。這個版本直接呈現「奇偶性」不變量。

### `ArraySign2`：逐一翻轉符號

從 `sign = 1` 開始，同樣在遇到零時立即回傳 `0`；每遇到一個負數就執行
`sign = -sign`。它不需儲存負數總數，狀態就是目前已掃描非零前綴的乘積符號；代價是讀者
必須理解每個負因子恰好翻轉一次符號。

| 方法 | 時間 | 結果空間 | 輔助空間 |
| --- | --- | --- | --- |
| `ArraySign`（負數計數） | `O(n)` | `O(1)` | `O(1)` |
| `ArraySign2`（符號翻轉） | `O(n)` | `O(1)` | `O(1)` |

兩者都是純函式：不修改輸入、不輸出主控台、不建立與輸入長度成比例的資料結構，也不計算
完整乘積。

## 核心不變量與易錯點

- 只要任一元素為零，完整乘積必為零；必須優先立即回傳，不能把零當成一般正負值。
- 當所有元素都非零時，負數個數的奇偶性就是乘積符號；不必也不應進行完整乘法。
- 每個 API 都應只讀取 `nums`。harness 為每個 API 建立獨立副本，確認結果正確且輸入保存。
- `[-1, 1, -1, 1, -1]` 可抓出把奇數個負數誤判為正數的錯誤；1,000 個元素案例則覆蓋題目
  上限與 999 個負數的奇數性。

## 逐步範例

以 `[-1, -2, 3]` 為例：第一個負數使負數數量為 1（或把 `sign` 從 `1` 翻成 `-1`）；
第二個負數使數量成為 2（或再翻回 `1`）；`3` 不改變符號。因此乘積 `6` 為正，兩個方法
都回傳 `1`。如果陣列改為 `[-1, 0, -2]`，掃描到零時立即回傳 `0`。

## Acceptance Harness

`Main` 會執行八個確定性案例。每案分別為 `ArraySign` 與 `ArraySign2` 建立獨立輸入副本，
並檢查「結果等於預期」和「輸入未被修改」；因此共有 32 項檢查。任一檢查失敗時 process
exit code 為 1。

| # | 輸入 | 預期 | 驗證目的 |
| ---: | --- | ---: | --- |
| 1 | `[-1,-2,-3,-4,3,2,1]` | 1 | 官方正數範例，偶數個負數 |
| 2 | `[1,5,0,2,-3]` | 0 | 官方零範例，零立即回傳 |
| 3 | `[-1,1,-1,1,-1]` | -1 | 官方負數範例，奇數個負數 |
| 4 | `[100]` | 1 | 最小長度正數 |
| 5 | `[-100]` | -1 | 最小長度負數 |
| 6 | legacy sample | -1 | 保留舊專案案例 |
| 7 | `[0,-1,-1]` | 0 | 零位於開頭 |
| 8 | 999 個 `-100` 後接一個 `100` | -1 | 長度 1,000 與奇數負數上限 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_1822/leetcode_1822/leetcode_1822.csproj --nologo
dotnet run --no-build --project leetcode_1822/leetcode_1822/leetcode_1822.csproj
```

若直接開啟題目根目錄 `leetcode_1822/`，使用：

```bash
dotnet build leetcode_1822/leetcode_1822.csproj --nologo
dotnet run --no-build --project leetcode_1822/leetcode_1822.csproj
```

以下是 fresh run 的完整輸出：

```text
LeetCode 1822 Acceptance Harness
Case: Official positive example
Input: [-1, -2, -3, -4, 3, 2, 1]
PASS ArraySign result | Expected: 1 | Actual: 1
PASS ArraySign input preserved | Expected: True | Actual: True
PASS ArraySign2 result | Expected: 1 | Actual: 1
PASS ArraySign2 input preserved | Expected: True | Actual: True

Case: Official zero example
Input: [1, 5, 0, 2, -3]
PASS ArraySign result | Expected: 0 | Actual: 0
PASS ArraySign input preserved | Expected: True | Actual: True
PASS ArraySign2 result | Expected: 0 | Actual: 0
PASS ArraySign2 input preserved | Expected: True | Actual: True

Case: Official negative example
Input: [-1, 1, -1, 1, -1]
PASS ArraySign result | Expected: -1 | Actual: -1
PASS ArraySign input preserved | Expected: True | Actual: True
PASS ArraySign2 result | Expected: -1 | Actual: -1
PASS ArraySign2 input preserved | Expected: True | Actual: True

Case: Minimum positive
Input: [100]
PASS ArraySign result | Expected: 1 | Actual: 1
PASS ArraySign input preserved | Expected: True | Actual: True
PASS ArraySign2 result | Expected: 1 | Actual: 1
PASS ArraySign2 input preserved | Expected: True | Actual: True

Case: Minimum negative
Input: [-100]
PASS ArraySign result | Expected: -1 | Actual: -1
PASS ArraySign input preserved | Expected: True | Actual: True
PASS ArraySign2 result | Expected: -1 | Actual: -1
PASS ArraySign2 input preserved | Expected: True | Actual: True

Case: Legacy sample
Input: [9, 72, 34, 29, -49, -22, -77, -17, -66, -75, -44, -30, -24]
PASS ArraySign result | Expected: -1 | Actual: -1
PASS ArraySign input preserved | Expected: True | Actual: True
PASS ArraySign2 result | Expected: -1 | Actual: -1
PASS ArraySign2 input preserved | Expected: True | Actual: True

Case: Zero at beginning
Input: [0, -1, -1]
PASS ArraySign result | Expected: 0 | Actual: 0
PASS ArraySign input preserved | Expected: True | Actual: True
PASS ArraySign2 result | Expected: 0 | Actual: 0
PASS ArraySign2 input preserved | Expected: True | Actual: True

Case: Upper-bound odd negatives
Input: [-100 x 999, 100]
PASS ArraySign result | Expected: -1 | Actual: -1
PASS ArraySign input preserved | Expected: True | Actual: True
PASS ArraySign2 result | Expected: -1 | Actual: -1
PASS ArraySign2 input preserved | Expected: True | Actual: True

Summary: 32/32 checks passed.
```

## 專案結構

```plaintext
leetcode_1822/
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
└── leetcode_1822/
    ├── Program.cs
    └── leetcode_1822.csproj
```
