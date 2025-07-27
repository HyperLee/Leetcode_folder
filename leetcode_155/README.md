# Leetcode 155. Min Stack 最小堆疊

## 題目說明

設計一個支援以下操作的堆疊（Stack）：
- `Push(val)`：將元素 val 推入堆疊。
- `Pop()`：移除堆疊頂端元素。
- `Top()`：取得堆疊頂端元素。
- `GetMin()`：取得堆疊中的最小元素。

**所有操作必須為 O(1) 時間複雜度。**

Leetcode 題目連結：
- [英文版](https://leetcode.com/problems/min-stack/description/)
- [中文版](https://leetcode.cn/problems/min-stack/description/)

---

## 解法詳解

### 雙堆疊設計

為了讓 `GetMin()` 操作能在 O(1) 時間取得最小值，採用「雙堆疊」設計：

- **主堆疊 stack**：儲存所有推入的值。
- **輔助堆疊 minStack**：每次推入新值時，若該值小於等於目前最小值，也推入 minStack。這樣 minStack 的頂端永遠是目前 stack 的最小值。

#### 操作流程

- **Push(val)**  
  1. 將 val 推入主堆疊 stack。
  2. 若 minStack 為空或 val <= minStack.Peek()，則也推入 minStack。

- **Pop()**  
  1. 若 stack.Peek() == minStack.Peek()，則 minStack 也要 Pop。
  2. 主堆疊 Pop。

- **Top()**  
  直接回傳 stack.Peek()。

- **GetMin()**  
  直接回傳 minStack.Peek()。

#### 時間複雜度

- Push: O(1)
- Pop: O(1)
- Top: O(1)
- GetMin: O(1)

---

## 程式碼說明

```csharp
public class MinStack
{
    private readonly Stack<int> stack;      // 主堆疊，儲存所有值
    private readonly Stack<int> minStack;   // 輔助堆疊，儲存最小值

    public MinStack()
    {
        stack = new Stack<int>();
        minStack = new Stack<int>();
    }

    public void Push(int val)
    {
        stack.Push(val);
        if (minStack.Count == 0 || val <= minStack.Peek())
        {
            minStack.Push(val);
        }
    }

    public void Pop()
    {
        if (stack.Count == 0) return;
        if (stack.Peek() == minStack.Peek())
        {
            minStack.Pop();
        }
        stack.Pop();
    }

    public int Top()
    {
        return stack.Peek();
    }

    public int GetMin()
    {
        return minStack.Peek();
    }
}
```

---

## 使用範例

```csharp
MinStack minStack = new MinStack();
minStack.Push(-2);
minStack.Push(0);
minStack.Push(-3);
Console.WriteLine(minStack.GetMin()); // 輸出 -3
minStack.Pop();
Console.WriteLine(minStack.Top());    // 輸出 0
Console.WriteLine(minStack.GetMin()); // 輸出 -2
```

---

## 設計細節與優化

- **空間複雜度**：最壞情況下，minStack 會和 stack 一樣大（例如每次都插入更小的值），但這是為了保證 O(1) 取得最小值。
- **例外處理**：Pop/Top/GetMin 操作前應確認堆疊不為空，避免例外。
- **泛型擴充**：本題只需支援 int 型別，若需支援其他型別可考慮泛型設計。

---

## 參考資料

- [Leetcode 官方解法](https://leetcode.cn/problems/min-stack/solutions/242190/zui-xiao-zhan-by-leetcode-solution/)
- [前綴最小值設計](https://leetcode.cn/problems/min-stack/solutions/2974438/ben-zhi-shi-wei-hu-qian-zhui-zui-xiao-zh-x0g8/)
- [其他高票解法](https://leetcode.cn/problems/min-stack/solutions/1456182/by-stormsunshine-dtzd/)

---

## 測試

請務必針對各種邊界情境（如空堆疊、重複最小值等）進行測試，確保程式碼穩定。

---
