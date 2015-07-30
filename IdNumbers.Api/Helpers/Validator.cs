using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdNumbers.Api.Helpers
{
    public class Validator
    {
        //Format: {YYMMDD}{G}{SSS}{C}{A}{Z}
        //YYMMDD : Date of birth.
        //G  : Gender. 0-4 Female; 5-9 Male.
        //SSS  : Sequence No. for DOB/G combination.
        //C  : Citizenship. 0 SA; 1 Other.
        //A  : Usually 8, or 9 [can be other values]
        //Z  : Control digit calculated in the following section:

        //Formula to calculate the check digit for a 13 digit identity number:

        //According to the provisions of the Identification Amendment Act, 2000 (Act No. 28 of 2000,
        //which was promulgated on 13 October 2000) all forms of identity documents other than the
        //green bar-coded identity document are invalid. [my observation: the following algorithm appears to work for the older 'blue' - book id numbers as well].  In accordance with the legislation, 
        //the control figure which is the 13th digit of all identity numbers which have 08 and 09 is 
        //calculated as follows using ID Number 800101 5009 087 as an example:

        //Add all the digits in the odd positions(excluding last digit).
        //  8 + 0 + 0 + 5 + 0 + 0 = 13...................[1]
        //Move the even positions into a field and multiply the number by 2.
        //  011098 x 2 = 22196
        //Add the digits of the result in b).
        //  2 + 2 + 1 + 9 + 6 = 20.........................[2]
        //Add the answer in [2]
        //to the answer in [1].
        //  13 + 20 = 33
        //Subtract the second digit(i.e. 3) from 10.  The number must tally with the last number in the ID Number.If the result is 2 digits, the last digit is used to compare against the last number in the ID Number.If the answer differs, the ID number is invalid.

        

        public static bool IsValidIdNumber(string id)
        {
            if (!id.IsNumeric(id))
                return false;
            return Convert.ToInt32(id.Substring(12, 1)) == GetControlDigit(id);
        }
    }

    public static class StringExtensions
    {
        public static bool IsNumeric(this string s, string id)
        {
            return IsValidInput(id);
        }

        private static bool IsValidInput(string id)
        {
            bool isValid = true;
            for (int i = 0; i < id.Length; i++)
            {
                if (!Char.IsDigit(id[i]))
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        public static bool IsValid(this string s, string id)
        {
            return Convert.ToInt32(id.Substring(12, 1)) == GetControlDigit(id);
        }

        private static int GetControlDigit(string parsedId)
        {
            int d = -1;
            try
            {
                int a = 0;
                for (int i = 0; i < 6; i++)
                {
                    a += int.Parse(parsedId[2 * i].ToString());
                }
                int b = 0;
                for (int i = 0; i < 6; i++)
                {
                    b = b * 10 + int.Parse(parsedId[2 * i + 1].ToString());
                }
                b *= 2;
                int c = 0;
                do
                {
                    c += b % 10;
                    b = b / 10;
                }
                while (b > 0);
                c += a;
                d = 10 - (c % 10);
                if (d == 10) d = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return d;
        }
    }
}
