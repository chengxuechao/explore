using UnityEngine;

/***
 * BatchingRenderer.cs 
 * 
 * @author abaojin
 */
namespace GameCore
{
    public class BatchingRenderer : MonoBehaviour
    {
        void Awake()
        {
            StaticBatchingUtility.Combine(gameObject);
        }

        void OnEnable()
        {
            Destroy(this);
        }
    }
}
