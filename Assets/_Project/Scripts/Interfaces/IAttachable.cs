
using UnityEngine;

public interface IAttachable 
{
    bool RequiresInputToAttach { get; }

    public void OnAttach(AttachableMovement player);
    public void HandleAttachedInput(AttachableMovement player);
    public void OnDetach(AttachableMovement player, bool isForced);    
}
