# 1544. Make The String Great／整理字串

給定只含英文大小寫字母的字串，反覆刪除相鄰、同一字母但大小寫相反的字元對，直到沒有
可刪除的配對為止。本專案保留單一、純函式的 Stack<char> 解法。

- [LeetCode English](https://leetcode.com/problems/make-the-string-great/)
- [LeetCode 中文](https://leetcode.cn/problems/make-the-string-great/)

## 題目說明與限制條件

若兩個相鄰字元代表相同英文字母且大小寫不同，例如 a 與 A，便必須移除該字元對；
移除後新形成的相鄰字元也可能繼續消除。回傳無法再消除的最終字串。

- `1 <= s.length <= 100`
- `s` 僅包含英文大寫與小寫字母。
- `MakeGood` 只處理題目保證的有效輸入，不加入額外的無效輸入行為。

## 保留解法：Stack<char>

堆疊保存目前已處理字元的「已整理前綴」。讀入新字元時，只須與堆疊頂端比較：若兩者
相差 ASCII 的 32，便是同一英文字母的相反大小寫，彈出頂端；否則推入新字元。因為
彈出後的新頂端正是新形成的相鄰字元，所以一次掃描即可處理連鎖消除。

核心不變量：每次處理完一個輸入字元後，堆疊由底到頂恰好是該輸入前綴的已整理結果，
其中沒有可消除的相鄰配對。

容易出錯之處：

- 只做一次取代會漏掉移除後才相鄰的配對，例如 `abBA`。
- `a` 與 `a` 雖相同但不是相反大小寫，不可消除。
- Stack 枚舉順序是頂端到低端；建立結果時需從結果字串尾端回填，才能恢復輸入順序。

## 複雜度

| 方法 | 時間 | 結果空間 | 輔助空間 |
| --- | --- | --- | --- |
| `MakeGood`（Stack<char>） | `O(n)` | `O(n)` | `O(n)` |

每個字元至多被推入與彈出一次。結果字串依堆疊內容一次建立；Stack<char> 是唯一的演算法
輔助結構。

## 逐步走查

以 `abBAcC` 為例：

| 讀入 | 動作 | 堆疊（底至頂） |
| --- | --- | --- |
| `a` | 推入 | `a` |
| `b` | 推入 | `ab` |
| `B` | 與 `b` 配對，彈出 | `a` |
| `A` | 與 `a` 配對，彈出 | `(empty)` |
| `c` | 推入 | `c` |
| `C` | 與 `c` 配對，彈出 | `(empty)` |

最終回傳空字串。

## Acceptance Harness

Main 執行十個確定性案例。每案都印出 Case、Input、Expected、Actual 與 PASS/FAIL；任何
案例失敗會將 process exit code 設為 1。

| # | 輸入 | 預期 | 驗證目的 |
| ---: | --- | --- | --- |
| 1 | `leEeetcode` | `leetcode` | 官方範例與中間連鎖消除 |
| 2 | `abBAcC` | `(empty)` | 多段連鎖消除 |
| 3 | `s` | `s` | 最小長度 |
| 4 | `aa` | `aa` | 同大小寫不可消除 |
| 5 | `abA` | `abA` | 非相鄰或不同字母不可消除 |
| 6 | `aA` | `(empty)` | 小寫後大寫配對 |
| 7 | `Aa` | `(empty)` | 大寫後小寫配對 |
| 8 | `abBA` | `(empty)` | 移除後形成的新配對 |
| 9 | `aAbBc` | `c` | 多次連鎖後保留尾端 |
| 10 | `aA` 重複 50 次（長度 100） | `(empty)` | 題目上限 spot check |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_1544/leetcode_1544/leetcode_1544.csproj --nologo
dotnet run --no-build --project leetcode_1544/leetcode_1544/leetcode_1544.csproj
```

若直接開啟題目根目錄 `leetcode_1544/`，使用：

```bash
dotnet build leetcode_1544/leetcode_1544.csproj --nologo
dotnet run --no-build --project leetcode_1544/leetcode_1544.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1
Input: leEeetcode
Expected: leetcode
Actual: leetcode
Result: PASS

Case: 2
Input: abBAcC
Expected: (empty)
Actual: (empty)
Result: PASS

Case: 3
Input: s
Expected: s
Actual: s
Result: PASS

Case: 4
Input: aa
Expected: aa
Actual: aa
Result: PASS

Case: 5
Input: abA
Expected: abA
Actual: abA
Result: PASS

Case: 6
Input: aA
Expected: (empty)
Actual: (empty)
Result: PASS

Case: 7
Input: Aa
Expected: (empty)
Actual: (empty)
Result: PASS

Case: 8
Input: abBA
Expected: (empty)
Actual: (empty)
Result: PASS

Case: 9
Input: aAbBc
Expected: c
Actual: c
Result: PASS

Case: 10
Input: aAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaA
Expected: (empty)
Actual: (empty)
Result: PASS

Summary: 10/10 checks passed.
```

## 專案結構

```plaintext
leetcode_1544/
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
└── leetcode_1544/
    ├── Program.cs
    └── leetcode_1544.csproj
```
