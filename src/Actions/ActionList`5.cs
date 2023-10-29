using Bytz.Collections.Dispatch.Contracts;

namespace Bytz.Collections.Dispatch.Actions;

/// <summary>
/// action list taking 5 parameter.
/// </summary>
/// <typeparam name="T1">parameter 1</typeparam>
/// <typeparam name="T2">parameter 2</typeparam>
/// <typeparam name="T3">parameter 3</typeparam>
/// <typeparam name="T4">parameter 4</typeparam>
/// <typeparam name="T5">parameter 5</typeparam>
/// <typeparam name="TReturn">type of the return from the function</typeparam>
public class ActionList<T1, T2, T3, T4, T5>
: Dictionary<Func<T1, T2, T3, T4, T5, bool>, Action<T1, T2, T3, T4, T5>>, IActionDispatch
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
    /// <param name="p1">parameter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <param name="p4">parameter 4</param>
    /// <param name="p5">parameter 5</param>
    /// <returns>related element for parameter</returns>
    /// <exception cref="InvalidOperationException">if the same criteria for the key has been defined more than once.</exception>
    public Action<T1, T2, T3, T4, T5> Single
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5
    )
    {
        return this.Where(e => e.Key(p1, p2, p3, p4, p5)).Select(v => v.Value).Single();
    }

    /// <summary>
    /// get a single entry based on the input or null if not found.
    /// </summary>
    /// <param name="p1">parameter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <param name="p4">parameter 4</param>
    /// <param name="p5">parameter 5</param>
    /// <returns>related element for parameter</returns>
    /// <exception cref="InvalidOperationException">if the same criteria for the key has been defined more than once.</exception>
    public Action<T1, T2, T3, T4, T5> SingleOrDefault
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5
    )
    {
        return this.Where(e => e.Key(p1, p2, p3, p4, p5)).Select(v => v.Value).SingleOrDefault();
    }

    /// <summary>
    /// call the identified method with the specified input.
    /// </summary>
    /// <param name="p1">parameter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <param name="p4">parameter 4</param>
    /// <param name="p5">parameter 5</param>
    /// <returns>result from calling the identified function</returns>
    /// <exception cref="InvalidOperationException">if the criteria produces no results for the input [use the default overload]</exception>>
    public void Call
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5
    )
    {
        Single(p1, p2, p3, p4, p5)(p1, p2, p3, p4, p5);
    }

    /// <summary>
    /// call the identified method with the specified input.
    /// </summary>
    /// <param name="p1">parameter 1</param>k
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <param name="p4">parameter 4</param>
    /// <param name="p5">parameter 5</param>
    /// <param name="default">default to call if no match found</param>
    /// <returns>result from calling the identified function</returns>
    /// <exception cref="InvalidOperationException">if the same criteria for the key has been defined more than once.</exception>
    public void Call
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5,
        Action<T1, T2, T3, T4, T5> @default
    )
    {
        (SingleOrDefault(p1, p2, p3, p4, p5) ?? @default)(p1, p2, p3, p4, p5);
    }

    /// <summary>
    /// get the index of the entry that meets the state of the input value(s)
    /// </summary>
    /// <param name="p1">parameter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <param name="p4">parameter 4</param>
    /// <param name="p5">parameter 5</param>
    /// <returns>index for the matching element</returns>
    /// <exception cref="InvalidOperationException">Sequence contains no elements.  When no match for the parameter(s) is found.</exception>
    /// <remarks>
    /// the dictionary does not inately provide an index base off-of a key.  this method
    /// approximates the position by using the enumerable.select overload that provides
    /// this.  for static lists i expect that this should always consistent.
    /// </remarks>
    public int IndexOf
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5
    )
    {
        return this
            .Select
            (
                (e, i) => new { Element = e, Index = i }
            )
            .Where(c => c.Element.Key(p1, p2, p3, p4, p5))
            .Select(e => e.Index)
            .Single();
    }

    /// <summary>
    /// calls all actions that match the current state of p1
    /// </summary>
    /// <param name="p1">paramter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <param name="p4">parameter 4</param>
    /// <param name="p5">parameter 5</param>
    public void CallAll
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5
    )
    {
        this.Where(p1, p2, p3, p4, p5).ToList().ForEach(a => a(p1, p2, p3, p4, p5));
    }

    /// <summary>
    /// find all actions that match the current state of p1
    /// </summary>
    /// <param name="p1">parameter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <param name="p4">parameter 4</param>
    /// <param name="p5">parameter 5</param>
    /// <returns></returns>
    public IEnumerable<Action<T1, T2, T3, T4, T5>> Where
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5
    )
    {
        return this.Where(e => e.Key(p1, p2, p3, p4, p5)).Select(v => v.Value);
    }

    /// <summary>
    /// calls all actions that match the current state of p1
    /// </summary>
    /// <param name="p1">paramter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <param name="p4">parameter 4</param>
    /// <param name="p5">parameter 5</param>
    /// <param name="default">default to call if no match found</param>
    /// <param name="default">default action to call if no match found.</param>
    public void CallAll
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5,
        Action<T1, T2, T3, T4, T5> @default
    )
    {
        IEnumerable<Action<T1, T2, T3, T4, T5>> actions = Where(p1, p2, p3, p4, p5);

        if (actions.Any() == false)
        {
            @default(p1, p2, p3, p4, p5);
            return;
        }

        actions.ToList().ForEach(a => a(p1, p2, p3, p4, p5));
    }

    /// <summary>
    /// count the number of conditions that match the state of the parameter(s)
    /// </summary>
    /// <param name="p2">parameter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <param name="p4">parameter 4</param>
    /// <param name="p5">parameter 5</param>
    /// <returns>count of items in the list.</returns>
    public new int Count
    (
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5
    )
    {
        return this.Count(k => k.Key(p1, p2, p3, p4, p5));
    }
}