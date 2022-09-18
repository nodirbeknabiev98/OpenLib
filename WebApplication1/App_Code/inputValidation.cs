using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;


//For validation in other files
//Add this file with alias
//Create seperate function validateInput()


namespace usersInputValidation
{
    public class inputValidation
    {
       
        public static bool validateFullName(String input_fname)
        {
            bool valid = true;
            var regExVar = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z ]*$");

            if (regExVar.IsMatch(input_fname) == false)
            {
                valid = false;
            }

            if (input_fname.Trim().Length < 1)
            {
                valid = false;
            }

            
            return valid;
        }

        public static bool validatePhoneNumber(String input_pnum)
        {
            bool valid = true;
            String tempNum = input_pnum;
            var regExVar = new System.Text.RegularExpressions.Regex(@"^[0-9+.)(-]*$");

            if (tempNum.Contains("("))
            {
                tempNum = tempNum.Replace("(", "");
            }
            if (tempNum.Contains(")"))
            {
                tempNum = tempNum.Replace(")", "");
            }
            if (tempNum.Contains("-"))
            {
                tempNum = tempNum.Replace("-", "");
            }
            if (tempNum.Contains("+"))
            {
                tempNum = tempNum.Replace("+", "");
            }
            if (tempNum.Contains("."))
            {
                tempNum = tempNum.Replace(".", "");
            }
            if(tempNum.Trim().Length < 12 || regExVar.IsMatch(tempNum.Trim()) == false)
            {
                valid = false;
            }

            return valid;
        }


        public static bool validateEmail(String input_email)
        {

            bool valid = true;
            int index1 = input_email.IndexOf("@");
            int index2 = input_email.LastIndexOf("@");
            int num = input_email.Split('@').Length - 1;

            var regExVar = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9@.)(-]*$");

            if (input_email.Split('@').Length - 1 < 1 || regExVar.IsMatch(input_email) == false)
            {
                valid = false;
            }
            if(index1 != index2)
            {
                valid = false;
            }
            if(input_email.Trim() == "")
            {
                valid = false;

            }
                
            return valid;
        }


        public static bool validateCityName(String input_cname)
        {
            bool valid = true;
            var regExVar = new System.Text.RegularExpressions.Regex(@"^[a-zA-z]*$");

            if (input_cname.Trim().Length < 1 || regExVar.IsMatch(input_cname.Trim()) == false)
            {
                valid = false;
            }

            return valid;
        }

        public static bool validatePostalCode(String input_pcode)
        {
            bool valid = true;
            var regExVar = new System.Text.RegularExpressions.Regex(@"^[0-9]*$");

            if (input_pcode.Trim().Length < 1 || regExVar.IsMatch(input_pcode.Trim()) == false)
            {
                valid = false;
            }

            return valid;
        }


        public static bool validateFullAddress(String input_faddr)
        {
            bool valid = true;
            var regExvar = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9-, ]*$");

            if (input_faddr.Trim().Length < 1 || regExvar.IsMatch(input_faddr) == false)
            {
                valid = false;
            }

            return valid;
        }

        public static bool validateUserName(String input_uname) // UserName is the same as UserID,just different naming
        {
            bool valid = true;
            var regExvar = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9_ ]*$");

            if (input_uname.Trim().Length < 1 || regExvar.IsMatch(input_uname) == false)
            {
                valid = false;
            }

            return valid;
        }




        public static bool validatePassword(String input_pass)
        {
            bool valid = true;
            var regExvar = new System.Text.RegularExpressions.Regex(@"^[-a-zA-Z0-9!@#?%^&*()_+=[{]};:<>|./?]*$");

            if (input_pass.Trim().Length < 7  || regExvar.IsMatch(input_pass) == false)
            {
                valid = false;
            }

            return valid;

        }

        //-------------------------------------------------BOOK DETAILS INPUT VALIDITON----------------------------------------------------------//

        public static bool validateBookName(String input_fname)
        {
            bool valid = true;
            var regExVar = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z ]*$");

            if (regExVar.IsMatch(input_fname) == false)
            {
                valid = false;
            }

            if (input_fname.Trim().Length < 1)
            {
                valid = false;
            }


            return valid;
        }

        public static bool validateBookId(String input_uname)
        {
            bool valid = true;
            var regExvar = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9_ ]*$");

            if (input_uname.Trim().Length < 1 || regExvar.IsMatch(input_uname) == false)
            {
                valid = false;
            }

            return valid;
        }


        public static bool validateBookEdition(String input_uname)
        {
            bool valid = true;
            var regExvar = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9_ ]*$");

            if (input_uname.Trim().Length < 1 || regExvar.IsMatch(input_uname) == false)
            {
                valid = false;
            }

            return valid;
        }

        public static bool validateBookCost(String input_uname)
        {
            bool valid = true;
            var regExvar = new System.Text.RegularExpressions.Regex(@"^[0-9_$ ]*$");

            if (input_uname.Trim().Length < 1 || regExvar.IsMatch(input_uname) == false)
            {
                valid = false;
            }

            return valid;
        }
        public static bool validateNoOfPages(String input_uname)
        {
            bool valid = true;
            var regExvar = new System.Text.RegularExpressions.Regex(@"^[0-9 ]*$");

            if (input_uname.Trim().Length < 1 || regExvar.IsMatch(input_uname) == false)
            {
                valid = false;
            }

            return valid;
        }

        public static bool validateActualStock(String input_uname)
        {
            bool valid = true;
            var regExvar = new System.Text.RegularExpressions.Regex(@"^[0-9 ]*$");

            if (input_uname.Trim().Length < 1 || regExvar.IsMatch(input_uname) == false)
            {
                valid = false;
            }

            return valid;
        }

        public static bool validateBookDescription(String input_faddr)
        {
            bool valid = true;
            var regExvar = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9-,. ]*$");

            if (input_faddr.Trim().Length < 1 || regExvar.IsMatch(input_faddr) == false)
            {
                valid = false;
            }

            return valid;
        }


        ////////////////////////////////////////////////////////////////////////General///////////////////////////////////////////////////////
        public static bool validateDate(String input_date)
        {
            bool valid = true;
            var regExvar = new System.Text.RegularExpressions.Regex(@"^[0-9/-]*$");

            if (input_date.Trim().Length < 1 || regExvar.IsMatch(input_date) == false)
            {
                valid = false;
            }

            return valid;
        }




    }
}