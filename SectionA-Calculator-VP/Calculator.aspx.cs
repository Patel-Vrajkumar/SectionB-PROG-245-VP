using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SectionA_Calculator_VP
{
    public partial class Calculator : Page
    {
        // Use at most two state variables (ViewState)
        const string VSAccum = "Accum";   // running total
        const string VSOp = "Op";         // last/pending operator

        protected void Page_Load(object s, EventArgs e)
        {
            if (!IsPostBack)
            { 
                lblEquation.Text = "0"; 
                ViewState[VSAccum] = 0d; 
                ViewState[VSOp] = string.Empty;
                drpTheme.Items.Add("Water");
                drpTheme.Items.Add("Fire");
                drpTheme.Items.Add("Mountains");
                drpTheme.Items.Add("Galaxy");
            }
            
            // Reapply theme on every page load (including postbacks)
            ApplyStoredTheme();
        }
        
        private void ApplyStoredTheme()
        {
            // Read cookie first (persisted); fall back to Session
            string selectedTheme = null;
            var cookie = Request.Cookies["SelectedTheme"];
            if (cookie != null && !string.IsNullOrWhiteSpace(cookie.Value))
                selectedTheme = cookie.Value;
            else
                selectedTheme = Session["SelectedTheme"] as string;

            if (string.IsNullOrEmpty(selectedTheme)) return;

            // Apply CSS link based on the selected theme
            string cssFile = string.Empty;
            string imageFilter = string.Empty;
            string imageUrl = string.Empty;

            switch (selectedTheme)
            {
                case "Water":
                    cssFile = "~/App_Themes/DropDownTheme/Water.css";
                    imageFilter = "sepia(100%) saturate(300%) hue-rotate(180deg) brightness(0.8)";
                    imageUrl = "~/App_Themes/DropDownTheme/Water_Logo.jpg";
                    break;
                case "Fire":
                    cssFile = "~/App_Themes/DropDownTheme/Fire.css";
                    imageFilter = "sepia(100%) saturate(400%) hue-rotate(-10deg) brightness(1.1)";
                    imageUrl = "~/App_Themes/DropDownTheme/Fire_Logo.jpg";
                    break;
                case "Earth":
                    cssFile = "~/App_Themes/DropDownTheme/Earth.css";
                    imageFilter = "sepia(100%) saturate(200%) hue-rotate(10deg) brightness(0.7)";
                    imageUrl = "~/App_Themes/DropDownTheme/Mountains_Logo.jpg";
                    break;
                case "Alien Planet":
                    cssFile = "~/App_Themes/DropDownTheme/Alien Planet.css";
                    imageFilter = "brightness(0.3) contrast(1.2)";
                    imageUrl = "~/App_Themes/DropDownTheme/Planet_Logo.jpg"; // avoid spaces in filenames if possible
                    break;
            }

            // Add the CSS file dynamically (only if not already present)
            if (!string.IsNullOrEmpty(cssFile))
            {
                var link = new System.Web.UI.HtmlControls.HtmlLink();
                link.Href = ResolveUrl(cssFile);
                link.Attributes["rel"] = "stylesheet";
                link.Attributes["type"] = "text/css";
                Page.Header.Controls.Add(link);
            }

            // Apply filter to Image1
            if (!string.IsNullOrEmpty(imageFilter))
            {
                Image1.Style["filter"] = imageFilter;
            }

            // Set Image1 source
            if (!string.IsNullOrEmpty(imageUrl))
            {
                Image1.ImageUrl = ResolveUrl(imageUrl);
                Image1.AlternateText = selectedTheme;
            }

            // Optional: Store filter in ViewState to persist
            ViewState["ImageFilter"] = imageFilter;
        }
        
        void EvalRun()
        {
            try
            { 
                lblResult.Text = new System.Data.DataTable().Compute(lblEquation.Text, null).ToString(); 
            }
            catch { }
        }
        
        protected void Number_CLick(object s, EventArgs e)
        {
            var t = ((Button)s).Text;
            lblEquation.Text = lblEquation.Text == "0" ? t : lblEquation.Text + t;
        }
        
        protected void Operator_Click(object s, EventArgs e)
        {
            var eq = lblEquation.Text ?? "";
            
            // Don't add operator if equation already ends with an operator
            if (EndsOp(eq)) return;
            
            // Evaluate running total if there's a valid expression
            if (!string.IsNullOrWhiteSpace(eq) && !EndsOp(eq)) EvalRun();
            
            // Handle × and ÷ symbols, convert to * and / for calculation
            var opText = ((Button)s).Text;
            var calcOp = opText == "×" ? "*" : opText == "÷" ? "/" : opText;
            
            double acc; 
            if (double.TryParse(lblResult.Text, out acc)) 
                ViewState[VSAccum] = acc; 
            ViewState[VSOp] = calcOp;

            lblEquation.Text = (eq == "" ? "0" : eq) + " " + calcOp + " ";
        }
        
        protected void Decimal_Click(object s, EventArgs e)
        {
            var eq = lblEquation.Text ?? ""; var last = LastToken(eq);
            if (eq == "0" || EndsOp(eq) || last == "")
            { 
                lblEquation.Text = eq == "0" ? "0." : eq + "0."; return; 
            }
            if (!last.Contains(".")) 
                lblEquation.Text += ".";
        }
        
        protected void EqualTo_Click(object s, EventArgs e)
        {
            try 
            { 
                lblResult.Text = new System.Data.DataTable().Compute(lblEquation.Text, null).ToString(); 
            }
            catch 
            {
                lblResult.Text = "Error"; 
            }
            ViewState[VSOp] = string.Empty;
        }
        
        protected void AllClear_Click(object s, EventArgs e)
        {
            lblEquation.Text = "0";
            lblResult.Text = "";
            ViewState[VSAccum] = 0d;
            ViewState[VSOp] = string.Empty;
        }
        
        protected void Clear_Click(object s, EventArgs e)
        {
            var t = lblEquation.Text;
            lblEquation.Text = t.Length > 1 ? t.Substring(0, t.Length - 1) : "0";
        }
        
        bool EndsOp(string x)
        {
            x = (x ?? "").TrimEnd(); 
            return x.EndsWith("+") || x.EndsWith("-") || x.EndsWith("*") || x.EndsWith("/");
        }
        
        string LastToken(string x)
        {
            if (string.IsNullOrWhiteSpace(x)) return "";
            var a = x.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var k = a[a.Length - 1]; 
            return (k == "+" || k == "-" || k == "*" || k == "/") ? "" : k;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnLoadTheme_Click(object sender, EventArgs e)
        {
            // Get the selected theme from the dropdown
            string selectedTheme = drpTheme.SelectedItem?.Text;
            
            if (string.IsNullOrEmpty(selectedTheme)) return;

            // Store the selected theme in Session to persist across postback
            Session["SelectedTheme"] = selectedTheme;

            // The theme will be applied by ApplyStoredTheme() on the next page load
            // Force immediate application by calling it directly
            ApplyStoredTheme();
        }
    }
}