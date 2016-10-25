namespace GofusSharp
{
    public class Liste<T> : System.Collections.Generic.List<T>
    {
        public static Liste<T> ConcatAlternate(Liste<T> first, Liste<T> second)
        {
            Liste<T> concatList = new Liste<T>();
            if (first.Count < second.Count)
            {
                concatList.AddRange(first);
                int i = 1;
                foreach (T item in second)
                {
                    if (i > concatList.Count)
                        i = concatList.Count;
                    concatList.Insert(i, item);
                    i += 2;
                }
            }
            else
            {
                concatList.AddRange(second);
                int i = 1;
                foreach (T item in first)
                {
                    if (i > concatList.Count)
                        i = concatList.Count;
                    concatList.Insert(i, item);
                    i += 2;
                }
            }
            return concatList;
        }
    }
}
