# LeetCode 2327 — 知道秘密的人數（中文說明）

本專案包含 LeetCode 題目 2327 的 C# 單檔解法，並提供詳盡的演算法說明與分析（包含 O(n) 前綴和版本與 O(forget) 空間最佳化版本）。

## 題目說明（中文）

第 1 天，有一個人發現了一個秘密。

給定整數 `delay`，表示每個人在發現秘密 `delay` 天後開始，每天會把秘密分享給一個新的人。給定整數 `forget`，表示每個人在發現秘密 `forget` 天後就會忘記秘密。人在忘記當天以及之後的任何一天都不能分享秘密。

給定整數 `n`，請返回在第 `n` 天結束時還知道秘密的人數。由於答案可能很大，請對 `10^9 + 7` 取模後返回。

## 專案內容

- `leetcode_2327/Program.cs` — 單一 C# 檔案示範實作，包含 `Main` 範例輸出。

## 直觀想法

令 `keep[day]` 為第 `day` 天新知道秘密的人數。

- 若某人於第 `t` 天知道秘密，則：
  - 他會在第 `t + delay` 天開始分享。
  - 他會在第 `t + forget` 天忘記秘密（該天不能分享）。

因此，在某天 `d` 能分享的人，是滿足：

- `t + delay <= d`（已達分享年齡）且
- `t + forget > d`（尚未忘記）

等價於 `t` 屬於區間 `[d - forget + 1, d - delay]`。

因此：

keep[d] = sum_{t = max(1, d - forget + 1)}^{d - delay} keep[t]

最後，在第 `n` 天仍然記得秘密的人數，為 `keep[t]` 在 `t` 屬於 `[max(1, n - forget + 1), n]` 的總和。

## 解法 A — O(n) 時間, O(n) 空間（前綴和）

- 維護 `keep[1..n]` 與 `prefix[0..n]`，其中 `prefix[i] = sum_{1..i} keep[i] (mod M)`。
- 對每個 `d`，使用前綴和在 O(1) 計算區間 `[max(1, d - forget + 1), d - delay]` 的和。
- 複雜度：
  - 時間：O(n)
  - 空間：O(n)

此做法直觀但使用 O(n) 空間；注意實際只需保留最近 `forget` 天的資訊，因此可優化空間。

## 解法 B — O(n) 時間, O(forget) 空間（環形緩衝 + 滑動和）

為了把空間降到 O(forget)，使用長度為 `forget` 的環形緩衝 `buf[0..forget-1]`，其中 `buf[i]` 儲存對應那天新知道的人數（以模盤索引）。同時維護兩個滑動總和：

- `totalRemembering`：目前仍記得秘密的總人數（buffer 值的總和）。
- `shareableSum`：當天可分享的總人數（已達 `delay` 但尚未 `forget`）。

演算法概要：

- 初始化 `buf[0] = 1`（第 1 天 1 人），`totalRemembering = 1`，`shareableSum = 0`。
- 對於 `day = 2 .. n`：
  - `idx = (day - 1) % forget`：要覆寫的 slot（代表 `day - forget` 那天的人）。
  - 先從 `totalRemembering` 中移除 `buf[idx]`（這些人在今天忘記）。
  - 若 `day - delay >= 1`，把 `buf[(day - delay - 1) % forget]` 加入 `shareableSum`（那些人今天剛好可以開始分享）。
  - 若 `day - forget >= 1`，把 `buf[(day - forget - 1) % forget]` 從 `shareableSum` 中移除（那些人在今天忘記，若之前已算入 `shareableSum`，要移除）。
  - `newLearners = shareableSum (mod M)`。
  - `buf[idx] = newLearners`，並把它加回 `totalRemembering`。

複雜度：
- 時間：O(n)
- 空間：O(forget)

這也是目前 `Program.cs` 使用的實作。

## 正確性與邊界情況

- 若 `delay >= forget`，則沒有人會成功分享（分享期晚於或等於忘記時刻）。程式應能正確處理此情況。
- 處理小的 `n`（例如 `n = 1`）應直接回傳 1。
- 所有運算皆模 `10^9 + 7` 以避免溢位。

## 如何執行

在專案根目錄（PowerShell）：

```powershell
dotnet run --project "d:\Leetcode_folder\Leetcode_folder\leetcode_2327\leetcode_2327.csproj"
```

或執行已編譯的 DLL / exe：

```powershell
dotnet "d:\Leetcode_folder\Leetcode_folder\leetcode_2327\bin\Debug\net8.0\leetcode_2327.dll"
& 'd:\Leetcode_folder\Leetcode_folder\leetcode_2327\bin\Debug\net8.0\leetcode_2327.exe'
```

## 測試與未來改進

- 可新增 xUnit 單元測試來覆蓋範例與邊界條件（需要我可以幫忙新增）。
- 可把演算法拆成可測試的類別並補上 XML 註解以便文件化。

## 備註

- 目前實作放在 `Program.cs`，以簡潔為主。
- README 著重於演算法細節與推導，方便讀者理解實作原理。
