using System.Collections.Generic;
using UnityEngine;

namespace WeatLogic
{
    public class WeatField: MonoBehaviour
    {
        [SerializeField] private GameObject WeatObject;
        [SerializeField] private float _sizeX;
        [SerializeField] private float _sizeZ;
        [SerializeField] private float _cellSize;
        private List<Weat> _weatList = new List<Weat>();

        private void Start()
        {
            Spawn();
        }

        public void RemoveWeat(Weat weat)
        {
            _weatList.Remove(weat);
            if (_weatList == null || _weatList.Count == 0)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            for(var x = 0; x< _sizeX; x++)
            {
                for (int z = 0; z < _sizeZ; z++)
                {
                    var position = new Vector3(transform.position.x + x*_cellSize, transform.position.y, transform.position.z + z*_cellSize);
                    var weat = Instantiate(WeatObject, position, Quaternion.identity, transform);
                    _weatList.Add(weat.GetComponent<Weat>());
                }
            }
        }
    }
}