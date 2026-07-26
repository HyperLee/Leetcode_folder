# LeetCode 2971：找到最大周長的多邊形

這是一個以 C# 撰寫的 .NET 10 主控台專案。`LargestPerimeter` 使用排序、前綴和
與貪婪判斷，找出可由輸入邊長組成的多邊形最大周長；`Main` 提供可重複執行的
acceptance harness，驗證官方範例、嚴格不等式、64-bit 周長與題目上限。

- [英文題目：2971. Find Polygon With the Largest Perimeter](https://leetcode.com/problems/find-polygon-with-the-largest-perimeter/)
- [中文題目：2971. 找到最大周長的多邊形](https://leetcode.cn/problems/find-polygon-with-the-largest-perimeter/)

## 題目說明

給定正整數陣列 `nums`，每個元素代表一條邊。多邊形至少需要三條邊，而且其最長邊
必須嚴格小於其他邊長之和。請從陣列中選取若干邊，回傳可以形成之多邊形的最大
周長；如果沒有任何有效選法，回傳 `-1`。

## 限制條件

- `3 <= nums.Length <= 100000`
- `1 <= nums[i] <= 10^9`
- 實作只處理 LeetCode 定義的有效輸入，不另外定義 null、空陣列或非法邊長行為。

## 核心不變量

將陣列由小到大排序後，走訪到 `edgeLength` 時，它就是目前前綴中的最長邊。
假設前綴總和為 `prefixSum`，其他邊的總和便是
`prefixSum - edgeLength`。形成多邊形的條件為：

```plaintext
prefixSum - edgeLength > edgeLength
prefixSum > 2 * edgeLength
```

若目前前綴符合條件，加入任何不大於最長邊的正數邊都只會增加可用周長，因此應
保留完整前綴。後續遇到不符合條件的更長邊時，也不能清除先前已找到的候選周長。
程式使用 `long` 儲存前綴和及 `2L * edgeLength` 的比較結果，避免最大周長超出
`Int32`。

## 解法設計

公開介面如下：

```csharp
public static long LargestPerimeter(int[] nums)
```

1. 使用 `Array.Sort(nums)` 將輸入陣列就地排序；呼叫完成後，輸入順序會被改變。
2. 由小到大累加 `long prefixSum`。
3. 每當 `prefixSum > 2L * edgeLength`，將目前前綴和記為新的最大周長。
4. 完成走訪後回傳最後一個有效周長；從未形成多邊形時回傳 `-1`。

只保留這個具有教學價值的單一解法。`LargestPerimeter` 不寫入主控台，所有
Expected、Actual 與 PASS/FAIL 輸出均由 `Main` 負責。

## 複雜度

- 時間複雜度：`O(n log n)`，排序主導整體成本，走訪為 `O(n)`。
- 結果空間：`O(1)`，回傳值只有一個 `long`。
- 額外輔助空間：`O(log n)`，來自 .NET `Array.Sort` 的排序堆疊；前綴走訪本身
  只使用固定數量變數。

## 官方範例 2 逐步走查

輸入 `[1, 12, 1, 2, 5, 50, 3]` 排序後為 `[1, 1, 2, 3, 5, 12, 50]`：

| 目前最長邊 | 前綴和 | `prefixSum > 2 * edgeLength` | 最大有效周長 |
| ---: | ---: | :---: | ---: |
| 1 | 1 | 否 | -1 |
| 1 | 2 | 否 | -1 |
| 2 | 4 | 否（必須嚴格大於） | -1 |
| 3 | 7 | 是 | 7 |
| 5 | 12 | 是 | 12 |
| 12 | 24 | 否 | 12 |
| 50 | 74 | 否 | 12 |

最後回傳 `12`，對應邊長 `[1, 1, 2, 3, 5]`。

## 可執行驗證案例

`Main` 共執行九組案例與九項檢查：

| 案例 | 輸入摘要 | Expected | 驗證重點 |
| ---: | --- | ---: | --- |
| 1 | `[5,5,5]` | 15 | 官方範例：有效三邊形 |
| 2 | `[1,12,1,2,5,50,3]` | 12 | 官方範例：保留較早有效前綴 |
| 3 | `[5,5,50]` | -1 | 官方範例：無法形成多邊形 |
| 4 | `[1,1,1]` | 3 | 最小有效輸入 |
| 5 | `[1,1,2]` | -1 | 最長邊條件必須是嚴格不等式 |
| 6 | `[1,2,3,4,5]` | 15 | 完整前綴皆可使用 |
| 7 | `[2,3,3]` | 8 | 三邊回歸案例 |
| 8 | 三個 `10^9` | 3,000,000,000 | 回傳值必須使用 `long` |
| 9 | 100,000 個 `10^9` | 100,000,000,000,000 | 最大輸入長度與最大周長 |

每項檢查都輸出案例名稱、輸入、Expected、Actual 與 PASS/FAIL。若任何檢查失敗，
程式會將 `Environment.ExitCode` 設為 1。此專案沒有獨立測試專案或測試框架；
可執行驗證器是目前的驗證機制。

## 建置與執行

請從此 README 所在的外層 `leetcode_2971` 目錄執行：

```bash
dotnet build leetcode_2971/leetcode_2971.csproj --nologo
dotnet run --no-build --project leetcode_2971/leetcode_2971.csproj
```

以下是重新建置後執行第二個命令的完整輸出：

```text
LeetCode 2971 acceptance harness

PASS | Official example 1 | Input: [5, 5, 5] | Expected: 15 | Actual: 15
PASS | Official example 2 | Input: [1, 12, 1, 2, 5, 50, 3] | Expected: 12 | Actual: 12
PASS | Official example 3 | Input: [5, 5, 50] | Expected: -1 | Actual: -1
PASS | Minimum valid input | Input: [1, 1, 1] | Expected: 3 | Actual: 3
PASS | Strict inequality | Input: [1, 1, 2] | Expected: -1 | Actual: -1
PASS | Complete valid prefix | Input: [1, 2, 3, 4, 5] | Expected: 15 | Actual: 15
PASS | Three-side regression | Input: [2, 3, 3] | Expected: 8 | Actual: 8
PASS | 64-bit perimeter | Input: [1000000000, 1000000000, 1000000000] | Expected: 3000000000 | Actual: 3000000000
PASS | Upper-bound spot check | Input: [1_000_000_000 repeated 100000 times] | Expected: 100000000000000 | Actual: 100000000000000

Summary: 9/9 checks passed.
```

## 專案結構

```plaintext
.
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── docs/
│   └── readme-template.md
├── leetcode_2971/
│   ├── Program.cs
│   └── leetcode_2971.csproj
├── AGENTS.md
└── README.md
```
