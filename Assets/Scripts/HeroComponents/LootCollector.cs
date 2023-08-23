using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace HeroComponents
{
    [RequireComponent(typeof(HeroAnimator), typeof(Hero))]
    public class LootCollector : MonoBehaviour
    {
        public event Action <int, int> IsLootCollected;

        private Hero _heroData;
        private HeroAnimator _animator;
        private ColliderTracker _colliderTracker;

        private Transform _backpackTransform;
        private float _lootPosOffset;
        private int _maxLoot;

        private Stack<Loot> _lootStack = new Stack<Loot>();
        private Transform _parentTransform;
        private Transform _houseTransform;
        private bool _canThrow =true;

        private void Awake()
        {
            _animator = GetComponent<HeroAnimator>();
            _heroData = GetComponent<Hero>();
        }
        private IEnumerator Start()
        {
            if (_heroData.BackpackTransform == null)
            {
                throw new Exception("Get backpack");
            }
            
            yield return null;
            
            _colliderTracker = _heroData.CollusionTracker;
            _backpackTransform = _heroData.BackpackTransform;
            _lootPosOffset = _heroData.LootPosOffset;
            _maxLoot = _heroData.MaxLoot;

            InitParentTransform();
            
            IsLootCollected?.Invoke(_lootStack.Count, _maxLoot);

            _colliderTracker.IsLoot += CollectLoot;
            _colliderTracker.IsHouse += (delegate(Transform transform1) { StartCoroutine(RemoveLoot(transform1)); });
        }

        private void InitParentTransform()
        {
            _parentTransform = _backpackTransform.transform;
        }

        private void CollectLoot(GameObject loot, Loot body)
        {
            if (body.State == LootState.Collected)
            {
                return;
            }
            
            if (_lootStack.Count < _maxLoot)
            {
                var lootCollected = PutLootInBackpack(body);
                ChangeParentTo(loot.transform);
                _lootStack.Push(lootCollected);
                IsLootCollected?.Invoke(_lootStack.Count, _maxLoot);
            }
        }

        private void ChangeParentTo(Transform parentLink) => 
            _parentTransform = parentLink;

        private Loot PutLootInBackpack(Loot loot)
        {
            var direction = new Vector3(0, 0 + _lootPosOffset, 0);
            loot.lootRb.isKinematic = true;
            loot.transform.SetParent(_parentTransform);
            return loot.ThrowTo(LootState.Collected, _parentTransform, direction, _lootStack.Count == 0, _maxLoot);
        }

        private IEnumerator RemoveLoot(Transform at)
        {
            if (_lootStack.Count > 0 && _canThrow)
            {
                _canThrow = false;
                _houseTransform = at;
                transform.LookAt(new Vector3(0, 0, at.position.z));
                _animator.PlayThrow();
                ThrowLoot();
                yield return new WaitForSeconds(0.2f);
                _canThrow = true;
            }
        }

        private void ThrowLoot()
        {
            var loot = _lootStack.Pop();
            loot.transform.parent = null;
            var direction = new Vector3(_houseTransform.position.x, _houseTransform.position.y, _houseTransform.position.z + 1);
            loot.ThrowTo(LootState.OnSell, direction);
            if (_lootStack.Count > 0)
            {
                ChangeParentTo(_lootStack.Peek().transform);
            }
            else
            {
                InitParentTransform();
            }

            IsLootCollected?.Invoke(_lootStack.Count, _maxLoot);
        }

    }
}