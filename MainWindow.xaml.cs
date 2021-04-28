using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using Bambora.Connect2T;

namespace DebugPOS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Connection connection;
        bool ConnectionStatus = false;
        Dictionary<int, Dictionary<string, string>> ElementIDs = PopulateElementIDs();
        List<int> RefundElements = new List<int> { 9, 75, 3, 4, 5, 6, 7, 11, 18, 33, 46, 56, 57, 58, 61, 62, 69, 104, 114, 124, 126, 132 };

        Helpers helpers = new Helpers();
        ReceiptHandler receiptHandler = new ReceiptHandler();
        
        public MainWindow()
        {
            InitializeComponent();
            //ConnectCommand(Properties.GlobalSettings.Default.IPorCOM, Properties.GlobalSettings.Default.PortOrBaud);
            
        }

        private static Dictionary<int, Dictionary<string, string>> PopulateElementIDs()
        {
            Dictionary<string, string> d = new Dictionary<string, string> { { "name", "Result" }, { "type", "int" } };

            Dictionary<int, Dictionary<string, string>> dict =
                new Dictionary<int, Dictionary<string, string>> {
                    {0,
                        new Dictionary<string, string> {
                            {"name", "Result"},
                            {"type", "integer"}
                        }
                    },
                    {1,
                        new Dictionary<string, string> {
                            {"name", "Status"},
                            {"type", "integer"}
                        }
                    },
                    {2,
                        new Dictionary<string, string> {
                            {"name", "TransactionResult"},
                            {"type", "integer"}
                        }
                    },
                    {3,
                        new Dictionary<string, string> {
                            {"name", "PrintReceipt"},
                            {"type", "bool"}
                        }
                    },
                    {4,
                        new Dictionary<string, string> {
                            {"name", "CardholderLanguage"},
                            {"type", "string"}
                        }
                    },
                    {5,
                        new Dictionary<string, string> {
                            {"name", "MerchantLanguage"},
                            {"type", "string"}
                        }
                    },
                    {6,
                        new Dictionary<string, string> {
                            {"name", "CashierId"},
                            {"type", "string"}
                        }
                    },
                    {7,
                        new Dictionary<string, string> {
                            {"name", "RegisterId"},
                            {"type", "string"}
                        }
                    },
                    {8,
                        new Dictionary<string, string> {
                            {"name", "TransactionType"},
                            {"type", "integer"}
                        }
                    },
                    {9,
                        new Dictionary<string, string> {
                            {"name", "CurrencyCode"},
                            {"type", "string"}
                        }
                    },
                    {10,
                        new Dictionary<string, string> {
                            {"name", "CurrencySymbol"},
                            {"type", "string"}
                        }
                    },
                    {11,
                        new Dictionary<string, string> {
                            {"name", "BaseAmount"},
                            {"type", "integer"}
                        }
                    },
                    {12,
                        new Dictionary<string, string> {
                            {"name", "VATAmount"},
                            {"type", "integer"}
                        }
                    },
                    {13,
                        new Dictionary<string, string> {
                            {"name", "SurchargeAmount"},
                            {"type", "integer"}
                        }
                    },
                    {14,
                        new Dictionary<string, string> {
                            {"name", "CashbackAmount"},
                            {"type", "integer"}
                        }
                    },
                    {15,
                        new Dictionary<string, string> {
                            {"name", "ExtraAmount"},
                            {"type", "integer"}
                        }
                    },
                    {16,
                        new Dictionary<string, string> {
                            {"name", "TotalAmount"},
                            {"type", "integer"}
                        }
                    },
                    {17,
                        new Dictionary<string, string> {
                            {"name", "InternalReference"},
                            {"type", "string"}
                        }
                    },
                    {18,
                        new Dictionary<string, string> {
                            {"name", "ExternalReference"},
                            {"type", "string"}
                        }
                    },
                    {19,
                        new Dictionary<string, string> {
                            {"name", "OriginalInternalReference"},
                            {"type", "string"}
                        }
                    },
                    {20,
                        new Dictionary<string, string> {
                            {"name", "OriginalExternalReference"},
                            {"type", "string"}
                        }
                    },
                    {21,
                        new Dictionary<string, string> {
                            {"name", "BatchReference"},
                            {"type", "string"}
                        }
                    },
                    {22,
                        new Dictionary<string, string> {
                            {"name", "TransactionReference"},
                            {"type", "string"}
                        }
                    },
                    {23,
                        new Dictionary<string, string> {
                            {"name", "RetrievalReference"},
                            {"type", "string"}
                        }
                    },
                    {24,
                        new Dictionary<string, string> {
                            {"name", "ReversalReference"},
                            {"type", "string"}
                        }
                    },
                    {25,
                        new Dictionary<string, string> {
                            {"name", "SettleReference"},
                            {"type", "string"}
                        }
                    },
                    {26,
                        new Dictionary<string, string> {
                            {"name", "InternalReferenceList"},
                            {"type", "stringlist"}
                        }
                    },
                    {27,
                        new Dictionary<string, string> {
                            {"name", "Timestamp"},
                            {"type", "string"}
                        }
                    },
                    {28,
                        new Dictionary<string, string> {
                            {"name", "EntryMode"},
                            {"type", "char"}
                        }
                    },
                    {29,
                        new Dictionary<string, string> {
                            {"name", "VerificationMethod"},
                            {"type", "char"}
                        }
                    },
                    {30,
                        new Dictionary<string, string> {
                            {"name", "VerificationOption"},
                            {"type", "char"}
                        }
                    },
                    {31,
                        new Dictionary<string, string> {
                            {"name", "Onboard"},
                            {"type", "bool"}
                        }
                    },
                    {32,
                        new Dictionary<string, string> {
                            {"name", "AuthorizationMethod"},
                            {"type", "char"}
                        }
                    },
                    {33,
                        new Dictionary<string, string> {
                            {"name", "AuthorizationOption"},
                            {"type", "char"}
                        }
                    },
                    {34,
                        new Dictionary<string, string> {
                            {"name", "AuthorizationResponder"},
                            {"type", "char"}
                        }
                    },
                    {35,
                        new Dictionary<string, string> {
                            {"name", "AccountType"},
                            {"type", "char"}
                        }
                    },
                    {36,
                        new Dictionary<string, string> {
                            {"name", "FinancialInstitution"},
                            {"type", "string"}
                        }
                    },
                    {37,
                        new Dictionary<string, string> {
                            {"name", "SPDHResponseCode"},
                            {"type", "string"}
                        }
                    },
                    {38,
                        new Dictionary<string, string> {
                            {"name", "ApprovalCode"},
                            {"type", "string"}
                        }
                    },
                    {39,
                        new Dictionary<string, string> {
                            {"name", "MaskedPAN"},
                            {"type", "string"}
                        }
                    },
                    {40,
                        new Dictionary<string, string> {
                            {"name", "EMVData"},
                            {"type", "stringmap"}
                        }
                    },
                    {41,
                        new Dictionary<string, string> {
                            {"name", "EncryptedInformation"},
                            {"type", "stringlist"}
                        }
                    },
                    {42,
                        new Dictionary<string, string> {
                            {"name", "MerchantInformation"},
                            {"type", "stringmap"}
                        }
                    },
                    {43,
                        new Dictionary<string, string> {
                            {"name", "MaintenanceStatus"},
                            {"type", "integer"}
                        }
                    },
                    {44,
                        new Dictionary<string, string> {
                            {"name", "Reserved"},
                            {"type", "n/a"}
                        }
                    },
                    {45,
                        new Dictionary<string, string> {
                            {"name", "Reserved"},
                            {"type", "n/a"}
                        }
                    },
                    {46,
                        new Dictionary<string, string> {
                            {"name", "CustomData"},
                            {"type", "stringmap"}
                        }
                    },
                    {47,
                        new Dictionary<string, string> {
                            {"name", "Reserved"},
                            {"type", "n/a"}
                        }
                    },
                    {48,
                        new Dictionary<string, string> {
                            {"name", "Reserved"},
                            {"type", "n/a"}
                        }
                    },
                    {49,
                        new Dictionary<string, string> {
                            {"name", "TerminalInfoRequest"},
                            {"type", "stringlist"}
                        }
                    },
                    {50,
                        new Dictionary<string, string> {
                            {"name", "TerminalInfo"},
                            {"type", "stringmap"}
                        }
                    },
                    {51,
                        new Dictionary<string, string> {
                            {"name", "Host"},
                            {"type", "string"}
                        }
                    },
                    {52,
                        new Dictionary<string, string> {
                            {"name", "Data"},
                            {"type", "binary"}
                        }
                    },
                    {53,
                        new Dictionary<string, string> {
                            {"name", "DialogId"},
                            {"type", "integer"}
                        }
                    },
                    {54,
                        new Dictionary<string, string> {
                            {"name", "DialogText"},
                            {"type", "string"}
                        }
                    },
                    {55,
                        new Dictionary<string, string> {
                            {"name", "DialogResult"},
                            {"type", "string"}
                        }
                    },
                    {56,
                        new Dictionary<string, string> {
                            {"name", "EnableDialog"},
                            {"type", "char"}
                        }
                    },
                    {57,
                        new Dictionary<string, string> {
                            {"name", "CardInsertionTimeout"},
                            {"type", "integer"}
                        }
                    },
                    {58,
                        new Dictionary<string, string> {
                            {"name", "EchoData"},
                            {"type", "string"}
                        }
                    },
                    {59,
                        new Dictionary<string, string> {
                            {"name", "TerminalId"},
                            {"type", "string"}
                        }
                    },
                    {60,
                        new Dictionary<string, string> {
                            {"name", "AcquirerId"},
                            {"type", "string"}
                        }
                    },
                    {61,
                        new Dictionary<string, string> {
                            {"name", "MerchantId"},
                            {"type", "string"}
                        }
                    },
                    {62,
                        new Dictionary<string, string> {
                            {"name", "EnableExternalNetworking"},
                            {"type", "bool"}
                        }
                    },
                    {63,
                        new Dictionary<string, string> {
                            {"name", "Token"},
                            {"type", "string"}
                        }
                    },
                    {64,
                        new Dictionary<string, string> {
                            {"name", "ProductName"},
                            {"type", "string"}
                        }
                    },
                    {65,
                        new Dictionary<string, string> {
                            {"name", "CardholderName"},
                            {"type", "string"}
                        }
                    },
                    {66,
                        new Dictionary<string, string> {
                            {"name", "ISOResponseCode"},
                            {"type", "string"}
                        }
                    },
                    {67,
                        new Dictionary<string, string> {
                            {"name", "Timeout"},
                            {"type", "integer"}
                        }
                    },
                    {68,
                        new Dictionary<string, string> {
                            {"name", "MaxLength"},
                            {"type", "integer"}
                        }
                    },
                    {69,
                        new Dictionary<string, string> {
                            {"name", "ReceiptData"},
                            {"type", "string"}
                        }
                    },
                    {70,
                        new Dictionary<string, string> {
                            {"name", "DialogTitle"},
                            {"type", "string"}
                        }
                    },
                    {71,
                        new Dictionary<string, string> {
                            {"name", "DialogType"},
                            {"type", "integer"}
                        }
                    },
                    {72,
                        new Dictionary<string, string> {
                            {"name", "DialogOptions"},
                            {"type", "stringmap"}
                        }
                    },
                    {73,
                        new Dictionary<string, string> {
                            {"name", "CardDataRequest"},
                            {"type", "stringlist"}
                        }
                    },
                    {74,
                        new Dictionary<string, string> {
                            {"name", "CardData"},
                            {"type", "stringmap"}
                        }
                    },
                    {75,
                        new Dictionary<string, string> {
                            {"name", "EntryOption"},
                            {"type", "char"}
                        }
                    },
                    {76,
                        new Dictionary<string, string> {
                            {"name", "ClosingReference"},
                            {"type", "string"}
                        }
                    },
                    {77,
                        new Dictionary<string, string> {
                            {"name", "Bonus"},
                            {"type", "integer"}
                        }
                    },
                    {78,
                        new Dictionary<string, string> {
                            {"name", "ProtocolVersion"},
                            {"type", "string"}
                        }
                    },
                    {79,
                        new Dictionary<string, string> {
                            {"name", "PreferredProtocolVersion"},
                            {"type", "string"}
                        }
                    },
                    {80,
                        new Dictionary<string, string> {
                            {"name", "Key"},
                            {"type", "string"}
                        }
                    },
                    {85,
                        new Dictionary<string, string> {
                            {"name", "SoftwareVersion"},
                            {"type", "string"}
                        }
                    },
                    {86,
                        new Dictionary<string, string> {
                            {"name", "SoftwareCompatibility"},
                            {"type", "stringlist"}
                        }
                    },
                    {87,
                        new Dictionary<string, string> {
                            {"name", "TargetSoftwareVersion"},
                            {"type", "string"}
                        }
                    },
                    {88,
                        new Dictionary<string, string> {
                            {"name", "TargetSoftwareCompatibility"},
                            {"type", "stringlist"}
                        }
                    },
                    {89,
                        new Dictionary<string, string> {
                            {"name", "UnmaskedPAN"},
                            {"type", "string"}
                        }
                    },
                    {97,
                        new Dictionary<string, string> {
                            {"name", "Balance"},
                            {"type", "integer"}
                        }
                    },
                    {98,
                        new Dictionary<string, string> {
                            {"name", "Uptime"},
                            {"type", "string"}
                        }
                    },
                    {99,
                        new Dictionary<string, string> {
                            {"name", "ErrorField"},
                            {"type", "integer"}
                        }
                    },
                    {100,
                        new Dictionary<string, string> {
                            {"name", "ErrorData"},
                            {"type", "any"}
                        }
                    },
                    {101,
                        new Dictionary<string, string> {
                            {"name", "CardTotals"},
                            {"type", "stringlist"}
                        }
                    },
                    {102,
                        new Dictionary<string, string> {
                            {"name", "CashierTotals"},
                            {"type", "stringlist"}
                        }
                    },
                    {103,
                        new Dictionary<string, string> {
                            {"name", "ExtraSettleInformation"},
                            {"type", "bool"}
                        }
                    },
                    {104,
                        new Dictionary<string, string> {
                            {"name", "UseDcc"},
                            {"type", "bool"}
                        }
                    },
                    {105,
                        new Dictionary<string, string> {
                            {"name", "DCCAmount"},
                            {"type", "integer"}
                        }
                    },
                    {106,
                        new Dictionary<string, string> {
                            {"name", "DCCCurrencyCode"},
                            {"type", "string"}
                        }
                    },
                    {107,
                        new Dictionary<string, string> {
                            {"name", "DCCCurrencyExponent"},
                            {"type", "integer"}
                        }
                    },
                    {108,
                        new Dictionary<string, string> {
                            {"name", "DCCCurrencySymbol"},
                            {"type", "string"}
                        }
                    },
                    {109,
                        new Dictionary<string, string> {
                            {"name", "DCCExchangeRate"},
                            {"type", "string"}
                        }
                    },
                    {110,
                        new Dictionary<string, string> {
                            {"name", "DCCMarginRate"},
                            {"type", "string"}
                        }
                    },
                    {111,
                        new Dictionary<string, string> {
                            {"name", "DCCMerchantDisclaimer"},
                            {"type", "string"}
                        }
                    },
                    {112,
                        new Dictionary<string, string> {
                            {"name", "DCCCardholderDisclaimer"},
                            {"type", "string"}
                        }
                    },
                    {113,
                        new Dictionary<string, string> {
                            {"name", "ReceiptType"},
                            {"type", "integer"}
                        }
                    },
                    {114,
                        new Dictionary<string, string> {
                            {"name", "SendReceipt"},
                            {"type", "bool"}
                        }
                    },
                    {115,
                        new Dictionary<string, string> {
                            {"name", "CurrencyExponent"},
                            {"type", "integer"}
                        }
                    },
                    {116,
                        new Dictionary<string, string> {
                            {"name", "BatchTimestamp"},
                            {"type", "string"}
                        }
                    },
                    {117,
                        new Dictionary<string, string> {
                            {"name", "InvoiceCustomerName"},
                            {"type", "string"}
                        }
                    },
                    {118,
                        new Dictionary<string, string> {
                            {"name", "InvoiceCustomerAddress"},
                            {"type", "string"}
                        }
                    },
                    {119,
                        new Dictionary<string, string> {
                            {"name", "InvoiceCustomerZipCode"},
                            {"type", "string"}
                        }
                    },
                    {120,
                        new Dictionary<string, string> {
                            {"name", "InvoiceCustomerCity"},
                            {"type", "string"}
                        }
                    },
                    {121,
                        new Dictionary<string, string> {
                            {"name", "InvoiceCustomerCountry"},
                            {"type", "string"}
                        }
                    },
                    {122,
                        new Dictionary<string, string> {
                            {"name", "InvoiceSupplier"},
                            {"type", "string"}
                        }
                    },
                    {123,
                        new Dictionary<string, string> {
                            {"name", "InvoiceOrders"},
                            {"type", "string"}
                        }
                    },
                    {124,
                        new Dictionary<string, string> {
                            {"name", "InvoiceReference"},
                            {"type", "string"}
                        }
                    },
                    {125,
                        new Dictionary<string, string> {
                            {"name", "InvoiceFeeAmount"},
                            {"type", "integer"}
                        }
                    },
                    {126,
                        new Dictionary<string, string> {
                            {"name", "SalesLocationID"},
                            {"type", "string"}
                        }
                    },
                    {127,
                        new Dictionary<string, string> {
                            {"name", "InvoiceMaskedSSN"},
                            {"type", "string"}
                        }
                    },
                    {128,
                        new Dictionary<string, string> {
                            {"name", "DCCEnabled"},
                            {"type", "bool"}
                        }
                    },
                    {129,
                        new Dictionary<string, string> {
                            {"name", "InvoiceEnabled"},
                            {"type", "bool"}
                        }
                    },
                    {130,
                        new Dictionary<string, string> {
                            {"name", "MultiUserEnabled"},
                            {"type", "bool"}
                        }
                    },
                    {131,
                        new Dictionary<string, string> {
                            {"name", "SalesLocations"},
                            {"type", "stringmap"}
                        }
                    },
                    {132,
                        new Dictionary<string, string> {
                            {"name", "MobilePayReference"},
                            {"type", "string"}
                        }
                    },
                    {133,
                        new Dictionary<string, string> {
                            {"name", "MobilePayEnabled"},
                            {"type", "bool"}
                        }
                    },
                    {134,
                        new Dictionary<string, string> {
                            {"name", "TimeUntilReboot"},
                            {"type", "integer"}
                        }
                    }
                    };               
                
            /*
            foreach (var item in dict)
            {
                Console.WriteLine(item.Value["type"] + " " + item.Key + "=" + item.Value["name"]);
            }         
            */

            return dict;
        }
        private void ConnectCommand(string IPorCOM, int PortOrBaud)
        {

            if (Properties.GlobalSettings.Default.ServerEnabled)
            {
                try
                {

                    foreach (var item in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                    {
                        if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            Properties.GlobalSettings.Default.LocalIP = item.ToString();
                            break;
                        }
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }

                Console.WriteLine("Setting POS as server at " + Properties.GlobalSettings.Default.LocalIP + ":" + Properties.GlobalSettings.Default.LocalPort);
                try
                {
                    //connection = new Connection(TcpConnectionMode.Server, Properties.GlobalSettings.Default.LocalIP, Properties.GlobalSettings.Default.LocalPort, ResponseCallback, ErrorCallback, NetworkCallback);
                }
                catch (Exception ex)
                {

                    //Console.WriteLine(ex.ToString() + "\n" + ex.InnerException);
                    Console.WriteLine("No terminal connected, reopening server...");
                    //connection = new Connection(TcpConnectionMode.Server, Properties.GlobalSettings.Default.LocalIP, Properties.GlobalSettings.Default.LocalPort, ResponseCallback, ErrorCallback, NetworkCallback);
                }

            }
            else
            {
                if (IPorCOM.Length > 4)
                {
                    Console.WriteLine("Connecting to " + IPorCOM + ":" + PortOrBaud);
                    try
                    {
                        connection = new Connection(IPorCOM, PortOrBaud, ResponseCallback, ErrorCallback, NetworkCallback);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.InnerException);
                    }
                }
                else
                {
                    try
                    {
                        connection = new Connection(Int32.Parse(IPorCOM), PortOrBaud, ResponseCallback, ErrorCallback, NetworkCallback);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.ToString());
                    }
                }
                try
                {
                    //SendRequestToTerminal(new GetInformationRequest { EchoData = "ping" }, false);
                    connection.LogMode = Int32.Parse(Properties.GlobalSettings.Default.LogMode);
                    if (Properties.GlobalSettings.Default.IntegrationKey != null)
                    {
                        connection.IntegrationKey = Properties.GlobalSettings.Default.IntegrationKey;
                    }
                    
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.InnerException);
                }
            }

        }

        private void button_connect_Click(object sender, RoutedEventArgs e)
        {
            int PortOrBaud;
            string IPorCOM = IPorCOMTextBox.Text;
            Int32.TryParse(PortOrBaudTextBox.Text, out PortOrBaud);
            Properties.GlobalSettings.Default.IPorCOM = IPorCOM;
            Properties.GlobalSettings.Default.PortOrBaud = PortOrBaud;
            ConnectCommand(Properties.GlobalSettings.Default.IPorCOM, Properties.GlobalSettings.Default.PortOrBaud);
        }

        private void button_disconnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Dispose();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            ChangeConnectionStatus();
        }

        private void SendRequestToTerminal(Transaction req, bool ShowInDebug = true)
        {
            if (ShowInDebug)
            {
                PrintToDebug(req);
            }
            connection.SendRequest(req);
        }

        private void ChangeConnectionStatus()
        {
            if(ConnectionStatus)
            {
                ConnectionStatus = false;
            }
            else
            {
                ConnectionStatus = true;
            }
            if(ConnectionStatus)
            {
                this.Dispatcher.Invoke(() =>
                {
                    //button_connect.Content = "Connected";
                    button_connect.IsEnabled = false;
                    button_disconnect.IsEnabled = ConnectionStatus;
                    //ConnectedSnackbar.IsActive = true;

                });
            } else
            {
                this.Dispatcher.Invoke(() =>
                {
                    button_connect.Content = "Connect";
                    button_connect.IsEnabled = true;
                    button_disconnect.IsEnabled = ConnectionStatus;
                    //ConnectedSnackbar. = ;
                    //ConnectedSnackbar.IsActive = true;
                });
            }

        }


       
        private void ResizeGridViewColumn(GridViewColumn column)
        {
            if (double.IsNaN(column.Width))
            {
                column.Width = column.ActualWidth;
            }

            column.Width = double.NaN;
        }





        public void SendButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            //Console.WriteLine(btn.Name);
            //Console.WriteLine(e.);
            try
            {
                switch (btn.Name)
                {
                    case "SaleButton":
                        SaleRequest SaleReq = new SaleRequest();
                        SaleReq = (SaleRequest)helpers.PopulateTransactionProperties(SaleReq);
                        SendRequestToTerminal(SaleReq);
                        break;

                    case "RefundButton":
                        RefundRequest RefundReq = new RefundRequest();
                        RefundReq = (RefundRequest)helpers.PopulateTransactionProperties(RefundReq);
                        SendRequestToTerminal(RefundReq);
                        break;

                    case "ReversalButton":
                        ReversalRequest ReversalReq = new ReversalRequest();
                        ReversalReq = (ReversalRequest)helpers.PopulateTransactionProperties(ReversalReq);
                        SendRequestToTerminal(ReversalReq);
                        break;

                    case "SettleButton":
                        SettleRequest SettleReq = new SettleRequest();
                        SettleReq = (SettleRequest)helpers.PopulateTransactionProperties(SettleReq);
                        SendRequestToTerminal(SettleReq);
                        break;

                    case "GetTransactionButton":
                        GetTransactionRequest GetTransactionReq = new GetTransactionRequest();
                        GetTransactionReq = (GetTransactionRequest)helpers.PopulateTransactionProperties(GetTransactionReq);
                        SendRequestToTerminal(GetTransactionReq);
                        break;

                    case "TransactionListButton":
                        TransactionListRequest TransactionListReq = new TransactionListRequest();
                        TransactionListReq = (TransactionListRequest)helpers.PopulateTransactionProperties(TransactionListReq);
                        SendRequestToTerminal(TransactionListReq);
                        break;

                    case "CashAdvanceButton":
                        CashAdvanceRequest CashAdvanceReq = new CashAdvanceRequest();
                        CashAdvanceReq = (CashAdvanceRequest)helpers.PopulateTransactionProperties(CashAdvanceReq);
                        SendRequestToTerminal(CashAdvanceReq);
                        break;

                    case "GetInformationButton":
                        GetInformationRequest GetInformationReq = new GetInformationRequest();
                        GetInformationReq = (GetInformationRequest)helpers.PopulateTransactionProperties(GetInformationReq);
                        SendRequestToTerminal(GetInformationReq);
                        break;

                    case "ReadCardButton":
                        ReadCardRequest ReadCardReq = new ReadCardRequest();
                        ReadCardReq = (ReadCardRequest)helpers.PopulateTransactionProperties(ReadCardReq);
                        SendRequestToTerminal(ReadCardReq);
                        break;

                    case "LogonButton":
                        LogonRequest LogonReq = new LogonRequest();
                        LogonReq = (LogonRequest)helpers.PopulateTransactionProperties(LogonReq);
                        SendRequestToTerminal(LogonReq);
                        break;

                    case "InitializeButton":
                        InitializeRequest InitializeReq = new InitializeRequest();
                        InitializeReq = (InitializeRequest)helpers.PopulateTransactionProperties(InitializeReq);
                        SendRequestToTerminal(InitializeReq);
                        break;

                    case "SoftwareUpdateButton":
                        SoftwareUpdateRequest SoftwareUpdateReq = new SoftwareUpdateRequest();
                        SoftwareUpdateReq = (SoftwareUpdateRequest)helpers.PopulateTransactionProperties(SoftwareUpdateReq);
                        SendRequestToTerminal(SoftwareUpdateReq);
                        break;

                    case "PrintButton":
                        PrintRequest PrintReq = new PrintRequest();
                        PrintReq = (PrintRequest)helpers.PopulateTransactionProperties(PrintReq);
                        SendRequestToTerminal(PrintReq);
                        break;

                    case "RestartButton":
                        RestartRequest RestartReq = new RestartRequest();
                        RestartReq = (RestartRequest)helpers.PopulateTransactionProperties(RestartReq);
                        SendRequestToTerminal(RestartReq);
                        break;

                    case "GetSalesLocationsButton":
                        GetSalesLocationsRequest GetSalesLocationsReq = new GetSalesLocationsRequest();
                        GetSalesLocationsReq = (GetSalesLocationsRequest)helpers.PopulateTransactionProperties(GetSalesLocationsReq);
                        SendRequestToTerminal(GetSalesLocationsReq);
                        break;

                    case "BonusButton":
                        BonusRequest BonusReq = new BonusRequest();
                        BonusReq = (BonusRequest)helpers.PopulateTransactionProperties(BonusReq);
                        SendRequestToTerminal(BonusReq);
                        break;

                    case "AuthorizeButton":
                        AuthorizeRequest AuthorizeReq = new AuthorizeRequest();
                        AuthorizeReq = (AuthorizeRequest)helpers.PopulateTransactionProperties(AuthorizeReq);
                        SendRequestToTerminal(AuthorizeReq);
                        break;

                    case "FinalizeButton":
                        FinalizeRequest FinalizeReq = new FinalizeRequest();
                        FinalizeReq = (FinalizeRequest)helpers.PopulateTransactionProperties(FinalizeReq);
                        SendRequestToTerminal(FinalizeReq);
                        break;

                    case "ReleaseButton":
                        ReleaseRequest ReleaseReq = new ReleaseRequest();
                        ReleaseReq = (ReleaseRequest)helpers.PopulateTransactionProperties(ReleaseReq);
                        SendRequestToTerminal(ReleaseReq);
                        break;

                    case "PreauthorizationButton":
                        PreauthorizationRequest PreauthorizationReq = new PreauthorizationRequest();
                        PreauthorizationReq = (PreauthorizationRequest)helpers.PopulateTransactionProperties(PreauthorizationReq);
                        SendRequestToTerminal(PreauthorizationReq);
                        break;

                    case "IncrementButton":
                        IncrementRequest IncrementReq = new IncrementRequest();
                        IncrementReq = (IncrementRequest)helpers.PopulateTransactionProperties(IncrementReq);
                        SendRequestToTerminal(IncrementReq);
                        break;

                    case "DecrementButton":
                        DecrementRequest DecrementReq = new DecrementRequest();
                        DecrementReq = (DecrementRequest)helpers.PopulateTransactionProperties(DecrementReq);
                        SendRequestToTerminal(DecrementReq);
                        break;

                    case "CompletionButton":
                        CompletionRequest CompletionReq = new CompletionRequest();
                        CompletionReq = (CompletionRequest)helpers.PopulateTransactionProperties(CompletionReq);
                        SendRequestToTerminal(CompletionReq);
                        break;

                    case "ReleasePreauthorizationButton":
                        ReleasePreauthorizationRequest ReleasePreauthorizationReq = new ReleasePreauthorizationRequest();
                        ReleasePreauthorizationReq = (ReleasePreauthorizationRequest)helpers.PopulateTransactionProperties(ReleasePreauthorizationReq);
                        SendRequestToTerminal(ReleasePreauthorizationReq);
                        break;

                    case "CancelButton":
                        CancelRequest CancelReq = new CancelRequest();
                        SendRequestToTerminal(CancelReq);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
           

            
        }
        private bool isNotNull(string s)
        {
            if(s == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /*
        private StringMap ConvertToStringMap(string s)
        {
            var s1 = s.Split(';');
            StringMap sm;
            foreach(string s2 in s1)
            {
                var kv = s2.Split(':');
                sm +=
            }
        }
        */

        private void PrintToDebug(Response data)
        {
            this.Dispatcher.Invoke(() =>
            {
                Expander expander = new Expander();
                expander.HorizontalAlignment = HorizontalAlignment.Stretch;
                expander.Header = data.GetMessageType + " at " + DateTime.Now.ToString("HH:mm:ss");
                ListView ListView_Debug = new ListView();
                //ListView_Debug.Height = 700;

                GridView grdView = new GridView();
                GridViewColumn nameColumn = new GridViewColumn();
                nameColumn.DisplayMemberBinding = new Binding("Name");
                nameColumn.Header = "Name";
                nameColumn.Width = 200;
                grdView.Columns.Add(nameColumn);
                GridViewColumn ValueColumn = new GridViewColumn();
                ValueColumn.DisplayMemberBinding = new Binding("Value");
                ValueColumn.Header = "Value";
                ValueColumn.Width = 200;
                grdView.Columns.Add(ValueColumn);
                ListView_Debug.View = grdView;

                foreach (System.Reflection.PropertyInfo prop in data.GetType().GetProperties())
                {
                    if (prop.GetValue(data) != null)
                    {

                        switch (prop.GetValue(data).GetType().ToString())
                        {
                            case "System.Collections.Generic.List`1[System.String]":
                                List<String> sl = (List<String>)prop.GetValue(data);
                                //Console.WriteLine(prop.Name);
                                for (int i = 0; i < sl.Count(); i++)
                                {
                                    //Console.WriteLine(i + " : " + sl[i]);
                                }
                                foreach (var item in sl)
                                {
                                    UpdateDebugListView(ListView_Debug, prop.Name, item);
                                    //Console.WriteLine(prop.Name +" : " + item);
                                }
                                
                                break;
                            case "System.Collections.Generic.List`1[Bambora.Connect2T.StringMap]":
                                System.Collections.Generic.List<Bambora.Connect2T.StringMap> sm = (System.Collections.Generic.List<Bambora.Connect2T.StringMap>)prop.GetValue(data);
                                foreach (var item in sm)
                                {
                                    UpdateDebugListView(ListView_Debug, item.key, item.value);
                                    //Console.WriteLine(item.key + " : " + item.value);
                                }

                                break;
                            default:
                                UpdateDebugListView(ListView_Debug, prop.Name, prop.GetValue(data).ToString());
                                //Console.WriteLine(prop.Name + " : " + prop.GetValue(data));
                                break;
                        }
                    }
                }

                expander.Content = ListView_Debug;
                Border border = new Border();
                border.SetResourceReference(Border.BackgroundProperty, "MaterialDesignDivider");
                border.Height = 1;
                border.HorizontalAlignment = HorizontalAlignment.Stretch;
                border.SnapsToDevicePixels = true;
                //border.Padding = new Thickness(10);
                //border.Margin = new Thickness(0,5,0,5);
                DebugStackPanel.Children.Insert(0, border);
                DebugStackPanel.Children.Insert(0, expander);
                //DebugStackPanel.CanVerticallyScroll = true;

            });

        }
        private void PrintToDebug(Transaction data)
        {
            string RequestType = data.GetType().ToString().Split('.')[2];
            this.Dispatcher.Invoke(() =>
            {
                Expander expander = new Expander();
                expander.HorizontalAlignment = HorizontalAlignment.Stretch;
                expander.Header = RequestType + " at " + DateTime.Now.ToString("HH:mm:ss"); ;
                ListView ListView_Debug = new ListView();
                //ListView_Debug.Height = 700;

                GridView grdView = new GridView();
                GridViewColumn nameColumn = new GridViewColumn();
                nameColumn.DisplayMemberBinding = new Binding("Name");
                nameColumn.Header = "Name";
                nameColumn.Width = 200;
                grdView.Columns.Add(nameColumn);
                GridViewColumn ValueColumn = new GridViewColumn();
                ValueColumn.DisplayMemberBinding = new Binding("Value");
                ValueColumn.Header = "Value";
                ValueColumn.Width = 200;
                grdView.Columns.Add(ValueColumn);
                ListView_Debug.View = grdView;

                foreach (System.Reflection.PropertyInfo prop in data.GetType().GetProperties())
                {
                    if (prop.GetValue(data) != null)
                    {

                        switch (prop.GetValue(data).GetType().ToString())
                        {
                            case "System.Collections.Generic.List`1[System.String]":
                                List<String> sl = (List<String>)prop.GetValue(data);
                                foreach (var item in sl)
                                {
                                    UpdateDebugListView(ListView_Debug, prop.Name, item);
                                    //Console.WriteLine(prop.Name +" : " + item);
                                }
                                break;
                            case "System.Collections.Generic.List`1[Bambora.Connect2T.StringMap]":
                                System.Collections.Generic.List<Bambora.Connect2T.StringMap> sm = (System.Collections.Generic.List<Bambora.Connect2T.StringMap>)prop.GetValue(data);
                                foreach (var item in sm)
                                {
                                    UpdateDebugListView(ListView_Debug, item.key, item.value);
                                    //Console.WriteLine(item.key + " : " + item.value);
                                }

                                break;
                            default:
                                UpdateDebugListView(ListView_Debug, prop.Name, prop.GetValue(data).ToString());
                                //Console.WriteLine(prop.Name + " : " + prop.GetValue(data));
                                break;
                        }
                    }
                }

                expander.Content = ListView_Debug;
                Border border = new Border();
                border.SetResourceReference(Border.BackgroundProperty, "MaterialDesignDivider");
                border.Height = 1;
                border.HorizontalAlignment = HorizontalAlignment.Stretch;
                border.SnapsToDevicePixels = true;
                //border.Padding = new Thickness(10);
                //border.Margin = new Thickness(0,5,0,5);
                DebugStackPanel.Children.Insert(0, border);
                DebugStackPanel.Children.Insert(0, expander);
                //DebugStackPanel.CanVerticallyScroll = true;

            });
        }
        private void UpdateDebugListView(ListView ListView_Debug, string name, string value)
        {
            DebugItem debugItem = new DebugItem { Name = name, Value = value };
            this.Dispatcher.Invoke(() =>
            {
                ListView_Debug.Items.Add(debugItem);
                //ResizeGridViewColumn(NameColumn);
                //ResizeGridViewColumn(ValueColumn);
            });
            //Console.WriteLine(debugItem.Name + " : " + debugItem.Value);
            //Console.Write
        }


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Properties.GlobalSettings.Default.Save();
            Properties.TransactionSettings.Default.Save();

        }

        private List<int> ValidElements(Transaction data) {
            List<int> list = new List<int>();
            foreach (System.Reflection.PropertyInfo prop in data.GetType().GetProperties())
            {
                foreach (var item in ElementIDs)
                {
                    if(item.Value["name"] == prop.Name)
                    {
                        list.Add(item.Key);
                    }
                }
                
                /*
                if (prop.GetValue(data) != null)
                {
                    
                    switch (prop.GetValue(data).GetType().ToString())
                    {
                        case "System.Collections.Generic.List`1[System.String]":
                            List<String> sl = (List<String>)prop.GetValue(data);
                            foreach (var item in sl)
                            {
                                UpdateDebugListView(ListView_Debug, prop.Name, item);
                                //Console.WriteLine(prop.Name +" : " + item);
                            }
                            break;
                        case "System.Collections.Generic.List`1[Bambora.Connect2T.StringMap]":
                            System.Collections.Generic.List<Bambora.Connect2T.StringMap> sm = (System.Collections.Generic.List<Bambora.Connect2T.StringMap>)prop.GetValue(data);
                            foreach (var item in sm)
                            {
                                UpdateDebugListView(ListView_Debug, item.key, item.value);
                                //Console.WriteLine(item.key + " : " + item.value);
                            }

                            break;
                        default:
                            UpdateDebugListView(ListView_Debug, prop.Name, prop.GetValue(data).ToString());
                            //Console.WriteLine(prop.Name + " : " + prop.GetValue(data));
                            break;
                    }                                        
                }
                */
            }
            return list;
        }
        private void ExpandRequestProperties_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            //List<int> ValidElements = new List<int> { };
            //Console.WriteLine(btn.Name);
            //Console.WriteLine(e.);
            switch (btn.Name)
            {
                case "ExpandSaleRequestButton":
                    //
                    SendButton.Name = "SaleButton";
                    //ValidElements = new  List<int>{ 9, 75, 3, 4, 5, 6, 7, 11, 12, 14, 18, 30, 33, 46, 56, 57, 58, 61, 62, 69, 75, 114, 123, 126 };

                    PopulateRequestPropertiesPanel(ValidElements(new SaleRequest { }));
                    break;
                case "ExpandSettleRequestButton":
                    //
                    SendButton.Name = "SettleButton";
                    //ValidElements = new List<int> { 3, 5, 18, 56, 58, 61, 62, 69, 103, 114, 126 };
                    PopulateRequestPropertiesPanel(ValidElements(new SettleRequest { }));
                    break;
                case "ExpandRefundRequestButton":
                    //
                    SendButton.Name = "RefundButton";
                    //ValidElements = new List<int> { 9, 75, 3, 4, 5, 6, 7, 11, 18, 33, 46, 56, 57, 58, 61, 62, 69, 104, 114, 124, 126, 132 };
                    PopulateRequestPropertiesPanel(ValidElements(new RefundRequest { }));
                    break;
                case "ExpandReversalRequestButton":
                    //
                    SendButton.Name = "ReversalButton";
                    PopulateRequestPropertiesPanel(ValidElements(new ReversalRequest { }));
                    break;
                case "ExpandGetTransactionRequestButton":
                    //
                    SendButton.Name = "GetTransactionButton";
                    PopulateRequestPropertiesPanel(ValidElements(new GetTransactionRequest { }));
                    break;
                case "ExpandCashAdvanceRequestButton":
                    //
                    SendButton.Name = "CashAdvanceButton";
                    PopulateRequestPropertiesPanel(ValidElements(new CashAdvanceRequest { }));
                    break;
                case "ExpandTransactionListRequestButton":
                    //
                    SendButton.Name = "TransactionListButton";
                    PopulateRequestPropertiesPanel(ValidElements(new TransactionListRequest { }));
                    break;
                case "ExpandGetInformationRequestButton":
                    //
                    SendButton.Name = "GetInformationButton";
                    PopulateRequestPropertiesPanel(ValidElements(new GetInformationRequest { }));
                    break;
                case "ExpandReadCardRequestButton":
                    //
                    SendButton.Name = "ReadCardButton";
                    PopulateRequestPropertiesPanel(ValidElements(new ReadCardRequest { }));
                    break;
                case "ExpandLogonRequestButton":
                    //
                    SendButton.Name = "LogonButton";
                    PopulateRequestPropertiesPanel(ValidElements(new LogonRequest { }));
                    break;
                case "ExpandInitializeRequestButton":
                    //
                    SendButton.Name = "InitializeButton";
                    PopulateRequestPropertiesPanel(ValidElements(new InitializeRequest { }));
                    break;
                case "ExpandSoftwareUpdateRequestButton":
                    //
                    SendButton.Name = "SoftwareUpdateButton";
                    PopulateRequestPropertiesPanel(ValidElements(new SoftwareUpdateRequest { }));
                    break;
                case "ExpandPrintRequestButton":
                    //
                    SendButton.Name = "PrintButton";
                    PopulateRequestPropertiesPanel(ValidElements(new PrintRequest { }));
                    break;
                case "ExpandRestartRequestButton":
                    //
                    SendButton.Name = "RestartButton";
                    PopulateRequestPropertiesPanel(ValidElements(new RestartRequest { }));
                    break;
                case "ExpandGetSalesLocationsRequestButton":
                    //
                    SendButton.Name = "GetSalesLocationsButton";
                    PopulateRequestPropertiesPanel(ValidElements(new GetSalesLocationsRequest { }));
                    break;
                case "ExpandBonusRequestButton":
                    //
                    SendButton.Name = "BonusButton";
                    PopulateRequestPropertiesPanel(ValidElements(new BonusRequest { }));
                    break;
                case "ExpandAuthorizeRequestButton":
                    //
                    SendButton.Name = "AuthorizeButton";
                    PopulateRequestPropertiesPanel(ValidElements(new AuthorizeRequest { }));
                    break;
                case "ExpandFinalizeRequestButton":
                    //
                    SendButton.Name = "FinalizeButton";
                    PopulateRequestPropertiesPanel(ValidElements(new FinalizeRequest { }));
                    break;
                case "ExpandReleaseRequestButton":
                    //
                    SendButton.Name = "ReleaseButton";
                    PopulateRequestPropertiesPanel(ValidElements(new ReleaseRequest { }));
                    break;
                case "ExpandPreauthorizationRequestButton":
                    //
                    SendButton.Name = "PreauthorizationButton";
                    PopulateRequestPropertiesPanel(ValidElements(new PreauthorizationRequest { }));
                    break;
                case "ExpandIncrementRequestButton":
                    //
                    SendButton.Name = "IncrementButton";
                    PopulateRequestPropertiesPanel(ValidElements(new IncrementRequest { }));
                    break;
                case "ExpandDecrementRequestButton":
                    //
                    SendButton.Name = "DecrementButton";
                    PopulateRequestPropertiesPanel(ValidElements(new DecrementRequest { }));
                    break;
                case "ExpandCompletionRequestButton":
                    //
                    SendButton.Name = "CompletionButton";
                    PopulateRequestPropertiesPanel(ValidElements(new CompletionRequest { }));
                    break;
                case "ExpandReleasePreauthorizationRequestButton":
                    //
                    SendButton.Name = "ReleasePreauthorizationButton";
                    PopulateRequestPropertiesPanel(ValidElements(new ReleasePreauthorizationRequest { }));
                    break;
                default:
                    Console.WriteLine("Unhandled button pressed: " + btn.Name);
                    break;

            }
        }

        private void PopulateRequestPropertiesPanel(List<int> ValidElements)
        {
            var RequestPanel = new UserControls.RequestPropertiesUserControl();
            if(ValidElements.Count > 0 )
            {
                foreach (var item in ValidElements)
                {
                    //Console.WriteLine("Element (" + item + ":" + ElementIDs[item]["name"] + ") found!");
                    Binding b = new Binding();
                    if (ElementIDs[item]["type"] == "bool")
                    {
                        StackPanel sp = new StackPanel();
                        sp.Orientation = Orientation.Horizontal;
                        sp.Margin = new Thickness(5,10,0,0);
                        Label lbl = new Label();
                        lbl.SetResourceReference(Label.StyleProperty, "MaterialDesignLabel");
                        lbl.Content = ElementIDs[item]["name"];

                        ToggleButton tglbtn = new ToggleButton();
                        //MaterialDesignThemes.Wpf.ToggleButtonAssist.SetOnContent(tglbtn, "MaterialDesignSwitchToggleButton");
                        //tglbtn.Style = MaterialDesignThemes.Wpf.ToggleButtonAssist.SetOnContent(tglbtn, "MaterialDesignSwitchToggleButton");
                        b.Source = Properties.TransactionSettings.Default;
                        b.Path = new PropertyPath(ElementIDs[item]["name"]);
                        tglbtn.SetBinding(ToggleButton.IsCheckedProperty, b);
                        tglbtn.SetResourceReference(ToggleButton.StyleProperty, "MaterialDesignSwitchToggleButton");

                        sp.Children.Add(lbl);
                        sp.Children.Add(tglbtn);

                        RequestPanel.RequestPropertiesPanel.Children.Add(sp);
                    }
                    else
                    {
                        TextBox tb = new TextBox();
                        tb.Margin = new Thickness(10);
                        MaterialDesignThemes.Wpf.HintAssist.SetHelperText(tb, ElementIDs[item]["name"]);
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(tb, "Enter " + ElementIDs[item]["name"]);
                        b.Source = Properties.TransactionSettings.Default;
                        b.Path = new PropertyPath(ElementIDs[item]["name"]);
                        tb.SetBinding(TextBox.TextProperty, b);
                        RequestPanel.RequestPropertiesPanel.Children.Add(tb);
                    }

                }
            }
            else
            {
                Label lbl = new Label();
                lbl.SetResourceReference(Label.StyleProperty, "MaterialDesignLabel");
                lbl.Content = "The request does not accept any elements.";
                RequestPanel.RequestPropertiesPanel.Children.Add(lbl);
            }
            this.Dispatcher.Invoke(() =>
            {
                //RequestPropertiesGroupBox.Header = 
                RequestPropertiesPanel.Children.Clear();
                RequestPropertiesPanel.Children.Add(RequestPanel);

            });
        }

        private void PrintReceipt(string receiptStr)
        {
            this.Dispatcher.Invoke(() =>
            {
                //Receipt_TextBlock.Text = receiptStr;
                

            });
        }


        public void ResponseCallback(object sender, MessageReceivedArgs e)
        {
            Response response = e.GetData();
            Console.WriteLine(DateTime.Now + ": " + response.GetMessageType);
            Console.WriteLine(nameof(this.ResponseCallback));

            if (response.GetMessageType == "GetInformationResponse" && response.EchoData == "ping")
            {
                ChangeConnectionStatus();
            }
            else
            {
                PrintToDebug(response);
                switch (response.GetMessageType)
                {
                    case "GetInformationResponse":
                        //Console.WriteLine(DateTime.Now + ": " + response.GetMessageType);
                        break;
                    case "ReceiptRequest":
                        Console.WriteLine(response.ReceiptData);
                        //PrintReceipt(receiptHandler.HandleReceiptData(response.ReceiptData));
                        this.Dispatcher.Invoke(() =>
                        {
                            ReceiptStackPanel.Children.Insert(0, receiptHandler.HandleReceiptData(response.ReceiptData));
                            //ReceiptDocument.RowGroups.Add(receiptHandler.HandleReceiptData(response.ReceiptData));

                        });
                        
                        break;
                    case "DialogRequest":
                        Console.WriteLine(DateTime.Now + ": " + response.DialogTitle);
                        Console.WriteLine(DateTime.Now + ": " + response.DialogText);
                        if (response.DialogTitle != null || response.DialogText != null)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                DialogTitle_Textbox.Text = response.DialogTitle;
                                DialogContent_Textbox.Text = response.DialogText;

                            });
                        }
                        switch (response.DialogType)
                        {
                            case 2:
                                // Confirmation 0=no, 1=yes
                                
                                //MaterialDesignThemes.Wpf.SnackbarMessage sbm = new MaterialDesignThemes.Wpf.SnackbarMessage();
                                //sbm.Content = "Enter Response: ";
                                //sbm.ActionContent = "Yes";
                                
                                //sbm.ActionClick = 
                                //sbm.Content = 
                                this.Dispatcher.Invoke(() =>
                                {
                                    //Grid DialogSnackbarGrid = new Grid();
                                    //DialogSnackbarGrid.RowDefinitions
                                    //StackPanel DialogResponseStackpanel = new StackPanel();
                                    //DialogResponseStackpanel.Orientation = Orientation.Horizontal;
                                    Button YesBtn = new Button();
                                    YesBtn.Name = "ButtonYes";
                                    YesBtn.Click += DialogReqestButtonClick;
                                    YesBtn.SetResourceReference(Button.StyleProperty, "MaterialDesignRaisedLightButton");
                                    YesBtn.Content = "Yes";
                                    YesBtn.Margin = new Thickness(5,0,0,5);
                                    YesBtn.FontSize = 12;
                                    //Grid.SetColumn(YesBtn, 2);
                                    //Grid.SetColumnSpan(YesBtn, 2);

                                    Button NoBtn = new Button();
                                    NoBtn.Name = "ButtonNo";
                                    NoBtn.Click += DialogReqestButtonClick;
                                    NoBtn.SetResourceReference(Button.StyleProperty, "MaterialDesignRaisedLightButton");
                                    NoBtn.Content = "No";
                                    NoBtn.Margin = new Thickness(0,0,5,5);
                                    NoBtn.FontSize = 12;
                                    //Grid.SetColumn(NoBtn, 0);
                                    //Grid.SetColumnSpan(NoBtn, 2);



                                    DialogResponseStackpanel.Children.Add(NoBtn);
                                    DialogResponseStackpanel.Children.Add(YesBtn);

                                    //DialogResponseSnackbarMsg.Content = DialogResponseStackpanel;
                                    //DialogResponseSnackbarGrid.Children.Add(NoBtn);
                                    //DialogResponseSnackbarGrid.Children.Add(YesBtn);
                                    
                                    DialogResponseSnackbar.IsActive = true;

                                });
                                
                                break;
                            case 3:
                                // Selection DialogOptions contains the valid selections
                                break;
                            case 4:
                                //Amount entry
                                break;
                            case 5:
                                // Numeric Entry
                                break;
                            case 6:
                                // Text Entry
                                break;
                            case 7:
                                // Password entry
                                break;
                            default:
                                break;
                        }

                        break;
                    case "SaleResponse":
                        Console.WriteLine(DateTime.Now + ": " + response.TransactionResult);
                        Console.WriteLine(DateTime.Now + ": " + response.TotalAmount);
                        break;
                    default:
                        //Console.WriteLine(DateTime.Now + ": " + response.GetMessageType);
                        break;
                }
            }


        }
        public void ErrorCallback(object sender, MessageReceivedArgs e)
        {
            Response response = e.GetData();
            PrintToDebug(response);

        }
        public void NetworkCallback(object sender, MessageReceivedArgs e)
        {
            Response response = e.GetData();
            PrintToDebug(response);

        }

        private void DialogReqestButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "ButtonYes":
                    SendRequestToTerminal(new DialogResponse { DialogResult = "1" });
                    break;
                case "ButtonNo":
                    SendRequestToTerminal(new DialogResponse { DialogResult = "0" });
                    break;
                default:
                    break;
            }
            
            this.Dispatcher.Invoke(() =>
            {
                DialogResponseSnackbar.IsActive = false;
                DialogResponseStackpanel.Children.Clear();
            });
           

        }
    }
}
