using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMeleAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /**
     * Атака мили оружием ГГ. Проссчитываем пересечение коллайдера меча с коллайдерами врагов 
     **/
    public static void Action(BoxCollider2D weapon, string layerMask, int damage, bool allTargets)
    {
        ContactFilter2D overlapFilter = new ContactFilter2D();
        overlapFilter.SetLayerMask(LayerMask.GetMask(layerMask));
        Collider2D[] resultArray = new Collider2D[10];

        int result = weapon.OverlapCollider(overlapFilter, resultArray);

        if (result > 0 && !allTargets) {
            GameObject obj = resultArray[0].gameObject;

            if (obj.GetComponent<Enemy>()) {
                obj.GetComponent<Enemy>().hits -= damage;
            }
            return;
        } else if (allTargets) {
            foreach (Collider2D hit in resultArray) {
                if (hit.gameObject.GetComponent<Enemy>()) {
                    hit.gameObject.GetComponent<Enemy>().hits -= damage;
                    Debug.LogError(hit.gameObject.GetComponent<Enemy>().hits);
                }
            }
        }
    }
}
