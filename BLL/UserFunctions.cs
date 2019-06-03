using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{

   public static class UserFunctions
    {
        static AshavatAvedaEntities db = new AshavatAvedaEntities();
        //null פונקצית כניסה האם המשתמש קים ומחזירה את המשתמש שנמצא או 
        public static DTO.UserDTO Login(string userRecognizeName, string password)
        {

            DAL.User u1 = db.Users.FirstOrDefault(u => u.UserRecognizeName == userRecognizeName && u.UserPassword == password);
            if (u1 != null)
            {
                return BLL.Convertions.UserToDto(u1);
            }
            return null;

        }
        //פונקצית הרשמת משתמש
        public static DTO.UserDTO Register(DTO.UserDTO user)
        {
            DAL.User u1 = BLL.Convertions.UserDtoToDAL(user);
            if (Login(u1.UserRecognizeName, u1.UserPassword) == null)
            {
                u1.RoleId = 3;
                db.Users.Add(u1);
                db.SaveChanges();
                return BLL.Convertions.UserToDto(u1);

            }
            else
            {
                return null;
            }

        }
        //פונקציה שמציגה רשימת משתמשים:
        public static List<DTO.UserDTO> UserList()
        {
            List<DAL.User> u = db.Users.ToList();
            if(u!=null)
            {
                List<DTO.UserDTO> u1=new List<UserDTO>();
                foreach (var item in u)
                {
                    u1.Add(BLL.Convertions.UserToDto(item));
                }
                return u1;
            }
            return null;
        }
       
    }
}
