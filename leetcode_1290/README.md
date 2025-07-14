# leetcode_1290

## 專案簡介

本專案為 LeetCode 第 1290 題「Convert Binary Number in a Linked List to Integer（二進位鍊錶轉整數）」的 C# 解法，採用最新 C# 13 標準，並遵循最佳程式設計與註解規範。

## 題目說明

給定一個只包含 0 與 1 的單向鍊錶，代表一個二進位數字，請將其轉換為十進位整數。

- 範例：
  - 鍊錶：1 → 1 → 0
  - 對應二進位：110
  - 十進位結果：6

## 解法詳解

### 解題思路

本題可用「累積左移」法，逐步將鍊錶中的二進位數字轉為十進位整數。

#### 詳細步驟

1. 初始化累積結果 res = 0。
2. 使用 currNode 指向鍊錶頭節點 head。
3. 進入 while 迴圈，當 currNode 不為 null 時重複：
   - 將 res 左移一位（乘以 2），再加上 currNode.val（即目前節點的位元值）。
   - 這步驟等同於將新位元加到最低位。
   - 移動 currNode 到下一個節點。
4. 迴圈結束後，res 即為十進位整數。

#### 程式碼片段

```csharp
public int GetDecimalValue(ListNode head)
{
    ListNode? currNode = head;
    int res = 0;
    while (currNode != null)
    {
        res = res * 2 + currNode.val;
        currNode = currNode.next;
    }
    return res;
}
```

#### 執行範例

```csharp
ListNode head = new ListNode(1, new ListNode(1, new ListNode(0)));
int result = new Program().GetDecimalValue(head); // result = 6
```

### 複雜度分析

- 時間複雜度：O(n)
  - 需遍歷每個節點一次，n 為鍊錶長度。
- 空間複雜度：O(1)
  - 僅使用常數額外空間。

## 執行方式

1. 確認已安裝 .NET 8 SDK。
2. 於專案根目錄執行：

```bash
# 建構專案
 dotnet build
# 執行程式
 dotnet run --project leetcode_1290/leetcode_1290.csproj
```

## 專案結構

- `leetcode_1290/Program.cs`：主程式與解題邏輯
- `leetcode_1290/leetcode_1290.csproj`：C# 專案檔
- `.editorconfig`：程式碼風格設定
- `.gitignore`：Git 忽略檔案設定

## 參考
- [LeetCode 題目連結（英文）](https://leetcode.com/problems/convert-binary-number-in-a-linked-list-to-integer/description/?envType=daily-question&envId=2025-07-14)
- [LeetCode 題目連結（中文）](https://leetcode.cn/problems/convert-binary-number-in-a-linked-list-to-integer/description/?envType=daily-question&envId=2025-07-14)

---
如需更多解法或測試資料，歡迎提出！
