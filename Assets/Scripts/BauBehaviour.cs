using UnityEngine;

public class BauBehaviour : MonoBehaviour
{
    public void addBauPoints(){
        ScoreManager.Instance.AddPoints(1);
    }
}
