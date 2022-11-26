using UnityEngine;

public class ManagerUpdateComponent : MonoBehaviour
{
    private ManagerUpdate _managerUpdate;

    public void Setup(ManagerUpdate managerUpdate) => _managerUpdate = managerUpdate;

    private void Update() => _managerUpdate.Tick();

    private void FixedUpdate() => _managerUpdate.TickFixed();

    private void LateUpdate() => _managerUpdate.TickLate();
}