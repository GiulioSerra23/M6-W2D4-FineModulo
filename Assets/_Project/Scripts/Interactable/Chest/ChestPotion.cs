using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPotion : Chest
{
    [SerializeField] private RandomDropTable _dropTable;

    protected override void Drop()
    {
        _dropTable.RandomDrop(_newPos);
    }
}
