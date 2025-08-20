using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABB.Robotics.Controllers;
using RobotStudio.Services.RobApi.RobApi1;
using ABB.Robotics.Controllers;
namespace Latte_Art_GoFa
{
    public partial class DrinkSelectionForm_User : Form
    {
        private Controller robotController;

        public DrinkSelectionForm_User(Controller controller)
        {
            InitializeComponent();
            robotController = controller;
        }

        private void buttonLatteartcoffe_Click(object sender, EventArgs e)
        {
            string selectedDrink = "Latte Art";
            DrinkPreferenceForm preferenceForm = new DrinkPreferenceForm(selectedDrink, robotController);
            preferenceForm.Show();
            this.Hide();
        }

        private void buttonMilkCoffe_Click(object sender, EventArgs e)
        {
            string selectedDrink = "Milk Coffee";
            DrinkPreferenceForm preferenceForm = new DrinkPreferenceForm(selectedDrink, robotController);
            preferenceForm.Show();
            this.Hide();
        }

        private void buttonCoffe_Click(object sender, EventArgs e)
        {
            string selectedDrink = "Black Coffee";
            DrinkPreferenceForm preferenceForm = new DrinkPreferenceForm(selectedDrink, robotController);
            preferenceForm.Show();
            this.Hide();
        }

        private void buttonCoffeewithmilk_Click(object sender, EventArgs e)
        {
            string selectedDrink = "Bac Xiu";
            DrinkPreferenceForm preferenceForm = new DrinkPreferenceForm(selectedDrink, robotController);
            preferenceForm.Show();
            this.Hide();
        }

        private void OrderWindow_Load(object sender, EventArgs e)
        {
            // Optional: do nothing or pre-load settings
        }

        private void buttonback_Click(object sender, EventArgs e)
        {
            loginWindow loginWindow = new loginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void groupBox_button_Coffee_Enter(object sender, EventArgs e)
        {
            // Optional event
        }

        private void button_Coffee_Click(object sender, EventArgs e)
        {
            // Optional duplicate
        }

        private void button_Latteartcoffe_Click(object sender, EventArgs e)
        {
            string selectedDrink = "Latte Art";
            DrinkPreferenceForm preferenceForm = new DrinkPreferenceForm(selectedDrink, robotController);
            preferenceForm.Show();
            this.Hide();
        }

        private void groupBox_button_MilkCoffe_Enter(object sender, EventArgs e)
        {

        }
    }
}