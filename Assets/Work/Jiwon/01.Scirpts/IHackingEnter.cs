/// <summary>
/// 해킹 할때 하는 이벤트들 연결해
/// </summary>
public interface IHackingEnter
{
    public abstract void HackingEnter(Player player);
}

/// <summary>
/// 해킹 할때 하는 이벤트들 구독 취소해
/// </summary>
public interface IHackingExit
{
    public abstract void HackingExit();
}
