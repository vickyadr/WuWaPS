using GameServer.Network;

namespace GameServer.Controllers.Gacha;
internal class PoolInfo : Controller
{
    public Dictionary<string, GachaPool> _bannerRoles = new();
    public PoolInfo(PlayerSession session) : base(session)
    {
        _bannerRoles.Add("Beginner", new GachaPool(1, "Beginner", false, 60, 10, 80, [93.2f, 6f, 0.8f]));
        _bannerRoles.Add("Standard", new GachaPool(1, "Standard", false, 60, 10, 80, [93.2f, 6f, 0.8f]));
        _bannerRoles.Add("Limited1", new GachaPool(1, "Jiyan", false, 60, 10, 80, [93.2f, 6f, 0.8f]));
        _bannerRoles.Add("Limited2", new GachaPool(1, "Yinlin", false, 60, 10, 80, [93.2f, 6f, 0.8f]));
        _bannerRoles.Add("WeaponL", new GachaPool(1, "Verdant Summit", false, 60, 10, 80, [93.2f, 6f, 0.8f]));
        _bannerRoles.Add("WeaponS", new GachaPool(1, "Stringmaster", false, 60, 10, 80, [93.2f, 6f, 0.8f]));
    }
}