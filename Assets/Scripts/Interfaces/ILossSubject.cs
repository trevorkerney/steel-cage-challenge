public interface ILossSubject
{
    public void AddObserver(ILossObserver observer);
    public void RemoveObserver(ILossObserver observer);
    public void Notify(int player);
}
