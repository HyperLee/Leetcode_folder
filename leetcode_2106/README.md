# Leetcode 2106: Maximum Fruits Harvested After at Most K Steps

> [!TIP]
> 本專案為 Leetcode 2106 題「Maximum Fruits Harvested After at Most K Steps」的 C# 解題專案，包含詳細註解與多種最佳化演算法實作。

## 專案簡介

- 題目連結：[Leetcode 2106](https://leetcode.com/problems/maximum-fruits-harvested-after-at-most-k-steps/)
- 中文題解：[Leetcode CN 2106](https://leetcode.cn/problems/maximum-fruits-harvested-after-at-most-k-steps/)
- 本專案以 C# 13 撰寫，並依據現代 C# 最佳實踐設計，適合學習滑動視窗、前綴和等演算法技巧。

## Features

- ✨ 多種最佳化演算法（滑動視窗、前綴和）
- 📚 詳細中英文註解，便於學習與理解
- 🧪 易於擴充測試與驗證
- 🛠️ 遵循現代 C# 13 語法與風格

## 解法說明

### 解法一：滑動視窗 + 前綴和

此方法利用滑動視窗（Sliding Window）與前綴和（Prefix Sum）優化區間查詢，適合處理已排序且區間查詢頻繁的情境。

**步驟說明：**
1. 先將所有水果位置與數量建立前綴和陣列 `prefix`，可 O(1) 查詢任意區間水果總數。
2. 針對每個可能的收穫區間，分別考慮：
   - 先往左再往右（left 固定，right 擴展），只要步數不超過 k 就持續擴展 right。
   - 先往右再往左（right 固定，left 收縮），只要步數不超過 k 就持續收縮 left。
3. 每次計算合法區間內水果總數，取最大值。

**優點：**
- 查詢區間和效率高（O(1)），總體複雜度 O(n)
- 適合資料量大、查詢密集的場景

**缺點：**
- 需額外 O(n) 空間儲存前綴和
- 需正確處理步數計算與區間合法性

---

### 解法二：滑動視窗 + 動態步數計算

此方法進一步將步數計算公式抽象化，針對每個滑動視窗區間 [left, right]，動態計算從 startPos 覆蓋該區間的最小步數。

**步驟說明：**
1. 使用雙指標維護一個滑動視窗 [left, right]，每次右指標 right 向右擴展。
2. 當目前視窗區間所需步數超過 k 時，左指標 left 右移，縮小視窗。
3. 步數計算公式：
   - 若區間全在 startPos 左側：step = startPos - left
   - 若區間全在右側：step = right - startPos
   - 若 startPos 在區間內：step = (right - left) + min(|right - startPos|, |startPos - left|)
4. 每次維護區間內水果總數 sum，並更新最大值。

**優點：**
- 步數計算更彈性，適用於各種起點與區間組合
- 複雜度 O(n)，空間需求低

**缺點：**
- 需正確實作步數計算公式
- 對於初學者理解較為抽象

> [!NOTE]
> 兩種解法皆可 AC，建議根據個人習慣與理解選擇。

## Getting Started

### 前置需求
- .NET 8 SDK 或更新版本
- 建議使用 Visual Studio Code 或 Visual Studio 2022 以上

### 安裝與執行

```bash
# 1. 取得原始碼
 git clone <本專案網址>
 cd leetcode_2106

# 2. 建構專案
 dotnet build leetcode_2106/leetcode_2106.csproj

# 3. 執行主程式
 dotnet run --project leetcode_2106/leetcode_2106.csproj
```

> [!NOTE]
> 預設主程式僅輸出 Hello, World!，請依需求修改 `Program.cs` 以執行演算法測試。

### 測試

目前專案未內建單元測試，建議可依照 C# 標準測試框架（如 xUnit、NUnit）自行擴充。

## Usage Example

```csharp
// 範例呼叫
var solution = new Program();
int[][] fruits = new int[][] {
    new int[] {2, 8},
    new int[] {6, 3},
    new int[] {9, 5}
};
int startPos = 5;
int k = 4;
int result = solution.MaxTotalFruits(fruits, startPos, k);
Console.WriteLine($"最多可收穫水果數：{result}");
```

## Project Structure

```
leetcode_2106.sln
leetcode_2106/
  leetcode_2106.csproj
  Program.cs         # 主程式與演算法實作
  bin/               # 編譯輸出
  obj/               # 中繼檔
```

- `Program.cs`：主程式與所有演算法解法，含詳細註解
- `leetcode_2106.csproj`：C# 專案檔

## Resources

- [Leetcode 2106 題解（官方）](https://leetcode.cn/problems/maximum-fruits-harvested-after-at-most-k-steps/solutions/2254268/zhai-shui-guo-by-leetcode-solution-4j9v/)
- [C# 官方文件](https://learn.microsoft.com/dotnet/csharp/)
- [滑動視窗演算法介紹](https://labuladong.github.io/algo/di-yi-zhan-da78c/shou-ba-sh-48c1d/)

## Troubleshooting

- 若遇到 .NET SDK 版本問題，請確認已安裝 .NET 8 或以上版本。
- 若執行出現權限問題，請嘗試以管理員身份執行命令。

> [!IMPORTANT]
> 如需進一步協助，請於 Issues 區提出問題。
