# LeetCode 448：Find All Numbers Disappeared in an Array／找到所有陣列中消失的數字

此 .NET 10 主控台專案以原地負號標記法，找出 `1` 到 `n` 範圍中未出現的
數字。公開解法不輸出主控台；`Main` 專責執行可重複的十項 acceptance checks。

## 題目連結

- [English: 448. Find All Numbers Disappeared in an Array](https://leetcode.com/problems/find-all-numbers-disappeared-in-an-array/)
- [中文：448. 找到所有陣列中消失的數字](https://leetcode.cn/problems/find-all-numbers-disappeared-in-an-array/)

## 題意與限制

**English.** Given an array `nums` of `n` integers, where every value is in
`[1, n]`, return every value in that range that does not appear in `nums`.

**中文。** 給定長度為 `n` 的整數陣列 `nums`，每個元素都介於 `[1, n]`；請回傳
此範圍內沒有出現在陣列中的所有數字。回傳結果依數字遞增順序產生。

官方輸入限制如下：

- `n == nums.Length`
- `1 <= n <= 100000`
- `1 <= nums[i] <= n`
- 每個 `nums[i]` 在陣列中出現一次或兩次。

## 核心不變量

對每個讀到的值 `v`，它對應的零起始索引固定是 `v - 1`。將 `nums[v - 1]`
標成負數，代表值 `v` 曾經出現；全部走訪後，仍保持正數的索引 `i` 就代表
`i + 1` 從未出現，應被回傳。

容易出錯的地方：

- 前一輪可能已將目前位置變負，因此取值時必須使用 `Math.Abs(nums[index])`。
- 標記前只在目標位置為正數時才取負，避免重複值把已標記的位置翻回正數。
- 演算法會修改輸入陣列；acceptance harness 必須先複製每個來源陣列，不能直接
  傳入來源資料。
- 索引是零起始，值 `v` 對應 `v - 1`；掃描回傳時才轉回 `index + 1`。

## 解法：原地負號標記

第一趟走訪把每個值所對應的位置標成負數，第二趟掃描所有仍為正數的位置並加入
結果。這避免使用額外的 `HashSet` 或布林陣列，因此輔助空間保持常數；代價是
`FindDisappearedNumbers` 會原地改寫 `nums`。公開 API
`public static IList<int> FindDisappearedNumbers(int[] nums)` 沒有 console I/O，
方便由不同呼叫端使用，而顯示與驗證保留在 `Main`。

## 複雜度

- 時間複雜度：`O(n)`，標記與掃描各走訪陣列一次。
- 額外輔助空間：`O(1)`，不計回傳結果，只使用固定數量的區域變數。
- 結果空間：`O(k)`，其中 `k` 是缺失數字的數量。

## 逐步範例

官方範例 `nums = [4, 3, 2, 7, 8, 2, 3, 1]` 的每一次標記如下。讀取時一律取
絕對值，故即使前面的位置已被標記成負數，仍會映射到正確的 `v - 1` 索引。

```plaintext
起始： [4, 3, 2, 7, 8, 2, 3, 1]
讀到 4：標記索引 3，得到 [4, 3, 2, -7, 8, 2, 3, 1]
讀到 3：標記索引 2，得到 [4, 3, -2, -7, 8, 2, 3, 1]
讀到 2：標記索引 1，得到 [4, -3, -2, -7, 8, 2, 3, 1]
讀到 7：標記索引 6，得到 [4, -3, -2, -7, 8, 2, -3, 1]
讀到 8：標記索引 7，得到 [4, -3, -2, -7, 8, 2, -3, -1]
讀到 2、3：目標索引已為負數，維持不變
讀到 1：標記索引 0，得到 [-4, -3, -2, -7, 8, 2, -3, -1]
掃描正數位置：索引 4 與 5，所以回傳 [5, 6]
```

## Acceptance Harness

專案沒有正式測試專案。`Main` 是目前的 acceptance harness；每個案例都顯示輸入、
預期值、實際值與 `PASS`／`FAIL`，並在失敗時設定非零結束碼。

| 案例名稱 | 輸入／預期結果 | 驗證重點 |
| --- | --- | --- |
| `Official example` | `[4, 3, 2, 7, 8, 2, 3, 1]` → `[5, 6]` | 題目官方範例 |
| `Minimum` | `[1]` → `[]` | 最小有效輸入 |
| `Duplicate one` | `[1, 1]` → `[2]` | 單一重複值 |
| `Full coverage` | `[1, 2, 3, 4, 5]` → `[]` | 沒有缺失數字 |
| `Repeated values` | `[2, 2, 3, 3, 4, 4]` → `[1, 5, 6]` | 缺少首項與尾端值 |
| `Missing tail` | `[1, 1, 2, 3]` → `[4]` | 缺少最後一個值 |
| `Two missing values` | `[2, 1, 2, 1]` → `[3, 4]` | 多個缺口 |
| `Reverse duplicates` | `[4, 4, 3, 3, 2, 2, 1, 1]` → `[5, 6, 7, 8]` | 反向的重複配對 |
| `Upper bound n=100000` | 產生的長度 100000 資料 → `[100000]` | 上限與輸出精簡表示 |
| `Sign-marking invariant` | 官方範例 | 正數位置當且僅當其一位基數值被回傳 |

成功條件為十個案例都通過，並輸出精確的
`Summary: 10/10 checks passed.`。

## 建置與執行

請從此 README 所在的外層 `leetcode_448` 目錄執行：

```bash
dotnet build leetcode_448/leetcode_448.csproj --nologo
dotnet run --no-build --project leetcode_448/leetcode_448.csproj
```

第二個命令使用 `--no-build`，因此必須先成功執行建置命令。

## 實際執行輸出

以下是完成建置後，以 `dotnet run --no-build` 取得的完整 fresh-run 輸出：

```text
Case: Official example
Input: [4, 3, 2, 7, 8, 2, 3, 1]
Expected: [5, 6]
Actual: [5, 6]
Result: PASS
Case: Minimum
Input: [1]
Expected: []
Actual: []
Result: PASS
Case: Duplicate one
Input: [1, 1]
Expected: [2]
Actual: [2]
Result: PASS
Case: Full coverage
Input: [1, 2, 3, 4, 5]
Expected: []
Actual: []
Result: PASS
Case: Repeated values
Input: [2, 2, 3, 3, 4, 4]
Expected: [1, 5, 6]
Actual: [1, 5, 6]
Result: PASS
Case: Missing tail
Input: [1, 1, 2, 3]
Expected: [4]
Actual: [4]
Result: PASS
Case: Two missing values
Input: [2, 1, 2, 1]
Expected: [3, 4]
Actual: [3, 4]
Result: PASS
Case: Reverse duplicates
Input: [4, 4, 3, 3, 2, 2, 1, 1]
Expected: [5, 6, 7, 8]
Actual: [5, 6, 7, 8]
Result: PASS
Case: Upper bound n=100000
Input: generated n=100000: [1, 2, ..., 99999, 99999]
Expected: [100000]
Actual: [100000]
Result: PASS
Case: Sign-marking invariant
Input: [4, 3, 2, 7, 8, 2, 3, 1]
Expected: a position is positive iff its one-based value is returned
Actual: a position is positive iff its one-based value is returned
Result: PASS
Summary: 10/10 checks passed.
```

## 專案結構

```plaintext
.
├── .editorconfig                         # C# 與結構化檔案格式
├── .gitattributes                        # 文字與二進位檔案屬性
├── .gitignore                            # .NET／IDE 產生檔案排除規則
├── .vscode/
│   ├── launch.json                       # 直接偵錯 net10.0 輸出
│   └── tasks.json                        # 預設建置工作
├── AGENTS.md                             # 本題協作指南
├── README.md                             # 題目、解法與驗證紀錄
├── docs/
│   ├── readme-template.md                # 初次建立 README 的範本
│   └── superpowers/
│       ├── plans/                        # 遷移實作計畫
│       └── specs/                        # 遷移設計紀錄
└── leetcode_448/
    ├── Program.cs                        # 純解法與 acceptance harness
    └── leetcode_448.csproj               # .NET 10 SDK 專案設定
```
