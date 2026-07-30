# LeetCode 76：最小覆蓋子串

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)
![C#](https://img.shields.io/badge/C%23-console-239120)
![LeetCode Hard](https://img.shields.io/badge/LeetCode-Hard-ef4743)

這是一個使用 C# 與 .NET 10 撰寫的主控台教學專案，示範如何用滑動視窗解決
[LeetCode 76. Minimum Window Substring](https://leetcode.com/problems/minimum-window-substring/description/)。
專案保留兩種解法，並透過固定案例同時驗證兩者的輸出。

## 題目說明

給定兩個長度分別為 `m`、`n` 的字串 `s` 與 `t`，請在 `s` 中找出最短的連續子字串，
使它包含 `t` 的每一個字元。若 `t` 中某個字元重複出現，答案中也必須包含相同次數。

- 若不存在符合條件的子字串，回傳空字串 `""`。
- 測試資料保證最短答案唯一。
- 子字串必須連續，不能跳過中間字元。

例如：

```text
s = "ADOBECODEBANC"
t = "ABC"
```

`"ADOBEC"`、`"CODEBA"` 與 `"BANC"` 都涵蓋 `A`、`B`、`C`，但最短的是 `"BANC"`。

## 限制條件

- `m == s.Length`
- `n == t.Length`
- `1 <= m, n <= 10^5`
- `s` 與 `t` 僅由大小寫英文字母組成
- 延伸要求：嘗試設計時間複雜度為 `O(m + n)` 的演算法

目前兩種實作都使用長度為 `128` 的陣列記錄 ASCII 字元。這符合題目的英文字母輸入條件，
但不適用於任意 Unicode 字元。

## 解題概念與出發點

### 為什麼使用滑動視窗

最直接的暴力法會枚舉 `s` 的所有子字串，再逐一檢查是否涵蓋 `t`。
長度為 `m` 的字串共有 `O(m²)` 個子字串，若每次再統計字元，成本會更高。

這題具有適合滑動視窗的單調性：

1. 如果目前視窗尚未涵蓋 `t`，縮小視窗不可能讓缺少的字元出現，只能向右擴張。
2. 如果目前視窗已涵蓋 `t`，繼續擴張不會得到更短答案，應改為從左側收縮。
3. 收縮到剛好失效後，再繼續擴張右界，便能尋找下一個候選答案。

因此兩個指標各自只會由左向右移動，不必重新枚舉每個起點與終點。

### 視窗共同流程

```text
統計 t 的需求
    ↓
右界納入新字元
    ↓
目前視窗是否涵蓋 t？ ── 否 ──→ 繼續擴張右界
    │
    是
    ↓
更新最短答案
    ↓
移除左界字元並向右收縮
    ↓
視窗失效後，回到右界擴張
```

兩種解法的差別不在視窗移動方式，而在「如何快速判斷視窗是否有效」。

## 解法一：雙計數陣列 `MinWindow`

### 設計

這個版本使用兩個長度為 `128` 的陣列：

- `cntT[c]`：目標字串 `t` 需要多少個字元 `c`。
- `cntS[c]`：目前視窗中有多少個字元 `c`。

每當右界移入一個字元，就增加 `cntS`。接著呼叫 `isCovered` 掃描 ASCII 範圍：

```text
對每一個目標字元 c：
cntS[c] >= cntT[c]  → 數量足夠
cntS[c] <  cntT[c]  → 視窗仍然無效
```

只有 `cntT[c] > 0` 的字元會影響答案。當全部需求都滿足時，程式開始移動左界：

1. 若目前視窗比既有答案短，記錄左右邊界。
2. 從 `cntS` 扣除左界字元。
3. 左界右移一格。
4. 再次呼叫 `isCovered`；若仍有效，就繼續收縮。

### 關鍵不變量

- `cntS` 永遠代表閉區間 `[left, right]` 內的字元次數。
- 進入收縮迴圈時，`cntS[c] >= cntT[c]` 對所有必要字元都成立。
- 離開收縮迴圈時，至少有一個必要字元不足；下一步只能繼續擴張右界。
- `ansLeft` 與 `ansRight` 始終記錄目前看過的最短有效視窗。

### 範例演示

以 `s = "ADOBECODEBANC"`、`t = "ABC"` 為例：

| 階段 | 視窗 | 狀態 | 動作 |
|---|---|---|---|
| 右界到 `C` | `ADOBEC` | 首次包含 `A/B/C` | 記錄長度 6 |
| 移除左側 `A` | `DOBEC` | 缺少 `A` | 停止收縮 |
| 右界到第二個 `A` | `DOBECODEBA` | 再次有效 | 連續移除無關或多餘字元 |
| 左界移到 `O` 後 | `ODEBA` | 缺少 `C` | 停止收縮 |
| 右界到第二個 `C` | `ODEBANC` | 再次有效 | 逐步收縮 |
| 收縮到 `EBANC` | `EBANC` | 有效，長度 5 | 更新答案 |
| 收縮到 `BANC` | `BANC` | 有效，長度 4 | 更新答案 |
| 移除 `B` | `ANC` | 缺少 `B` | 最終答案為 `BANC` |

### 複雜度

- 建立目標計數：`O(n)`。
- 左右指標各最多移動 `m` 次，但每次有效性檢查最多掃描固定的 128 格：
  `O(n + 128m)`，因 128 是常數，可簡寫為 `O(m + n)`。
- 額外空間：兩個固定長度陣列，為 `O(128)`，也可視為 `O(1)`。

這個版本的優點是狀態直觀，適合第一次學習滑動視窗；代價是收縮時會重複掃描計數陣列。

## 解法二：差額陣列 `MinWindowOptimized`

### 設計

優化版本只使用一個 `map` 陣列與一個整數 `count`：

- `map[c]` 初始化為 `t` 還需要多少個字元 `c`。
- `count` 初始化為 `t.Length`，代表目前總共還缺多少個字元，重複字元會分別計算。

右界加入字元 `c` 時執行：

```csharp
if (map[c]-- > 0)
{
    count--;
}
```

`map[c]` 在遞減後的意義如下：

| 值 | 意義 |
|---|---|
| 正數 | 視窗仍缺少這個字元 |
| `0` | 視窗中的數量剛好滿足需求 |
| 負數 | 視窗中有多餘的這個字元，或它不是目標字元 |

判斷式比較的是遞減前的值。只有原值大於 `0`，新加入的字元才補到一個尚未滿足的需求，
此時 `count` 才減一。當 `count == 0`，視窗已涵蓋 `t`，不必再掃描 128 格陣列。

收縮左界時執行：

```csharp
if (map[s[start]]++ == 0)
{
    count++;
}
```

若遞增前為 `0`，表示該字元原本剛好足夠；移除後會變成 `1`，視窗重新缺少一個必要字元，
所以 `count` 增加。若遞增前為負數，只是移除多餘字元，視窗仍然有效。

### 關鍵不變量

- `count` 等於目前尚未被視窗滿足的字元總數。
- `count > 0` 時視窗無效，只能擴張右界。
- `count == 0` 時視窗有效，可以更新答案並收縮左界。
- 每次右移左界時，`map` 都會回補被移除字元的差額，讓下一輪判斷保持正確。

### 範例演示

同樣使用 `s = "ADOBECODEBANC"`、`t = "ABC"`：

| 操作 | 加入或移除字元 | `count` | 說明 |
|---|---:|---:|---|
| 初始化 | — | 3 | 尚缺 `A`、`B`、`C` |
| 右界加入 `A` | `A` | 2 | 補到必要字元 |
| 右界加入 `D/O` | `D/O` | 2 | 非必要字元，不影響需求 |
| 右界加入 `B` | `B` | 1 | 再補到一種必要字元 |
| 右界加入 `C` | `C` | 0 | 視窗有效，記錄 `ADOBEC` |
| 左界移除 `A` | `A` | 1 | `A` 重新不足，停止收縮 |
| 右界加入第二個 `A` | `A` | 0 | 視窗再次有效 |
| 收縮至移除 `C` | `C` | 1 | 視窗失效 |
| 右界加入第二個 `C` | `C` | 0 | 視窗有效 |
| 連續收縮 | `O/D/E` | 0 | 移除非必要或多餘字元 |
| 視窗成為 `BANC` | — | 0 | 更新為長度 4 |
| 左界移除 `B` | `B` | 1 | 視窗失效，答案保持 `BANC` |

### 複雜度

- 建立需求差額：`O(n)`。
- 左右指標都只向右移動，每個字元最多被加入、移除一次：`O(m)`。
- 總時間複雜度：`O(m + n)`。
- 額外空間：一個固定長度陣列，為 `O(128)`，也可視為 `O(1)`。

這個版本避免重複掃描整個字元表，常數成本較低，但理解 `map` 的正負值與後置遞增／遞減
需要更仔細地追蹤狀態。

## 兩種解法比較

| 項目 | `MinWindow` | `MinWindowOptimized` |
|---|---|---|
| 視窗策略 | 右界擴張、左界收縮 | 右界擴張、左界收縮 |
| 字元狀態 | `cntS` 與 `cntT` 兩個陣列 | `map` 一個差額陣列 |
| 有效性判斷 | 呼叫 `isCovered` 掃描 128 格 | 檢查 `count == 0` |
| 時間複雜度 | `O(n + 128m)`，簡寫 `O(m + n)` | `O(m + n)` |
| 額外空間 | `O(128)` | `O(128)` |
| 教學特性 | 狀態直接、容易觀察 | 不變量較精簡、常數成本較低 |

## 固定驗證案例

`Main` 會把同一組案例交給兩個方法，共執行 12 次比較：

| 案例 | `s` | `t` | 期望結果 | 驗證重點 |
|---|---|---|---|---|
| 官方一般案例 | `ADOBECODEBANC` | `ABC` | `BANC` | 多次擴張與收縮 |
| 單字元 | `a` | `a` | `a` | 最小合法輸入 |
| 目標字串較長 | `a` | `aa` | `<empty>` | 重複需求無法滿足 |
| 重複需求字元 | `ADOBECODEBANCBA` | `AABC` | `ANCBA` | 同一字元必須出現兩次 |
| 整段即答案 | `ABC` | `ABC` | `ABC` | 不可再縮短 |
| 答案位於尾端 | `bba` | `ab` | `ba` | 移除左側多餘字元 |

任一結果與期望值不同時，該列會輸出 `FAIL`，程式也會設定非零結束碼。

## 建置與執行

### 環境需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

從此 README 所在的專案根目錄執行：

```powershell
dotnet restore leetcode_076/leetcode_076.csproj
dotnet build leetcode_076/leetcode_076.csproj --nologo
dotnet run --project leetcode_076/leetcode_076.csproj
```

專案目前沒有獨立的自動化測試專案；`Main` 的固定案例 runner 是行為驗收入口。

### 實際執行輸出

```text
===== MinWindow =====
[PASS] 官方一般案例: s="ADOBECODEBANC", t="ABC", expected="BANC", actual="BANC"
[PASS] 單字元: s="a", t="a", expected="a", actual="a"
[PASS] 目標字串較長: s="a", t="aa", expected="<empty>", actual="<empty>"
[PASS] 重複需求字元: s="ADOBECODEBANCBA", t="AABC", expected="ANCBA", actual="ANCBA"
[PASS] 整段即答案: s="ABC", t="ABC", expected="ABC", actual="ABC"
[PASS] 答案位於尾端: s="bba", t="ab", expected="ba", actual="ba"
小計: 6/6 通過

===== MinWindowOptimized =====
[PASS] 官方一般案例: s="ADOBECODEBANC", t="ABC", expected="BANC", actual="BANC"
[PASS] 單字元: s="a", t="a", expected="a", actual="a"
[PASS] 目標字串較長: s="a", t="aa", expected="<empty>", actual="<empty>"
[PASS] 重複需求字元: s="ADOBECODEBANCBA", t="AABC", expected="ANCBA", actual="ANCBA"
[PASS] 整段即答案: s="ABC", t="ABC", expected="ABC", actual="ABC"
[PASS] 答案位於尾端: s="bba", t="ab", expected="ba", actual="ba"
小計: 6/6 通過

總計: 12/12 通過
```

## 專案結構

```text
leetcode_076/
├─ leetcode_076.sln
├─ README.md
├─ docs/
│  └─ readme-template.md
├─ .vscode/
│  ├─ launch.json
│  └─ tasks.json
└─ leetcode_076/
   ├─ leetcode_076.csproj
   └─ Program.cs
```
