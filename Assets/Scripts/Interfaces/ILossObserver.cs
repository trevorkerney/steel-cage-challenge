public interface ILossObserver
{
    public void Acknowledge(string winner, string loser);
}
