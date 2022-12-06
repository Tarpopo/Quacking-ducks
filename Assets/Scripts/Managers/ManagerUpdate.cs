using System.Collections.Generic;
using DefaultNamespace;

public class ManagerUpdate : ManagerBase
{
    private readonly List<ITick> _ticks = new List<ITick>();
    private readonly List<ITickFixed> _ticksFixes = new List<ITickFixed>();
    private readonly List<ITickLate> _ticksLate = new List<ITickLate>();

    public static void AddTo(object updatable)
    {
        var mngUpdate = Toolbox.Get<ManagerUpdate>();

        if (updatable is ITick tick) mngUpdate._ticks.Add(tick);

        if (updatable is ITickFixed tickFixed) mngUpdate._ticksFixes.Add(tickFixed);

        if (updatable is ITickLate tickLate) mngUpdate._ticksLate.Add(tickLate);
    }

    public static void RemoveFrom(object updatable)
    {
        var mngUpdate = Toolbox.Get<ManagerUpdate>();
        if (updatable is ITick tick) mngUpdate._ticks.Remove(tick);

        if (updatable is ITickFixed tickFixed) mngUpdate._ticksFixes.Remove(tickFixed);

        if (updatable is ITickLate tickLate) mngUpdate._ticksLate.Remove(tickLate);
    }

    private void Update()
    {
        for (int i = 0; i < _ticks.Count; i++) _ticks[i].Tick();
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < _ticksFixes.Count; i++) _ticksFixes[i].TickFixed();
    }

    public void LateUpdate()
    {
        for (int i = 0; i < _ticksLate.Count; i++) _ticksLate[i].TickLate();
    }

    public override void ClearScene()
    {
        _ticks.Clear();
        _ticksFixes.Clear();
        _ticksLate.Clear();
    }
}