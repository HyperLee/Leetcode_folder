# LeetCode 1386 - Cinema Seat Allocation

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![LeetCode 1386](https://img.shields.io/badge/LeetCode-1386-F89F1B?logo=leetcode&logoColor=white)](https://leetcode.com/problems/cinema-seat-allocation/description/)

這是一個以 .NET 10 console project 實作的 LeetCode 1386 教學專案。程式保留兩種位元運算解法，並加入一種不使用位元運算的 `HashSet` 解法；`Main` 會以固定案例同時驗證三種解法。

- 題目：[Cinema Seat Allocation](https://leetcode.com/problems/cinema-seat-allocation/description/)
- 中文題目：[安排电影院座位](https://leetcode.cn/problems/cinema-seat-allocation/description/?envType=daily-question&amp;envId=2026-08-19)
- Target framework：`net10.0`

## 題目說明

電影院有 `n` 排座位，每排有 10 個座位，座位編號為 1 到 10。`reservedSeats[i] = [row_i, seat_i]` 表示該座位已經被預約。

每個四人團體必須坐在同一排的四個連續座位，而且只能使用以下三個座位區塊：

| 區塊 | 座位 | 特性 |
| --- | --- | --- |
| 左側區塊 | 2、3、4、5 | 與中間區塊重疊 |
| 中間區塊 | 4、5、6、7 | 同時與左右區塊重疊 |
| 右側區塊 | 6、7、8、9 | 與中間區塊重疊 |

區塊中的任何一個座位被預約，就不能使用該區塊；同一個座位也不能分配給兩個團體。目標是回傳整間電影院最多能安排的四人團體數量。

## 限制條件

限制條件依[官方題目規格](https://leetcode.com/problems/cinema-seat-allocation/description/)整理：

- `1 <= n <= 10^9`
- `1 <= reservedSeats.length <= min(10 * n, 10^4)`
- 每一筆資料的格式為 `reservedSeats[i] = [row_i, seat_i]`
- `1 <= row_i <= n`
- `1 <= seat_i <= 10`
- 所有 `reservedSeats[i]` 都不重複

這些限制條件的關鍵在於 `n` 可能非常大，但實際提供的預約資料最多只有 10,000 筆，因此不能建立包含所有排的陣列逐排掃描。

## 解題概念與出發點

一排最多只能安排兩個四人團體，而且只有座位 2 到 9 會影響三個候選區塊。座位 1 和 10 即使被預約，也不會阻擋任何團體。

因此三種解法都採取相同的高層策略：

1. 只記錄在座位 2 到 9 有預約的排。
2. 沒有被記錄的排，代表三個候選區塊都完整可用，直接增加 2 組。
3. 對被記錄的排判斷三個區塊：
   - 左側與右側區塊彼此不重疊，兩者都可用時增加 2 組。
   - 否則只要任一區塊可用，就增加 1 組。
   - 三個區塊都不可用時增加 0 組。

三個方法的差別只在於「如何保存一排的預約狀態」以及「如何表達區塊可用條件」。

## 解法一：補集遮罩的位元運算

API：`MaxNumberOfFamilies(int n, int[][] reservedSeats)`

### 資料表示

將座位 2 到 9 對應到 bit 0 到 bit 7：

```text
座位：  2 3 4 5 6 7 8 9
bit ：  0 1 2 3 4 5 6 7
```

每一排使用一個整數保存預約狀態。三個候選區塊使用「區塊以外的座位」作為補集遮罩：

```text
2、3、4、5 的補集：11110000
4、5、6、7 的補集：11000011
6、7、8、9 的補集：00001111
```

如果某排的 `occupiedMask` 與候選區塊的補集遮罩 OR 之後仍等於補集遮罩，表示預約 bit 全部落在區塊以外，該區塊就是空的。例如：

```text
(occupiedMask | outsideMask) == outsideMask
```

每個被影響的排只要三個條件至少一個成立，就能再安排 1 組；未出現在 dictionary 的排則在一開始直接計入 2 組。

### 範例演示

使用官方範例 1：

```text
n = 3
reservedSeats = [[1,2],[1,3],[1,8],[2,6],[3,1],[3,10]]
```

1. 第 1 排的有效預約座位是 2、3、8，bit 狀態為 `01000011`。
   - 左側區塊被座位 2、3 阻擋。
   - 中間區塊的補集檢查成立，因此 4、5、6、7 可用。
   - 右側區塊被座位 8 阻擋。
   - 第 1 排增加 1 組。
2. 第 2 排的有效預約座位是 6，bit 狀態為 `00010000`。
   - 左側區塊可用。
   - 中間與右側區塊都受到座位 6 影響。
   - 第 2 排增加 1 組。
3. 第 3 排只有座位 1 和 10 被預約，因此不會進入 dictionary。
   - 第 3 排視為完整可用，增加 2 組。
4. 總數為 `1 + 1 + 2 = 4`。

### 複雜度

令 `m = reservedSeats.Length`，`r` 為真正影響座位 2 到 9 的不同排數。建立 dictionary 與檢查每排都是期望 `O(m)` 時間，空間為 `O(r)`。

## 解法二：直接區塊遮罩的位元運算

API：`MaxNumberOfFamilies2(int n, int[][] reservedSeats)`

### 資料表示

同樣把座位 2 到 9 壓縮成 bit 0 到 bit 7，但這次直接使用三個候選區塊的遮罩：

```text
2、3、4、5：00001111
4、5、6、7：00111100
6、7、8、9：11110000
```

### 判斷流程

直接將預約狀態與區塊遮罩 AND：

```text
(occupiedMask & blockMask) == 0
```

結果為 0 代表該區塊的四個 bit 都沒有預約，因此區塊可用。其他計數方式與解法一相同，仍然只處理有相關預約的排。

### 範例演示

仍使用官方範例 1：

1. 第 1 排的狀態為 `01000011`。
   - 與左側遮罩 `00001111` AND 後不為 0，左側不可用。
   - 與中間遮罩 `00111100` AND 後為 0，中間可用。
   - 與右側遮罩 `11110000` AND 後不為 0，右側不可用。
   - 第 1 排增加 1 組。
2. 第 2 排的狀態為 `00010000`。
   - 與左側遮罩 AND 為 0，因此左側可用。
   - 中間與右側遮罩都包含座位 6 的 bit，因此不可用。
   - 第 2 排增加 1 組。
3. 第 3 排只預約座位 1、10，仍是未記錄排，增加 2 組。
4. 總數為 `4`。

### 複雜度

時間複雜度為期望 `O(m)`，空間複雜度為 `O(r)`。

## 解法三：每排 HashSet 與直接座位檢查

API：`MaxNumberOfFamilies3(int n, int[][] reservedSeats)`

### 資料表示

使用 `Dictionary<int, HashSet<int>>`：

- dictionary key 是排號。
- dictionary value 是該排實際被預約的座位號集合。
- 座位 1 和 10 不會放入集合，因為不會影響任何候選區塊。

這個版本不壓縮 bit，而是直接保留題目中的座位號，因此最容易與題目敘述逐項對照。

### 判斷流程

`IsBlockAvailable` 從區塊起點開始檢查連續四個座位：

```text
startSeat = 2 -> 檢查 2、3、4、5
startSeat = 4 -> 檢查 4、5、6、7
startSeat = 6 -> 檢查 6、7、8、9
```

對每一排得到 `canUseLeftBlock`、`canUseMiddleBlock` 與 `canUseRightBlock` 三個布林值，再依照左右區塊不重疊的規則計算 0、1 或 2 組。

### 範例演示

使用官方範例 1 時，dictionary 內容可理解為：

```text
第 1 排：{ 2, 3, 8 }
第 2 排：{ 6 }
第 3 排：未建立資料，因為只有 1、10 號座位被預約
```

1. 第 1 排：
   - `2..5` 包含已預約座位 2、3，不可用。
   - `4..7` 沒有預約座位，可用。
   - `6..9` 包含已預約座位 8，不可用。
   - 增加 1 組。
2. 第 2 排：
   - `2..5` 沒有座位 6，可用。
   - `4..7` 與 `6..9` 都包含座位 6，不可用。
   - 增加 1 組。
3. 第 3 排沒有記錄，增加 2 組。
4. 總數為 `1 + 1 + 2 = 4`。

### 複雜度

建立集合需要期望 `O(m)` 時間與 `O(m)` 空間。每個受影響排固定檢查 3 個區塊、每個區塊 4 個座位，因此檢查階段是 `O(r)`；總時間為 `O(m + r)`，在題目限制下可視為 `O(m)`。

## 三種解法比較

| 解法 | 狀態表示 | 區塊判斷 | 時間 | 空間 |
| --- | --- | --- | --- | --- |
| `MaxNumberOfFamilies` | 整數 bit mask | 預約狀態 OR 區塊補集 | `O(m)` | `O(r)` |
| `MaxNumberOfFamilies2` | 整數 bit mask | 預約狀態 AND 區塊遮罩 | `O(m)` | `O(r)` |
| `MaxNumberOfFamilies3` | 每排 `HashSet<int>` | 直接呼叫 `Contains` 檢查座位 | `O(m + r)` | `O(m)` |

兩種位元解法節省資料表示空間；HashSet 解法則以較直觀的題目座位號換取較容易閱讀與驗證的流程。三者都不建立 `n` 排的完整資料，因此能處理 `n = 10^9` 的輸入。

## 可執行驗證

專案沒有獨立測試專案，因此 `Main` 是可直接執行的 deterministic harness。每一組案例會呼叫三個 API，並以固定 Expected 值比較三個 Actual 值。

### 建置與執行

請在本 README 所在目錄執行：

```bash
dotnet restore leetcode_1386/leetcode_1386.csproj
dotnet build leetcode_1386/leetcode_1386.csproj
dotnet run --project leetcode_1386/leetcode_1386.csproj
```

建置完成後，也可以使用下列命令快速執行，不重新編譯：

```bash
dotnet run --project leetcode_1386/leetcode_1386.csproj --no-build
```

### 最新驗證輸出

以下內容直接來自完成程式修改後的 `dotnet run --project leetcode_1386/leetcode_1386.csproj --no-build`：

```text
[官方範例 1] Expected: 4 | Actual: M1=4, M2=4, M3=4 | PASS
[官方範例 2] Expected: 2 | Actual: M1=2, M2=2, M3=2 | PASS
[官方範例 3] Expected: 4 | Actual: M1=4, M2=4, M3=4 | PASS
[只預約第 1、10 號座位] Expected: 2 | Actual: M1=2, M2=2, M3=2 | PASS
[單一座位區塊受阻] Expected: 1 | Actual: M1=1, M2=1, M3=1 | PASS
[所有候選區塊受阻] Expected: 0 | Actual: M1=0, M2=0, M3=0 | PASS
[十億排的稀疏資料] Expected: 1999999999 | Actual: M1=1999999999, M2=1999999999, M3=1999999999 | PASS
總結：21/21 項驗證通過
```

若任一方法的實際結果與 Expected 不同，程式會輸出 `FAIL`，並以非零結束碼結束，方便在 CI 或重新導向輸出時辨識失敗。

### 其他驗證命令

嚴格 XML 文件建置與格式檢查可使用：

```bash
dotnet build leetcode_1386/leetcode_1386.csproj -p:GenerateDocumentationFile=true -p:TreatWarningsAsErrors=true
dotnet format leetcode_1386/leetcode_1386.csproj --verify-no-changes --no-restore
git diff --check
```

## 專案結構

```text
.
├── leetcode_1386/
│   ├── Program.cs
│   └── leetcode_1386.csproj
├── docs/
│   └── readme-template.md
├── AGENTS.md
└── README.md
```

`bin/` 與 `obj/` 是 .NET 建置產物，不屬於教學原始碼；演算法 API 本身只回傳結果，console 輸出集中在 `Main` 的驗證 harness。