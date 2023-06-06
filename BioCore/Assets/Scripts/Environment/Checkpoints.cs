using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
        [SerializeField] Transform[] _tpPoints;
        [SerializeField] GameObject _player;
        private Transform _currentPoint;

        public void TeleportPlayer()
        {
            float distance = float.MaxValue;
            foreach (Transform point in _tpPoints)
            {
                Debug.Log(point.gameObject.name);
                if(point.GetComponent<PointTrigger>().isActive)
                {
                    float temp = Mathf.Abs(Mathf.Pow((_player.transform.position.x - point.position.x), 2) + Mathf.Pow((_player.transform.position.y - point.position.y),2));
                    if(temp < distance)
                    {
                        distance = temp;
                        _currentPoint = point;
                    }
                }
                
            }
            if(_currentPoint != null)
            {
                _player.transform.position = new Vector3(_currentPoint.position.x, _currentPoint.position.y, _player.transform.position.z);
                distance = float.MaxValue;
            }
        }
}
