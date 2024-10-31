/// <summary>
/// 해킹 할때 하는 이벤트들 연결해
/// </summary>
public interface IHackingEnter
{
    public void HackingEnter();
}

/// <summary>
/// 해킹 할때 하는 이벤트들 구독 취소해
/// </summary>
public interface IHackingExit
{
    public void HackingExit();
}
