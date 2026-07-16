# 1424. Diagonal Traverse II（對角線遍歷 II）

[LeetCode 題目](https://leetcode.com/problems/diagonal-traverse-ii/) · [力扣中文題目](https://leetcode.cn/problems/diagonal-traverse-ii/)

給定一個每列長度可能不同的二維整數串列，依對角線順序回傳所有元素。同一條對角線
必須由較下方的列走向較上方的列，再接續下一條對角線。本專案使用 .NET 10 主控台
程式與對角線 bucket，並由 `Main` 執行七個確定性 acceptance checks。

## 題目與限制

公開 API 為
`public static int[] FindDiagonalOrder(IList<IList<int>> nums)`。解法不輸出且不修改
輸入，只處理 LeetCode 保證的有效資料：

- `1 <= nums.length <= 100,000`。
- `1 <= nums[i].length <= 100,000`。
- 所有列的元素總數介於 `1` 與 `100,000` 之間。
- `1 <= nums[i][j] <= 1,000,000,000`。

## 解法：依 row + column 分桶

座標 `(row, column)` 所在的對角線編號恆為 `row + column`。演算法先掃描一次輸入，
取得總元素數 `N` 與最大欄數 `C`，再建立 `R + C - 1` 個 bucket。接著從最後一列往
第一列掃描，並依規格讓每列由左向右。由於列的掃描順序是由下往上，加入同一 bucket
的元素自然已符合「由下往上」順序，不必再排序。最後依 bucket 索引遞增攤平到結果陣列。

相較於把每個元素連同座標收集後排序，分桶不需要 `O(N log N)` 排序時間；代價是要
保留對角線 bucket 結構。這也比逐條對角線反覆搜尋列更直接地保證每個元素只走訪
固定次數。

### 核心不變量與容易出錯的地方

- bucket 索引只能是 `row + column`；相同索引的元素才位於同一條對角線。
- 列必須由下往上掃描；若由上往下，bucket 內順序會與題目要求相反。
- 每列由左往右是指定的掃描順序；元素所屬 bucket 只由 `row + column` 決定。
- bucket 數量是 `R + C - 1`，其中 `C` 是最大列長度，不是第一列長度。
- 公開方法只計算並回傳新陣列，不能修改輸入或輸出主控台。

### 逐步走查

以 `[[1,2,3],[4],[5,6,7]]` 為例，由最後一列往上掃描：

```plaintext
row 2：5 -> bucket 2，6 -> bucket 3，7 -> bucket 4
row 1：4 -> bucket 1
row 0：1 -> bucket 0，2 -> bucket 1，3 -> bucket 2
buckets：0=[1]，1=[4,2]，2=[5,3]，3=[6]，4=[7]
攤平結果：[1,4,2,5,3,6,7]
```

## 複雜度

令 `N` 為總元素數、`R` 為列數、`C` 為最大列長度：

- 時間複雜度：`O(N)`；盤點、分桶與攤平皆為線性工作。
- 結果空間複雜度：`O(N)`；回傳含全部元素的新陣列。
- 輔助空間複雜度：`O(N + R + C)`；bucket 保存 `N` 個元素，並配置
  `R + C - 1` 個 bucket。

## 七項 acceptance checks

| 案例 | 預期 | 驗證目的 |
| --- | --- | --- |
| 官方範例一，規則 `3 x 3` | `[1,4,2,7,5,3,8,6,9]` | 一般對角線順序。 |
| 官方範例二，極不規則列 | `[1,6,2,...,15,16]` | 官方鋸齒形輸入。 |
| 交錯長短列 | `[1,4,2,5,3,8,6,9,7,10,11]` | 防止以固定欄數走訪。 |
| 單列六個元素 | `[1,2,3,4,5,6]` | 只有橫向資料。 |
| 單一元素 | `[42]` | 最小有效輸入。 |
| 多種列長且檢查輸入不變 | `[1,2,5,3,6,4,8,7,9,10,11]` | 純函式契約與 bucket 順序。 |
| 100,000 個單元素列 | 長度與首／中／尾為 `100000`、`1/50001/100000` | 總元素上限，不印出巨量資料。 |

每個案例都印出名稱、Input、Expected、Actual 與 PASS/FAIL；任一失敗會設定
`Environment.ExitCode = 1`。

## 在本題目根目錄建置與執行

本資料夾沒有獨立 solution 或正式測試專案。請在外層 `leetcode_1424/` 內使用明確
的巢狀 project 路徑：

```bash
dotnet build leetcode_1424/leetcode_1424.csproj --nologo
dotnet run --no-build --project leetcode_1424/leetcode_1424.csproj
```

VS Code 開啟外層 `leetcode_1424/` 後，選擇 `Debug leetcode_1424`；它會先執行
`build leetcode_1424`，再啟動 .NET 10 DLL。

## 已驗證的執行輸出

下列內容來自新鮮執行 `dotnet run --no-build --project leetcode_1424/leetcode_1424.csproj`：

```text
LeetCode 1424 acceptance harness

Case 1: official example 1
Input: [[1,2,3],[4,5,6],[7,8,9]]
Expected: [1,4,2,7,5,3,8,6,9]
Actual: [1,4,2,7,5,3,8,6,9]
PASS

Case 2: official example 2
Input: [[1,2,3,4,5],[6,7],[8],[9,10,11],[12,13,14,15,16]]
Expected: [1,6,2,8,7,3,9,4,12,10,5,13,11,14,15,16]
Actual: [1,6,2,8,7,3,9,4,12,10,5,13,11,14,15,16]
PASS

Case 3: irregular rows
Input: [[1,2,3],[4],[5,6,7],[8],[9,10,11]]
Expected: [1,4,2,5,3,8,6,9,7,10,11]
Actual: [1,4,2,5,3,8,6,9,7,10,11]
PASS

Case 4: single row
Input: [[1,2,3,4,5,6]]
Expected: [1,2,3,4,5,6]
Actual: [1,2,3,4,5,6]
PASS

Case 5: single value
Input: [[42]]
Expected: [42]
Actual: [42]
PASS

Case 6: input preservation
Input: [[1],[2,3,4],[5],[6,7],[8,9,10,11]]
Expected: [1,2,5,3,6,4,8,7,9,10,11]; input unchanged: True
Actual: [1,2,5,3,6,4,8,7,9,10,11]; input unchanged: True
PASS

Case 7: 100,000 single-element rows
Input: 100,000 rows containing [1] through [100000]
Expected: length = 100000; first/middle/last = 1/50001/100000
Actual: length = 100000; first/middle/last = 1/50001/100000
PASS

Summary: 7/7 checks passed.
```

## 專案結構

```plaintext
leetcode_1424/
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
└── leetcode_1424/
    ├── leetcode_1424.csproj
    └── Program.cs
```

舊式 `leetcode_1424.sln`、`App.config` 與 `Properties/AssemblyInfo.cs` 已移除。
