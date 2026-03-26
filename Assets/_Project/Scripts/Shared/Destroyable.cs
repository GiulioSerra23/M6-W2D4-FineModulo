using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

// Ho fatto questo script che poi assegnerò a tutti i parent dei gameObject che vado a distruggere tramite animation event,
// perchè volevo una hierarchy più pulita mettendo ogni oggetto di tipo: Chest, Enemy, Door ecc... come figlio di un
// gameObject vuoto in scena per ogni tipo, e grazie a questo script io posso richiamare la funzione dell'animation event
// anche da un figlio del gameObject ma mi andrà comunque a distruggere tutto e solo l'oggetto interessato.
