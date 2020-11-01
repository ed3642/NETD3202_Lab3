/* MainWindow.xaml.cs
 *      Program Title: IncInc Payroll (messages)
 * Last Modified Date: Nov 1, 2020
 *         Written By: Eduardo San Martin Celi
 * 
 * Description:
 * This form formats labs 1 and 2 into tabs and adds the feature of database functionality
 * and accessing the database data.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NETD3202_Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes the main window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            SetDefaults();
        }

        #region "Event_Hndlers"

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateStatus("Status: load up");
        }

        /// <summary>
        /// closes the form
        /// </summary>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// resets the color when an message box with an error is changed
        /// </summary>
        private void EditedTextbox(object sender, TextChangedEventArgs e)
        {
            TextBox txtEdited = (TextBox)sender;
            txtEdited.Background = Brushes.White;
            UpdateStatus("Status: error potentially fixed");
        }

        /// <summary>
        /// updates the view when the tab is changed
        /// </summary>
        private void SwitchTab(object sender, SelectionChangedEventArgs e)
        {
            if(tbcPayrollInterface.SelectedIndex == 0)
            {
                UpdateStatus("Status: showing payroll entry");
            }
            else if (tbcPayrollInterface.SelectedIndex == 1)
            {
                // outputing latest info onto the summary page
                txtEmployeesProcessed.Text = PieceworkWorker.TotalWorkers.ToString();
                txtAccumulativePay.Text = PieceworkWorker.TotalPay.ToString("c");
                txtAccumulativeMessages.Text = PieceworkWorker.TotalMessages.ToString();
                txtAveragePay.Text = PieceworkWorker.AveragePay.ToString("c");
                UpdateStatus("Status: showing employees summary page");
            }
            else if(tbcPayrollInterface.SelectedIndex == 2)
            {
                // populates the datagrid
                dgEmployeeRecords.ItemsSource = PieceworkWorker.AllWorkers.DefaultView;
                UpdateStatus("Status: showing employee list");
            }
        }

        /// <summary>
        /// Validation for creating a new worker and inserts it into the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // creating object and adding it to the database
                var myWorker = new PieceworkWorker(txtFName.Text, txtLName.Text, txtTextSent.Text);

                txtTotalPay.Text = myWorker.Pay.ToString("c");

                txtFName.IsEnabled = false;
                txtLName.IsEnabled = false;
                txtTextSent.IsEnabled = false;
                btnCalculate.IsEnabled = false;

                btnClear.Focus();

                UpdateStatus("Status: worker " + txtFName.Text + " " + txtLName.Text + " added with " + txtTextSent.Text + " messages sent");
            }
            catch (ArgumentNullException ex)
            {
                if (ex.ParamName == "Null Name")
                {
                    ExceptionText(txtFName);
                    lblWorkerFNameError.Content = ex.Message;
                    UpdateStatus("Status: error");
                }
                if (ex.ParamName == "Null Name")
                {
                    ExceptionText(txtLName);
                    lblWorkerLNameError.Content = ex.Message;
                    UpdateStatus("Status: error");
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                if (ex.ParamName == "Messages Range")
                {
                    ExceptionText(txtTextSent);
                    lblMessagesSentError.Content = ex.Message;
                    UpdateStatus("Status: error");
                }
            }
            catch (ArgumentException ex)
            {
                if (ex.ParamName == "Messages Datatype")
                {
                    ExceptionText(txtTextSent);
                    lblMessagesSentError.Content = ex.Message;
                    UpdateStatus("Status: error");
                }
            }
            catch (Exception)
            {
                // exceptions not prepared for
                MessageBox.Show("Critical Error!", "Something very unexpected happened.");
            }
        }

        /// <summary>
        /// Sets dafaults event
        /// </summary>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            SetDefaults();
            UpdateStatus("Status: cleared input fields");
        }

        #endregion

        #region "Methods"
        /// <summary>
        /// Updates the status bar
        /// </summary>
        /// <param name="statusUpdate">The new status</param>
        private void UpdateStatus(String statusUpdate)
        {
            lblStatus.Content = statusUpdate;
            lblTime.Content = DateTime.Now.ToString();
        }

        /// <summary>
        /// returns the form to default state
        /// </summary>
        private void SetDefaults()
        {
            //entry form defaults
            txtFName.Focus();

            txtFName.Clear();
            txtLName.Clear();
            txtTextSent.Clear();
            txtTotalPay.Clear();

            txtFName.IsEnabled = true;
            txtLName.IsEnabled = true;
            txtTextSent.IsEnabled = true;
            btnCalculate.IsEnabled = true;
        }

        /// <summary>
        /// Changes a textboxes properties
        /// </summary>
        /// <param name="txtBox">The textbox to be managed</param>
        private void ExceptionText(TextBox txtBox)
        {
            txtBox.SelectAll();
            txtBox.Focus();
            txtBox.Background = Brushes.Red;
        }

        #endregion
    }
}
