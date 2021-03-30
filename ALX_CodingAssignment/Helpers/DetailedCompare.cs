﻿using System.Collections.Generic;
using System.Reflection;

namespace ALX_CodingAssignment.Helpers
{
    public static class extentions
    {
        public static List<Variance> DetailedCompare<T>(this T val1, T val2)
        {
            List<Variance> variances = new List<Variance>();
            FieldInfo[] fi = val1.GetType().GetFields();
            foreach (FieldInfo f in fi)
            {
                Variance v = new Variance();
                v.Prop = f.Name;
                v.valA = f.GetValue(val1);
                v.valB = f.GetValue(val2);
                if (!v.valA.Equals(v.valB))
                    variances.Add(v);

            }
            return variances;
        }


    }
    public class Variance
    {
        public string Prop { get; set; }
        public object valA { get; set; }
        public object valB { get; set; }
    }
}