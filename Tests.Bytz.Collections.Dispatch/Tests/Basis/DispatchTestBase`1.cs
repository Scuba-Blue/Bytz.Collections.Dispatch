using Bytz.Collections.Dispatch.Contracts.Basis;

namespace Tests.Bytz.Collections.Dispatch.Tests.Basis;

public abstract class DispatchTestBase<TRule>
where TRule : IDispatchBase
{
    protected abstract TRule Rule { get; }
}