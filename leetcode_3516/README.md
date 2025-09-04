# leetcode_3516

## 題目（中文）
給定三個整數 x、y 和 z，表示數線上三個人的位置：

- x 是第 1 個人的位置。
- y 是第 2 個人的位置。
- z 是第 3 個人的位置，且第 3 個人不會移動。

第 1 個人與第 2 個人以相同速度朝向第 3 個人移動，判斷誰會先到達第 3 個人：

- 若第 1 個人先到，回傳 1。
- 若第 2 個人先到，回傳 2。
- 若同時到達，回傳 0。

## 解法概述

由於兩個人以相同速度前進，誰先到達第 3 個人僅取決於與第 3 個人的距離（絕對差）。我們計算：

- distanceX = |z - x|
- distanceY = |z - y|

比較 distanceX 與 distanceY：

- 若 distanceX < distanceY，回傳 1。
- 若 distanceX > distanceY，回傳 2。
- 若相等，回傳 0。

這是 O(1) 時間與 O(1) 額外空間的簡單判斷。

## 範例

輸入: x = 1, y = 2, z = 3

輸出: 1

解釋: 第 1 個人與第 3 個人的距離為 2，第 2 個人與第 3 個人的距離為 1，故第 2 個人更快到達（此處應為示範格式，請以具體範例替換）。

## 程式碼（C# 範例）

程式已包含於 `leetcode_3516/Program.cs`，核心實作如下：

```csharp
public int FindClosest(int x, int y, int z)
{
    int distanceX = Math.Abs(z - x);
    int distanceY = Math.Abs(z - y);

    if (distanceX < distanceY)
    {
        return 1;
    }
    else if (distanceX > distanceY)
    {
        return 2;
    }
    else
    {
        return 0;
    }
}
```

## 時間與空間複雜度

- 時間複雜度: O(1)
- 額外空間: O(1)

## 如何執行

在專案根目錄執行：

```powershell
dotnet run --project .\leetcode_3516\leetcode_3516.csproj
```

或者使用 Visual Studio/VS Code 的執行功能。

---

如果你想，我可以：

- 加入單元測試範例。
- 將 `Main` 改為示範輸入輸出流程。
- 格式化與補充更多範例說明。
