namespace leetcode_3310;

class Program
{
    /// <summary>
    /// https://leetcode.com/problems/remove-methods-from-project/description/
    /// 3310. Remove Methods From Project
    /// https://leetcode.cn/problems/remove-methods-from-project/description/
    /// 3310. 移除可疑的方法
    ///
    /// English:
    /// You are maintaining a project that has n methods numbered from 0 to n - 1.
    ///
    /// You are given two integers n and k, and a 2D integer array invocations, where invocations[i] = [ai, bi] indicates that method ai invokes method bi.
    ///
    /// There is a known bug in method k. Method k, along with any method invoked by it, either directly or indirectly, are considered suspicious and we aim to remove them.
    ///
    /// A group of methods can only be removed if no method outside the group invokes any methods within it.
    ///
    /// Return an array containing all the remaining methods after removing all the suspicious methods. You may return the answer in any order. If it is not possible to remove all the suspicious methods, none should be removed.
    ///
    /// 繁體中文：
    /// 你正在維護一個共有 n 個方法的專案，這些方法的編號從 0 到 n - 1。
    ///
    /// 給定兩個整數 n 和 k，以及一個二維整數陣列 invocations，其中 invocations[i] = [ai, bi] 表示方法 ai 會呼叫方法 bi。
    ///
    /// 方法 k 已知存在錯誤。方法 k，以及所有由它直接或間接呼叫的方法，都被視為可疑方法，我們希望移除它們。
    ///
    /// 只有在群組外沒有任何方法呼叫群組內的方法時，才能移除一組方法。
    ///
    /// 請回傳移除所有可疑方法後的所有剩餘方法。答案可以按照任意順序回傳。如果無法移除所有可疑方法，則不移除任何方法。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
