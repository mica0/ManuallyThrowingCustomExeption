using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManuallyThrowingCustomExeption
{
    public partial class Form1 : Form
    {
        class NumberFormatException : Exception
        { 
            public NumberFormatException(string quantity) : base(quantity)
            { }
        }
        class StringFormatException : Exception
        {
            public StringFormatException(string name) : base(name) 
            { }
        }
        class CurrenceyFormatException : Exception
        { 
            public CurrenceyFormatException(string price) : base(price) 
            { }
        }

        private int _Quantity;
        private double _SellingPrice;
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        BindingSource showProductList;

        public Form1()
        {
            showProductList = new BindingSource();
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ListofProductCategory = new string[]
                {
                    "Beveragers",
                    "Bread / Bakery", 
                    "Canned / Jarred Goods",
                    "Dairy", 
                    "Frozen Goods",
                    "Meat",
                    "Personal Care",
                    "Others."
                };
            foreach (string prodcategory in ListofProductCategory)
            {
                cbCategory.Items.Add(prodcategory);
            }
        }

        public string Product_Name(string name)
        {
            try
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    throw new StringFormatException(name);
                }
            }
            catch (StringFormatException sf) 
            {
                MessageBox.Show("String format input is product name." + sf.Message);
            }
                return name;
        }
        public int Quantity(string qty)
        {
            try
            {

                if (!Regex.IsMatch(qty, @"^[0-9]"))
                {
                    throw new NumberFormatException(qty);
                }
            }
            catch (NumberFormatException nf)
            {
                MessageBox.Show("Number format input in quantity." + nf.Message);
            }
                return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            try
            {
                if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                {
                    throw new CurrenceyFormatException(price);
                }
            }
            catch (CurrenceyFormatException cf)
            {
                MessageBox.Show("Currency format input is price." + cf.Message);
            }
                return Convert.ToDouble(price);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            _ProductName = Product_Name(txtProductName.Text);
            _Category = cbCategory.Text;
            _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
            _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
            _Description = richTxtDescription.Text;
            _Quantity = Quantity(txtQuantity.Text);
            _SellingPrice = SellingPrice(txtSellPrice.Text);
            showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
            _ExpDate, _SellingPrice, _Quantity, _Description));
            gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridViewProductList.DataSource = showProductList;

        }
    }
}
        