using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ID3_Hunter
{
    class Utilities
    {        
        public static void GenericCatchBlock(Exception ex)
        {
            System.Diagnostics.StackTrace stkExceptionStack;
            int intExceptionLineNum;
            string strExceptionFormName;
            string strExceptionMessage;

            stkExceptionStack = new System.Diagnostics.StackTrace(ex, true);
            intExceptionLineNum = stkExceptionStack.GetFrame(stkExceptionStack.FrameCount - 1).GetFileLineNumber();
            strExceptionFormName = stkExceptionStack.GetFrame(stkExceptionStack.FrameCount - 1).GetMethod().DeclaringType.ToString();
            strExceptionMessage = ex.Message;

            if (strExceptionMessage == "Parameter is not valid.")
            {
                strExceptionMessage = "Parameter is not valid. This is likely because the album artwork is not stored in valid picture format. Try resetting the album artwork of this file.";
            }
            if (strExceptionMessage == "The remote server returned an error: (400) Bad Request.")
            {
                strExceptionMessage = "The remote server returned an error: (400) Bad Request. This is likely because the song could not be found on Last.FM. Try again later.";
            }

            MessageBox.Show(strExceptionMessage + "\n\n" + "Debug Info: \nLine #: " + intExceptionLineNum + " on " + strExceptionFormName, 
                            "ID3 Hunter - Exception Raised", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void CheckToolstripEnable(ref ToolStrip tsForCheck)
        {
            ToolStripItem tsbtnMoveFirst = tsForCheck.Items["tsbtnMoveFirst"];
            ToolStripItem tsbtnMovePrevious = tsForCheck.Items["tsbtnMovePrevious"];
            ToolStripItem tslblCurrentPosition = tsForCheck.Items["tslblCurrentPosition"];
            ToolStripItem tslblTotalSongs = tsForCheck.Items["tslblItemCount"];
            ToolStripItem tsbtnMoveNext = tsForCheck.Items["tsbtnMoveNext"];
            ToolStripItem tsbtnMoveLast = tsForCheck.Items["tsbtnMoveLast"];

            //easier to just disable as needed instead of enabling/disabling as needed
            //so just enable all to start, and disable what we need to
            tsbtnMoveFirst.Enabled = true;
            tsbtnMoveLast.Enabled = true;
            tsbtnMoveNext.Enabled = true;
            tsbtnMovePrevious.Enabled = true;

            //no songs so disable all navigation
            if (tslblTotalSongs.Text == "0")
            {
                tsbtnMoveFirst.Enabled = false;
                tsbtnMoveLast.Enabled = false;
                tsbtnMoveNext.Enabled = false;
                tsbtnMovePrevious.Enabled = false;
                return;
            }

            //at the end, so disable next/last navigation
            if (tslblCurrentPosition.Text == tslblTotalSongs.Text)
            {
                tsbtnMoveLast.Enabled = false;
                tsbtnMoveNext.Enabled = false;
                return;
            }

            //at the beginning, so disable previous/first navigation
            if (tslblCurrentPosition.Text == "1")
            {
                tsbtnMoveFirst.Enabled = false;
                tsbtnMovePrevious.Enabled = false;
                return;
            }
        }

        public static bool IsDate(string strDateToCheck)
        {
            try
            {
                DateTime dt = DateTime.Parse(strDateToCheck);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
