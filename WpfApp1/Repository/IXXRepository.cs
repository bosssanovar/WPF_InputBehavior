using Entity.XX;

namespace Repository
{
    public interface IXXRepository
    {
        void Save(XXEntity entity);
        XXEntity Load();

        void SaveSnapShot(XXEntity entity);
        XXEntity LoadSnapShot();
    }
}
