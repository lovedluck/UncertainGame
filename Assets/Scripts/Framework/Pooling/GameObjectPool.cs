using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Framework.Pooling
{
    public class GameObjectPool : ObjectPool<GameObject>, IDisposable
    {
        public void Dispose()
        {
            
        }

        public override GameObject Get()
        {
            throw new NotImplementedException();
        }

        public override void Init()
        {
            throw new NotImplementedException();
        }

        public override void Put(GameObject obj)
        {
            throw new NotImplementedException();
        }

        protected override GameObject Create()
        {
            throw new NotImplementedException();
        }

        protected override void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}
