using static Adventure.SDK.Library.Classes.Native.PVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Objects
{
    public class Cart : SDK.Library.API.Objects.Cart
    {
        public Cart() : base()
        {
            // Load Cart textures every time
            LoadPVMFile("OBJ_SHAROBJ", (IntPtr)0x38AEB70);
        }
    }
}
