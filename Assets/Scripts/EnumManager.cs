using GoogleSheet.Core.Type;

namespace EnumManager
{
    public enum Achive { UnlockPoato, UnlockBean }
    
}

[UGS(typeof(SkillType))]
public enum SkillType
{
    passive, basic, engraving, evolution
}

[UGS(typeof(Tema))]
public enum Tema 
{
    발탄, 비아키스
}