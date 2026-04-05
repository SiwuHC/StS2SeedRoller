namespace StS2SeedRoller.Data;

public enum RelicRarity
{
    None,
    Starter,
    Common,
    Uncommon,
    Rare,
    Shop,
    Event,
    Ancient
}

public enum RelicPool
{
    Shared,
    Ironclad,
    Silent,
    Defect,
    Necrobinder,
    Regent
}

public enum Character
{
    Ironclad,
    Silent,
    Defect,
    Necrobinder,
    Regent
}

public record RelicInfo(string Name, RelicRarity Rarity, RelicPool Pool);
