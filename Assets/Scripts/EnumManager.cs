using GoogleSheet.Core.Type;

namespace EnumManager
{
    public enum Achive { UnlockPoato, UnlockBean }
    public enum Equipment { Head, Chest, Leg, Hand, Shoulder, Weapon };
    public enum ItemT { Bomb, Gold, Heal};

    public enum Sfx { Dead, Hit, LevelUp = 3, Lose, Mode, Select, Win, Enter, Refuse}


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