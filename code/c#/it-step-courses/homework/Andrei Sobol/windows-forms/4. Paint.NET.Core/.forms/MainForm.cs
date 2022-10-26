namespace Paint.NET.Core
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var graphics = e.Graphics;
        }


        private void OnMainFormPaint(object? sender, PaintEventArgs e)
        {

        }
    }
}