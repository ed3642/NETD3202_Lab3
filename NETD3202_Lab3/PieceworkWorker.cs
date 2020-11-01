/* PieceworkWorker.cs
 * Program Title: IncInc Payroll (text messages)
 * Last Modified Date: October 31, 2020
 *         Written By: Eduardo San Martin Celi
 * 
 * Description:
 * This is a class representing individual worker objects. Each stores
 * their own name, messages sent. The class methods allow
 * for calculation of the worker's pay and for updating of shared summary
 * values. Name and messages sent worked are received as strings.
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NETD3202_Lab3
{
    class PieceworkWorker
    {
        #region "Variable Declarations"

        // Instance variables
        private int employeeId;
        private string employeeFirstName;
        private string employeeLastName;
        private int employeeMessages;
        private decimal employeePay;
        private DateTime employeeEntryDate;

        //private bool isValid = true;

        // Shared class variables
        //private static int overallNumberOfEmployees = 0;
        //private static int overallMessages = 0;
        //private static decimal overallPayroll = 0M;
        //private static decimal averagePayroll = 0M;

        // constants
        private int MIN_MESSAGES = 1;
        private int MAX_MESSAGES = 10000;

        #endregion

        #region "Constructors"

        /// <summary>
        /// PieceworkWorker constructor: accepts a worker's name and number of
        /// messages, sets and calculates values as appropriate.
        /// </summary>
        /// <param name="nameValue">the worker's name</param>
        /// <param name="messageValue">a worker's number of messages sent</param>
        public PieceworkWorker(string nameFValue, string nameLValue, string messagesValue)
        {
            // Validate and set the worker's name
            this.FirstName = nameFValue;
            // Validate and set the worker's last name
            this.LastName = nameLValue;
            // Validate and set the worker's number of messages
            this.Messages = messagesValue;
            //current time entryDate
            this.EntryDate = DateTime.Now;
            // Calculcate the worker's pay and update all summary values
            FindPay();

            //insert the worker into the database
            DataAccess.InsertNewRecord(this);
        }

        /// <summary>
        /// HourlyWorker default constructor: empty constructor used strictly for inheritance and instantiation
        /// </summary>
        internal PieceworkWorker()
        {

        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Currently called in the constructor, the findPay() method is
        /// used to calculate a worker's pay using their pay rate and hours
        /// worked. This also updates all summary values.
        /// </summary>
        private void FindPay()
        {
            //const int Threshold1 = 1;
            const int Threshold2 = 1000;
            const int Threshold3 = 2000;
            const int Threshold4 = 3000;
            const int Threshold5 = 4000;
            const decimal rate1 = 0.021M;
            const decimal rate2 = 0.028M;
            const decimal rate3 = 0.035M;
            const decimal rate4 = 0.040M;
            const decimal rate5 = 0.045M;

            // if - else block for determining the right rate for each employee
            if (employeeMessages >= Threshold5)
            {
                employeePay = Math.Round(employeeMessages * rate5, 2);
            }
            else if (employeeMessages >= Threshold4)
            {
                employeePay = Math.Round(employeeMessages * rate4, 2);
            }
            else if (employeeMessages >= Threshold3)
            {
                employeePay = Math.Round(employeeMessages * rate3, 2);
            }
            else if (employeeMessages >= Threshold2)
            {
                employeePay = Math.Round(employeeMessages * rate2, 2);
            }
            else
            {
                employeePay = Math.Round(employeeMessages * rate1, 2);
            }
            // accumulates the values of each processed employee
            /*overallNumberOfEmployees++;
            overallMessages += employeeMessages;
            overallPayroll += employeePay;*/
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// Gets and sets a first worker's name
        /// </summary>
        /// <returns>an employee's first name</returns>
        public string FirstName
        {
            get
            {
                return employeeFirstName;
            }
            set
            {
                // Add validation for the worker's name based on the requirements
                // document
                // to check if the string is empty
                if (value == "")
                {
                    //MessageBox.Show("Please enter the worker's name.");
                    //isValid = false;
                    throw new ArgumentNullException("Null Name", "Please enter the worker's name.");
                }
                else
                {
                    employeeFirstName = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets a last worker's name
        /// </summary>
        /// <returns>an employee's last name</returns>
        public string LastName
        {
            get
            {
                return employeeLastName;
            }
            set
            {
                // Add validation for the worker's name based on the requirements
                // document
                // to check if the string is empty
                if (value == "")
                {
                    //MessageBox.Show("Please enter the worker's name.");
                    //isValid = false;
                    throw new ArgumentNullException("Null Name", "Please enter the worker's name.");
                }
                else
                {
                    employeeLastName = value;
                }
            }
        }

        /// <summary>
        /// Gets a worker's entry date
        /// </summary>
        /// <returns>an employee's entry date</returns>
        public DateTime EntryDate
        {
            get
            {
                return employeeEntryDate;
            }
            set
            {
                employeeEntryDate = value;
            }
        }

        /// <summary>
        /// Gets and sets the number of messages sent by a worker
        /// </summary>
        /// <returns>an employee's number of messages</returns>
        public string Messages
        {
            get
            {
                return employeeMessages.ToString();
            }
            set
            {
                // Add validation for the number of messages based on the
                // requirements document
                if (!int.TryParse(value, out employeeMessages))
                {
                    //MessageBox.Show("Please enter the worker's messages sent as a positive integer value.");
                    //isValid = false;
                    throw new ArgumentException("Must be a positive integer value", "Messages Datatype");
                }
                else if (Convert.ToInt32(value) < MIN_MESSAGES || Convert.ToInt32(value) > MAX_MESSAGES)
                {
                    throw new ArgumentOutOfRangeException("Messages Range", "Must be between" + MIN_MESSAGES + " and " + MAX_MESSAGES + ".");
                }
            }
        }

        /// <summary>
        /// Gets the worker's pay
        /// </summary>
        /// <returns>a worker's pay</returns>
        public decimal Pay
        {
            get
            {
                return employeePay;
            }
            set
            {
                employeePay = value;
            }
        }

        /// <summary>
        /// Gets the worker's id
        /// </summary>
        /// <returns>a worker's Id</returns>
        public int Id
        {
            get
            {
                return employeeId;
            }
            set
            {
                employeeId = value;
            }
        }

        /// <summary>
        /// Gets the overall total pay among all workers
        /// </summary>
        /// <returns>the overall total pay among all workers</returns>
        public static decimal TotalPay
        {
            get
            {
                return Convert.ToDecimal(DataAccess.GetTotalPay());
            }
        }

        /// <summary>
        /// Gets the overall number of workers
        /// </summary>
        /// <returns>the overall number of workers</returns>
        public static int TotalWorkers
        {
            get
            {
                return Convert.ToInt32(DataAccess.GetTotalEmployees());
            }
        }

        /// <summary>
        /// Gets the overall number of messages sent
        /// </summary>
        /// <returns>the overall number of messages sent</returns>
        public static int TotalMessages
        {
            get
            {
                return Convert.ToInt32(DataAccess.GetTotalMessages());
            }
        }

        /// <summary>
        /// Calculates and returns an average pay among all workers
        /// </summary>
        /// <returns>the average pay among all workers</returns>
        public static decimal AveragePay
        {
            get
            {
                // Write the logic that will return the average pay among all workers. Test this carefully!
                // avoids deviding by 0
                return (TotalWorkers == 0) ? 0 : Math.Round(TotalPay / TotalWorkers, 2);

            }
        }

        /// <summary>
        /// To populate the datagrid
        /// </summary>
        public static DataTable AllWorkers
        {
            get
            {
                return DataAccess.GetEmployeeList();
            }
        }

        #endregion

    }
}
