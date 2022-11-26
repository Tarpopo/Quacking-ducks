using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ManagerUpdate : ManagerBase, ISceneChanged
{
    private List<ITick> ticks = new List<ITick>();
    private List<ITickFixed> ticksFixes = new List<ITickFixed>();
    private List<ITickLate> ticksLate = new List<ITickLate>();

    public static void AddTo(object updateble)
    {
        //Debug.Log("mngUpdate");
        var mngUpdate = Toolbox.Get<ManagerUpdate>();

        if (updateble is ITick)
            mngUpdate.ticks.Add(updateble as ITick);

        if (updateble is ITickFixed)
            mngUpdate.ticksFixes.Add(updateble as ITickFixed);

        if (updateble is ITickLate)
            mngUpdate.ticksLate.Add(updateble as ITickLate);
    }

    public static void RemoveFrom(object updateble)
    {
        var mngUpdate = Toolbox.Get<ManagerUpdate>();
        if (updateble is ITick)
            mngUpdate.ticks.Remove(updateble as ITick);

        if (updateble is ITickFixed)
            mngUpdate.ticksFixes.Remove(updateble as ITickFixed);

        if (updateble is ITickLate)
            mngUpdate.ticksLate.Remove(updateble as ITickLate);
    }

    public void Tick()
    {
        for (int i = 0; i < ticks.Count; i++)
        {
            ticks[i].Tick();
        }
    }

    public void TickFixed()
    {
        for (int i = 0; i < ticksFixes.Count; i++)
        {
            ticksFixes[i].TickFixed();
        }
    }

    public void TickLate()
    {
        for (int i = 0; i < ticksLate.Count; i++)
        {
            ticksLate[i].TickLate();
        }
    }

    public override void ClearScene()
    {
        ticks.Clear();
        ticksFixes.Clear();
        ticksLate.Clear();
    }

    public void OnChangeScene()
    {
        GameObject.Find("[Setup]")?.AddComponent<ManagerUpdateComponent>().Setup(this);
    }
}