using System;
using System.Collections.Generic;
using System.Linq;

public class Combination
{
    public static void GenerateCombinations<TElement>(IEnumerable<TElement> values, int maxLength, Func<TElement[], bool> callback)
    {
        GenerateCombinations(values, 1, maxLength, callback);
    }

    public static void GenerateCombinations<TElement>(IEnumerable<TElement> values, int minLength, int maxLength, Func<TElement[], bool> callback)
    {
        if (minLength <= 0)
        {
            throw new ArgumentException("MinLength must be a number more than zero.");
        }

        if (maxLength <= 0)
        {
            throw new ArgumentException("MaxLength must be a number more than zero.");
        }

        if (minLength > maxLength)
        {
            throw new ArgumentException("MinLength must be less than or equal to MaxLength.");
        }

        for (int length = minLength; length <= maxLength; length++)
        {
            var array = new TElement[length];
            GenerateCombinations(array, values, callback);
        }
    }

    public static void GenerateCombinations<TElement>(IEnumerable<TElement> values, int maxLength, Action<TElement[]> callback)
    {
        GenerateCombinations(values, 1, maxLength, callback);
    }

    public static void GenerateCombinations<TElement>(IEnumerable<TElement> values, int minLength, int maxLength, Action<TElement[]> callback)
    {
        GenerateCombinations(values, minLength, maxLength, (array) =>
            {
                callback(array);

                return true;
            });
    }

    private static void GenerateCombinations<TElement>(TElement[] array, IEnumerable<TElement> values, Func<TElement[], bool> callback, int index = 0)
    {
        foreach (var value in values)
        {
            array[index] = value;

            if (index < array.Length - 1)
            {
                GenerateCombinations(array, values, callback, index + 1);
            }
            else if (!callback(array))
            {
                break;
            }
        }
    }
}