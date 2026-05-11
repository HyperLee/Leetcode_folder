# LeetCode 344 - Reverse String

這是一個使用 C# 與 .NET 10 撰寫的 LeetCode 344「Reverse String」解題範例。專案以 console application 呈現，`Main` 會執行幾組測試資料，展示字元陣列被原地反轉後的結果。

## 快速開始

從 repository 根目錄執行：

```powershell
dotnet build leetcode_344/leetcode_344.csproj
dotnet run --project leetcode_344/leetcode_344.csproj
```

預期執行輸出：

```text
Input:      ['h', 'e', 'l', 'l', 'o']
Solution 1: ['o', 'l', 'l', 'e', 'h']
Solution 2: ['o', 'l', 'l', 'e', 'h']

Input:      ['H', 'a', 'n', 'n', 'a', 'h']
Solution 1: ['h', 'a', 'n', 'n', 'a', 'H']
Solution 2: ['h', 'a', 'n', 'n', 'a', 'H']

Input:      []
Solution 1: []
Solution 2: []
```

> [!NOTE]
> `leetcode_344.slnx` 目前存在於 repository 中，但檔案尚未列出任何專案。此 README 以 `leetcode_344/leetcode_344.csproj` 作為可驗證的建置與執行目標。

## 題目說明

給定一個字元陣列 `s`，請撰寫一個函式反轉其中的字元。題目要求直接修改輸入陣列，不回傳新的字串或新的陣列。

範例：

```text
Input:  ['h', 'e', 'l', 'l', 'o']
Output: ['o', 'l', 'l', 'e', 'h']
```

## 限制條件

- 必須原地修改輸入的 `char[]`。
- 額外記憶體需為 `O(1)`。
- 輸入依照 LeetCode 題目契約視為非 null 字元陣列。

## 解題概念與出發點

字串反轉具有對稱性：第一個字元會移到最後一個位置，最後一個字元會移到第一個位置。依此類推，只要從陣列兩端開始交換字元，並逐步往中間靠攏，就能完成反轉。

這個出發點可以避免建立額外陣列，符合題目要求的原地修改與 `O(1)` 額外空間。

## 解法設計：雙指針

解法一使用雙指針。

- `left` 從陣列開頭開始。
- `right` 從陣列結尾開始。
- 當 `left < right` 時，交換 `s[left]` 與 `s[right]`。
- 每次交換後，`left` 往右移、`right` 往左移。
- 兩個指針相遇或交錯時，代表所有需要交換的位置都已處理完成。

時間複雜度為 `O(n)`，其中 `n` 是字元陣列長度。空間複雜度為 `O(1)`。

## 解法設計：單指針

解法二使用單指針。因為要交換的位置具有對稱關係，若左側索引是 `i`，右側索引就可以用 `n - 1 - i` 推導出來。

- `n` 是字元陣列長度。
- `i` 從 `0` 開始，只需要走到 `n / 2` 之前。
- 每次交換 `s[i]` 與 `s[n - 1 - i]`。
- 前半段處理完成後，後半段也會同步完成反轉。

這個寫法和雙指針解法做的是相同交換，只是右側索引不另外宣告成變數，而是每次由公式計算出來。

時間複雜度為 `O(n)`，其中 `n` 是字元陣列長度。空間複雜度為 `O(1)`。

## 範例演示流程

以下流程以解法一的 `left` 與 `right` 呈現。解法二會執行相同的交換，只是用 `i` 表示左側索引，並以 `n - 1 - i` 推導右側索引。

### 範例一：`['h', 'e', 'l', 'l', 'o']`

初始狀態：

```text
['h', 'e', 'l', 'l', 'o']
```

交換流程：

```text
left = 0, right = 4: 交換 'h' 與 'o' -> ['o', 'e', 'l', 'l', 'h']
left = 1, right = 3: 交換 'e' 與 'l' -> ['o', 'l', 'l', 'e', 'h']
left = 2, right = 2: 指針相遇，停止
```

結果：

```text
['o', 'l', 'l', 'e', 'h']
```

### 範例二：`['H', 'a', 'n', 'n', 'a', 'h']`

初始狀態：

```text
['H', 'a', 'n', 'n', 'a', 'h']
```

交換流程：

```text
left = 0, right = 5: 交換 'H' 與 'h' -> ['h', 'a', 'n', 'n', 'a', 'H']
left = 1, right = 4: 交換 'a' 與 'a' -> ['h', 'a', 'n', 'n', 'a', 'H']
left = 2, right = 3: 交換 'n' 與 'n' -> ['h', 'a', 'n', 'n', 'a', 'H']
left = 3, right = 2: 指針交錯，停止
```

結果：

```text
['h', 'a', 'n', 'n', 'a', 'H']
```

### 範例三：`[]`

空陣列沒有任何字元需要交換，`right` 會從 `-1` 開始，因此迴圈不會執行。

結果：

```text
[]
```

## 專案結構

```text
.
|-- docs/
|   `-- readme-template.md
|-- leetcode_344/
|   |-- leetcode_344.csproj
|   `-- Program.cs
|-- leetcode_344.slnx
`-- README.md
```

## 開發與驗證

建置專案：

```powershell
dotnet build leetcode_344/leetcode_344.csproj
```

執行範例：

```powershell
dotnet run --project leetcode_344/leetcode_344.csproj
```

目前 repository 沒有獨立測試專案；若之後新增測試，建議放在 sibling project，例如 `leetcode_344.Tests/`，並加入 solution，讓 `dotnet test` 可從根目錄執行。
