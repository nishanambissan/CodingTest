using System;
using System.ComponentModel;
namespace ATMMachine.BusinessLogic
{
    public enum DenominationType
    {
        [Description("1p")]
        OneP,

        [Description("2p")]
        TwoP,

        [Description("5p")]
        FiveP,

        [Description("10p")]
        TenP,

        [Description("20p")]
        TwentyP,

        [Description("50p")]
        FiftyP,

        [Description("1£")]
        OnePound,

        [Description("2£")]
        TwoPound,

        [Description("5£")]
        FivePound,

        [Description("10£")]
        TenPound,

        [Description("20£")]
        TwentyPound,

        [Description("50£")]
        FiftyPound
    }
}
