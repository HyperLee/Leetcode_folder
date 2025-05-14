# Leetcode 232: 用堆疊實作佇列

## 題目描述

使用兩個堆疊 (Stack) 來建立一個佇列 (Queue)，需支援 `push(x)`、`pop()`、`peek()`、`empty()` 四種操作。
你只能使用標準的堆疊操作 (push、pop、peek、empty)。

- [Leetcode 232. Implement Queue using Stacks](https://leetcode.com/problems/implement-queue-using-stacks/description/)
- [Leetcode 中文題目連結](https://leetcode.cn/problems/implement-queue-using-stacks/description/)

## 解題概念與出發點

1. 使用兩個堆疊 `inStack` 與 `outStack`。
2. push 時將元素推入 `inStack`。
3. pop/peek 時，若 `outStack` 為空，將 `inStack` 所有元素依序彈出並推入 `outStack`，這樣 `outStack` 的頂端即為佇列開頭。
4. empty 則判斷兩個堆疊皆為空。

這樣可確保所有操作皆符合佇列 (先進先出) 特性。

## 程式碼架構

- `MyQueue` 類別：
  - `Push(int x)`: 將元素推到佇列尾端。
  - `Pop()`: 移除並返回佇列開頭元素。
  - `Peek()`: 返回佇列開頭元素但不移除。
  - `Empty()`: 判斷佇列是否為空。

## 範例測試

```csharp
MyQueue queue = new MyQueue();
queue.Push(1);
queue.Push(2);
Console.WriteLine(queue.Peek()); // 輸出 1
Console.WriteLine(queue.Pop());  // 輸出 1
Console.WriteLine(queue.Empty()); // 輸出 False
```

## 參考資料

- [Leetcode 官方解說 (中文)](https://leetcode.cn/problems/implement-queue-using-stacks/solution/yong-zhan-shi-xian-dui-lie-by-leetcode-s-xnb6/)

---

本專案使用 .NET 8.0，主程式於 `Program.cs`。
