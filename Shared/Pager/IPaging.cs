namespace Shared.Pager
{
    public interface IPaging
    {
        int Skip { get; }
        int Take { get; }
    }
}