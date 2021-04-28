using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Bambora.Connect2T;


namespace DebugPOS
{
    class Helpers
    {



        public Transaction PopulateTransactionProperties(Transaction trx)
        {
            foreach (System.Reflection.PropertyInfo prop in trx.GetType().GetProperties())
            {
                //Console.WriteLine("Prop.Name: " + prop.Name);
                //Console.WriteLine("Prop.Value: " + trx.GetType().GetProperty(prop.Name).GetValue(trx));
                /*
                Console.WriteLine(
                    "Setting: " +
                    Properties.TransactionSettings.Default.GetType().GetProperty(prop.Name).Name + " : " +
                    Properties.TransactionSettings.Default.GetType().GetProperty(prop.Name).GetValue(Properties.TransactionSettings.Default)
                    );
                    */
                try
                {
                    Console.WriteLine(Properties.TransactionSettings.Default[prop.Name]);
                    if (//true
                          Properties.TransactionSettings.Default.GetType().GetProperty(prop.Name).GetValue(Properties.TransactionSettings.Default) != null &
                          Convert.ToString(Properties.TransactionSettings.Default.GetType().GetProperty(prop.Name).GetValue(Properties.TransactionSettings.Default)) != ""
                          )
                    {
                        /*
                        Console.WriteLine(
                            "PropertyName: " +
                            Properties.TransactionSettings.Default.GetType().GetProperty(prop.Name).Name + ", PropertyValue: " +
                            Properties.TransactionSettings.Default.GetType().GetProperty(prop.Name).GetValue(Properties.TransactionSettings.Default)
                            );
                            */
                        switch (trx.GetType().GetProperty(prop.Name).PropertyType.ToString())
                        {
                            case "System.Nullable`1[System.Int32]":
                                var int_value = Int32.Parse(Properties.TransactionSettings.Default.GetType().GetProperty(prop.Name).GetValue(Properties.TransactionSettings.Default).ToString());
                                //Console.WriteLine("Value: " + int_value);
                                trx.GetType().GetProperty(prop.Name).SetValue(trx, int_value);
                                break;
                            case "System.Nullable`1[System.Boolean]":
                                bool? bool_value = (bool?)Properties.TransactionSettings.Default.GetType().GetProperty(prop.Name).GetValue(Properties.TransactionSettings.Default);
                                //Console.WriteLine("Type: " + bool_value.GetType());
                                trx.GetType().GetProperty(prop.Name).SetValue(trx, bool_value);
                                break;
                            case "System.Nullable`1[System.Char]":
                                char? char_value = Properties.TransactionSettings.Default.GetType().GetProperty(prop.Name).GetValue(Properties.TransactionSettings.Default).ToString()[0];
                                trx.GetType().GetProperty(prop.Name).SetValue(trx, char_value);
                                break;
                            default:
                                var value = Convert.ChangeType(
                                    Properties.TransactionSettings.Default.GetType().GetProperty(prop.Name).GetValue(Properties.TransactionSettings.Default),
                                    trx.GetType().GetProperty(prop.Name).PropertyType,
                                    null
                                    );
                                //Console.WriteLine("Value: " + value);
                                trx.GetType().GetProperty(prop.Name).SetValue(trx, value);
                                break;
                        }

                        //trx.GetType().GetProperty(prop.Name).SetValue(trx, value);
                        /*
                        Console.WriteLine(
                            "prop.Name: " +
                            prop.Name + " : " +
                            trx.GetType().GetProperty(prop.Name).GetValue(trx)
                            );
                          */  
                    }
                }
                catch (System.Configuration.SettingsPropertyNotFoundException ex)
                {

                    Console.WriteLine("Property " + prop.Name + " not found in Properties.TransactionSettings.Default!");
                    Console.WriteLine(ex);
                }
            }
            return trx;
        }
    }
}
