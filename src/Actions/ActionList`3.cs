using Bytz.Collections.Dispatch.Abstractions.Contracts;

namespace Bytz.Collections.Dispatch.Actions;

/// <summary>
/// function list taking 3 parameters.
/// </summary>
/// <typeparam name="T1">parameter 1</typeparam>
/// <typeparam name="T2">parameter 2</typeparam>
/// <typeparam name="T3">parameter 3</typeparam>
/// <typeparam name="TReturn">type of the return from the function</typeparam>
public class ActionList<T1, T2, T3>
: Dictionary<Func<T1, T2, T3, bool>, Action<T1, T2, T3>>, IActionDispatch
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
    /// <returns>related element for parameter</returns>
    /// <exception cref="InvalidOperationException">if the same criteria for the key has been defined more than once.</exception>
    public Action<T1, T2, T3> Single
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
    /// <returns>related element for parameter</returns>
    /// <exception cref="InvalidOperationException">if the same criteria for the key has been defined more than once.</exception>
    public Action<T1, T2, T3> SingleOrDefault
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
    public void Call
    (
        T1 p1,
        T2 p2,
        T3 p3
    )
    {
        Single(p1, p2, p3)(p1, p2, p3);
    }

    /// <summary>
    /// call the identified method with the specified input.
    /// </summary>
    /// <param name="p1">parameter 1</param>k
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <param name="default">default to call if no match found</param>
    /// <returns>result from calling the identified function</returns>
    /// <exception cref="InvalidOperationException">if the same criteria for the key has been defined more than once.</exception>
    public void Call
    (
        T1 p1,
        T2 p2,
        T3 p3,
        Action<T1, T2, T3> @default
    )
    {
        (SingleOrDefault(p1, p2, p3) ?? @default)(p1, p2, p3);
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
    /// the dictionary does not inately provide an index base off-of a key.  this method
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
    /// calls all actions that match the current state of p1
    /// </summary>
    /// <param name="p1">paramter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    public void CallAll
    (
        T1 p1,
        T2 p2,
        T3 p3
    )
    {
        this.Where(p1, p2, p3).ToList().ForEach(a => a(p1, p2, p3));
    }

    /// <summary>
    /// find all actions that match the current state of p1
    /// </summary>
    /// <param name="p1">parameter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <returns></returns>
    public IEnumerable<Action<T1, T2, T3>> Where
    (
        T1 p1,
        T2 p2,
        T3 p3
    )
    {
        return this.Where(e => e.Key(p1, p2, p3)).Select(v => v.Value);
    }

    /// <summary>
    /// calls all actions that match the current state of p1
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
        Action<T1, T2, T3> @default
    )
    {
        IEnumerable<Action<T1, T2, T3>> actions = Where(p1, p2, p3);

        if (actions.Any() == false)
        {
            @default(p1, p2, p3);
            return;
        }

        actions.ToList().ForEach(a => a(p1, p2, p3));
    }

    /// <summary>
    /// count the number of conditions that match the state of the parameter(s)
    /// </summary>
    /// <param name="p2">parameter 1</param>
    /// <param name="p2">parameter 2</param>
    /// <param name="p3">parameter 3</param>
    /// <returns>count of items in the list.</returns>
    public new int Count
    (
        T1 p1,
        T2 p2,
        T3 p3
    )
    {
        return this.Count(k => k.Key(p1, p2, p3));
    }
}