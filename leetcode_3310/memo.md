這題的敘述確實有點繞，尤其是 **"可以移除"** 的條件，很容易第一次看不懂。

我用一個故事來解釋。

---

## Step 1. 先找出所有「可疑方法」

給你一個 bug 在 `k`。

只要是

* `k`
* `k` 呼叫的方法
* 那些方法再呼叫的方法
* ...

全部都算 **可疑(suspicious)**。

例如

```
0 -> 1 -> 2 -> 5
      |
      v
      3
4
```

如果

```
k = 1
```

那麼

```
Suspicious = {1,2,3,5}
```

因為都是從 1 可以一路走到的。

這一步就是一個 DFS/BFS。([LeetCode][1])

---

## Step 2. 我們想把這些方法整包刪掉

也就是想把

```
{1,2,3,5}
```

全部移除。

但是有一個限制：

> **外面的 method 不能呼叫裡面的 method**

也就是說

```
Outside  ---> Suspicious
```

這種邊不能存在。

只要有一條，就不能刪。

---

## 為什麼？

想像真正的程式：

```
method0()
{
    method1();   // method1 要被刪掉
}
```

如果把

```
method1
```

刪掉

那

```
method0
```

就會壞掉。

因此：

> 如果有正常的方法還依賴可疑的方法，就不能只刪可疑的方法。

---

# Example 1

```
n = 4

1 -> 2
0 -> 1
3 -> 2

k = 1
```

畫圖：

```
0 ----\
       v
       1 --->2
            ^
            |
            3
```

先找 suspicious：

```
1
2
```

所以

```
S = {1,2}
```

外面的點

```
0
3
```

看看有沒有指進去

```
0 ->1   YES
3 ->2   YES
```

有！

表示

```
Outside
   |
   v
Suspicious
```

存在。

所以不能刪。

答案就是

```
[0,1,2,3]
```

也就是

> 一個都不要刪。([LeetCode][1])

---

# Example 2

```
0 ->1
0 ->2
1 ->2

3 ->4

k=0
```

圖：

```
0
|\
| \
v  v
1->2

3->4
```

Suspicious：

```
0
1
2
```

外面只有

```
3
4
```

有沒有

```
3 ->0
3 ->1
3 ->2
4 ->...
```

沒有。

所以

```
Suspicious
```

已經跟外界完全隔離。

可以整包刪。

剩

```
3
4
```

答案

```
[3,4]
```

---

# Example 3

```
2 ->0
0 ->1
1 ->2

k=2
```

圖：

```
0
↑ \
|  \
2<-1
```

從 2 可以走到

```
2
0
1
```

全部都是 suspicious。

整個 project 都是 suspicious。

那就全部刪。

答案

```
[]
```

---

# 真正的重點（很多人卡在這）

很多人以為題目是在檢查

> suspicious 是否會呼叫 normal

不是。

它檢查的是

> **normal 是否呼叫 suspicious**

方向非常重要。

允許：

```
Suspicious
      |
      v
Normal
```

因為刪掉 suspicious 後，整條路一起消失了，不會有人去呼叫它。

**不允許：**

```
Normal
     |
     v
Suspicious
```

因為 normal 還活著，卻會呼叫一個不存在的方法。

---

## 所以整題其實只有兩步

1. 從 `k` DFS/BFS，找出所有 `suspicious`。
2. 掃描所有邊 `(u, v)`：

   * 如果

     ```
     u 不在 suspicious
     v 在 suspicious
     ```

     代表外部呼叫內部。
     **不能刪任何方法**，直接回傳 `0 ~ n-1`。
   * 如果完全沒有這種邊，就把 suspicious 全部刪掉，回傳剩下的方法。([AlgoMonster][2])

理解這個判斷條件後，這題的實作其實就相當直接，只需要一次 DFS/BFS 加一次遍歷所有邊即可。

[1]: https://leetcode.com/problems/remove-methods-from-project/solutions/?utm_source=chatgpt.com "Remove Methods From Project - LeetCode"
[2]: https://algo.monster/liteproblems/3310?utm_source=chatgpt.com "3310. Remove Methods From Project - In-Depth Explanation"
