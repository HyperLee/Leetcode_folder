# LeetCode 273：整數轉換英文表示

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/language-C%23-239120)
![LeetCode](https://img.shields.io/badge/LeetCode-273%20Hard-FFA116)

這是一個以 .NET 10 主控台程式實作的教學範例。專案保留原有的高位分組遞迴解法，
並新增由低位分組、區塊內疊代的比較解法。`Main` 內建十筆固定案例，可直接對照
兩種解法的預期值、實際結果與通過狀態。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：高位分組與區塊遞迴](#解法一高位分組與區塊遞迴)
- [解法二：低位分組與區塊疊代](#解法二低位分組與區塊疊代)
- [兩種解法比較](#兩種解法比較)
- [可執行驗證案例](#可執行驗證案例)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定一個非負整數 `num`，將它轉換成英文數字表示。結果中的英文單字以一個空格
分隔，不使用逗號或連字號，也不能保留前導或結尾空白。

題目連結：

- [LeetCode 273 - Integer to English Words](https://leetcode.com/problems/integer-to-english-words/description/)
- [力扣 273 - 整數轉換英文表示](https://leetcode.cn/problems/integer-to-english-words/description/)

### 官方範例

```text
輸入：num = 123
輸出："One Hundred Twenty Three"

輸入：num = 12345
輸出："Twelve Thousand Three Hundred Forty Five"

輸入：num = 1234567
輸出："One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven"
```

## 限制條件

- `0 <= num <= 2^31 - 1`
- 最大輸入為 `2,147,483,647`，因此最高位階只需要處理 `Billion`。
- 輸入保證是非負整數；兩個公開解法不另外定義負數的轉換行為。
- `0` 是特殊案例，必須直接輸出 `Zero`。

## 解題概念與出發點

英文數字具有重複的三位數結構。將十進位數字像加入千分位逗號一樣，每三位切成
一個區塊後，每個區塊都只需要處理 `1` 到 `999`，再附加對應位階：

| 數值範圍 | 位階文字 | 範例 |
| ---: | --- | --- |
| `10^9` | `Billion` | `2,000,000,000` → `Two Billion` |
| `10^6` | `Million` | `3,000,000` → `Three Million` |
| `10^3` | `Thousand` | `4,000` → `Four Thousand` |
| `1` | 無 | `567` → `Five Hundred Sixty Seven` |

例如 `1,234,567` 可拆成：

```text
1 | 234 | 567
↓     ↓     ↓
One Million
Two Hundred Thirty Four Thousand
Five Hundred Sixty Seven
```

三位數區塊內則依英文命名規則分成四種情況：

- `1` 到 `9`：直接查 `singles`，例如 `7` → `Seven`。
- `10` 到 `19`：每個數字都有特殊名稱，查 `teens`。
- `20` 到 `99`：先查整十 `tens`，再處理個位數。
- `100` 到 `999`：先輸出個位數字加 `Hundred`，再處理後兩位。

`singles`、`teens`、`tens` 與 `thousands` 四張查表把不規則英文名稱集中管理，
演算法本身只需決定目前數值屬於哪個範圍。

## 解法一：高位分組與區塊遞迴

### 設計說明

`NumberToWords` 從 `1,000,000,000` 開始，由左到右檢查 Billion、Million、
Thousand 與個位四個三位數區塊：

1. 以 `num / unit` 取得目前區塊。
2. 區塊為 `0` 時直接略過，避免產生 `Zero Million` 或 `Zero Thousand`。
3. 非零區塊交給 `Recursion` 轉換百位、十位與個位。
4. 依目前索引附加 `Billion`、`Million`、`Thousand` 或空字串。
5. 從剩餘數值扣除已處理區塊，將 `unit` 除以 `1000` 後繼續。
6. 最後移除建構過程留下的結尾空白。

`Recursion` 每次都先輸出目前最高的有效部分，再把餘數交給下一層：

- 小於 `10`：查個位數。
- 小於 `20`：查特殊的 teen 名稱。
- 小於 `100`：輸出整十，再遞迴處理 `num % 10`。
- 其餘：輸出百位與 `Hundred`，再遞迴處理 `num % 100`。

每個區塊最多只有三位數，因此遞迴深度很小；高位到低位的處理順序也與最後英文
輸出的順序一致，可以持續附加到同一個 `StringBuilder`。

### 範例演示：`1,234,567`

| 位階 | `unit` | 目前區塊 | 遞迴產生文字 | 附加結果 |
| --- | ---: | ---: | --- | --- |
| Billion | `1,000,000,000` | `0` | 略過 | 空 |
| Million | `1,000,000` | `1` | `One` | `One Million` |
| Thousand | `1,000` | `234` | `Two Hundred Thirty Four` | `Two Hundred Thirty Four Thousand` |
| 個位區塊 | `1` | `567` | `Five Hundred Sixty Seven` | `Five Hundred Sixty Seven` |

其中 `Recursion(234)` 的流程為：

```text
234
├─ 百位：2 → Two Hundred
└─ 遞迴處理 34
   ├─ 十位：3 → Thirty
   └─ 遞迴處理 4 → Four
```

依序串接後得到：

```text
One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven
```

### 複雜度

令 `d` 為輸入的十進位位數：

- 時間複雜度：`O(d)`，每個三位數區塊與其中每個有效位數只處理固定次數。
- 額外空間：不計輸出字串時，遞迴深度對每個三位數區塊最多為常數；實作使用
  `StringBuilder` 保存長度為 `O(d)` 的輸出。

## 解法二：低位分組與區塊疊代

### 設計說明

`NumberToWords2` 從最低三位開始反覆取餘數，分組方向與解法一相反：

1. 使用 `num % 1000` 取得目前最低的三位數區塊。
2. 非零區塊交給 `ConvertChunkIterative`；零區塊完全略過。
3. 依 `scaleIndex` 附加空位階、`Thousand`、`Million` 或 `Billion`。
4. 將處理完成的區塊加入 `chunks`，再以 `num /= 1000` 移往下一組。
5. 因為加入順序是低位到高位，完成後反轉 `chunks`。
6. 使用單一空格連接所有區塊。

`ConvertChunkIterative` 不使用遞迴，而是用同一個剩餘值依序判斷：

1. `num >= 100` 時取出百位、加入 `Hundred`，再保留後兩位。
2. `num >= 20` 時取出整十，再保留個位。
3. 剩餘值為 `10` 到 `19` 時查 `teens`。
4. 剩餘值為 `1` 到 `9` 時查 `singles`。

這讓分組方向與區塊轉換方式都和解法一不同，適合比較遞迴與疊代的狀態管理。

### 範例演示：`1,000,010`

| 輪次 | 尚未處理數值 | `num % 1000` | 位階 | 動作 |
| ---: | ---: | ---: | --- | --- |
| 1 | `1,000,010` | `10` | 無 | 產生 `Ten`，加入清單 |
| 2 | `1,000` | `0` | Thousand | 略過整個零區塊 |
| 3 | `1` | `1` | Million | 產生 `One Million`，加入清單 |

加入清單時的順序是：

```text
[Ten, One Million]
```

反轉後成為：

```text
[One Million, Ten]
```

最後以空格連接，得到 `One Million Ten`。略過中間的零區塊是本題的重要邊界，
不能輸出 `Zero Thousand`。

### 複雜度

令 `d` 為輸入的十進位位數：

- 時間複雜度：`O(d)`，每輪移除三位數，且每個區塊只進行固定次數判斷。
- 額外空間：`O(d)`，低位到高位產生的區塊與區塊內單字會暫存在清單中；最後
  輸出字串本身同樣為 `O(d)`。

## 兩種解法比較

| 比較項目 | 解法一：高位分組＋遞迴 | 解法二：低位分組＋疊代 |
| --- | --- | --- |
| 公開 API | `NumberToWords(int num)` | `NumberToWords2(int num)` |
| 分組方向 | Billion 到個位，由高位往低位 | 個位到 Billion，由低位往高位 |
| 區塊取得方式 | 除以固定 `unit` | `% 1000` 後 `/= 1000` |
| 區塊內處理 | `Recursion` 遞迴處理餘數 | `ConvertChunkIterative` 依序判斷 |
| 零區塊 | 高位掃描時直接略過 | 取到餘數 `0` 時不加入清單 |
| 順序管理 | 產生順序即輸出順序 | 完成後必須反轉區塊清單 |
| 時間複雜度 | `O(d)` | `O(d)` |
| 額外狀態 | 輸出 builder 與固定深度遞迴 | 區塊及單字清單 |
| 適合觀察 | 位階與輸出順序直接對應 | 除法分組與顯式狀態管理 |

## 可執行驗證案例

`Main` 使用同一組預期值驗證兩個公開解法。每筆案例各產生兩項檢查，共十筆案例、
二十項驗證。

| 案例 | 輸入 | 預期結果 | 涵蓋重點 |
| ---: | ---: | --- | --- |
| 1 | `0` | `Zero` | 題目下界 |
| 2 | `7` | `Seven` | 個位數 |
| 3 | `13` | `Thirteen` | 10 到 19 |
| 4 | `20` | `Twenty` | 整十 |
| 5 | `100` | `One Hundred` | 整百且餘數為零 |
| 6 | `123` | `One Hundred Twenty Three` | 官方百位範例 |
| 7 | `13401` | `Thirteen Thousand Four Hundred One` | 原始專案範例 |
| 8 | `1000010` | `One Million Ten` | 中間三位區塊為零 |
| 9 | `1234567` | `One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven` | 官方多區塊範例 |
| 10 | `2147483647` | `Two Billion One Hundred Forty Seven Million Four Hundred Eighty Three Thousand Six Hundred Forty Seven` | 32 位元整數上界 |

## 建置與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

從本 repository 根目錄執行：

```bash
dotnet restore leetcode_0273/leetcode_0273.csproj
dotnet build leetcode_0273/leetcode_0273.csproj --no-restore --nologo
dotnet run --project leetcode_0273/leetcode_0273.csproj --no-build
```

目前沒有獨立的自動化測試專案；驗收方式是成功建置，再執行 `Main` 內固定的
Expected/Actual 比對案例。

## 實際執行結果

以下內容來自上述 `dotnet run` 命令：

```text
案例 1：題目下界
  輸入：0
  預期：Zero
  解法一（高位分組＋遞迴）：Zero => PASS
  解法二（低位分組＋疊代）：Zero => PASS

案例 2：個位數
  輸入：7
  預期：Seven
  解法一（高位分組＋遞迴）：Seven => PASS
  解法二（低位分組＋疊代）：Seven => PASS

案例 3：十到十九
  輸入：13
  預期：Thirteen
  解法一（高位分組＋遞迴）：Thirteen => PASS
  解法二（低位分組＋疊代）：Thirteen => PASS

案例 4：整十
  輸入：20
  預期：Twenty
  解法一（高位分組＋遞迴）：Twenty => PASS
  解法二（低位分組＋疊代）：Twenty => PASS

案例 5：整百
  輸入：100
  預期：One Hundred
  解法一（高位分組＋遞迴）：One Hundred => PASS
  解法二（低位分組＋疊代）：One Hundred => PASS

案例 6：官方百位範例
  輸入：123
  預期：One Hundred Twenty Three
  解法一（高位分組＋遞迴）：One Hundred Twenty Three => PASS
  解法二（低位分組＋疊代）：One Hundred Twenty Three => PASS

案例 7：原始專案範例
  輸入：13401
  預期：Thirteen Thousand Four Hundred One
  解法一（高位分組＋遞迴）：Thirteen Thousand Four Hundred One => PASS
  解法二（低位分組＋疊代）：Thirteen Thousand Four Hundred One => PASS

案例 8：中間三位區塊為零
  輸入：1000010
  預期：One Million Ten
  解法一（高位分組＋遞迴）：One Million Ten => PASS
  解法二（低位分組＋疊代）：One Million Ten => PASS

案例 9：官方多區塊範例
  輸入：1234567
  預期：One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven
  解法一（高位分組＋遞迴）：One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven => PASS
  解法二（低位分組＋疊代）：One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven => PASS

案例 10：32 位元整數上界
  輸入：2147483647
  預期：Two Billion One Hundred Forty Seven Million Four Hundred Eighty Three Thousand Six Hundred Forty Seven
  解法一（高位分組＋遞迴）：Two Billion One Hundred Forty Seven Million Four Hundred Eighty Three Thousand Six Hundred Forty Seven => PASS
  解法二（低位分組＋疊代）：Two Billion One Hundred Forty Seven Million Four Hundred Eighty Three Thousand Six Hundred Forty Seven => PASS

總結：20/20 項驗證通過
```

## 專案結構

```text
leetcode_0273/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_0273.sln
└── leetcode_0273/
    ├── leetcode_0273.csproj
    └── Program.cs
```

- `leetcode_0273/Program.cs`：兩種轉換解法、查表資料與可執行案例。
- `leetcode_0273/leetcode_0273.csproj`：目標框架為 `net10.0` 的主控台專案。
- `docs/readme-template.md`：README 初次建立的內容與驗證規範。
