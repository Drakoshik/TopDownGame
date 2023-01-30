using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameArchitecture.Pool
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly bool _autoExpand;
        private readonly Transform _container;
        private List<T> _pool;

        public ObjectPool(T prefab, int count, bool autoExpand)
        {
            this._prefab = prefab;
            var container = new GameObject();
            _container = container.transform;
            container.name = prefab.name;
            this._autoExpand = autoExpand;
            this.CreatePool(count);
        }

        private void CreatePool(int count)
        {
            this._pool = new List<T>();
            for (var i = 0; i < count; i++)
            {
                this.CreateObject();
            }
        }

        private T CreateObject(bool isActive = false)
        {
            var createdObject = Object.Instantiate(this._prefab, this._container);
            createdObject.gameObject.SetActive(isActive);
            this._pool.Add(createdObject);
            return createdObject;
        }


        public T GetFreeElement()
        {
            foreach (var obj in _pool.Where(obj =>
                         !obj.gameObject.activeInHierarchy))
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
            
        
            if (this._autoExpand)
                return this.CreateObject(true);
        
            throw new Exception("No free elements");
        }

        public void HideAllElements()
        {
            foreach (var obj in _pool)
            {
                obj.gameObject.SetActive(false);
            }
        }

        public List<T> GetAllElements()
        {
            return _pool;
        }
    

    }
}
