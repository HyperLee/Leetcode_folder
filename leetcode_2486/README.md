# LeetCode 2486：追加字元以取得子序列

本專案以 .NET 10 console application 示範 LeetCode 2486「Append Characters to String to Make Subsequence」。
程式保留原本的線性雙指標解法，另外加入位置索引搭配 lower-bound 二分搜尋的解法，並在 `Main` 中使用六組固定案例比較兩者結果。

- 題目連結：[LeetCode 2486](https://leetcode.com/problems/append-characters-to-string-to-make-subsequence/)
- 中文題目連結：[LeetCode 中文站](https://leetcode.cn/problems/append-characters-to-string-to-make-subsequence/description/)
- 專案檔案：`leetcode_2486/leetcode_2486.csproj`

## 題目說明

給定兩個只包含小寫英文字母的字串 `s` 與 `t`，可以將字元附加到 `s` 的尾端。請回傳最少需要附加多少個字元，才能讓 `t` 成為 `s` 的子序列。

子序列是從原字串刪除任意數量的字元後得到的字串，但保留剩餘字元的相對順序。例如，`ace` 是 `abcde` 的子序列；`aec` 則不是，因為 `e` 出現在 `c` 之後。

這題只能把字元加到 `s` 的尾端，因此前面已經在 `s` 中依序找到的部分，必須是 `t` 的前綴。找到的前綴越長，需要附加的字元就越少。

### 限制條件

- `1 <= s.length, t.length <= 10^5`
- `s` 與 `t` 只包含小寫英文字母。
- 方法只回傳需要附加的數量，不需要真的建立附加後的新字串。

## 解題概念與出發點

令 `tIndex` 表示目前已經在 `s` 中匹配完成的 `t` 前綴長度：

1. 從 `t[0]` 開始，嘗試依序在 `s` 中找相同字元。
2. 找到相同字元時，代表 `t` 的前綴又多匹配一個字元。
3. `s` 中不符合目前目標的字元可以被跳過，因為子序列允許刪除字元。
4. 掃描結束後，`t[tIndex..]` 無法再由原本的 `s` 提供，這一段必須全部附加到 `s` 尾端。

所以答案固定是：

```text
t.Length - tIndex
```

兩種解法都遵守相同的核心條件：匹配字元在 `s` 中必須由左到右出現，且只能消耗 `t` 的前綴。

## 解法一：線性雙指標

### API

```csharp
public static int AppendCharacters(string s, string t)
```

### 設計方式

使用兩個索引：

- `sIndex`：目前檢查到 `s` 的位置。
- `tIndex`：目前等待匹配的 `t` 位置，也等於已完成匹配的前綴長度。

每次迴圈都先比較 `s[sIndex]` 與 `t[tIndex]`：

- 如果相同，`tIndex` 前進，表示成功匹配 `t` 的下一個前綴字元。
- 不論是否相同，`sIndex` 都前進，因為目前的 `s` 字元已經處理完畢。
- 如果不相同，只能跳過 `s` 的字元，不能跳過 `t` 的目標字元；否則會錯過最長前綴的判斷。

當其中一個字串掃描完成時，`tIndex` 就是可保留的最長前綴長度，回傳 `t.Length - tIndex` 即可。

### 範例演示：`s = "coaching"`、`t = "coding"`

目前等待的 `t` 字元會在 `s` 中依序尋找：

| `sIndex` | `s[sIndex]` | 目前 `t[tIndex]` | 結果 | 下一步 |
|---:|:---:|:---:|:---|:---|
| 0 | `c` | `c` | 匹配 | `sIndex`、`tIndex` 都前進 |
| 1 | `o` | `o` | 匹配 | `sIndex`、`tIndex` 都前進 |
| 2 | `a` | `d` | 不匹配 | 只前進 `sIndex` |
| 3 | `c` | `d` | 不匹配 | 只前進 `sIndex` |
| 4 | `h` | `d` | 不匹配 | 只前進 `sIndex` |
| 5 | `i` | `d` | 不匹配 | 只前進 `sIndex` |
| 6 | `n` | `d` | 不匹配 | 只前進 `sIndex` |
| 7 | `g` | `d` | 不匹配 | 只前進 `sIndex` |

最後只匹配到 `t` 的前綴 `"co"`，剩下的 `"ding"` 需要附加，因此答案是 `6 - 2 = 4`。

### 複雜度

- 時間：`O(m + n)`，其中 `m = s.Length`、`n = t.Length`；兩個索引都只會向右前進。
- 額外空間：`O(1)`。

## 解法二：位置索引＋lower-bound 二分搜尋

### API

```csharp
public static int AppendCharacters2(string s, string t)
```

### 設計方式

這個解法先將 `s` 的資訊整理成 26 組位置清單。例如 `s = "coaching"` 時：

```text
c -> [0, 3]
o -> [1]
a -> [2]
h -> [4]
i -> [5]
n -> [6]
g -> [7]
d -> []
```

每一組清單天然按照索引遞增。接著用 `nextSearchIndex` 記住下一次允許搜尋的 `s` 起點：

1. 讀取目前的 `t[tIndex]`，取得該字元的位置清單。
2. 對位置清單執行 lower-bound，找出第一個大於或等於 `nextSearchIndex` 的位置。
3. 找到後，將 `nextSearchIndex` 設為該位置加一，確保下一個匹配一定位於右側。
4. 找不到時，代表 `t` 的前綴無法再延伸，剩餘 `t` 字元全部需要附加。

lower-bound 維持以下區間條件：

- `[0, left)` 的位置都小於目標值。
- `[right, Count)` 的位置都大於或等於目標值。
- 搜尋結束時 `left` 就是第一個符合條件的位置；如果 `left == Count`，表示不存在可用位置。

### 範例演示：`s = "coaching"`、`t = "coding"`

| `tIndex` | 目標字元 | 搜尋起點 | 位置清單 | lower-bound 結果 | 動作 |
|---:|:---:|---:|:---|---:|:---|
| 0 | `c` | 0 | `[0, 3]` | 0 | 匹配 `s[0]`，下一次從 1 搜尋 |
| 1 | `o` | 1 | `[1]` | 0 | 匹配 `s[1]`，下一次從 2 搜尋 |
| 2 | `d` | 2 | `[]` | 不存在 | 停止，剩下 `"ding"` |

可匹配的最長前綴仍然是 `"co"`，所以需要附加 `4` 個字元。

重複字元案例 `s = "aabb"`、`t = "aaab"` 可以說明搜尋起點的重要性：第一次匹配 `a` 使用索引 0，第二次使用索引 1；第三個 `a` 已沒有位於索引 2 之後的位置，因此停止並回傳剩餘的 `2` 個字元。

### 複雜度

- 建立位置清單：`O(m)`。
- 每個 `t` 字元執行一次 lower-bound：最壞 `O(log m)`，合計 `O(n log m)`。
- 總時間：`O(m + n log m)`。
- 額外空間：`O(m)`，所有 `s` 的索引各保存一次，另有 26 組清單。

這個解法在單次查詢時通常不如線性雙指標簡單，但它示範了如何把字串預處理成可重複查詢的索引結構。

## 兩種解法比較

| 解法 | 核心技巧 | 時間複雜度 | 額外空間 | 適合情境 |
|:---|:---|:---:|:---:|:---|
| `AppendCharacters` | 單次雙指標掃描 | `O(m + n)` | `O(1)` | 單次查詢、追求最佳效率 |
| `AppendCharacters2` | 字元位置索引＋lower-bound | `O(m + n log m)` | `O(m)` | 想重用 `s` 的位置資訊、示範索引查詢 |

兩個方法都不修改輸入字串，也不在方法內輸出內容；測試輸出集中在 `Main`。

## 內建測試案例

`Main` 內建六組案例，每組都會呼叫兩個解法，共 12 項檢查：

| 案例 | `s` | `t` | Expected |
|:---|:---|:---|---:|
| 官方案例 1 | `coaching` | `coding` | 4 |
| 官方案例 2 | `abcde` | `a` | 0 |
| 官方案例 3 | `z` | `abcde` | 5 |
| 非連續完整匹配 | `abcde` | `ace` | 0 |
| 重複字元部分匹配 | `aabb` | `aaab` | 2 |
| 完全沒有可匹配首字元 | `xyz` | `abc` | 3 |

如果任一實際結果不等於 Expected，程式會標示 `FAIL` 並以非零 exit code 結束。

## 執行方式

請在 repository root，也就是包含 `leetcode_2486` 資料夾的目錄執行：

```bash
dotnet restore leetcode_2486/leetcode_2486.csproj
dotnet build leetcode_2486/leetcode_2486.csproj --nologo
dotnet run --no-build --project leetcode_2486/leetcode_2486.csproj
```

本專案沒有獨立 automated test project；建置與固定 console harness 是目前的驗證方式。

## 範例執行結果

以下內容來自上述命令的實際執行：

```text
案例：官方案例 1
  s = "coaching", t = "coding", Expected = 4
  AppendCharacters: Actual = 4 => PASS
  AppendCharacters2: Actual = 4 => PASS
案例：官方案例 2
  s = "abcde", t = "a", Expected = 0
  AppendCharacters: Actual = 0 => PASS
  AppendCharacters2: Actual = 0 => PASS
案例：官方案例 3
  s = "z", t = "abcde", Expected = 5
  AppendCharacters: Actual = 5 => PASS
  AppendCharacters2: Actual = 5 => PASS
案例：非連續完整匹配
  s = "abcde", t = "ace", Expected = 0
  AppendCharacters: Actual = 0 => PASS
  AppendCharacters2: Actual = 0 => PASS
案例：重複字元部分匹配
  s = "aabb", t = "aaab", Expected = 2
  AppendCharacters: Actual = 2 => PASS
  AppendCharacters2: Actual = 2 => PASS
案例：完全沒有可匹配首字元
  s = "xyz", t = "abc", Expected = 3
  AppendCharacters: Actual = 3 => PASS
  AppendCharacters2: Actual = 3 => PASS
總結：12/12 項驗證通過
```

## 專案結構

```text
leetcode_2486/
├── leetcode_2486/
│   ├── Program.cs
│   └── leetcode_2486.csproj
├── docs/
│   └── readme-template.md
└── README.md
```
