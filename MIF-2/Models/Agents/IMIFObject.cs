using MIF2.Models.MIFMap;

namespace MIF2.Models
{
    interface IMIFObject
    {
        void Move(Vector vector);
        float ApplyDamage(float damage);
    }
}
