# LeetCode 953 — 驗證外星語詞典 (Verifying an Alien Dictionary)

> 在外星語言中，他們也使用英文小寫字母，但字母的字典順序為某種排列。給定一系列外星語單字與字母順序，判斷這些單字是否依此外星字典順序遞增排列。

- 英文題目：[953. Verifying an Alien Dictionary](https://leetcode.com/problems/verifying-an-alien-dictionary/description/)
- 中文題目：[953. 驗證外星語詞典](https://leetcode.cn/problems/verifying-an-alien-dictionary/description/)
- 難度：Easy
- 主題標籤：Hash Table、String、Array

---

## 目錄

- [題目描述](#題目描述)
- [輸入輸出](#輸入輸出)
- [範例](#範例)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法詳解：直接遍歷](#解法詳解直接遍歷)
- [流程演示](#流程演示)
- [複雜度分析](#複雜度分析)
- [專案結構](#專案結構)
- [執行方式](#執行方式)

---

## 題目描述

在外星語言中，令人驚訝的是，他們也使用英文小寫字母，但可能以不同的順序排列。字母順序是 26 個小寫字母的某種排列。

給定一個用外星語言撰寫的單詞序列 `words`，以及字母的順序 `order`，**當且僅當** `words` 在此外星語言中按字典升序排序時，回傳 `true`，否則回傳 `false`。

## 輸入輸出

| 參數 | 型別 | 說明 |
| --- | --- | --- |
| `words` | `string[]` | 外星語單字陣列，元素皆由小寫英文字母組成 |
| `order` | `string` | 長度為 26 的字串，為 `a`~`z` 的某個排列，代表外星字母表 |
| 回傳值 | `bool` | `words` 是否符合 `order` 規範的字典升序排列 |

## 範例

### 範例 1

```text
輸入: words = ["hello","leetcode"], order = "hlabcdefgijkmnopqrstuvwxyz"
輸出: true
解釋: 在此 order 中，'h' 排第 0、'l' 排第 1，因此 "hello" < "leetcode"。
```

### 範例 2

```text
輸入: words = ["word","world","row"], order = "worldabcefghijkmnpqstuvxyz"
輸出: false
解釋: 'd' 在 order 中排第 4、'l' 在 order 中排第 3，
      所以 "word" 的第 4 個字元 'd' 大於 "world" 的第 4 個字元 'l'，
      "word" > "world"，違反升序。
```

### 範例 3

```text
輸入: words = ["apple","app"], order = "abcdefghijklmnopqrstuvwxyz"
輸出: false
解釋: "apple" 與 "app" 的前 3 個字元相同，但 "apple" 較長，
      依字典序 "apple" > "app"，違反升序。
      此情境屬於「較短字串應排在前」的特殊情況。
```

## 限制條件

- `1 <= words.length <= 100`
- `1 <= words[i].length <= 20`
- `order.length == 26`
- `words[i]` 與 `order` 皆只由小寫英文字母組成
- `order` 為 26 個字母的排列（不重複）

---

## 解題概念與出發點

字典序的本質是**逐位置比較字元**：

1. 從左到右逐字元比較兩個字串。
2. 第一個出現差異的位置即決定大小。
3. 若一字串為另一字串的前綴，則**較短者較小**。

題目唯一不同之處在於「字元的排序」並非 `a < b < c < ...`，而是由 `order` 重新定義。
因此核心觀察是：

> 只要建立一個對應表，把每個字元映射到它在 `order` 中的「排序位置」，
> 比較兩字元字典序的問題就退化為比較兩個整數大小。

這個對應表只需要長度 26 的陣列即可，查詢為 $O(1)$。

> [!TIP]
> 把「字元的字典序」轉成「整數排名」是處理自定義排序問題的常見技巧，
> 在比較器、排序、字串比對等情境都很實用。

---

## 解法詳解：直接遍歷

### 演算法步驟

#### 步驟 1：建立索引表 `index`

宣告 `int[] index = new int[26]`，遍歷 `order`，令 `index[order[i] - 'a'] = i`。
- `index[c - 'a']` 表示字元 `c` 在外星字母表中的「排名」（0 為最小，25 為最大）。
- 兩字元 $c_1, c_2$ 的字典序比較等同於比較 `index[c1 - 'a']` 與 `index[c2 - 'a']`。

#### 步驟 2：相鄰單字兩兩比較

對每個 $i \in [1, n-1]$，比較 `words[i - 1]` 與 `words[i]`：

1. 設定 `valid = false`，表示「尚未在共同長度內判定前者嚴格小於後者」。
2. 從 `j = 0` 開始，同時不超出兩字串長度，逐字元比較：
   - `prev = index[words[i - 1][j] - 'a']`
   - `curr = index[words[i][j] - 'a']`
   - 若 `prev < curr`：前者字典序較小，已符合升序，設 `valid = true` 並中斷此組比較。
   - 若 `prev > curr`：前者字典序較大，**違反升序**，立即回傳 `false`。
   - 若 `prev == curr`：繼續比較下一位置。

#### 步驟 3：處理前綴相同的特殊情況

若內層迴圈走完仍 `valid == false`，代表兩字串在共同長度內字元皆相等：

- 若 `words[i - 1].Length > words[i].Length`（如 `"apple"` vs `"app"`），
  依字典序定義較長者較大，違反升序，回傳 `false`。
- 否則（前者較短或等長）滿足升序，繼續檢查下一組。

#### 步驟 4：所有相鄰比較皆通過

回傳 `true`。

### 為什麼「相鄰兩兩比較」就足夠？

字典序具有**遞移性**：若 $w_1 \le w_2$ 且 $w_2 \le w_3$，則 $w_1 \le w_3$。
因此只要每一組相鄰單字都符合升序，整個陣列必為升序，無需做 $O(n^2)$ 兩兩比較。

### 為什麼用陣列而非 `Dictionary`？

字元集固定為 26 個小寫字母，使用長度 26 的陣列：
- 查詢成本 $O(1)$ 且常數極小。
- 無雜湊計算、無裝箱、無記憶體配置開銷。
- 程式碼意圖清晰。

> [!NOTE]
> 此即 LeetCode 官方解的「方法一：直接遍歷」，是時間複雜度最佳且實作最簡潔的解法。

### 核心程式碼

```csharp
public bool IsAlienSorted(string[] words, string order)
{
    int[] index = new int[26];
    for (int i = 0; i < order.Length; i++)
    {
        index[order[i] - 'a'] = i;
    }

    for (int i = 1; i < words.Length; i++)
    {
        bool valid = false;
        for (int j = 0; j < words[i - 1].Length && j < words[i].Length; j++)
        {
            int prev = index[words[i - 1][j] - 'a'];
            int curr = index[words[i][j] - 'a'];

            if (prev < curr) { valid = true; break; }
            else if (prev > curr) { return false; }
        }

        if (!valid && words[i - 1].Length > words[i].Length)
        {
            return false;
        }
    }

    return true;
}
```

---

## 流程演示

以 **範例 2** 為例：`words = ["word","world","row"]`，`order = "worldabcefghijkmnpqstuvxyz"`。

### 1. 建立 `index` 表（僅列出涉及的字元）

| 字元 | 在 `order` 中的位置 | `index` 值 |
| :-: | :-: | :-: |
| `w` | 0 | 0 |
| `o` | 1 | 1 |
| `r` | 2 | 2 |
| `l` | 3 | 3 |
| `d` | 4 | 4 |

### 2. 比較 `words[0] = "word"` 與 `words[1] = "world"`

| `j` | `words[0][j]` | `words[1][j]` | `prev` | `curr` | 判定 |
| :-: | :-: | :-: | :-: | :-: | :-- |
| 0 | `w` | `w` | 0 | 0 | 相等，繼續 |
| 1 | `o` | `o` | 1 | 1 | 相等，繼續 |
| 2 | `r` | `r` | 2 | 2 | 相等，繼續 |
| 3 | `d` | `l` | 4 | 3 | `prev > curr`，**回傳 `false`** |

> [!IMPORTANT]
> 注意在 `order` 中 `d` 的排名（4）大於 `l` 的排名（3），
> 因此 `"word"` 在外星字典序中**大於** `"world"`，違反升序。

最終答案：`false`。

---

### 額外演示：範例 3 的前綴情境

`words = ["apple","app"]`，`order` 為標準英文字母序。

| `j` | `words[0][j]` | `words[1][j]` | 判定 |
| :-: | :-: | :-: | :-- |
| 0 | `a` | `a` | 相等 |
| 1 | `p` | `p` | 相等 |
| 2 | `p` | `p` | 相等 |

共同長度走完，`valid` 仍為 `false`。
此時檢查長度：`"apple".Length (5) > "app".Length (3)` → **回傳 `false`**。

---

## 複雜度分析

設 $N$ 為 `words.Length`，$M$ 為單字平均長度。

- **時間複雜度**：$O(N \cdot M)$
  - 建立 `index`：$O(26)$。
  - 相鄰兩兩比較：每組最多比較 $M$ 個字元，共 $N - 1$ 組。
- **空間複雜度**：$O(1)$
  - 僅使用固定長度 26 的整數陣列。

---

## 專案結構

```text
leetcode_953/
├── leetcode_953.sln
├── README.md
└── leetcode_953/
    ├── leetcode_953.csproj
    └── Program.cs        # 解題實作與測試入口
```

## 執行方式

> [!NOTE]
> 需要安裝 [.NET 10 SDK](https://dotnet.microsoft.com/download)（依 `csproj` 設定的 TargetFramework）。

```bash
# 還原並建構
dotnet build leetcode_953/leetcode_953.csproj

# 執行（會跑 Program.Main 中的測試案例）
dotnet run --project leetcode_953/leetcode_953.csproj
```

預期輸出（節錄）：

```text
Test 1 expected True, got: True
Test 2 expected False, got: False
Test 3 expected False, got: False
Test 4 expected True, got: True
Test 5 expected True, got: True
```
