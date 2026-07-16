# 1436. Destination City／旅行終點站

使用兩趟 HashSet 線性掃描，找出旅行路線中唯一沒有外出路徑的城市。

- [English problem](https://leetcode.com/problems/destination-city/)
- [中文題目](https://leetcode.cn/problems/destination-city/)

## 題目說明

每個 `paths[i] = [cityA, cityB]` 代表一條從 `cityA` 前往 `cityB` 的單向路徑。所有路徑形成一條沒有迴圈的旅行線，因此恰好有一座城市沒有任何外出路徑；回傳這座終點城市。

## 限制條件

- `1 <= paths.Length <= 100`
- `paths[i].Count == 2`
- `1 <= cityA.Length, cityB.Length <= 10`
- `cityA != cityB`
- 城市名稱只包含大小寫英文字母與空格。

## 解法與核心不變量

先將每條路徑的出發城市加入 `HashSet<string>`，再逐一檢查抵達城市。中途城市仍有下一段路徑，因此必定出現在出發城市集合；唯一不在集合中的抵達城市就是終點。

保留兩趟掃描可讓不變量直接對應程式碼，也不依賴 `paths` 的排列順序。公開方法只回傳答案，不修改輸入或輸出到主控台。Acceptance harness 會在呼叫前深層複製每個案例的路徑，並把答案正確與完整巢狀結構未變動共同列為該案例 PASS 的必要條件。

### 複雜度

- 時間複雜度：`O(n)`
- 結果空間：`O(1)`
- 輔助空間：`O(n)`，用於出發城市集合

## 逐步範例

官方範例一：

```plaintext
London -> New York -> Lima -> Sao Paulo
```

出發城市集合為 `{ London, New York, Lima }`。三個抵達城市中，只有 `Sao Paulo` 不在集合內，所以它是旅行終點。

## Acceptance harness

| 案例 | 驗證重點 | 預期結果 |
| --- | --- | --- |
| Official example 1 | 一般連續路線 | `Sao Paulo` |
| Official example 2 | 輸入順序不是旅行順序 | `A` |
| Official example 3 / minimum input | 單一路徑 | `Z` |
| Shuffled path order | 額外驗證順序獨立性 | `Delta` |
| Early destination candidate becomes a source | 防止過早回傳 | `D` |
| City names containing spaces | 合法空格字元 | `Cape Town` |
| Case-sensitive city names | 大小寫不同城市 | `a` |
| Maximum 100-path chain | 100 條合法路徑、101 個僅含英文字母的城市，以及輸入不變 | `CityCW` |

本專案沒有正式測試專案；`Main` 的確定性 acceptance harness 是可執行驗證機制。八個案例各自同時驗證回傳值與 `paths` 深層結構未被修改；任一案例失敗時，程式會將 exit code 設為 1。

## 建置與執行

從此題目根目錄 `leetcode_1436/` 執行：

```bash
dotnet build leetcode_1436/leetcode_1436.csproj --nologo
dotnet run --no-build --project leetcode_1436/leetcode_1436.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: Official example 1
Input: [[London -> New York], [New York -> Lima], [Lima -> Sao Paulo]]
Expected: Sao Paulo
Actual: Sao Paulo
Result: PASS

Case: Official example 2
Input: [[B -> C], [D -> B], [C -> A]]
Expected: A
Actual: A
Result: PASS

Case: Official example 3 / minimum input
Input: [[A -> Z]]
Expected: Z
Actual: Z
Result: PASS

Case: Shuffled path order
Input: [[Gamma -> Delta], [Alpha -> Beta], [Beta -> Gamma]]
Expected: Delta
Actual: Delta
Result: PASS

Case: Early destination candidate becomes a source
Input: [[A -> B], [C -> D], [B -> C]]
Expected: D
Actual: D
Result: PASS

Case: City names containing spaces
Input: [[New York -> Rio City], [Rio City -> Cape Town]]
Expected: Cape Town
Actual: Cape Town
Result: PASS

Case: Case-sensitive city names
Input: [[A -> a]]
Expected: a
Actual: a
Result: PASS

Case: Maximum 100-path chain
Input: [100-path chain CityA -> ... -> CityCW]
Expected: CityCW
Actual: CityCW
Result: PASS

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_1436/
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
│       │   └── 2026-07-17-leetcode-1436-net10-migration.md
│       └── specs/
│           └── 2026-07-17-leetcode-1436-net10-migration-design.md
└── leetcode_1436/
    ├── Program.cs
    └── leetcode_1436.csproj
```
