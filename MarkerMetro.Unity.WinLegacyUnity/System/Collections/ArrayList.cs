﻿
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
using System;
using System.Runtime.InteropServices;

namespace MarkerMetro.Unity.WinLegacy.Collections
{
    /**
     * An ArrayList is just a dynamic array of generic objects... very close to List<> which is supported in Metro
     */
    public class ArrayList : List<object>
    {
        public global::System.Array ToArray(global::System.Type elementType)
        {
            var array = global::System.Array.CreateInstance(elementType, Count);
            global::System.Array.Copy(ToArray(), array, Count);

            return array;
        }

        public ArrayList() { }
        public ArrayList(IEnumerable enumerable) : base(enumerable.Cast<object>()) { }
    }
}

