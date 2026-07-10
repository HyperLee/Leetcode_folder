# LeetCode 389：找不同

這是一個以 C# 撰寫的 .NET 10 主控台專案，示範三種找出新增字母的方式：
排序後比對、可變串列逐一移除，以及 XOR 消去法。專案保留並修復舊版思路，
同時用確定性 acceptance harness 讓六組案例逐一驗證三種實作。

- [英文題目：389. Find the Difference](https://leetcode.com/problems/find-the-difference/)
- [中文題目：389. 找不同](https://leetcode.cn/problems/find-the-difference/)

## 題目說明

給定字串 `s` 與 `t`。`t` 是先將 `s` 的字母重新排列，再額外加入一個字母
所形成的字串。請找出並回傳這個新增的字母。

例如 `s = "abcd"`、`t = "abcde"` 時，新增的字母是 `'e'`。字母可以重複，
而且重新排列後新增字母不一定位於 `t` 的尾端，因此解法不能只比較最後一個
位置。

## 限制條件

- `0 <= s.Length <= 1000`
- `t.Length == s.Length + 1`
- `s` 與 `t` 只包含小寫英文字母。
- `t` 一定是將 `s` 重新排列後再加入一個字母的結果。
- 本實作依照題目輸入契約設計，不另外定義 `null` 或無效輸入的行為。

## 舊版 List 寫法的問題與修復

舊版方法分別把 `s`、`t` 放進兩個串列，再逐一檢查 `s` 的字母是否存在於
`t`。但題目已保證 `t` 包含 `s` 的全部字母，所以這個檢查不會找出新增字母；
最後程式只好直接回傳 `t` 的最後一個字母，等於錯誤假設新增字母一定在尾端。

反例是 `s = "abcd"`、`t = "eabcd"`：正確答案為 `'e'`，舊版卻回傳尾端的
`'d'`。此外，單純使用 `Contains` 不會消耗已匹配的字母，也無法正確表達重複
字母的數量差。

修復後的 `FindTheDifference2` 先用 `t` 的所有字母建立一個可變串列，再針對
`s` 的每個字母呼叫一次 `Remove`。`List<T>.Remove` 每次只移除第一個相符項目，
所以重複字母會逐一配對；完成後唯一剩下的字母就是答案。

## 三種解法

### 1. 排序後比對：`FindTheDifference`

1. 將 `s` 與 `t` 各自複製為字元陣列。
2. 排序兩個陣列，使相同字母落在相同索引。
3. 從索引 0 開始尋找第一個不同位置，回傳 `t` 陣列的字母。
4. 若前 `s.Length` 個位置全部相同，新增字母就是排序後 `t` 的最後一個字母。

這個方法容易理解，但排序是主要成本。

### 2. 可變串列移除：`FindTheDifference2`

先把 `t` 複製到 `List<char>`，再為 `s` 的每個字母移除一個相符項目。這保留
舊版使用 List 比對的方向，同時修正「新增字母在尾端」及重複字母計數問題。
由於每次 `Remove` 都可能線性掃描串列，整體時間為平方級。

### 3. XOR 消去法：`FindTheDifference3`

用同一個整數累加器 XOR `s` 與 `t` 的每個字元碼。XOR 具有交換律與結合律，
而且 `x ^ x == 0`、`0 ^ x == x`；兩個字串中成對出現的字母會互相消去，最後
只留下新增字母的字元碼。這是三種方法中時間與額外空間都最精簡的解法。

## 複雜度比較

令 `n = s.Length`：

| 解法 | 方法 | 時間複雜度 | 額外空間 |
| --- | --- | --- | --- |
| 排序後比對 | `FindTheDifference` | `O(n log n)` | `O(n)` |
| 可變串列移除 | `FindTheDifference2` | `O(n^2)` | `O(n)` |
| XOR 消去法 | `FindTheDifference3` | `O(n)` | `O(1)` |

## 逐步示範

### 排序：新增字母位於重新排列後的開頭

以 `s = "abcd"`、`t = "eabcd"` 為例，排序後得到：

- `s`：`[a, b, c, d]`
- `t`：`[a, b, c, d, e]`

前四個位置相同，因此回傳 `t` 的最後一個字母 `'e'`。原始 `t` 中 `'e'` 在
開頭並不影響結果，因為解法比較的是排序後陣列。

### List：重複字母逐一消耗

以 `s = "aabbcc"`、`t = "cbacaba"` 為例，初始剩餘串列包含 `t` 的七個字母。
依序為 `s` 移除兩個 `'a'`、兩個 `'b'`、兩個 `'c'` 後，串列只剩一個 `'a'`，
所以新增字母是 `'a'`。每次只移除一個相符項目，正是重複字母能正確配對的
關鍵。

### XOR：相同字母互相消去

以 `s = "ae"`、`t = "aea"` 為例，把兩邊所有字元碼 XOR 在一起，可重新分組
為 `('a' ^ 'a') ^ ('e' ^ 'e') ^ 'a'`。前兩組都變成 0，最後保留的 `'a'`
就是答案。

## 可執行驗證案例

`Main` 對每組案例執行三個方法，共 18 項檢查：

| `s` | `t` | 預期 | 驗證重點 |
| --- | --- | --- | --- |
| `"abcd"` | `"abcde"` | `'e'` | 基本案例，新增字母在尾端 |
| `""` | `"y"` | `'y'` | 空的來源字串 |
| `"abcd"` | `"eabcd"` | `'e'` | 新增字母在重新排列結果開頭 |
| `"a"` | `"aa"` | `'a'` | 最小重複字母案例 |
| `"ae"` | `"aea"` | `'a'` | 重複字母與重新排列 |
| `"aabbcc"` | `"cbacaba"` | `'a'` | 多組重複字母 |

每項檢查都輸出預期值、實際值與 `PASS`／`FAIL`。若任何檢查失敗，程式會將
`Environment.ExitCode` 設為 1。此專案沒有獨立測試專案或測試框架；可執行
驗證器是目前的主要驗證方式。

## 建置與執行

請從此 README 所在的外層 `leetcode_389` 目錄執行：

```bash
dotnet build leetcode_389/leetcode_389.csproj --nologo
dotnet run --no-build --project leetcode_389/leetcode_389.csproj
```

以下是重新建置後執行第二個命令的完整輸出：

```text
LeetCode 389 acceptance harness

Case 1
Input: s = "abcd", t = "abcde"
Expected added character: 'e'
PASS | Sorting | Expected: 'e' | Actual: 'e'
PASS | List removal | Expected: 'e' | Actual: 'e'
PASS | XOR | Expected: 'e' | Actual: 'e'

Case 2
Input: s = "", t = "y"
Expected added character: 'y'
PASS | Sorting | Expected: 'y' | Actual: 'y'
PASS | List removal | Expected: 'y' | Actual: 'y'
PASS | XOR | Expected: 'y' | Actual: 'y'

Case 3
Input: s = "abcd", t = "eabcd"
Expected added character: 'e'
PASS | Sorting | Expected: 'e' | Actual: 'e'
PASS | List removal | Expected: 'e' | Actual: 'e'
PASS | XOR | Expected: 'e' | Actual: 'e'

Case 4
Input: s = "a", t = "aa"
Expected added character: 'a'
PASS | Sorting | Expected: 'a' | Actual: 'a'
PASS | List removal | Expected: 'a' | Actual: 'a'
PASS | XOR | Expected: 'a' | Actual: 'a'

Case 5
Input: s = "ae", t = "aea"
Expected added character: 'a'
PASS | Sorting | Expected: 'a' | Actual: 'a'
PASS | List removal | Expected: 'a' | Actual: 'a'
PASS | XOR | Expected: 'a' | Actual: 'a'

Case 6
Input: s = "aabbcc", t = "cbacaba"
Expected added character: 'a'
PASS | Sorting | Expected: 'a' | Actual: 'a'
PASS | List removal | Expected: 'a' | Actual: 'a'
PASS | XOR | Expected: 'a' | Actual: 'a'

Summary: 18/18 checks passed.
```

## 專案結構

```plaintext
.
├── .editorconfig              # C# 與結構化檔案的格式規範
├── .gitattributes             # 文字與二進位檔案屬性
├── .gitignore                 # .NET／IDE 產生檔案排除規則
├── .vscode/
│   ├── launch.json            # 直接偵錯 net10.0 輸出
│   └── tasks.json             # 預設建置工作
├── docs/
│   └── readme-template.md     # 初次建立 README 的範本
├── leetcode_389/
│   ├── Program.cs             # 三種解法與可執行驗證器
│   └── leetcode_389.csproj    # .NET 10 SDK 專案設定
├── AGENTS.md                  # 本專案協作指南
└── README.md                  # 題目、解法與驗證紀錄
```
