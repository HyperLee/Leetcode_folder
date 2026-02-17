# LeetCode 401 — Binary Watch（二進位手錶）

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14-239120?logo=csharp)](https://learn.microsoft.com/dotnet/csharp/)
[![LeetCode](https://img.shields.io/badge/LeetCode-401-FFA116?logo=leetcode)](https://leetcode.com/problems/binary-watch/)

> 使用 C# 實作三種不同解法來解決 LeetCode 第 401 題「二進位手錶」。

## 題目說明

一個二進位手錶上方有 **4 顆 LED** 表示小時（0–11），下方有 **6 顆 LED** 表示分鐘（0–59）。每顆 LED 代表一個位元（0 或 1），最低位元在最右邊。

```
  ┌───────────────────────┐
  │  8   4   2   1        │  ← 小時（4 bits）
  │  ●   ○   ○   ●        │  → 8 + 1 = 9
  │                       │
  │ 32  16   8   4   2   1│  ← 分鐘（6 bits）
  │  ○   ○   ●   ○   ●   ○│  → 8 + 2 = 10
  └───────────────────────┘
        顯示 → 9:10
  ● = LED 亮（1）  ○ = LED 滅（0）
```

給定整數 `turnedOn`，代表目前亮起的 LED 數量，回傳手錶能顯示的**所有合法時間**。

### 限制條件

- 小時不得有前導零（`"01:00"` ✗ → `"1:00"` ✓）
- 分鐘必須為兩位數（`"10:2"` ✗ → `"10:02"` ✓）
- `0 ≤ turnedOn ≤ 10`

### 範例

| 輸入 | 輸出 |
|------|------|
| `turnedOn = 1` | `["0:01","0:02","0:04","0:08","0:16","0:32","1:00","2:00","4:00","8:00"]` |
| `turnedOn = 0` | `["0:00"]` |
| `turnedOn = 9` | `[]`（不存在合法時間） |

---

## 三種解題概念與出發點

### 概念一：枚舉時分（Enumerate Hours & Minutes）

**出發點**：小時只有 12 種可能、分鐘只有 60 種可能，總共 720 種組合。直接窮舉所有時間組合，檢查「亮燈數 = turnedOn」即可。

- 不需要理解位元操作的組合意義
- 程式碼最直觀、最容易理解
- 時間複雜度：$O(12 \times 60) = O(720)$

### 概念二：二進位枚舉（Binary Enumeration）

**出發點**：將 10 顆 LED 視為一個 10-bit 的二進位數（高 4 位 = 小時，低 6 位 = 分鐘）。枚舉 $2^{10} = 1024$ 種開關組合，篩選合法者。

- 用一個整數同時表達時與分，更加精簡
- 位元運算取出時/分，效率更高
- 時間複雜度：$O(1024)$

### 概念三：回溯剪枝（Backtracking with Pruning）

**出發點**：從 10 顆 LED 中「選 k 顆點亮」本質上是組合問題 $C(10, k)$。用回溯法遞迴選取，並在選取過程中即時剪枝（小時 ≥ 12 或分鐘 ≥ 60 就回溯）。

- 與組合搜尋、排列搜尋等經典回溯題型直接對應
- 剪枝可以大幅減少無效搜尋
- 時間複雜度：$O(C(10, k))$，最多 $C(10,5) = 252$

---

## 核心工具函式：BitCount（Population Count）

三種解法中，解法一與解法二都依賴 `BitCount` 函式來計算一個整數的二進位表示中有多少個 `1`。這個運算在電腦科學中稱為 **Population Count**（popcount）或 **Hamming Weight**。

### 為什麼需要 BitCount？

每顆 LED 亮起對應二進位中的一個 `1`。要判斷某個小時/分鐘組合是否恰好用了 `turnedOn` 顆 LED，就需要快速計算二進位中 1 的個數。

### 演算法原理：分治位元合併

此實作不使用迴圈，而是透過 **5 步位元操作** 以 $O(1)$ 時間完成計算。核心思想是將 32 位元的整數分成越來越大的區塊，逐步合併相鄰區塊的 1 的個數。

```csharp
private static int BitCount(int i)
{
    i = i - ((i >> 1) & 0x55555555);        // 步驟 1
    i = (i & 0x33333333) + ((i >> 2) & 0x33333333); // 步驟 2
    i = (i + (i >> 4)) & 0x0f0f0f0f;       // 步驟 3
    i = i + (i >> 8);                        // 步驟 4
    i = i + (i >> 16);                       // 步驟 5
    return i & 0x3f;                         // 取最低 6 位元
}
```

### 逐步拆解（以 `i = 11` 即二進位 `1011` 為例）

**步驟 1 — 每 2 位元一組，計算每組中 1 的個數**

```
遮罩 0x55555555 = ...01 01 01 01（交替的 01）

原始：    ...00 00 10 11
i >> 1：  ...00 00 01 01
& 遮罩：  ...00 00 01 01
相減：    ...00 00 01 10
                    ↑  ↑
           第2組有1個1 第1組有2個1
```

> [!NOTE]
> 這一步利用了巧妙的數學性質：對於 2 位元數 `ab`，其 1 的個數 = `ab - (ab >> 1)`。例如 `11`(3) - `01`(1) = `10`(2)，表示有 2 個 1。

**步驟 2 — 每 4 位元一組，合併相鄰 2 位元組的結果**

```
遮罩 0x33333333 = ...0011 0011（每 4 位取低 2 位）

目前：        ...0000 0110
& 0x33：      ...0000 0010    （低 2 位元組）
>> 2 & 0x33：  ...0000 0001    （高 2 位元組）
相加：        ...0000 0011 → 表示低 4 位元共有 3 個 1
```

**步驟 3 — 每 8 位元一組，合併相鄰 4 位元組的結果**

```
遮罩 0x0f0f0f0f = ...00001111（每 8 位取低 4 位）

(i + (i >> 4)) & 0x0f：每個位元組（byte）內的 1 的個數
```

**步驟 4 & 5 — 逐步擴展到 16 位元、32 位元**

```
i + (i >> 8)：合併相鄰位元組
i + (i >> 16)：合併為最終 32 位元的總計
& 0x3f：取最低 6 位元（最大值 32，只需要 6 位元表示）
```

### 完整演示：`BitCount(11)`

| 步驟 | 操作 | 結果（二進位） | 說明 |
|------|------|---------------|------|
| 輸入 | — | `0000...1011` | 十進位 11 |
| 步驟 1 | 每 2 位元計數 | `0000...0110` | 各組：`01`,`10` = 1個, 2個 |
| 步驟 2 | 每 4 位元合併 | `0000...0011` | 低 4 位元共 3 個 1 |
| 步驟 3 | 每 8 位元合併 | `0000...0011` | 低 8 位元共 3 個 1 |
| 步驟 4 | 每 16 位元合併 | `0000...0011` | 同上 |
| 步驟 5 | 32 位元合併 | `0000...0011` | 同上 |
| 回傳 | `& 0x3f` | **3** | `11` 的二進位有 3 個 1 ✓ |

### 替代方案

在 .NET 中也可以使用內建方法達到相同效果：

```csharp
// 使用 .NET 內建的 PopCount（底層可能使用 CPU 的 POPCNT 指令）
int count = int.PopCount(11); // 回傳 3
```

> [!TIP]
> 本專案保留手動實作是為了展示位元操作的演算法原理。在生產環境中建議使用 `int.PopCount()` 以獲得最佳效能與可讀性。

---

## 解法詳細說明

### 解法一：枚舉時分

```csharp
public IList<string> ReadBinaryWatch(int turnedOn)
{
    IList<string> ans = new List<string>();
    for (int h = 0; h < 12; ++h)
    {
        for (int m = 0; m < 60; ++m)
        {
            if (BitCount(h) + BitCount(m) == turnedOn)
            {
                ans.Add(h + ":" + (m < 10 ? "0" : "") + m);
            }
        }
    }
    return ans;
}
```

**關鍵步驟：**

1. 外層迴圈枚舉小時 `h`（0 ~ 11），內層迴圈枚舉分鐘 `m`（0 ~ 59）
2. `BitCount(h)` 計算小時的二進位 1 的個數、`BitCount(m)` 計算分鐘的二進位 1 的個數
3. 兩者之和等於 `turnedOn` 則為合法時間，加入結果

> [!NOTE]
> 此方法的範圍已天然限制在合法時間內，無需額外邊界檢查。

---

### 解法二：二進位枚舉

```csharp
public IList<string> ReadBinaryWatch2(int turnedOn)
{
    IList<string> ans = new List<string>();
    for (int i = 0; i < 1024; ++i)
    {
        int h = i >> 6;       // 取高 4 位元 → 小時
        int m = i & 63;       // 取低 6 位元 → 分鐘
        if (h < 12 && m < 60 && BitCount(i) == turnedOn)
        {
            ans.Add(h + ":" + (m < 10 ? "0" : "") + m);
        }
    }
    return ans;
}
```

**關鍵步驟：**

1. 用一個 10-bit 整數 `i` 表示 10 顆 LED 的狀態
2. `i >> 6`（右移 6 位）取出高 4 位，即小時
3. `i & 63`（AND 0b111111）取出低 6 位，即分鐘
4. 檢查 `h < 12`、`m < 60` 以及 `BitCount(i) == turnedOn`

> [!NOTE]
> 與解法一相比，此方法只需要一層迴圈且只呼叫一次 `BitCount`。

---

### 解法三：回溯剪枝

```csharp
public IList<string> ReadBinaryWatch3(int turnedOn)
{
    IList<string> ans = new List<string>();
    int[] hourValues   = [1, 2, 4, 8, 0, 0, 0, 0, 0, 0];
    int[] minuteValues = [0, 0, 0, 0, 1, 2, 4, 8, 16, 32];
    Backtrack(ans, hourValues, minuteValues, turnedOn, 0, 0, 0);
    return ans;
}

private void Backtrack(IList<string> ans, int[] hourValues, int[] minuteValues,
    int remaining, int start, int hour, int minute)
{
    if (hour > 11 || minute > 59) return;       // 剪枝
    if (remaining == 0)                          // 終止條件
    {
        ans.Add(hour + ":" + (minute < 10 ? "0" : "") + minute);
        return;
    }
    for (int i = start; i < 10; ++i)
    {
        if (10 - i < remaining) break;           // 剪枝：剩餘不足
        Backtrack(ans, hourValues, minuteValues,
            remaining - 1, i + 1,
            hour + hourValues[i], minute + minuteValues[i]);
    }
}
```

**關鍵步驟：**

1. 定義每顆 LED 對小時/分鐘的貢獻值陣列
2. 遞迴從 `start` 開始逐一嘗試選取 LED
3. **剪枝 1**：若 `hour > 11` 或 `minute > 59`，提前回溯
4. **剪枝 2**：若剩餘可選 LED 不足以填滿 `remaining`，直接 `break`
5. `remaining == 0` 時記錄結果

> [!TIP]
> 回溯法的通用架構（選擇 → 遞迴 → 撤銷）在此處由參數傳遞自動完成撤銷，無需手動還原狀態。

---

## 演示流程（以 `turnedOn = 1` 為例）

### 解法一演示：枚舉時分

```
h=0 : m=0  → BitCount(0)+BitCount(0) = 0+0 = 0 ≠ 1 → 跳過
h=0 : m=1  → BitCount(0)+BitCount(1) = 0+1 = 1 = 1 → 加入 "0:01" ✓
h=0 : m=2  → BitCount(0)+BitCount(2) = 0+1 = 1 = 1 → 加入 "0:02" ✓
h=0 : m=3  → BitCount(0)+BitCount(3) = 0+2 = 2 ≠ 1 → 跳過
...
h=0 : m=32 → BitCount(0)+BitCount(32)= 0+1 = 1 = 1 → 加入 "0:32" ✓
...
h=1 : m=0  → BitCount(1)+BitCount(0) = 1+0 = 1 = 1 → 加入 "1:00" ✓
h=2 : m=0  → BitCount(2)+BitCount(0) = 1+0 = 1 = 1 → 加入 "2:00" ✓
h=4 : m=0  → BitCount(4)+BitCount(0) = 1+0 = 1 = 1 → 加入 "4:00" ✓
h=8 : m=0  → BitCount(8)+BitCount(0) = 1+0 = 1 = 1 → 加入 "8:00" ✓
```

結果：`["0:01", "0:02", "0:04", "0:08", "0:16", "0:32", "1:00", "2:00", "4:00", "8:00"]`

### 解法二演示：二進位枚舉

```
i=0    (0000 000000) → h=0,  m=0  → BitCount=0  ≠ 1 → 跳過
i=1    (0000 000001) → h=0,  m=1  → BitCount=1  = 1 → 加入 "0:01" ✓
i=2    (0000 000010) → h=0,  m=2  → BitCount=1  = 1 → 加入 "0:02" ✓
i=3    (0000 000011) → h=0,  m=3  → BitCount=2  ≠ 1 → 跳過
i=4    (0000 000100) → h=0,  m=4  → BitCount=1  = 1 → 加入 "0:04" ✓
i=8    (0000 001000) → h=0,  m=8  → BitCount=1  = 1 → 加入 "0:08" ✓
i=16   (0000 010000) → h=0,  m=16 → BitCount=1  = 1 → 加入 "0:16" ✓
i=32   (0000 100000) → h=0,  m=32 → BitCount=1  = 1 → 加入 "0:32" ✓
i=64   (0001 000000) → h=1,  m=0  → BitCount=1  = 1 → 加入 "1:00" ✓
i=128  (0010 000000) → h=2,  m=0  → BitCount=1  = 1 → 加入 "2:00" ✓
i=256  (0100 000000) → h=4,  m=0  → BitCount=1  = 1 → 加入 "4:00" ✓
i=512  (1000 000000) → h=8,  m=0  → BitCount=1  = 1 → 加入 "8:00" ✓
```

結果：`["0:01", "0:02", "0:04", "0:08", "0:16", "0:32", "1:00", "2:00", "4:00", "8:00"]`

### 解法三演示：回溯剪枝

```
LED 編號：         0    1    2    3    4    5    6    7    8    9
小時貢獻值：       1    2    4    8    0    0    0    0    0    0
分鐘貢獻值：       0    0    0    0    1    2    4    8   16   32

Backtrack(remaining=1, start=0, h=0, m=0)
├─ 選 LED 0 → h=1, m=0
│  └─ remaining=0 → 加入 "1:00" ✓
├─ 選 LED 1 → h=2, m=0
│  └─ remaining=0 → 加入 "2:00" ✓
├─ 選 LED 2 → h=4, m=0
│  └─ remaining=0 → 加入 "4:00" ✓
├─ 選 LED 3 → h=8, m=0
│  └─ remaining=0 → 加入 "8:00" ✓
├─ 選 LED 4 → h=0, m=1
│  └─ remaining=0 → 加入 "0:01" ✓
├─ 選 LED 5 → h=0, m=2
│  └─ remaining=0 → 加入 "0:02" ✓
├─ 選 LED 6 → h=0, m=4
│  └─ remaining=0 → 加入 "0:04" ✓
├─ 選 LED 7 → h=0, m=8
│  └─ remaining=0 → 加入 "0:08" ✓
├─ 選 LED 8 → h=0, m=16
│  └─ remaining=0 → 加入 "0:16" ✓
└─ 選 LED 9 → h=0, m=32
   └─ remaining=0 → 加入 "0:32" ✓
```

結果：`["1:00", "2:00", "4:00", "8:00", "0:01", "0:02", "0:04", "0:08", "0:16", "0:32"]`

---

## 快速開始

### 環境需求

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) 或以上版本

### 執行

```bash
dotnet run --project leetcode_401/leetcode_401.csproj
```

### 建構

```bash
dotnet build leetcode_401/leetcode_401.csproj
```
