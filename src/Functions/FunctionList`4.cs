using Bytz.Collections.Dispatch.Contracts;

namespace Bytz.Collections.Dispatch.Functions;

/// <summary>
/// function list taking 3 parameter(s).
/// </summary>
/// <typeparam name="T1">parameter 1</typeparam>
/// <typeparam name="T2">parameter 2</typeparam>
/// <typeparam name="T3">parameter 3</typeparam>
/// <typeparam name="TReturn">type of the return from the function</typeparam>
public class FunctionList<T1, T2, T3, TReturn>
: Dictionary<Func<T1, T2, T3, bool>, Func<T1, T2, T3, TReturn>>, IFunctionDispatch
{
    /// <summary>
    /// register when created.
    /// </summary>
    public FunctionList()
    {
        OnRegister();
    }

    /// <summary>
    /// register functions.
    /// </summary>
    public void OnRegister()
    { }

    /// <summary>
    /// get a single entry based on the input.
    /// </summary>
    /// <param name="p1">parameter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <returns>related element or null</returns>
    /// <exception cref="InvalidOperationException">if the same criteria for the key has been defined more than once.</exception>
    public Func<T1, T2, T3, TReturn> Single
    (
        T1 p1,
        T2 p2,
        T3 p3
    )
    {
        return this.Where(e => e.Key(p1, p2, p3)).Select(v => v.Value).Single();
    }

    /// <summary>
    /// get a single entry based on the input or null if not found.
    /// </summary>
    /// <param name="p1">parameter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <returns>related element or null</returns>
    /// <exception cref="InvalidOperationException">if the same criteria for the key has been defined more than once.</exception>
    public Func<T1, T2, T3, TReturn> SingleOrDefault
    (
        T1 p1,
        T2 p2,
        T3 p3
    )
    {
        return this.Where(e => e.Key(p1, p2, p3)).Select(v => v.Value).SingleOrDefault();
    }

    /// <summary>
    /// call the identified method with the specified input.
    /// </summary>
    /// <param name="p1">parameter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <returns>result from calling the identified function</returns>
    /// <exception cref="InvalidOperationException">if the criteria produces no results for the input [use the default overload]</exception>>
    public TReturn Call
    (
        T1 p1,
        T2 p2,
        T3 p3
    )
    {
        return Single(p1, p2, p3)(p1, p2, p3);
    }

    /// <summary>
    /// call the identified method with the specified input.
    /// </summary>
    /// <param name="p1">parameter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <param name="default">default to call if no match found</param>
    /// <returns>result from calling the identified function</returns>
    /// <exception cref="InvalidOperationException">if the same criteria for the key has been defined more than once.</exception>
    public TReturn Call
    (
        T1 p1,
        T2 p2,
        T3 p3,
        Func<T1, T2, T3, TReturn> @default
    )
    {
        return (SingleOrDefault(p1, p2, p3) ?? @default)(p1, p2, p3);
    }

    /// <summary>
    /// get the index of the entry that meets the state of the input value(s)
    /// </summary>
    /// <param name="p1">parameter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <returns>index for the matching element</returns>
    /// <exception cref="InvalidOperationException">Sequence contains no elements.  When no match for the parameter(s) is found.</exception>
    /// <remarks>
    /// the dictionary does not inherently provide an index base off-of a key.  this method
    /// approximates the position by using the enumerable.select overload that provides
    /// this.  for static lists i expect that this should always consistent.
    /// </remarks>
    public int IndexOf
    (
        T1 p1,
        T2 p2,
        T3 p3
    )
    {
        return this
            .Select
            (
                (e, i) => new { Element = e, Index = i }
            )
            .Where(c => c.Element.Key(p1, p2, p3))
            .Select(e => e.Index)
            .Single();
    }

    /// <summary>
    /// calls all actions that match the current state of the parameter(s)
    /// </summary>
    /// <param name="p1">paramter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <remarks>
    /// i think that I want to return the product of all function calls but this poses a problem that i am 
    /// uncertain of how to handle or of the value provided since it will be tightly-coupled to the ordinal execution:  
    /// this feels like it would introduce code-smell.
    /// 
    ///     ordering    - while i guess it should be easy to return the results in order of execution, i am uncertain 
    ///                 - of the reliability of this. (see remarks for IndexOf)
    ///     chaining    - it MAY be possible to use the results of a function to a successive call, i am not certain how to attain this yet. 
    ///                 - i need to think on this.  likely not possible (or overly complicated to implement) with the number of variations.
    ///
    ///     any input/thoughts/insights are appreciated.
    /// </remarks>
    public void CallAll
    (
        T1 p1,
        T2 p2,
        T3 p3
    )
    {
        this.Where(p1, p2, p3).ToList().ForEach(f => f(p1, p2, p3));
    }

    /// <summary>
    /// find all actions that match the current state of p1
    /// </summary>
    /// <param name="p1">paramter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <returns></returns>
    public IEnumerable<Func<T1, T2, T3, TReturn>> Where
    (
        T1 p1,
        T2 p2,
        T3 p3
    )
    {
        return this.Where(e => e.Key(p1, p2, p3)).Select(v => v.Value);
    }

    /// <summary>
    /// calls all actions that match the current state of the parameter(s)
    /// </summary>
    /// <param name="p1">paramter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <param name="default">default to call if no match found</param>
    public void CallAll
    (
        T1 p1,
        T2 p2,
        T3 p3,
        Func<T1, T2, T3, TReturn> @default
    )
    {
        IEnumerable<Func<T1, T2, T3, TReturn>> functions = Where(p1, p2, p3);

        if (functions.Any() == false)
        {
            @default(p1, p2, p3);
            return;
        }

        functions.ToList().ForEach(f => f(p1, p2, p3));
    }

    /// <summary>
    /// count the number of conditions that match the state of the parameter(s)
    /// </summary>
    /// <param name="p1">paramter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <returns>count of items that match the state of the parameter(s).</returns>
    [Obsolete("use count-of")]
    public int CountFor
    (
        T1 p1,
        T2 p2,
        T3 p3
    )
    {
        return this.Count(k => k.Key(p1, p2, p3));
    }

    /// count the number of conditions that match the state of the parameter(s)
    /// </summary>
    /// <param name="p1">paramter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <returns>count of items that match the state of the parameter(s).</returns>
    public int CountOf
    (
        T1 p1,
        T2 p2,
        T3 p3
    )
    {
        return this.Count(k => k.Key(p1, p2, p3));
    }
}
