# LeetCode 657 — Robot Return to Origin

[![Build](https://img.shields.io/badge/build-passing-brightgreen?style=flat-square)](leetcode_657/leetcode_657.csproj)
[![Language](https://img.shields.io/badge/language-C%23%2014-512BD4?style=flat-square&logo=dotnet)](leetcode_657/Program.cs)
[![Platform](https://img.shields.io/badge/.NET-10-512BD4?style=flat-square&logo=dotnet)](leetcode_657/leetcode_657.csproj)
[![Difficulty](https://img.shields.io/badge/difficulty-Easy-5cb85c?style=flat-square)](https://leetcode.com/problems/robot-return-to-origin/)

> 判斷機器人依照指令移動後，是否能回到起點。

---

## 題目說明

在一個二維平面上，有一個機器人從原點 `(0, 0)` 出發。  
給定一個由四種字元組成的指令字串 `moves`，每個字元代表一個移動方向：

| 字元 | 方向 | 座標變化 |
|------|------|----------|
| `U`  | 上   | y + 1    |
| `D`  | 下   | y − 1    |
| `L`  | 左   | x − 1    |
| `R`  | 右   | x + 1    |

機器人依序執行所有指令後，若最終位置回到原點 `(0, 0)`，則返回 `true`；否則返回 `false`。

**限制條件：**
- `1 <= moves.length <= 2 * 10⁴`
- `moves` 只包含字元 `'U'`、`'D'`、`'L'`、`'R'`

---

## 解題概念

### 方法：模擬法 (Simulation)

**出發點：**  
機器人最終能回到原點，代表水平方向的總位移與垂直方向的總位移都必須為 `0`。因此只需用兩個整數 `x`、`y` 追蹤目前座標，逐一執行每道指令，最後判斷是否同時歸零。

**關鍵觀察：**
- `U` 與 `D` 互為反向，若出現次數相同則垂直位移為 0。
- `L` 與 `R` 互為反向，若出現次數相同則水平位移為 0。
- 兩個條件同時成立 → 回到原點。

### 演算法步驟

```
1. 若 moves 為 null → 拋出 ArgumentNullException
2. 若 moves.Length 為奇數 → 直接回傳 false（兩軸必定無法同時抵銷）
3. 初始化 x = 0, y = 0
4. 遍歷 moves 中的每個字元：
   - 'U' → y + 1
   - 'D' → y - 1
   - 'L' → x - 1
   - 'R' → x + 1
5. 回傳 (x == 0 && y == 0)
```

### 複雜度分析

| 項目 | 複雜度 |
|------|--------|
| 時間 | O(n)，n 為指令字串長度 |
| 空間 | O(1)，只用固定數量的整數 |

> **補充：** 奇數長度早期返回為 O(1)，可在無需遍歷的情況下快速排除必為 `false` 的情形。

---

## 範例演示

### 範例 1：`"UD"` → `true`

```
初始位置：(0, 0)
執行 'U'  → (0,  1)
執行 'D'  → (0,  0)  ← 回到原點
結果：true
```

### 範例 2：`"LL"` → `false`

```
初始位置：(0, 0)
執行 'L'  → (-1, 0)
執行 'L'  → (-2, 0)  ← 未回到原點
結果：false
```

### 範例 3：`"UDLR"` → `true`

```
初始位置：(0, 0)
執行 'U'  → (0,  1)
執行 'D'  → (0,  0)
執行 'L'  → (-1, 0)
執行 'R'  → (0,  0)  ← 回到原點
結果：true
```

### 範例 4：`"LDRRUUDD"` → `false`

```
初始位置：(0, 0)
執行 'L'  → (-1,  0)
執行 'D'  → (-1, -1)
執行 'R'  → ( 0, -1)
執行 'R'  → ( 1, -1)
執行 'U'  → ( 1,  0)
執行 'U'  → ( 1,  1)
執行 'D'  → ( 1,  0)
執行 'D'  → ( 1, -1)  ← 未回到原點
結果：false
```

---

## 程式碼

```csharp
public bool JudgeCircle(string moves)
{
    ArgumentNullException.ThrowIfNull(moves);

    if (moves.Length % 2 != 0)
        return false;

    int x = 0;
    int y = 0;

    foreach (char move in moves)
    {
        (x, y) = move switch
        {
            'U' => (x,     y + 1),
            'D' => (x,     y - 1),
            'L' => (x - 1, y    ),
            'R' => (x + 1, y    ),
            _   => (x,     y    )
        };
    }

    return x == 0 && y == 0;
}
```

> [!NOTE]
> 此解法為單次線性掃描，不需要任何額外的資料結構，是此題最精簡且最高效的作法。奇數長度早期返回提供額外的 O(1) 優化，`ArgumentNullException.ThrowIfNull` 確保輸入安全性。

---

## 執行方式

```bash
dotnet run --project leetcode_657/leetcode_657.csproj
```

預期輸出：

```
Test 1 "UD"    : True
Test 2 "LL"    : False
Test 3 "UDLR" : True
Test 4 "LDRRUUDD": False
```

---

## 參考資料

- [LeetCode 657 — Robot Return to Origin](https://leetcode.com/problems/robot-return-to-origin/)
