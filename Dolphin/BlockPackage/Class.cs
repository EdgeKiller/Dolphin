namespace Dolphin.BlockPackage
{
    public class Class : Block
    {
        string name;

        public Class(string name) : base(null)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public override void Execute()
        {
            foreach (Block b in GetSubBlocks())
            {
                if (b is Method)
                {
                    Method m = b as Method;
                    if (m.GetName().Equals("main") && m.GetReturnType() == "void" && m.GetParameters().Length == 0)
                        m.Execute();
                }
            }
        }
    }
}
