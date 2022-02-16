namespace Interfaces.SoundsTypes
{
    public interface ISoundVisitor
    {
        void Visit(IChestSound sound);
        void Visit(IBarrelSound sound);
        void Visit(IBodySound sound);
    }
}