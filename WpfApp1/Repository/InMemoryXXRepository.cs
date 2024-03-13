using Entity.XX;

namespace Repository
{
    public class InMemoryXXRepository : IXXRepository
    {
        private XXEntity _entity;

        private XXEntity _entitySnapShot;

        public InMemoryXXRepository()
        {
            _entity = new XXEntity();
            _entitySnapShot = new XXEntity();
        }

        public XXEntity Load()
        {
            return _entity.Clone();
        }

        public void Save(XXEntity entity)
        {
            _entity = entity.Clone();
        }

        public void SaveSnapShot(XXEntity entity)
        {
            _entitySnapShot = entity.Clone();
        }

        public XXEntity LoadSnapShot()
        {
            return _entitySnapShot.Clone();
        }
    }
}
