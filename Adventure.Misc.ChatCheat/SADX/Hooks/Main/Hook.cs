using Reloaded.Hooks.Definitions;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Hooks.Main
{
    public class Hook<T>
    {
        public static long Address { get; set; }
        public static T Function { get; set; }
        public static IHook<T> HookFunction;

        public bool IsEnabled
        {
            get => HookFunction.IsHookEnabled;
            set
            {
                if (value)
                    HookFunction.Enable();
                else
                    HookFunction.Disable();
            }
        }
    }
}
