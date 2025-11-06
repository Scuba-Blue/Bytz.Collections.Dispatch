using Bytz.Collections.Dispatch.Contracts;

namespace Bytz.Collections.Dispatch.Actions;

/// <summary>
/// action list taking 1 parameter.
/// </summary>
/// <typeparam name="T1">generic parameter 1</typeparam>
public class ActionList<T1>
: Dictionary<Func<T1, bool>, Action<T1>>
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
    /// <returns>related element or null</returns>
    /// <exception cref="InvalidOperationException">if the same criteria for the key has been defined more than once.</exception>
    public Action<T1> Single
    (
        T1 p1
    )
    {
        return this.Where(e => e.Key(p1)).Select(v => v.Value).Single();
    }

    /// <summary>
    /// get a single entry based on the input or null if not found.
    /// </summary>
    /// <param name="p1">generic parameter 1</param>
    /// <returns>related element or null</returns>
    /// <exception cref="InvalidOperationException">if the same criteria for the key has been defined more than once.</exception>
    public Action<T1> SingleOrDefault
    (
        T1 p1
    )
    {
        return this.Where(e => e.Key(p1)).Select(v => v.Value).SingleOrDefault();
    }

    /// <summary>
    /// call the identified method with the specified input.
    /// </summary>
    /// <param name="p1">generic parameter 1</param>
    /// <returns>result from calling the identified function</returns>
    /// <exception cref="InvalidOperationException">if the criteria produces no results for the input</exception>>
    public void Call
    (
        T1 p1
    )
    {
        Single(p1)(p1);
    }

    /// <summary>
    /// call the identified method with the specified input.
    /// </summary>
    /// <param name="p1">generic parameter 1</param>
    /// <param name="default">default to action to call when no match is found</param>
    /// <returns>result from calling the identified function</returns>
    /// <exception cref="InvalidOperationException">if the same criteria for the key has been defined more than once.</exception>
    public void Call
    (
        T1 p1,
        Action<T1> @default
    )
    {
        (SingleOrDefault(p1) ?? @default)(p1);
    }

    /// <summary>
    /// get the index of the entry that meets the state of the input value(s)
    /// </summary>
    /// <param name="p1">generic parameter 1</param>
    /// <returns>index for the matching element</returns>
    /// <exception cref="InvalidOperationException">Sequence contains no elements.  When no match for the generic parameter(s) is found.</exception>
    /// <remarks>
    /// the dictionary does not inherently provide an index base off-of a key.  this method
    /// approximates the position by using the enumerable.select overload that provides
    /// this.  once registered through the OnRegister or an initializer, this should always be unchanging.
    /// </remarks>
    public int IndexOf
    (
        T1 p1
    )
    {
        return this
            .Select
            (
                (e, i) => new { Element = e, Index = i }
            )
            .Where(c => c.Element.Key(p1))
            .Select(e => e.Index)
            .Single();
    }

    /// <summary>
    /// calls all actions that match the current state of the generic parameter(s)
    /// </summary>
    /// <param name="p1">generic parameter 1</param>
    public void CallAll
    (
        T1 p1
    )
    {
        this.Where(p1).ToList().ForEach(a => a(p1));
    }

    /// <summary>
    /// find all actions that match the current state of p1
    /// </summary>
    /// <param name="p1"></param>
    /// <returns></returns>
    public IEnumerable<Action<T1>> Where
    (
        T1 p1
    )
    {
        return this.Where(e => e.Key(p1)).Select(v => v.Value);
    }

    /// <summary>
    /// calls all actions that match the current state of the generic parameter(s)
    /// </summary>
    /// <param name="p1">generic parameter 1</param>
    /// <param name="default">default action to call when no match is found.</param>
    public void CallAll
    (
        T1 p1,
        Action<T1> @default
    )
    {
        IEnumerable<Action<T1>> actions = Where(p1);

        if (!actions.Any())
        {
            @default(p1);
            return;
        }

        actions.ToList().ForEach(a => a(p1));
    }

    /// <summary>
    /// count the number of conditions that match the state of the generic parameter(s)
    /// </summary>
    /// <param name="p1"></param>
    /// <returns>count of items that match the state of the generic parameter(s).</returns>
    public int CountOf
    (
        T1 p1
    )
    {
        return this.Count(k => k.Key(p1));
    }
}