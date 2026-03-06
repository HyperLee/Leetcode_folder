# LeetCode 1784 — Check if Binary String Has at Most One Segment of Ones

> LeetCode Daily Question · 2026-03-06

## 題目說明

**原題連結**
- [LeetCode (英文)](https://leetcode.com/problems/check-if-binary-string-has-at-most-one-segment-of-ones/description/?envType=daily-question&envId=2026-03-06)
- [LeetCode (中文)](https://leetcode.cn/problems/check-if-binary-string-has-at-most-one-segment-of-ones/description/?envType=daily-question&envId=2026-03-06)

**題意**

給定一個**不含前導零**的二進位字串 `s`，判斷其中由連續 `1` 所組成的段落是否**至多只有一段**。若是，回傳 `true`；否則回傳 `false`。

**限制條件**

| 項目 | 範圍 |
|------|------|
| 字串長度 | `1 ≤ s.length ≤ 100` |
| 字元集合 | 僅包含 `'0'` 和 `'1'` |
| 前導零 | 不存在（`s[0]` 必為 `'1'` 或字串全為 `'0'`） |

---

## 解題概念與出發點

### 核心問題

判斷連續 `1` 的「段數」是否 ≤ 1，等價於：

> 字串中是否存在「先有一段 1，中間出現 0，之後又有 1」的結構？

---

## 解法

### 方法一：迴圈遍歷（狀態機）

**出發點**

以兩個布林旗標模擬一台只有三個狀態的簡易狀態機：

```
[尚未見到 1] --遇到 1--> [在 1 段中] --遇到 0--> [已離開 1 段]
                                                       |
                                            再遇到 1 → 回傳 false
```

**實作**

```csharp
public bool CheckOnesSegment(string s)
{
    bool inSegment = false;   // 目前是否正在 1 段內
    bool leftSegment = false; // 是否已曾離開過一段 1

    foreach (char c in s)
    {
        if (c == '1')
        {
            if (leftSegment) return false; // 第二段出現
            inSegment = true;
        }
        else
        {
            if (inSegment) leftSegment = true; // 退出第一段
            inSegment = false;
        }
    }
    return true;
}
```

**複雜度**

| | 複雜度 |
|---|---|
| 時間 | O(n) |
| 空間 | O(1) |

---

### 方法二：尋找子字串 `"01"`（一行解）

**出發點**

觀察所有符合條件的字串形態：

| 情況 | 字串樣貌 | 含 `"01"` 子字串？ |
|------|---------|---------------|
| 全為 0 | `000…0` | 否 |
| 一段 1 後接 0 | `111…100…0` | 否 |
| 多段 1（不符合） | `1…10…01…1` | **是** |

由於題目保證無前導零，字串若以 `1` 開頭，任何「中斷後再出現 1」的情況必然會在字串中形成 `"01"` 子字串。因此**只要偵測到 `"01"`，即代表存在第二段以上的 1**。

**實作**

```csharp
public bool CheckOnesSegment2(string s)
{
    return !s.Contains("01");
}
```

**複雜度**

| | 複雜度 |
|---|---|
| 時間 | O(n) |
| 空間 | O(1) |

---

## 範例演示

### 範例 1：`s = "1110000"` → `true`

```
字元位置：  1  1  1  0  0  0  0
            ↑
         inSegment = true

走到第一個 '0'：
  inSegment=true → leftSegment=true, inSegment=false

後續皆為 '0'，leftSegment 維持 true 但未觸發第二段。
→ 回傳 true
```

### 範例 2：`s = "1000110"` → `false`

```
字元位置：  1  0  0  0  1  1  0
            ↑
         inSegment = true

走到 '0'：leftSegment = true, inSegment = false

走到第二個 '1'（index=4）：
  leftSegment == true → 立即回傳 false ✗
```

### 範例 3（方法二）：`s = "101"` → `false`

```
s = "101"
"101".Contains("01") → true
!true → false ✗
```

### 範例 4（方法二）：`s = "110"` → `true`

```
s = "110"
"110".Contains("01") → false
!false → true ✓
```

---

## 測試結果

執行 `dotnet run` 後的預期輸出：

```
=== LeetCode 1784 測試結果 ===
Input        Expected   Method1    Method2    Pass?
-------------------------------------------------------
1            True       True       True       PASS
110          True       True       True       PASS
1110000      True       True       True       PASS
1000110      False      False      False      PASS
10           True       True       True       PASS
11           True       True       True       PASS
101          False      False      False      PASS
```

---

## 環境需求

- [.NET 10](https://dotnet.microsoft.com/download/dotnet/10.0) SDK 以上

```bash
dotnet build
dotnet run --project leetcode_1784
```
