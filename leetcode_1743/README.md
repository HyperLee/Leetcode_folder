# LeetCode 1743 - Restore the Array From Adjacent Pairs（從相鄰元素對還原陣列）
- [LeetCode English](https://leetcode.com/problems/restore-the-array-from-adjacent-pairs/)
- [LeetCode 中文](https://leetcode.cn/problems/restore-the-array-from-adjacent-pairs/)

## 題目說明

有一個由 `n` 個相異整數組成的陣列 `nums`，但現在只保留了所有相鄰元素對。
`adjacentPairs[i] = [uᵢ, vᵢ]` 表示兩個值在原陣列中相鄰；每組 pair 的方向以及
pairs 之間的順序都不保證與原陣列一致。請還原任一個符合全部相鄰關係的陣列；整體反向
也是合法答案。

## 限制條件與 API

- `nums.length == n`
- `adjacentPairs.length == n - 1`
- `adjacentPairs[i].length == 2`
- `2 <= n <= 100000`
- `-100000 <= nums[i], uᵢ, vᵢ <= 100000`
- 題目保證至少存在一個符合所有 pairs 的原始陣列。

```csharp
public int[] RestoreArray(int[][] adjacentPairs);
```

## 設計、不變量與取捨

把每組 pair 視為無向圖的一條邊。因原陣列的元素皆相異且 pairs 完整描述一條鏈：

- 兩個端點的度數恰為 1，任選一端作為起點只會決定輸出方向。
- 每個內部節點的度數恰為 2。
- 走到內部節點時，其中一個鄰居是上一個值；排除它後，另一個鄰居就是下一個值。

實作先建立雙向鄰接表，再從端點線性走訪。相較於 DFS，直接記住前一個值便不需要額外
`visited` 集合或遞迴堆疊；相較於反覆拼接陣列片段，也不會退化成平方時間。解法只讀取
`adjacentPairs`，不改變任何 pair。

- 時間複雜度：`O(n)`；建立鄰接表與走訪各一次。
- 結果空間：`O(n)`；回傳長度為 `n` 的陣列。
- 輔助空間：`O(n)`；鄰接表共保存 `2(n - 1)` 筆鄰接關係。

## 逐步走查

以 `adjacentPairs = [[2,1],[3,4],[3,2]]` 為例：

```plaintext
鄰接表：1->[2], 2->[1,3], 3->[4,2], 4->[3]
選擇端點 1：restored = [1]
唯一鄰居為 2：restored = [1,2]
在 2 排除上一個值 1，選擇 3：restored = [1,2,3]
在 3 排除上一個值 2，選擇 4：restored = [1,2,3,4]
```

若從端點 4 開始，得到 `[4,3,2,1]`，同樣符合全部相鄰關係。

## Acceptance harness

| Case | 驗證重點 |
| --- | --- |
| Official example 1 | pair 順序與方向不等於輸出順序。 |
| Official example 2 | 包含負數，並接受整體反向答案。 |
| Minimum valid input | 官方第三例，也是只有一組 pair 的最小輸入。 |
| Scrambled order and directions | 多組 pair 同時打亂順序與方向。 |
| Zero is an internal value | 驗證 `0` 不會被誤當成未設定的端點。 |
| Longer regression chain | 驗證每一步都排除上一個節點，不會回頭。 |
| Maximum length spot check | 100000 個值、反向 pair 順序與交錯方向，不輸出巨大陣列。 |
| Empty result reporting regression | 模擬錯誤解法回傳空陣列，確認 harness 仍會安全輸出結果而不拋例外。 |

每個解法案例也會深複製輸入，確認呼叫前後每組 pair 的內容完全一致。答案驗證不強迫固定
方向，而是檢查長度、相異元素集合，以及輸出的每一組相鄰值都存在於輸入 pairs。

## 建置與執行

以下命令的工作目錄是 `leetcode_1743/` 題目根目錄：

```bash
dotnet build leetcode_1743/leetcode_1743.csproj --nologo
dotnet run --no-build --project leetcode_1743/leetcode_1743.csproj
```

本題沒有正式測試專案；`Main` 中的 deterministic acceptance harness 是可重複執行的
驗證入口。以下為 fresh run 的完整輸出：

```text
Case: Official example 1
Input: adjacentPairs=[[2,1],[3,4],[3,2]]
Expected: valid restoration; input preserved=true
Actual: restored=[1,2,3,4]; valid restoration=true; input preserved=true
Result: PASS

Case: Official example 2
Input: adjacentPairs=[[4,-2],[1,4],[-3,1]]
Expected: valid restoration; input preserved=true
Actual: restored=[-2,4,1,-3]; valid restoration=true; input preserved=true
Result: PASS

Case: Minimum valid input
Input: adjacentPairs=[[100000,-100000]]
Expected: valid restoration; input preserved=true
Actual: restored=[100000,-100000]; valid restoration=true; input preserved=true
Result: PASS

Case: Scrambled order and directions
Input: adjacentPairs=[[7,5],[1,3],[9,7],[5,3]]
Expected: valid restoration; input preserved=true
Actual: restored=[1,3,5,7,9]; valid restoration=true; input preserved=true
Result: PASS

Case: Zero is an internal value
Input: adjacentPairs=[[6,0],[-2,0]]
Expected: valid restoration; input preserved=true
Actual: restored=[6,0,-2]; valid restoration=true; input preserved=true
Result: PASS

Case: Longer regression chain
Input: adjacentPairs=[[8,6],[2,4],[10,8],[6,4],[12,10]]
Expected: valid restoration; input preserved=true
Actual: restored=[2,4,6,8,10,12]; valid restoration=true; input preserved=true
Result: PASS

Case: Maximum length spot check
Input: 100000 values from -50000 through 49999; reversed pair order and alternating directions
Expected: length=100000; valid restoration; input preserved=true
Actual: length=100000; endpoints=[49999,-50000]; valid restoration=true; input preserved=true
Result: PASS

Case: Empty result reporting regression
Input: simulated invalid restoration=[]
Expected: length=0; endpoints=[]; valid restoration=false; input preserved=true
Actual: length=0; endpoints=[]; valid restoration=false; input preserved=true
Result: PASS

Summary: 8/8 checks passed.
```

## 最終專案結構

```plaintext
leetcode_1743/
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
│       │   └── 2026-07-22-leetcode-1743-net10.md
│       └── specs/
│           └── 2026-07-22-leetcode-1743-net10-design.md
└── leetcode_1743/
    ├── Program.cs
    └── leetcode_1743.csproj
```
