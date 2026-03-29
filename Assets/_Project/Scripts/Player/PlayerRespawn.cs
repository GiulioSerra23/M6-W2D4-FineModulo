
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{   
    private LifeController _lifeController;
    private AttachableMovement _attachableMovement;
    private Mover3D _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover3D>();
        _lifeController = GetComponent<LifeController>();
        _attachableMovement = GetComponent<AttachableMovement>();
    }
     
    public void Respawn()
    {
        if (!CheckPointManager.Instance.HasCheckPoint()) return;

        transform.position = CheckPointManager.Instance.GetCheckPoint();

        UI_LifeFeedBack.Instance.ResetFeedBack(_lifeController.MaxHp);
        _attachableMovement.ForceDetach();
        _mover.ResetMoveAndRotate();
        _mover.ResetSpeedMultiplier();
        _lifeController.RestoreFullHp();        
    }
}
