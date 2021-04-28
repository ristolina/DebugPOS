using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DebugPOS
{
    class ReceiptHandler
    {
        private enum Alignment {Left, Center, Right };
        private bool italic = false;
        private bool bold = false;
        private bool doubleHeight = false;
        Alignment alignment;

        public Grid HandleReceiptData(string receiptData)
        {
            // Set default Alignment to Left
            alignment = Alignment.Left;
            // Create Grid to hold the receipt.
            Grid receiptGrid = new Grid();
            // Add Left, Center, Right columns to grid.
            receiptGrid.ColumnDefinitions.Add(new ColumnDefinition());
            receiptGrid.ColumnDefinitions.Add(new ColumnDefinition());
            receiptGrid.ColumnDefinitions.Add(new ColumnDefinition());
            
            
            // Validate that receiptData is not empty.
            if (receiptData != null)
            {
                // Split receiptData on newline character.
                string[] receiptDataArray = receiptData.Split((char)0x0A);

                // Process each line in receiptData
                foreach (string line in receiptDataArray)
                {
                    // Create Row to store the information for a specific line in the receipt.
                    RowDefinition row = new RowDefinition();
                    // Add row to Grid.
                    receiptGrid.RowDefinitions.Add(row);
                    //Console.WriteLine("Added row nr: " + receiptGrid.RowDefinitions.Count);

                    // FormatReceiptData will return a TextBlock[]. Add all the returned element to the row.
                    foreach (TextBlock tb in FormatReceiptData(line))
                    {
                        // Set row to current row.
                        Grid.SetRow(tb, receiptGrid.RowDefinitions.Count);
                        // Add element
                        receiptGrid.Children.Add(tb);
                        //Console.WriteLine("Added "  + tb.Text + " to col: " + Grid.GetColumn(tb));
                    }
                    
   
                }
                RowDefinition SeparatorRow = new RowDefinition();
                receiptGrid.RowDefinitions.Add(SeparatorRow);
                TextBlock SeparatorBlock = new TextBlock { Text = "*********************************************" };
                Grid.SetRow(SeparatorBlock, receiptGrid.RowDefinitions.Count);
                Grid.SetColumn(SeparatorBlock, 0);
                Grid.SetColumnSpan(SeparatorBlock, 3);
                receiptGrid.Children.Add(SeparatorBlock);
            }
            // Return the complete Grid
            return receiptGrid;
        }

        public TextBlock[] FormatReceiptData(string receiptDataLine)
        {

            //Receipt_TextBlock.Text = receiptStr;
            //Console.WriteLine(i + ": " + receiptDataArray[i]);            
                    
            // Split each line on control character 0x1b. 0x1b means that the following character has special meaning, like formatting etc.
            string[] txtToFormat = receiptDataLine.Split((char)0x1b);
            // isFirstChar is used to determine if the first char in the line is a special character or not.
            bool isFirstChar = true;

            // Create 3 textblocks, one for each column.
            TextBlock tbLeft = new TextBlock
            {
                TextAlignment = TextAlignment.Left,
                FontFamily = new FontFamily("Courier New"),
                ClipToBounds = false
            };
            // Set column to match the alignment.
            Grid.SetColumn(tbLeft, 0);
            TextBlock tbCenter = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                FontFamily = new FontFamily("Courier New"),
                ClipToBounds = false                
            };
            Grid.SetColumn(tbCenter, 1);
            TextBlock tbRight = new TextBlock
            {
                TextAlignment = TextAlignment.Right,
                FontFamily = new FontFamily("Courier New"),
                ClipToBounds = false
            };
            Grid.SetColumn(tbRight, 2);            

            // Process each part beginning with 0x1b.
            foreach (string part in txtToFormat)
            {
                // Placeholder for text.
                string receiptLine = "";

                // Set isFirstChar to false if length < 1.
                if (part.Length < 1)
                {
                    isFirstChar = false;
                    continue;
                }

                if (!isFirstChar)
                {     
                    // Switch case based on first character in string to handle formatting.
                    receiptLine = part.Substring(1);
                    switch (part[0])
                    {
                        case (char)0x20: // Above header
                        case (char)0x21: //Header                                   
                        case (char)0x22: // Below header
                        case (char)0x23: // Above footer
                        case (char)0x24: // Footer
                        case (char)0x25: // Below footer
                        case (char)0x26: // Content
                        case (char)0x27: // End of receipt
                        case (char)0x30: // All receipts
                        case (char)0x31: // Approved receipts
                        case (char)0x32: // Declined receipts
                        case (char)0x33: // Merchant receipts
                        case (char)0x34: // Cardholder receipts
                        case (char)0x40: // Left Aligned
                            alignment = Alignment.Left;
                            break;
                        case (char)0x41: // Center Aligned
                            alignment = Alignment.Center;
                    break;
                        case (char)0x42: // Right Aligned      
                            alignment = Alignment.Right;
                            break;
                        case (char)0x50: // Normal text
                            italic = false;
                            bold = false;
                            doubleHeight = false;
                            break;
                        case (char)0x51: // Italic text
                            italic = true;
                            break;
                        case (char)0x52: // Bold text
                            bold = true;
                            break;
                        case (char)0x53: // Double height
                            doubleHeight = true;
                            break;
                        case (char)0x54: // 1.5x height
                        case (char)0x60: // EAN8 barcode START
                        case (char)0x61: // EAN8 barcode STOP
                        case (char)0x62: // EAN13 barcode START
                        case (char)0x63: // EAN13 barcode STOP
                        case (char)0x64: // Reserved
                        case (char)0x65: // Reserved
                        case (char)0x70: // Horizontal separator (---------)
                            //receiptLine = "----------------------------------------\n";
                            break;
                        case (char)0x71: // Pad with following character, e.g. ‘_’ as long as the receipt width.
                        default:
                            receiptLine = part;
                            break;
                    }
                }
                else
                {
                    // Set firstChar to false and add part to receiptLine
                    isFirstChar = false;
                    receiptLine = part;
                }
                
                // Remove newline character.
                //receiptLine.TrimEnd((char)0x0A);
                // Start formatting
                if (receiptLine.Length > 0)
                {
                    // Fontweight formatting
                    // Not displaying correctly in TextBlock.
                    if (bold)
                    {
                        //tb.FontWeight = FontWeights.Bold;

                        tbLeft.FontWeight = FontWeights.Bold;
                        tbCenter.FontWeight = FontWeights.Bold;
                        tbRight.FontWeight = FontWeights.Bold;
                        
                    }
                    else
                    {
                        //tb.FontWeight = FontWeights.Normal;
                        
                        tbLeft.FontWeight = FontWeights.Normal;
                        tbCenter.FontWeight = FontWeights.Normal;
                        tbRight.FontWeight = FontWeights.Normal;
                        
                    }

                    if (italic)
                    {
                        //tb.FontWeight = FontWeights.Normal;
                        
                        tbLeft.FontWeight = FontWeights.Normal;
                        tbCenter.FontWeight = FontWeights.Normal;
                        tbRight.FontWeight = FontWeights.Normal;

                        tbLeft.FontStyle = FontStyles.Italic;
                        tbCenter.FontStyle = FontStyles.Italic;
                        tbRight.FontStyle = FontStyles.Italic;

                    }
                    else
                    {
                        //tb.FontWeight = FontWeights.Normal;
                        
                        tbLeft.FontWeight = FontWeights.Normal;
                        tbCenter.FontWeight = FontWeights.Normal;
                        tbRight.FontWeight = FontWeights.Normal;

                        tbLeft.FontStyle = FontStyles.Normal;
                        tbCenter.FontStyle = FontStyles.Normal;
                        tbRight.FontStyle = FontStyles.Normal;

                    }

                    if (doubleHeight)
                    {
                    }
                    else
                    {
                    }
                    // Alignment formatting
                    if (alignment == Alignment.Left)
                    {
                        //tb.Text = receiptLine;
                        //Grid.SetColumn(tb, 0);

                        tbLeft.Text= receiptLine;

                    }

                    if (alignment == Alignment.Center)
                    {
                        /*
                        tb.TextAlignment = TextAlignment.Center;
                        tb.HorizontalAlignment = HorizontalAlignment.Center;
                        tb.Text = receiptLine;
                        Grid.SetColumn(tb, 1);
                        */
                        
                        tbCenter.TextAlignment = TextAlignment.Center;
                        tbCenter.Text = receiptLine;
                        tbCenter.HorizontalAlignment = HorizontalAlignment.Center;
                        
                        
                    }

                    if (alignment == Alignment.Right)
                    {
                        /*
                        tb.TextAlignment = TextAlignment.Right;
                        tb.Text = receiptLine;
                        Grid.SetColumn(tb, 2);
                        */
                        
                        tbRight.TextAlignment = TextAlignment.Right;
                        tbRight.Text = receiptLine;    
                        
                    }

                    
                    //Console.WriteLine("Processed line, align " + alignment);
                    //Console.WriteLine(receiptLine);       
                }
            }
            // If only Right contains data: set columnspan to 3
            if (tbLeft.Text == "" & tbCenter.Text == "")
            {
                Grid.SetColumnSpan(tbRight, 3);
                tbRight.HorizontalAlignment = HorizontalAlignment.Right;
            }
            // If only Left contains data: set columnspan to 3
            else if (tbCenter.Text == "" & tbRight.Text == "")
            {
                Grid.SetColumnSpan(tbLeft, 3);
                tbLeft.HorizontalAlignment = HorizontalAlignment.Left;
            }
            // If only Center contains data: set columnspan to 3 and column to 0 to center text in row
            else if (tbLeft.Text == "" & tbRight.Text == "")
            {
                Grid.SetColumnSpan(tbCenter, 3);
                Grid.SetColumn(tbCenter, 0);
                tbCenter.HorizontalAlignment = HorizontalAlignment.Center;
            }
            // If Left and Right contains data: set columnspan to 2 and Right column to 2
            else if (tbCenter.Text == "")
            {
                Grid.SetColumnSpan(tbLeft, 2);
                Grid.SetColumn(tbLeft, 0);
                tbLeft.HorizontalAlignment = HorizontalAlignment.Left;
                Grid.SetColumnSpan(tbRight, 2);
                Grid.SetColumn(tbRight, 1);
                tbRight.HorizontalAlignment = HorizontalAlignment.Right;
            }          


            //Console.WriteLine("L: " + left.ColumnSpan + ", C: " + center.ColumnSpan + ", R: " + right.ColumnSpan);
            //Console.WriteLine("Left content: " + left.Blocks.FirstBlock); 
            // Add textblocks to array
            TextBlock[] textBlocks = { tbLeft, tbCenter, tbRight };

            // Return Array
            return textBlocks;
        }
    }
}
