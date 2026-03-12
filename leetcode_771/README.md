# 771. Jewels and Stones / 寶石與石頭

> LeetCode 第 771 題 — 判斷石頭中有多少顆是寶石

- [English Description (LeetCode)](https://leetcode.com/problems/jewels-and-stones/description/)
- [中文說明 (力扣)](https://leetcode.cn/problems/jewels-and-stones/description/)

## 題目描述

給定兩個字串：

- **`jewels`**：代表寶石的種類，每個字元代表一種寶石。
- **`stones`**：代表你擁有的石頭，每個字元代表一顆石頭。

請計算 `stones` 中有多少顆石頭同時也是寶石。

> [!NOTE]
> 字母**區分大小寫**，例如 `"a"` 與 `"A"` 被視為不同種類的石頭。

### 範例

| 輸入 | 輸出 | 說明 |
|------|------|------|
| `jewels = "aA"`, `stones = "aAAbbbb"` | `3` | `'a'`、`'A'`、`'A'` 共 3 顆寶石 |
| `jewels = "z"`, `stones = "ZZ"` | `0` | `'z'` ≠ `'Z'`，大小寫不同，0 顆寶石 |

### 限制條件

- `1 <= jewels.length, stones.length <= 50`
- `jewels` 和 `stones` 只包含英文字母
- `jewels` 中的所有字元都是**唯一**的

---

## 解題概念與出發點

這題的核心在於**集合查找**：對 `stones` 中的每個字元，判斷它是否屬於 `jewels` 集合。

關鍵思考方向：
1. **最直覺**：直接利用字串內建方法逐一檢查。
2. **拆解本質**：改用雙層迴圈手動比對，理解底層運作。
3. **優化查找**：將 `jewels` 放入 HashSet，使單次查找從 O(n) 降為 O(1)。

---

## 三種解法詳解

### 方法一：`string.Contains()` 方法

**思路**：遍歷 `stones` 的每個字元，呼叫 `jewels.Contains()` 判斷是否為寶石。

- **時間複雜度**：$O(m \times n)$ — `m` 為 `stones` 長度，`n` 為 `jewels` 長度
- **空間複雜度**：$O(1)$

```csharp
public int NumJewelsInStones(string jewels, string stones)
{
    int cnt = 0;
    for (int i = 0; i < stones.Length; i++)
    {
        if (jewels.Contains(stones[i]))
            cnt++;
    }
    return cnt;
}
```

> [!TIP]
> 這是最簡潔的寫法，善用 C# 內建方法，但底層仍為線性搜尋。

---

### 方法二：暴力法（雙層迴圈）

**思路**：外層遍歷每顆石頭，內層逐一比對每種寶石。找到匹配後 `break` 跳出內層迴圈。

- **時間複雜度**：$O(m \times n)$
- **空間複雜度**：$O(1)$

```csharp
public int NumJewelsInStones2(string jewels, string stones)
{
    int jewelsCount = 0;
    for (int i = 0; i < stones.Length; i++)
    {
        for (int j = 0; j < jewels.Length; j++)
        {
            if (stones[i] == jewels[j])
            {
                jewelsCount++;
                break;
            }
        }
    }
    return jewelsCount;
}
```

> [!NOTE]
> 與方法一本質相同，但將 `Contains` 的邏輯手動展開，更易理解底層運作。

---

### 方法三：HashSet 雜湊集合

**思路**：先將 `jewels` 中的所有字元存入 `HashSet<char>`，再遍歷 `stones`，透過 HashSet 進行 O(1) 查找。

- **時間複雜度**：$O(m + n)$ — 建立 HashSet 為 O(n)，查找為 O(m)
- **空間複雜度**：$O(n)$ — HashSet 儲存 `jewels` 的字元

```csharp
public int NumJewelsInStones3(string jewels, string stones)
{
    int jewelsCount = 0;
    HashSet<char> jewelsSet = new HashSet<char>();
    for (int i = 0; i < jewels.Length; i++)
        jewelsSet.Add(jewels[i]);

    for (int i = 0; i < stones.Length; i++)
    {
        if (jewelsSet.Contains(stones[i]))
            jewelsCount++;
    }
    return jewelsCount;
}
```

> [!TIP]
> 這是三種方法中效率最佳的，以空間換時間，將查找複雜度降至常數等級。

---

## 演示流程

以下用三個範例，逐步演示每種方法的執行過程。

### 範例 1：`jewels = "aA"`, `stones = "aAAbbbb"`

#### 方法一（Contains）

| stones[i] | jewels.Contains? | cnt |
|-----------|-----------------|-----|
| `'a'` | `"aA".Contains('a')` → **true** | 1 |
| `'A'` | `"aA".Contains('A')` → **true** | 2 |
| `'A'` | `"aA".Contains('A')` → **true** | 3 |
| `'b'` | `"aA".Contains('b')` → false | 3 |
| `'b'` | `"aA".Contains('b')` → false | 3 |
| `'b'` | `"aA".Contains('b')` → false | 3 |
| `'b'` | `"aA".Contains('b')` → false | 3 |

**結果：3**

#### 方法二（暴力法）

| stones[i] | 與 jewels 比對 | 匹配? | count |
|-----------|---------------|-------|-------|
| `'a'` | `'a'`==`'a'` → match, break | Yes | 1 |
| `'A'` | `'A'`==`'a'`? No → `'A'`==`'A'`? match, break | Yes | 2 |
| `'A'` | `'A'`==`'a'`? No → `'A'`==`'A'`? match, break | Yes | 3 |
| `'b'` | `'b'`==`'a'`? No → `'b'`==`'A'`? No | No | 3 |
| `'b'` ~ `'b'` | 同上 | No | 3 |

**結果：3**

#### 方法三（HashSet）

1. 建立 HashSet：`{ 'a', 'A' }`
2. 遍歷 stones：

| stones[i] | HashSet.Contains? | count |
|-----------|-------------------|-------|
| `'a'` | **true** | 1 |
| `'A'` | **true** | 2 |
| `'A'` | **true** | 3 |
| `'b'` | false | 3 |
| `'b'` | false | 3 |
| `'b'` | false | 3 |
| `'b'` | false | 3 |

**結果：3**

---

### 範例 2：`jewels = "z"`, `stones = "ZZ"`

| 方法 | 過程 | 結果 |
|------|------|------|
| Contains | `"z".Contains('Z')` → false × 2 | **0** |
| 暴力法 | `'Z'`==`'z'`? No × 2 | **0** |
| HashSet | Set=`{'z'}`，`'Z'` 不在集合中 × 2 | **0** |

大小寫不同，所以結果為 **0**。

---

### 範例 3：`jewels = "abc"`, `stones = "aabbccdd"`

#### 方法三（HashSet）演示

1. 建立 HashSet：`{ 'a', 'b', 'c' }`
2. 遍歷 stones：

| stones[i] | HashSet.Contains? | count |
|-----------|-------------------|-------|
| `'a'` | **true** | 1 |
| `'a'` | **true** | 2 |
| `'b'` | **true** | 3 |
| `'b'` | **true** | 4 |
| `'c'` | **true** | 5 |
| `'c'` | **true** | 6 |
| `'d'` | false | 6 |
| `'d'` | false | 6 |

**結果：6**

---

## 複雜度比較

| 方法 | 時間複雜度 | 空間複雜度 | 特點 |
|------|-----------|-----------|------|
| 方法一 Contains | $O(m \times n)$ | $O(1)$ | 程式碼最簡潔 |
| 方法二 暴力法 | $O(m \times n)$ | $O(1)$ | 手動展開，易理解原理 |
| 方法三 HashSet | $O(m + n)$ | $O(n)$ | 效率最佳，以空間換時間 |

## 技術棧

- C# / .NET 10
- Console Application
