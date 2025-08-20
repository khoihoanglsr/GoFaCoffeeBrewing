using System;
using System.Timers;

using System.Windows.Forms;
using System.Windows.Input;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Controllers.Configuration;
using ABB.Robotics.Controllers.MotionDomain;
using System.Globalization;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Latte_Art_GoFa
{

    public partial class Login_Window : Form
    {
        NetworkScanner? Netscaner;      //this variable is used to scan the network to look for controllers
        NetworkWatcher networkwatcher = null;  //this variable is used to watch over the network to discover new controller
        public ControllerInfoCollection? Controllers;  //Info about the scanned controllers will be saved here
        public Controller? SelectedController;    //this variable is used to connect with the chosen controller
        string AdminUsername = "admin";
        string AdminPassword = "123";

        public Login_Window()
        {
            InitializeComponent();
            NetScan();   //scan the controller right after and save into the comboBox 

            this.networkwatcher = new NetworkWatcher(Netscaner.Controllers);
            this.networkwatcher.Found += new EventHandler<NetworkWatcherEventArgs>(HandleFoundEvent);
            this.networkwatcher.Lost += new EventHandler<NetworkWatcherEventArgs>(HandleLostEvent);
            this.networkwatcher.EnableRaisingEvents = true;
        }

        private void NetScan()    //this function is used to scan all the existing controllers in the same network
        {
            Netscaner = new NetworkScanner();   //initialize the variable
            Netscaner.Scan();   // start to scan
            Controllers = Netscaner.Controllers;   //store the scanned controllers into this variable
             
            foreach (ControllerInfo info in Controllers)
            {
                combo_Box_Controllers.Items.Add(info);  //load info into the comboBox
            }
        }

        void HandleFoundEvent(object sender, NetworkWatcherEventArgs e)
        {
            this.Invoke(new
            EventHandler<NetworkWatcherEventArgs>(AddControllerToListView),
            new System.Object[] { this, e });
        }

        void HandleLostEvent(object sender, NetworkWatcherEventArgs e)
        {
            this.Invoke(new
            EventHandler<NetworkWatcherEventArgs>(RemoveControllerFromListView),
            new System.Object[] { this, e });
        }

        private void AddControllerToListView(object sender, NetworkWatcherEventArgs e)
        {
            ControllerInfo controllerInfo = e.Controller;
            this.combo_Box_Controllers.Items.Add(controllerInfo);
        }

        private void RemoveControllerFromListView(object sender, NetworkWatcherEventArgs e)
        {
            ControllerInfo controllerInfo = e.Controller;
            this.combo_Box_Controllers.Items.Remove(controllerInfo);
        }

        private void comboBoxControllers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_Box_Controllers.SelectedItem == null) return;

            ControllerInfo selectedInfo = (ControllerInfo)combo_Box_Controllers.SelectedItem;   //sotr the info of the selected controller from the box

            if (selectedInfo is null) return;


            try
            {
                SelectedController = Controller.Connect(selectedInfo, ConnectionType.Standalone);  //Connect() is a basic function of PC SDK to connect to a specific controller
                SelectedController.Logon(UserInfo.DefaultUser);   //choose the user as default

                labelStatus.Text = $"Connected to {SelectedController.SystemName}";  //notify when connected


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to controller: " + ex.Message);   //notify when having error

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {


        }

        private void ButtonLoginAdmin_Click(object sender, EventArgs e)
        {


            if (text_BoxUsername.Text == AdminUsername && text_BoxtextPassword.Text == AdminPassword)
            {
                Latte_Art_GoFa Latte_Art_GoFa = new Latte_Art_GoFa();
                Latte_Art_GoFa.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Username or password is incorrect !");
            }
        }

        private void buttonLoginDefaultUser_Click(object sender, EventArgs e)
        {
            {
                DrinkSelectionForm_User OrderWindow = new DrinkSelectionForm_User(SelectedController);


                OrderWindow.Show();
                this.Hide();


            }
        }

        private void text_BoxUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_BoxtextPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
