using GoogleSheet.Core.Type;

namespace EnumManager
{
    public enum Achive { UnlockPoato, UnlockBean }
    public enum Equipment { Head, Chest, Leg, Hand, Shoulder, Weapon };
}

[UGS(typeof(SkillType))]
public enum SkillType
{
    기본, 패시브, 액티브, 진화
}

[UGS(typeof(Tema))]
public enum Tema 
{
    발탄, 비아키스
}