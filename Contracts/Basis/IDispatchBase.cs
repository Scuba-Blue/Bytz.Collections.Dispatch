namespace Bytz.Collections.Dispatch.Contracts.Basis;

/// <summary>
/// basis for all dispatch lists.
/// </summary>
public interface IDispatchBase
{
    /// <summary>
    /// occurs on instantiation and regsiters dispatch criteria.
    /// </summary>
    void OnRegister();
}