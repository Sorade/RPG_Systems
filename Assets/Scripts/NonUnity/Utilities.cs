using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Utilities {

    /*public static List<T> Filter<T>(IList a)
    {
        List<T> filtered = a.OfType<T>().ToList();
        return filtered;
    }*/

    public static T[] Filter<T>(IEnumerable a)
    {
        T[] filtered = a.OfType<T>().ToArray();
        return filtered;
    }


}
