using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Machine.BLL.BusinessModels
{
    public class RestOfMoney
    {
        //public RestOfMoney(int val)
        //{
        //    _value = val;
        //}
        //private int _value = 0;
        //public int Value { get { return _value; } }
        public Dictionary<int, int> CalculateChange(int Money)
        {
            Dictionary<int, int> Dic = new Dictionary<int, int>();
            int[] FaceValues = { 10, 5, 2, 1 };
            foreach (int item in FaceValues)
            {
                if (Money / item == 0) continue;
                Dic.Add(item, Money / item);
                Money %= item;
                if (Money == 0) break;
            }
            return Dic;
        }
    }
}