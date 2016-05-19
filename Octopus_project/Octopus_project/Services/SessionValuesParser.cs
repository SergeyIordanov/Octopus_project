using System;

namespace Octopus_project.Services
{
    public class SessionValuesParser
    {
        public static bool FindIdFromLikesString(string value, int id)
        {
            string[] arr = value.Split('|');
            foreach (string s in arr)
                if (Convert.ToInt32(s) == id)
                    return true;
            return false;
        }
    }
}