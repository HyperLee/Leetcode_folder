# LeetCode 2418 — Sort the People

> 按身高排序｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/sort-the-people/)
- [中文題目](https://leetcode.cn/problems/sort-the-people/)

## 題目說明

給定長度皆為 `n` 的姓名陣列 `names` 與身高陣列 `heights`。
`names[i]` 與 `heights[i]` 代表同一個人；回傳依身高由高至低排列的新姓名陣列。

題目限制：

- `n == names.length == heights.length`
- `1 <= n <= 1000`
- `1 <= names[i].length <= 20`
- `1 <= heights[i] <= 100000`
- `names[i]` 只包含大小寫英文字母
- 所有 `heights[i]` 互不相同

## 解法

公開 API：

```csharp
public static string[] SortPeople(string[] names, int[] heights)
public static string[] SortPeople2(string[] names, int[] heights)
```

兩個方法都回傳新陣列，不修改 `names` 或 `heights`，不輸出主控台，也不加入題目
契約外的 invalid-input 行為。

### 解法一：身高字典

`SortPeople` 將每個身高映射至姓名。由於題目保證身高互不相同，身高可安全作為
`Dictionary<int,string>` 的 key；最後依 key 降冪列舉並取得姓名。

這個版本直接表達「由身高找姓名」的關係，適合說明雜湊映射與排序後投影。

### 解法二：索引排序

`SortPeople2` 建立 `0` 至 `n - 1` 的索引陣列，依各索引對應的身高降冪排序，再以排序
後的索引讀取姓名。比較器使用 `CompareTo`，避免以減法撰寫比較器的脆弱模式。

這個版本不建立身高到姓名的字典；索引本身保留兩個輸入陣列間的配對關係。

### 核心不變量與易錯處

- 同一索引的姓名與身高必須始終視為一組，不能分別排序兩個輸入陣列。
- 排序方向必須是身高降冪，而不是姓名字典序或身高升冪。
- 姓名可以重複；不能使用姓名作為唯一 key。
- 身高互不相同，因此不需要額外定義同身高時的排序規則。
- 解法只排序字典項目或新建的索引陣列，兩個輸入陣列均保持不變。

### 逐步範例

以 `names = [Mary,John,Emma]`、`heights = [180,165,170]` 為例：

```plaintext
索引 0：Mary  -> 180
索引 1：John  -> 165
索引 2：Emma  -> 170

身高降冪索引：0, 2, 1
結果：Mary, Emma, John
```

### 複雜度

令 `n` 為人數；兩個方法的結果空間都是新姓名陣列的 `O(n)`。

| 方法 | 時間 | 輔助空間 | 結果空間 |
| --- | --- | --- | --- |
| `SortPeople` | `O(n log n)` | `O(n)` | `O(n)` |
| `SortPeople2` | `O(n log n)` | `O(n)` | `O(n)` |

## Acceptance Harness

`Main` 是唯一的 console I/O 邊界。七個確定性案例各以獨立副本呼叫兩個公開 API；
每個方法分別檢查精確結果、姓名陣列未修改與身高陣列未修改，因此共有 42 個檢查。
任何失敗都會將 process exit code 設為 `1`。長度上限案例使用 1000 個純英文字母姓名
與嚴格遞增身高，能驗證完整反向結果而不輸出整份大型陣列。

| # | 案例 | 預期重點 | 驗證目的 |
| ---: | --- | --- | --- |
| 1 | `Mary/John/Emma` | `Mary,Emma,John` | 官方範例一 |
| 2 | `Alice/Bob/Bob` | `Bob,Alice,Bob` | 官方範例二與重複姓名 |
| 3 | 單一人物 | 原姓名 | 最小 `n`、最短姓名與最低身高 |
| 4 | 身高 `1,2,3,4` | 完整反向 | 防止升冪排序 |
| 5 | 姓名字典序與身高順序不同 | 依身高排序 | 防止誤按姓名排序 |
| 6 | 身高 `1`、`100000` 與 20 字元姓名 | 高至低 | 數值與字串邊界 |
| 7 | 1000 人、身高 `1..1000` | 完整反向 | 長度上限與大型結果摘要 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_2418/leetcode_2418/leetcode_2418.csproj --nologo
dotnet run --no-build --project leetcode_2418/leetcode_2418/leetcode_2418.csproj
```

若直接開啟題目根目錄 `leetcode_2418/`，使用：

```bash
dotnet build leetcode_2418/leetcode_2418.csproj --nologo
dotnet run --no-build --project leetcode_2418/leetcode_2418.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: Official example 1 [SortPeople]
Input: names=[Mary,John,Emma], heights=[180,165,170]
PASS result | Expected: [Mary,Emma,John] | Actual: [Mary,Emma,John]
PASS names preserved | Expected: True | Actual: True
PASS heights preserved | Expected: True | Actual: True

Case: Official example 1 [SortPeople2]
Input: names=[Mary,John,Emma], heights=[180,165,170]
PASS result | Expected: [Mary,Emma,John] | Actual: [Mary,Emma,John]
PASS names preserved | Expected: True | Actual: True
PASS heights preserved | Expected: True | Actual: True

Case: Official example 2 with duplicate names [SortPeople]
Input: names=[Alice,Bob,Bob], heights=[155,185,150]
PASS result | Expected: [Bob,Alice,Bob] | Actual: [Bob,Alice,Bob]
PASS names preserved | Expected: True | Actual: True
PASS heights preserved | Expected: True | Actual: True

Case: Official example 2 with duplicate names [SortPeople2]
Input: names=[Alice,Bob,Bob], heights=[155,185,150]
PASS result | Expected: [Bob,Alice,Bob] | Actual: [Bob,Alice,Bob]
PASS names preserved | Expected: True | Actual: True
PASS heights preserved | Expected: True | Actual: True

Case: Minimum input [SortPeople]
Input: names=[A], heights=[1]
PASS result | Expected: [A] | Actual: [A]
PASS names preserved | Expected: True | Actual: True
PASS heights preserved | Expected: True | Actual: True

Case: Minimum input [SortPeople2]
Input: names=[A], heights=[1]
PASS result | Expected: [A] | Actual: [A]
PASS names preserved | Expected: True | Actual: True
PASS heights preserved | Expected: True | Actual: True

Case: Strictly increasing heights [SortPeople]
Input: names=[A,B,C,D], heights=[1,2,3,4]
PASS result | Expected: [D,C,B,A] | Actual: [D,C,B,A]
PASS names preserved | Expected: True | Actual: True
PASS heights preserved | Expected: True | Actual: True

Case: Strictly increasing heights [SortPeople2]
Input: names=[A,B,C,D], heights=[1,2,3,4]
PASS result | Expected: [D,C,B,A] | Actual: [D,C,B,A]
PASS names preserved | Expected: True | Actual: True
PASS heights preserved | Expected: True | Actual: True

Case: Name order differs from height order [SortPeople]
Input: names=[Zoe,Amy,Mia,Leo], heights=[40,10,30,20]
PASS result | Expected: [Zoe,Mia,Leo,Amy] | Actual: [Zoe,Mia,Leo,Amy]
PASS names preserved | Expected: True | Actual: True
PASS heights preserved | Expected: True | Actual: True

Case: Name order differs from height order [SortPeople2]
Input: names=[Zoe,Amy,Mia,Leo], heights=[40,10,30,20]
PASS result | Expected: [Zoe,Mia,Leo,Amy] | Actual: [Zoe,Mia,Leo,Amy]
PASS names preserved | Expected: True | Actual: True
PASS heights preserved | Expected: True | Actual: True

Case: Height and name length boundaries [SortPeople]
Input: names=[aaaaaaaaaaaaaaaaaaaa,Top,Middle], heights=[1,100000,50000]
PASS result | Expected: [Top,Middle,aaaaaaaaaaaaaaaaaaaa] | Actual: [Top,Middle,aaaaaaaaaaaaaaaaaaaa]
PASS names preserved | Expected: True | Actual: True
PASS heights preserved | Expected: True | Actual: True

Case: Height and name length boundaries [SortPeople2]
Input: names=[aaaaaaaaaaaaaaaaaaaa,Top,Middle], heights=[1,100000,50000]
PASS result | Expected: [Top,Middle,aaaaaaaaaaaaaaaaaaaa] | Actual: [Top,Middle,aaaaaaaaaaaaaaaaaaaa]
PASS names preserved | Expected: True | Actual: True
PASS heights preserved | Expected: True | Actual: True

Case: Maximum length [SortPeople]
Input: names=[PersonAAA..PersonBML], heights=[1..1000] (length 1000)
PASS result | Expected: [PersonBML,PersonBMK,PersonBMJ,...,PersonAAC,PersonAAB,PersonAAA] (length 1000) | Actual: [PersonBML,PersonBMK,PersonBMJ,...,PersonAAC,PersonAAB,PersonAAA] (length 1000)
PASS names preserved | Expected: True | Actual: True
PASS heights preserved | Expected: True | Actual: True

Case: Maximum length [SortPeople2]
Input: names=[PersonAAA..PersonBML], heights=[1..1000] (length 1000)
PASS result | Expected: [PersonBML,PersonBMK,PersonBMJ,...,PersonAAC,PersonAAB,PersonAAA] (length 1000) | Actual: [PersonBML,PersonBMK,PersonBMJ,...,PersonAAC,PersonAAB,PersonAAA] (length 1000)
PASS names preserved | Expected: True | Actual: True
PASS heights preserved | Expected: True | Actual: True

Summary: 42/42 checks passed.
```

## 專案結構

```plaintext
leetcode_2418/
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
└── leetcode_2418/
    ├── Program.cs
    └── leetcode_2418.csproj
```
