# LeetCode 2609：Find the Longest Balanced Substring of a Binary String

以 C# 與 .NET 10 實作 LeetCode 2609「最長平衡子字串」。專案保留三個 O(n) 解法，
並由 `Main` 使用固定測試資料逐一驗證所有實作。

- [題目連結（LeetCode）](https://leetcode.com/problems/find-the-longest-balanced-substring-of-a-binary-string/)
- [題目連結（力扣）](https://leetcode.cn/problems/find-the-longest-balanced-substring-of-a-binary-string/)
- [解題概念與出發點](#解題概念與出發點)
- [三種解法](#三種解法)
- [建置與執行](#建置與執行)

## 題目說明

給定一個只包含 `0` 與 `1` 的字串 `s`。若其中一段連續子字串同時符合下列條件，
就稱為「平衡子字串」：

1. 所有 `0` 都出現在所有 `1` 之前。
2. `0` 與 `1` 的數量相同。

目標是回傳最長平衡子字串的長度。空字串也被視為平衡子字串，因此找不到非空答案時回傳 `0`。

例如：

```text
s = "01000111"
```

其中 `"000111"` 是合法平衡子字串：

- 三個 `0` 全部位於三個 `1` 之前。
- `0` 與 `1` 都有三個。
- 長度為 `3 + 3 = 6`。

### 限制條件

```text
1 <= s.length <= 50
s[i] 為 '0' 或 '1'
```

> [!NOTE]
> Console runner 額外包含空字串案例，用來確認三個方法在此安全輸入下都會回傳 `0`。
> 空字串不是 LeetCode 正式輸入範圍的一部分。

### 官方範例

#### 範例一

```text
輸入：s = "01000111"
輸出：6
```

最長平衡子字串為 `"000111"`。

#### 範例二

```text
輸入：s = "00111"
輸出：4
```

可以選擇 `"0011"`。雖然原字串最後還有一個 `1`，但只有兩個 `0` 可以與 `1` 配對，
所以最長長度是 `2 × 2 = 4`。

#### 範例三

```text
輸入：s = "111"
輸出：0
```

字串中沒有任何 `0`，因此除了空字串之外，不存在平衡子字串。

## 解題概念與出發點

### 平衡子字串一定長什麼樣子？

因為所有 `0` 必須在所有 `1` 之前，所以合法子字串只能具有以下形式：

```text
000...000111...111
└─ a 個 0 ┘└─ b 個 1 ┘
```

若要讓兩種字元數量相同，只能從兩段中各取：

```text
min(a, b)
```

個字元。因此，這一對相鄰 `0`、`1` 區段能形成的最長平衡子字串長度為：

```text
2 × min(a, b)
```

這也是三個解法共同使用的核心公式。

### 為什麼只需要關注連續區段？

子字串必須連續，不能跳過中間字元。例如：

```text
0011100
```

最後兩個 `0` 不能跨過中間的 `111`，再與開頭的 `00` 合併。每當掃描方向從 `1`
重新回到 `0`，前一組候選 `0...01...1` 就已經結束，後續答案必須從新的 `0` 區段開始計算。

### 為什麼答案一定是偶數？

平衡子字串包含相同數量的 `0` 與 `1`。若各有 `k` 個，總長度就是：

```text
k + k = 2k
```

所以任何非空答案必定是偶數。

## 三種解法

三個方法都使用相同的數學關係，但記錄掃描狀態的方式不同：

| 方法 | 狀態表示 | 更新答案的時機 |
| --- | --- | --- |
| `FindTheLongestBalancedSubstring` | 目前候選區段的 `zeroCount`、`oneCount` | 每讀到一個 `1` |
| `FindTheLongestBalancedSubstring2` | 每輪完整統計一段 `0` 與其後一段 `1` | 每組區段掃描完成後 |
| `FindTheLongestBalancedSubstring3` | 上一個與目前同字元分組的長度 | 一段連續 `1` 結束時 |

### 解法一：逐字元雙計數

`FindTheLongestBalancedSubstring` 在一次由左至右的掃描中維護：

- `zeroCount`：目前候選中連續 `0` 的數量。
- `oneCount`：緊接在這段 `0` 後面的連續 `1` 數量。
- `maxLength`：目前找到的最大答案。

#### 設計步驟

1. 遇到 `0`：
   - 若它是第一個字元，或前一個字元是 `1`，表示新的 `0` 區段開始。
   - 將 `zeroCount` 設為 `1`，並把舊的 `oneCount` 清為 `0`。
   - 否則，它仍屬於目前的連續 `0` 區段，只增加 `zeroCount`。
2. 遇到 `1`：
   - 增加 `oneCount`。
   - 目前可配對的字元組數為 `Math.Min(zeroCount, oneCount)`。
   - 使用其兩倍更新 `maxLength`。

#### 為什麼遇到新的 `0` 必須重設？

假設已經掃描到：

```text
000111
```

此時候選區段已經完成。下一個字元若是 `0`，字串會變成：

```text
0001110
```

新的 `0` 位於 `1` 之後，不能再與開頭的 `000` 共同形成「所有 0 都在 1 前面」的子字串，
所以必須開始一組全新的候選區段。

#### 範例流程：`s = "01000111"`

| 索引 | 字元 | 動作 | `zeroCount` | `oneCount` | `maxLength` |
| ---: | :---: | --- | ---: | ---: | ---: |
| 0 | `0` | 開始新的 0 區段 | 1 | 0 | 0 |
| 1 | `1` | 可配對 `min(1,1)` 組 | 1 | 1 | 2 |
| 2 | `0` | 前一字元是 1，重設候選 | 1 | 0 | 2 |
| 3 | `0` | 延長目前 0 區段 | 2 | 0 | 2 |
| 4 | `0` | 延長目前 0 區段 | 3 | 0 | 2 |
| 5 | `1` | 可配對 `min(3,1)` 組 | 3 | 1 | 2 |
| 6 | `1` | 可配對 `min(3,2)` 組 | 3 | 2 | 4 |
| 7 | `1` | 可配對 `min(3,3)` 組 | 3 | 3 | 6 |

最後回傳 `6`。

#### 正確性說明

掃描到每個 `1` 時，`zeroCount` 代表它前方最近一段連續 `0` 的長度，`oneCount`
代表目前連續 `1` 已出現的長度。兩段按照原字串順序相鄰，所以由兩段各取較短長度，
必定可以得到合法的 `0...01...1`。反過來，任何合法平衡子字串都會結束於某個 `1`；
演算法掃描到該位置時會計算包含它的最佳配對，因此不會漏掉最長答案。

### 解法二：成對掃描 0/1 區段

`FindTheLongestBalancedSubstring2` 不在每個字元上更新答案，而是一次處理完整的一對區段：

```text
[一段連續 0][緊接的一段連續 1]
```

#### 設計步驟

1. 從目前 `index` 開始，使用第一個 `while` 統計連續 `0` 的數量。
2. 接著使用第二個 `while` 統計連續 `1` 的數量。
3. 計算 `2 × Math.Min(zeroCount, oneCount)`。
4. 使用結果更新最大值。
5. `index` 此時已位於下一組候選的起點，重複上述流程。

這個寫法直接對應題目要求的形狀，因此區段邊界相當容易觀察。

#### 範例流程：`s = "00110011"`

字串可分成：

```text
00 | 11 | 00 | 11
```

| 輪次 | 連續 0 | 緊接的連續 1 | 候選長度 | 最大值 |
| ---: | ---: | ---: | ---: | ---: |
| 1 | 2 | 2 | `2 × min(2,2) = 4` | 4 |
| 2 | 2 | 2 | `2 × min(2,2) = 4` | 4 |

兩個平衡區段不能跨越中間的 `10` 邊界合併，所以答案不是 8，而是 4。

#### 不對稱區段如何處理？

以 `"00111"` 為例：

```text
zeroCount = 2
oneCount  = 3
```

只能從三個 `1` 中取兩個與兩個 `0` 配對：

```text
2 × min(2, 3) = 4
```

對稱地，`"00011"` 也只能取兩個 `0` 與兩個 `1`，答案同樣是 4。

#### 正確性說明

任何平衡子字串必定完全位於某一段連續 `0` 與其後相鄰的連續 `1` 之內。
每輪掃描都完整取得這兩段的長度，並算出它們能形成的最佳答案。
演算法依序處理字串中的所有相鄰 `0→1` 區段，所以所有可能的平衡子字串都會被涵蓋。

### 解法三：相鄰分組長度

`FindTheLongestBalancedSubstring3` 將字串看成多個「相同字元的連續分組」。

例如：

```text
"01000111" → "0" | "1" | "000" | "111"
分組長度       1     1       3       3
```

方法只需保存：

- `previousRunLength`：上一組的長度。
- `currentRunLength`：目前組的長度。
- `maxLength`：目前最大答案。

#### 設計步驟

1. 每讀到一個字元，就增加 `currentRunLength`。
2. 若下一個字元不同，或已到字串尾端，代表目前分組結束。
3. 只有目前分組的字元是 `1` 時才計算答案：
   - 在二進位字串中，相鄰分組必定交替。
   - 目前組若是 `1`，存在的上一組必定是 `0`。
4. 使用 `2 × min(previousRunLength, currentRunLength)` 更新答案。
5. 將目前組長度保存成上一組，並開始下一組。

#### 為什麼目前組為 `0` 時不更新？

平衡子字串要求 `0` 在前、`1` 在後。若目前分組是 `0`，上一組只能是 `1`，
兩者形成的是 `1...10...0`，順序不合法。這組 `0` 仍需保留，因為它可能與下一組 `1`
形成合法候選。

#### 範例流程：`s = "01000111"`

| 完成的分組 | 上一組長度 | 目前組長度 | 是否計算 | 候選答案 |
| --- | ---: | ---: | --- | ---: |
| `"0"` | 0 | 1 | 否，目前是 0 | 0 |
| `"1"` | 1 | 1 | 是，形成 `"01"` | 2 |
| `"000"` | 1 | 3 | 否，目前是 0 | 2 |
| `"111"` | 3 | 3 | 是，形成 `"000111"` | 6 |

最後回傳 `6`。

#### 正確性說明

分組後，每一個合法候選都對應到一組連續 `0` 及緊接的一組連續 `1`。
當 `1` 分組結束時，演算法擁有這兩組的完整長度，能計算該邊界的最佳平衡長度。
所有 `1` 分組都會被處理一次，因此所有合法 `0→1` 邊界都會被比較，最大值即為答案。

## 複雜度分析

| 方法 | 時間複雜度 | 額外空間複雜度 | 特點 |
| --- | --- | --- | --- |
| `FindTheLongestBalancedSubstring` | O(n) | O(1) | 每遇到 1 即更新，容易觀察即時答案 |
| `FindTheLongestBalancedSubstring2` | O(n) | O(1) | 直接掃描成對區段，最貼近題目形狀 |
| `FindTheLongestBalancedSubstring3` | O(n) | O(1) | 使用通用的連續分組觀點，只保留兩組長度 |

解法二雖然包含兩個內層 `while`，但 `index` 只會向右移動，每個字元最多被掃描一次，
因此不是 O(n²)，仍然是 O(n)。

## 可執行測試案例

`Main` 會讓三個方法分別執行以下十組案例：

| 輸入 | 預期結果 | 驗證目的 |
| --- | ---: | --- |
| `"01000111"` | 6 | 官方案例；先有短候選，再出現更長候選 |
| `"00111"` | 4 | 官方案例；1 的區段比 0 長 |
| `"111"` | 0 | 官方案例；沒有可配對的 0 |
| `""` | 0 | 題目範圍外的空字串安全行為 |
| `"0"` | 0 | 最小長度且只有 0 |
| `"0000"` | 0 | 全部為 0 |
| `"1111"` | 0 | 全部為 1 |
| `"01"` | 2 | 最小非空平衡子字串 |
| `"000111"` | 6 | 完整且等長的兩段 |
| `"00110011"` | 4 | 多組平衡候選不能跨越 `10` 合併 |

共執行：

```text
10 組案例 × 3 個解法 = 30 次比較
```

## 建置與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

在此工作區根目錄執行：

```powershell
dotnet build .\leetcode_2609\leetcode_2609.csproj
dotnet run --project .\leetcode_2609\leetcode_2609.csproj
dotnet format .\leetcode_2609\leetcode_2609.csproj --verify-no-changes
git diff --check
```

### 實際執行輸出

```text
LeetCode 2609 - Find the Longest Balanced Substring of a Binary String

Case 1: s = "01000111", expected = 6
  FindTheLongestBalancedSubstring      actual = 6, expected = 6 => PASS
  FindTheLongestBalancedSubstring2     actual = 6, expected = 6 => PASS
  FindTheLongestBalancedSubstring3     actual = 6, expected = 6 => PASS

Case 2: s = "00111", expected = 4
  FindTheLongestBalancedSubstring      actual = 4, expected = 4 => PASS
  FindTheLongestBalancedSubstring2     actual = 4, expected = 4 => PASS
  FindTheLongestBalancedSubstring3     actual = 4, expected = 4 => PASS

Case 3: s = "111", expected = 0
  FindTheLongestBalancedSubstring      actual = 0, expected = 0 => PASS
  FindTheLongestBalancedSubstring2     actual = 0, expected = 0 => PASS
  FindTheLongestBalancedSubstring3     actual = 0, expected = 0 => PASS

Case 4: s = "", expected = 0
  FindTheLongestBalancedSubstring      actual = 0, expected = 0 => PASS
  FindTheLongestBalancedSubstring2     actual = 0, expected = 0 => PASS
  FindTheLongestBalancedSubstring3     actual = 0, expected = 0 => PASS

Case 5: s = "0", expected = 0
  FindTheLongestBalancedSubstring      actual = 0, expected = 0 => PASS
  FindTheLongestBalancedSubstring2     actual = 0, expected = 0 => PASS
  FindTheLongestBalancedSubstring3     actual = 0, expected = 0 => PASS

Case 6: s = "0000", expected = 0
  FindTheLongestBalancedSubstring      actual = 0, expected = 0 => PASS
  FindTheLongestBalancedSubstring2     actual = 0, expected = 0 => PASS
  FindTheLongestBalancedSubstring3     actual = 0, expected = 0 => PASS

Case 7: s = "1111", expected = 0
  FindTheLongestBalancedSubstring      actual = 0, expected = 0 => PASS
  FindTheLongestBalancedSubstring2     actual = 0, expected = 0 => PASS
  FindTheLongestBalancedSubstring3     actual = 0, expected = 0 => PASS

Case 8: s = "01", expected = 2
  FindTheLongestBalancedSubstring      actual = 2, expected = 2 => PASS
  FindTheLongestBalancedSubstring2     actual = 2, expected = 2 => PASS
  FindTheLongestBalancedSubstring3     actual = 2, expected = 2 => PASS

Case 9: s = "000111", expected = 6
  FindTheLongestBalancedSubstring      actual = 6, expected = 6 => PASS
  FindTheLongestBalancedSubstring2     actual = 6, expected = 6 => PASS
  FindTheLongestBalancedSubstring3     actual = 6, expected = 6 => PASS

Case 10: s = "00110011", expected = 4
  FindTheLongestBalancedSubstring      actual = 4, expected = 4 => PASS
  FindTheLongestBalancedSubstring2     actual = 4, expected = 4 => PASS
  FindTheLongestBalancedSubstring3     actual = 4, expected = 4 => PASS

30/30 tests passed.
```

## 專案結構

```text
leetcode_2609/
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_2609/
    ├── leetcode_2609.csproj
    └── Program.cs
```
