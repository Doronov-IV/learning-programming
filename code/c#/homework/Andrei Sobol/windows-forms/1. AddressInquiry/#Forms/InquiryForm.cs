using System.Net;
using System.Text.RegularExpressions;
using AddressInquiry.Service;



namespace AddressInquiry.Forms
{
    /// <summary>
    /// Program's main form;
    /// <br />
    /// Основная форма программы;
    /// </summary>
    public partial class InquiryForm : Form
    {

        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public InquiryForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// On load;
        /// <br />
        /// При загрузке;
        /// </summary>
        private void OnInquiryFormLoad(object sender, EventArgs e)
        {
            ExampleRadioButton1.Text = IpAddressProvider.Generate();

            do ExampleRadioButton2.Text = IpAddressProvider.Generate();
            while (ExampleRadioButton2.Text == ExampleRadioButton1.Text);

            do ExampleRadioButton3.Text = IpAddressProvider.Generate();
            while (ExampleRadioButton3.Text == ExampleRadioButton2.Text && ExampleRadioButton3.Text == ExampleRadioButton1.Text);
        }


        /// <summary>
        /// KEEP OUT;
        /// <br />
        /// НЕ ВЛЕЗАЙ УБЬЁТ;
        /// </summary>
        private void OnAddressBoxTextChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Submit the address;
        /// <br />
        /// Принять адрес;
        /// </summary>
        private void OnMainButtonClick(object sender, EventArgs e)
        {
            bool bResult = AddressInspector.Inspect(AddressTextBox.Text);
            if (bResult == true)
            {
                MainPictureBox.ImageLocation = "../../../Resources/success.png";
                TryShowLocation();
            }
            else
            {
                MainPictureBox.ImageLocation = "../../../Resources/fail.png";
                CountryLabel.Text = "Incorrect!";
                CountryLabel.Visible = true;
            }
        }


        /// <summary>
        /// Tries to unveil the adresses' location;
        /// <br />
        /// Увидел у Жени - понравилось, захотел сам попробовать;
        /// </summary>
        private void TryShowLocation()
        {
            try
            {
                string sCountryJSON = "\"country\":\"(.*?)\"";
                string sCityJSON = "\"city\":\"(.*?)\"";

                var sLine = new WebClient().DownloadString($"https://api.2ip.ua/geo.json?ip={AddressTextBox.Text}");

                CountryLabel.Text = "Country: " + Regex.Match(sLine, sCountryJSON).Groups[1].Value;
                CityLabel.Text = "City: " + Regex.Match(sLine, sCityJSON).Groups[1].Value;

                CountryLabel.Visible = true;
                CityLabel.Visible = true;
            }
            catch (WebException ex)
            {
                CountryLabel.Text = "Success!";
                CountryLabel.Visible = true;
            }
        }


        /// <summary>
        /// KEEP OUT;
        /// <br />
        /// НЕ ВЛЕЗАЙ УБЬЁТ;
        /// </summary>
        private void ImagePanel_Paint(object sender, PaintEventArgs e)
        {

        }


        /// <summary>
        /// 1st Radio button checked;
        /// <br />
        /// 1-я радио кнопка;
        /// </summary>
        private void OnExampleRadioButton1Checked(object sender, EventArgs e)
        {
            AddressTextBox.Text = ExampleRadioButton1.Text;
        }


        /// <summary>
        /// 2nd Radio button checked;
        /// <br />
        /// 2-я радио кнопка;
        /// </summary>
        private void OnExampleRadioButton2Checked(object sender, EventArgs e)
        {
            AddressTextBox.Text = ExampleRadioButton2.Text;
        }

        
        /// <summary>
        /// 3rd Radio button checked;
        /// <br />
        /// 3-я радио кнопка;
        /// </summary>
        private void OnExampleRadioButton3Checked(object sender, EventArgs e)
        {
            AddressTextBox.Text = ExampleRadioButton3.Text;
        }


        /// <summary>
        /// KEEP OUT;
        /// <br />
        /// НЕ ВЛЕЗАЙ УБЬЁТ;
        /// </summary>
        private void AddressPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}