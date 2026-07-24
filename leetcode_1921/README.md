# LeetCode 1921 — Eliminate Maximum Number of Monsters

> 消滅怪物的最大數量｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/eliminate-maximum-number-of-monsters/)
- [中文題目](https://leetcode.cn/problems/eliminate-maximum-number-of-monsters/)

## 題目說明

給定兩個等長整數陣列 `dist` 與 `speed`。第 `i` 隻怪物距離城市 `dist[i]`，且每分鐘向
城市移動 `speed[i]` 的距離。從第 `0` 分鐘起，每分鐘可消滅一隻怪物；只要任一怪物在攻擊前
或當下抵達城市便失敗。回傳城市失守前最多可消滅的怪物數量。

題目限制：

- `1 <= dist.length == speed.length <= 100000`
- `1 <= dist[i], speed[i] <= 100000`

## 解法：整數抵達時間排序

公開 API：

```csharp
public static int EliminateMaximum(int[] dist, int[] speed)
```

第 `i` 隻怪物的抵達時間必須以整數向上取整：

```csharp
(dist[i] - 1) / speed[i] + 1
```

此式避免浮點數，並精確表示怪物第一次在整數分鐘邊界抵達城市的時刻。將所有抵達時間放入新的
陣列並排序後，第 `i` 個最早抵達的怪物必須能在第 `i` 分鐘前處理；若
`arrivalTimes[i] <= i`，它已在該次攻擊前抵達，答案就是 `i`。若全部都通過，則可消滅全部
怪物。

核心不變量是：排序後，前 `i` 個最早抵達的怪物必須各自保留一個不同的攻擊分鐘 `0` 到
`i - 1`；因此每個 `arrivalTimes[i]` 都必須嚴格大於 `i`。容易出錯之處是不可使用整數截斷
`dist[i] / speed[i]`，也不可把相同分鐘抵達的怪物排到稍後才檢查。

此實作只讀取 `dist` 與 `speed`，將抵達時間寫入新的陣列後排序，因此公開方法不修改輸入、
不輸出主控台，也不加入題目契約外的無效輸入行為。

### 逐步範例

以 `dist=[1, 3, 4]`、`speed=[1, 1, 1]` 為例：抵達時間為 `[1, 3, 4]`，排序後不變。
第 `0`、`1`、`2` 分鐘分別處理抵達時間 `1`、`3`、`4` 的怪物；每個抵達時間都大於對應攻擊
分鐘，因此可消滅 `3` 隻。

相對地，`dist=[1, 1, 2, 3]`、`speed=[1, 1, 1, 1]` 的排序抵達時間是 `[1, 1, 2, 3]`。
第 `0` 分鐘能消滅一隻，但第 `1` 個抵達時間也是 `1`，滿足 `arrivalTimes[1] <= 1`，故只能
消滅 `1` 隻。

`dist=[1, 2, 2, 10]`、`speed=[1, 1, 1, 1]` 則會得到 `[1, 2, 2, 10]`。前兩隻可於第
`0` 與 `1` 分鐘消滅，但第 `2` 個抵達時間為 `2`，恰好在第 `2` 分鐘攻擊前抵達，因此答案是
`2`；這個一般性的部分失敗案例可避免把所有非零失敗錯誤地回傳為 `1`。

### 複雜度與取捨

| 項目 | 複雜度 | 說明 |
| --- | --- | --- |
| 時間 | `O(n log n)` | 建立抵達時間為 `O(n)`，排序主導成本。 |
| 輔助空間 | `O(n)` | 新建 `arrivalTimes`，以保留兩個輸入陣列。 |
| 結果空間 | `O(1)` | 僅回傳一個整數。 |

排序是為了把每分鐘的一次攻擊優先分配給最早抵達的怪物；不排序便無法由單一掃描維持此貪心
不變量。

## Acceptance Harness

`Main` 執行九個確定性案例。每案驗證答案與 `dist`、`speed` 的合併輸入保存，因此共有
18 個檢查；任何失敗都會把 process exit code 設為 `1`。大型案例以精簡標籤呈現，避免輸出
十萬個元素。

| # | 輸入 | 預期 | 驗證目的 |
| ---: | --- | ---: | --- |
| 1 | `dist=[1,3,4]`, `speed=[1,1,1]` | 3 | 官方範例 |
| 2 | `dist=[1,1,2,3]`, `speed=[1,1,1,1]` | 1 | 官方第二範例與同時抵達 |
| 3 | `dist=[3,2,4]`, `speed=[5,3,2]` | 1 | 官方第三範例 |
| 4 | `dist=[1]`, `speed=[100000]` | 1 | 最小有效輸入 |
| 5 | `dist=[1,3]`, `speed=[1,2]` | 2 | 向上取整回歸 |
| 6 | `dist=[3,4]`, `speed=[1,4]` | 2 | 抵達順序重排 |
| 7 | `dist=[1,2,2,10]`, `speed=[1,1,1,1]` | 2 | 一般部分失敗，避免固定回傳 1 |
| 8 | 100000 個 `dist=100000`、`speed=1` | 100000 | 題目上限與完整可消滅 |
| 9 | 100000 個 `dist=100000`、`speed=100000` | 1 | 題目上限與同分鐘抵達 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_1921/leetcode_1921/leetcode_1921.csproj --nologo
dotnet run --no-build --project leetcode_1921/leetcode_1921/leetcode_1921.csproj
```

若直接開啟題目根目錄 `leetcode_1921/`，使用：

```bash
dotnet build leetcode_1921/leetcode_1921.csproj --nologo
dotnet run --no-build --project leetcode_1921/leetcode_1921.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1 - Official example
Input: dist=[1, 3, 4], speed=[1, 1, 1]
PASS EliminateMaximum result | Expected: 3 | Actual: 3
PASS Input preserved | Expected: True | Actual: True

Case: 2 - Official second example
Input: dist=[1, 1, 2, 3], speed=[1, 1, 1, 1]
PASS EliminateMaximum result | Expected: 1 | Actual: 1
PASS Input preserved | Expected: True | Actual: True

Case: 3 - Official third example
Input: dist=[3, 2, 4], speed=[5, 3, 2]
PASS EliminateMaximum result | Expected: 1 | Actual: 1
PASS Input preserved | Expected: True | Actual: True

Case: 4 - Minimum input
Input: dist=[1], speed=[100000]
PASS EliminateMaximum result | Expected: 1 | Actual: 1
PASS Input preserved | Expected: True | Actual: True

Case: 5 - Ceiling regression
Input: dist=[1, 3], speed=[1, 2]
PASS EliminateMaximum result | Expected: 2 | Actual: 2
PASS Input preserved | Expected: True | Actual: True

Case: 6 - Arrival order
Input: dist=[3, 4], speed=[1, 4]
PASS EliminateMaximum result | Expected: 2 | Actual: 2
PASS Input preserved | Expected: True | Actual: True

Case: 7 - General partial loss
Input: dist=[1, 2, 2, 10], speed=[1, 1, 1, 1]
PASS EliminateMaximum result | Expected: 2 | Actual: 2
PASS Input preserved | Expected: True | Actual: True

Case: 8 - Maximum count, unit speed
Input: dist=[100000 x 100000], speed=[1 x 100000]
PASS EliminateMaximum result | Expected: 100000 | Actual: 100000
PASS Input preserved | Expected: True | Actual: True

Case: 9 - Maximum count, maximum speed
Input: dist=[100000 x 100000], speed=[100000 x 100000]
PASS EliminateMaximum result | Expected: 1 | Actual: 1
PASS Input preserved | Expected: True | Actual: True

Summary: 18/18 checks passed.
```

## 專案結構

```plaintext
leetcode_1921/
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
└── leetcode_1921/
    ├── Program.cs
    └── leetcode_1921.csproj
```
