using UnityEngine;

public class Define 
{

    public enum EState
    {
        Idle,
        Move,
        Attack,
        Die,
    }

    public static class CharacterAnimationName
    {
        public const string IDLE = "1_idle";
        public const string MOVE = "2_move";
        public const string ATTACK = "3_attack";
        public const string ACTIVE_SKILL = "4_skill";
        public const string Special_SKILL = "4_skill";
        public const string AXE = "5_axe";
        public const string MINE = "6_mine";
        public const string DIE = "7_die";
        public const string DIE_END = "7_die_end";
        public const string DIE_START = "7_die_start";
        public const string PORTAL_END = "8_portal_end";
        public const string PORTAL_START = "8_portal_start";
    }
}
