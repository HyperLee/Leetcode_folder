namespace leetcode_1356;

class Program
{
    /// <summary>
    /// <summary>
    /// 1356. Sort Integers by The Number of 1 Bits
    /// Problem description:
    /// You are given an integer array arr. Sort the integers in the array
    /// in ascending order by the number of 1's in their binary representation
    /// and in case of two or more integers have the same number of 1's you
    /// have to sort them in ascending order.
    /// <para/>
    /// Return the array after sorting it.
    ///
    /// 題目說明（繁體中文）:
    /// 給定一個整數陣列 arr。請根據每個整數二進位表示中 1 的數量，以遞增順序對陣列進行排序，
    /// 若兩個或多個整數具有相同的 1 的數量，則按數值大小升序排序。
    ///
    /// 傳回排序後的陣列。
    ///
    /// 參考連結：https://leetcode.com/problems/sort-integers-by-the-number-of-1-bits/
    /// 及中文版：https://leetcode.cn/problems/sort-integers-by-the-number-of-1-bits/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 先升序排序，再依次收集到与1的数目对应的桶中。
    /// 
    /// SortedDictionary
    /// key: 二進制有幾個 1
    /// value: list型態依序存放排序後 塞入的arr[i]
    /// 
    /// ex:
    /// dict = [0, 1]  => 0個1, value是0
    /// dict = [1, 4]  => 4個1, value是1,2,4,8
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    public int[] SortByBits(int[] arr)
    {
        Array.Sort(arr);

        var dict = new SortedDictionary<int, List<int>>();

        for(int i = 0; i < arr.Length; i++)
        {
            int key = PopCount2(arr[i]);

            if(dict.ContainsKey(key))
            {
                dict[key].Add(arr[i]);
            }
            else
            {
                dict.Add(key, new List<int>() { arr[i] });
            }
        }

        var ret = new int[arr.Length];
        int idx = 0;

        foreach(var kvp in dict)
        {
            foreach(var num in kvp.Value)
            {
                ret[idx++] = num;
            }
        }

        return ret;
    }

    /// <summary>
    /// 計算 二進制中 1個數有幾個
    ///  01 & 10 => 0
    ///  10 & 00 => 0
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int PopCount(int n)
    {
        int counter = 0;

        while(n > 0)
        {
            counter++;
            n = n & (n - 1);
        }

        return counter;
    }


    /// <summary>
    /// 計算 二進制中 1個數有幾個
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int PopCount2(int n)
    {
        int res = 0;
        while(n != 0)
        {
            res += (n % 2);
            n /= 2;
        }

        return res;
    }    
}
