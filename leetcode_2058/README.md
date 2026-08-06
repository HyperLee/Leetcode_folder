# LeetCode 2058：找出臨界點之間的最小和最大距離

這是一個以 .NET 10 撰寫的主控台教學專案，示範如何在單向鏈結串列中找出所有臨界點，並計算任意兩個臨界點之間的最小與最大距離。專案保留常數額外空間的一次走訪解法，另提供先收集臨界點索引的直觀比較解法。

題目連結：[Find the Minimum and Maximum Number of Nodes Between Critical Points](https://leetcode.com/problems/find-the-minimum-and-maximum-number-of-nodes-between-critical-points/description/)

## 題目說明

給定單向鏈結串列 `head`，若某個節點同時具有前一個與下一個節點，而且符合下列任一條件，該節點就是臨界點：

- **局部極大值**：目前值嚴格大於前一個值與下一個值。
- **局部極小值**：目前值嚴格小於前一個值與下一個值。

因為頭節點沒有前一個節點、尾節點沒有下一個節點，所以兩者都不能成為臨界點。相等值也不符合「嚴格大於」或「嚴格小於」，因此平台區段不會形成臨界點。

回傳長度為 2 的陣列：

```text
[minDistance, maxDistance]
```

- `minDistance`：任意兩個不同臨界點之間的最小索引距離。
- `maxDistance`：任意兩個不同臨界點之間的最大索引距離。
- 若臨界點少於兩個，回傳 `[-1,-1]`。

## 限制條件

- 鏈結串列節點數介於 `2` 到 `100,000`。
- `1 <= Node.val <= 100,000`。
- 測試資料皆符合非空且至少包含兩個節點的題目輸入契約。

節點上限可達十萬，因此解法必須避免列舉所有臨界點配對的 O(k²) 作法，其中 `k` 是臨界點數量。

## 解題概念與出發點

假設依走訪順序取得臨界點索引：

```text
c0 < c1 < c2 < ... < ck-1
```

可以利用兩個重要性質：

1. **最小距離只需比較相鄰臨界點**
   若選擇不相鄰的 `ci` 與 `cj`，兩者之間至少還有一個臨界點；整段距離由一個以上的相鄰間隔組成，不可能小於其中每一段。因此全域最小值必定存在於相鄰索引差。
2. **最大距離必定來自第一個與最後一個臨界點**
   索引已由小到大排列，能取得的最大差就是 `ck-1 - c0`。

兩種解法都只走訪鏈結串列一次，差別在於是否保存全部臨界點索引。

## 解法一：串流更新臨界點狀態

### 設計說明

`NodesBetweenCriticalPoints` 在走訪過程中只保留以下狀態：

- `firstCriticalIndex`：第一個臨界點索引，用來計算最大距離。
- `previousCriticalIndex`：上一個臨界點索引，用來計算相鄰臨界點距離。
- `minDistance`：目前為止最小的相鄰臨界點距離。
- `maxDistance`：第一個臨界點到目前臨界點的距離。

走訪以 `(previous, current, next)` 三節點視窗判斷 `current`：

1. 從索引 `1` 的第二個節點開始，因為頭節點不可能是臨界點。
2. 若 `current` 是第一個臨界點，記錄其索引。
3. 若先前已找到臨界點，以目前索引減去上一個臨界點索引更新最小距離。
4. 以目前索引減去第一個臨界點索引更新最大距離。
5. 視窗向右移動，直到 `current` 沒有下一個節點。
6. 若始終無法形成一對臨界點，回傳 `[-1,-1]`。

### 範例演示

輸入 `[5,3,1,2,5,1,2]`，程式使用從 `0` 開始的索引：

| 索引 | 三節點值 | 判斷 | 第一個臨界點 | 上一個臨界點 | 最小距離 | 最大距離 |
| ---: | --- | --- | ---: | ---: | ---: | ---: |
| 1 | `5,3,1` | 不是嚴格極值 | - | - | - | - |
| 2 | `3,1,2` | 局部極小值 | 2 | 2 | - | - |
| 3 | `1,2,5` | 不是嚴格極值 | 2 | 2 | - | - |
| 4 | `2,5,1` | 局部極大值 | 2 | 4 | `4 - 2 = 2` | `4 - 2 = 2` |
| 5 | `5,1,2` | 局部極小值 | 2 | 5 | `min(2, 5 - 4) = 1` | `5 - 2 = 3` |

最後得到 `[1,3]`。這個方法不需要保存臨界點集合，適合追求最小額外空間的情境。

## 解法二：收集所有臨界點索引

### 設計說明

`NodesBetweenCriticalPoints2` 將「辨識臨界點」與「計算距離」拆成兩個階段：

1. 使用相同的三節點視窗走訪串列。
2. 每次找到臨界點時，將索引依序加入 `criticalIndices`。
3. 若收集到的索引少於兩個，回傳 `[-1,-1]`。
4. 逐一計算相鄰索引差並取最小值。
5. 以最後索引減去第一個索引取得最大距離。

這種寫法直接保留所有臨界點位置，步驟容易觀察與除錯，代價是需要 O(k) 額外空間。

### 範例演示

同樣輸入 `[5,3,1,2,5,1,2]`：

1. 走訪後收集到臨界點索引 `[2,4,5]`。
2. 相鄰距離為 `4 - 2 = 2` 與 `5 - 4 = 1`。
3. 最小距離為 `min(2,1) = 1`。
4. 最大距離為最後與第一個索引差：`5 - 2 = 3`。
5. 回傳 `[1,3]`。

## 複雜度比較

令 `n` 為鏈結串列節點數，`k` 為臨界點數量。

| 解法 | 時間複雜度 | 額外空間複雜度 | 特點 |
| --- | --- | --- | --- |
| 串流更新狀態 | O(n) | O(1) | 不保存全部索引，空間最省 |
| 收集臨界點索引 | O(n) | O(k) | 流程直觀，方便檢視臨界點位置 |

兩種解法都不修改輸入鏈結串列。測試入口仍為兩種解法建立獨立串列，避免未來修改任一實作時互相污染測試資料。

## 測試案例

主程式會對兩種解法各執行五個案例，共十項檢查：

| 案例 | 輸入 | 預期輸出 | 驗證重點 |
| --- | --- | --- | --- |
| 最短合法串列 | `[3,1]` | `[-1,-1]` | 無內部節點，因此無臨界點 |
| 相鄰臨界點 | `[1,3,1,2]` | `[1,1]` | 兩個臨界點距離為 1 |
| 一般多臨界點 | `[5,3,1,2,5,1,2]` | `[1,3]` | 同時驗證最小與最大距離 |
| 間隔臨界點 | `[1,3,2,2,3,2,2,2,7]` | `[3,3]` | 僅兩個臨界點時兩種距離相同 |
| 重複值平台 | `[2,3,3,2]` | `[-1,-1]` | 相等值不符合嚴格極值 |

每個結果都顯示 `Expected`、`Actual` 與 `PASS/FAIL`。只要有任何檢查失敗，程式會設定非零結束碼，方便命令列或 CI 判斷失敗。

## 建置與執行

請從本 README 所在的 `leetcode_2058` 目錄執行：

```bash
dotnet restore leetcode_2058/leetcode_2058.csproj
dotnet build leetcode_2058/leetcode_2058.csproj --nologo
dotnet run --no-build --project leetcode_2058/leetcode_2058.csproj
```

若要驗證格式與 Git 差異空白，可執行：

```bash
dotnet format leetcode_2058/leetcode_2058.csproj --verify-no-changes --no-restore
git diff --check
```

## 實際執行結果

以下內容來自 `dotnet run --no-build --project leetcode_2058/leetcode_2058.csproj`：

```text
Case: 最短合法串列，無臨界點
Input: [3,1]
  Solution 1 - 串流狀態
    Expected: [-1,-1]
    Actual:   [-1,-1]
    Result:   PASS
  Solution 2 - 收集索引
    Expected: [-1,-1]
    Actual:   [-1,-1]
    Result:   PASS

Case: 恰好兩個相鄰臨界點
Input: [1,3,1,2]
  Solution 1 - 串流狀態
    Expected: [1,1]
    Actual:   [1,1]
    Result:   PASS
  Solution 2 - 收集索引
    Expected: [1,1]
    Actual:   [1,1]
    Result:   PASS

Case: 三個臨界點的一般案例
Input: [5,3,1,2,5,1,2]
  Solution 1 - 串流狀態
    Expected: [1,3]
    Actual:   [1,3]
    Result:   PASS
  Solution 2 - 收集索引
    Expected: [1,3]
    Actual:   [1,3]
    Result:   PASS

Case: 兩個間隔臨界點
Input: [1,3,2,2,3,2,2,2,7]
  Solution 1 - 串流狀態
    Expected: [3,3]
    Actual:   [3,3]
    Result:   PASS
  Solution 2 - 收集索引
    Expected: [3,3]
    Actual:   [3,3]
    Result:   PASS

Case: 相等值平台不構成嚴格極值
Input: [2,3,3,2]
  Solution 1 - 串流狀態
    Expected: [-1,-1]
    Actual:   [-1,-1]
    Result:   PASS
  Solution 2 - 收集索引
    Expected: [-1,-1]
    Actual:   [-1,-1]
    Result:   PASS

Summary: 10/10 checks passed.
```

## 專案結構

```text
leetcode_2058/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_2058.sln
└── leetcode_2058/
    ├── leetcode_2058.csproj
    └── Program.cs
```

- `Program.cs`：鏈結串列節點、兩種演算法、自我驗證案例與輸出格式。
- `leetcode_2058.csproj`：以 `net10.0` 為目標的主控台專案設定。
- `docs/readme-template.md`：本專案 README 使用的撰寫指引。