using Bytz.Collections.Dispatch.Contracts;

namespace Bytz.Collections.Dispatch.Actions;

/// <summary>
/// action list taking 6 generic parameter(s).
/// </summary>
/// <typeparam name="T1">generic parameter 1</typeparam>
/// <typeparam name="T2">generic parameter 2</typeparam>
/// <typeparam name="T3">generic parameter 3</typeparam>
/// <typeparam name="T4">generic parameter 4</typeparam>
/// <typeparam name="T5">generic parameter 5</typeparam>
/// <typeparam name="T6">generic parameter 6</typeparam>
public class ActionList<T1, T2, T3, T4, T5, T6>
: Dictionary<Func<T1, T2, T3, T4, T5, T6, bool>, Action<T1, T2, T3, T4, T5, T6>>
, IActionDispatch
{
    /// <summary>
    /// register when created.
    /// </summary>
    public ActionList()
    {
        OnRegister();
    }

    /// <summary>
    /// register actions.
    /// </summary>
    public virtual void OnRegister()
    { }

    /// <summary>
    /// get a single entry based on the input.
    /// </summary>
    /// <param name="p1">generic parameter 1</param>
    /// <param name="p2">generic parameter 2</param>
    /// <param name="p3">generic parameter 3</param>
    /// <param name="p4">generic parameter 4</param>
    /// <param name="p5">generic parameter 5</param>
    /// <param name="p6">generic parameter 6</param>
    /// <returns>related element or null</returns>
    /// <exception cref="InvalidOperationException">Sequence contains no elements.  When no match for the generic parameter(s) is found.</exception>
    /// <exception cref="InvalidOperationException">if the same criteria for the key has been defined more than once.</exception>
    public Action<T1, T2, T3, T4, T5, T6> Single
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5,
        T6 p6
    )
    {
        return this.Where(e => e.Key(p1, p2, p3, p4, p5, p6)).Select(v => v.Value).Single();
    }

    /// <summary>
    /// get a single entry based on the input or null if not found.
    /// </summary>
    /// <param name="p1">generic parameter 1</param>
    /// <param name="p2">generic parameter 2</param>
    /// <param name="p3">generic parameter 3</param>
    /// <param name="p4">generic parameter 4</param>
    /// <param name="p5">generic parameter 5</param>
    /// <param name="p6">generic parameter 6</param>
    /// <returns>related element or null</returns>
    /// <exception cref="InvalidOperationException">if the same criteria for the key has been defined more than once.</exception>
    public Action<T1, T2, T3, T4, T5, T6> SingleOrDefault
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5,
        T6 p6
    )
    {
        return this.Where(e => e.Key(p1, p2, p3, p4, p5, p6)).Select(v => v.Value).SingleOrDefault();
    }

    /// <summary>
    /// call the identified method with the specified input.
    /// </summary>
    /// <param name="p1">generic parameter 1</param>
    /// <param name="p2">generic parameter 2</param>
    /// <param name="p3">generic parameter 3</param>
    /// <param name="p4">generic parameter 4</param>
    /// <param name="p5">generic parameter 5</param>
    /// <param name="p6">generic parameter 6</param>
    /// <returns>result from calling the identified function</returns>
    /// <exception cref="InvalidOperationException">if the criteria produces no results for the input</exception>>
    public void Call
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5,
        T6 p6
    )
    {
        Single(p1, p2, p3, p4, p5, p6)(p1, p2, p3, p4, p5, p6);
    }

    /// <summary>
    /// call the identified method with the specified input.
    /// </summary>
    /// <param name="p1">generic parameter 1</param>
    /// <param name="p2">generic parameter 2</param>
    /// <param name="p3">generic parameter 3</param>
    /// <param name="p4">generic parameter 4</param>
    /// <param name="p5">generic parameter 5</param>
    /// <param name="p6">generic parameter 6</param>
    /// <param name="default">default to action to call when no match is found</param>
    /// <returns>result from calling the identified function</returns>
    /// <exception cref="InvalidOperationException">if the same criteria for the key has been defined more than once.</exception>
    public void Call
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5,
        T6 p6,
        Action<T1, T2, T3, T4, T5, T6> @default
    )
    {
        (SingleOrDefault(p1, p2, p3, p4, p5, p6) ?? @default)(p1, p2, p3, p4, p5, p6);
    }

    /// <summary>
    /// get the index of the entry that meets the state of the input value(s)
    /// </summary>
    /// <param name="p1">generic parameter 1</param>
    /// <param name="p2">generic parameter 2</param>
    /// <param name="p3">generic parameter 3</param>
    /// <param name="p4">generic parameter 4</param>
    /// <param name="p5">generic parameter 5</param>
    /// <param name="p6">generic parameter 6</param>
    /// <returns>index for the matching element</returns>
    /// <remarks>
    /// the dictionary does not inherently provide an index base off-of a key.  this method
    /// approximates the position by using the enumerable.select overload that provides
    /// this.  once registered through the OnRegister or an initializer, this should always be unchanging.
    /// </remarks>
    public int IndexOf
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5,
        T6 p6
    )
    {
        return this
            .Select
            (
                (e, i) => new { Element = e, Index = i }
            )
            .Where(c => c.Element.Key(p1, p2, p3, p4, p5, p6))
            .Select(e => e.Index)
            .Single();
    }

    /// <summary>
    /// calls all actions that match the current state of the generic parameter(s)
    /// </summary>
    /// <param name="p1">generic parameter 1</param>
    /// <param name="p2">generic parameter 2</param>
    /// <param name="p3">generic parameter 3</param>
    /// <param name="p4">generic parameter 4</param>
    /// <param name="p5">generic parameter 5</param>
    /// <param name="p6">generic parameter 6</param>
    public void CallAll
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5,
        T6 p6
    )
    {
        this.Where(p1, p2, p3, p4, p5, p6).ToList().ForEach(a => a(p1, p2, p3, p4, p5, p6));
    }

    /// <summary>
    /// find all actions that match the current state of p1
    /// </summary>
    /// <param name="p1">generic parameter 1</param>
    /// <param name="p2">generic parameter 2</param>
    /// <param name="p3">generic parameter 3</param>
    /// <param name="p4">generic parameter 4</param>
    /// <param name="p5">generic parameter 5</param>
    /// <param name="p6">generic parameter 6</param>
    /// <returns></returns>
    public IEnumerable<Action<T1, T2, T3, T4, T5, T6>> Where
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5,
        T6 p6
    )
    {
        return this.Where(e => e.Key(p1, p2, p3, p4, p5, p6)).Select(v => v.Value);
    }

    /// <summary>
    /// calls all actions that match the current state of the generic parameter(s)
    /// </summary>
    /// <param name="p1">generic parameter 1</param>
    /// <param name="p2">generic parameter 2</param>
    /// <param name="p3">generic parameter 3</param>
    /// <param name="p4">generic parameter 4</param>
    /// <param name="p5">generic parameter 5</param>
    /// <param name="p6">generic parameter 6</param>
    /// <param name="default">default to action to call when no match is found</param>
    public void CallAll
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5,
        T6 p6,
        Action<T1, T2, T3, T4, T5, T6> @default
    )
    {
        IEnumerable<Action<T1, T2, T3, T4, T5, T6>> actions = Where(p1, p2, p3, p4, p5, p6);

        if (!actions.Any())
        {
            @default(p1, p2, p3, p4, p5, p6);
            return;
        }

        actions.ToList().ForEach(a => a(p1, p2, p3, p4, p5, p6));
    }

    /// <summary>
    /// count the number of conditions that match the state of the generic parameter(s)
    /// </summary>
    /// <param name="p1">generic parameter 1</param>
    /// <param name="p2">generic parameter 2</param>
    /// <param name="p3">generic parameter 3</param>
    /// <param name="p4">generic parameter 4</param>
    /// <param name="p5">generic parameter 5</param>
    /// <param name="p6">generic parameter 6</param>
    /// <returns>count of items that match the state of the generic parameter(s).</returns>
    public int CountOf
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5,
        T6 p6
    )
    {
        return this.Count(k => k.Key(p1, p2, p3, p4, p5, p6));
    }
}