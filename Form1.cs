namespace CodeGenerator
{
    public partial class Form1 : Form
    {
        CodeGenerator CodeGenerator = new CodeGenerator();

        public Form1()
        {
            InitializeComponent();
        }

        private void generateCode_Click(object sender, EventArgs e)
        {
            if (TemplateDialog.ShowDialog() == DialogResult.OK)
                if (MetadataDialog.ShowDialog() == DialogResult.OK)
                {
                    CodeGenerator.generateCode2(TemplateDialog.FileName, MetadataDialog.FileName, "GeneratedCode.cs");
                    MessageBox.Show("Run `dotnet build` in bin directory with GeneratedCode.cs");
                }
        }
    }
}
